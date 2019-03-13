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
    /// Electrical Measurementcluster implementation (Cluster ID 0x0B04).
    ///
    /// This cluster provides a mechanism for querying data about the electrical properties as measured
    /// by the device. This cluster may be implemented on any device type and be implemented on a per-endpoint
    /// basis. For example, a power  strip device could represent each outlet on a  different endpoint and
    /// report electrical  information for each individual outlet. The only caveat is that if you implement
    /// an attribute that has an associated multiplier and divisor, then you must implement the associated
    /// multiplier and divisor attributes. For example if you implement DCVoltage, you must also implement
    /// DCVoltageMultiplier and DCVoltageDivisor.
    /// <p>
    /// If you are interested in reading information about the power supply or battery level on the device,
    /// please see the Power Configuration cluster.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclElectricalMeasurementCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0B04;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Electrical Measurement";

        /* Attribute constants */

        /// <summary>
        /// This attribute indicates a device’s measurement capabilities. This will be indicated by setting
        /// the desire measurement bits to 1.
        /// </summary>
        public const ushort ATTR_MEASUREMENTTYPE = 0x0000;

        /// <summary>
        /// The ACFrequency attribute represents the most recent AC Frequency reading in Hertz (Hz).
        /// If the frequency cannot be measured, a value of 0xFFFF is returned.
        /// </summary>
        public const ushort ATTR_ACFREQUENCY = 0x0300;

        /// <summary>
        /// Active power represents the current demand of active power delivered or received at the
        /// premises, in kW. Positive values indicate power delivered to the premises where negative
        /// values indicate power received from the premises. In case if device is capable of measuring
        /// multi elements or phases then this will be net active power value.
        /// </summary>
        public const ushort ATTR_TOTALACTIVEPOWER = 0x0304;

        /// <summary>
        /// Reactive power represents the  current demand of reactive power delivered or
        /// received at the premises, in kVAr. Positive values indicate power delivered to
        /// the premises where negative values indicate power received from the premises. In
        /// case if device is capable of measuring multi elements or phases then this will be net reactive
        /// power value.
        /// </summary>
        public const ushort ATTR_TOTALREACTIVEPOWER = 0x0305;

        /// <summary>
        /// Represents the current demand of apparent power, in kVA. In case if device is capable of
        /// measuring multi elements or phases then this will be net apparent power value.
        /// </summary>
        public const ushort ATTR_TOTALAPPARENTPOWER = 0x0306;

        /// <summary>
        /// Represents the  most recent RMS voltage reading in Volts (V). If the RMS voltage cannot be
        /// measured, a value of 0xFFFF is returned.
        /// </summary>
        public const ushort ATTR_RMSVOLTAGE = 0x0505;

        /// <summary>
        /// Represents the most recent RMS current reading in Amps (A). If the power cannot be measured,
        /// a value of 0xFFFF is returned.
        /// </summary>
        public const ushort ATTR_RMSCURRENT = 0x0508;

        /// <summary>
        /// Represents the single phase or Phase A, current demand of active power delivered or received at
        /// the premises, in Watts (W). Positive values indicate power delivered to the premises where negative
        /// values indicate power received from the premises.
        /// </summary>
        public const ushort ATTR_ACTIVEPOWER = 0x050B;

        /// <summary>
        /// </summary>
        public const ushort ATTR_ACVOLTAGEMULTIPLIER = 0x0600;

        /// <summary>
        /// </summary>
        public const ushort ATTR_ACVOLTAGEDIVISOR = 0x0601;

        /// <summary>
        /// Provides a value to be multiplied against the InstantaneousCurrent and RMSCurrentattributes.
        /// his attribute must be used in conjunction with the ACCurrentDivisorattribute. 0x0000 is an invalid value for this attribute.
        /// </summary>
        public const ushort ATTR_ACCURRENTMULTIPLIER = 0x0602;

        /// <summary>
        /// Provides  a  value  to  be  divided  against the ACCurrent, InstantaneousCurrent and
        /// RMSCurrentattributes. This attribute must be used in conjunction with the ACCurrentMultiplierattribute
        /// 0x0000 is an invalid value for this attribute.
        /// </summary>
        public const ushort ATTR_ACCURRENTDIVISOR = 0x0603;

        /// <summary>
        /// Provides a value to be multiplied against the InstantaneousPower and ActivePowerattributes.
        /// This attribute must be used in conjunction with the ACPowerDivisorattribute. 0x0000 is an invalid
        /// value for this attribute
        /// </summary>
        public const ushort ATTR_ACPOWERMULTIPLIER = 0x0604;

        /// <summary>
        /// Provides a value to be divided against the InstantaneousPower and ActivePowerattributes.
        /// This  attribute must be used in conjunction with the ACPowerMultiplierattribute. 0x0000 is an
        /// invalid value for this attribute.
        /// </summary>
        public const ushort ATTR_ACPOWERDIVISOR = 0x0605;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(14);

            ZclClusterType electricalMeasurement = ZclClusterType.GetValueById(ClusterType.ELECTRICAL_MEASUREMENT);

            attributeMap.Add(ATTR_MEASUREMENTTYPE, new ZclAttribute(electricalMeasurement, ATTR_MEASUREMENTTYPE, "MeasurementType", ZclDataType.Get(DataType.BITMAP_32_BIT), true, true, false, false));
            attributeMap.Add(ATTR_ACFREQUENCY, new ZclAttribute(electricalMeasurement, ATTR_ACFREQUENCY, "ACFrequency", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TOTALACTIVEPOWER, new ZclAttribute(electricalMeasurement, ATTR_TOTALACTIVEPOWER, "TotalActivePower", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TOTALREACTIVEPOWER, new ZclAttribute(electricalMeasurement, ATTR_TOTALREACTIVEPOWER, "TotalReactivePower", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TOTALAPPARENTPOWER, new ZclAttribute(electricalMeasurement, ATTR_TOTALAPPARENTPOWER, "TotalApparentPower", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGE, new ZclAttribute(electricalMeasurement, ATTR_RMSVOLTAGE, "RMSVoltage", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RMSCURRENT, new ZclAttribute(electricalMeasurement, ATTR_RMSCURRENT, "RMSCurrent", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACTIVEPOWER, new ZclAttribute(electricalMeasurement, ATTR_ACTIVEPOWER, "ActivePower", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACVOLTAGEMULTIPLIER, new ZclAttribute(electricalMeasurement, ATTR_ACVOLTAGEMULTIPLIER, "ACVoltageMultiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACVOLTAGEDIVISOR, new ZclAttribute(electricalMeasurement, ATTR_ACVOLTAGEDIVISOR, "ACVoltageDivisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACCURRENTMULTIPLIER, new ZclAttribute(electricalMeasurement, ATTR_ACCURRENTMULTIPLIER, "ACCurrentMultiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACCURRENTDIVISOR, new ZclAttribute(electricalMeasurement, ATTR_ACCURRENTDIVISOR, "ACCurrentDivisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACPOWERMULTIPLIER, new ZclAttribute(electricalMeasurement, ATTR_ACPOWERMULTIPLIER, "ACPowerMultiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACPOWERDIVISOR, new ZclAttribute(electricalMeasurement, ATTR_ACPOWERDIVISOR, "ACPowerDivisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Electrical Measurement cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclElectricalMeasurementCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Get the MeasurementType attribute [attribute ID0].
        ///
        /// This attribute indicates a device’s measurement capabilities. This will be indicated by setting
        /// the desire measurement bits to 1.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetMeasurementTypeAsync()
        {
            return Read(_attributes[ATTR_MEASUREMENTTYPE]);
        }

        /// <summary>
        /// Synchronously Get the MeasurementType attribute [attribute ID0].
        ///
        /// This attribute indicates a device’s measurement capabilities. This will be indicated by setting
        /// the desire measurement bits to 1.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public int GetMeasurementType(long refreshPeriod)
        {
            if (_attributes[ATTR_MEASUREMENTTYPE].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_MEASUREMENTTYPE].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_MEASUREMENTTYPE]);
        }


        /// <summary>
        /// Get the ACFrequency attribute [attribute ID768].
        ///
        /// The ACFrequency attribute represents the most recent AC Frequency reading in Hertz (Hz).
        /// If the frequency cannot be measured, a value of 0xFFFF is returned.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetACFrequencyAsync()
        {
            return Read(_attributes[ATTR_ACFREQUENCY]);
        }

        /// <summary>
        /// Synchronously Get the ACFrequency attribute [attribute ID768].
        ///
        /// The ACFrequency attribute represents the most recent AC Frequency reading in Hertz (Hz).
        /// If the frequency cannot be measured, a value of 0xFFFF is returned.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetACFrequency(long refreshPeriod)
        {
            if (_attributes[ATTR_ACFREQUENCY].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ACFREQUENCY].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ACFREQUENCY]);
        }


        /// <summary>
        /// Get the TotalActivePower attribute [attribute ID772].
        ///
        /// Active power represents the current demand of active power delivered or received at the
        /// premises, in kW. Positive values indicate power delivered to the premises where negative
        /// values indicate power received from the premises. In case if device is capable of measuring
        /// multi elements or phases then this will be net active power value.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetTotalActivePowerAsync()
        {
            return Read(_attributes[ATTR_TOTALACTIVEPOWER]);
        }

        /// <summary>
        /// Synchronously Get the TotalActivePower attribute [attribute ID772].
        ///
        /// Active power represents the current demand of active power delivered or received at the
        /// premises, in kW. Positive values indicate power delivered to the premises where negative
        /// values indicate power received from the premises. In case if device is capable of measuring
        /// multi elements or phases then this will be net active power value.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public int GetTotalActivePower(long refreshPeriod)
        {
            if (_attributes[ATTR_TOTALACTIVEPOWER].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_TOTALACTIVEPOWER].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_TOTALACTIVEPOWER]);
        }


        /// <summary>
        /// Get the TotalReactivePower attribute [attribute ID773].
        ///
        /// Reactive power represents the  current demand of reactive power delivered or
        /// received at the premises, in kVAr. Positive values indicate power delivered to
        /// the premises where negative values indicate power received from the premises. In
        /// case if device is capable of measuring multi elements or phases then this will be net reactive
        /// power value.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetTotalReactivePowerAsync()
        {
            return Read(_attributes[ATTR_TOTALREACTIVEPOWER]);
        }

        /// <summary>
        /// Synchronously Get the TotalReactivePower attribute [attribute ID773].
        ///
        /// Reactive power represents the  current demand of reactive power delivered or
        /// received at the premises, in kVAr. Positive values indicate power delivered to
        /// the premises where negative values indicate power received from the premises. In
        /// case if device is capable of measuring multi elements or phases then this will be net reactive
        /// power value.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public int GetTotalReactivePower(long refreshPeriod)
        {
            if (_attributes[ATTR_TOTALREACTIVEPOWER].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_TOTALREACTIVEPOWER].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_TOTALREACTIVEPOWER]);
        }


        /// <summary>
        /// Get the TotalApparentPower attribute [attribute ID774].
        ///
        /// Represents the current demand of apparent power, in kVA. In case if device is capable of
        /// measuring multi elements or phases then this will be net apparent power value.
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetTotalApparentPowerAsync()
        {
            return Read(_attributes[ATTR_TOTALAPPARENTPOWER]);
        }

        /// <summary>
        /// Synchronously Get the TotalApparentPower attribute [attribute ID774].
        ///
        /// Represents the current demand of apparent power, in kVA. In case if device is capable of
        /// measuring multi elements or phases then this will be net apparent power value.
        ///
        /// The attribute is of type uint.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public uint GetTotalApparentPower(long refreshPeriod)
        {
            if (_attributes[ATTR_TOTALAPPARENTPOWER].IsLastValueCurrent(refreshPeriod))
            {
                return (uint)_attributes[ATTR_TOTALAPPARENTPOWER].LastValue;
            }

            return (uint)ReadSync(_attributes[ATTR_TOTALAPPARENTPOWER]);
        }


        /// <summary>
        /// Get the RMSVoltage attribute [attribute ID1285].
        ///
        /// Represents the  most recent RMS voltage reading in Volts (V). If the RMS voltage cannot be
        /// measured, a value of 0xFFFF is returned.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetRMSVoltageAsync()
        {
            return Read(_attributes[ATTR_RMSVOLTAGE]);
        }

        /// <summary>
        /// Synchronously Get the RMSVoltage attribute [attribute ID1285].
        ///
        /// Represents the  most recent RMS voltage reading in Volts (V). If the RMS voltage cannot be
        /// measured, a value of 0xFFFF is returned.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetRMSVoltage(long refreshPeriod)
        {
            if (_attributes[ATTR_RMSVOLTAGE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_RMSVOLTAGE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_RMSVOLTAGE]);
        }


        /// <summary>
        /// Get the RMSCurrent attribute [attribute ID1288].
        ///
        /// Represents the most recent RMS current reading in Amps (A). If the power cannot be measured,
        /// a value of 0xFFFF is returned.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetRMSCurrentAsync()
        {
            return Read(_attributes[ATTR_RMSCURRENT]);
        }

        /// <summary>
        /// Synchronously Get the RMSCurrent attribute [attribute ID1288].
        ///
        /// Represents the most recent RMS current reading in Amps (A). If the power cannot be measured,
        /// a value of 0xFFFF is returned.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetRMSCurrent(long refreshPeriod)
        {
            if (_attributes[ATTR_RMSCURRENT].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_RMSCURRENT].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_RMSCURRENT]);
        }


        /// <summary>
        /// Get the ActivePower attribute [attribute ID1291].
        ///
        /// Represents the single phase or Phase A, current demand of active power delivered or received at
        /// the premises, in Watts (W). Positive values indicate power delivered to the premises where negative
        /// values indicate power received from the premises.
        ///
        /// The attribute is of type short.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetActivePowerAsync()
        {
            return Read(_attributes[ATTR_ACTIVEPOWER]);
        }

        /// <summary>
        /// Synchronously Get the ActivePower attribute [attribute ID1291].
        ///
        /// Represents the single phase or Phase A, current demand of active power delivered or received at
        /// the premises, in Watts (W). Positive values indicate power delivered to the premises where negative
        /// values indicate power received from the premises.
        ///
        /// The attribute is of type short.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public short GetActivePower(long refreshPeriod)
        {
            if (_attributes[ATTR_ACTIVEPOWER].IsLastValueCurrent(refreshPeriod))
            {
                return (short)_attributes[ATTR_ACTIVEPOWER].LastValue;
            }

            return (short)ReadSync(_attributes[ATTR_ACTIVEPOWER]);
        }


        /// <summary>
        /// Get the ACVoltageMultiplier attribute [attribute ID1536].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetACVoltageMultiplierAsync()
        {
            return Read(_attributes[ATTR_ACVOLTAGEMULTIPLIER]);
        }

        /// <summary>
        /// Synchronously Get the ACVoltageMultiplier attribute [attribute ID1536].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetACVoltageMultiplier(long refreshPeriod)
        {
            if (_attributes[ATTR_ACVOLTAGEMULTIPLIER].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ACVOLTAGEMULTIPLIER].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ACVOLTAGEMULTIPLIER]);
        }


        /// <summary>
        /// Get the ACVoltageDivisor attribute [attribute ID1537].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetACVoltageDivisorAsync()
        {
            return Read(_attributes[ATTR_ACVOLTAGEDIVISOR]);
        }

        /// <summary>
        /// Synchronously Get the ACVoltageDivisor attribute [attribute ID1537].
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetACVoltageDivisor(long refreshPeriod)
        {
            if (_attributes[ATTR_ACVOLTAGEDIVISOR].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ACVOLTAGEDIVISOR].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ACVOLTAGEDIVISOR]);
        }


        /// <summary>
        /// Get the ACCurrentMultiplier attribute [attribute ID1538].
        ///
        /// Provides a value to be multiplied against the InstantaneousCurrent and RMSCurrentattributes.
        /// his attribute must be used in conjunction with the ACCurrentDivisorattribute. 0x0000 is an invalid value for this attribute.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetACCurrentMultiplierAsync()
        {
            return Read(_attributes[ATTR_ACCURRENTMULTIPLIER]);
        }

        /// <summary>
        /// Synchronously Get the ACCurrentMultiplier attribute [attribute ID1538].
        ///
        /// Provides a value to be multiplied against the InstantaneousCurrent and RMSCurrentattributes.
        /// his attribute must be used in conjunction with the ACCurrentDivisorattribute. 0x0000 is an invalid value for this attribute.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetACCurrentMultiplier(long refreshPeriod)
        {
            if (_attributes[ATTR_ACCURRENTMULTIPLIER].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ACCURRENTMULTIPLIER].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ACCURRENTMULTIPLIER]);
        }


        /// <summary>
        /// Get the ACCurrentDivisor attribute [attribute ID1539].
        ///
        /// Provides  a  value  to  be  divided  against the ACCurrent, InstantaneousCurrent and
        /// RMSCurrentattributes. This attribute must be used in conjunction with the ACCurrentMultiplierattribute
        /// 0x0000 is an invalid value for this attribute.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetACCurrentDivisorAsync()
        {
            return Read(_attributes[ATTR_ACCURRENTDIVISOR]);
        }

        /// <summary>
        /// Synchronously Get the ACCurrentDivisor attribute [attribute ID1539].
        ///
        /// Provides  a  value  to  be  divided  against the ACCurrent, InstantaneousCurrent and
        /// RMSCurrentattributes. This attribute must be used in conjunction with the ACCurrentMultiplierattribute
        /// 0x0000 is an invalid value for this attribute.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetACCurrentDivisor(long refreshPeriod)
        {
            if (_attributes[ATTR_ACCURRENTDIVISOR].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ACCURRENTDIVISOR].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ACCURRENTDIVISOR]);
        }


        /// <summary>
        /// Get the ACPowerMultiplier attribute [attribute ID1540].
        ///
        /// Provides a value to be multiplied against the InstantaneousPower and ActivePowerattributes.
        /// This attribute must be used in conjunction with the ACPowerDivisorattribute. 0x0000 is an invalid
        /// value for this attribute
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetACPowerMultiplierAsync()
        {
            return Read(_attributes[ATTR_ACPOWERMULTIPLIER]);
        }

        /// <summary>
        /// Synchronously Get the ACPowerMultiplier attribute [attribute ID1540].
        ///
        /// Provides a value to be multiplied against the InstantaneousPower and ActivePowerattributes.
        /// This attribute must be used in conjunction with the ACPowerDivisorattribute. 0x0000 is an invalid
        /// value for this attribute
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetACPowerMultiplier(long refreshPeriod)
        {
            if (_attributes[ATTR_ACPOWERMULTIPLIER].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ACPOWERMULTIPLIER].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ACPOWERMULTIPLIER]);
        }


        /// <summary>
        /// Get the ACPowerDivisor attribute [attribute ID1541].
        ///
        /// Provides a value to be divided against the InstantaneousPower and ActivePowerattributes.
        /// This  attribute must be used in conjunction with the ACPowerMultiplierattribute. 0x0000 is an
        /// invalid value for this attribute.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetACPowerDivisorAsync()
        {
            return Read(_attributes[ATTR_ACPOWERDIVISOR]);
        }

        /// <summary>
        /// Synchronously Get the ACPowerDivisor attribute [attribute ID1541].
        ///
        /// Provides a value to be divided against the InstantaneousPower and ActivePowerattributes.
        /// This  attribute must be used in conjunction with the ACPowerMultiplierattribute. 0x0000 is an
        /// invalid value for this attribute.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetACPowerDivisor(long refreshPeriod)
        {
            if (_attributes[ATTR_ACPOWERDIVISOR].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_ACPOWERDIVISOR].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_ACPOWERDIVISOR]);
        }

    }
}
