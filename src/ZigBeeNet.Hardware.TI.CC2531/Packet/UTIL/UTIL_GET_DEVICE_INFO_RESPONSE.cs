using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Hardware.TI.CC2531.Util;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.UTIL
{
    public class UTIL_GET_DEVICE_INFO_RESPONSE : ZToolPacket 
    {
    /// <name>TI.ZPI1.SYS_GET_DEVICE_INFO_RESPONSE.AssocDevicesList</name>
    /// <summary>Dynamic array; Assoc Devices List</summary>
    public DoubleByte[] AssocDevicesList { get; private set; }
    /// <name>TI.ZPI1.SYS_GET_DEVICE_INFO_RESPONSE.DeviceState</name>
    /// <summary>Device Type</summary>
    public byte DeviceState { get; private set; }
    /// <name>TI.ZPI1.SYS_GET_DEVICE_INFO_RESPONSE.DeviceType</name>
    /// <summary>Bitmap byte field indicating device type; where bits 0 to 2 indicate the capability for the device to
    /// operate as a coordinator; router; or end device; respectively</summary>
    public byte DeviceType { get; private set; }
    /// <name>TI.ZPI1.SYS_GET_DEVICE_INFO_RESPONSE.IEEEAddr</name>
    /// <summary>IEEE Address</summary>
    public ZToolAddress64 IEEEAddr { get; private set; }
    /// <name>TI.ZPI1.SYS_GET_DEVICE_INFO_RESPONSE.NumAssocDevices</name>
    /// <summary>Number Assoc Devices</summary>
    public int NumAssocDevices { get; private set; }
    /// <name>TI.ZPI1.SYS_GET_DEVICE_INFO_RESPONSE.ShortAddress</name>
    /// <summary>Short Address</summary>
    public ZToolAddress16 ShortAddress { get; private set; }
    /// <name>TI.ZPI1.SYS_GET_DEVICE_INFO_RESPONSE.Status</name>
    /// <summary>The fail status is returned if the address value in the command message was not within the valid
    /// range.</summary>
    public byte Status { get; private set; }

    /// <name>TI.ZPI1.SYS_GET_DEVICE_INFO_RESPONSE</name>
    /// <summary>Constructor</summary>
    public UTIL_GET_DEVICE_INFO_RESPONSE()
    {
        this.AssocDevicesList = new DoubleByte[0xff];
    }

    public UTIL_GET_DEVICE_INFO_RESPONSE(byte[] framedata)
    {

        this.Status = framedata[0];
        byte[] bytes = new byte[8];
        for (int i = 0; i < 8; i++)
        {
            bytes[7 - i] = (byte)framedata[i + 1];
        }
        this.IEEEAddr = new ZToolAddress64(bytes);
        this.ShortAddress = new ZToolAddress16(framedata[9], framedata[10]);
        this.DeviceType = framedata[11];
        this.DeviceState = framedata[12];
        this.NumAssocDevices = framedata[13];
        // AssocDevicesList=new DoubleByte[(framedata.length-14)/2];//Actually more than NumAssocDevices
        AssocDevicesList = new DoubleByte[this.NumAssocDevices];
        for (int i = 0; i < this.AssocDevicesList.Length; i++)
        {
            AssocDevicesList[i] = new DoubleByte(framedata[14 + (i * 2)], framedata[15 + (i * 2)]);
        }

        BuildPacket(new DoubleByte((ushort)ZToolCMD.UTIL_GET_DEVICE_INFO_RESPONSE), framedata);
    }

    public override string ToString()
    {
        return "UTIL_GET_DEVICE_INFO_RESPONSE{" + "AssocDevicesList=" + AssocDevicesList
                + ", DeviceState=" + DeviceState + ", DeviceType=" + DeviceType + ", IEEEAddr=" + IEEEAddr
                + ", NumAssocDevices=" + NumAssocDevices + ", ShortAddress=" + ShortAddress + ", Status="
                + Status + '}';
    }
}
}
