using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Serilog;
using ZigBeeNet.App;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.DAO;
using ZigBeeNet.Hardware.TI.CC2531;
using ZigBeeNet.Serial;
using ZigBeeNet.Transaction;
using ZigBeeNet.Transport;
using ZigBeeNet.Util;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Clusters;
using ZigBeeNet.ZCL.Clusters.ColorControl;
using ZigBeeNet.ZCL.Clusters.General;
using ZigBeeNet.ZCL.Clusters.LevelControl;
using ZigBeeNet.ZCL.Clusters.OnOff;
using ZigBeeNet.ZDO.Command;

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
                Console.Write("Enter COM Port: ");

                string port = Console.ReadLine();

                ZigBeeSerialPort zigbeePort = new ZigBeeSerialPort(port);

                IZigBeeTransportTransmit dongle = new ZigBeeDongleTiCc2531(zigbeePort);

                ZigBeeNetworkManager networkManager = new ZigBeeNetworkManager(dongle);

                JsonNetworkSerializer deviceSerializer = new JsonNetworkSerializer("devices.json");
                networkManager.NetworkStateSerializer = deviceSerializer;

                ZigBeeDiscoveryExtension discoveryExtension = new ZigBeeDiscoveryExtension();
                discoveryExtension.SetUpdatePeriod(60);
                networkManager.AddExtension(discoveryExtension);

                // Initialise the network
                networkManager.Initialize();

                networkManager.AddCommandListener(new ZigBeeNetworkDiscoverer(networkManager));
                //networkManager.AddCommandListener(new ZigBeeNodeServiceDiscoverer(networkManager));
                networkManager.AddCommandListener(new ZigBeeTransaction(networkManager));
                networkManager.AddCommandListener(new ConsoleCommandListener());
                networkManager.AddNetworkNodeListener(new ConsoleNetworkNodeListener());
                               
                networkManager.AddSupportedCluster(0x06);
                networkManager.AddSupportedCluster(0x08);
                networkManager.AddSupportedCluster(0x0300);

                ((ZigBeeDongleTiCc2531)dongle).SetLedMode(1, false); // green led
                ((ZigBeeDongleTiCc2531)dongle).SetLedMode(2, false); // red led

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
                    Console.WriteLine(networkManager.Nodes.Count + " node(s)");

                    if (!string.IsNullOrEmpty(cmd))
                    {
                        Console.WriteLine("Destination Address: ");
                        string nwkAddr = Console.ReadLine();

                        if (ushort.TryParse(nwkAddr, out ushort addr))
                        {
                            var node = networkManager.GetNode(addr);

                            if (node != null)
                            {
                                ZigBeeEndpointAddress endpointAddress = null;
                                var endpoint = node.Endpoints.Values.FirstOrDefault();

                                if (endpoint != null)
                                    endpointAddress = endpoint.GetEndpointAddress();

                                if (endpointAddress == null)
                                {
                                    Console.WriteLine("No endpoint found");

                                    continue;
                                }

                                try
                                {
                                    if (cmd == "toggle")
                                    {
                                        networkManager.Send(endpointAddress, new ToggleCommand()).GetAwaiter().GetResult();
                                    }
                                    else if (cmd == "level")
                                    {
                                        Console.WriteLine("Level between 0 and 255: ");
                                        string level = Console.ReadLine();

                                        Console.WriteLine("time between 0 and 65535: ");
                                        string time = Console.ReadLine();

                                        var command = new MoveToLevelWithOnOffCommand()
                                        {
                                            Level = byte.Parse(level),
                                            TransitionTime = ushort.Parse(time)
                                        };

                                        networkManager.Send(endpointAddress, command).GetAwaiter().GetResult();
                                    }
                                    else if (cmd == "move")
                                    {
                                        networkManager.Send(endpointAddress, new MoveCommand() { MoveMode = 1, Rate = 100 }).GetAwaiter().GetResult();
                                    }
                                    else if (cmd == "on")
                                    {
                                        networkManager.Send(endpointAddress, new OnCommand()).GetAwaiter().GetResult();
                                    }
                                    else if (cmd == "off")
                                    {
                                        networkManager.Send(endpointAddress, new OffCommand()).GetAwaiter().GetResult();
                                    }
                                    else if (cmd == "effect")
                                    {
                                        networkManager.Send(endpointAddress, new OffCommand()).GetAwaiter().GetResult();

                                        bool state = false;
                                        for (int i = 0; i < 10; i++)
                                        {
                                            if (state)
                                                networkManager.Send(endpointAddress, new OffCommand()).GetAwaiter().GetResult();
                                            else
                                                networkManager.Send(endpointAddress, new OnCommand()).GetAwaiter().GetResult();

                                            state = !state;
                                            System.Threading.Thread.Sleep(1000);
                                        }
                                    }
                                    else if (cmd == "desc")
                                    {
                                        NodeDescriptorRequest nodeDescriptorRequest = new NodeDescriptorRequest()
                                        {
                                            DestinationAddress = endpointAddress,
                                            NwkAddrOfInterest = addr
                                        };
                                        networkManager.SendTransaction(nodeDescriptorRequest);
                                    }
                                    else if (cmd == "color")
                                    {
                                        Console.WriteLine("Red between 0 and 255: ");
                                        string r = Console.ReadLine();

                                        Console.WriteLine("Green between 0 and 255: ");
                                        string g = Console.ReadLine();

                                        Console.WriteLine("Blue between 0 and 255: ");
                                        string b = Console.ReadLine();

                                        if (int.TryParse(r, out int _r) && int.TryParse(g, out int _g) && int.TryParse(b, out int _b))
                                        {
                                            CieColor xyY = ColorConverter.RgbToCie(_r, _g, _b);

                                            MoveToColorCommand command = new MoveToColorCommand()
                                            {
                                                ColorX = xyY.X,
                                                ColorY = xyY.Y,
                                                TransitionTime = 10
                                            };

                                            networkManager.Send(endpointAddress, command).GetAwaiter().GetResult();
                                        }
                                    }
                                    else if (cmd == "hue")
                                    {
                                        Console.WriteLine("Red between 0 and 255: ");
                                        string hue = Console.ReadLine();

                                        if (byte.TryParse(hue, out byte _hue))
                                        {
                                            MoveToHueCommand command = new MoveToHueCommand()
                                            {
                                                Hue = _hue,
                                                Direction = 0,
                                                TransitionTime = 10
                                            };

                                            networkManager.Send(endpointAddress, command).GetAwaiter().GetResult();
                                        } 
                                    }
                                    else if (cmd == "read")
                                    {
                                        var value = ((ZclOnOffCluster)endpoint.GetInputCluster(6)).GetOnOff(0);

                                        var lastValue = value;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log.Logger.Error(ex, "{Error}");
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Node {addr} not found");
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

        public static double[] RGBtoxyY(double r, double g, double b)
        {
            var red = (r > 0.04045) ? Math.Pow((r + 0.055) / (1.0 + 0.055), 2.4) : (r / 12.92);
            var green = (g > 0.04045) ? Math.Pow((g + 0.055) / (1.0 + 0.055), 2.4) : (g / 12.92);
            var blue = (b > 0.04045) ? Math.Pow((b + 0.055) / (1.0 + 0.055), 2.4) : (b / 12.92);

            //RGB values to XYZ using the Wide RGB D65 conversion formula 
            var X = red * 0.664511 + green * 0.154324 + blue * 0.162028;
            var Y = red * 0.283881 + green * 0.668433 + blue * 0.047685;
            var Z = red * 0.000088 + green * 0.072310 + blue * 0.986039;

            //Calculate the xy values from the XYZ values 
            var x = (X / (X + Y + Z)); //.toFixed(4);
            var y = (Y / (X + Y + Z)); //.toFixed(4);

            //if (isNaN(x)) x = 0;
            //if (isNaN(y)) y = 0;

            return new double[] { x, y };
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

    public class JsonNetworkSerializer : IZigBeeNetworkStateSerializer
    {
        private string _filename;

        public JsonNetworkSerializer(string filename)
        {
            _filename = filename;
        }

        public void Deserialize(ZigBeeNetworkManager networkManager)
        {
            if (File.Exists(_filename) == false)
                return;

            List<ZigBeeNodeDao> nodes = JsonConvert.DeserializeObject<List<ZigBeeNodeDao>>(File.ReadAllText(_filename));

            if (nodes == null)
                return;

            foreach (var nodeDao in nodes)
            {
                ZigBeeNode node = new ZigBeeNode(networkManager, new IeeeAddress(nodeDao.IeeeAddress));
                node.SetDao(nodeDao);
                               
                networkManager.AddNode(node);
            }
        }

        public void Serialize(ZigBeeNetworkManager networkManager)
        {

            List<ZigBeeNodeDao> nodes = new List<ZigBeeNodeDao>();

            foreach (var node in networkManager.Nodes)
            {
                ZigBeeNodeDao nodeDao = node.GetDao();
                nodes.Add(nodeDao);
            }

            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(nodes, Formatting.Indented, settings);

            File.WriteAllText(_filename, json);
        }
    }
}
