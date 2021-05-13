using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
namespace ZigBeeNet
{
    public static partial class ByteHelper
    {

        public static ulong ToUInt64(this byte[] buffer) => ToUInt64(buffer,0);
        public static ulong ToUInt64(this byte[] buffer,int offset) => ToUInt64(buffer,ref offset);
        public static ulong ToUInt64(this byte[] buffer,ref int offset)
        {
            if (buffer.Length-offset<8)
                throw new ArgumentException("Buffer is too small",nameof(buffer));
            ulong result = buffer[offset++]
                            | (ulong)buffer[offset++]<<8
                            | (ulong)buffer[offset++]<<16
                            | (ulong)buffer[offset++]<<24
                            | (ulong)buffer[offset++]<<32
                            | (ulong)buffer[offset++]<<40
                            | (ulong)buffer[offset++]<<48
                            | (ulong)buffer[offset++]<<56;
            return result;
        }

        public static void FromUInt64(this byte[] buffer,ulong value) => FromUInt64(buffer,0,value);
        public static void FromUInt64(this byte[] buffer,int offset,ulong value) => FromUInt64(buffer,ref offset,value);
        public static void FromUInt64(this byte[] buffer,ref int offset,ulong value)
        {
            if (buffer.Length-offset<8)
                throw new ArgumentException("Buffer is too small",nameof(buffer));
            buffer[offset++] = (byte)(value);
            buffer[offset++] = (byte)(value>>8);
            buffer[offset++] = (byte)(value>>16);
            buffer[offset++] = (byte)(value>>24);
            buffer[offset++] = (byte)(value>>32);
            buffer[offset++] = (byte)(value>>40);
            buffer[offset++] = (byte)(value>>48);
            buffer[offset++] = (byte)(value>>56);
        }
        public static byte[] FromUInt64(ulong value) 
        {
            byte[] buffer=new byte[sizeof(ulong)];
            FromUInt64(buffer,0,value);
            return buffer;
        }

        public static uint ToUInt32(this byte[] buffer) => ToUInt32(buffer,0);
        public static uint ToUInt32(this byte[] buffer,int offset) => ToUInt32(buffer,ref offset);
        public static uint ToUInt32(this byte[] buffer,ref int offset)
        {
            if (buffer.Length-offset<4)
                throw new ArgumentException("Buffer is too small",nameof(buffer));
            uint result = buffer[offset++]
                            | (uint)buffer[offset++]<<8
                            | (uint)buffer[offset++]<<16
                            | (uint)buffer[offset++]<<24;
            return result;
        }

        public static void FromUInt32(this byte[] buffer,uint value) => FromUInt32(buffer,0,value);
        public static void FromUInt32(this byte[] buffer,int offset,uint value) => FromUInt32(buffer,ref offset,value);
        public static void FromUInt32(this byte[] buffer,ref int offset,uint value)
        {
            if (buffer.Length-offset<4)
                throw new ArgumentException("Buffer is too small",nameof(buffer));
            buffer[offset++] = (byte)(value);
            buffer[offset++] = (byte)(value>>8);
            buffer[offset++] = (byte)(value>>16);
            buffer[offset++] = (byte)(value>>24);
        }
        public static byte[] FromUInt32(uint value)
        {
            var buffer = new byte[sizeof(UInt32)];
            FromUInt32(buffer,0,value);
            return buffer;
        } 
    }
}
