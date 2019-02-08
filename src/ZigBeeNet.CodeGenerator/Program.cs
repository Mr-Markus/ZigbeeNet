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

            Console.ReadLine();
        }
    }
}
