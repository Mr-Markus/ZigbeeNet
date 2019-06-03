using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Util
{
    public class ColorConverter
    {
        public static CieColor RgbToCie(int red, int green, int blue)
        {
            // Apply a gamma correction to the RGB values, which makes the color more vivid and more the like the color displayed on the screen of your device
            var r = (red > 0.04045) ? Math.Pow((red + 0.055) / (1.0 + 0.055), 2.4) : (red / 12.92);
            var g = (green > 0.04045) ? Math.Pow((green + 0.055) / (1.0 + 0.055), 2.4) : (green / 12.92);
            var b = (blue > 0.04045) ? Math.Pow((blue + 0.055) / (1.0 + 0.055), 2.4) : (blue / 12.92);

            // RGB values to XYZ using the Wide RGB D65 conversion formula
            var X = r * 0.664511 + g * 0.154324 + b * 0.162028;
            var Y = r * 0.283881 + g * 0.668433 + b * 0.047685;
            var Z = r * 0.000088 + g * 0.072310 + b * 0.986039;

            // Calculate the xy values from the XYZ values
            var x = (X / (X + Y + Z));
            var y = (Y / (X + Y + Z));

            return new CieColor(x, y);
        }
    }
}
