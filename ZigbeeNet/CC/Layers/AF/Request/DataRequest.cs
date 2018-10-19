using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.AF
{
    public class DataRequest : ZpiSREQ
    {
        public DataRequest()
            : base(AfCommand.dataRequest)
        {

        }

        /// <summary>
        /// Short address of the destination device
        /// </summary>
        public ushort DestinationAddress
        {
            get
            {
                return (ushort)RequestArguments["dstaddr"];
            }
            set
            {
                RequestArguments["dstaddr"] = value;
            }
        }

        /// <summary>
        /// Endpoint of the destination device 
        /// </summary>
        public byte DestinationEndpoint
        {
            get
            {
                return (byte)RequestArguments["destendpoint"];
            }
            set
            {
                RequestArguments["destendpoint"] = value;
            }
        }

        /// <summary>
        /// Endpoint of the source device 
        /// </summary>
        public byte SourceEndpoint
        {
            get
            {
                return (byte)RequestArguments["srcendpoint"];
            }
            set
            {
                RequestArguments["srcendpoint"] = value;
            }
        }

        /// <summary>
        /// Specifies the cluster ID 
        /// </summary>
        public ushort Cluster
        {
            get
            {
                return (ushort)RequestArguments["clusterid"];
            }
            set
            {
                RequestArguments["clusterid"] = value;
            }
        }

        /// <summary>
        /// Specifies the transaction sequence number of the message
        /// </summary>
        public byte TransactionSeqNumber
        {
            get
            {
                return (byte)RequestArguments["transid"];
            }
            set
            {
                RequestArguments["transid"] = value;
            }
        }

        /// <summary>
        /// Transmit options bit mask according to the following defines 
        /// from AF.h: bit 1: sets ‘Wildcard Profile ID’; bit 4: turns on/off 
        /// ‘APS ACK’; bit 5 sets ‘discover route’; bit 6 sets ‘APS security’; 
        /// bit 7 sets ‘skip routing’
        /// </summary>
        public byte Options
        {
            get
            {
                return (byte)RequestArguments["options"];
            }
            set
            {
                RequestArguments["options"] = value;
            }
        }

        /// <summary>
        /// Specifies the number of hops allowed delivering the message (see AF_DEFAULT_RADIUS.) 
        /// </summary>
        public byte Radius
        {
            get
            {
                return (byte)RequestArguments["radius"];
            }
            set
            {
                RequestArguments["radius"] = value;
            }
        }

        /// <summary>
        /// Length of the data
        /// </summary>
        public byte Length
        {
            get
            {
                return (byte)RequestArguments["len"];
            }
            set
            {
                RequestArguments["len"] = value;
            }
        }

        /// <summary>
        /// 0-128 bytes data 
        /// </summary>
        public byte[] Data
        {
            get
            {
                return (byte[])RequestArguments["data"];
            }
            set
            {
                RequestArguments["data"] = value;
            }
        }
    }
}
