using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ZigBeeNet.Digi.XBee.CodeGenerator.Extensions
{
    public static class StringExtension
    {
        public static string ToCamelCase(this string value)
        {
            Regex regex = new Regex("\\(.*?\\) ?");
            value = regex.Replace(value, "");
            value = value.Replace("+", "_Plus");
            value = value.Replace(" ", "_");
            value = value.Replace("-", "_");
            value = value.Replace(".", "_");
            value = value.Replace("/", "_");
            regex = new Regex("_+");
            value = regex.Replace(value, "_");
            string[] parts = value.Split("_");
            string camelCaseString = "";
            foreach (string part in parts)
            {
                camelCaseString = camelCaseString + ToProperCase(part);
            }
            return camelCaseString;
        }

        public static string ToLowerCamelCase(this string value)
        {
            string cc = ToCamelCase(value);

            return cc.Substring(0, 1).ToLower() + cc.Substring(1);
        }

        public static string ToUpperCamelCase(this string value)
        {
            return ToCamelCase(value);
        }

        public static string ToProperCase(this string value)
        {
            return value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();
        }

        public static string UpperCaseFirstCharacter(this string value)
        {
            return value.Substring(0, 1).ToUpper() + value.Substring(1);
        }

        public static string LowerCaseFirstCharacter(this string value)
        {
            return value.Substring(0, 1).ToLower() + value.Substring(1);
        }
    }
}
