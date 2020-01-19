using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigBeeNet.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();
            var section = configuration.GetSection("Settings");

            ZigBeeCodeGenerator.Generate(section.GetValue<string>("outputPath"));

            Console.WriteLine("Code generation done. Press any key to close this window ...");
            Console.ReadLine();
        }
    }
}
