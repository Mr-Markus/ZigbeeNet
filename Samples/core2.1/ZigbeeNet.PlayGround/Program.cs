using System;
using Serilog;

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
                var zigbeeService = new ZigBeeService(new Options { Baudrate = 115200, Port = "COM3" });
                zigbeeService.Start();

                Log.Information("Application started");

                Console.ReadLine();
                zigbeeService.Stop();
                Log.Information("Application stopped");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
