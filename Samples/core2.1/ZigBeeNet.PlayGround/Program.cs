using System;
using System.Linq;
using Serilog;
using ZigBeeNet.App;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.CC;
using ZigBeeNet.Serial;
using ZigBeeNet.Transaction;
using ZigBeeNet.Transport;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Clusters;
using ZigBeeNet.ZCL.Clusters.LevelControl;

namespace ZigBeeNet.PlayGround
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .CreateLogger();

            try
            {
                ZigBeeSerialPort zigbeePort = new ZigBeeSerialPort("COM3");

                IZigBeeTransportTransmit dongle = new ZigBeeDongleTiCc2531(zigbeePort);

                ZigBeeNetworkManager networkManager = new ZigBeeNetworkManager(dongle);

                //ZigBeeDiscoveryExtension discoveryExtension = new ZigBeeDiscoveryExtension();
                //discoveryExtension.setUpdatePeriod(60);
                //networkManager.AddExtension(discoveryExtension);

                // Initialise the network
                networkManager.Initialize();

                networkManager.AddCommandListener(new ZigBeeNetworkDiscoverer(networkManager));
                //networkManager.AddCommandListener(new ZigBeeDiscoveryExtension());
                networkManager.AddCommandListener(new ZigBeeTransaction(networkManager));
                networkManager.AddCommandListener(new ConsoleCommandListener());
                networkManager.AddNetworkNodeListener(new ConsoleNetworkNodeListener());

                networkManager.AddSupportedCluster(0x06);
                networkManager.AddSupportedCluster(0x08);

                ZigBeeStatus startupSucceded = networkManager.Startup(false);

                if (startupSucceded == ZigBeeStatus.SUCCESS)
                {
                    Log.Logger.Information("ZigBee console starting up ... [OK]");
                }
                else
                {
                    Log.Logger.Information("ZigBee console starting up ... [FAIL]");
                    return;
                }

                ZigBeeNode coord = networkManager.GetNode(0);

                coord.PermitJoin(true);

                Console.WriteLine("Joining enabled...");

                string cmd = Console.ReadLine();

                while (cmd != "exit")
                {
                    Console.WriteLine(networkManager.GetNodes().Count + " node(s)");

                    if (!string.IsNullOrEmpty(cmd))
                    {
                        //Console.WriteLine("Destination Address: ");
                        //string nwkAddr = Console.ReadLine();
                        string nwkAddr = "35468";

                        if (ushort.TryParse(nwkAddr, out ushort addr))
                        {
                            var node = networkManager.GetNode(addr);

                            if (node != null)
                            {
                                ZigBeeEndpoint ep = new ZigBeeEndpoint(node, 1);
                                node.AddEndpoint(ep);

                                try
                                {
                                    if(cmd == "bind")
                                    {
                                        //ZigBeeEndpoint sourceEndpoint;
                                        //foreach (ZigBeeNode znode in networkManager.GetNodes())
                                        //{
                                        //    foreach (var endpoint in znode.Endpoints)
                                        //    {
                                        //        if (endpointId.Equals(znode.NetworkAddress + "/" + endpoint.Value.EndpointId))
                                        //        {
                                        //            sourceEndpoint = endpoint;
                                        //            break;
                                        //        }
                                        //    }
                                        //}

                                         //= getEndpoint(networkManager, sourceEndpointParam);
                                        //ZclCluster cluster = getCluster(sourceEndpoint, clusterSpecParam);

                                        ZigBeeEndpoint sourceEndpoint = networkManager.GetNodes().Last().Endpoints[0];
                                        ZclLevelControlCluster cluster = new ZclLevelControlCluster(sourceEndpoint, networkManager);
                                        //ZclCluster cluster = sourceEndpoint.GetInputCluster(sourceEndpoint.GetInputClusterIds().FirstOrDefault());

                                        IeeeAddress destAddress;
                                        byte destEndpoint;

                                        destAddress = networkManager.GetNodes().First().IeeeAddress;
                                        destEndpoint = 1;

                                        cluster.Bind(destAddress, destEndpoint);

                                        //if (destEndpointParam != null)
                                        //{
                                        //    ZigBeeEndpoint destination = getEndpoint(networkManager, destEndpointParam);
                                        //    destAddress = destination.getIeeeAddress();
                                        //    destEndpoint = destination.getEndpointId();
                                        //}
                                        //else
                                        //{
                                        //destAddress = networkManager.GetNode(0).IeeeAddress;
                                        //destEndpoint = 1;
                                        //}

                                        //ZclLevelControlCluster level = new ZclLevelControlCluster(node.GetEndpoint(0), networkManager);
                                        //level.Bind(node.IeeeAddress, 0);
                                    }
                                    else if (cmd == "toggle")
                                    {
                                        ZclOnOffCluster onOff = new ZclOnOffCluster(node.GetEndpoint(0), networkManager);

                                        var result = onOff.ToggleCommand().GetAwaiter().GetResult();
                                    }
                                    else if (cmd == "level")
                                    {
                                        Console.WriteLine("Level between 0 and 255: ");
                                        string level = Console.ReadLine();

                                        MoveToLevelCommand command = new MoveToLevelCommand();

                                        // Set the fields
                                        command.SetLevel((byte)int.Parse(level));
                                        command.SetTransitionTime(0);

                                        networkManager.Send(new ZigBeeEndpointAddress(node.NetworkAddress, 1), command).GetAwaiter().GetResult();
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        }
                    }

                    cmd = Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class ConsoleCommandListener : IZigBeeCommandListener
    {
        public void CommandReceived(ZigBeeCommand command)
        {
            //Console.WriteLine(command);
        }
    }

    public class ConsoleNetworkNodeListener : IZigBeeNetworkNodeListener
    {
        public void NodeAdded(ZigBeeNode node)
        {
            Console.WriteLine("Node " + node.IeeeAddress + " added " + node);
            if (node.NetworkAddress != 0)
            {
                //    ZclOnOffCluster onOff = new ZclOnOffCluster(node.GetEndpoint(0));

                //    onOff.ToggleCommand();
            }
        }

        public void NodeRemoved(ZigBeeNode node)
        {
            Console.WriteLine("Node removed " + node);
        }

        public void NodeUpdated(ZigBeeNode node)
        {
            Console.WriteLine("Node updated " + node);
        }
    }
}
