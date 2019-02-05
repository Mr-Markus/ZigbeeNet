using System;
using System.Linq;
using Serilog;
using ZigBeeNet.App;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.Hardware.CC;
using ZigBeeNet.Serial;
using ZigBeeNet.Transaction;
using ZigBeeNet.Transport;
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
                        Console.WriteLine("Destination Address: ");
                        string nwkAddr = Console.ReadLine();

                        if (ushort.TryParse(nwkAddr, out ushort addr))
                        {
                            var node = networkManager.GetNode(addr);

                            if (node != null)
                            {
                                var endpointAddress = node.Endpoints.FirstOrDefault().Value.GetEndpointAddress();

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

                                        var command = new MoveToLevelWithOnOffCommand(byte.Parse(level), ushort.Parse(time));

                                        networkManager.Send(endpointAddress, command).GetAwaiter().GetResult();
                                    }
                                    else if (cmd == "move")
                                    {
                                        networkManager.Send(endpointAddress, new MoveCommand(1, 100)).GetAwaiter().GetResult();
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
                                        //Console.WriteLine("ColorX between 0 and 65535: ");
                                        //string x = Console.ReadLine();

                                        //Console.WriteLine("ColorY between 0 and 65535: ");
                                        //string y = Console.ReadLine();

                                        Console.WriteLine("Red between 0 and 255: ");
                                        string r = Console.ReadLine();

                                        Console.WriteLine("Green between 0 and 255: ");
                                        string g = Console.ReadLine();

                                        Console.WriteLine("Blue between 0 and 255: ");
                                        string b = Console.ReadLine();

                                        if (double.TryParse(r, out double _r) && double.TryParse(g, out double _g) && double.TryParse(b, out double _b))
                                        {
                                            double[] xyY = RGBtoxyY(_r, _g, _b);

                                            MoveToColorCommand command = new MoveToColorCommand()
                                            {
                                                ColorX = (ushort)(xyY[0] * 100000),
                                                ColorY = (ushort)(xyY[1] * 100000),
                                                TransitionTime = 10
                                            };

                                            networkManager.Send(endpointAddress, command).GetAwaiter().GetResult();
                                        }
                                    }
                                    else if (cmd == "hue")
                                    {
                                        Console.WriteLine("Hue between 0 and 255: ");
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
}
