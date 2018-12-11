using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using ZigBeeNet;
using ZigBeeNet.CC;
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
                Console.Write("Please enter your COM Port: ");

                var port = Console.ReadLine();

                ZigBeeSerialPort zigbeePort = new ZigBeeSerialPort(port);

                IZigBeeTransportTransmit dongle = new ZigBeeDongleTiCc2531(zigbeePort);

                ZigBeeNetworkManager networkManager = new ZigBeeNetworkManager(dongle);

                // Initialise the network
                networkManager.Initialize();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }        
    }
}
