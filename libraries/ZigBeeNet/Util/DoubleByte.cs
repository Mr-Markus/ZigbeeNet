using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigBeeNet
{
    public static class DoubleByte
    {
        // /// <summary>
        // /// Gets or sets high byte
        // /// </summary>
        // public byte Msb { 
        //     get => (byte)(Value>>8);
        //     set => Value = (ushort)(((byte)Value) | value<<8);
        //      }

        // /// <summary>
        // /// Gets or sets low byte
        // /// </summary>
        // public byte Lsb { 
        //     get => unchecked( (byte)Value );
        //     set => Value = (ushort)(((byte)value) | Value&0xFF00 );
        //     }


        // // public DoubleByte()
        // // {

        // // }

        // /// <summary>
        // /// Decomposes a 16bit int into high and low bytes
        // /// </summary>
        // /// <param name="val"></param>
        // public DoubleByte(ushort val)
        // {
        //     Value = val;
        // }

        // /// <summary>
        // /// Constructs a 16bit value from two bytes (high and low)
        // /// </summary>
        // /// <param name="msb"></param>
        // /// <param name="lsb"></param>
        // public DoubleByte(byte msb, byte lsb) : this( (ushort)( msb<<8 | lsb))
        // {
           
        // }

        // public ushort Value { get; private set; }

        // public override string ToString()
        // {
        //     return Value.ToString();
        // }

        public static ushort Convert(byte MSB,byte LSB) => (ushort)(MSB<<8 | LSB);
        public static byte MSB(ushort doubleByte)  => (byte)(doubleByte>>8);
        public static byte LSB(ushort doubleByte)  => (byte)doubleByte;
    }
}
