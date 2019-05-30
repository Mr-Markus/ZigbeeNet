using Mono.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZigBeeNet.App.Basic;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.DAO;
using ZigBeeNet.Hardware.Digi.XBee;
using ZigBeeNet.Hardware.TI.CC2531;
using ZigBeeNet.Tranport.SerialPort;
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
        static async Task Main(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Console()
               .CreateLogger();

            bool showHelp = false;
            ZigBeeDongle zigBeeDongle = ZigBeeDongle.TiCc2531;

            OptionSet options = new OptionSet
            {
                { "h|help", "show this message and exit", h => showHelp = h != null },
                { "zbd|zigbeeDongle=", "the zigbee dongle to use. 0 = TiCc2531 | 1 = DigiXBee", (ZigBeeDongle zbd) => zigBeeDongle = zbd }
            };

            try
            {
                IList<string> extraArgs = options.Parse(args);
                foreach (string extraArg in extraArgs)
                {
                    Console.WriteLine($"Error: Unknown option: {extraArg}");
                    showHelp = true;
                }

                Console.Write("Enter COM Port: ");

                string port = Console.ReadLine();

                ZigBeeSerialPort zigbeePort = new ZigBeeSerialPort(port);

                IZigBeeTransportTransmit dongle;
                switch (zigBeeDongle)
                {
                    case ZigBeeDongle.TiCc2531:
                        {
                            dongle = new ZigBeeDongleTiCc2531(zigbeePort);
                        }
                        break;
                    case ZigBeeDongle.DigiXbee:
                        {
                            dongle = new ZigBeeDongleXBee(zigbeePort);
                        }
                        break;
                    default:
                        {
                            dongle = new ZigBeeDongleTiCc2531(zigbeePort);
                        }
                        break;
                }

                ZigBeeNetworkManager networkManager = new ZigBeeNetworkManager(dongle);

                JsonNetworkSerializer deviceSerializer = new JsonNetworkSerializer("devices.json");
                //networkManager.NetworkStateSerializer = deviceSerializer;

                ZigBeeDiscoveryExtension discoveryExtension = new ZigBeeDiscoveryExtension();
                discoveryExtension.SetUpdatePeriod(60);
                networkManager.AddExtension(discoveryExtension);

                // Initialise the network
                networkManager.Initialize();

                /* Network (de)serialization */
                //networkManager.AddCommandListener(new ZigBeeNetworkDiscoverer(networkManager));
                //networkManager.AddCommandListener(new ZigBeeNodeServiceDiscoverer(networkManager));

                networkManager.AddCommandListener(new ZigBeeTransaction(networkManager));
                networkManager.AddCommandListener(new ConsoleCommandListener());

                networkManager.AddNetworkNodeListener(new ConsoleNetworkNodeListener());

                networkManager.AddSupportedCluster(ZclOnOffCluster.CLUSTER_ID);
                networkManager.AddSupportedCluster(ZclColorControlCluster.CLUSTER_ID);
                networkManager.AddSupportedCluster(ZclTouchlinkCluster.CLUSTER_ID);

                networkManager.AddExtension(new ZigBeeBasicServerExtension());

                if (zigBeeDongle == ZigBeeDongle.TiCc2531)
                {
                    ((ZigBeeDongleTiCc2531)dongle).SetLedMode(1, false); // green led
                    ((ZigBeeDongleTiCc2531)dongle).SetLedMode(2, false); // red led
                }

                ZigBeeStatus startupSucceded = networkManager.Startup(false);

                if (startupSucceded == ZigBeeStatus.SUCCESS)
                {
                    Log.Logger.Information("ZigBee console starting up ... [OK]");
                }
                else
                {
                    Log.Logger.Information("ZigBee console starting up ... [FAIL]");
                    Log.Logger.Information("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }

                ZigBeeNode coord = networkManager.GetNode(0);

                Console.WriteLine("Joining enabled...");

                string cmd = string.Empty;

                while (cmd != "exit")
                {
                    Console.WriteLine(networkManager.Nodes.Count + " node(s)" + Environment.NewLine);

                    if (cmd == "join")
                    {
                        coord.PermitJoin(true);
                    }
                    else if (cmd == "unjoin")
                    {
                        coord.PermitJoin(false);
                    }
                    else if (!string.IsNullOrEmpty(cmd))
                    {
                        var tmp = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("Destination Address: ");
                        Console.ForegroundColor = tmp;
                        string nwkAddr = Console.ReadLine();

                        if (ushort.TryParse(nwkAddr, out ushort addr))
                        {
                            var node = networkManager.GetNode(addr);

                            if (node != null)
                            {
                                ZigBeeEndpointAddress endpointAddress = null;
                                var endpoint = node.Endpoints.Values.FirstOrDefault();

                                if (endpoint != null)
                                {
                                    endpointAddress = endpoint.GetEndpointAddress();
                                }

                                if (endpointAddress == null)
                                {
                                    Console.WriteLine("No endpoint found");

                                    continue;
                                }

                                try
                                {
                                    if (cmd == "toggle")
                                    {
                                        await networkManager.Send(endpointAddress, new ToggleCommand());
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

                                        await networkManager.Send(endpointAddress, command);
                                    }
                                    else if (cmd == "move")
                                    {
                                        await networkManager.Send(endpointAddress, new MoveCommand() { MoveMode = 1, Rate = 100 });
                                    }
                                    else if (cmd == "on")
                                    {
                                        await networkManager.Send(endpointAddress, new OnCommand());
                                    }
                                    else if (cmd == "off")
                                    {
                                        await networkManager.Send(endpointAddress, new OffCommand());
                                    }
                                    else if (cmd == "effect")
                                    {
                                        await networkManager.Send(endpointAddress, new OffCommand());

                                        bool state = false;
                                        for (int i = 0; i < 10; i++)
                                        {
                                            if (state)
                                            {
                                                await networkManager.Send(endpointAddress, new OffCommand());
                                            }
                                            else
                                            {
                                                await networkManager.Send(endpointAddress, new OnCommand());
                                            }

                                            state = !state;
                                            await Task.Delay(1000);
                                        }
                                    }
                                    else if (cmd == "stress")
                                    {
                                        await networkManager.Send(endpointAddress, new OffCommand());

                                        bool state = false;
                                        for (int i = 0; i < 100; i++)
                                        {
                                            if (state)
                                            {
                                                await networkManager.Send(endpointAddress, new OffCommand());
                                            }
                                            else
                                            {
                                                await networkManager.Send(endpointAddress, new OnCommand());
                                            }

                                            state = !state;

                                            await Task.Delay(1);
                                        }
                                    }
                                    else if (cmd == "desc")
                                    {
                                        NodeDescriptorRequest nodeDescriptorRequest = new NodeDescriptorRequest()
                                        {
                                            Destination = endpointAddress,
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

                                            await networkManager.Send(endpointAddress, command);
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

                                            await networkManager.Send(endpointAddress, command);
                                        }
                                    }
                                    else if (cmd == "read")
                                    {
                                        var result = await ((ZclElectricalMeasurementCluster)endpoint.GetInputCluster(ZclElectricalMeasurementCluster.CLUSTER_ID)).Read(ZclElectricalMeasurementCluster.ATTR_MEASUREMENTTYPE);

                                        if (result.IsSuccess())
                                        {
                                            ReadAttributesResponse response = result.GetResponse<ReadAttributesResponse>();
                                            if (response.Records.Count == 0)
                                            {
                                                Console.WriteLine("No records returned");
                                                continue;
                                            }

                                            ZclStatus statusCode = response.Records[0].Status;
                                            if (statusCode == ZclStatus.SUCCESS)
                                            {
                                                Console.WriteLine("Cluster " + response + ", Attribute "
                                                        + response.Records[0].AttributeIdentifier + ", type "
                                                        + response.Records[0].AttributeDataType + ", value: "
                                                        + response.Records[0].AttributeValue);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Attribute value read error: " + statusCode);
                                            }
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

                    var currentForeGroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("cmd> ");
                    Console.ForegroundColor = currentForeGroundColor;
                    cmd = Console.ReadLine();
                }
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                showHelp = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if (showHelp)
            {
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                return;
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

    public class JsonNetworkSerializer : IZigBeeNetworkStateSerializer
    {
        private readonly string _filename;

        public JsonNetworkSerializer(string filename)
        {
            _filename = filename;
        }

        public void Deserialize(ZigBeeNetworkManager networkManager)
        {
            if (File.Exists(_filename) == false)
            {
                return;
            }

            List<ZigBeeNodeDao> nodes = JsonConvert.DeserializeObject<List<ZigBeeNodeDao>>(File.ReadAllText(_filename));

            if (nodes == null)
            {
                return;
            }

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
                if (node.NetworkAddress != 0)
                {
                    ZigBeeNodeDao nodeDao = node.GetDao();
                    nodes.Add(nodeDao);
                }
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
