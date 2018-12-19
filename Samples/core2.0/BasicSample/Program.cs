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


                ZigBeeKey nwkKey;
                ZigBeeKey linkKey;
                ExtendedPanId extendedPan;
                byte channel;
                ushort pan;

                //if (cmdline.hasOption("channel"))
                //{
                    channel = 11; // parseDecimalOrHexInt(cmdline.getOptionValue("channel"));
                //}
                //else
                //{
                //    channel = 11;
                //}
                //if (cmdline.hasOption("pan"))
                //{
                //    pan = parseDecimalOrHexInt(cmdline.getOptionValue("pan"));
                //}
                //else
                //{
                    pan = 1;
                //}
                //if (cmdline.hasOption("epan"))
                //{
                //    extendedPan = new ExtendedPanId(cmdline.getOptionValue("epan"));
                //}
                //else
                //{
                    extendedPan = new ExtendedPanId();
                //}

                //if (cmdline.hasOption("nwkkey"))
                //{
                //    nwkKey = new ZigBeeKey(cmdline.getOptionValue("nwkkey"));
                //}
                //else
                //{
                    nwkKey = ZigBeeKey.CreateRandom();
                //}
                //if (cmdline.hasOption("linkkey"))
                //{
                //    linkKey = new ZigBeeKey(cmdline.getOptionValue("linkkey"));
                //}
                //else
                //{
                    linkKey = new ZigBeeKey(new byte[] { 0x5A, 0x69, 0x67, 0x42, 0x65, 0x65, 0x41, 0x6C, 0x6C, 0x69, 0x61,
                        0x6E, 0x63, 0x65, 0x30, 0x39 });
                //}

                //if (cmdline.hasOption("nwkkeyoutcnt"))
                //{
                //    nwkKey.setOutgoingFrameCounter(parseDecimalOrHexInt(cmdline.getOptionValue("nwkkeyoutcnt")));
                //}
                //if (cmdline.hasOption("linkkeyoutcnt"))
                //{
                //    linkKey.setOutgoingFrameCounter(parseDecimalOrHexInt(cmdline.getOptionValue("linkkeyoutcnt")));
                //}

                Console.WriteLine("*** Resetting network");
                Console.WriteLine("  * Channel                = " + channel);
                Console.WriteLine("  * PAN ID                 = " + pan);
                Console.WriteLine("  * Extended PAN ID        = " + extendedPan);
                Console.WriteLine("  * Link Key               = " + linkKey);
                //if (linkKey.OutgoingFrameCounter)
                //{
                //    Console.WriteLine("  * Link Key Frame Cnt     = " + linkKey.OutgoingFrameCounter);
                //}
                //Console.WriteLine("  * Network Key            = " + nwkKey);
                //if (nwkKey.OutgoingFrameCounter)
                //{
                //    Console.WriteLine("  * Network Key Frame Cnt  = " + nwkKey.OutgoingFrameCounter);
                //}

                networkManager.SetZigBeeChannel((ZigBeeChannel)channel);
                networkManager.SetZigBeePanId(pan);
                networkManager.SetZigBeeExtendedPanId(extendedPan);
                networkManager.SetZigBeeNetworkKey(nwkKey);
                networkManager.SetZigBeeLinkKey(linkKey);

                transportOptions.AddOption(TransportConfigOption.TRUST_CENTRE_LINK_KEY, new ZigBeeKey(new byte[] { 0x5A, 0x69,
                0x67, 0x42, 0x65, 0x65, 0x41, 0x6C, 0x6C, 0x69, 0x61, 0x6E, 0x63, 0x65, 0x30, 0x39 }));

            dongle.UpdateTransportConfig(transportOptions);

            networkManager.AddSupportedCluster(0x06);

            if (networkManager.Startup(false) != ZigBeeStatus.SUCCESS)
            {
                Log.Logger.Information("ZigBee console starting up ... [FAIL]");
                return;
            }
            else
            {
                Log.Logger.Information("ZigBee console starting up ... [OK]");
            }

            //networkManager.Startup(true);

            //Console.Write("How long to permit join? : ");
            //string duration = Console.ReadLine();

            //int durationInt = 0;

            //bool result = int.TryParse(duration, out durationInt);

            //if (result)
            //{
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

                            ZclOnOffCluster onOff = new ZclOnOffCluster(node.GetEndpoint(0));

                            onOff.ToggleCommand();
                        }
                    }
                }

                cmd = Console.ReadLine();
            }
            //}
        }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
    //Console.ReadLine();
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
