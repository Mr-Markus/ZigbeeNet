using Mono.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZigbeeNet.Hardware.ConBee;
using ZigBeeNet.App.Basic;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.Database;
using ZigBeeNet.Hardware.Digi.XBee;
using ZigBeeNet.Hardware.Ember;
using ZigBeeNet.Hardware.TI.CC2531;
using ZigBeeNet.Security;
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
using ZigBeeNet.ZDO.Field;

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
            string port = "";
            int baudrate = 115200;
            string flow = "";
            FlowControl flowControl = FlowControl.FLOWCONTROL_OUT_NONE;
            bool resetNetwork = false;

            OptionSet options = new OptionSet
            {
                { "h|help", "show this message and exit", h => showHelp = h != null },
                { "zbd|zigbeeDongle=", "the zigbee dongle to use. 0 = TiCc2531 | 1 = DigiXBee | 2 = Conbee | 3 = Ember ", (ZigBeeDongle zbd) => zigBeeDongle = zbd },
                { "p|port=", "the COM port to use", p =>  port = p},
                { "b|baud=", $"the port baud rate to use. default is {baudrate}", b => int.TryParse(b, out baudrate)},
                { "f|flow=", $"the flow control (none | hardware | software)", f => flow = f },
                { "r|reset", $"Reset the Zigbee network", r => resetNetwork = r != null },
            };

            try
            {
                IList<string> extraArgs = options.Parse(args);
                foreach (string extraArg in extraArgs)
                {
                    Console.WriteLine($"Error: Unknown option: {extraArg}");
                    showHelp = true;
                }
                if(showHelp)
                {
                    ShowHelp(options);
                    return;
                }

                if (string.IsNullOrEmpty(port))
                {
                    Console.Write("Enter COM Port: ");
                    port = Console.ReadLine();
                }

                if(string.IsNullOrEmpty(flow))
                {
                    // Default the flow control based on the dongle
                    switch (zigBeeDongle)
                    {
                        case ZigBeeDongle.Ember:
                            flowControl = FlowControl.FLOWCONTROL_OUT_XONOFF;
                            break;
                        default:
                            flowControl = FlowControl.FLOWCONTROL_OUT_NONE;
                            break;
                    }
                }
                else
                {
                    switch(flow)
                    {
                        case "software":
                            flowControl = FlowControl.FLOWCONTROL_OUT_XONOFF;
                            break;
                        case "hardware":
                            flowControl = FlowControl.FLOWCONTROL_OUT_RTSCTS;
                            break;
                        case "none":
                            flowControl = FlowControl.FLOWCONTROL_OUT_NONE;
                            break;
                        default:
                            Console.WriteLine($"Unknown flow control option used: {flow}");
                            break;
                    }
                }

                ZigBeeSerialPort zigbeePort = new ZigBeeSerialPort(port, baudrate, flowControl);

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
                    case ZigBeeDongle.ConBee:
                        {
                            dongle = new ZigbeeDongleConBee(zigbeePort);
                        }
                        break;
                    case ZigBeeDongle.Ember:
                        {
                            dongle = new ZigBeeDongleEzsp(zigbeePort);
                            ((ZigBeeDongleEzsp)dongle).SetPollRate(0);
                        }
                        break;
                    default:
                        {
                            dongle = new ZigBeeDongleTiCc2531(zigbeePort);
                        }
                        break;
                }

                ZigBeeNetworkManager networkManager = new ZigBeeNetworkManager(dongle);

                JsonNetworkDataStore dataStore = new JsonNetworkDataStore("devices");
                networkManager.SetNetworkDataStore(dataStore);

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

                networkManager.AddExtension(new ZigBeeBasicServerExtension());

                if (zigBeeDongle == ZigBeeDongle.TiCc2531)
                {
                    ((ZigBeeDongleTiCc2531)dongle).SetLedMode(1, false); // green led
                    ((ZigBeeDongleTiCc2531)dongle).SetLedMode(2, false); // red led
                }
                Console.WriteLine($"PAN ID           = {networkManager.ZigBeePanId}");
                Console.WriteLine($"Extended PAN ID  = {networkManager.ZigBeeExtendedPanId}");
                Console.WriteLine($"Channel          = {networkManager.ZigbeeChannel}");
                Console.WriteLine($"Network Key      = {networkManager.ZigBeeNetworkKey}");
                Console.WriteLine($"Link Key         = {networkManager.ZigBeeLinkKey}");

                if (resetNetwork)
                {
                    //TODO: make the network parameters configurable
                    ushort panId = 1;
                    ExtendedPanId extendedPanId = new ExtendedPanId();
                    ZigBeeChannel channel = ZigBeeChannel.CHANNEL_11;
                    ZigBeeKey networkKey = ZigBeeKey.CreateRandom();
                    ZigBeeKey linkKey = new ZigBeeKey(new byte[] { 0x5A, 0x69, 0x67, 0x42, 0x65, 0x65, 0x41, 0x6C, 0x6C, 0x69, 0x61, 0x6E, 0x63, 0x65, 0x30, 0x39 });

                    Console.WriteLine($"*** Resetting network");
                    Console.WriteLine($"  * PAN ID           = {panId}");
                    Console.WriteLine($"  * Extended PAN ID  = {extendedPanId}");
                    Console.WriteLine($"  * Channel          = {channel}");
                    Console.WriteLine($"  * Network Key      = {networkKey}");
                    Console.WriteLine($"  * Link Key         = {linkKey}");

                    networkManager.SetZigBeeChannel(channel);
                    networkManager.SetZigBeePanId(panId);
                    networkManager.SetZigBeeExtendedPanId(extendedPanId);
                    networkManager.SetZigBeeNetworkKey(networkKey);
                    networkManager.SetZigBeeLinkKey(linkKey);
                }

                ZigBeeStatus startupSucceded = networkManager.Startup(resetNetwork);

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
                    if (cmd == "join")
                    {
                        coord.PermitJoin(true);
                    }
                    else if (cmd == "unjoin")
                    {
                        coord.PermitJoin(false);
                    }
                    else if(cmd == "endpoints")
                    {
                        var tmp = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("Destination Address: ");
                        Console.ForegroundColor = tmp;
                        string nwkAddr = Console.ReadLine();

                        if (ushort.TryParse(nwkAddr, out ushort addr))
                        {
                            var node = networkManager.Nodes.FirstOrDefault(n => n.NetworkAddress == addr);

                            if(node != null) 
                            {
                                Console.WriteLine(new string('-', 20));

                                foreach (var endpoint in node.Endpoints.Values)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Input Cluster:" + Environment.NewLine);
                                    Console.ForegroundColor = tmp;

                                    foreach (var inputClusterId in endpoint.GetInputClusterIds())
                                    {
                                        var cluster = endpoint.GetInputCluster(inputClusterId);
                                        var clusterName = cluster.GetClusterName();
                                        Console.WriteLine($"{clusterName}");
                                    }
                                }

                                Console.WriteLine();

                                foreach (var endpoint in node.Endpoints.Values)
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Output Cluster:" + Environment.NewLine);
                                    Console.ForegroundColor = tmp;

                                    foreach (var outputClusterIds in endpoint.GetOutputClusterIds())
                                    {
                                        var cluster = endpoint.GetOutputCluster(outputClusterIds);
                                        var clusterName = cluster.GetClusterName();
                                        Console.WriteLine($"{clusterName}");
                                    }
                                }

                                Console.WriteLine(new string('-', 20));
                            }
                        }
                      
                    }
                    else if (cmd == "add")
                    {
                        var tmp = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write("Destination Address: ");
                        Console.ForegroundColor = tmp;
                        string nwkAddr = Console.ReadLine();

                        if (ushort.TryParse(nwkAddr, out ushort addr))
                        {
                            Console.Write("IeeeAddress: ");
                            Console.ForegroundColor = tmp;
                            string ieeeAddr = Console.ReadLine();

                            networkManager.UpdateNode(new ZigBeeNode() { NetworkAddress = addr, IeeeAddress = new IeeeAddress(ieeeAddr) });
                        }
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
                                    if (cmd == "leave")
                                    {
                                        await networkManager.Leave(node.NetworkAddress, node.IeeeAddress);
                                    }
                                    else if (cmd == "toggle")
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
                                        var cluster = endpoint.GetInputCluster(ZclBasicCluster.CLUSTER_ID);
                                        if (cluster != null)
                                        {
                                            string manufacturerName = (string)(await cluster.ReadAttributeValue(ZclBasicCluster.ATTR_MANUFACTURERNAME));
                                            string model = (string)(await cluster.ReadAttributeValue(ZclBasicCluster.ATTR_MODELIDENTIFIER));

                                            Console.WriteLine($"Manufacturer Name = {manufacturerName}");
                                            Console.WriteLine($"Model identifier = {model}");
                                        }
                                    }
                                    else if (cmd == "discover attributes")
                                    {
                                        foreach (int clusterId in endpoint.GetInputClusterIds())
                                        {
                                            ZclCluster cluster = endpoint.GetInputCluster(clusterId);
                                            if (!await cluster.DiscoverAttributes(true))
                                                Console.WriteLine("Error while discovering attributes for cluster {0}", cluster.GetClusterName());
                                        }
                                    }
                                    else if (cmd == "update binding table")
                                    {
                                        ZigBeeStatus statusCode = await node.UpdateBindingTable();

                                        if(statusCode != ZigBeeStatus.SUCCESS)
                                        {
                                            Console.WriteLine("Error while reading binding table: " + statusCode);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log.Logger.Error(ex, "Error while executing cmd {Command}", cmd);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Node {addr} not found");
                            }
                        }
                    }

                    await Task.Delay(100);

                    Console.WriteLine(Environment.NewLine + networkManager.Nodes.Count + " node(s)" + Environment.NewLine);

                    for (int i = 0; i < networkManager.Nodes.Count; i++)
                    {
                        var node = networkManager.Nodes[i];
                        Console.WriteLine($"{i}. {node.LogicalType}: {node.NetworkAddress}");
                    }

                    Console.WriteLine();
                    var currentForeGroundColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write("cmd> ");
                    Console.ForegroundColor = currentForeGroundColor;
                    cmd = Console.ReadLine();
                }
                networkManager.Shutdown();
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                ShowHelp(options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }

        static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
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

    public class JsonNetworkDataStore : IZigBeeNetworkDataStore
    {
        private readonly string _dirname;

        public JsonNetworkDataStore(string dirname)
        {
            _dirname = dirname;
            
            if (!Directory.Exists(_dirname))
                Directory.CreateDirectory(_dirname);
        }

        private string GetFileName(IeeeAddress address)
        {
            return _dirname + "/" + address.ToString() + ".json";
        }

        public ISet<IeeeAddress> ReadNetworkNodes()
        {
            ISet<IeeeAddress> nodes = new HashSet<IeeeAddress>();

            try
            {
                if (Directory.Exists(_dirname))
                {
                    var jsonFiles = Directory.EnumerateFiles(_dirname, "*.json");

                    foreach(string file in jsonFiles)
                    {
                        try
                        {
                            IeeeAddress address = new IeeeAddress(file.Substring(file.Length - (16 + 5), 16));
                            nodes.Add(address);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return nodes;
        }


        public ZigBeeNodeDao ReadNode(IeeeAddress address)
        {
            ZigBeeNodeDao node = null;

            try
            {
                string filename = GetFileName(address);

                if (File.Exists(filename))
                {
                    node = JsonConvert.DeserializeObject<ZigBeeNodeDao>(File.ReadAllText(filename));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return node;
        }

        public void RemoveNode(IeeeAddress address)
        {
            try
            {
                string filename = GetFileName(address);

                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void WriteNode(ZigBeeNodeDao node)
        {
            try
            {
                string filename = GetFileName(node.IeeeAddress);

                var settings = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string json = JsonConvert.SerializeObject(node, Formatting.Indented, settings);

                File.WriteAllText(filename, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
