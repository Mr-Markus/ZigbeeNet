
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
    /// Illuminance Level Sensing cluster implementation (Cluster ID 0x0401).
    ///
    /// The cluster provides an interface to illuminance level sensing functionality,
    /// including configuration and provision of notifications of whether the illuminance is
    /// within, above or below a target band.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclIlluminanceLevelSensingCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0401;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Illuminance Level Sensing";

        // Attribute constants

        /// <summary>
        /// The LevelStatus attribute indicates whether the measured illuminance is above,
        /// below, or within a band around IlluminanceTargetLevel .
        /// </summary>
        public const ushort ATTR_LEVELSTATUS = 0x0000;

        /// <summary>
        /// The LightSensorType attribute specifies the electronic type of the light sensor.
        /// </summary>
        public const ushort ATTR_LIGHTSENSORTYPE = 0x0001;

        /// <summary>
        /// The IlluminanceTargetLevel attribute specifies the target illuminance level.
        /// This target level is taken as the centre of a 'dead band', which must be sufficient in
        /// width, with hysteresis bands at both top and bottom, to provide reliable
        /// notifications without 'chatter'. Such a dead band and hysteresis bands must be
        /// provided by any implementation of this cluster. (N.B. Manufacturer specific
        /// attributes may be provided to configure these).
        /// IlluminanceTargetLevel represents illuminance in Lux (symbol lx) as follows:
        /// IlluminanceTargetLevel = 10,000 x log10 Illuminance
        /// Where 1 lx <= Illuminance <=3.576 Mlx, corresponding to a MeasuredValue in the
        /// range 0 to 0xfffe.
        /// A value of 0xffff indicates that this attribute is not valid.
        /// </summary>
        public const ushort ATTR_ILLUMINANCETARGETLEVEL = 0x0010;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(3);

            attributeMap.Add(ATTR_LEVELSTATUS, new ZclAttribute(this, ATTR_LEVELSTATUS, "Level Status", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, true));
            attributeMap.Add(ATTR_LIGHTSENSORTYPE, new ZclAttribute(this, ATTR_LIGHTSENSORTYPE, "Light Sensor Type", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_ILLUMINANCETARGETLEVEL, new ZclAttribute(this, ATTR_ILLUMINANCETARGETLEVEL, "Illuminance Target Level", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Illuminance Level Sensing cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclIlluminanceLevelSensingCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }
    }
}
