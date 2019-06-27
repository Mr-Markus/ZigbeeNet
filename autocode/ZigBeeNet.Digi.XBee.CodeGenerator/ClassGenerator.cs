using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ZigBeeNet.Digi.XBee.CodeGenerator.Extensions;
using ZigBeeNet.Digi.XBee.CodeGenerator.Xml;

namespace ZigBeeNet.Digi.XBee.CodeGenerator
{
    public abstract class ClassGenerator
    {
        readonly int _lineLen = 80;
        readonly string _sourceRootPath = "ZigBeeNet.Libraries.ZigBeeNet.Hardware.Digi.XBee";
        readonly List<string> _importList = new List<string>();

        protected StringBuilder GetClassOut(FileInfo packageFile, string className)
        {
            string classFileName = Path.Combine(packageFile.FullName, className + ".cs");
            Console.WriteLine("Generating: " + classFileName);

            var sr = new StreamReader(classFileName);
            return new StringBuilder(sr.ReadToEnd());
        }

        protected void OutputImports(StringBuilder stringBuilder)
        {
            _importList.Sort();
            foreach (var importClass in _importList)
            {
                stringBuilder.AppendLine("using " + importClass + ";");
            }
        }

        protected StringBuilder OutputCopyright(StringBuilder stringBuilder)
        {
            return new StringBuilder(DateTime.Now.Year);
        }

        protected void OutputWithLineBreak(StringBuilder stringBuilder, string indent, string line)
        {
            Regex regex = new Regex("\\s+");
            string[] words = regex.Split(line);

            if (words.Length == 0)
            {
                return;
            }

            //stringBuilder.Append(indent + "///");

            int len = 2;
            foreach (var word in words)
            {
                if (word.ToLower().Equals("note:"))
                {
                    if (len > 2)
                    {
                        stringBuilder.AppendLine();
                    }
                    stringBuilder.AppendLine(indent + " * <p>");
                    stringBuilder.Append(indent + " * <b>Note:</b>");
                    continue;
                }
                if (len + word.Length > _lineLen)
                {
                    stringBuilder.AppendLine();
                    //stringBuilder.Append(indent + "///");
                    len = 2;
                }
                stringBuilder.Append(" ");
                stringBuilder.Append(word);
                len += word.Length;
            }

            if (len != 0)
            {
                stringBuilder.AppendLine();
            }
        }

        protected string FormatParameterString(Parameter parameter)
        {
            if (parameter.DisplayType != null)
            {
                switch (parameter.DisplayType.ToLower())
                {
                    case "hex":
                        string size = "";
                        if (parameter.DisplayLength != 0)
                        {
                            size = "0" + parameter.DisplayLength;
                        }
                        return "string.Format(\"%" + size + "X\", " + parameter.Name.ToLowerCamelCase() + ")";
                }
            }
            return parameter.Name.ToLowerCamelCase();
        }
    }
}
