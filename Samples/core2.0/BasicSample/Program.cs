using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using ZigbeeNet;

namespace BasicSample
{
    class Program
    {
        private static ZigBeeService _service;
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
                _service = new ZigBeeService(new Options { Baudrate = 115200, Port = "COM4" });
                _service.OnReady += ZigbeeService_OnReady;
                _service.Controller.NewEndpoint += Controller_NewEndpoint; ;
                _service.Controller.NewDevice += Controller_NewDevice;
                _service.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }

        private static void Controller_NewEndpoint(object sender, ZigBeeEndpoint e)
        {
            if (sender is ZigbeeAddress16 nwkDst)
            {
                ZigBeeNode node = _nodes.Single(n => n.NwkAdress.Equals(nwkDst));

                node.Endpoints.Add(e);
            }
        }

        private static void Controller_NewDevice(object sender, ZigBeeNode e)
        {
            if(_nodes.Exists(n => n.IeeeAddress == e.IeeeAddress) == false)
            {
                _nodes.Add(e);
            }
        }

        private static void ZigbeeService_OnReady(object sender, EventArgs e)
        {
            _service.PermitJoining(255);
        }
    }
}
