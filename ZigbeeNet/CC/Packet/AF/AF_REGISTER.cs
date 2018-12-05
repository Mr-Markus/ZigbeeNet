using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.AF
{
    /// <summary>
    /// This command enables the tester to register an application’s endpoint description
    /// </summary>
    public class AF_REGISTER : SynchronousRequest
    {
        /// <summary>
        /// Specifies the endpoint of the device 
        /// </summary>
        public byte EndPoint { get; private set; }

        /// <summary>
        /// Specifies the profile Id of the application 
        /// </summary>
        public DoubleByte AppProfId { get; private set; }

        /// <summary>
        /// Specifies the device description Id for  this endpoint 
        /// </summary>
        public DoubleByte AppDeviceId { get; private set; }

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
        public DoubleByte[] AppInClusterList { get; private set; }

        /// <summary>
        /// Specifies the number of Output cluster Id’s following in the AppOutClusterList
        /// </summary>
        public byte AppNumOutClusters { get; private set; }

        /// <summary>
        /// Specifies the list of Output Cluster Id’s 
        /// </summary>
        public DoubleByte[] AppOutClusterList { get; private set; }

        public AF_REGISTER(byte endpoint, DoubleByte appProfId, DoubleByte appDeviceId, byte addDevVer,
                                DoubleByte[] appInClusterList, DoubleByte[] appOutClusterList)
        {
            EndPoint = endpoint;
            AppProfId = appProfId;
            AppDeviceId = appDeviceId;
            AddDevVer = addDevVer;
            AppNumInClusters = (byte)appInClusterList.Length;
            AppInClusterList = new DoubleByte[appInClusterList.Length];
            AppNumOutClusters = (byte)appOutClusterList.Length;
            AppOutClusterList = new DoubleByte[appOutClusterList.Length];

            byte[] framedata = new byte[9 + appInClusterList.Length * 2 + AppOutClusterList.Length * 2];
            framedata[0] = EndPoint;
            framedata[1] = AppProfId.Lsb;
            framedata[2] = AppProfId.Msb;
            framedata[3] = AppDeviceId.Lsb;
            framedata[4] = AppDeviceId.Msb;
            framedata[5] = AddDevVer;
            framedata[6] = 0;
            framedata[7] = AppNumInClusters;
            for (int i = 0; i < AppInClusterList.Length; i++)
            {
                framedata[(i * 2) + 8] = AppInClusterList[i].Lsb;
                framedata[(i * 2) + 9] = AppInClusterList[i].Msb;
            }
            framedata[((AppInClusterList.Length) * 2) + 8] = AppNumOutClusters;
            for (int i = 0; i < AppOutClusterList.Length; i++)
            {
                framedata[(i * 2) + ((AppInClusterList.Length) * 2) + 9] = AppOutClusterList[i].Lsb;
                framedata[(i * 2) + ((AppInClusterList.Length) * 2) + 10] = AppOutClusterList[i].Msb;
            }

            BuildPacket(CommandType.AF_REGISTER, framedata);
        }
    }
}
