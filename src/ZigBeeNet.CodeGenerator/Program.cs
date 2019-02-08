using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.CodeGenerator.Zcl;

namespace ZigBeeNet.CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ZclProtocolCodeGenerator.Generate();

            Console.WriteLine("Code generation done. Press any key to close this window ...");
            Console.ReadLine();
        }
    }
}
