using System;
using Serilog;
using ZigbeeNet;

namespace BasicSample
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
                var zigbeeService = new ZigbeeService(new Options { Baudrate = 115200, Port = "COM4" });
                zigbeeService.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
    }
}
