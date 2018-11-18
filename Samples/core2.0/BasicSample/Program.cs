using System;
using Serilog;
using ZigbeeNet;

namespace BasicSample
{
    class Program
    {
        private static ZigbeeService _service;

        static void Main(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            try
            {
                _service = new ZigbeeService(new Options { Baudrate = 115200, Port = "COM4" });
                _service.OnReady += ZigbeeService_OnReady;
                _service.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }

        private static void ZigbeeService_OnReady(object sender, EventArgs e)
        {
            _service.PermitJoining(10);
        }
    }
}
