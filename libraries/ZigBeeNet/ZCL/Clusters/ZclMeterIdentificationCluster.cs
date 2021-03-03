
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Meter Identification cluster implementation (Cluster ID 0x0B01).
    ///
    /// This cluster provides attributes and commands for determining advanced information
    /// about utility metering device.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclMeterIdentificationCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0B01;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Meter Identification";

        // Attribute constants

        /// <summary>
        /// This attribute defines the meter manufacturer name, decided by manufacturer.
        /// </summary>
        public const ushort ATTR_COMPANYNAME = 0x0000;

        /// <summary>
        /// This attribute defines the Meter installation features, decided by
        /// manufacturer.
        /// </summary>
        public const ushort ATTR_METERTYPEID = 0x0001;

        /// <summary>
        /// This attribute defines the Meter Simple Metering information certification
        /// type, decided by manufacturer.
        /// </summary>
        public const ushort ATTR_DATAQUALITYID = 0x0004;
        public const ushort ATTR_CUSTOMERNAME = 0x0005;

        /// <summary>
        /// This attribute defines the meter model name, decided by manufacturer.
        /// </summary>
        public const ushort ATTR_MODEL = 0x0006;

        /// <summary>
        /// This attribute defines the meter part number, decided by manufacturer.
        /// </summary>
        public const ushort ATTR_PARTNUMBER = 0x0007;

        /// <summary>
        /// This attribute defines the meter revision code, decided by manufacturer.
        /// </summary>
        public const ushort ATTR_PRODUCTREVISION = 0x0008;

        /// <summary>
        /// This attribute defines the meter software revision code, decided by
        /// manufacturer.
        /// </summary>
        public const ushort ATTR_SOFTWAREREVISION = 0x000A;
        public const ushort ATTR_UTILITYNAME = 0x000B;

        /// <summary>
        /// This attribute is the unique identification ID of the premise connection point. It
        /// is also a contractual information known by the clients and indicated in the bill.
        /// </summary>
        public const ushort ATTR_POD = 0x000C;

        /// <summary>
        /// This attribute represents the InstantaneousDemand that can be distributed to the
        /// customer (e.g., 3.3KW power) without any risk of overload. The Available Power
        /// shall use the same formatting conventions as the one used in the simple metering
        /// cluster formatting attribute set for the InstantaneousDemand attribute, i.e.,
        /// the UnitOfMeasure and DemandFormatting.
        /// </summary>
        public const ushort ATTR_AVAILABLEPOWER = 0x000D;

        /// <summary>
        /// This attribute represents a threshold of InstantaneousDemand distributed to the
        /// customer (e.g., 4.191KW) that will lead to an imminent risk of overload. The
        /// PowerThreshold shall use the same formatting conventions as the one used in the
        /// AvailablePower attributes and therefore in the simple metering cluster
        /// formatting attribute set for the InstantaneousDemand attribute, i.e., the
        /// UnitOfMeasure and DemandFormatting.
        /// </summary>
        public const ushort ATTR_POWERTHRESHOLD = 0x000E;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(12);

            attributeMap.Add(ATTR_COMPANYNAME, new ZclAttribute(this, ATTR_COMPANYNAME, "Company Name", ZclDataType.Get(DataType.CHARACTER_STRING), true, true, false, false));
            attributeMap.Add(ATTR_METERTYPEID, new ZclAttribute(this, ATTR_METERTYPEID, "Meter Type ID", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DATAQUALITYID, new ZclAttribute(this, ATTR_DATAQUALITYID, "Data Quality ID", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CUSTOMERNAME, new ZclAttribute(this, ATTR_CUSTOMERNAME, "Customer Name", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_MODEL, new ZclAttribute(this, ATTR_MODEL, "Model", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));
            attributeMap.Add(ATTR_PARTNUMBER, new ZclAttribute(this, ATTR_PARTNUMBER, "Part Number", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));
            attributeMap.Add(ATTR_PRODUCTREVISION, new ZclAttribute(this, ATTR_PRODUCTREVISION, "Product Revision", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));
            attributeMap.Add(ATTR_SOFTWAREREVISION, new ZclAttribute(this, ATTR_SOFTWAREREVISION, "Software Revision", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));
            attributeMap.Add(ATTR_UTILITYNAME, new ZclAttribute(this, ATTR_UTILITYNAME, "Utility Name", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));
            attributeMap.Add(ATTR_POD, new ZclAttribute(this, ATTR_POD, "POD", ZclDataType.Get(DataType.CHARACTER_STRING), true, true, false, false));
            attributeMap.Add(ATTR_AVAILABLEPOWER, new ZclAttribute(this, ATTR_AVAILABLEPOWER, "Available Power", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_POWERTHRESHOLD, new ZclAttribute(this, ATTR_POWERTHRESHOLD, "Power Threshold", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Meter Identification cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclMeterIdentificationCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
