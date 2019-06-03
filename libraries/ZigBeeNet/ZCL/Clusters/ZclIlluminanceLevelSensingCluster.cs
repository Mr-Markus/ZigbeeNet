// License text here

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Illuminance level sensingcluster implementation (Cluster ID 0x0401).
    ///
    /// The cluster provides an interface to illuminance level sensing functionality,
    /// including configuration and provision of notifications of whether the illuminance
    /// is within, above or below a target band.
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
        public const string CLUSTER_NAME = "Illuminance level sensing";

        /* Attribute constants */

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
        /// The IlluminanceTargetLevel attribute specifies the target illuminance level. This
        /// target level is taken as the centre of a 'dead band', which must be sufficient in
        /// width, with hysteresis bands at both top and bottom, to provide reliable
        /// notifications without 'chatter'. Such a dead band and hysteresis bands must be
        /// provided by any implementation of this cluster. (N.B. Manufacturer specific
        /// attributes may be provided to configure these).
        /// 
        /// IlluminanceTargetLevel represents illuminance in Lux (symbol lx) as follows:
        /// 
        /// IlluminanceTargetLevel = 10,000 x log10 Illuminance
        /// 
        /// Where 1 lx <= Illuminance <=3.576 Mlx, corresponding to a MeasuredValue in
        /// the range 0 to 0xfffe.
        /// 
        /// A value of 0xffff indicates that this attribute is not valid.
        /// </summary>
        public const ushort ATTR_ILLUMINANCETARGETLEVEL = 0x0010;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(3);

            ZclClusterType illuminancelevelsensing = ZclClusterType.GetValueById(ClusterType.ILLUMINANCE_LEVEL_SENSING);

            attributeMap.Add(ATTR_LEVELSTATUS, new ZclAttribute(illuminancelevelsensing, ATTR_LEVELSTATUS, "LevelStatus", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, true));
            attributeMap.Add(ATTR_LIGHTSENSORTYPE, new ZclAttribute(illuminancelevelsensing, ATTR_LIGHTSENSORTYPE, "LightSensorType", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_ILLUMINANCETARGETLEVEL, new ZclAttribute(illuminancelevelsensing, ATTR_ILLUMINANCETARGETLEVEL, "IlluminanceTargetLevel", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Illuminance level sensing cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclIlluminanceLevelSensingCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Get the LevelStatus attribute [attribute ID0].
        ///
        /// The LevelStatus attribute indicates whether the measured illuminance is above,
        /// below, or within a band around IlluminanceTargetLevel .
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetLevelStatusAsync()
        {
            return Read(_attributes[ATTR_LEVELSTATUS]);
        }

        /// <summary>
        /// Synchronously Get the LevelStatus attribute [attribute ID0].
        ///
        /// The LevelStatus attribute indicates whether the measured illuminance is above,
        /// below, or within a band around IlluminanceTargetLevel .
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetLevelStatus(long refreshPeriod)
        {
            if (_attributes[ATTR_LEVELSTATUS].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_LEVELSTATUS].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_LEVELSTATUS]);
        }


        /// <summary>
        /// Set reporting for the LevelStatus attribute [attribute ID0].
        ///
        /// The LevelStatus attribute indicates whether the measured illuminance is above,
        /// below, or within a band around IlluminanceTargetLevel .
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="minInterval">Minimum reporting period</param>
        /// <param name="maxInterval">Maximum reporting period</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetLevelStatusReporting(ushort minInterval, ushort maxInterval)
        {
            return SetReporting(_attributes[ATTR_LEVELSTATUS], minInterval, maxInterval);
        }


        /// <summary>
        /// Get the LightSensorType attribute [attribute ID1].
        ///
        /// The LightSensorType attribute specifies the electronic type of the light sensor.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetLightSensorTypeAsync()
        {
            return Read(_attributes[ATTR_LIGHTSENSORTYPE]);
        }

        /// <summary>
        /// Synchronously Get the LightSensorType attribute [attribute ID1].
        ///
        /// The LightSensorType attribute specifies the electronic type of the light sensor.
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetLightSensorType(long refreshPeriod)
        {
            if (_attributes[ATTR_LIGHTSENSORTYPE].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_LIGHTSENSORTYPE].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_LIGHTSENSORTYPE]);
        }


        /// <summary>
        /// Get the IlluminanceTargetLevel attribute [attribute ID16].
        ///
        /// The IlluminanceTargetLevel attribute specifies the target illuminance level. This
        /// target level is taken as the centre of a 'dead band', which must be sufficient in
        /// width, with hysteresis bands at both top and bottom, to provide reliable
        /// notifications without 'chatter'. Such a dead band and hysteresis bands must be
        /// provided by any implementation of this cluster. (N.B. Manufacturer specific
        /// attributes may be provided to configure these).
        /// 
        /// IlluminanceTargetLevel represents illuminance in Lux (symbol lx) as follows:
        /// 
        /// IlluminanceTargetLevel = 10,000 x log10 Illuminance
        /// 
        /// Where 1 lx <= Illuminance <=3.576 Mlx, corresponding to a MeasuredValue in
        /// the range 0 to 0xfffe.
        /// 
        /// A value of 0xffff indicates that this attribute is not valid.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetIlluminanceTargetLevelAsync()
        {
            return Read(_attributes[ATTR_ILLUMINANCETARGETLEVEL]);
        }

        /// <summary>
        /// Synchronously Get the IlluminanceTargetLevel attribute [attribute ID16].
        ///
        /// The IlluminanceTargetLevel attribute specifies the target illuminance level. This
        /// target level is taken as the centre of a 'dead band', which must be sufficient in
        /// width, with hysteresis bands at both top and bottom, to provide reliable
        /// notifications without 'chatter'. Such a dead band and hysteresis bands must be
        /// provided by any implementation of this cluster. (N.B. Manufacturer specific
        /// attributes may be provided to configure these).
        /// 
        /// IlluminanceTargetLevel represents illuminance in Lux (symbol lx) as follows:
        /// 
        /// IlluminanceTargetLevel = 10,000 x log10 Illuminance
        /// 
        /// Where 1 lx <= Illuminance <=3.576 Mlx, corresponding to a MeasuredValue in
        /// the range 0 to 0xfffe.
        /// 
        /// A value of 0xffff indicates that this attribute is not valid.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetIlluminanceTargetLevel(long refreshPeriod)
        {
            if (_attributes[ATTR_ILLUMINANCETARGETLEVEL].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ILLUMINANCETARGETLEVEL].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ILLUMINANCETARGETLEVEL]);
        }

    }
}
