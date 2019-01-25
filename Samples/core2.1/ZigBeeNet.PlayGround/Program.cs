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
using ZigBeeNet.ZCL.Clusters.OnOff;

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

                coord.PermitJoin(false);

                //Console.WriteLine("Joining enabled...");

                ZigBeeNode light = new ZigBeeNode(networkManager, new IeeeAddress(BitConverter.GetBytes(3192981736732173)));
                light.NetworkAddress = 35468;
                networkManager.AddNode(light);

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
                                var endpointAddress = new ZigBeeEndpointAddress(node.NetworkAddress, 1);
                                var endpoint = new ZigBeeEndpoint(node, 1);
                                node.AddEndpoint(endpoint);

                                try
                                {
                                    if (cmd == "bind")
                                    {
                                        var sourceEndpoint = new ZigBeeEndpoint(coord, 1);
                                        ZclCluster cluster = new ZclOnOffCluster(sourceEndpoint, networkManager);
                                        cluster.Bind(node.IeeeAddress, 1);
                                    }
                                    else if (cmd == "toggle")
                                    {
                                        networkManager.Send(endpointAddress, new ToggleCommand()).GetAwaiter().GetResult();
                                    }
                                    else if (cmd == "level")
                                    {
                                        Console.WriteLine("Level between 0 and 255: ");
                                        string level = Console.ReadLine();

                                        Console.WriteLine("time between 0 and 65535: ");
                                        string time = Console.ReadLine();

                                        var command = new MoveToLevelWithOnOffCommand(byte.Parse(level), ushort.Parse(time));

                                        networkManager.Send(endpointAddress, command).GetAwaiter().GetResult();
                                    }
                                    else if (cmd == "move")
                                    {
                                        networkManager.Send(endpointAddress, new MoveCommand(1, 100)).GetAwaiter().GetResult();
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
