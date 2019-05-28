using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Extensions;

namespace ZigBeeNet.Hardware.TI.CC2531.Packet.AF
{
    /// <summary>
    /// This command enables the tester to register an application’s endpoint description
    /// </summary>
    public class AF_REGISTER : ZToolPacket
    {
        /// <summary>
        /// Specifies the endpoint of the device 
        /// </summary>
        public byte EndPoint { get; private set; }

        /// <summary>
        /// Specifies the profile Id of the application 
        /// </summary>
        public ushort AppProfId { get; private set; }

        /// <summary>
        /// Specifies the device description Id for  this endpoint 
        /// </summary>
        public ushort AppDeviceId { get; private set; }

        /// <summary>
        /// Specifies the device version number
        /// </summary>
        public byte AddDevVer { get; private set; }

        /// <summary>
        /// Specifies latency.  
        ///     0x00-No latency 
        ///     0x01-fast beacons 
        ///     0x02-slow beacons 
        /// </summary>
        public byte LatencyReq { get; private set; }

        /// <summary>
        /// Specifies the number of Input cluster Id’s following in the AppInClusterList
        /// </summary>
        public byte AppNumInClusters { get; private set; }

        /// <summary>
        /// Specifies the list of Input Cluster Id’s
        /// </summary>
        public ushort[] AppInClusterList { get; private set; }

        /// <summary>
        /// Specifies the number of Output cluster Id’s following in the AppOutClusterList
        /// </summary>
        public byte AppNumOutClusters { get; private set; }

        /// <summary>
        /// Specifies the list of Output Cluster Id’s 
        /// </summary>
        public ushort[] AppOutClusterList { get; private set; }

        public AF_REGISTER(byte endpoint, ushort appProfId, ushort appDeviceId, byte addDevVer,
                                ushort[] appInClusterList, ushort[] appOutClusterList)
        {
            EndPoint = endpoint;
            AppProfId = appProfId;
            AppDeviceId = appDeviceId;
            AddDevVer = addDevVer;
            AppNumInClusters = (byte)appInClusterList.Length;
            AppInClusterList = new ushort[appInClusterList.Length];
            AppNumOutClusters = (byte)appOutClusterList.Length;
            AppOutClusterList = new ushort[appOutClusterList.Length];

            byte[] framedata = new byte[9 + appInClusterList.Length * 2 + AppOutClusterList.Length * 2];
            framedata[0] = EndPoint;
            framedata[1] = AppProfId.GetByte(0);
            framedata[2] = AppProfId.GetByte(1);
            framedata[3] = AppDeviceId.GetByte(0);
            framedata[4] = AppDeviceId.GetByte(1);
            framedata[5] = AddDevVer;
            framedata[6] = 0;
            framedata[7] = AppNumInClusters;
            for (int i = 0; i < AppInClusterList.Length; i++)
            {
                framedata[(i * 2) + 8] = AppInClusterList[i].GetByte(0);
                framedata[(i * 2) + 9] = AppInClusterList[i].GetByte(1);
            }
            framedata[((AppInClusterList.Length) * 2) + 8] = AppNumOutClusters;
            for (int i = 0; i < AppOutClusterList.Length; i++)
            {
                framedata[(i * 2) + ((AppInClusterList.Length) * 2) + 9] = AppOutClusterList[i].GetByte(0);
                framedata[(i * 2) + ((AppInClusterList.Length) * 2) + 10] = AppOutClusterList[i].GetByte(1);
            }

            BuildPacket(new DoubleByte((ushort)ZToolCMD.AF_REGISTER), framedata);
        }
    }
}
