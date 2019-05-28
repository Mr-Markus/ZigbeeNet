using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ZigBeeNet.CodeGenerator
{
    public class CodeGeneratorUtil
    {
        public static string ToHex(int profileId)
        {
            return "0x" + profileId.ToString("X");
        }

        public static int FromHex(string headerIdString)
        {
            headerIdString = headerIdString.Replace("0x", "");
            return int.Parse(headerIdString, NumberStyles.HexNumber);
        }

        public static string LabelToEnumerationValue(string dataType)
        {
            string val = dataType
                            .ToUpper()
                            .Trim()
                            .Replace(" ", "_")
                            .Replace("-", "_")
                            .Replace("/", "_")
                            .Replace("(", "_")
                            .Replace(")", "_");

            if (val.EndsWith("_"))
            {
                val = val.Substring(0, val.Length - 1);
            }
            if ("0123456789".IndexOf(val[0]) >= 0)
            {
                // Swap the last word to the beginning
                string[] partsInitial = val.Split("_");
                StringBuilder sb = new StringBuilder();
                sb.Append(partsInitial[partsInitial.Length - 1]);

                for (int c = 0; c < partsInitial.Length - 1; c++)
                {
                    sb.Append("_");
                    sb.Append(partsInitial[c]);
                }

                return sb.ToString();
            }
            return val;
        }

        public static string LabelToUpperCamelCase(string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                    SplitCamelCase(value)
                        .Replace("-", " ")
                        .Replace("_", " ")
                        .Replace("/", " ")
                        .Replace("(", " ")
                        .Replace(")", " "))
                        .Replace(" ", "");
        }

        public static string UpperCamelCaseToLowerCamelCase(string value)
        {
            if (value.Length == 0)
            {
                return "";
            }

            return value.Substring(0, 1).ToLower() + value.Substring(1);
        }

        public static string SplitCamelCase(string value)
        {
            value = Regex.Replace(value, @"%s|%s|%s", " ");
            value = Regex.Replace(value, @"(?<=[A-Z])(?=[A-Z][a-z])", " ");
            value = Regex.Replace(value, @"(?<=[^A-Z])(?=[A-Z])", " ");
            value = Regex.Replace(value, @"(?<=[A-Za-z])(?=[^A-Za-z])", " ");

            return value;

        }

        public static StringBuilder OutputLicense(StringBuilder builder)
        {
            builder.AppendLine("// License text here");
            return builder;
            //BufferedReader br;
            //try
            //{
            //    br = new BufferedReader(new FileReader("../src/etc/header.txt"));
            //    String line = br.readLine();

            //out.println("/**");
            //    while (line != null)
            //    {
            //    out.println(" * " + line.replaceFirst("\\$\\{year\\}", "2018"));
            //        line = br.readLine();
            //    }
            //out.println(" */");
            //    br.close();
            //}
            //catch (FileNotFoundException e)
            //{
            //    // TODO Auto-generated catch block
            //    e.printStackTrace();
            //}
            //catch (IOException e)
            //{
            //    // TODO Auto-generated catch block
            //    e.printStackTrace();
            //}
        }
    }
}
