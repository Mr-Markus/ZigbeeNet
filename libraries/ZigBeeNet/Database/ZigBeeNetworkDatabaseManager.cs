using Serilog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace ZigBeeNet.Database
{
    /// <summary>
    /// This class implements the management functions for the network database. The network database persists data about the
    /// nodes that are currently part of the network between restarts of the system.
    /// 
    /// The database manager will not normally write data to the data store immediately. It will instead defer the write for
    /// the deferredWriteTime. If there are many consecutive writes that continue to retrigger the deferred timer,
    /// then after deferredWriteTimeout period, the node will be written to the data store.
    /// 
    /// All writes to the ZigBeeDataStore are managed through a single thread scheduler to ensure that only a single
    /// write is in progress at once. This allows the data store to be kept simple and ensures writes don't get queued thus
    /// causing performance issues or multiple threads to be executed.
    /// </summary>
    public class ZigBeeNetworkDatabaseManager : IZigBeeNetworkNodeListener
    {
        /// <summary>
        ///The default time(in milliseconds) to defer writes.This will prevent multiple writes to a single node within this period.
        /// </summary>
        private const int DEFERRED_WRITE_DEFAULT = 250;

        /// <summary>
        ///The default timeout after which continued retriggers of the deferred timer will force a write.
        /// </summary>
        private const int DEFERRED_WRITE_TIMEOUT = 1000;

        /// <summary>
        ///The maximum time that the user can set the deferred timeout to.
        /// </summary>
        private const int DEFERRED_WRITE_TIMEOUT_MAX = 10000;

        /// <summary>
        ///The time to wait for all threads to shutdown in milliseconds
        /// </summary>
        private const int SHUTDOWN_TIMEOUT = 3000;

        /// <summary>
        /// the IZigBeeNetworkDataStore that will be used to store the data
        /// </summary>
        public IZigBeeNetworkDataStore DataStore { get; set; }

        /// <summary>
        /// The ZigBeeNetworkManager that this database is linked to
        /// </summary>
        private ZigBeeNetworkManager _networkManager;

        /// <summary>
        /// The time to defer writes.If the node is not updated for this number of milliseconds, the data will be written.
        /// </summary>
        private int _deferredWriteTime = DEFERRED_WRITE_DEFAULT;

        /// <summary>
        /// The maximum amount of time we will defer writes in milliseconds
        /// </summary>
        private long _deferredWriteTimeout = DEFERRED_WRITE_TIMEOUT;

        /// <summary>
        /// Map of deferred write timer for each node
        /// </summary>
        private ConcurrentDictionary<IeeeAddress, DeferedWritetimer> _deferredWriteTimers = new ConcurrentDictionary<IeeeAddress, DeferedWritetimer>();

        /// <summary>
        /// The list of nodes that need to be processed by the writer task
        /// </summary>
        private BlockingCollection<ZigBeeNode> _nodesToSave;

        /// <summary>
        /// Creates the database manager
        /// </summary>
        /// <param name="networkManager"> the ZigBeeNetworkManager to which this database is linked </param>
        public ZigBeeNetworkDatabaseManager(ZigBeeNetworkManager networkManager) 
        {
            _networkManager = networkManager;
        }

        /// <summary>
        /// Sets the deferred write timer. This is used to avoid writing to the data store too often, which may occur during
        /// network discovery when a node is being updated often
        /// </summary>
        /// <param name="deferredWriteTime">the number of milliseconds to wait before writing to the store</param>
        public void SetDeferredWriteTime(int deferredWriteTime) 
        {
            if (deferredWriteTime > DEFERRED_WRITE_TIMEOUT_MAX) 
            {
                _deferredWriteTime = DEFERRED_WRITE_TIMEOUT_MAX;
                return;
            }
            _deferredWriteTime = deferredWriteTime;
        }

        /// <summary>
        /// Sets the maximum deferred write timer. This will ensure that the data store is updated periodically in the event
        /// that continuous updates are received for a node.
        /// </summary>
        /// <param name="deferredWriteMax">the maximum number of milliseconds that writes will be deferred</param>
        public void SetMaxDeferredWriteTime(int deferredWriteMax) 
        {
            if (deferredWriteMax > DEFERRED_WRITE_TIMEOUT_MAX) 
            {
                _deferredWriteTimeout = DEFERRED_WRITE_TIMEOUT_MAX;
                return;
            }
            _deferredWriteTimeout = deferredWriteMax;
        }

        /// <summary>
        /// Clears all data from the data store. This may be used when initialising a network to remove any previous data.
        /// </summary>
        public void Clear() 
        {
            if (DataStore == null) 
            {
                Log.Debug("Data store: Undefined so network is not cleared.");
                return;
            }

            Log.Debug("Data store:  Clearing all nodes.");
            ISet<IeeeAddress> nodes = DataStore.ReadNetworkNodes();
            foreach (IeeeAddress nodeAddress in nodes) 
            {
                DataStore.RemoveNode(nodeAddress);
            }
        }

        /// <summary>
        /// Starts the database manager. This will call the ZigBeeNetworkDataStore to retrieve the list of nodes, and
        /// then read all the nodes from the store, adding them to the ZigBeeNetworkManager.
        /// </summary>
        public void Startup() 
        {
            if (DataStore == null) 
            {
                Log.Debug("Data store: Undefined so network is not restored.");
                return;
            }

            ISet<IeeeAddress> nodes = DataStore.ReadNetworkNodes();
            foreach (IeeeAddress nodeAddress in nodes) 
            {
                ZigBeeNode node = new ZigBeeNode(_networkManager, nodeAddress);
                ZigBeeNodeDao nodeDao = DataStore.ReadNode(nodeAddress);
                if (nodeDao == null) 
                {
                    Log.Debug("{IeeeAddress}: Data store: Node was not found in database.", nodeAddress);
                    continue;
                }
                node.SetDao(nodeDao);
                Log.Debug("{IeeeAddress}: Data store: Node was restored.", nodeAddress);
                _networkManager.UpdateNode(node);
            }

            _nodesToSave = new BlockingCollection<ZigBeeNode>();

            //The consumer task that process the nodes produced by the elapsed timers
            //This ensure all write are done using a single thread.
            Task.Factory.StartNew(WriteNodeLoop, TaskCreationOptions.LongRunning);

            _networkManager.AddNetworkNodeListener(this);
        }

        /// <summary>
        /// Shuts down the database. This ensures that any outstanding writes are closed, thus ensuring the current state of
        /// the network is saved.
        /// </summary>
        public void Shutdown() 
        {
            Log.Debug("Data store: Shutting down.");
            _networkManager.RemoveNetworkNodeListener(this);

            _nodesToSave.CompleteAdding();
        }

        public void NodeAdded(ZigBeeNode node) 
        {
            NodeUpdated(node);
        }

        public void NodeUpdated(ZigBeeNode node) 
        {
            if (DataStore == null)
                return;

            SaveNode(node);
        }

        public void NodeRemoved(ZigBeeNode node) 
        {
            if (DataStore == null)
                return;

            DataStore.RemoveNode(node.IeeeAddress);
        }

        private class DeferedWritetimer : System.Timers.Timer
        {
            public DateTime CreatedTime { get; set; }
            public ZigBeeNode Node { get; set; }
        }

        private void SaveNode(ZigBeeNode node) 
        {
            int deferredDelay = _deferredWriteTime;

            lock (_deferredWriteTimers) 
            {
                DeferedWritetimer timer;
                if (_deferredWriteTimers.TryGetValue(node.IeeeAddress, out timer)) 
                {
                    if (DateTime.Now - timer.CreatedTime > TimeSpan.FromMilliseconds(_deferredWriteTimeout))
                    {
                        Log.Debug("{IeeeAddress}: Data store: Maximum deferred time reached.", node.IeeeAddress);

                        // Run the write immediately.
                        deferredDelay = 1;
                    }

                    timer.Interval = deferredDelay;
                    timer.Start();
                } 
                else 
                {
                    // First deferred write - save the time
                    timer = new DeferedWritetimer();
                    timer.Node = node;
                    timer.CreatedTime = DateTime.Now;
                    timer.AutoReset = false;
                    timer.Interval = deferredDelay;
                    timer.Elapsed += new ElapsedEventHandler(OnDeferedWriteTimerElapsedEvent);
                    _deferredWriteTimers.TryAdd(node.IeeeAddress, timer);
                    timer.Start();
                }

                Log.Debug("{IeeeAddress}: Data store: Deferring write for {deferredDelay}ms.", node.IeeeAddress, deferredDelay);
            }
        }

        private void OnDeferedWriteTimerElapsedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                lock (_deferredWriteTimers)
                {
                    DeferedWritetimer timer = (DeferedWritetimer)sender;
                    DeferedWritetimer timerOut;
                    _deferredWriteTimers.TryRemove(timer.Node.IeeeAddress, out timerOut);
                    _nodesToSave.Add(timer.Node);
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex, "Data store: error in timer elapsed event");
            }
        }


        private void WriteNodeLoop()
        {
            try
            {
                foreach (ZigBeeNode node in _nodesToSave.GetConsumingEnumerable())
                {
                    try
                    {
                        Log.Debug("{IeeeAddress}: Data store: Writing node.", node.IeeeAddress);
                        DataStore.WriteNode(node.GetDao());
                    }
                    catch (Exception ex)
                    {
                        Log.Debug(ex, "{IeeeAddress}: Data store: error while writing node", node.IeeeAddress);
                    }
                }
            }
            catch(Exception ex)
            {
                Log.Debug(ex, "Data store: Write Node Loop error");
            }
        }
    }

}
