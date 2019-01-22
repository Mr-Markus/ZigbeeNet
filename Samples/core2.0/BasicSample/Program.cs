using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using ZigBeeNet;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.CC;
using ZigBeeNet.Security;
using ZigBeeNet.Serial;
using ZigBeeNet.Transport;
using ZigBeeNet.ZCL.Clusters;
using ZigBeeNet.ZCL.Clusters.OnOff;

namespace BasicSample
{
    class Program
    {
        private static List<ZigBeeNode> _nodes;

        static void Main(string[] args)
        {
            _nodes = new List<ZigBeeNode>();

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            try
            {
                TransportConfig transportOptions = new TransportConfig();

                Console.Write("Please enter your COM Port: ");

                var port = Console.ReadLine();

                ZigBeeSerialPort zigbeePort = new ZigBeeSerialPort(port);

                IZigBeeTransportTransmit dongle = new ZigBeeDongleTiCc2531(zigbeePort);

                ZigBeeNetworkManager networkManager = new ZigBeeNetworkManager(dongle);

                ZigBeeDiscoveryExtension discoveryExtension = new ZigBeeDiscoveryExtension();
                discoveryExtension.setUpdatePeriod(60);
                networkManager.AddExtension(discoveryExtension);

                // Initialise the network
                networkManager.Initialize();

                networkManager.AddCommandListener(new ConsoleCommandListener());
                networkManager.AddNetworkNodeListener(new ConsoleNetworkNodeListener());

                Log.Logger.Information("PAN ID: {PanId}", networkManager.ZigBeePanId);
                Log.Logger.Information("Extended PAN ID: {ExtendenPanId}", networkManager.ZigBeeExtendedPanId);
                Log.Logger.Information("Channel: {Channel}", networkManager.ZigbeeChannel);

                byte channel = 11;

                byte pan = 1;

                ExtendedPanId extendedPan = new ExtendedPanId();

                ZigBeeKey nwkKey = ZigBeeKey.CreateRandom();

                ZigBeeKey linkKey = new ZigBeeKey(new byte[] { 0x5A, 0x69, 0x67, 0x42, 0x65, 0x65, 0x41, 0x6C, 0x6C, 0x69, 0x61,
                        0x6E, 0x63, 0x65, 0x30, 0x39 });


                Console.WriteLine("*** Resetting network");
                Console.WriteLine("  * Channel                = " + channel);
                Console.WriteLine("  * PAN ID                 = " + pan);
                Console.WriteLine("  * Extended PAN ID        = " + extendedPan);
                Console.WriteLine("  * Link Key               = " + linkKey);

                networkManager.SetZigBeeChannel((ZigBeeChannel)channel);
                networkManager.SetZigBeePanId(pan);
                networkManager.SetZigBeeExtendedPanId(extendedPan);
                networkManager.SetZigBeeNetworkKey(nwkKey);
                networkManager.SetZigBeeLinkKey(linkKey);

                transportOptions.AddOption(TransportConfigOption.TRUST_CENTRE_LINK_KEY, new ZigBeeKey(new byte[] { 0x5A, 0x69,
                0x67, 0x42, 0x65, 0x65, 0x41, 0x6C, 0x6C, 0x69, 0x61, 0x6E, 0x63, 0x65, 0x30, 0x39 }));

                dongle.UpdateTransportConfig(transportOptions);

                networkManager.AddSupportedCluster(0x06);

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
                    if (cmd == "toggle")
                    {
                        Console.WriteLine("Destination Address: ");
                        string nwkAddr = Console.ReadLine();

                        if (ushort.TryParse(nwkAddr, out ushort addr))
                        {
                            var node = networkManager.GetNode(addr);

                            if (node != null)
                            {
                                ZigBeeEndpoint ep = new ZigBeeEndpoint(node, 0);
                                node.AddEndpoint(ep);

                                ZclOnOffCluster onOff = new ZclOnOffCluster(node.GetEndpoint(0), networkManager);

                                onOff.ToggleCommand();
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
            //if (node.NetworkAddress != 0)
            //{
            //    ZclOnOffCluster onOff = new ZclOnOffCluster(node.GetEndpoint(0));

            //    onOff.ToggleCommand();
            //}
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
