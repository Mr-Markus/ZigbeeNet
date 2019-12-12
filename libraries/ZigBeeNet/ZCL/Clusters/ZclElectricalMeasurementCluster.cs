
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.ElectricalMeasurement;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Electrical Measurement cluster implementation (Cluster ID 0x0B04).
    ///
    /// This cluster provides a mechanism for querying data about the electrical properties as
    /// measured by the device. This cluster may be implemented on any device type and be
    /// implemented on a per-endpoint basis. For example, a power strip device could represent
    /// each outlet on a different endpoint and report electrical information for each
    /// individual outlet. The only caveat is that if you implement an attribute that has an
    /// associated multiplier and divisor, then you must implement the associated multiplier
    /// and divisor attributes. For example if you implement DCVoltage, you must also
    /// implement DCVoltageMultiplier and DCVoltageDivisor.
    /// If you are interested in reading information about the power supply or battery level on
    /// the device, please see the Power Configuration cluster.
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

        // Attribute constants

        /// <summary>
        /// This attribute indicates a device’s measurement capabilities. This will be
        /// indicated by setting the desire measurement bits to 1.
        /// </summary>
        public const ushort ATTR_MEASUREMENTTYPE = 0x0000;
        public const ushort ATTR_DCVOLTAGE = 0x0100;
        public const ushort ATTR_DCVOLTAGEMIN = 0x0101;
        public const ushort ATTR_DCVOLTAGEMAX = 0x0102;
        public const ushort ATTR_DCCURRENT = 0x0103;
        public const ushort ATTR_DCCURRENTMIN = 0x0104;
        public const ushort ATTR_DCCURRENTMAX = 0x0105;
        public const ushort ATTR_DCPOWER = 0x0106;
        public const ushort ATTR_DCPOWERMIN = 0x0107;
        public const ushort ATTR_DCPOWERMAX = 0x0108;
        public const ushort ATTR_DCVOLTAGEMULTIPLIER = 0x0200;
        public const ushort ATTR_DCVOLTAGEDIVISOR = 0x0201;
        public const ushort ATTR_DCCURRENTMULTIPLIER = 0x0202;
        public const ushort ATTR_DCCURRENTDIVISOR = 0x0203;
        public const ushort ATTR_DCPOWERMULTIPLIER = 0x0204;
        public const ushort ATTR_DCPOWERDIVISOR = 0x0205;

        /// <summary>
        /// The ACFrequency attribute represents the most recent AC Frequency reading in
        /// Hertz (Hz). If the frequency cannot be measured, a value of 0xFFFF is returned.
        /// </summary>
        public const ushort ATTR_ACFREQUENCY = 0x0300;
        public const ushort ATTR_ACFREQUENCYMIN = 0x0301;
        public const ushort ATTR_ACFREQUENCYMAX = 0x0302;
        public const ushort ATTR_NEUTRALCURRENT = 0x0303;

        /// <summary>
        /// Active power represents the current demand of active power delivered or received
        /// at the premises, in kW. Positive values indicate power delivered to the premises
        /// where negative values indicate power received from the premises. In case if device
        /// is capable of measuring multi elements or phases then this will be net active power
        /// value.
        /// </summary>
        public const ushort ATTR_TOTALACTIVEPOWER = 0x0304;

        /// <summary>
        /// Reactive power represents the current demand of reactive power delivered or
        /// received at the premises, in kVAr. Positive values indicate power delivered to the
        /// premises where negative values indicate power received from the premises. In case
        /// if device is capable of measuring multi elements or phases then this will be net
        /// reactive power value.
        /// </summary>
        public const ushort ATTR_TOTALREACTIVEPOWER = 0x0305;

        /// <summary>
        /// Represents the current demand of apparent power, in kVA. In case if device is
        /// capable of measuring multi elements or phases then this will be net apparent power
        /// value.
        /// </summary>
        public const ushort ATTR_TOTALAPPARENTPOWER = 0x0306;
        public const ushort ATTR_MEASURED1STHARMONICCURRENT = 0x0307;
        public const ushort ATTR_MEASURED3RDHARMONICCURRENT = 0x0308;
        public const ushort ATTR_MEASURED5THHARMONICCURRENT = 0x0309;
        public const ushort ATTR_MEASURED7THHARMONICCURRENT = 0x030A;
        public const ushort ATTR_MEASURED9THHARMONICCURRENT = 0x030B;
        public const ushort ATTR_MEASURED11THHARMONICCURRENT = 0x030C;
        public const ushort ATTR_MEASUREDPHASE1STHARMONICCURRENT = 0x030D;
        public const ushort ATTR_MEASUREDPHASE3RDHARMONICCURRENT = 0x030E;
        public const ushort ATTR_MEASUREDPHASE5THHARMONICCURRENT = 0x030F;
        public const ushort ATTR_MEASUREDPHASE7THHARMONICCURRENT = 0x0310;
        public const ushort ATTR_MEASUREDPHASE9THHARMONICCURRENT = 0x0311;
        public const ushort ATTR_MEASUREDPHASE11THHARMONICCURRENT = 0x0312;
        public const ushort ATTR_ACFREQUENCYMULTIPLIER = 0x0400;
        public const ushort ATTR_ACFREQUENCYDIVISOR = 0x0401;
        public const ushort ATTR_POWERMULTIPLIER = 0x0402;
        public const ushort ATTR_POWERDIVISOR = 0x0403;
        public const ushort ATTR_HARMONICCURRENTMULTIPLIER = 0x0404;
        public const ushort ATTR_PHASEHARMONICCURRENTMULTIPLIER = 0x0405;
        public const ushort ATTR_INSTANTANEOUSVOLTAGE = 0x0500;
        public const ushort ATTR_INSTANTANEOUSLINECURRENT = 0x0501;
        public const ushort ATTR_INSTANTANEOUSACTIVECURRENT = 0x0502;
        public const ushort ATTR_INSTANTANEOUSREACTIVECURRENT = 0x0503;
        public const ushort ATTR_INSTANTANEOUSPOWER = 0x0504;

        /// <summary>
        /// Represents the most recent RMS voltage reading in Volts (V). If the RMS voltage
        /// cannot be measured, a value of 0xFFFF is returned.
        /// </summary>
        public const ushort ATTR_RMSVOLTAGE = 0x0505;
        public const ushort ATTR_RMSVOLTAGEMIN = 0x0506;
        public const ushort ATTR_RMSVOLTAGEMAX = 0x0507;

        /// <summary>
        /// Represents the most recent RMS current reading in Amps (A). If the power cannot be
        /// measured, a value of 0xFFFF is returned.
        /// </summary>
        public const ushort ATTR_RMSCURRENT = 0x0508;
        public const ushort ATTR_RMSCURRENTMIN = 0x0509;
        public const ushort ATTR_RMSCURRENTMAX = 0x050A;

        /// <summary>
        /// Represents the single phase or Phase A, current demand of active power delivered or
        /// received at the premises, in Watts (W). Positive values indicate power delivered
        /// to the premises where negative values indicate power received from the premises.
        /// </summary>
        public const ushort ATTR_ACTIVEPOWER = 0x050B;
        public const ushort ATTR_ACTIVEPOWERMIN = 0x050C;
        public const ushort ATTR_ACTIVEPOWERMAX = 0x050D;
        public const ushort ATTR_REACTIVEPOWER = 0x050E;
        public const ushort ATTR_APPARENTPOWER = 0x050F;
        public const ushort ATTR_POWERFACTOR = 0x0510;
        public const ushort ATTR_AVERAGERMSVOLTAGEMEASUREMENTPERIOD = 0x0511;
        public const ushort ATTR_AVERAGERMSUNDERVOLTAGECOUNTER = 0x0513;
        public const ushort ATTR_RMSEXTREMEOVERVOLTAGEPERIOD = 0x0514;
        public const ushort ATTR_RMSEXTREMEUNDERVOLTAGEPERIOD = 0x0515;
        public const ushort ATTR_RMSVOLTAGESAGPERIOD = 0x0516;
        public const ushort ATTR_RMSVOLTAGESWELLPERIOD = 0x0517;
        public const ushort ATTR_ACVOLTAGEMULTIPLIER = 0x0600;
        public const ushort ATTR_ACVOLTAGEDIVISOR = 0x0601;

        /// <summary>
        /// Provides a value to be multiplied against the InstantaneousCurrent and
        /// RMSCurrentattributes. his attribute must be used in conjunction with the
        /// ACCurrentDivisorattribute. 0x0000 is an invalid value for this attribute.
        /// </summary>
        public const ushort ATTR_ACCURRENTMULTIPLIER = 0x0602;

        /// <summary>
        /// Provides a value to be divided against the ACCurrent, InstantaneousCurrent and
        /// RMSCurrentattributes. This attribute must be used in conjunction with the
        /// ACCurrentMultiplierattribute 0x0000 is an invalid value for this attribute.
        /// </summary>
        public const ushort ATTR_ACCURRENTDIVISOR = 0x0603;

        /// <summary>
        /// Provides a value to be multiplied against the InstantaneousPower and
        /// ActivePowerattributes. This attribute must be used in conjunction with the
        /// ACPowerDivisorattribute. 0x0000 is an invalid value for this attribute
        /// </summary>
        public const ushort ATTR_ACPOWERMULTIPLIER = 0x0604;

        /// <summary>
        /// Provides a value to be divided against the InstantaneousPower and
        /// ActivePowerattributes. This attribute must be used in conjunction with the
        /// ACPowerMultiplierattribute. 0x0000 is an invalid value for this attribute.
        /// </summary>
        public const ushort ATTR_ACPOWERDIVISOR = 0x0605;
        public const ushort ATTR_OVERLOADALARMSMASK = 0x0700;
        public const ushort ATTR_VOLTAGEOVERLOAD = 0x0701;
        public const ushort ATTR_CURRENTOVERLOAD = 0x0702;
        public const ushort ATTR_ACOVERLOADALARMSMASK = 0x0800;
        public const ushort ATTR_ACVOLTAGEOVERLOAD = 0x0801;
        public const ushort ATTR_ACCURRENTOVERLOAD = 0x0802;
        public const ushort ATTR_ACACTIVEPOWEROVERLOAD = 0x0803;
        public const ushort ATTR_ACREACTIVEPOWEROVERLOAD = 0x0804;
        public const ushort ATTR_AVERAGERMSOVERVOLTAGE = 0x0805;
        public const ushort ATTR_AVERAGERMSUNDERVOLTAGE = 0x0806;
        public const ushort ATTR_RMSEXTREMEOVERVOLTAGE = 0x0807;
        public const ushort ATTR_RMSEXTREMEUNDERVOLTAGE = 0x0808;
        public const ushort ATTR_RMSVOLTAGESAG = 0x0809;
        public const ushort ATTR_RMSVOLTAGESWELL = 0x080A;
        public const ushort ATTR_LINECURRENTPHASEB = 0x0901;
        public const ushort ATTR_ACTIVECURRENTPHASEB = 0x0902;
        public const ushort ATTR_REACTIVECURRENTPHASEB = 0x0903;
        public const ushort ATTR_RMSVOLTAGEPHASEB = 0x0905;
        public const ushort ATTR_RMSVOLTAGEMINPHASEB = 0x0906;
        public const ushort ATTR_RMSVOLTAGEMAXPHASEB = 0x0907;
        public const ushort ATTR_RMSCURRENTPHASEB = 0x0908;
        public const ushort ATTR_RMSCURRENTMINPHASEB = 0x0909;
        public const ushort ATTR_RMSCURRENTMAXPHASEB = 0x090A;
        public const ushort ATTR_ACTIVEPOWERPHASEB = 0x090B;
        public const ushort ATTR_ACTIVEPOWERMINPHASEB = 0x090C;
        public const ushort ATTR_ACTIVEPOWERMAXPHASEB = 0x090D;
        public const ushort ATTR_REACTIVEPOWERPHASEB = 0x090E;
        public const ushort ATTR_APPARENTPOWERPHASEB = 0x090F;
        public const ushort ATTR_POWERFACTORPHASEB = 0x0910;
        public const ushort ATTR_AVERAGERMSVOLTAGEMEASUREMENTPERIODPHASEB = 0x0911;
        public const ushort ATTR_AVERAGERMSOVERVOLTAGECOUNTERPHASEB = 0x0912;
        public const ushort ATTR_AVERAGERMSUNDERVOLTAGECOUNTERPHASEB = 0x0913;
        public const ushort ATTR_RMSEXTREMEOVERVOLTAGEPERIODPHASEB = 0x0914;
        public const ushort ATTR_RMSEXTREMEUNDERVOLTAGEPERIODPHASEB = 0x0915;
        public const ushort ATTR_RMSVOLTAGESAGPERIODPHASEB = 0x0916;
        public const ushort ATTR_RMSVOLTAGESWELLPERIODPHASEB = 0x0917;
        public const ushort ATTR_LINECURRENTPHASEC = 0x0A01;
        public const ushort ATTR_ACTIVECURRENTPHASEC = 0x0A02;
        public const ushort ATTR_REACTIVECURRENTPHASEC = 0x0A03;
        public const ushort ATTR_RMSVOLTAGEPHASEC = 0x0A05;
        public const ushort ATTR_RMSVOLTAGEMINPHASEC = 0x0A06;
        public const ushort ATTR_RMSVOLTAGEMAXPHASEC = 0x0A07;
        public const ushort ATTR_RMSCURRENTPHASEC = 0x0A08;
        public const ushort ATTR_RMSCURRENTMINPHASEC = 0x0A09;
        public const ushort ATTR_RMSCURRENTMAXPHASEC = 0x0A0A;
        public const ushort ATTR_ACTIVEPOWERPHASEC = 0x0A0B;
        public const ushort ATTR_ACTIVEPOWERMINPHASEC = 0x0A0C;
        public const ushort ATTR_ACTIVEPOWERMAXPHASEC = 0x0A0D;
        public const ushort ATTR_REACTIVEPOWERPHASEC = 0x0A0E;
        public const ushort ATTR_APPARENTPOWERPHASEC = 0x0A0F;
        public const ushort ATTR_POWERFACTORPHASEC = 0x0A10;
        public const ushort ATTR_AVERAGERMSVOLTAGEMEASUREMENTPERIODPHASEC = 0x0A11;
        public const ushort ATTR_AVERAGERMSOVERVOLTAGECOUNTERPHASEC = 0x0A12;
        public const ushort ATTR_AVERAGERMSUNDERVOLTAGECOUNTERPHASEC = 0x0A13;
        public const ushort ATTR_RMSEXTREMEOVERVOLTAGEPERIODPHASEC = 0x0A14;
        public const ushort ATTR_RMSEXTREMEUNDERVOLTAGEPERIODPHASEC = 0x0A15;
        public const ushort ATTR_RMSVOLTAGESAGPERIODPHASEC = 0x0A16;
        public const ushort ATTR_RMSVOLTAGESWELLPERIODPHASEC = 0x0A17;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(128);

            attributeMap.Add(ATTR_MEASUREMENTTYPE, new ZclAttribute(this, ATTR_MEASUREMENTTYPE, "Measurement Type", ZclDataType.Get(DataType.BITMAP_32_BIT), true, true, false, false));
            attributeMap.Add(ATTR_DCVOLTAGE, new ZclAttribute(this, ATTR_DCVOLTAGE, "DC Voltage", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCVOLTAGEMIN, new ZclAttribute(this, ATTR_DCVOLTAGEMIN, "DC Voltage Min", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCVOLTAGEMAX, new ZclAttribute(this, ATTR_DCVOLTAGEMAX, "DC Voltage Max", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCCURRENT, new ZclAttribute(this, ATTR_DCCURRENT, "DC Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCCURRENTMIN, new ZclAttribute(this, ATTR_DCCURRENTMIN, "DC Current Min", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCCURRENTMAX, new ZclAttribute(this, ATTR_DCCURRENTMAX, "DC Current Max", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCPOWER, new ZclAttribute(this, ATTR_DCPOWER, "DC Power", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCPOWERMIN, new ZclAttribute(this, ATTR_DCPOWERMIN, "DC Power Min", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCPOWERMAX, new ZclAttribute(this, ATTR_DCPOWERMAX, "DC Power Max", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCVOLTAGEMULTIPLIER, new ZclAttribute(this, ATTR_DCVOLTAGEMULTIPLIER, "DC Voltage Multiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCVOLTAGEDIVISOR, new ZclAttribute(this, ATTR_DCVOLTAGEDIVISOR, "DC Voltage Divisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCCURRENTMULTIPLIER, new ZclAttribute(this, ATTR_DCCURRENTMULTIPLIER, "DC Current Multiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCCURRENTDIVISOR, new ZclAttribute(this, ATTR_DCCURRENTDIVISOR, "DC Current Divisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCPOWERMULTIPLIER, new ZclAttribute(this, ATTR_DCPOWERMULTIPLIER, "DC Power Multiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DCPOWERDIVISOR, new ZclAttribute(this, ATTR_DCPOWERDIVISOR, "DC Power Divisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACFREQUENCY, new ZclAttribute(this, ATTR_ACFREQUENCY, "AC Frequency", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACFREQUENCYMIN, new ZclAttribute(this, ATTR_ACFREQUENCYMIN, "AC Frequency Min", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACFREQUENCYMAX, new ZclAttribute(this, ATTR_ACFREQUENCYMAX, "AC Frequency Max", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NEUTRALCURRENT, new ZclAttribute(this, ATTR_NEUTRALCURRENT, "Neutral Current", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOTALACTIVEPOWER, new ZclAttribute(this, ATTR_TOTALACTIVEPOWER, "Total Active Power", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TOTALREACTIVEPOWER, new ZclAttribute(this, ATTR_TOTALREACTIVEPOWER, "Total Reactive Power", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TOTALAPPARENTPOWER, new ZclAttribute(this, ATTR_TOTALAPPARENTPOWER, "Total Apparent Power", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_MEASURED1STHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASURED1STHARMONICCURRENT, "Measured 1st Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASURED3RDHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASURED3RDHARMONICCURRENT, "Measured 3rd Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASURED5THHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASURED5THHARMONICCURRENT, "Measured 5th Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASURED7THHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASURED7THHARMONICCURRENT, "Measured 7th Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASURED9THHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASURED9THHARMONICCURRENT, "Measured 9th Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASURED11THHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASURED11THHARMONICCURRENT, "Measured 11th Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASUREDPHASE1STHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASUREDPHASE1STHARMONICCURRENT, "Measured Phase 1st Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASUREDPHASE3RDHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASUREDPHASE3RDHARMONICCURRENT, "Measured Phase 3rd Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASUREDPHASE5THHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASUREDPHASE5THHARMONICCURRENT, "Measured Phase 5th Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASUREDPHASE7THHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASUREDPHASE7THHARMONICCURRENT, "Measured Phase 7th Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASUREDPHASE9THHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASUREDPHASE9THHARMONICCURRENT, "Measured Phase 9th Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MEASUREDPHASE11THHARMONICCURRENT, new ZclAttribute(this, ATTR_MEASUREDPHASE11THHARMONICCURRENT, "Measured Phase 11th Harmonic Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACFREQUENCYMULTIPLIER, new ZclAttribute(this, ATTR_ACFREQUENCYMULTIPLIER, "AC Frequency Multiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACFREQUENCYDIVISOR, new ZclAttribute(this, ATTR_ACFREQUENCYDIVISOR, "AC Frequency Divisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_POWERMULTIPLIER, new ZclAttribute(this, ATTR_POWERMULTIPLIER, "Power Multiplier", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_POWERDIVISOR, new ZclAttribute(this, ATTR_POWERDIVISOR, "Power Divisor", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_HARMONICCURRENTMULTIPLIER, new ZclAttribute(this, ATTR_HARMONICCURRENTMULTIPLIER, "Harmonic Current Multiplier", ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PHASEHARMONICCURRENTMULTIPLIER, new ZclAttribute(this, ATTR_PHASEHARMONICCURRENTMULTIPLIER, "Phase Harmonic Current Multiplier", ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_INSTANTANEOUSVOLTAGE, new ZclAttribute(this, ATTR_INSTANTANEOUSVOLTAGE, "Instantaneous Voltage", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_INSTANTANEOUSLINECURRENT, new ZclAttribute(this, ATTR_INSTANTANEOUSLINECURRENT, "Instantaneous Line Current", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_INSTANTANEOUSACTIVECURRENT, new ZclAttribute(this, ATTR_INSTANTANEOUSACTIVECURRENT, "Instantaneous Active Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_INSTANTANEOUSREACTIVECURRENT, new ZclAttribute(this, ATTR_INSTANTANEOUSREACTIVECURRENT, "Instantaneous Reactive Current", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_INSTANTANEOUSPOWER, new ZclAttribute(this, ATTR_INSTANTANEOUSPOWER, "Instantaneous Power", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGE, new ZclAttribute(this, ATTR_RMSVOLTAGE, "RMS Voltage", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGEMIN, new ZclAttribute(this, ATTR_RMSVOLTAGEMIN, "RMS Voltage Min", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGEMAX, new ZclAttribute(this, ATTR_RMSVOLTAGEMAX, "RMS Voltage Max", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSCURRENT, new ZclAttribute(this, ATTR_RMSCURRENT, "RMS Current", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RMSCURRENTMIN, new ZclAttribute(this, ATTR_RMSCURRENTMIN, "RMS Current Min", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSCURRENTMAX, new ZclAttribute(this, ATTR_RMSCURRENTMAX, "RMS Current Max", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACTIVEPOWER, new ZclAttribute(this, ATTR_ACTIVEPOWER, "Active Power", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACTIVEPOWERMIN, new ZclAttribute(this, ATTR_ACTIVEPOWERMIN, "Active Power Min", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACTIVEPOWERMAX, new ZclAttribute(this, ATTR_ACTIVEPOWERMAX, "Active Power Max", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_REACTIVEPOWER, new ZclAttribute(this, ATTR_REACTIVEPOWER, "Reactive Power", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APPARENTPOWER, new ZclAttribute(this, ATTR_APPARENTPOWER, "Apparent Power", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_POWERFACTOR, new ZclAttribute(this, ATTR_POWERFACTOR, "Power Factor", ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGERMSVOLTAGEMEASUREMENTPERIOD, new ZclAttribute(this, ATTR_AVERAGERMSVOLTAGEMEASUREMENTPERIOD, "Average RMS Voltage Measurement Period", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_AVERAGERMSUNDERVOLTAGECOUNTER, new ZclAttribute(this, ATTR_AVERAGERMSUNDERVOLTAGECOUNTER, "Average RMS Under Voltage Counter", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_RMSEXTREMEOVERVOLTAGEPERIOD, new ZclAttribute(this, ATTR_RMSEXTREMEOVERVOLTAGEPERIOD, "RMS Extreme Over Voltage Period", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_RMSEXTREMEUNDERVOLTAGEPERIOD, new ZclAttribute(this, ATTR_RMSEXTREMEUNDERVOLTAGEPERIOD, "RMS Extreme Under Voltage Period", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_RMSVOLTAGESAGPERIOD, new ZclAttribute(this, ATTR_RMSVOLTAGESAGPERIOD, "RMS Voltage Sag Period", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_RMSVOLTAGESWELLPERIOD, new ZclAttribute(this, ATTR_RMSVOLTAGESWELLPERIOD, "RMS Voltage Swell Period", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_ACVOLTAGEMULTIPLIER, new ZclAttribute(this, ATTR_ACVOLTAGEMULTIPLIER, "AC Voltage Multiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACVOLTAGEDIVISOR, new ZclAttribute(this, ATTR_ACVOLTAGEDIVISOR, "AC Voltage Divisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACCURRENTMULTIPLIER, new ZclAttribute(this, ATTR_ACCURRENTMULTIPLIER, "AC Current Multiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACCURRENTDIVISOR, new ZclAttribute(this, ATTR_ACCURRENTDIVISOR, "AC Current Divisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACPOWERMULTIPLIER, new ZclAttribute(this, ATTR_ACPOWERMULTIPLIER, "AC Power Multiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACPOWERDIVISOR, new ZclAttribute(this, ATTR_ACPOWERDIVISOR, "AC Power Divisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_OVERLOADALARMSMASK, new ZclAttribute(this, ATTR_OVERLOADALARMSMASK, "Overload Alarms Mask", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, true, true));
            attributeMap.Add(ATTR_VOLTAGEOVERLOAD, new ZclAttribute(this, ATTR_VOLTAGEOVERLOAD, "Voltage Overload", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTOVERLOAD, new ZclAttribute(this, ATTR_CURRENTOVERLOAD, "Current Overload", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACOVERLOADALARMSMASK, new ZclAttribute(this, ATTR_ACOVERLOADALARMSMASK, "AC Overload Alarms Mask", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_ACVOLTAGEOVERLOAD, new ZclAttribute(this, ATTR_ACVOLTAGEOVERLOAD, "AC Voltage Overload", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACCURRENTOVERLOAD, new ZclAttribute(this, ATTR_ACCURRENTOVERLOAD, "AC Current Overload", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACACTIVEPOWEROVERLOAD, new ZclAttribute(this, ATTR_ACACTIVEPOWEROVERLOAD, "AC Active Power Overload", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACREACTIVEPOWEROVERLOAD, new ZclAttribute(this, ATTR_ACREACTIVEPOWEROVERLOAD, "AC Reactive Power Overload", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGERMSOVERVOLTAGE, new ZclAttribute(this, ATTR_AVERAGERMSOVERVOLTAGE, "Average RMS Over Voltage", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGERMSUNDERVOLTAGE, new ZclAttribute(this, ATTR_AVERAGERMSUNDERVOLTAGE, "Average RMS Under Voltage", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSEXTREMEOVERVOLTAGE, new ZclAttribute(this, ATTR_RMSEXTREMEOVERVOLTAGE, "RMS Extreme Over Voltage", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSEXTREMEUNDERVOLTAGE, new ZclAttribute(this, ATTR_RMSEXTREMEUNDERVOLTAGE, "RMS Extreme Under Voltage", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGESAG, new ZclAttribute(this, ATTR_RMSVOLTAGESAG, "RMS Voltage Sag", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGESWELL, new ZclAttribute(this, ATTR_RMSVOLTAGESWELL, "RMS Voltage Swell", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_LINECURRENTPHASEB, new ZclAttribute(this, ATTR_LINECURRENTPHASEB, "Line Current Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACTIVECURRENTPHASEB, new ZclAttribute(this, ATTR_ACTIVECURRENTPHASEB, "Active Current Phase B", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_REACTIVECURRENTPHASEB, new ZclAttribute(this, ATTR_REACTIVECURRENTPHASEB, "Reactive Current Phase B", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGEPHASEB, new ZclAttribute(this, ATTR_RMSVOLTAGEPHASEB, "RMS Voltage Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGEMINPHASEB, new ZclAttribute(this, ATTR_RMSVOLTAGEMINPHASEB, "RMS Voltage Min Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGEMAXPHASEB, new ZclAttribute(this, ATTR_RMSVOLTAGEMAXPHASEB, "RMS Voltage Max Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSCURRENTPHASEB, new ZclAttribute(this, ATTR_RMSCURRENTPHASEB, "RMS Current Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSCURRENTMINPHASEB, new ZclAttribute(this, ATTR_RMSCURRENTMINPHASEB, "RMS Current Min Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSCURRENTMAXPHASEB, new ZclAttribute(this, ATTR_RMSCURRENTMAXPHASEB, "RMS Current Max Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACTIVEPOWERPHASEB, new ZclAttribute(this, ATTR_ACTIVEPOWERPHASEB, "Active Power Phase B", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACTIVEPOWERMINPHASEB, new ZclAttribute(this, ATTR_ACTIVEPOWERMINPHASEB, "Active Power Min Phase B", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACTIVEPOWERMAXPHASEB, new ZclAttribute(this, ATTR_ACTIVEPOWERMAXPHASEB, "Active Power Max Phase B", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_REACTIVEPOWERPHASEB, new ZclAttribute(this, ATTR_REACTIVEPOWERPHASEB, "Reactive Power Phase B", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APPARENTPOWERPHASEB, new ZclAttribute(this, ATTR_APPARENTPOWERPHASEB, "Apparent Power Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_POWERFACTORPHASEB, new ZclAttribute(this, ATTR_POWERFACTORPHASEB, "Power Factor Phase B", ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGERMSVOLTAGEMEASUREMENTPERIODPHASEB, new ZclAttribute(this, ATTR_AVERAGERMSVOLTAGEMEASUREMENTPERIODPHASEB, "Average RMS Voltage Measurement Period Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGERMSOVERVOLTAGECOUNTERPHASEB, new ZclAttribute(this, ATTR_AVERAGERMSOVERVOLTAGECOUNTERPHASEB, "Average RMS Over Voltage Counter Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGERMSUNDERVOLTAGECOUNTERPHASEB, new ZclAttribute(this, ATTR_AVERAGERMSUNDERVOLTAGECOUNTERPHASEB, "Average RMS Under Voltage Counter Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSEXTREMEOVERVOLTAGEPERIODPHASEB, new ZclAttribute(this, ATTR_RMSEXTREMEOVERVOLTAGEPERIODPHASEB, "RMS Extreme Over Voltage Period Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSEXTREMEUNDERVOLTAGEPERIODPHASEB, new ZclAttribute(this, ATTR_RMSEXTREMEUNDERVOLTAGEPERIODPHASEB, "RMS Extreme Under Voltage Period Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGESAGPERIODPHASEB, new ZclAttribute(this, ATTR_RMSVOLTAGESAGPERIODPHASEB, "RMS Voltage Sag Period Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGESWELLPERIODPHASEB, new ZclAttribute(this, ATTR_RMSVOLTAGESWELLPERIODPHASEB, "RMS Voltage Swell Period Phase B", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_LINECURRENTPHASEC, new ZclAttribute(this, ATTR_LINECURRENTPHASEC, "Line Current Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACTIVECURRENTPHASEC, new ZclAttribute(this, ATTR_ACTIVECURRENTPHASEC, "Active Current Phase C", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_REACTIVECURRENTPHASEC, new ZclAttribute(this, ATTR_REACTIVECURRENTPHASEC, "Reactive Current Phase C", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGEPHASEC, new ZclAttribute(this, ATTR_RMSVOLTAGEPHASEC, "RMS Voltage Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGEMINPHASEC, new ZclAttribute(this, ATTR_RMSVOLTAGEMINPHASEC, "RMS Voltage Min Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGEMAXPHASEC, new ZclAttribute(this, ATTR_RMSVOLTAGEMAXPHASEC, "RMS Voltage Max Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSCURRENTPHASEC, new ZclAttribute(this, ATTR_RMSCURRENTPHASEC, "RMS Current Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSCURRENTMINPHASEC, new ZclAttribute(this, ATTR_RMSCURRENTMINPHASEC, "RMS Current Min Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSCURRENTMAXPHASEC, new ZclAttribute(this, ATTR_RMSCURRENTMAXPHASEC, "RMS Current Max Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACTIVEPOWERPHASEC, new ZclAttribute(this, ATTR_ACTIVEPOWERPHASEC, "Active Power Phase C", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACTIVEPOWERMINPHASEC, new ZclAttribute(this, ATTR_ACTIVEPOWERMINPHASEC, "Active Power Min Phase C", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ACTIVEPOWERMAXPHASEC, new ZclAttribute(this, ATTR_ACTIVEPOWERMAXPHASEC, "Active Power Max Phase C", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_REACTIVEPOWERPHASEC, new ZclAttribute(this, ATTR_REACTIVEPOWERPHASEC, "Reactive Power Phase C", ZclDataType.Get(DataType.SIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_APPARENTPOWERPHASEC, new ZclAttribute(this, ATTR_APPARENTPOWERPHASEC, "Apparent Power Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_POWERFACTORPHASEC, new ZclAttribute(this, ATTR_POWERFACTORPHASEC, "Power Factor Phase C", ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGERMSVOLTAGEMEASUREMENTPERIODPHASEC, new ZclAttribute(this, ATTR_AVERAGERMSVOLTAGEMEASUREMENTPERIODPHASEC, "Average RMS Voltage Measurement Period Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGERMSOVERVOLTAGECOUNTERPHASEC, new ZclAttribute(this, ATTR_AVERAGERMSOVERVOLTAGECOUNTERPHASEC, "Average RMS Over Voltage Counter Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_AVERAGERMSUNDERVOLTAGECOUNTERPHASEC, new ZclAttribute(this, ATTR_AVERAGERMSUNDERVOLTAGECOUNTERPHASEC, "Average RMS Under Voltage Counter Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSEXTREMEOVERVOLTAGEPERIODPHASEC, new ZclAttribute(this, ATTR_RMSEXTREMEOVERVOLTAGEPERIODPHASEC, "RMS Extreme Over Voltage Period Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSEXTREMEUNDERVOLTAGEPERIODPHASEC, new ZclAttribute(this, ATTR_RMSEXTREMEUNDERVOLTAGEPERIODPHASEC, "RMS Extreme Under Voltage Period Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGESAGPERIODPHASEC, new ZclAttribute(this, ATTR_RMSVOLTAGESAGPERIODPHASEC, "RMS Voltage Sag Period Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_RMSVOLTAGESWELLPERIODPHASEC, new ZclAttribute(this, ATTR_RMSVOLTAGESWELLPERIODPHASEC, "RMS Voltage Swell Period Phase C", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(2);

            commandMap.Add(0x0000, () => new GetProfileInfoResponseCommand());
            commandMap.Add(0x0001, () => new GetMeasurementProfileResponseCommand());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(2);

            commandMap.Add(0x0000, () => new GetProfileInfoCommand());
            commandMap.Add(0x0001, () => new GetMeasurementProfileCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Electrical Measurement cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclElectricalMeasurementCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Get Profile Info Command
        ///
        /// Retrieves the power profiling information from the electrical measurement
        /// server.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetProfileInfoCommand()
        {
            return Send(new GetProfileInfoCommand());
        }

        /// <summary>
        /// The Get Measurement Profile Command
        ///
        /// Retrieves an electricity measurement profile from the electricity measurement
        /// server for a specific attribute ID requested.
        ///
        /// <param name="attributeId" <see cref="ushort"> Attribute ID</ param >
        /// <param name="startTime" <see cref="uint"> Start Time</ param >
        /// <param name="numberOfIntervals" <see cref="byte"> Number Of Intervals</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetMeasurementProfileCommand(ushort attributeId, uint startTime, byte numberOfIntervals)
        {
            GetMeasurementProfileCommand command = new GetMeasurementProfileCommand();

            // Set the fields
            command.AttributeId = attributeId;
            command.StartTime = startTime;
            command.NumberOfIntervals = numberOfIntervals;

            return Send(command);
        }

        /// <summary>
        /// The Get Profile Info Response Command
        ///
        /// Returns the power profiling information requested in the GetProfileInfo
        /// command. The power profiling information consists of a list of attributes which
        /// are profiled along with the period used to profile them.
        ///
        /// <param name="profileCount" <see cref="byte"> Profile Count</ param >
        /// <param name="profileIntervalPeriod" <see cref="byte"> Profile Interval Period</ param >
        /// <param name="maxNumberOfIntervals" <see cref="byte"> Max Number Of Intervals</ param >
        /// <param name="listOfAttributes" <see cref="ushort"> List Of Attributes</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetProfileInfoResponseCommand(byte profileCount, byte profileIntervalPeriod, byte maxNumberOfIntervals, ushort listOfAttributes)
        {
            GetProfileInfoResponseCommand command = new GetProfileInfoResponseCommand();

            // Set the fields
            command.ProfileCount = profileCount;
            command.ProfileIntervalPeriod = profileIntervalPeriod;
            command.MaxNumberOfIntervals = maxNumberOfIntervals;
            command.ListOfAttributes = listOfAttributes;

            return Send(command);
        }

        /// <summary>
        /// The Get Measurement Profile Response Command
        ///
        /// Returns the electricity measurement profile. The electricity measurement
        /// profile includes information regarding the amount of time used to capture data
        /// related to the flow of electricity as well as the intervals thes
        ///
        /// <param name="startTime" <see cref="uint"> Start Time</ param >
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="profileIntervalPeriod" <see cref="byte"> Profile Interval Period</ param >
        /// <param name="numberOfIntervalsDelivered" <see cref="byte"> Number Of Intervals Delivered</ param >
        /// <param name="attributeId" <see cref="ushort"> Attribute ID</ param >
        /// <param name="intervals" <see cref="byte"> Intervals</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetMeasurementProfileResponseCommand(uint startTime, byte status, byte profileIntervalPeriod, byte numberOfIntervalsDelivered, ushort attributeId, byte intervals)
        {
            GetMeasurementProfileResponseCommand command = new GetMeasurementProfileResponseCommand();

            // Set the fields
            command.StartTime = startTime;
            command.Status = status;
            command.ProfileIntervalPeriod = profileIntervalPeriod;
            command.NumberOfIntervalsDelivered = numberOfIntervalsDelivered;
            command.AttributeId = attributeId;
            command.Intervals = intervals;

            return Send(command);
        }
    }
}
