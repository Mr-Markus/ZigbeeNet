using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using ZigBeeNet;
using ZigBeeNet.CC;
using ZigBeeNet.Security;
using ZigBeeNet.Serial;
using ZigBeeNet.Transport;

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

                ZigBeeSerialPort port = new ZigBeeSerialPort("COM4");

                IZigBeeTransportTransmit dongle = new ZigBeeDongleTiCc2531(port);

                ZigBeeNetworkManager networkManager = new ZigBeeNetworkManager(dongle);

                // Initialise the network
                networkManager.Initialize();

                Log.Logger.Information("PAN ID: {PanId}", networkManager.ZigBeePanId);
                Log.Logger.Information("Extended PAN ID: {ExtendenPanId}", networkManager.ZigBeeExtendedPanId);
                Log.Logger.Information("Channel: {Channel}", networkManager.ZigbeeChannel);

                transportOptions.AddOption(TransportConfigOption.TRUST_CENTRE_LINK_KEY, new ZigBeeKey(new byte[] { 0x5A, 0x69,
                0x67, 0x42, 0x65, 0x65, 0x41, 0x6C, 0x6C, 0x69, 0x61, 0x6E, 0x63, 0x65, 0x30, 0x39 }));

                dongle.UpdateTransportConfig(transportOptions);

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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }        
    }
}
