
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Metering;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Metering cluster implementation (Cluster ID 0x0702.
    ///
    /// The Metering Cluster provides a mechanism to retrieve usage information from
    /// Electric, Gas, Water, and potentially Thermal metering devices. These devices can
    /// operate on either battery or mains power, and can have a wide variety of sophistication.
    /// The Metering Cluster is designed to provide flexibility while limiting capabilities
    /// to a set number of metered information types. More advanced forms or data sets from
    /// metering devices will be supported in the Smart Energy Tunneling Cluster
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclMeteringCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0702;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Metering";

        // Attribute constants

        /// <summary>
        /// The FunctionalNotificationFlags attribute is implemented as a set of bit flags
        /// which are have a predefined action associated with a bit that is not based on a
        /// specific command, but may require the Mirrored device to trigger some additional
        /// functionality within the system.
        /// </summary>
        public const ushort ATTR_FUNCTIONALNOTIFICATIONFLAGS = 0x0000;

        /// <summary>
        /// NotificationFlags2 to NotificationFlags8 are 32-bit bitmaps that each
        /// represent a series of flags. Each flag represents an outstanding command that the
        /// Mirror is holding on behalf of the BOMD. Each flag represents a different command.
        /// The format of these attributes is dictated by the scheme that is currently in
        /// operation.
     /// </summary>
        public const ushort ATTR_NOTIFICATIONFLAGS2 = 0x0003;

        /// <summary>
        /// NotificationFlags2 to NotificationFlags8 are 32-bit bitmaps that each
        /// represent a series of flags. Each flag represents an outstanding command that the
        /// Mirror is holding on behalf of the BOMD. Each flag represents a different command.
        /// The format of these attributes is dictated by the scheme that is currently in
        /// operation.
     /// </summary>
        public const ushort ATTR_NOTIFICATIONFLAGS3 = 0x0004;

        /// <summary>
        /// NotificationFlags2 to NotificationFlags8 are 32-bit bitmaps that each
        /// represent a series of flags. Each flag represents an outstanding command that the
        /// Mirror is holding on behalf of the BOMD. Each flag represents a different command.
        /// The format of these attributes is dictated by the scheme that is currently in
        /// operation.
     /// </summary>
        public const ushort ATTR_NOTIFICATIONFLAGS4 = 0x0005;

        /// <summary>
        /// NotificationFlags2 to NotificationFlags8 are 32-bit bitmaps that each
        /// represent a series of flags. Each flag represents an outstanding command that the
        /// Mirror is holding on behalf of the BOMD. Each flag represents a different command.
        /// The format of these attributes is dictated by the scheme that is currently in
        /// operation.
     /// </summary>
        public const ushort ATTR_NOTIFICATIONFLAGS5 = 0x0006;

        /// <summary>
        /// NotificationFlags2 to NotificationFlags8 are 32-bit bitmaps that each
        /// represent a series of flags. Each flag represents an outstanding command that the
        /// Mirror is holding on behalf of the BOMD. Each flag represents a different command.
        /// The format of these attributes is dictated by the scheme that is currently in
        /// operation.
     /// </summary>
        public const ushort ATTR_NOTIFICATIONFLAGS6 = 0x0007;

        /// <summary>
        /// NotificationFlags2 to NotificationFlags8 are 32-bit bitmaps that each
        /// represent a series of flags. Each flag represents an outstanding command that the
        /// Mirror is holding on behalf of the BOMD. Each flag represents a different command.
        /// The format of these attributes is dictated by the scheme that is currently in
        /// operation.
     /// </summary>
        public const ushort ATTR_NOTIFICATIONFLAGS7 = 0x0008;

        /// <summary>
        /// NotificationFlags2 to NotificationFlags8 are 32-bit bitmaps that each
        /// represent a series of flags. Each flag represents an outstanding command that the
        /// Mirror is holding on behalf of the BOMD. Each flag represents a different command.
        /// The format of these attributes is dictated by the scheme that is currently in
        /// operation.
     /// </summary>
        public const ushort ATTR_NOTIFICATIONFLAGS8 = 0x0009;

        /// <summary>
        /// CurrentSummationDelivered represents the most recent summed value of Energy,
        /// Gas, or Water delivered and consumed in the premises. CurrentSummationDelivered
        /// is mandatory and must be provided as part of the minimum data set to be provided by the
        /// metering device. CurrentSummationDelivered is updated continuously as new
        /// measurements are made.
        /// </summary>
        public const ushort ATTR_CURRENTSUMMATIONDELIVERED = 0x0000;

        /// <summary>
        /// CurrentSummationReceived represents the most recent summed value of Energy,
        /// Gas, or Water generated and delivered from the premises. If optionally provided,
        /// CurrentSummationReceived is updated continuously as new measurements are made.
        /// </summary>
        public const ushort ATTR_CURRENTSUMMATIONRECEIVED = 0x0001;

        /// <summary>
        /// CurrentMaxDemandDelivered represents the maximum demand or rate of delivered
        /// value of Energy, Gas, or Water being utilized at the premises. If optionally
        /// provided, CurrentMaxDemandDelivered is updated continuously as new
        /// measurements are made.
        /// </summary>
        public const ushort ATTR_CURRENTMAXDEMANDDELIVERED = 0x0002;

        /// <summary>
        /// CurrentMaxDemandReceived represents the maximum demand or rate of received
        /// value of Energy, Gas, or Water being utilized by the utility. If optionally
        /// provided, CurrentMaxDemandReceived is updated continuously as new
        /// measurements are made.
        /// </summary>
        public const ushort ATTR_CURRENTMAXDEMANDRECEIVED = 0x0003;

        /// <summary>
        /// DFTSummation represents a snapshot of attribute CurrentSummationDelivered
        /// captured at the time indicated by attribute DailyFreezeTime. If optionally
        /// provided, DFTSummation is updated once every 24 hours.
        /// </summary>
        public const ushort ATTR_DFTSUMMATION = 0x0004;

        /// <summary>
        /// DailyFreezeTime represents the time of day when DFTSummation is captured.
        /// DailyFreezeTime is an unsigned 16-bit value representing the hour and minutes for
        /// DFT.
        /// Bits 0 to 7: Range of 0 to 0x3B representing the number of minutes past the top of the
        /// hour.
        /// Bits 8 to 15: Range of 0 to 0x17 representing the hour of the day (in 24-hour format).
        /// Note that midnight shall be represented as 00:00 only.
        /// </summary>
        public const ushort ATTR_DAILYFREEZETIME = 0x0005;

        /// <summary>
        /// PowerFactor contains the Average Power Factor ratio in 1/100ths. Valid values are
        /// 0 to 99.
        /// </summary>
        public const ushort ATTR_POWERFACTOR = 0x0006;

        /// <summary>
        /// The ReadingSnapshotTime attribute represents the last time all of the
        /// CurrentSummationDelivered, CurrentSummationReceived,
        /// CurrentMaxDemandDelivered, and CurrentMaxDemandReceived attributes that are
        /// supported by the device were updated.
        /// </summary>
        public const ushort ATTR_READINGSNAPSHOTTIME = 0x0007;

        /// <summary>
        /// The CurrentMaxDemandDeliveredTime attribute represents the time when
        /// CurrentMaxDemandDelivered reading was captured.
        /// </summary>
        public const ushort ATTR_CURRENTMAXDEMANDDELIVEREDTIME = 0x0008;

        /// <summary>
        /// The CurrentMaxDemandReceivedTime attribute represents the time when
        /// CurrentMaxDemandReceived reading was captured.
        /// </summary>
        public const ushort ATTR_CURRENTMAXDEMANDRECEIVEDTIME = 0x0009;

        /// <summary>
        /// The DefaultUpdatePeriod attribute represents the interval (seconds) at which
        /// the InstantaneousDemand attribute is updated when not in fast poll mode.
        /// InstantaneousDemand may be continuously updated as new measurements are
        /// acquired, but at a minimum InstantaneousDemand must be updated at the
        /// DefaultUpdatePeriod. The DefaultUpdatePeriod may apply to other attributes as
        /// defined by the device manufacturer.
        /// </summary>
        public const ushort ATTR_DEFAULTUPDATEPERIOD = 0x000A;

        /// <summary>
        /// The FastPollUpdatePeriod attribute represents the interval (seconds) at which
        /// the InstantaneousDemandattribute is updated when in fast poll mode.
        /// InstantaneousDemand may be continuously updated as new measurements are
        /// acquired, but at a minimum, InstantaneousDemand must be updated at the
        /// FastPollUpdatePeriod. The FastPollUpdatePeriod may apply to other attributes
        /// as defined by the device manufacturer.
        /// </summary>
        public const ushort ATTR_FASTPOLLUPDATEPERIOD = 0x000B;

        /// <summary>
        /// The CurrentBlockPeriodConsumptionDelivered attribute represents the most
        /// recent summed value of Energy, Gas or Water delivered and consumed in the premises
        /// during the Block Tariff Period.
        /// The CurrentBlockPeriodConsumptionDelivered is reset at the start of each Block
        /// Tariff Period.
        /// </summary>
        public const ushort ATTR_CURRENTBLOCKPERIODCONSUMPTIONDELIVERED = 0x000C;

        /// <summary>
        /// The DailyConsumptionTarget attribute is a daily target consumption amount that
        /// can be displayed to the consumer on a HAN device, with the intent that it can be used to
        /// compare to actual daily consumption (e.g. compare to the
        /// CurrentDayConsumptionDelivered).
        /// </summary>
        public const ushort ATTR_DAILYCONSUMPTIONTARGET = 0x000D;

        /// <summary>
        /// When Block Tariffs are enabled, CurrentBlock is an 8-bit Enumeration which
        /// indicates the currently active block. If blocks are active then the current active
        /// block is based on the CurrentBlockPeriodConsumptionDelivered and the block
        /// thresholds. Block 1 is active when the value of
        /// CurrentBlockPeriodConsumptionDelivered is less than or equal to the
        /// Block1Threshold value, Block 2 is active when
        /// CurrentBlockPeriodConsumptionDelivered is greater than Block1Threshold
        /// value and less than or equal to the8 Block2Threshold value, and so on. Block 16 is
        /// active when the value of CurrentBlockPeriodConsumptionDelivered is greater
        /// than Block15Threshold value.
        /// </summary>
        public const ushort ATTR_CURRENTBLOCK = 0x000E;

        /// <summary>
        /// The ProfileIntervalPeriod attribute is currently included in the Get Profile
        /// Response command payload, but does not appear in an attribute set. This represents
        /// the duration of each interval. ProfileIntervalPeriod represents the interval or
        /// time frame used to capture metered Energy, Gas, and Water consumption for
        /// profiling purposes.
        /// </summary>
        public const ushort ATTR_PROFILEINTERVALPERIOD = 0x000F;
        public const ushort ATTR_INTERVALREADREPORTINGPERIOD = 0x0010;

        /// <summary>
        /// The PresetReadingTime attribute represents the time of day (in quarter hour
        /// increments) at which the meter will wake up and report a register reading even if
        /// there has been no consumption for the previous 24 hours. PresetReadingTime is an
        /// unsigned 16-bit value representing the hour and minutes.
        /// </summary>
        public const ushort ATTR_PRESETREADINGTIME = 0x0011;

        /// <summary>
        /// The VolumePerReport attribute represents the volume per report increment from
        /// the water or gas meter. For example a gas meter might be set to report its register
        /// reading for every time 1 cubic meter of gas is used. For a water meter it might report
        /// the register value every 10 liters of water usage.
        /// </summary>
        public const ushort ATTR_VOLUMEPERREPORT = 0x0012;

        /// <summary>
        /// The FlowRestriction attribute represents the volume per minute limit set in the
        /// flow restrictor. This applies to water but not for gas. A setting of 0xFF indicates
        /// this feature is disabled.
        /// </summary>
        public const ushort ATTR_FLOWRESTRICTION = 0x0013;

        /// <summary>
        /// The SupplyStatus attribute represents the state of the supply at the customer's
        /// premises.
        /// </summary>
        public const ushort ATTR_SUPPLYSTATUS = 0x0014;

        /// <summary>
        /// CurrentInletEnergyCarrierSummation is the current integrated volume of a given
        /// energy carrier measured on the inlet. The formatting and unit of measure for this
        /// value is specified in the EnergyCarrierUnitOfMeasure and
        /// EnergyCarrierSummationFormatting attributes
        /// </summary>
        public const ushort ATTR_CURRENTINLETENERGYCARRIERSUMMATION = 0x0015;

        /// <summary>
        /// CurrentOutletEnergyCarrierSummation is the current integrated volume of a
        /// given energy carrier measured on the outlet. The formatting and unit of measure for
        /// this value is specified in the EnergyCarrierUnitOfMeasure and
        /// EnergyCarrierSummationFormatting attributes.
        /// </summary>
        public const ushort ATTR_CURRENTOUTLETENERGYCARRIERSUMMATION = 0x0016;

        /// <summary>
        /// InletTemperature is the temperature measured on the energy carrier inlet.
        /// </summary>
        public const ushort ATTR_INLETTEMPERATURE = 0x0017;

        /// <summary>
        /// OutletTemperature is the temperature measured on the energy carrier outlet.
        /// </summary>
        public const ushort ATTR_OUTLETTEMPERATURE = 0x0018;

        /// <summary>
        /// ControlTemperature is a reference temperature measured on the meter used to
        /// validate the Inlet/Outlet temperatures.
        /// </summary>
        public const ushort ATTR_CONTROLTEMPERATURE = 0x0019;

        /// <summary>
        /// CurrentInletEnergyCarrierDemand is the current absolute demand on the energy
        /// carrier inlet.
        /// </summary>
        public const ushort ATTR_CURRENTINLETENERGYCARRIERDEMAND = 0x001A;

        /// <summary>
        /// CurrentOutletEnergyCarrierDemand is the current absolute demand on the energy
        /// carrier outlet.
        /// </summary>
        public const ushort ATTR_CURRENTOUTLETENERGYCARRIERDEMAND = 0x001B;

        /// <summary>
        /// The PreviousBlockPeriodConsumptionDelivered attribute represents the total
        /// value of Energy, Gas or Water delivered and consumed in the premises at the end of the
        /// previous Block Tariff Period. If supported, the
        /// PreviousBlockPeriodConsumptionDelivered attribute is updated at the end of
        /// each Block Tariff Period.
        /// </summary>
        public const ushort ATTR_PREVIOUSBLOCKPERIODCONSUMPTIONDELIVERED = 0x001C;

        /// <summary>
        /// The CurrentBlockPeriodConsumptionReceived attribute represents the most
        /// recent summed value of Energy, Gas or Water received by the energy supplier from the
        /// premises during the Block Tariff Period. The
        /// CurrentBlockPeriodConsumptionReceived attribute is reset at the start of each
        /// Block Tariff Period.
        /// </summary>
        public const ushort ATTR_CURRENTBLOCKPERIODCONSUMPTIONRECEIVED = 0x001D;

        /// <summary>
        /// When Block Tariffs are enabled, CurrentBlockReceived is an 8-bit Enumeration
        /// which indicates the currently active block. If blocks are active then the current
        /// active block is based on the CurrentBlockPeriodConsumptionReceived and the
        /// block thresholds. Block 1 is active when the value of
        /// CurrentBlockPeriodConsumptionReceived is less than or equal to the
        /// Block1Threshold value, Block 2 is active when
        /// CurrentBlockPeriodConsumptionReceived is greater than Block1Threshold value
        /// and less than or equal to the Block2Threshold value, and so on. Block 16 is active
        /// when the value of CurrentBlockPeriodConsumptionReceived is greater than
        /// Block15Threshold value.
        /// </summary>
        public const ushort ATTR_CURRENTBLOCKRECEIVED = 0x001E;

        /// <summary>
        /// DFTSummationReceived represents a snapshot of attribute
        /// CurrentSummationReceived captured at the time indicated by the DailyFreezeTime
        /// attribute.
        /// </summary>
        public const ushort ATTR_DFTSUMMATIONRECEIVED = 0x001F;

        /// <summary>
        /// The ActiveRegisterTierDelivered attribute indicates the current register tier
        /// that the energy consumed is being accumulated against.
        /// </summary>
        public const ushort ATTR_ACTIVEREGISTERTIERDELIVERED = 0x0020;

        /// <summary>
        /// The ActiveRegisterTierReceived attribute indicates the current register tier
        /// that the energy generated is being accumulated against.
        /// </summary>
        public const ushort ATTR_ACTIVEREGISTERTIERRECEIVED = 0x0021;

        /// <summary>
        /// This attribute allows other devices to determine the time at which a meter switches
        /// from one block to another.
        /// When Block Tariffs are enabled, the LastBlockSwitchTime attribute represents
        /// the timestamp of the last update to the CurrentBlock attribute, as a result of the
        /// consumption exceeding a threshold, or the start of a new block period and/or
        /// billing period.
        /// If, at the start of a new block period and/or billing period, the value of the
        /// CurrentBlock attribute is still set to Block1 (0x01), the CurrentBlock attribute
        /// value will not change but the LastBlockSwitchTime attribute shall be updated to
        /// indicate this change.
        /// </summary>
        public const ushort ATTR_LASTBLOCKSWITCHTIME = 0x0022;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER1SUMMATIONDELIVERED = 0x0101;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER3SUMMATIONDELIVERED = 0x0103;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER5SUMMATIONDELIVERED = 0x0105;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER7SUMMATIONDELIVERED = 0x0107;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER9SUMMATIONDELIVERED = 0x0109;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER11SUMMATIONDELIVERED = 0x010B;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER13SUMMATIONDELIVERED = 0x010D;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER15SUMMATIONDELIVERED = 0x010F;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER17SUMMATIONDELIVERED = 0x0111;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER19SUMMATIONDELIVERED = 0x0113;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER21SUMMATIONDELIVERED = 0x0115;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER23SUMMATIONDELIVERED = 0x0117;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER25SUMMATIONDELIVERED = 0x0119;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER27SUMMATIONDELIVERED = 0x011B;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER29SUMMATIONDELIVERED = 0x011D;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER31SUMMATIONDELIVERED = 0x011F;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER33SUMMATIONDELIVERED = 0x0121;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER35SUMMATIONDELIVERED = 0x0123;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER37SUMMATIONDELIVERED = 0x0125;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER39SUMMATIONDELIVERED = 0x0127;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER41SUMMATIONDELIVERED = 0x0129;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER43SUMMATIONDELIVERED = 0x012B;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER45SUMMATIONDELIVERED = 0x012D;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER47SUMMATIONDELIVERED = 0x012F;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER49SUMMATIONDELIVERED = 0x0131;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER51SUMMATIONDELIVERED = 0x0133;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER53SUMMATIONDELIVERED = 0x0135;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER55SUMMATIONDELIVERED = 0x0137;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER57SUMMATIONDELIVERED = 0x0139;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER59SUMMATIONDELIVERED = 0x013B;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER61SUMMATIONDELIVERED = 0x013D;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER63SUMMATIONDELIVERED = 0x013F;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER65SUMMATIONDELIVERED = 0x0141;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER67SUMMATIONDELIVERED = 0x0143;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER69SUMMATIONDELIVERED = 0x0145;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER71SUMMATIONDELIVERED = 0x0147;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER73SUMMATIONDELIVERED = 0x0149;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER75SUMMATIONDELIVERED = 0x014B;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER77SUMMATIONDELIVERED = 0x014D;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER79SUMMATIONDELIVERED = 0x014F;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER81SUMMATIONDELIVERED = 0x0151;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER83SUMMATIONDELIVERED = 0x0153;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER85SUMMATIONDELIVERED = 0x0155;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER87SUMMATIONDELIVERED = 0x0157;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER89SUMMATIONDELIVERED = 0x0159;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER91SUMMATIONDELIVERED = 0x015B;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER93SUMMATIONDELIVERED = 0x015D;

        /// <summary>
        /// Attributes CurrentTier1SummationDelivered through
        /// CurrentTierNSummationDelivered represent the most recent summed value of
        /// Energy, Gas, or Water delivered to the premises (i.e. delivered to the customer
        /// from the utility) at a specific price tier as defined by a TOU schedule or a real time
        /// pricing period. If optionally provided, attributes
        /// CurrentTier1SummationDelivered through CurrentTierNSummationDelivered are
        /// updated continuously as new measurements are made.
     /// </summary>
        public const ushort ATTR_CURRENTTIER95SUMMATIONDELIVERED = 0x015F;
        public const ushort ATTR_CURRENTTIER1SUMMATIONRECEIVED = 0x0102;
        public const ushort ATTR_CURRENTTIER3SUMMATIONRECEIVED = 0x0104;
        public const ushort ATTR_CURRENTTIER5SUMMATIONRECEIVED = 0x0106;
        public const ushort ATTR_CURRENTTIER7SUMMATIONRECEIVED = 0x0108;
        public const ushort ATTR_CURRENTTIER9SUMMATIONRECEIVED = 0x010A;
        public const ushort ATTR_CURRENTTIER11SUMMATIONRECEIVED = 0x010C;
        public const ushort ATTR_CURRENTTIER13SUMMATIONRECEIVED = 0x010E;
        public const ushort ATTR_CURRENTTIER15SUMMATIONRECEIVED = 0x0110;
        public const ushort ATTR_CURRENTTIER17SUMMATIONRECEIVED = 0x0112;
        public const ushort ATTR_CURRENTTIER19SUMMATIONRECEIVED = 0x0114;
        public const ushort ATTR_CURRENTTIER21SUMMATIONRECEIVED = 0x0116;
        public const ushort ATTR_CURRENTTIER23SUMMATIONRECEIVED = 0x0118;
        public const ushort ATTR_CURRENTTIER25SUMMATIONRECEIVED = 0x011A;
        public const ushort ATTR_CURRENTTIER27SUMMATIONRECEIVED = 0x011C;
        public const ushort ATTR_CURRENTTIER29SUMMATIONRECEIVED = 0x011E;
        public const ushort ATTR_CURRENTTIER31SUMMATIONRECEIVED = 0x0120;
        public const ushort ATTR_CURRENTTIER33SUMMATIONRECEIVED = 0x0122;
        public const ushort ATTR_CURRENTTIER35SUMMATIONRECEIVED = 0x0124;
        public const ushort ATTR_CURRENTTIER37SUMMATIONRECEIVED = 0x0126;
        public const ushort ATTR_CURRENTTIER39SUMMATIONRECEIVED = 0x0128;
        public const ushort ATTR_CURRENTTIER41SUMMATIONRECEIVED = 0x012A;
        public const ushort ATTR_CURRENTTIER43SUMMATIONRECEIVED = 0x012C;
        public const ushort ATTR_CURRENTTIER45SUMMATIONRECEIVED = 0x012E;
        public const ushort ATTR_CURRENTTIER47SUMMATIONRECEIVED = 0x0130;
        public const ushort ATTR_CURRENTTIER49SUMMATIONRECEIVED = 0x0132;
        public const ushort ATTR_CURRENTTIER51SUMMATIONRECEIVED = 0x0134;
        public const ushort ATTR_CURRENTTIER53SUMMATIONRECEIVED = 0x0136;
        public const ushort ATTR_CURRENTTIER55SUMMATIONRECEIVED = 0x0138;
        public const ushort ATTR_CURRENTTIER57SUMMATIONRECEIVED = 0x013A;
        public const ushort ATTR_CURRENTTIER59SUMMATIONRECEIVED = 0x013C;
        public const ushort ATTR_CURRENTTIER61SUMMATIONRECEIVED = 0x013E;
        public const ushort ATTR_CURRENTTIER63SUMMATIONRECEIVED = 0x0140;
        public const ushort ATTR_CURRENTTIER65SUMMATIONRECEIVED = 0x0142;
        public const ushort ATTR_CURRENTTIER67SUMMATIONRECEIVED = 0x0144;
        public const ushort ATTR_CURRENTTIER69SUMMATIONRECEIVED = 0x0146;
        public const ushort ATTR_CURRENTTIER71SUMMATIONRECEIVED = 0x0148;
        public const ushort ATTR_CURRENTTIER73SUMMATIONRECEIVED = 0x014A;
        public const ushort ATTR_CURRENTTIER75SUMMATIONRECEIVED = 0x014C;
        public const ushort ATTR_CURRENTTIER77SUMMATIONRECEIVED = 0x014E;
        public const ushort ATTR_CURRENTTIER79SUMMATIONRECEIVED = 0x0150;
        public const ushort ATTR_CURRENTTIER81SUMMATIONRECEIVED = 0x0152;
        public const ushort ATTR_CURRENTTIER83SUMMATIONRECEIVED = 0x0154;
        public const ushort ATTR_CURRENTTIER85SUMMATIONRECEIVED = 0x0156;
        public const ushort ATTR_CURRENTTIER87SUMMATIONRECEIVED = 0x0158;
        public const ushort ATTR_CURRENTTIER89SUMMATIONRECEIVED = 0x015A;
        public const ushort ATTR_CURRENTTIER91SUMMATIONRECEIVED = 0x015C;
        public const ushort ATTR_CURRENTTIER93SUMMATIONRECEIVED = 0x015E;
        public const ushort ATTR_CURRENTTIER95SUMMATIONRECEIVED = 0x0160;

        /// <summary>
        /// CPP1SummationDelivered represents the most recent summed value of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) while Critical Peak Price ‘CPP1’ was being applied. If optionally
        /// provided, attribute CPP1SummationDelivered is updated continuously as new
        /// measurements are made.
        /// </summary>
        public const ushort ATTR_CPP1SUMMATIONDELIVERED = 0x01FC;

        /// <summary>
        /// CPP2SummationDelivered represents the most recent summed value of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) while Critical Peak Price ‘CPP2’ was being applied. If optionally
        /// provided, attribute CPP2SummationDelivered is updated continuously as new
        /// measurements are made.
        /// </summary>
        public const ushort ATTR_CPP2SUMMATIONDELIVERED = 0x01FE;

        /// <summary>
        /// The Status attribute provides indicators reflecting the current error
        /// conditions found by the metering device. This attribute is an 8-bit field where
        /// when an individual bit is set, an error or warning condition exists. The behavior
        /// causing the setting or resetting each bit is device specific. In other words, the
        /// application within the metering device will determine and control when these
        /// settings are either set or cleared.
        /// </summary>
        public const ushort ATTR_STATUS = 0x0200;

        /// <summary>
        /// RemainingBatteryLife represents the estimated remaining life of the battery in %
        /// of capacity. A setting of 0xFF indicates this feature is disabled. The range 0 - 100
        /// where 100 = 100%, 0xFF = Unknown.
        /// </summary>
        public const ushort ATTR_REMAININGBATTERYLIFE = 0x0201;

        /// <summary>
        /// HoursInOperation is a counter that increments once every hour during operation.
        /// This may be used as a check for tampering.
        /// </summary>
        public const ushort ATTR_HOURSINOPERATION = 0x0202;

        /// <summary>
        /// HoursInFault is a counter that increments once every hour when the device is in
        /// operation with a fault detected. This may be used as a check for tampering.
        /// </summary>
        public const ushort ATTR_HOURSINFAULT = 0x0203;

        /// <summary>
        /// The ExtendedStatus attribute reflects the state of items in a meter that the
        /// standard Status attribute cannot show. The Extended Status BitMap is split into
        /// two groups of flags: general flags and metering type specific flags. Flags are
        /// currently defined for electricity and gas meters; flag definitions for other
        /// commodities will be added as and when their usage is agreed.
        /// These flags are set and reset by the meter autonomously; they cannot be reset by
        /// other devices.
        /// </summary>
        public const ushort ATTR_EXTENDEDSTATUS = 0x0204;

        /// <summary>
        /// RemainingBatteryLifeInDays attribute represents the estimated remaining life
        /// of the battery in days of capacity. The range is 0 – 0xFFFE, where 0xFFFF represents
        /// 'Invalid', 'Unused' and 'Disabled'.
        /// </summary>
        public const ushort ATTR_REMAININGBATTERYLIFEINDAYS = 0x0205;

        /// <summary>
        /// CurrentMeterID attribute is the current ID for the Meter. This could be the current
        /// firmware version supported on the meter.
        /// </summary>
        public const ushort ATTR_CURRENTMETERID = 0x0206;

        /// <summary>
        /// The AmbientConsumptionIndicator attribute is an 8-bit enumeration which
        /// provides a simple (i.e. Low/Medium/High) indication of the amount of a commodity
        /// being consumed within the premises. The status is achieved by comparing the
        /// current value of the InstantaneousDemand attribute with low/medium and
        /// medium/high thresholds.
        /// </summary>
        public const ushort ATTR_AMBIENTCONSUMPTIONINDICATOR = 0x0207;

        /// <summary>
        /// UnitofMeasure provides a label for the Energy, Gas, or Water being measured by the
        /// metering device. The unit of measure applies to all summations, consumptions/
        /// profile interval and demand/rate supported by this cluster other than those
        /// specifically identified as being based upon the EnergyCarrierUnitOfMeasure or
        /// the AlternativeUnitofMeasure. Other measurements such as the power factor are
        /// self describing.
        /// </summary>
        public const ushort ATTR_UNITOFMEASURE = 0x0300;

        /// <summary>
        /// Multiplier provides a value to be multiplied against a raw or uncompensated sensor
        /// count of Energy, Gas, or Water being measured by the metering device. If present,
        /// this attribute must be applied against all summation, consumption and demand
        /// values to derive the delivered and received values expressed in the unit of measure
        /// specified. This attribute must be used in conjunction with the Divisor attribute.
        /// </summary>
        public const ushort ATTR_MULTIPLIER = 0x0301;

        /// <summary>
        /// Divisor provides a value to divide the results of applying the Multiplier
        /// Attribute against a raw or uncompensated sensor count of Energy, Gas, or Water
        /// being measured by the metering device. If present, this attribute must be applied
        /// against all summation, consumption and demand values to derive the delivered and
        /// received values expressed in the unit of measure specified. This attribute must be
        /// used in conjunction with the Multiplier attribute.
        /// </summary>
        public const ushort ATTR_DIVISOR = 0x0302;

        /// <summary>
        /// SummationFormatting provides a method to properly decipher the number of digits
        /// and the decimal location of the values found in the Summation Information Set of
        /// attributes.
        /// </summary>
        public const ushort ATTR_SUMMATIONFORMATTING = 0x0303;

        /// <summary>
        /// DemandFormatting provides a method to properly decipher the number of digits and
        /// the decimal location of the values found in the Demand-related attributes.
        /// </summary>
        public const ushort ATTR_DEMANDFORMATTING = 0x0304;

        /// <summary>
        /// HistoricalConsumptionFormatting provides a method to properly decipher the
        /// number of digits and the decimal location of the values found in the Historical
        /// Consumption Set of attributes.
        /// </summary>
        public const ushort ATTR_HISTORICALCONSUMPTIONFORMATTING = 0x0305;

        /// <summary>
        /// MeteringDeviceType provides a label for identifying the type of metering device
        /// present. The attribute are values representing Energy, Gas, Water, Thermal,
        /// Heat, Cooling, and mirrored metering devices.
        /// </summary>
        public const ushort ATTR_METERINGDEVICETYPE = 0x0306;

        /// <summary>
        /// The SiteID is a ZCL Octet String field capable of storing a 32 character string (the
        /// first Octet indicates length) encoded in UTF-8 format. The SiteID is a text string,
        /// known in the UK as the MPAN number for electricity, MPRN for gas and 'Stand Point' in
        /// South Africa. These numbers specify the meter point location in a standardized
        /// way. The field is defined to accommodate the number of characters typically found
        /// in the UK and Europe (16 digits). Generally speaking the field is numeric but is
        /// defined for the possibility of an alpha-numeric format by specifying an octet
        /// string.
        /// </summary>
        public const ushort ATTR_SITEID = 0x0307;

        /// <summary>
        /// The MeterSerialNumber is a ZCL Octet String field capable of storing a 24 character
        /// string (the first Octet indicates length) encoded in UTF-8 format. It is used to
        /// provide a unique identification of the metering device.
        /// </summary>
        public const ushort ATTR_METERSERIALNUMBER = 0x0308;

        /// <summary>
        /// The EnergyCarrierUnitOfMeasure specifies the unit of measure that the
        /// EnergyCarrier is measured in. This unit of measure is typically a unit of volume or
        /// flow and cannot be an amount of energy.
        /// </summary>
        public const ushort ATTR_ENERGYCARRIERUNITOFMEASURE = 0x0309;

        /// <summary>
        /// EnergyCarrierSummationFormatting provides a method to properly decipher the
        /// number of digits and the decimal location of the values found in the Summation-
        /// related attributes.
        /// </summary>
        public const ushort ATTR_ENERGYCARRIERSUMMATIONFORMATTING = 0x030A;

        /// <summary>
        /// EnergyCarrierDemandFormatting provides a method to properly decipher the
        /// number of digits and the decimal location of the values found in the Demand-related
        /// attributes.
        /// </summary>
        public const ushort ATTR_ENERGYCARRIERDEMANDFORMATTING = 0x030B;

        /// <summary>
        /// TemperatureFormatting provides a method to properly decipher the number of
        /// digits and the decimal location of the values found in the Temperature-related
        /// attributes.
        /// </summary>
        public const ushort ATTR_TEMPERATUREUNITOFMEASURE = 0x030C;

        /// <summary>
        /// The TemperatureUnitOfMeasure specifies the unit of measure that temperatures
        /// are measured in.
        /// </summary>
        public const ushort ATTR_TEMPERATUREFORMATTING = 0x030D;

        /// <summary>
        /// The ModuleSerialNumber attribute represents the serial number (unique
        /// identifier) of the meter module. It is a ZCL Octet String field capable of storing a
        /// 24 character string (the first Octet indicates length) encoded in UTF-8 format. It
        /// shall be used to uniquely identify the meter communications module.
        /// </summary>
        public const ushort ATTR_MODULESERIALNUMBER = 0x030E;

        /// <summary>
        /// The OperatingTariffLabelDelivered attribute is the meter’s version of the
        /// TariffLabel attribute that is found within the Tariff Information attribute set
        /// of the Price Cluster. It is used to identify the current consumption tariff
        /// operating on the meter. The attribute is a ZCL Octet String field capable of storing
        /// a 24 character string (the first Octet indicates length) encoded in UTF-8 format.
        /// </summary>
        public const ushort ATTR_OPERATINGTARIFFLABELDELIVERED = 0x030F;

        /// <summary>
        /// The OperatingTariffLabelReceived attribute is the meter’s version of the
        /// ReceivedTariffLabel attribute that is found within the Tariff Information
        /// attribute set of the Price Cluster. It is used to identify the current generation
        /// tariff operating on the meter. The attribute is a ZCL Octet String field capable of
        /// storing a 24 character string (the first Octet indicates length) encoded in UTF-8
        /// format.
        /// </summary>
        public const ushort ATTR_OPERATINGTARIFFLABELRECEIVED = 0x0310;

        /// <summary>
        /// The CustomerIDNumber attribute provides a customer identification which may be
        /// used to confirm the customer at the premises. The attribute is a ZCL Octet String
        /// field capable of storing a 24 character string (not including the first Octet which
        /// indicates length) encoded in UTF-8 format.
        /// </summary>
        public const ushort ATTR_CUSTOMERIDNUMBER = 0x0311;

        /// <summary>
        /// Unless stated otherwise, the AlternativeUnitofMeasure attribute provides a
        /// base for the attributes in the Alternative Historical Consumption attribute set.
        /// </summary>
        public const ushort ATTR_ALTERNATIVEUNITOFMEASURE = 0x0312;

        /// <summary>
        /// AlternativeDemandFormatting provides a method to properly decipher the number
        /// of digits and the decimal location of the values found in the Alternative
        /// Demand-related attributes.
        /// </summary>
        public const ushort ATTR_ALTERNATIVEDEMANDFORMATTING = 0x0313;
        public const ushort ATTR_ALTERNATIVECONSUMPTIONFORMATTING = 0x0314;

        /// <summary>
        /// InstantaneousDemand represents the current Demand of Energy, Gas, or Water
        /// delivered or received at the premises. Positive values indicate demand delivered
        /// to the premises where negative values indicate demand received from the premises.
        /// InstantaneousDemand is updated continuously as new measurements are made. The
        /// frequency of updates to this field is specific to the metering device, but should be
        /// within the range of once every second to once every 5 seconds.
        /// </summary>
        public const ushort ATTR_INSTANTANEOUSDEMAND = 0x0400;

        /// <summary>
        /// CurrentDayConsumptionDelivered represents the summed value of Energy, Gas, or
        /// Water delivered to the premises since the Historical Freeze Time (HFT). If
        /// optionally provided, CurrentDayConsumptionDelivered is updated continuously
        /// as new measurements are made. If the optional HFT attribute is not available,
        /// default to midnight local time.
        /// </summary>
        public const ushort ATTR_CURRENTDAYCONSUMPTIONDELIVERED = 0x0401;

        /// <summary>
        /// CurrentDayConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises since the Historical Freeze Time (HFT). If
        /// optionally provided, CurrentDayConsumptionReceived is updated continuously
        /// as new measurements are made. If the optional HFT attribute is not available,
        /// default to midnight local time.
        /// </summary>
        public const ushort ATTR_CURRENTDAYCONSUMPTIONRECEIVED = 0x0402;

        /// <summary>
        /// PreviousDayConsumptionDelivered represents the summed value of Energy, Gas, or
        /// Water delivered to the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If optionally provided,
        /// PreviousDayConsumptionDelivered is updated every HFT. If the optional HFT
        /// attribute is not available, default to midnight local time.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYCONSUMPTIONDELIVERED = 0x0403;

        /// <summary>
        /// PreviousDayConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If optionally provided,
        /// PreviousDayConsumptionReceived is updated every HFT. If the optional HFT
        /// attribute is not available, default to midnight local time.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYCONSUMPTIONRECEIVED = 0x0404;

        /// <summary>
        /// CurrentPartialProfileIntervalStartTimeDelivered represents the start time
        /// of the current Load Profile interval being accumulated for commodity delivered.
        /// </summary>
        public const ushort ATTR_CURRENTPARTIALPROFILEINTERVALSTARTTIMEDELIVERED = 0x0405;

        /// <summary>
        /// CurrentPartialProfileIntervalStartTimeReceived represents the start time of
        /// the current Load Profile interval being accumulated for commodity received.
        /// </summary>
        public const ushort ATTR_CURRENTPARTIALPROFILEINTERVALSTARTTIMERECEIVED = 0x0406;

        /// <summary>
        /// CurrentPartialProfileIntervalValueDelivered represents the value of the
        /// current Load Profile interval being accumulated for commodity delivered.
        /// </summary>
        public const ushort ATTR_CURRENTPARTIALPROFILEINTERVALVALUEDELIVERED = 0x0407;

        /// <summary>
        /// CurrentPartialProfileIntervalValueReceived represents the value of the
        /// current Load Profile interval being accumulated for commodity received.
        /// </summary>
        public const ushort ATTR_CURRENTPARTIALPROFILEINTERVALVALUERECEIVED = 0x0408;

        /// <summary>
        /// CurrentDayMaxPressure is the maximum pressure reported during a day from the
        /// water or gas meter.
        /// </summary>
        public const ushort ATTR_CURRENTDAYMAXPRESSURE = 0x0409;

        /// <summary>
        /// CurrentDayMinPressure is the minimum pressure reported during a day from the
        /// water or gas meter.
        /// </summary>
        public const ushort ATTR_CURRENTDAYMINPRESSURE = 0x040A;

        /// <summary>
        /// PreviousDayMaxPressure represents the maximum pressure reported during
        /// previous day from the water or gas meter.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYMAXPRESSURE = 0x040B;

        /// <summary>
        /// PreviousDayMinPressure represents the minimum pressure reported during
        /// previous day from the water or gas meter.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYMINPRESSURE = 0x040C;

        /// <summary>
        /// CurrentDayMaxDemand represents the maximum demand or rate of delivered value of
        /// Energy, Gas, or Water being utilized at the premises.
        /// </summary>
        public const ushort ATTR_CURRENTDAYMAXDEMAND = 0x040D;

        /// <summary>
        /// PreviousDayMaxDemand represents the maximum demand or rate of delivered value of
        /// Energy, Gas, or Water being utilized at the premises.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYMAXDEMAND = 0x040E;

        /// <summary>
        /// CurrentMonthMaxDemand is the maximum demand reported during a month from the
        /// meter.
        /// </summary>
        public const ushort ATTR_CURRENTMONTHMAXDEMAND = 0x040F;

        /// <summary>
        /// CurrentYearMaxDemand is the maximum demand reported during a year from the meter.
        /// </summary>
        public const ushort ATTR_CURRENTYEARMAXDEMAND = 0x0410;

        /// <summary>
        /// CurrentDayMaxEnergyCarrierDemand is the maximum energy carrier demand
        /// reported during a day from the meter.
        /// </summary>
        public const ushort ATTR_CURRENTDAYMAXENERGYCARRIERDEMAND = 0x0411;

        /// <summary>
        /// PreviousDayMaxEnergyCarrierDemand is the maximum energy carrier demand
        /// reported during the previous day from the meter.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYMAXENERGYCARRIERDEMAND = 0x0412;

        /// <summary>
        /// CurrentMonthMaxEnergyCarrierDemand is the maximum energy carrier demand
        /// reported during a month from the meter.
        /// </summary>
        public const ushort ATTR_CURRENTMONTHMAXENERGYCARRIERDEMAND = 0x0413;

        /// <summary>
        /// CurrentMonthMinEnergyCarrierDemand is the minimum energy carrier demand
        /// reported during a month from the meter.
        /// </summary>
        public const ushort ATTR_CURRENTMONTHMINENERGYCARRIERDEMAND = 0x0414;

        /// <summary>
        /// CurrentYearMaxEnergyCarrierDemand is the maximum energy carrier demand
        /// reported during a year from the meter.
        /// </summary>
        public const ushort ATTR_CURRENTYEARMAXENERGYCARRIERDEMAND = 0x0415;

        /// <summary>
        /// CurrentYearMinEnergyCarrierDemand is the minimum energy carrier demand
        /// reported during a year from the heat meter.
        /// </summary>
        public const ushort ATTR_CURRENTYEARMINENERGYCARRIERDEMAND = 0x0416;

        /// <summary>
        /// PreviousDayNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous 24 hour period starting at
        /// the Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY2CONSUMPTIONDELIVERED = 0x0422;

        /// <summary>
        /// PreviousDayNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous 24 hour period starting at
        /// the Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY4CONSUMPTIONDELIVERED = 0x0424;

        /// <summary>
        /// PreviousDayNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous 24 hour period starting at
        /// the Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY6CONSUMPTIONDELIVERED = 0x0426;

        /// <summary>
        /// PreviousDayNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous 24 hour period starting at
        /// the Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY8CONSUMPTIONDELIVERED = 0x0428;

        /// <summary>
        /// PreviousDayNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous 24 hour period starting at
        /// the Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY10CONSUMPTIONDELIVERED = 0x042A;

        /// <summary>
        /// PreviousDayNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous 24 hour period starting at
        /// the Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY12CONSUMPTIONDELIVERED = 0x042C;

        /// <summary>
        /// PreviousDayNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous 24 hour period starting at
        /// the Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY14CONSUMPTIONDELIVERED = 0x042E;

        /// <summary>
        /// PreviousDayNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous 24 hour period starting at
        /// the Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY16CONSUMPTIONDELIVERED = 0x0430;

        /// <summary>
        /// PreviousDayNConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY2CONSUMPTIONRECEIVED = 0x0423;

        /// <summary>
        /// PreviousDayNConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY4CONSUMPTIONRECEIVED = 0x0425;

        /// <summary>
        /// PreviousDayNConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY6CONSUMPTIONRECEIVED = 0x0427;

        /// <summary>
        /// PreviousDayNConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY8CONSUMPTIONRECEIVED = 0x0429;

        /// <summary>
        /// PreviousDayNConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY10CONSUMPTIONRECEIVED = 0x042B;

        /// <summary>
        /// PreviousDayNConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY12CONSUMPTIONRECEIVED = 0x042D;

        /// <summary>
        /// PreviousDayNConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY14CONSUMPTIONRECEIVED = 0x042F;

        /// <summary>
        /// PreviousDayNConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY16CONSUMPTIONRECEIVED = 0x0431;

        /// <summary>
        /// CurrentWeekConsumptionDelivered represents the summed value of Energy, Gas, or
        /// Water delivered to the premises since the Historical Freeze Time (HFT) on Monday to
        /// the last HFT read. If optionally provided, CurrentWeekConsumptionDelivered is
        /// updated continuously as new measurements are made. If the optional HFT attribute
        /// is not available, default to midnight local time.
        /// </summary>
        public const ushort ATTR_CURRENTWEEKCONSUMPTIONDELIVERED = 0x0430;

        /// <summary>
        /// CurrentWeekConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises since the Historical Freeze Time (HFT) on Monday
        /// to the last HFT read. If optionally provided, CurrentWeekConsumptionReceived is
        /// updated continuously as new measurements are made. If the optional HFT attribute
        /// is not available, default to midnight local time.
        /// </summary>
        public const ushort ATTR_CURRENTWEEKCONSUMPTIONRECEIVED = 0x0431;

        /// <summary>
        /// PreviousWeekNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK1CONSUMPTIONDELIVERED = 0x0433;

        /// <summary>
        /// PreviousWeekNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK3CONSUMPTIONDELIVERED = 0x0435;

        /// <summary>
        /// PreviousWeekNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK5CONSUMPTIONDELIVERED = 0x0437;

        /// <summary>
        /// PreviousWeekNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK7CONSUMPTIONDELIVERED = 0x0439;

        /// <summary>
        /// PreviousWeekNConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK9CONSUMPTIONDELIVERED = 0x043B;

        /// <summary>
        /// PreviousWeekNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK1CONSUMPTIONRECEIVED = 0x0434;

        /// <summary>
        /// PreviousWeekNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK3CONSUMPTIONRECEIVED = 0x0436;

        /// <summary>
        /// PreviousWeekNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK5CONSUMPTIONRECEIVED = 0x0438;

        /// <summary>
        /// PreviousWeekNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK7CONSUMPTIONRECEIVED = 0x043A;

        /// <summary>
        /// PreviousWeekNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK9CONSUMPTIONRECEIVED = 0x043C;

        /// <summary>
        /// CurrentMonthConsumptionDelivered represents the summed value of Energy, Gas,
        /// or Water delivered to the premises since the Historical Freeze Time (HFT) on the 1st
        /// of the month to the last HFT read. If optionally provided,
        /// CurrentMonthConsumptionDelivered is updated continuously as new measurements
        /// are made. If the optional HFT attribute is not available, default to midnight local
        /// time.
        /// </summary>
        public const ushort ATTR_CURRENTMONTHCONSUMPTIONDELIVERED = 0x0440;

        /// <summary>
        /// CurrentMonthConsumptionReceived represents the summed value of Energy, Gas, or
        /// Water received from the premises since the Historical Freeze Time (HFT) on the 1st
        /// of the month to the last HFT read. If optionally provided,
        /// CurrentMonthConsumptionReceived is updated continuously as new measurements
        /// are made. If the optional HFT attribute is not available, default to midnight local
        /// time.
        /// </summary>
        public const ushort ATTR_CURRENTMONTHCONSUMPTIONRECEIVED = 0x0441;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH1CONSUMPTIONDELIVERED = 0x0443;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH3CONSUMPTIONDELIVERED = 0x0445;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH5CONSUMPTIONDELIVERED = 0x0447;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH7CONSUMPTIONDELIVERED = 0x0449;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH9CONSUMPTIONDELIVERED = 0x044B;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH11CONSUMPTIONDELIVERED = 0x044D;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH13CONSUMPTIONDELIVERED = 0x044F;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH15CONSUMPTIONDELIVERED = 0x0451;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH17CONSUMPTIONDELIVERED = 0x0453;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH19CONSUMPTIONDELIVERED = 0x0455;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH21CONSUMPTIONDELIVERED = 0x0457;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH23CONSUMPTIONDELIVERED = 0x0459;

        /// <summary>
        /// PreviousMonthNConsumptionDelivered represents the summed value of Energy,
        /// Gas, or Water delivered to the premises within the previous Month period starting
        /// at the Historical Freeze Time (HFT) on the 1st of the month to the last day of the
        /// month. If the optional HFT attribute is not available, default to midnight local
        /// time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH25CONSUMPTIONDELIVERED = 0x045B;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH1CONSUMPTIONRECEIVED = 0x0444;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH3CONSUMPTIONRECEIVED = 0x0446;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH5CONSUMPTIONRECEIVED = 0x0448;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH7CONSUMPTIONRECEIVED = 0x044A;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH9CONSUMPTIONRECEIVED = 0x044C;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH11CONSUMPTIONRECEIVED = 0x044E;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH13CONSUMPTIONRECEIVED = 0x0450;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH15CONSUMPTIONRECEIVED = 0x0452;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH17CONSUMPTIONRECEIVED = 0x0454;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH19CONSUMPTIONRECEIVED = 0x0456;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH21CONSUMPTIONRECEIVED = 0x0458;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH23CONSUMPTIONRECEIVED = 0x045A;

        /// <summary>
        /// PreviousMonthNConsumptionReceived represents the summed value of Energy, Gas,
        /// or Water received from the premises within the previous month period starting at
        /// the Historical Freeze Time (HFT) on the 1st of the month to the last day of the month.
        /// If the optional HFT attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSMONTH25CONSUMPTIONRECEIVED = 0x045C;

        /// <summary>
        /// HistoricalFreezeTime (HFT) represents the time of day, in Local Time, when
        /// Historical Consumption attributes and/or Alternative Historical Consumption
        /// attributes are captured. HistoricalFreezeTime is an unsigned 16-bit value
        /// representing the hour and minutes for HFT.
        /// </summary>
        public const ushort ATTR_HISTORICALFREEZETIME = 0x045C;

        /// <summary>
        /// MaxNumberofPeriodsDelivered represents the maximum number of intervals the
        /// device is capable of returning in one Get Profile Response command. It is required
        /// MaxNumberofPeriodsDelivered fit within the default Fragmentation ASDU size of
        /// 128 bytes, or an optionally agreed upon larger Fragmentation ASDU size supported
        /// by both devices.
        /// </summary>
        public const ushort ATTR_MAXNUMBEROFPERIODSDELIVERED = 0x0500;

        /// <summary>
        /// CurrentDemandDelivered represents the current Demand of Energy, Gas, or Water
        /// delivered at the premises. CurrentDemandDelivered may be continuously updated
        /// as new measurements are acquired, but at a minimum CurrentDemandDelivered must be
        /// updated at the end of each integration sub- period, which can be obtained by
        /// dividing the DemandIntegrationPeriod by the NumberOfDemandSubintervals.
        /// </summary>
        public const ushort ATTR_CURRENTDEMANDDELIVERED = 0x0600;

        /// <summary>
        /// DemandLimit reflects the current supply demand limit set in the meter. This value
        /// can be compared to the CurrentDemandDelivered attribute to understand if limits
        /// are being approached or exceeded.
        /// </summary>
        public const ushort ATTR_DEMANDLIMIT = 0x0601;

        /// <summary>
        /// DemandIntegrationPeriod is the number of minutes over which the
        /// CurrentDemandDelivered attribute is calculated. Valid range is 0x01 to 0xFF.
        /// 0x00 is a reserved value.
        /// </summary>
        public const ushort ATTR_DEMANDINTEGRATIONPERIOD = 0x0602;

        /// <summary>
        /// NumberOfDemandSubintervals represents the number of subintervals used within
        /// the DemandIntegrationPeriod. The subinterval duration (in minutes) is obtained
        /// by dividing the DemandIntegrationPeriod by the NumberOfDemandSubintervals.
        /// The CurrentDemandDelivered attribute is updated at the each of each subinterval.
        /// Valid range is 0x01 to 0xFF. 0x00 is a reserved value.
        /// </summary>
        public const ushort ATTR_NUMBEROFDEMANDSUBINTERVALS = 0x0603;

        /// <summary>
        /// An unsigned 16-bit integer that defines the length of time, in seconds, that the
        /// supply shall be disconnected if the DemandLimit attribute is enabled and the limit
        /// is exceeded. At the end of the time period the meter shall move to the ARMED status.
        /// This will allow the user to reconnect the supply.
        /// </summary>
        public const ushort ATTR_DEMANDLIMITARMDURATION = 0x0604;

        /// <summary>
        /// The LoadLimitSupplyState attribute indicates the required status of the supply
        /// once device is in a load limit state.
        /// </summary>
        public const ushort ATTR_LOADLIMITSUPPLYSTATE = 0x0605;

        /// <summary>
        /// An unsigned 8-bit integer used for counting the number of times that the demand
        /// limit has exceeded the set threshold.
        /// </summary>
        public const ushort ATTR_LOADLIMITCOUNTER = 0x0606;

        /// <summary>
        /// The SupplyTamperState indicates the required status of the supply following the
        /// detection of a tamper event within the metering device.
        /// </summary>
        public const ushort ATTR_SUPPLYTAMPERSTATE = 0x0607;

        /// <summary>
        /// The SupplyDepletionState indicates the required status of the supply following
        /// detection of a depleted battery within the metering device.
        /// </summary>
        public const ushort ATTR_SUPPLYDEPLETIONSTATE = 0x0608;

        /// <summary>
        /// The SupplyUncontrolledFlowState indicates the required status of the supply
        /// following detection of an uncontrolled flow event within the metering device.
        /// </summary>
        public const ushort ATTR_SUPPLYUNCONTROLLEDFLOWSTATE = 0x0609;
        public const ushort ATTR_CURRENTNOTIERBLOCK1SUMMATIONDELIVERED = 0x0701;
        public const ushort ATTR_CURRENTNOTIERBLOCK2SUMMATIONDELIVERED = 0x0702;
        public const ushort ATTR_CURRENTNOTIERBLOCK3SUMMATIONDELIVERED = 0x0703;
        public const ushort ATTR_CURRENTNOTIERBLOCK4SUMMATIONDELIVERED = 0x0704;
        public const ushort ATTR_CURRENTNOTIERBLOCK5SUMMATIONDELIVERED = 0x0705;
        public const ushort ATTR_CURRENTNOTIERBLOCK6SUMMATIONDELIVERED = 0x0706;
        public const ushort ATTR_CURRENTNOTIERBLOCK7SUMMATIONDELIVERED = 0x0707;
        public const ushort ATTR_CURRENTNOTIERBLOCK8SUMMATIONDELIVERED = 0x0708;
        public const ushort ATTR_CURRENTNOTIERBLOCK9SUMMATIONDELIVERED = 0x0709;
        public const ushort ATTR_CURRENTNOTIERBLOCK10SUMMATIONDELIVERED = 0x070A;
        public const ushort ATTR_CURRENTNOTIERBLOCK11SUMMATIONDELIVERED = 0x070B;
        public const ushort ATTR_CURRENTNOTIERBLOCK12SUMMATIONDELIVERED = 0x070C;
        public const ushort ATTR_CURRENTNOTIERBLOCK13SUMMATIONDELIVERED = 0x070D;
        public const ushort ATTR_CURRENTNOTIERBLOCK14SUMMATIONDELIVERED = 0x070E;
        public const ushort ATTR_CURRENTNOTIERBLOCK15SUMMATIONDELIVERED = 0x070F;
        public const ushort ATTR_CURRENTNOTIERBLOCK16SUMMATIONDELIVERED = 0x0710;
        public const ushort ATTR_CURRENTTIER1BLOCK1SUMMATIONDELIVERED = 0x0711;
        public const ushort ATTR_CURRENTTIER1BLOCK2SUMMATIONDELIVERED = 0x0712;
        public const ushort ATTR_CURRENTTIER1BLOCK3SUMMATIONDELIVERED = 0x0713;
        public const ushort ATTR_CURRENTTIER1BLOCK4SUMMATIONDELIVERED = 0x0714;
        public const ushort ATTR_CURRENTTIER1BLOCK5SUMMATIONDELIVERED = 0x0715;
        public const ushort ATTR_CURRENTTIER1BLOCK6SUMMATIONDELIVERED = 0x0716;
        public const ushort ATTR_CURRENTTIER1BLOCK7SUMMATIONDELIVERED = 0x0717;
        public const ushort ATTR_CURRENTTIER1BLOCK8SUMMATIONDELIVERED = 0x0718;
        public const ushort ATTR_CURRENTTIER1BLOCK9SUMMATIONDELIVERED = 0x0719;
        public const ushort ATTR_CURRENTTIER1BLOCK10SUMMATIONDELIVERED = 0x071A;
        public const ushort ATTR_CURRENTTIER1BLOCK11SUMMATIONDELIVERED = 0x071B;
        public const ushort ATTR_CURRENTTIER1BLOCK12SUMMATIONDELIVERED = 0x071C;
        public const ushort ATTR_CURRENTTIER1BLOCK13SUMMATIONDELIVERED = 0x071D;
        public const ushort ATTR_CURRENTTIER1BLOCK14SUMMATIONDELIVERED = 0x071E;
        public const ushort ATTR_CURRENTTIER1BLOCK15SUMMATIONDELIVERED = 0x071F;
        public const ushort ATTR_CURRENTTIER1BLOCK16SUMMATIONDELIVERED = 0x0720;
        public const ushort ATTR_CURRENTTIER2BLOCK1SUMMATIONDELIVERED = 0x0721;
        public const ushort ATTR_CURRENTTIER2BLOCK2SUMMATIONDELIVERED = 0x0722;
        public const ushort ATTR_CURRENTTIER2BLOCK3SUMMATIONDELIVERED = 0x0723;
        public const ushort ATTR_CURRENTTIER2BLOCK4SUMMATIONDELIVERED = 0x0724;
        public const ushort ATTR_CURRENTTIER2BLOCK5SUMMATIONDELIVERED = 0x0725;
        public const ushort ATTR_CURRENTTIER2BLOCK6SUMMATIONDELIVERED = 0x0726;
        public const ushort ATTR_CURRENTTIER2BLOCK7SUMMATIONDELIVERED = 0x0727;
        public const ushort ATTR_CURRENTTIER2BLOCK8SUMMATIONDELIVERED = 0x0728;
        public const ushort ATTR_CURRENTTIER2BLOCK9SUMMATIONDELIVERED = 0x0729;
        public const ushort ATTR_CURRENTTIER2BLOCK10SUMMATIONDELIVERED = 0x072A;
        public const ushort ATTR_CURRENTTIER2BLOCK11SUMMATIONDELIVERED = 0x072B;
        public const ushort ATTR_CURRENTTIER2BLOCK12SUMMATIONDELIVERED = 0x072C;
        public const ushort ATTR_CURRENTTIER2BLOCK13SUMMATIONDELIVERED = 0x072D;
        public const ushort ATTR_CURRENTTIER2BLOCK14SUMMATIONDELIVERED = 0x072E;
        public const ushort ATTR_CURRENTTIER2BLOCK15SUMMATIONDELIVERED = 0x072F;
        public const ushort ATTR_CURRENTTIER2BLOCK16SUMMATIONDELIVERED = 0x0730;
        public const ushort ATTR_CURRENTTIER3BLOCK1SUMMATIONDELIVERED = 0x0731;
        public const ushort ATTR_CURRENTTIER3BLOCK2SUMMATIONDELIVERED = 0x0732;
        public const ushort ATTR_CURRENTTIER3BLOCK3SUMMATIONDELIVERED = 0x0733;
        public const ushort ATTR_CURRENTTIER3BLOCK4SUMMATIONDELIVERED = 0x0734;
        public const ushort ATTR_CURRENTTIER3BLOCK5SUMMATIONDELIVERED = 0x0735;
        public const ushort ATTR_CURRENTTIER3BLOCK6SUMMATIONDELIVERED = 0x0736;
        public const ushort ATTR_CURRENTTIER3BLOCK7SUMMATIONDELIVERED = 0x0737;
        public const ushort ATTR_CURRENTTIER3BLOCK8SUMMATIONDELIVERED = 0x0738;
        public const ushort ATTR_CURRENTTIER3BLOCK9SUMMATIONDELIVERED = 0x0739;
        public const ushort ATTR_CURRENTTIER3BLOCK10SUMMATIONDELIVERED = 0x073A;
        public const ushort ATTR_CURRENTTIER3BLOCK11SUMMATIONDELIVERED = 0x073B;
        public const ushort ATTR_CURRENTTIER3BLOCK12SUMMATIONDELIVERED = 0x073C;
        public const ushort ATTR_CURRENTTIER3BLOCK13SUMMATIONDELIVERED = 0x073D;
        public const ushort ATTR_CURRENTTIER3BLOCK14SUMMATIONDELIVERED = 0x073E;
        public const ushort ATTR_CURRENTTIER3BLOCK15SUMMATIONDELIVERED = 0x073F;
        public const ushort ATTR_CURRENTTIER3BLOCK16SUMMATIONDELIVERED = 0x0740;
        public const ushort ATTR_CURRENTTIER4BLOCK1SUMMATIONDELIVERED = 0x0741;
        public const ushort ATTR_CURRENTTIER4BLOCK2SUMMATIONDELIVERED = 0x0742;
        public const ushort ATTR_CURRENTTIER4BLOCK3SUMMATIONDELIVERED = 0x0743;
        public const ushort ATTR_CURRENTTIER4BLOCK4SUMMATIONDELIVERED = 0x0744;
        public const ushort ATTR_CURRENTTIER4BLOCK5SUMMATIONDELIVERED = 0x0745;
        public const ushort ATTR_CURRENTTIER4BLOCK6SUMMATIONDELIVERED = 0x0746;
        public const ushort ATTR_CURRENTTIER4BLOCK7SUMMATIONDELIVERED = 0x0747;
        public const ushort ATTR_CURRENTTIER4BLOCK8SUMMATIONDELIVERED = 0x0748;
        public const ushort ATTR_CURRENTTIER4BLOCK9SUMMATIONDELIVERED = 0x0749;
        public const ushort ATTR_CURRENTTIER4BLOCK10SUMMATIONDELIVERED = 0x074A;
        public const ushort ATTR_CURRENTTIER4BLOCK11SUMMATIONDELIVERED = 0x074B;
        public const ushort ATTR_CURRENTTIER4BLOCK12SUMMATIONDELIVERED = 0x074C;
        public const ushort ATTR_CURRENTTIER4BLOCK13SUMMATIONDELIVERED = 0x074D;
        public const ushort ATTR_CURRENTTIER4BLOCK14SUMMATIONDELIVERED = 0x074E;
        public const ushort ATTR_CURRENTTIER4BLOCK15SUMMATIONDELIVERED = 0x074F;
        public const ushort ATTR_CURRENTTIER4BLOCK16SUMMATIONDELIVERED = 0x0750;
        public const ushort ATTR_CURRENTTIER5BLOCK1SUMMATIONDELIVERED = 0x0751;
        public const ushort ATTR_CURRENTTIER5BLOCK2SUMMATIONDELIVERED = 0x0752;
        public const ushort ATTR_CURRENTTIER5BLOCK3SUMMATIONDELIVERED = 0x0753;
        public const ushort ATTR_CURRENTTIER5BLOCK4SUMMATIONDELIVERED = 0x0754;
        public const ushort ATTR_CURRENTTIER5BLOCK5SUMMATIONDELIVERED = 0x0755;
        public const ushort ATTR_CURRENTTIER5BLOCK6SUMMATIONDELIVERED = 0x0756;
        public const ushort ATTR_CURRENTTIER5BLOCK7SUMMATIONDELIVERED = 0x0757;
        public const ushort ATTR_CURRENTTIER5BLOCK8SUMMATIONDELIVERED = 0x0758;
        public const ushort ATTR_CURRENTTIER5BLOCK9SUMMATIONDELIVERED = 0x0759;
        public const ushort ATTR_CURRENTTIER5BLOCK10SUMMATIONDELIVERED = 0x075A;
        public const ushort ATTR_CURRENTTIER5BLOCK11SUMMATIONDELIVERED = 0x075B;
        public const ushort ATTR_CURRENTTIER5BLOCK12SUMMATIONDELIVERED = 0x075C;
        public const ushort ATTR_CURRENTTIER5BLOCK13SUMMATIONDELIVERED = 0x075D;
        public const ushort ATTR_CURRENTTIER5BLOCK14SUMMATIONDELIVERED = 0x075E;
        public const ushort ATTR_CURRENTTIER5BLOCK15SUMMATIONDELIVERED = 0x075F;
        public const ushort ATTR_CURRENTTIER5BLOCK16SUMMATIONDELIVERED = 0x0760;
        public const ushort ATTR_CURRENTTIER6BLOCK1SUMMATIONDELIVERED = 0x0761;
        public const ushort ATTR_CURRENTTIER6BLOCK2SUMMATIONDELIVERED = 0x0762;
        public const ushort ATTR_CURRENTTIER6BLOCK3SUMMATIONDELIVERED = 0x0763;
        public const ushort ATTR_CURRENTTIER6BLOCK4SUMMATIONDELIVERED = 0x0764;
        public const ushort ATTR_CURRENTTIER6BLOCK5SUMMATIONDELIVERED = 0x0765;
        public const ushort ATTR_CURRENTTIER6BLOCK6SUMMATIONDELIVERED = 0x0766;
        public const ushort ATTR_CURRENTTIER6BLOCK7SUMMATIONDELIVERED = 0x0767;
        public const ushort ATTR_CURRENTTIER6BLOCK8SUMMATIONDELIVERED = 0x0768;
        public const ushort ATTR_CURRENTTIER6BLOCK9SUMMATIONDELIVERED = 0x0769;
        public const ushort ATTR_CURRENTTIER6BLOCK10SUMMATIONDELIVERED = 0x076A;
        public const ushort ATTR_CURRENTTIER6BLOCK11SUMMATIONDELIVERED = 0x076B;
        public const ushort ATTR_CURRENTTIER6BLOCK12SUMMATIONDELIVERED = 0x076C;
        public const ushort ATTR_CURRENTTIER6BLOCK13SUMMATIONDELIVERED = 0x076D;
        public const ushort ATTR_CURRENTTIER6BLOCK14SUMMATIONDELIVERED = 0x076E;
        public const ushort ATTR_CURRENTTIER6BLOCK15SUMMATIONDELIVERED = 0x076F;
        public const ushort ATTR_CURRENTTIER6BLOCK16SUMMATIONDELIVERED = 0x0770;
        public const ushort ATTR_CURRENTTIER7BLOCK1SUMMATIONDELIVERED = 0x0771;
        public const ushort ATTR_CURRENTTIER7BLOCK2SUMMATIONDELIVERED = 0x0772;
        public const ushort ATTR_CURRENTTIER7BLOCK3SUMMATIONDELIVERED = 0x0773;
        public const ushort ATTR_CURRENTTIER7BLOCK4SUMMATIONDELIVERED = 0x0774;
        public const ushort ATTR_CURRENTTIER7BLOCK5SUMMATIONDELIVERED = 0x0775;
        public const ushort ATTR_CURRENTTIER7BLOCK6SUMMATIONDELIVERED = 0x0776;
        public const ushort ATTR_CURRENTTIER7BLOCK7SUMMATIONDELIVERED = 0x0777;
        public const ushort ATTR_CURRENTTIER7BLOCK8SUMMATIONDELIVERED = 0x0778;
        public const ushort ATTR_CURRENTTIER7BLOCK9SUMMATIONDELIVERED = 0x0779;
        public const ushort ATTR_CURRENTTIER7BLOCK10SUMMATIONDELIVERED = 0x077A;
        public const ushort ATTR_CURRENTTIER7BLOCK11SUMMATIONDELIVERED = 0x077B;
        public const ushort ATTR_CURRENTTIER7BLOCK12SUMMATIONDELIVERED = 0x077C;
        public const ushort ATTR_CURRENTTIER7BLOCK13SUMMATIONDELIVERED = 0x077D;
        public const ushort ATTR_CURRENTTIER7BLOCK14SUMMATIONDELIVERED = 0x077E;
        public const ushort ATTR_CURRENTTIER7BLOCK15SUMMATIONDELIVERED = 0x077F;
        public const ushort ATTR_CURRENTTIER7BLOCK16SUMMATIONDELIVERED = 0x0780;
        public const ushort ATTR_CURRENTTIER8BLOCK1SUMMATIONDELIVERED = 0x0781;
        public const ushort ATTR_CURRENTTIER8BLOCK2SUMMATIONDELIVERED = 0x0782;
        public const ushort ATTR_CURRENTTIER8BLOCK3SUMMATIONDELIVERED = 0x0783;
        public const ushort ATTR_CURRENTTIER8BLOCK4SUMMATIONDELIVERED = 0x0784;
        public const ushort ATTR_CURRENTTIER8BLOCK5SUMMATIONDELIVERED = 0x0785;
        public const ushort ATTR_CURRENTTIER8BLOCK6SUMMATIONDELIVERED = 0x0786;
        public const ushort ATTR_CURRENTTIER8BLOCK7SUMMATIONDELIVERED = 0x0787;
        public const ushort ATTR_CURRENTTIER8BLOCK8SUMMATIONDELIVERED = 0x0788;
        public const ushort ATTR_CURRENTTIER8BLOCK9SUMMATIONDELIVERED = 0x0789;
        public const ushort ATTR_CURRENTTIER8BLOCK10SUMMATIONDELIVERED = 0x078A;
        public const ushort ATTR_CURRENTTIER8BLOCK11SUMMATIONDELIVERED = 0x078B;
        public const ushort ATTR_CURRENTTIER8BLOCK12SUMMATIONDELIVERED = 0x078C;
        public const ushort ATTR_CURRENTTIER8BLOCK13SUMMATIONDELIVERED = 0x078D;
        public const ushort ATTR_CURRENTTIER8BLOCK14SUMMATIONDELIVERED = 0x078E;
        public const ushort ATTR_CURRENTTIER8BLOCK15SUMMATIONDELIVERED = 0x078F;
        public const ushort ATTR_CURRENTTIER8BLOCK16SUMMATIONDELIVERED = 0x0790;
        public const ushort ATTR_CURRENTTIER9BLOCK1SUMMATIONDELIVERED = 0x0791;
        public const ushort ATTR_CURRENTTIER9BLOCK2SUMMATIONDELIVERED = 0x0792;
        public const ushort ATTR_CURRENTTIER9BLOCK3SUMMATIONDELIVERED = 0x0793;
        public const ushort ATTR_CURRENTTIER9BLOCK4SUMMATIONDELIVERED = 0x0794;
        public const ushort ATTR_CURRENTTIER9BLOCK5SUMMATIONDELIVERED = 0x0795;
        public const ushort ATTR_CURRENTTIER9BLOCK6SUMMATIONDELIVERED = 0x0796;
        public const ushort ATTR_CURRENTTIER9BLOCK7SUMMATIONDELIVERED = 0x0797;
        public const ushort ATTR_CURRENTTIER9BLOCK8SUMMATIONDELIVERED = 0x0798;
        public const ushort ATTR_CURRENTTIER9BLOCK9SUMMATIONDELIVERED = 0x0799;
        public const ushort ATTR_CURRENTTIER9BLOCK10SUMMATIONDELIVERED = 0x079A;
        public const ushort ATTR_CURRENTTIER9BLOCK11SUMMATIONDELIVERED = 0x079B;
        public const ushort ATTR_CURRENTTIER9BLOCK12SUMMATIONDELIVERED = 0x079C;
        public const ushort ATTR_CURRENTTIER9BLOCK13SUMMATIONDELIVERED = 0x079D;
        public const ushort ATTR_CURRENTTIER9BLOCK14SUMMATIONDELIVERED = 0x079E;
        public const ushort ATTR_CURRENTTIER9BLOCK15SUMMATIONDELIVERED = 0x079F;
        public const ushort ATTR_CURRENTTIER9BLOCK16SUMMATIONDELIVERED = 0x07A0;
        public const ushort ATTR_CURRENTTIER10BLOCK1SUMMATIONDELIVERED = 0x07A1;
        public const ushort ATTR_CURRENTTIER10BLOCK2SUMMATIONDELIVERED = 0x07A2;
        public const ushort ATTR_CURRENTTIER10BLOCK3SUMMATIONDELIVERED = 0x07A3;
        public const ushort ATTR_CURRENTTIER10BLOCK4SUMMATIONDELIVERED = 0x07A4;
        public const ushort ATTR_CURRENTTIER10BLOCK5SUMMATIONDELIVERED = 0x07A5;
        public const ushort ATTR_CURRENTTIER10BLOCK6SUMMATIONDELIVERED = 0x07A6;
        public const ushort ATTR_CURRENTTIER10BLOCK7SUMMATIONDELIVERED = 0x07A7;
        public const ushort ATTR_CURRENTTIER10BLOCK8SUMMATIONDELIVERED = 0x07A8;
        public const ushort ATTR_CURRENTTIER10BLOCK9SUMMATIONDELIVERED = 0x07A9;
        public const ushort ATTR_CURRENTTIER10BLOCK10SUMMATIONDELIVERED = 0x07AA;
        public const ushort ATTR_CURRENTTIER10BLOCK11SUMMATIONDELIVERED = 0x07AB;
        public const ushort ATTR_CURRENTTIER10BLOCK12SUMMATIONDELIVERED = 0x07AC;
        public const ushort ATTR_CURRENTTIER10BLOCK13SUMMATIONDELIVERED = 0x07AD;
        public const ushort ATTR_CURRENTTIER10BLOCK14SUMMATIONDELIVERED = 0x07AE;
        public const ushort ATTR_CURRENTTIER10BLOCK15SUMMATIONDELIVERED = 0x07AF;
        public const ushort ATTR_CURRENTTIER10BLOCK16SUMMATIONDELIVERED = 0x07B0;
        public const ushort ATTR_CURRENTTIER11BLOCK1SUMMATIONDELIVERED = 0x07B1;
        public const ushort ATTR_CURRENTTIER11BLOCK2SUMMATIONDELIVERED = 0x07B2;
        public const ushort ATTR_CURRENTTIER11BLOCK3SUMMATIONDELIVERED = 0x07B3;
        public const ushort ATTR_CURRENTTIER11BLOCK4SUMMATIONDELIVERED = 0x07B4;
        public const ushort ATTR_CURRENTTIER11BLOCK5SUMMATIONDELIVERED = 0x07B5;
        public const ushort ATTR_CURRENTTIER11BLOCK6SUMMATIONDELIVERED = 0x07B6;
        public const ushort ATTR_CURRENTTIER11BLOCK7SUMMATIONDELIVERED = 0x07B7;
        public const ushort ATTR_CURRENTTIER11BLOCK8SUMMATIONDELIVERED = 0x07B8;
        public const ushort ATTR_CURRENTTIER11BLOCK9SUMMATIONDELIVERED = 0x07B9;
        public const ushort ATTR_CURRENTTIER11BLOCK10SUMMATIONDELIVERED = 0x07BA;
        public const ushort ATTR_CURRENTTIER11BLOCK11SUMMATIONDELIVERED = 0x07BB;
        public const ushort ATTR_CURRENTTIER11BLOCK12SUMMATIONDELIVERED = 0x07BC;
        public const ushort ATTR_CURRENTTIER11BLOCK13SUMMATIONDELIVERED = 0x07BD;
        public const ushort ATTR_CURRENTTIER11BLOCK14SUMMATIONDELIVERED = 0x07BE;
        public const ushort ATTR_CURRENTTIER11BLOCK15SUMMATIONDELIVERED = 0x07BF;
        public const ushort ATTR_CURRENTTIER11BLOCK16SUMMATIONDELIVERED = 0x07C0;
        public const ushort ATTR_CURRENTTIER12BLOCK1SUMMATIONDELIVERED = 0x07C1;
        public const ushort ATTR_CURRENTTIER12BLOCK2SUMMATIONDELIVERED = 0x07C2;
        public const ushort ATTR_CURRENTTIER12BLOCK3SUMMATIONDELIVERED = 0x07C3;
        public const ushort ATTR_CURRENTTIER12BLOCK4SUMMATIONDELIVERED = 0x07C4;
        public const ushort ATTR_CURRENTTIER12BLOCK5SUMMATIONDELIVERED = 0x07C5;
        public const ushort ATTR_CURRENTTIER12BLOCK6SUMMATIONDELIVERED = 0x07C6;
        public const ushort ATTR_CURRENTTIER12BLOCK7SUMMATIONDELIVERED = 0x07C7;
        public const ushort ATTR_CURRENTTIER12BLOCK8SUMMATIONDELIVERED = 0x07C8;
        public const ushort ATTR_CURRENTTIER12BLOCK9SUMMATIONDELIVERED = 0x07C9;
        public const ushort ATTR_CURRENTTIER12BLOCK10SUMMATIONDELIVERED = 0x07CA;
        public const ushort ATTR_CURRENTTIER12BLOCK11SUMMATIONDELIVERED = 0x07CB;
        public const ushort ATTR_CURRENTTIER12BLOCK12SUMMATIONDELIVERED = 0x07CC;
        public const ushort ATTR_CURRENTTIER12BLOCK13SUMMATIONDELIVERED = 0x07CD;
        public const ushort ATTR_CURRENTTIER12BLOCK14SUMMATIONDELIVERED = 0x07CE;
        public const ushort ATTR_CURRENTTIER12BLOCK15SUMMATIONDELIVERED = 0x07CF;
        public const ushort ATTR_CURRENTTIER12BLOCK16SUMMATIONDELIVERED = 0x07D0;
        public const ushort ATTR_CURRENTTIER13BLOCK1SUMMATIONDELIVERED = 0x07D1;
        public const ushort ATTR_CURRENTTIER13BLOCK2SUMMATIONDELIVERED = 0x07D2;
        public const ushort ATTR_CURRENTTIER13BLOCK3SUMMATIONDELIVERED = 0x07D3;
        public const ushort ATTR_CURRENTTIER13BLOCK4SUMMATIONDELIVERED = 0x07D4;
        public const ushort ATTR_CURRENTTIER13BLOCK5SUMMATIONDELIVERED = 0x07D5;
        public const ushort ATTR_CURRENTTIER13BLOCK6SUMMATIONDELIVERED = 0x07D6;
        public const ushort ATTR_CURRENTTIER13BLOCK7SUMMATIONDELIVERED = 0x07D7;
        public const ushort ATTR_CURRENTTIER13BLOCK8SUMMATIONDELIVERED = 0x07D8;
        public const ushort ATTR_CURRENTTIER13BLOCK9SUMMATIONDELIVERED = 0x07D9;
        public const ushort ATTR_CURRENTTIER13BLOCK10SUMMATIONDELIVERED = 0x07DA;
        public const ushort ATTR_CURRENTTIER13BLOCK11SUMMATIONDELIVERED = 0x07DB;
        public const ushort ATTR_CURRENTTIER13BLOCK12SUMMATIONDELIVERED = 0x07DC;
        public const ushort ATTR_CURRENTTIER13BLOCK13SUMMATIONDELIVERED = 0x07DD;
        public const ushort ATTR_CURRENTTIER13BLOCK14SUMMATIONDELIVERED = 0x07DE;
        public const ushort ATTR_CURRENTTIER13BLOCK15SUMMATIONDELIVERED = 0x07DF;
        public const ushort ATTR_CURRENTTIER13BLOCK16SUMMATIONDELIVERED = 0x07E0;
        public const ushort ATTR_CURRENTTIER14BLOCK1SUMMATIONDELIVERED = 0x07E1;
        public const ushort ATTR_CURRENTTIER14BLOCK2SUMMATIONDELIVERED = 0x07E2;
        public const ushort ATTR_CURRENTTIER14BLOCK3SUMMATIONDELIVERED = 0x07E3;
        public const ushort ATTR_CURRENTTIER14BLOCK4SUMMATIONDELIVERED = 0x07E4;
        public const ushort ATTR_CURRENTTIER14BLOCK5SUMMATIONDELIVERED = 0x07E5;
        public const ushort ATTR_CURRENTTIER14BLOCK6SUMMATIONDELIVERED = 0x07E6;
        public const ushort ATTR_CURRENTTIER14BLOCK7SUMMATIONDELIVERED = 0x07E7;
        public const ushort ATTR_CURRENTTIER14BLOCK8SUMMATIONDELIVERED = 0x07E8;
        public const ushort ATTR_CURRENTTIER14BLOCK9SUMMATIONDELIVERED = 0x07E9;
        public const ushort ATTR_CURRENTTIER14BLOCK10SUMMATIONDELIVERED = 0x07EA;
        public const ushort ATTR_CURRENTTIER14BLOCK11SUMMATIONDELIVERED = 0x07EB;
        public const ushort ATTR_CURRENTTIER14BLOCK12SUMMATIONDELIVERED = 0x07EC;
        public const ushort ATTR_CURRENTTIER14BLOCK13SUMMATIONDELIVERED = 0x07ED;
        public const ushort ATTR_CURRENTTIER14BLOCK14SUMMATIONDELIVERED = 0x07EE;
        public const ushort ATTR_CURRENTTIER14BLOCK15SUMMATIONDELIVERED = 0x07EF;
        public const ushort ATTR_CURRENTTIER14BLOCK16SUMMATIONDELIVERED = 0x07F0;
        public const ushort ATTR_CURRENTTIER15BLOCK1SUMMATIONDELIVERED = 0x07F1;
        public const ushort ATTR_CURRENTTIER15BLOCK2SUMMATIONDELIVERED = 0x07F2;
        public const ushort ATTR_CURRENTTIER15BLOCK3SUMMATIONDELIVERED = 0x07F3;
        public const ushort ATTR_CURRENTTIER15BLOCK4SUMMATIONDELIVERED = 0x07F4;
        public const ushort ATTR_CURRENTTIER15BLOCK5SUMMATIONDELIVERED = 0x07F5;
        public const ushort ATTR_CURRENTTIER15BLOCK6SUMMATIONDELIVERED = 0x07F6;
        public const ushort ATTR_CURRENTTIER15BLOCK7SUMMATIONDELIVERED = 0x07F7;
        public const ushort ATTR_CURRENTTIER15BLOCK8SUMMATIONDELIVERED = 0x07F8;
        public const ushort ATTR_CURRENTTIER15BLOCK9SUMMATIONDELIVERED = 0x07F9;
        public const ushort ATTR_CURRENTTIER15BLOCK10SUMMATIONDELIVERED = 0x07FA;
        public const ushort ATTR_CURRENTTIER15BLOCK11SUMMATIONDELIVERED = 0x07FB;
        public const ushort ATTR_CURRENTTIER15BLOCK12SUMMATIONDELIVERED = 0x07FC;
        public const ushort ATTR_CURRENTTIER15BLOCK13SUMMATIONDELIVERED = 0x07FD;
        public const ushort ATTR_CURRENTTIER15BLOCK14SUMMATIONDELIVERED = 0x07FE;
        public const ushort ATTR_CURRENTTIER15BLOCK15SUMMATIONDELIVERED = 0x07FF;
        public const ushort ATTR_CURRENTTIER15BLOCK16SUMMATIONDELIVERED = 0x0800;
        public const ushort ATTR_GENERICALARMMASK = 0x0800;
        public const ushort ATTR_ELECTRICITYALARMMASK = 0x0801;
        public const ushort ATTR_GENERICFLOWPRESSUREALARMMASK = 0x0802;
        public const ushort ATTR_WATERSPECIFICALARMMASK = 0x0803;
        public const ushort ATTR_HEATANDCOOLINGSPECIFICALARMMASK = 0x0804;
        public const ushort ATTR_GASSPECIFICALARMMASK = 0x0805;
        public const ushort ATTR_EXTENDEDGENERICALARMMASK = 0x0806;
        public const ushort ATTR_MANUFACTUREALARMMASK = 0x0807;
        public const ushort ATTR_CURRENTNOTIERBLOCK1SUMMATIONRECEIVED = 0x0901;
        public const ushort ATTR_CURRENTNOTIERBLOCK2SUMMATIONRECEIVED = 0x0902;
        public const ushort ATTR_CURRENTNOTIERBLOCK3SUMMATIONRECEIVED = 0x0903;
        public const ushort ATTR_CURRENTNOTIERBLOCK4SUMMATIONRECEIVED = 0x0904;
        public const ushort ATTR_CURRENTNOTIERBLOCK5SUMMATIONRECEIVED = 0x0905;
        public const ushort ATTR_CURRENTNOTIERBLOCK6SUMMATIONRECEIVED = 0x0906;
        public const ushort ATTR_CURRENTNOTIERBLOCK7SUMMATIONRECEIVED = 0x0907;
        public const ushort ATTR_CURRENTNOTIERBLOCK8SUMMATIONRECEIVED = 0x0908;
        public const ushort ATTR_CURRENTNOTIERBLOCK9SUMMATIONRECEIVED = 0x0909;
        public const ushort ATTR_CURRENTNOTIERBLOCK10SUMMATIONRECEIVED = 0x090A;
        public const ushort ATTR_CURRENTNOTIERBLOCK11SUMMATIONRECEIVED = 0x090B;
        public const ushort ATTR_CURRENTNOTIERBLOCK12SUMMATIONRECEIVED = 0x090C;
        public const ushort ATTR_CURRENTNOTIERBLOCK13SUMMATIONRECEIVED = 0x090D;
        public const ushort ATTR_CURRENTNOTIERBLOCK14SUMMATIONRECEIVED = 0x090E;
        public const ushort ATTR_CURRENTNOTIERBLOCK15SUMMATIONRECEIVED = 0x090F;
        public const ushort ATTR_CURRENTNOTIERBLOCK16SUMMATIONRECEIVED = 0x0910;
        public const ushort ATTR_CURRENTTIER1BLOCK1SUMMATIONRECEIVED = 0x0911;
        public const ushort ATTR_CURRENTTIER1BLOCK2SUMMATIONRECEIVED = 0x0912;
        public const ushort ATTR_CURRENTTIER1BLOCK3SUMMATIONRECEIVED = 0x0913;
        public const ushort ATTR_CURRENTTIER1BLOCK4SUMMATIONRECEIVED = 0x0914;
        public const ushort ATTR_CURRENTTIER1BLOCK5SUMMATIONRECEIVED = 0x0915;
        public const ushort ATTR_CURRENTTIER1BLOCK6SUMMATIONRECEIVED = 0x0916;
        public const ushort ATTR_CURRENTTIER1BLOCK7SUMMATIONRECEIVED = 0x0917;
        public const ushort ATTR_CURRENTTIER1BLOCK8SUMMATIONRECEIVED = 0x0918;
        public const ushort ATTR_CURRENTTIER1BLOCK9SUMMATIONRECEIVED = 0x0919;
        public const ushort ATTR_CURRENTTIER1BLOCK10SUMMATIONRECEIVED = 0x091A;
        public const ushort ATTR_CURRENTTIER1BLOCK11SUMMATIONRECEIVED = 0x091B;
        public const ushort ATTR_CURRENTTIER1BLOCK12SUMMATIONRECEIVED = 0x091C;
        public const ushort ATTR_CURRENTTIER1BLOCK13SUMMATIONRECEIVED = 0x091D;
        public const ushort ATTR_CURRENTTIER1BLOCK14SUMMATIONRECEIVED = 0x091E;
        public const ushort ATTR_CURRENTTIER1BLOCK15SUMMATIONRECEIVED = 0x091F;
        public const ushort ATTR_CURRENTTIER1BLOCK16SUMMATIONRECEIVED = 0x0920;
        public const ushort ATTR_CURRENTTIER2BLOCK1SUMMATIONRECEIVED = 0x0921;
        public const ushort ATTR_CURRENTTIER2BLOCK2SUMMATIONRECEIVED = 0x0922;
        public const ushort ATTR_CURRENTTIER2BLOCK3SUMMATIONRECEIVED = 0x0923;
        public const ushort ATTR_CURRENTTIER2BLOCK4SUMMATIONRECEIVED = 0x0924;
        public const ushort ATTR_CURRENTTIER2BLOCK5SUMMATIONRECEIVED = 0x0925;
        public const ushort ATTR_CURRENTTIER2BLOCK6SUMMATIONRECEIVED = 0x0926;
        public const ushort ATTR_CURRENTTIER2BLOCK7SUMMATIONRECEIVED = 0x0927;
        public const ushort ATTR_CURRENTTIER2BLOCK8SUMMATIONRECEIVED = 0x0928;
        public const ushort ATTR_CURRENTTIER2BLOCK9SUMMATIONRECEIVED = 0x0929;
        public const ushort ATTR_CURRENTTIER2BLOCK10SUMMATIONRECEIVED = 0x092A;
        public const ushort ATTR_CURRENTTIER2BLOCK11SUMMATIONRECEIVED = 0x092B;
        public const ushort ATTR_CURRENTTIER2BLOCK12SUMMATIONRECEIVED = 0x092C;
        public const ushort ATTR_CURRENTTIER2BLOCK13SUMMATIONRECEIVED = 0x092D;
        public const ushort ATTR_CURRENTTIER2BLOCK14SUMMATIONRECEIVED = 0x092E;
        public const ushort ATTR_CURRENTTIER2BLOCK15SUMMATIONRECEIVED = 0x092F;
        public const ushort ATTR_CURRENTTIER2BLOCK16SUMMATIONRECEIVED = 0x0930;
        public const ushort ATTR_CURRENTTIER3BLOCK1SUMMATIONRECEIVED = 0x0931;
        public const ushort ATTR_CURRENTTIER3BLOCK2SUMMATIONRECEIVED = 0x0932;
        public const ushort ATTR_CURRENTTIER3BLOCK3SUMMATIONRECEIVED = 0x0933;
        public const ushort ATTR_CURRENTTIER3BLOCK4SUMMATIONRECEIVED = 0x0934;
        public const ushort ATTR_CURRENTTIER3BLOCK5SUMMATIONRECEIVED = 0x0935;
        public const ushort ATTR_CURRENTTIER3BLOCK6SUMMATIONRECEIVED = 0x0936;
        public const ushort ATTR_CURRENTTIER3BLOCK7SUMMATIONRECEIVED = 0x0937;
        public const ushort ATTR_CURRENTTIER3BLOCK8SUMMATIONRECEIVED = 0x0938;
        public const ushort ATTR_CURRENTTIER3BLOCK9SUMMATIONRECEIVED = 0x0939;
        public const ushort ATTR_CURRENTTIER3BLOCK10SUMMATIONRECEIVED = 0x093A;
        public const ushort ATTR_CURRENTTIER3BLOCK11SUMMATIONRECEIVED = 0x093B;
        public const ushort ATTR_CURRENTTIER3BLOCK12SUMMATIONRECEIVED = 0x093C;
        public const ushort ATTR_CURRENTTIER3BLOCK13SUMMATIONRECEIVED = 0x093D;
        public const ushort ATTR_CURRENTTIER3BLOCK14SUMMATIONRECEIVED = 0x093E;
        public const ushort ATTR_CURRENTTIER3BLOCK15SUMMATIONRECEIVED = 0x093F;
        public const ushort ATTR_CURRENTTIER3BLOCK16SUMMATIONRECEIVED = 0x0940;
        public const ushort ATTR_CURRENTTIER4BLOCK1SUMMATIONRECEIVED = 0x0941;
        public const ushort ATTR_CURRENTTIER4BLOCK2SUMMATIONRECEIVED = 0x0942;
        public const ushort ATTR_CURRENTTIER4BLOCK3SUMMATIONRECEIVED = 0x0943;
        public const ushort ATTR_CURRENTTIER4BLOCK4SUMMATIONRECEIVED = 0x0944;
        public const ushort ATTR_CURRENTTIER4BLOCK5SUMMATIONRECEIVED = 0x0945;
        public const ushort ATTR_CURRENTTIER4BLOCK6SUMMATIONRECEIVED = 0x0946;
        public const ushort ATTR_CURRENTTIER4BLOCK7SUMMATIONRECEIVED = 0x0947;
        public const ushort ATTR_CURRENTTIER4BLOCK8SUMMATIONRECEIVED = 0x0948;
        public const ushort ATTR_CURRENTTIER4BLOCK9SUMMATIONRECEIVED = 0x0949;
        public const ushort ATTR_CURRENTTIER4BLOCK10SUMMATIONRECEIVED = 0x094A;
        public const ushort ATTR_CURRENTTIER4BLOCK11SUMMATIONRECEIVED = 0x094B;
        public const ushort ATTR_CURRENTTIER4BLOCK12SUMMATIONRECEIVED = 0x094C;
        public const ushort ATTR_CURRENTTIER4BLOCK13SUMMATIONRECEIVED = 0x094D;
        public const ushort ATTR_CURRENTTIER4BLOCK14SUMMATIONRECEIVED = 0x094E;
        public const ushort ATTR_CURRENTTIER4BLOCK15SUMMATIONRECEIVED = 0x094F;
        public const ushort ATTR_CURRENTTIER4BLOCK16SUMMATIONRECEIVED = 0x0950;
        public const ushort ATTR_CURRENTTIER5BLOCK1SUMMATIONRECEIVED = 0x0951;
        public const ushort ATTR_CURRENTTIER5BLOCK2SUMMATIONRECEIVED = 0x0952;
        public const ushort ATTR_CURRENTTIER5BLOCK3SUMMATIONRECEIVED = 0x0953;
        public const ushort ATTR_CURRENTTIER5BLOCK4SUMMATIONRECEIVED = 0x0954;
        public const ushort ATTR_CURRENTTIER5BLOCK5SUMMATIONRECEIVED = 0x0955;
        public const ushort ATTR_CURRENTTIER5BLOCK6SUMMATIONRECEIVED = 0x0956;
        public const ushort ATTR_CURRENTTIER5BLOCK7SUMMATIONRECEIVED = 0x0957;
        public const ushort ATTR_CURRENTTIER5BLOCK8SUMMATIONRECEIVED = 0x0958;
        public const ushort ATTR_CURRENTTIER5BLOCK9SUMMATIONRECEIVED = 0x0959;
        public const ushort ATTR_CURRENTTIER5BLOCK10SUMMATIONRECEIVED = 0x095A;
        public const ushort ATTR_CURRENTTIER5BLOCK11SUMMATIONRECEIVED = 0x095B;
        public const ushort ATTR_CURRENTTIER5BLOCK12SUMMATIONRECEIVED = 0x095C;
        public const ushort ATTR_CURRENTTIER5BLOCK13SUMMATIONRECEIVED = 0x095D;
        public const ushort ATTR_CURRENTTIER5BLOCK14SUMMATIONRECEIVED = 0x095E;
        public const ushort ATTR_CURRENTTIER5BLOCK15SUMMATIONRECEIVED = 0x095F;
        public const ushort ATTR_CURRENTTIER5BLOCK16SUMMATIONRECEIVED = 0x0960;
        public const ushort ATTR_CURRENTTIER6BLOCK1SUMMATIONRECEIVED = 0x0961;
        public const ushort ATTR_CURRENTTIER6BLOCK2SUMMATIONRECEIVED = 0x0962;
        public const ushort ATTR_CURRENTTIER6BLOCK3SUMMATIONRECEIVED = 0x0963;
        public const ushort ATTR_CURRENTTIER6BLOCK4SUMMATIONRECEIVED = 0x0964;
        public const ushort ATTR_CURRENTTIER6BLOCK5SUMMATIONRECEIVED = 0x0965;
        public const ushort ATTR_CURRENTTIER6BLOCK6SUMMATIONRECEIVED = 0x0966;
        public const ushort ATTR_CURRENTTIER6BLOCK7SUMMATIONRECEIVED = 0x0967;
        public const ushort ATTR_CURRENTTIER6BLOCK8SUMMATIONRECEIVED = 0x0968;
        public const ushort ATTR_CURRENTTIER6BLOCK9SUMMATIONRECEIVED = 0x0969;
        public const ushort ATTR_CURRENTTIER6BLOCK10SUMMATIONRECEIVED = 0x096A;
        public const ushort ATTR_CURRENTTIER6BLOCK11SUMMATIONRECEIVED = 0x096B;
        public const ushort ATTR_CURRENTTIER6BLOCK12SUMMATIONRECEIVED = 0x096C;
        public const ushort ATTR_CURRENTTIER6BLOCK13SUMMATIONRECEIVED = 0x096D;
        public const ushort ATTR_CURRENTTIER6BLOCK14SUMMATIONRECEIVED = 0x096E;
        public const ushort ATTR_CURRENTTIER6BLOCK15SUMMATIONRECEIVED = 0x096F;
        public const ushort ATTR_CURRENTTIER6BLOCK16SUMMATIONRECEIVED = 0x0970;
        public const ushort ATTR_CURRENTTIER7BLOCK1SUMMATIONRECEIVED = 0x0971;
        public const ushort ATTR_CURRENTTIER7BLOCK2SUMMATIONRECEIVED = 0x0972;
        public const ushort ATTR_CURRENTTIER7BLOCK3SUMMATIONRECEIVED = 0x0973;
        public const ushort ATTR_CURRENTTIER7BLOCK4SUMMATIONRECEIVED = 0x0974;
        public const ushort ATTR_CURRENTTIER7BLOCK5SUMMATIONRECEIVED = 0x0975;
        public const ushort ATTR_CURRENTTIER7BLOCK6SUMMATIONRECEIVED = 0x0976;
        public const ushort ATTR_CURRENTTIER7BLOCK7SUMMATIONRECEIVED = 0x0977;
        public const ushort ATTR_CURRENTTIER7BLOCK8SUMMATIONRECEIVED = 0x0978;
        public const ushort ATTR_CURRENTTIER7BLOCK9SUMMATIONRECEIVED = 0x0979;
        public const ushort ATTR_CURRENTTIER7BLOCK10SUMMATIONRECEIVED = 0x097A;
        public const ushort ATTR_CURRENTTIER7BLOCK11SUMMATIONRECEIVED = 0x097B;
        public const ushort ATTR_CURRENTTIER7BLOCK12SUMMATIONRECEIVED = 0x097C;
        public const ushort ATTR_CURRENTTIER7BLOCK13SUMMATIONRECEIVED = 0x097D;
        public const ushort ATTR_CURRENTTIER7BLOCK14SUMMATIONRECEIVED = 0x097E;
        public const ushort ATTR_CURRENTTIER7BLOCK15SUMMATIONRECEIVED = 0x097F;
        public const ushort ATTR_CURRENTTIER7BLOCK16SUMMATIONRECEIVED = 0x0980;
        public const ushort ATTR_CURRENTTIER8BLOCK1SUMMATIONRECEIVED = 0x0981;
        public const ushort ATTR_CURRENTTIER8BLOCK2SUMMATIONRECEIVED = 0x0982;
        public const ushort ATTR_CURRENTTIER8BLOCK3SUMMATIONRECEIVED = 0x0983;
        public const ushort ATTR_CURRENTTIER8BLOCK4SUMMATIONRECEIVED = 0x0984;
        public const ushort ATTR_CURRENTTIER8BLOCK5SUMMATIONRECEIVED = 0x0985;
        public const ushort ATTR_CURRENTTIER8BLOCK6SUMMATIONRECEIVED = 0x0986;
        public const ushort ATTR_CURRENTTIER8BLOCK7SUMMATIONRECEIVED = 0x0987;
        public const ushort ATTR_CURRENTTIER8BLOCK8SUMMATIONRECEIVED = 0x0988;
        public const ushort ATTR_CURRENTTIER8BLOCK9SUMMATIONRECEIVED = 0x0989;
        public const ushort ATTR_CURRENTTIER8BLOCK10SUMMATIONRECEIVED = 0x098A;
        public const ushort ATTR_CURRENTTIER8BLOCK11SUMMATIONRECEIVED = 0x098B;
        public const ushort ATTR_CURRENTTIER8BLOCK12SUMMATIONRECEIVED = 0x098C;
        public const ushort ATTR_CURRENTTIER8BLOCK13SUMMATIONRECEIVED = 0x098D;
        public const ushort ATTR_CURRENTTIER8BLOCK14SUMMATIONRECEIVED = 0x098E;
        public const ushort ATTR_CURRENTTIER8BLOCK15SUMMATIONRECEIVED = 0x098F;
        public const ushort ATTR_CURRENTTIER8BLOCK16SUMMATIONRECEIVED = 0x0990;
        public const ushort ATTR_CURRENTTIER9BLOCK1SUMMATIONRECEIVED = 0x0991;
        public const ushort ATTR_CURRENTTIER9BLOCK2SUMMATIONRECEIVED = 0x0992;
        public const ushort ATTR_CURRENTTIER9BLOCK3SUMMATIONRECEIVED = 0x0993;
        public const ushort ATTR_CURRENTTIER9BLOCK4SUMMATIONRECEIVED = 0x0994;
        public const ushort ATTR_CURRENTTIER9BLOCK5SUMMATIONRECEIVED = 0x0995;
        public const ushort ATTR_CURRENTTIER9BLOCK6SUMMATIONRECEIVED = 0x0996;
        public const ushort ATTR_CURRENTTIER9BLOCK7SUMMATIONRECEIVED = 0x0997;
        public const ushort ATTR_CURRENTTIER9BLOCK8SUMMATIONRECEIVED = 0x0998;
        public const ushort ATTR_CURRENTTIER9BLOCK9SUMMATIONRECEIVED = 0x0999;
        public const ushort ATTR_CURRENTTIER9BLOCK10SUMMATIONRECEIVED = 0x099A;
        public const ushort ATTR_CURRENTTIER9BLOCK11SUMMATIONRECEIVED = 0x099B;
        public const ushort ATTR_CURRENTTIER9BLOCK12SUMMATIONRECEIVED = 0x099C;
        public const ushort ATTR_CURRENTTIER9BLOCK13SUMMATIONRECEIVED = 0x099D;
        public const ushort ATTR_CURRENTTIER9BLOCK14SUMMATIONRECEIVED = 0x099E;
        public const ushort ATTR_CURRENTTIER9BLOCK15SUMMATIONRECEIVED = 0x099F;
        public const ushort ATTR_CURRENTTIER9BLOCK16SUMMATIONRECEIVED = 0x09A0;
        public const ushort ATTR_CURRENTTIER10BLOCK1SUMMATIONRECEIVED = 0x09A1;
        public const ushort ATTR_CURRENTTIER10BLOCK2SUMMATIONRECEIVED = 0x09A2;
        public const ushort ATTR_CURRENTTIER10BLOCK3SUMMATIONRECEIVED = 0x09A3;
        public const ushort ATTR_CURRENTTIER10BLOCK4SUMMATIONRECEIVED = 0x09A4;
        public const ushort ATTR_CURRENTTIER10BLOCK5SUMMATIONRECEIVED = 0x09A5;
        public const ushort ATTR_CURRENTTIER10BLOCK6SUMMATIONRECEIVED = 0x09A6;
        public const ushort ATTR_CURRENTTIER10BLOCK7SUMMATIONRECEIVED = 0x09A7;
        public const ushort ATTR_CURRENTTIER10BLOCK8SUMMATIONRECEIVED = 0x09A8;
        public const ushort ATTR_CURRENTTIER10BLOCK9SUMMATIONRECEIVED = 0x09A9;
        public const ushort ATTR_CURRENTTIER10BLOCK10SUMMATIONRECEIVED = 0x09AA;
        public const ushort ATTR_CURRENTTIER10BLOCK11SUMMATIONRECEIVED = 0x09AB;
        public const ushort ATTR_CURRENTTIER10BLOCK12SUMMATIONRECEIVED = 0x09AC;
        public const ushort ATTR_CURRENTTIER10BLOCK13SUMMATIONRECEIVED = 0x09AD;
        public const ushort ATTR_CURRENTTIER10BLOCK14SUMMATIONRECEIVED = 0x09AE;
        public const ushort ATTR_CURRENTTIER10BLOCK15SUMMATIONRECEIVED = 0x09AF;
        public const ushort ATTR_CURRENTTIER10BLOCK16SUMMATIONRECEIVED = 0x09B0;
        public const ushort ATTR_CURRENTTIER11BLOCK1SUMMATIONRECEIVED = 0x09B1;
        public const ushort ATTR_CURRENTTIER11BLOCK2SUMMATIONRECEIVED = 0x09B2;
        public const ushort ATTR_CURRENTTIER11BLOCK3SUMMATIONRECEIVED = 0x09B3;
        public const ushort ATTR_CURRENTTIER11BLOCK4SUMMATIONRECEIVED = 0x09B4;
        public const ushort ATTR_CURRENTTIER11BLOCK5SUMMATIONRECEIVED = 0x09B5;
        public const ushort ATTR_CURRENTTIER11BLOCK6SUMMATIONRECEIVED = 0x09B6;
        public const ushort ATTR_CURRENTTIER11BLOCK7SUMMATIONRECEIVED = 0x09B7;
        public const ushort ATTR_CURRENTTIER11BLOCK8SUMMATIONRECEIVED = 0x09B8;
        public const ushort ATTR_CURRENTTIER11BLOCK9SUMMATIONRECEIVED = 0x09B9;
        public const ushort ATTR_CURRENTTIER11BLOCK10SUMMATIONRECEIVED = 0x09BA;
        public const ushort ATTR_CURRENTTIER11BLOCK11SUMMATIONRECEIVED = 0x09BB;
        public const ushort ATTR_CURRENTTIER11BLOCK12SUMMATIONRECEIVED = 0x09BC;
        public const ushort ATTR_CURRENTTIER11BLOCK13SUMMATIONRECEIVED = 0x09BD;
        public const ushort ATTR_CURRENTTIER11BLOCK14SUMMATIONRECEIVED = 0x09BE;
        public const ushort ATTR_CURRENTTIER11BLOCK15SUMMATIONRECEIVED = 0x09BF;
        public const ushort ATTR_CURRENTTIER11BLOCK16SUMMATIONRECEIVED = 0x09C0;
        public const ushort ATTR_CURRENTTIER12BLOCK1SUMMATIONRECEIVED = 0x09C1;
        public const ushort ATTR_CURRENTTIER12BLOCK2SUMMATIONRECEIVED = 0x09C2;
        public const ushort ATTR_CURRENTTIER12BLOCK3SUMMATIONRECEIVED = 0x09C3;
        public const ushort ATTR_CURRENTTIER12BLOCK4SUMMATIONRECEIVED = 0x09C4;
        public const ushort ATTR_CURRENTTIER12BLOCK5SUMMATIONRECEIVED = 0x09C5;
        public const ushort ATTR_CURRENTTIER12BLOCK6SUMMATIONRECEIVED = 0x09C6;
        public const ushort ATTR_CURRENTTIER12BLOCK7SUMMATIONRECEIVED = 0x09C7;
        public const ushort ATTR_CURRENTTIER12BLOCK8SUMMATIONRECEIVED = 0x09C8;
        public const ushort ATTR_CURRENTTIER12BLOCK9SUMMATIONRECEIVED = 0x09C9;
        public const ushort ATTR_CURRENTTIER12BLOCK10SUMMATIONRECEIVED = 0x09CA;
        public const ushort ATTR_CURRENTTIER12BLOCK11SUMMATIONRECEIVED = 0x09CB;
        public const ushort ATTR_CURRENTTIER12BLOCK12SUMMATIONRECEIVED = 0x09CC;
        public const ushort ATTR_CURRENTTIER12BLOCK13SUMMATIONRECEIVED = 0x09CD;
        public const ushort ATTR_CURRENTTIER12BLOCK14SUMMATIONRECEIVED = 0x09CE;
        public const ushort ATTR_CURRENTTIER12BLOCK15SUMMATIONRECEIVED = 0x09CF;
        public const ushort ATTR_CURRENTTIER12BLOCK16SUMMATIONRECEIVED = 0x09D0;
        public const ushort ATTR_CURRENTTIER13BLOCK1SUMMATIONRECEIVED = 0x09D1;
        public const ushort ATTR_CURRENTTIER13BLOCK2SUMMATIONRECEIVED = 0x09D2;
        public const ushort ATTR_CURRENTTIER13BLOCK3SUMMATIONRECEIVED = 0x09D3;
        public const ushort ATTR_CURRENTTIER13BLOCK4SUMMATIONRECEIVED = 0x09D4;
        public const ushort ATTR_CURRENTTIER13BLOCK5SUMMATIONRECEIVED = 0x09D5;
        public const ushort ATTR_CURRENTTIER13BLOCK6SUMMATIONRECEIVED = 0x09D6;
        public const ushort ATTR_CURRENTTIER13BLOCK7SUMMATIONRECEIVED = 0x09D7;
        public const ushort ATTR_CURRENTTIER13BLOCK8SUMMATIONRECEIVED = 0x09D8;
        public const ushort ATTR_CURRENTTIER13BLOCK9SUMMATIONRECEIVED = 0x09D9;
        public const ushort ATTR_CURRENTTIER13BLOCK10SUMMATIONRECEIVED = 0x09DA;
        public const ushort ATTR_CURRENTTIER13BLOCK11SUMMATIONRECEIVED = 0x09DB;
        public const ushort ATTR_CURRENTTIER13BLOCK12SUMMATIONRECEIVED = 0x09DC;
        public const ushort ATTR_CURRENTTIER13BLOCK13SUMMATIONRECEIVED = 0x09DD;
        public const ushort ATTR_CURRENTTIER13BLOCK14SUMMATIONRECEIVED = 0x09DE;
        public const ushort ATTR_CURRENTTIER13BLOCK15SUMMATIONRECEIVED = 0x09DF;
        public const ushort ATTR_CURRENTTIER13BLOCK16SUMMATIONRECEIVED = 0x09E0;
        public const ushort ATTR_CURRENTTIER14BLOCK1SUMMATIONRECEIVED = 0x09E1;
        public const ushort ATTR_CURRENTTIER14BLOCK2SUMMATIONRECEIVED = 0x09E2;
        public const ushort ATTR_CURRENTTIER14BLOCK3SUMMATIONRECEIVED = 0x09E3;
        public const ushort ATTR_CURRENTTIER14BLOCK4SUMMATIONRECEIVED = 0x09E4;
        public const ushort ATTR_CURRENTTIER14BLOCK5SUMMATIONRECEIVED = 0x09E5;
        public const ushort ATTR_CURRENTTIER14BLOCK6SUMMATIONRECEIVED = 0x09E6;
        public const ushort ATTR_CURRENTTIER14BLOCK7SUMMATIONRECEIVED = 0x09E7;
        public const ushort ATTR_CURRENTTIER14BLOCK8SUMMATIONRECEIVED = 0x09E8;
        public const ushort ATTR_CURRENTTIER14BLOCK9SUMMATIONRECEIVED = 0x09E9;
        public const ushort ATTR_CURRENTTIER14BLOCK10SUMMATIONRECEIVED = 0x09EA;
        public const ushort ATTR_CURRENTTIER14BLOCK11SUMMATIONRECEIVED = 0x09EB;
        public const ushort ATTR_CURRENTTIER14BLOCK12SUMMATIONRECEIVED = 0x09EC;
        public const ushort ATTR_CURRENTTIER14BLOCK13SUMMATIONRECEIVED = 0x09ED;
        public const ushort ATTR_CURRENTTIER14BLOCK14SUMMATIONRECEIVED = 0x09EE;
        public const ushort ATTR_CURRENTTIER14BLOCK15SUMMATIONRECEIVED = 0x09EF;
        public const ushort ATTR_CURRENTTIER14BLOCK16SUMMATIONRECEIVED = 0x09F0;
        public const ushort ATTR_CURRENTTIER15BLOCK1SUMMATIONRECEIVED = 0x09F1;
        public const ushort ATTR_CURRENTTIER15BLOCK2SUMMATIONRECEIVED = 0x09F2;
        public const ushort ATTR_CURRENTTIER15BLOCK3SUMMATIONRECEIVED = 0x09F3;
        public const ushort ATTR_CURRENTTIER15BLOCK4SUMMATIONRECEIVED = 0x09F4;
        public const ushort ATTR_CURRENTTIER15BLOCK5SUMMATIONRECEIVED = 0x09F5;
        public const ushort ATTR_CURRENTTIER15BLOCK6SUMMATIONRECEIVED = 0x09F6;
        public const ushort ATTR_CURRENTTIER15BLOCK7SUMMATIONRECEIVED = 0x09F7;
        public const ushort ATTR_CURRENTTIER15BLOCK8SUMMATIONRECEIVED = 0x09F8;
        public const ushort ATTR_CURRENTTIER15BLOCK9SUMMATIONRECEIVED = 0x09F9;
        public const ushort ATTR_CURRENTTIER15BLOCK10SUMMATIONRECEIVED = 0x09FA;
        public const ushort ATTR_CURRENTTIER15BLOCK11SUMMATIONRECEIVED = 0x09FB;
        public const ushort ATTR_CURRENTTIER15BLOCK12SUMMATIONRECEIVED = 0x09FC;
        public const ushort ATTR_CURRENTTIER15BLOCK13SUMMATIONRECEIVED = 0x09FD;
        public const ushort ATTR_CURRENTTIER15BLOCK14SUMMATIONRECEIVED = 0x09FE;
        public const ushort ATTR_CURRENTTIER15BLOCK15SUMMATIONRECEIVED = 0x09FF;
        public const ushort ATTR_CURRENTTIER15BLOCK16SUMMATIONRECEIVED = 0x0A00;

        /// <summary>
        /// BillToDateDelivered provides a value for the costs in the current billing period.
        /// This attribute is measured in a base unit of Currency with the decimal point located
        /// as indicated by the BillDeliveredTrailingDigit attribute.
        /// </summary>
        public const ushort ATTR_BILLTODATEDELIVERED = 0x0A00;

        /// <summary>
        /// The UTC timestamp when the associated BillToDateDelivered attribute was last
        /// updated.
        /// </summary>
        public const ushort ATTR_BILLTODATETIMESTAMPDELIVERED = 0x0A01;

        /// <summary>
        /// ProjectedBillDelivered provides a value indicating what the estimated state of
        /// the account will be at the end of the billing period based on past consumption. This
        /// attribute is measured in a base unit of Currency with the decimal point located as
        /// indicated by the BillDeliveredTrailingDigit attribute.
        /// </summary>
        public const ushort ATTR_PROJECTEDBILLDELIVERED = 0x0A02;

        /// <summary>
        /// The UTC timestamp when the associated ProjectedBillDelivered attribute was last
        /// updated.
        /// </summary>
        public const ushort ATTR_PROJECTEDBILLTIMESTAMPDELIVERED = 0x0A03;

        /// <summary>
        /// An 8-bit BitMap used to determine where the decimal point is located in the
        /// BillToDateDelivered and ProjectedBillDelivered attributes. The most
        /// significant nibble indicates the number of digits to the right of the decimal
        /// point. The least significant nibble is reserved and shall be 0. The
        /// BillDeliveredTrailingDigit attribute represents the current active value.
        /// </summary>
        public const ushort ATTR_BILLDELIVEREDTRAILINGDIGIT = 0x0A04;

        /// <summary>
        /// BillToDateReceived provides a value for the costs in the current billing period.
        /// This attribute is measured in a base unit of Currency with the decimal point located
        /// as indicated by the BillReceivedTrailingDigit attribute.
        /// </summary>
        public const ushort ATTR_BILLTODATERECEIVED = 0x0A10;

        /// <summary>
        /// The UTC timestamp when the associated BillToDateReceived attribute was last
        /// updated.
        /// </summary>
        public const ushort ATTR_BILLTODATETIMESTAMPRECEIVED = 0x0A11;

        /// <summary>
        /// ProjectedBillReceived provides a value indicating what the estimated state of
        /// the account will be at the end of the billing period based on past generation. This
        /// attribute is measured in a base unit of Currency with the decimal point located as
        /// indicated by the BillReceivedTrailingDigit attribute.
        /// </summary>
        public const ushort ATTR_PROJECTEDBILLRECEIVED = 0x0A12;

        /// <summary>
        /// The UTC timestamp when the associated ProjectedBillReceived attribute was last
        /// updated.
        /// </summary>
        public const ushort ATTR_PROJECTEDBILLTIMESTAMPRECEIVED = 0x0A13;

        /// <summary>
        /// An 8-bit BitMap used to determine where the decimal point is located in the
        /// BillToDateReceived and ProjectedBillReceived attributes. The most
        /// significant nibble indicates the number of digits to the right of the decimal
        /// point. The least significant nibble is reserved and shall be 0. The
        /// BillReceivedTrailingDigit attribute represents the current active value
        /// </summary>
        public const ushort ATTR_BILLRECEIVEDTRAILINGDIGIT = 0x0A14;

        /// <summary>
        /// The ProposedChangeImplementationTime attribute indicates the time at which a
        /// proposed change to the supply is to be implemented. If there is no change of supply
        /// pending, this attribute will be set to 0xFFFFFFFF.
        /// </summary>
        public const ushort ATTR_PROPOSEDCHANGESUPPLYIMPLEMENTATIONTIME = 0x0B00;

        /// <summary>
        /// The ProposedChangeSupplyStatus indicates the proposed status of the supply once
        /// the change to the supply has be been implemented.
        /// </summary>
        public const ushort ATTR_PROPOSEDCHANGESUPPLYSTATUS = 0x0B01;

        /// <summary>
        /// The Uncontrolled Flow Threshold attribute indicates the threshold above which a
        /// flow meter (e.g. Gas or Water) shall detect an uncontrolled flow. A value of 0x0000
        /// indicates the feature in unused.
        /// </summary>
        public const ushort ATTR_UNCONTROLLEDFLOWTHRESHOLD = 0x0B10;

        /// <summary>
        /// The Uncontrolled Flow Threshold Unit of Measure attribute indicates the unit of
        /// measure used in conjunction with the Uncontrolled Flow Threshold attribute.
        /// </summary>
        public const ushort ATTR_UNCONTROLLEDFLOWTHRESHOLDUNITOFMEASURE = 0x0B11;

        /// <summary>
        /// The Uncontrolled Flow Multiplier attribute indicates the multiplier, to be used
        /// in conjunction with the Uncontrolled Flow Threshold and Uncontrolled Flow
        /// Divisor attributes, to determine the true flow threshold value. A value of 0x0000
        /// is not allowed.
        /// </summary>
        public const ushort ATTR_UNCONTROLLEDFLOWTHRESHOLDMULTIPLIER = 0x0B12;

        /// <summary>
        /// The Uncontrolled Flow Divisor attribute indicates the divisor, to be used in
        /// conjunction with the Uncontrolled Flow Threshold and Uncontrolled Flow
        /// Multiplier attributes, to determine the true flow threshold value. A value of
        /// 0x0000 is not allowed.
        /// </summary>
        public const ushort ATTR_UNCONTROLLEDFLOWTHRESHOLDDIVISOR = 0x0B13;

        /// <summary>
        /// The Flow Stabilisation Period attribute indicates the time given to allow the flow
        /// to stabilize. It is defined in units of tenths of a second.
        /// </summary>
        public const ushort ATTR_FLOWSTABILIZATIONPERIOD = 0x0B14;

        /// <summary>
        /// The Flow Measurement Period attribute indicates the period over which the flow is
        /// measured and compared against the Uncontrolled Flow Threshold attribute. It is
        /// defined in units of 1 second.
        /// </summary>
        public const ushort ATTR_FLOWMEASUREMENTPERIOD = 0x0B15;

        /// <summary>
        /// AlternativeInstantaneousDemand represents the current Demand delivered or
        /// received at the premises. Positive values indicate demand delivered to the
        /// premises where negative values indicate demand received from the premises.
        /// AlternativeInstantaneousDemand is updated continuously as new measurements
        /// are made. The frequency of updates to this field is specific to the metering device,
        /// but should be within the range of once every second to once every 5 seconds.
        /// </summary>
        public const ushort ATTR_ALTERNATIVEINSTANTANEOUSDEMAND = 0x0C00;

        /// <summary>
        /// CurrentDayAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises since the Historical Freeze Time (HFT). If optionally
        /// provided, CurrentDayAlternativeConsumptionDelivered is updated
        /// continuously as new measurements are made. If the optional HFT attribute is not
        /// available, default to midnight local time.
        /// </summary>
        public const ushort ATTR_CURRENTDAYALTERNATIVECONSUMPTIONDELIVERED = 0x0C01;

        /// <summary>
        /// CurrentDayAlternativeConsumptionReceived represents the summed value
        /// received from the premises since the Historical Freeze Time (HFT). If optionally
        /// provided, CurrentDayAlternativeConsumptionReceived is updated continuously
        /// as new measurements are made. If the optional HFT attribute is not available,
        /// default to midnight local time.
        /// </summary>
        public const ushort ATTR_CURRENTDAYALTERNATIVECONSUMPTIONRECEIVED = 0x0C02;

        /// <summary>
        /// PreviousDayAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous 24 hour period starting at the
        /// Alternative Historical Freeze Time (HFT). If optionally provided,
        /// PreviousDayAlternativeConsumptionDelivered is updated every HFT. If the
        /// optional HFT attribute is not available, default to midnight local time.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYALTERNATIVECONSUMPTIONDELIVERED = 0x0C03;

        /// <summary>
        /// PreviousDayAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If optionally provided,
        /// PreviousDayAlternativeConsumptionReceived is updated every HFT. If the
        /// optional HFT attribute is not available, default to midnight local time.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYALTERNATIVECONSUMPTIONRECEIVED = 0x0C04;

        /// <summary>
        /// CurrentAlternativePartialProfileIntervalStartTimeDelivered represents
        /// the start time of the current Load Profile interval being accumulated for
        /// commodity delivered.
        /// </summary>
        public const ushort ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALSTARTTIMEDELIVERED = 0x0C05;

        /// <summary>
        /// CurrentAlternativePartialProfileIntervalStartTimeReceived represents the
        /// start time of the current Load Profile interval being accumulated for commodity
        /// received.
        /// </summary>
        public const ushort ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALSTARTTIMERECEIVED = 0x0C06;

        /// <summary>
        /// CurrentAlternativePartialProfileIntervalValueDelivered represents the
        /// value of the current Load Profile interval being accumulated for commodity
        /// delivered.
        /// </summary>
        public const ushort ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALVALUEDELIVERED = 0x0C07;

        /// <summary>
        /// CurrentAlternativePartialProfileIntervalValueReceived represents the
        /// value of the current Load Profile interval being accumulated for commodity
        /// received.
        /// </summary>
        public const ushort ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALVALUERECEIVED = 0x0C08;

        /// <summary>
        /// CurrentDayAlternativeMaxPressure is the maximum pressure reported during a day
        /// from the water or gas meter.
        /// </summary>
        public const ushort ATTR_CURRENTDAYALTERNATIVEMAXPRESSURE = 0x0C09;

        /// <summary>
        /// CurrentDayAlternativeMinPressure is the minimum pressure reported during a day
        /// from the water or gas meter.
        /// </summary>
        public const ushort ATTR_CURRENTDAYALTERNATIVEMINPRESSURE = 0x0C0A;

        /// <summary>
        /// PreviousDayAlternativeMaxPressure represents the maximum pressure reported
        /// during previous day from the water or gas meter.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYALTERNATIVEMAXPRESSURE = 0x0C0B;

        /// <summary>
        /// PreviousDayAlternativeMinPressure represents the minimum pressure reported
        /// during previous day from the water or gas meter.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYALTERNATIVEMINPRESSURE = 0x0C0C;

        /// <summary>
        /// CurrentDayAlternativeMaxDemand represents the maximum demand or rate of
        /// delivered value of Energy, Gas, or Water being utilized at the premises.
        /// </summary>
        public const ushort ATTR_CURRENTDAYALTERNATIVEMAXDEMAND = 0x0C0D;

        /// <summary>
        /// PreviousDayAlternativeMaxDemand represents the maximum demand or rate of
        /// delivered value of Energy, Gas, or Water being utilized at the premises.
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYALTERNATIVEMAXDEMAND = 0x0C0E;

        /// <summary>
        /// CurrentMonthAlternativeMaxDemand is the maximum demand reported during a month
        /// from the meter.
        /// </summary>
        public const ushort ATTR_CURRENTMONTHALTERNATIVEMAXDEMAND = 0x0C0F;

        /// <summary>
        /// CurrentYearAlternativeMaxDemand is the maximum demand reported during a year
        /// from the meter.
        /// </summary>
        public const ushort ATTR_CURRENTYEARALTERNATIVEMAXDEMAND = 0x0C10;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY2ALTERNATIVECONSUMPTIONDELIVERED = 0x0C22;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY4ALTERNATIVECONSUMPTIONDELIVERED = 0x0C24;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY6ALTERNATIVECONSUMPTIONDELIVERED = 0x0C26;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY8ALTERNATIVECONSUMPTIONDELIVERED = 0x0C28;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY10ALTERNATIVECONSUMPTIONDELIVERED = 0x0C2A;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY12ALTERNATIVECONSUMPTIONDELIVERED = 0x0C2C;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY2ALTERNATIVECONSUMPTIONRECEIVED = 0x0C23;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY4ALTERNATIVECONSUMPTIONRECEIVED = 0x0C25;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY6ALTERNATIVECONSUMPTIONRECEIVED = 0x0C27;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY8ALTERNATIVECONSUMPTIONRECEIVED = 0x0C29;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY10ALTERNATIVECONSUMPTIONRECEIVED = 0x0C2B;

        /// <summary>
        /// PreviousDayNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous 24 hour period starting at the
        /// Historical Freeze Time (HFT). If the optional HFT attribute is not available,
        /// default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSDAY12ALTERNATIVECONSUMPTIONRECEIVED = 0x0C2D;

        /// <summary>
        /// CurrentWeekAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises since the Historical Freeze Time (HFT) on Monday to the
        /// last HFT read. If optionally provided,
        /// CurrentWeekAlternativeConsumptionDelivered is updated continuously as new
        /// measurements are made. If the optional HFT attribute is not available, default to
        /// midnight local time.
        /// </summary>
        public const ushort ATTR_CURRENTWEEKALTERNATIVECONSUMPTIONDELIVERED = 0x0C30;

        /// <summary>
        /// CurrentWeekAlternativeConsumptionReceived represents the summed value
        /// received from the premises since the Historical Freeze Time (HFT) on Monday to the
        /// last HFT read. If optionally provided,
        /// CurrentWeekAlternativeConsumptionReceived is updated continuously as new
        /// measurements are made. If the optional HFT attribute is not available, default to
        /// midnight local time.
        /// </summary>
        public const ushort ATTR_CURRENTWEEKALTERNATIVECONSUMPTIONRECEIVED = 0x0C31;

        /// <summary>
        /// PreviousWeekNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK1ALTERNATIVECONSUMPTIONDELIVERED = 0x0C33;

        /// <summary>
        /// PreviousWeekNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK3ALTERNATIVECONSUMPTIONDELIVERED = 0x0C35;

        /// <summary>
        /// PreviousWeekNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK5ALTERNATIVECONSUMPTIONDELIVERED = 0x0C37;

        /// <summary>
        /// PreviousWeekNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK7ALTERNATIVECONSUMPTIONDELIVERED = 0x0C39;

        /// <summary>
        /// PreviousWeekNAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK9ALTERNATIVECONSUMPTIONDELIVERED = 0x0C3B;

        /// <summary>
        /// PreviousWeekNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK1ALTERNATIVECONSUMPTIONRECEIVED = 0x0C34;

        /// <summary>
        /// PreviousWeekNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK3ALTERNATIVECONSUMPTIONRECEIVED = 0x0C36;

        /// <summary>
        /// PreviousWeekNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK5ALTERNATIVECONSUMPTIONRECEIVED = 0x0C38;

        /// <summary>
        /// PreviousWeekNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK7ALTERNATIVECONSUMPTIONRECEIVED = 0x0C3A;

        /// <summary>
        /// PreviousWeekNAlternativeConsumptionReceived represents the summed value
        /// received from the premises within the previous week period starting at the
        /// Historical Freeze Time (HFT) on the Monday to the Sunday. If the optional HFT
        /// attribute is not available, default to midnight local time.
     /// </summary>
        public const ushort ATTR_PREVIOUSWEEK9ALTERNATIVECONSUMPTIONRECEIVED = 0x0C3C;

        /// <summary>
        /// CurrentMonthAlternativeConsumptionDelivered represents the summed value
        /// delivered to the premises since the Historical Freeze Time (HFT) on the 1st of the
        /// month to the last HFT read. If optionally provided,
        /// CurrentMonthAlternativeConsumptionDelivered is updated continuously as new
        /// measurements are made. If the optional HFT attribute is not available, default to
        /// midnight local time.
        /// </summary>
        public const ushort ATTR_CURRENTMONTHALTERNATIVECONSUMPTIONDELIVERED = 0x0C40;

        /// <summary>
        /// CurrentMonthAlternativeConsumptionReceived represents the summed value
        /// received from the premises since the Historical Freeze Time (HFT) on the 1st of the
        /// month to the last HFT read. If optionally provided,
        /// CurrentMonthAlternativeConsumptionReceived is updated continuously as new
        /// measurements are made. If the optional HFT attribute is not available, default to
        /// midnight local time.
        /// </summary>
        public const ushort ATTR_CURRENTMONTHALTERNATIVECONSUMPTIONRECEIVED = 0x0C41;
        public const ushort ATTR_PREVIOUSMONTH1ALTERNATIVECONSUMPTIONDELIVERED = 0x0C43;
        public const ushort ATTR_PREVIOUSMONTH3ALTERNATIVECONSUMPTIONDELIVERED = 0x0C45;
        public const ushort ATTR_PREVIOUSMONTH5ALTERNATIVECONSUMPTIONDELIVERED = 0x0C47;
        public const ushort ATTR_PREVIOUSMONTH7ALTERNATIVECONSUMPTIONDELIVERED = 0x0C49;
        public const ushort ATTR_PREVIOUSMONTH9ALTERNATIVECONSUMPTIONDELIVERED = 0x0C4B;
        public const ushort ATTR_PREVIOUSMONTH11ALTERNATIVECONSUMPTIONDELIVERED = 0x0C4D;
        public const ushort ATTR_PREVIOUSMONTH13ALTERNATIVECONSUMPTIONDELIVERED = 0x0C4F;
        public const ushort ATTR_PREVIOUSMONTH15ALTERNATIVECONSUMPTIONDELIVERED = 0x0C51;
        public const ushort ATTR_PREVIOUSMONTH17ALTERNATIVECONSUMPTIONDELIVERED = 0x0C53;
        public const ushort ATTR_PREVIOUSMONTH19ALTERNATIVECONSUMPTIONDELIVERED = 0x0C55;
        public const ushort ATTR_PREVIOUSMONTH21ALTERNATIVECONSUMPTIONDELIVERED = 0x0C57;
        public const ushort ATTR_PREVIOUSMONTH23ALTERNATIVECONSUMPTIONDELIVERED = 0x0C59;
        public const ushort ATTR_PREVIOUSMONTH25ALTERNATIVECONSUMPTIONDELIVERED = 0x0C5B;
        public const ushort ATTR_PREVIOUSMONTH1ALTERNATIVECONSUMPTIONRECEIVED = 0x0C44;
        public const ushort ATTR_PREVIOUSMONTH3ALTERNATIVECONSUMPTIONRECEIVED = 0x0C46;
        public const ushort ATTR_PREVIOUSMONTH5ALTERNATIVECONSUMPTIONRECEIVED = 0x0C48;
        public const ushort ATTR_PREVIOUSMONTH7ALTERNATIVECONSUMPTIONRECEIVED = 0x0C4A;
        public const ushort ATTR_PREVIOUSMONTH9ALTERNATIVECONSUMPTIONRECEIVED = 0x0C4C;
        public const ushort ATTR_PREVIOUSMONTH11ALTERNATIVECONSUMPTIONRECEIVED = 0x0C4E;
        public const ushort ATTR_PREVIOUSMONTH13ALTERNATIVECONSUMPTIONRECEIVED = 0x0C50;
        public const ushort ATTR_PREVIOUSMONTH15ALTERNATIVECONSUMPTIONRECEIVED = 0x0C52;
        public const ushort ATTR_PREVIOUSMONTH17ALTERNATIVECONSUMPTIONRECEIVED = 0x0C54;
        public const ushort ATTR_PREVIOUSMONTH19ALTERNATIVECONSUMPTIONRECEIVED = 0x0C56;
        public const ushort ATTR_PREVIOUSMONTH21ALTERNATIVECONSUMPTIONRECEIVED = 0x0C58;
        public const ushort ATTR_PREVIOUSMONTH23ALTERNATIVECONSUMPTIONRECEIVED = 0x0C5A;
        public const ushort ATTR_PREVIOUSMONTH25ALTERNATIVECONSUMPTIONRECEIVED = 0x0C5C;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(2);

            attributeMap.Add(ATTR_FUNCTIONALNOTIFICATIONFLAGS, new ZclAttribute(this, ATTR_FUNCTIONALNOTIFICATIONFLAGS, "Functional Notification Flags", ZclDataType.Get(DataType.BITMAP_32_BIT), true, true, false, false));
            attributeMap.Add(ATTR_NOTIFICATIONFLAGS2, new ZclAttribute(this, ATTR_NOTIFICATIONFLAGS2, "Notification Flags 2", ZclDataType.Get(DataType.BITMAP_32_BIT), true, true, false, false));
            attributeMap.Add(ATTR_NOTIFICATIONFLAGS3, new ZclAttribute(this, ATTR_NOTIFICATIONFLAGS3, "Notification Flags 3", ZclDataType.Get(DataType.BITMAP_32_BIT), true, true, false, false));
            attributeMap.Add(ATTR_NOTIFICATIONFLAGS4, new ZclAttribute(this, ATTR_NOTIFICATIONFLAGS4, "Notification Flags 4", ZclDataType.Get(DataType.BITMAP_32_BIT), true, true, false, false));
            attributeMap.Add(ATTR_NOTIFICATIONFLAGS5, new ZclAttribute(this, ATTR_NOTIFICATIONFLAGS5, "Notification Flags 5", ZclDataType.Get(DataType.BITMAP_32_BIT), true, true, false, false));
            attributeMap.Add(ATTR_NOTIFICATIONFLAGS6, new ZclAttribute(this, ATTR_NOTIFICATIONFLAGS6, "Notification Flags 6", ZclDataType.Get(DataType.BITMAP_32_BIT), true, true, false, false));
            attributeMap.Add(ATTR_NOTIFICATIONFLAGS7, new ZclAttribute(this, ATTR_NOTIFICATIONFLAGS7, "Notification Flags 7", ZclDataType.Get(DataType.BITMAP_32_BIT), true, true, false, false));
            attributeMap.Add(ATTR_NOTIFICATIONFLAGS8, new ZclAttribute(this, ATTR_NOTIFICATIONFLAGS8, "Notification Flags 8", ZclDataType.Get(DataType.BITMAP_32_BIT), true, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(198);

            attributeMap.Add(ATTR_CURRENTSUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTSUMMATIONDELIVERED, "Current Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTSUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTSUMMATIONRECEIVED, "Current Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTMAXDEMANDDELIVERED, new ZclAttribute(this, ATTR_CURRENTMAXDEMANDDELIVERED, "Current Max Demand Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTMAXDEMANDRECEIVED, new ZclAttribute(this, ATTR_CURRENTMAXDEMANDRECEIVED, "Current Max Demand Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_DFTSUMMATION, new ZclAttribute(this, ATTR_DFTSUMMATION, "Dft Summation", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_DAILYFREEZETIME, new ZclAttribute(this, ATTR_DAILYFREEZETIME, "Daily Freeze Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_POWERFACTOR, new ZclAttribute(this, ATTR_POWERFACTOR, "Power Factor", ZclDataType.Get(DataType.SIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_READINGSNAPSHOTTIME, new ZclAttribute(this, ATTR_READINGSNAPSHOTTIME, "Reading Snapshot Time", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMAXDEMANDDELIVEREDTIME, new ZclAttribute(this, ATTR_CURRENTMAXDEMANDDELIVEREDTIME, "Current Max Demand Delivered Time", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMAXDEMANDRECEIVEDTIME, new ZclAttribute(this, ATTR_CURRENTMAXDEMANDRECEIVEDTIME, "Current Max Demand Received Time", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_DEFAULTUPDATEPERIOD, new ZclAttribute(this, ATTR_DEFAULTUPDATEPERIOD, "Default Update Period", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_FASTPOLLUPDATEPERIOD, new ZclAttribute(this, ATTR_FASTPOLLUPDATEPERIOD, "Fast Poll Update Period", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTBLOCKPERIODCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTBLOCKPERIODCONSUMPTIONDELIVERED, "Current Block Period Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_DAILYCONSUMPTIONTARGET, new ZclAttribute(this, ATTR_DAILYCONSUMPTIONTARGET, "Daily Consumption Target", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTBLOCK, new ZclAttribute(this, ATTR_CURRENTBLOCK, "Current Block", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_PROFILEINTERVALPERIOD, new ZclAttribute(this, ATTR_PROFILEINTERVALPERIOD, "Profile Interval Period", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_INTERVALREADREPORTINGPERIOD, new ZclAttribute(this, ATTR_INTERVALREADREPORTINGPERIOD, "Interval Read Reporting Period", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRESETREADINGTIME, new ZclAttribute(this, ATTR_PRESETREADINGTIME, "Preset Reading Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_VOLUMEPERREPORT, new ZclAttribute(this, ATTR_VOLUMEPERREPORT, "Volume Per Report", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_FLOWRESTRICTION, new ZclAttribute(this, ATTR_FLOWRESTRICTION, "Flow Restriction", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_SUPPLYSTATUS, new ZclAttribute(this, ATTR_SUPPLYSTATUS, "Supply Status", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTINLETENERGYCARRIERSUMMATION, new ZclAttribute(this, ATTR_CURRENTINLETENERGYCARRIERSUMMATION, "Current Inlet Energy Carrier Summation", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTOUTLETENERGYCARRIERSUMMATION, new ZclAttribute(this, ATTR_CURRENTOUTLETENERGYCARRIERSUMMATION, "Current Outlet Energy Carrier Summation", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_INLETTEMPERATURE, new ZclAttribute(this, ATTR_INLETTEMPERATURE, "Inlet Temperature", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_OUTLETTEMPERATURE, new ZclAttribute(this, ATTR_OUTLETTEMPERATURE, "Outlet Temperature", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CONTROLTEMPERATURE, new ZclAttribute(this, ATTR_CONTROLTEMPERATURE, "Control Temperature", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTINLETENERGYCARRIERDEMAND, new ZclAttribute(this, ATTR_CURRENTINLETENERGYCARRIERDEMAND, "Current Inlet Energy Carrier Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTOUTLETENERGYCARRIERDEMAND, new ZclAttribute(this, ATTR_CURRENTOUTLETENERGYCARRIERDEMAND, "Current Outlet Energy Carrier Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSBLOCKPERIODCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSBLOCKPERIODCONSUMPTIONDELIVERED, "Previous Block Period Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTBLOCKPERIODCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTBLOCKPERIODCONSUMPTIONRECEIVED, "Current Block Period Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTBLOCKRECEIVED, new ZclAttribute(this, ATTR_CURRENTBLOCKRECEIVED, "Current Block Received", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_DFTSUMMATIONRECEIVED, new ZclAttribute(this, ATTR_DFTSUMMATIONRECEIVED, "Dft Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_ACTIVEREGISTERTIERDELIVERED, new ZclAttribute(this, ATTR_ACTIVEREGISTERTIERDELIVERED, "Active Register Tier Delivered", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_ACTIVEREGISTERTIERRECEIVED, new ZclAttribute(this, ATTR_ACTIVEREGISTERTIERRECEIVED, "Active Register Tier Received", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_LASTBLOCKSWITCHTIME, new ZclAttribute(this, ATTR_LASTBLOCKSWITCHTIME, "Last Block Switch Time", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1SUMMATIONDELIVERED, "Current Tier 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3SUMMATIONDELIVERED, "Current Tier 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5SUMMATIONDELIVERED, "Current Tier 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7SUMMATIONDELIVERED, "Current Tier 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9SUMMATIONDELIVERED, "Current Tier 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11SUMMATIONDELIVERED, "Current Tier 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13SUMMATIONDELIVERED, "Current Tier 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15SUMMATIONDELIVERED, "Current Tier 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER17SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER17SUMMATIONDELIVERED, "Current Tier 17 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER19SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER19SUMMATIONDELIVERED, "Current Tier 19 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER21SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER21SUMMATIONDELIVERED, "Current Tier 21 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER23SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER23SUMMATIONDELIVERED, "Current Tier 23 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER25SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER25SUMMATIONDELIVERED, "Current Tier 25 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER27SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER27SUMMATIONDELIVERED, "Current Tier 27 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER29SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER29SUMMATIONDELIVERED, "Current Tier 29 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER31SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER31SUMMATIONDELIVERED, "Current Tier 31 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER33SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER33SUMMATIONDELIVERED, "Current Tier 33 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER35SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER35SUMMATIONDELIVERED, "Current Tier 35 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER37SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER37SUMMATIONDELIVERED, "Current Tier 37 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER39SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER39SUMMATIONDELIVERED, "Current Tier 39 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER41SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER41SUMMATIONDELIVERED, "Current Tier 41 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER43SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER43SUMMATIONDELIVERED, "Current Tier 43 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER45SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER45SUMMATIONDELIVERED, "Current Tier 45 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER47SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER47SUMMATIONDELIVERED, "Current Tier 47 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER49SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER49SUMMATIONDELIVERED, "Current Tier 49 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER51SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER51SUMMATIONDELIVERED, "Current Tier 51 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER53SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER53SUMMATIONDELIVERED, "Current Tier 53 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER55SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER55SUMMATIONDELIVERED, "Current Tier 55 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER57SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER57SUMMATIONDELIVERED, "Current Tier 57 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER59SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER59SUMMATIONDELIVERED, "Current Tier 59 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER61SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER61SUMMATIONDELIVERED, "Current Tier 61 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER63SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER63SUMMATIONDELIVERED, "Current Tier 63 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER65SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER65SUMMATIONDELIVERED, "Current Tier 65 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER67SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER67SUMMATIONDELIVERED, "Current Tier 67 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER69SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER69SUMMATIONDELIVERED, "Current Tier 69 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER71SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER71SUMMATIONDELIVERED, "Current Tier 71 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER73SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER73SUMMATIONDELIVERED, "Current Tier 73 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER75SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER75SUMMATIONDELIVERED, "Current Tier 75 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER77SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER77SUMMATIONDELIVERED, "Current Tier 77 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER79SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER79SUMMATIONDELIVERED, "Current Tier 79 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER81SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER81SUMMATIONDELIVERED, "Current Tier 81 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER83SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER83SUMMATIONDELIVERED, "Current Tier 83 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER85SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER85SUMMATIONDELIVERED, "Current Tier 85 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER87SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER87SUMMATIONDELIVERED, "Current Tier 87 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER89SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER89SUMMATIONDELIVERED, "Current Tier 89 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER91SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER91SUMMATIONDELIVERED, "Current Tier 91 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER93SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER93SUMMATIONDELIVERED, "Current Tier 93 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER95SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER95SUMMATIONDELIVERED, "Current Tier 95 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1SUMMATIONRECEIVED, "Current Tier 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3SUMMATIONRECEIVED, "Current Tier 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5SUMMATIONRECEIVED, "Current Tier 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7SUMMATIONRECEIVED, "Current Tier 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9SUMMATIONRECEIVED, "Current Tier 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11SUMMATIONRECEIVED, "Current Tier 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13SUMMATIONRECEIVED, "Current Tier 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15SUMMATIONRECEIVED, "Current Tier 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER17SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER17SUMMATIONRECEIVED, "Current Tier 17 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER19SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER19SUMMATIONRECEIVED, "Current Tier 19 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER21SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER21SUMMATIONRECEIVED, "Current Tier 21 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER23SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER23SUMMATIONRECEIVED, "Current Tier 23 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER25SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER25SUMMATIONRECEIVED, "Current Tier 25 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER27SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER27SUMMATIONRECEIVED, "Current Tier 27 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER29SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER29SUMMATIONRECEIVED, "Current Tier 29 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER31SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER31SUMMATIONRECEIVED, "Current Tier 31 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER33SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER33SUMMATIONRECEIVED, "Current Tier 33 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER35SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER35SUMMATIONRECEIVED, "Current Tier 35 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER37SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER37SUMMATIONRECEIVED, "Current Tier 37 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER39SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER39SUMMATIONRECEIVED, "Current Tier 39 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER41SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER41SUMMATIONRECEIVED, "Current Tier 41 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER43SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER43SUMMATIONRECEIVED, "Current Tier 43 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER45SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER45SUMMATIONRECEIVED, "Current Tier 45 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER47SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER47SUMMATIONRECEIVED, "Current Tier 47 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER49SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER49SUMMATIONRECEIVED, "Current Tier 49 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER51SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER51SUMMATIONRECEIVED, "Current Tier 51 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER53SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER53SUMMATIONRECEIVED, "Current Tier 53 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER55SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER55SUMMATIONRECEIVED, "Current Tier 55 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER57SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER57SUMMATIONRECEIVED, "Current Tier 57 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER59SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER59SUMMATIONRECEIVED, "Current Tier 59 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER61SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER61SUMMATIONRECEIVED, "Current Tier 61 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER63SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER63SUMMATIONRECEIVED, "Current Tier 63 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER65SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER65SUMMATIONRECEIVED, "Current Tier 65 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER67SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER67SUMMATIONRECEIVED, "Current Tier 67 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER69SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER69SUMMATIONRECEIVED, "Current Tier 69 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER71SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER71SUMMATIONRECEIVED, "Current Tier 71 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER73SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER73SUMMATIONRECEIVED, "Current Tier 73 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER75SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER75SUMMATIONRECEIVED, "Current Tier 75 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER77SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER77SUMMATIONRECEIVED, "Current Tier 77 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER79SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER79SUMMATIONRECEIVED, "Current Tier 79 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER81SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER81SUMMATIONRECEIVED, "Current Tier 81 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER83SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER83SUMMATIONRECEIVED, "Current Tier 83 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER85SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER85SUMMATIONRECEIVED, "Current Tier 85 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER87SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER87SUMMATIONRECEIVED, "Current Tier 87 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER89SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER89SUMMATIONRECEIVED, "Current Tier 89 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER91SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER91SUMMATIONRECEIVED, "Current Tier 91 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER93SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER93SUMMATIONRECEIVED, "Current Tier 93 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER95SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER95SUMMATIONRECEIVED, "Current Tier 95 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CPP1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CPP1SUMMATIONDELIVERED, "CPP 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CPP2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CPP2SUMMATIONDELIVERED, "CPP 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_STATUS, new ZclAttribute(this, ATTR_STATUS, "Status", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_REMAININGBATTERYLIFE, new ZclAttribute(this, ATTR_REMAININGBATTERYLIFE, "Remaining Battery Life", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_HOURSINOPERATION, new ZclAttribute(this, ATTR_HOURSINOPERATION, "Hours In Operation", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_HOURSINFAULT, new ZclAttribute(this, ATTR_HOURSINFAULT, "Hours In Fault", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_EXTENDEDSTATUS, new ZclAttribute(this, ATTR_EXTENDEDSTATUS, "Extended Status", ZclDataType.Get(DataType.BITMAP_64_BIT), true, true, false, false));
            attributeMap.Add(ATTR_REMAININGBATTERYLIFEINDAYS, new ZclAttribute(this, ATTR_REMAININGBATTERYLIFEINDAYS, "Remaining Battery Life In Days", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMETERID, new ZclAttribute(this, ATTR_CURRENTMETERID, "Current Meter ID", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_AMBIENTCONSUMPTIONINDICATOR, new ZclAttribute(this, ATTR_AMBIENTCONSUMPTIONINDICATOR, "Ambient Consumption Indicator", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_UNITOFMEASURE, new ZclAttribute(this, ATTR_UNITOFMEASURE, "Unit Of Measure", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_MULTIPLIER, new ZclAttribute(this, ATTR_MULTIPLIER, "Multiplier", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DIVISOR, new ZclAttribute(this, ATTR_DIVISOR, "Divisor", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_SUMMATIONFORMATTING, new ZclAttribute(this, ATTR_SUMMATIONFORMATTING, "Summation Formatting", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_DEMANDFORMATTING, new ZclAttribute(this, ATTR_DEMANDFORMATTING, "Demand Formatting", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_HISTORICALCONSUMPTIONFORMATTING, new ZclAttribute(this, ATTR_HISTORICALCONSUMPTIONFORMATTING, "Historical Consumption Formatting", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_METERINGDEVICETYPE, new ZclAttribute(this, ATTR_METERINGDEVICETYPE, "Metering Device Type", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_SITEID, new ZclAttribute(this, ATTR_SITEID, "Site ID", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_METERSERIALNUMBER, new ZclAttribute(this, ATTR_METERSERIALNUMBER, "Meter Serial Number", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_ENERGYCARRIERUNITOFMEASURE, new ZclAttribute(this, ATTR_ENERGYCARRIERUNITOFMEASURE, "Energy Carrier Unit Of Measure", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_ENERGYCARRIERSUMMATIONFORMATTING, new ZclAttribute(this, ATTR_ENERGYCARRIERSUMMATIONFORMATTING, "Energy Carrier Summation Formatting", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_ENERGYCARRIERDEMANDFORMATTING, new ZclAttribute(this, ATTR_ENERGYCARRIERDEMANDFORMATTING, "Energy Carrier Demand Formatting", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_TEMPERATUREUNITOFMEASURE, new ZclAttribute(this, ATTR_TEMPERATUREUNITOFMEASURE, "Temperature Unit Of Measure", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_TEMPERATUREFORMATTING, new ZclAttribute(this, ATTR_TEMPERATUREFORMATTING, "Temperature Formatting", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_MODULESERIALNUMBER, new ZclAttribute(this, ATTR_MODULESERIALNUMBER, "Module Serial Number", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_OPERATINGTARIFFLABELDELIVERED, new ZclAttribute(this, ATTR_OPERATINGTARIFFLABELDELIVERED, "Operating Tariff Label Delivered", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_OPERATINGTARIFFLABELRECEIVED, new ZclAttribute(this, ATTR_OPERATINGTARIFFLABELRECEIVED, "Operating Tariff Label Received", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_CUSTOMERIDNUMBER, new ZclAttribute(this, ATTR_CUSTOMERIDNUMBER, "Customer ID Number", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_ALTERNATIVEUNITOFMEASURE, new ZclAttribute(this, ATTR_ALTERNATIVEUNITOFMEASURE, "Alternative Unit Of Measure", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_ALTERNATIVEDEMANDFORMATTING, new ZclAttribute(this, ATTR_ALTERNATIVEDEMANDFORMATTING, "Alternative Demand Formatting", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_ALTERNATIVECONSUMPTIONFORMATTING, new ZclAttribute(this, ATTR_ALTERNATIVECONSUMPTIONFORMATTING, "Alternative Consumption Formatting", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_INSTANTANEOUSDEMAND, new ZclAttribute(this, ATTR_INSTANTANEOUSDEMAND, "Instantaneous Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTDAYCONSUMPTIONDELIVERED, "Current Day Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTDAYCONSUMPTIONRECEIVED, "Current Day Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAYCONSUMPTIONDELIVERED, "Previous Day Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAYCONSUMPTIONRECEIVED, "Previous Day Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTPARTIALPROFILEINTERVALSTARTTIMEDELIVERED, new ZclAttribute(this, ATTR_CURRENTPARTIALPROFILEINTERVALSTARTTIMEDELIVERED, "Current Partial Profile Interval Start Time Delivered", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTPARTIALPROFILEINTERVALSTARTTIMERECEIVED, new ZclAttribute(this, ATTR_CURRENTPARTIALPROFILEINTERVALSTARTTIMERECEIVED, "Current Partial Profile Interval Start Time Received", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTPARTIALPROFILEINTERVALVALUEDELIVERED, new ZclAttribute(this, ATTR_CURRENTPARTIALPROFILEINTERVALVALUEDELIVERED, "Current Partial Profile Interval Value Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTPARTIALPROFILEINTERVALVALUERECEIVED, new ZclAttribute(this, ATTR_CURRENTPARTIALPROFILEINTERVALVALUERECEIVED, "Current Partial Profile Interval Value Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYMAXPRESSURE, new ZclAttribute(this, ATTR_CURRENTDAYMAXPRESSURE, "Current Day Max Pressure", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYMINPRESSURE, new ZclAttribute(this, ATTR_CURRENTDAYMINPRESSURE, "Current Day Min Pressure", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYMAXPRESSURE, new ZclAttribute(this, ATTR_PREVIOUSDAYMAXPRESSURE, "Previous Day Max Pressure", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYMINPRESSURE, new ZclAttribute(this, ATTR_PREVIOUSDAYMINPRESSURE, "Previous Day Min Pressure", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYMAXDEMAND, new ZclAttribute(this, ATTR_CURRENTDAYMAXDEMAND, "Current Day Max Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYMAXDEMAND, new ZclAttribute(this, ATTR_PREVIOUSDAYMAXDEMAND, "Previous Day Max Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMONTHMAXDEMAND, new ZclAttribute(this, ATTR_CURRENTMONTHMAXDEMAND, "Current Month Max Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTYEARMAXDEMAND, new ZclAttribute(this, ATTR_CURRENTYEARMAXDEMAND, "Current Year Max Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYMAXENERGYCARRIERDEMAND, new ZclAttribute(this, ATTR_CURRENTDAYMAXENERGYCARRIERDEMAND, "Current Day Max Energy Carrier Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYMAXENERGYCARRIERDEMAND, new ZclAttribute(this, ATTR_PREVIOUSDAYMAXENERGYCARRIERDEMAND, "Previous Day Max Energy Carrier Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMONTHMAXENERGYCARRIERDEMAND, new ZclAttribute(this, ATTR_CURRENTMONTHMAXENERGYCARRIERDEMAND, "Current Month Max Energy Carrier Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMONTHMINENERGYCARRIERDEMAND, new ZclAttribute(this, ATTR_CURRENTMONTHMINENERGYCARRIERDEMAND, "Current Month Min Energy Carrier Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTYEARMAXENERGYCARRIERDEMAND, new ZclAttribute(this, ATTR_CURRENTYEARMAXENERGYCARRIERDEMAND, "Current Year Max Energy Carrier Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTYEARMINENERGYCARRIERDEMAND, new ZclAttribute(this, ATTR_CURRENTYEARMINENERGYCARRIERDEMAND, "Current Year Min Energy Carrier Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY2CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY2CONSUMPTIONDELIVERED, "Previous Day 2 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY4CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY4CONSUMPTIONDELIVERED, "Previous Day 4 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY6CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY6CONSUMPTIONDELIVERED, "Previous Day 6 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY8CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY8CONSUMPTIONDELIVERED, "Previous Day 8 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY10CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY10CONSUMPTIONDELIVERED, "Previous Day 10 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY12CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY12CONSUMPTIONDELIVERED, "Previous Day 12 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY14CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY14CONSUMPTIONDELIVERED, "Previous Day 14 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY16CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY16CONSUMPTIONDELIVERED, "Previous Day 16 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY2CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY2CONSUMPTIONRECEIVED, "Previous Day 2 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY4CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY4CONSUMPTIONRECEIVED, "Previous Day 4 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY6CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY6CONSUMPTIONRECEIVED, "Previous Day 6 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY8CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY8CONSUMPTIONRECEIVED, "Previous Day 8 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY10CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY10CONSUMPTIONRECEIVED, "Previous Day 10 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY12CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY12CONSUMPTIONRECEIVED, "Previous Day 12 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY14CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY14CONSUMPTIONRECEIVED, "Previous Day 14 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY16CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY16CONSUMPTIONRECEIVED, "Previous Day 16 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTWEEKCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTWEEKCONSUMPTIONDELIVERED, "Current Week Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTWEEKCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTWEEKCONSUMPTIONRECEIVED, "Current Week Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK1CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK1CONSUMPTIONDELIVERED, "Previous Week 1 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK3CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK3CONSUMPTIONDELIVERED, "Previous Week 3 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK5CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK5CONSUMPTIONDELIVERED, "Previous Week 5 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK7CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK7CONSUMPTIONDELIVERED, "Previous Week 7 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK9CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK9CONSUMPTIONDELIVERED, "Previous Week 9 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK1CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK1CONSUMPTIONRECEIVED, "Previous Week 1 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK3CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK3CONSUMPTIONRECEIVED, "Previous Week 3 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK5CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK5CONSUMPTIONRECEIVED, "Previous Week 5 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK7CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK7CONSUMPTIONRECEIVED, "Previous Week 7 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK9CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK9CONSUMPTIONRECEIVED, "Previous Week 9 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMONTHCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTMONTHCONSUMPTIONDELIVERED, "Current Month Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMONTHCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTMONTHCONSUMPTIONRECEIVED, "Current Month Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH1CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH1CONSUMPTIONDELIVERED, "Previous Month 1 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH3CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH3CONSUMPTIONDELIVERED, "Previous Month 3 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH5CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH5CONSUMPTIONDELIVERED, "Previous Month 5 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH7CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH7CONSUMPTIONDELIVERED, "Previous Month 7 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH9CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH9CONSUMPTIONDELIVERED, "Previous Month 9 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH11CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH11CONSUMPTIONDELIVERED, "Previous Month 11 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH13CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH13CONSUMPTIONDELIVERED, "Previous Month 13 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH15CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH15CONSUMPTIONDELIVERED, "Previous Month 15 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH17CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH17CONSUMPTIONDELIVERED, "Previous Month 17 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH19CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH19CONSUMPTIONDELIVERED, "Previous Month 19 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH21CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH21CONSUMPTIONDELIVERED, "Previous Month 21 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH23CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH23CONSUMPTIONDELIVERED, "Previous Month 23 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH25CONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH25CONSUMPTIONDELIVERED, "Previous Month 25 Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH1CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH1CONSUMPTIONRECEIVED, "Previous Month 1 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH3CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH3CONSUMPTIONRECEIVED, "Previous Month 3 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH5CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH5CONSUMPTIONRECEIVED, "Previous Month 5 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH7CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH7CONSUMPTIONRECEIVED, "Previous Month 7 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH9CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH9CONSUMPTIONRECEIVED, "Previous Month 9 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH11CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH11CONSUMPTIONRECEIVED, "Previous Month 11 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH13CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH13CONSUMPTIONRECEIVED, "Previous Month 13 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH15CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH15CONSUMPTIONRECEIVED, "Previous Month 15 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH17CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH17CONSUMPTIONRECEIVED, "Previous Month 17 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH19CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH19CONSUMPTIONRECEIVED, "Previous Month 19 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH21CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH21CONSUMPTIONRECEIVED, "Previous Month 21 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH23CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH23CONSUMPTIONRECEIVED, "Previous Month 23 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH25CONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH25CONSUMPTIONRECEIVED, "Previous Month 25 Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_HISTORICALFREEZETIME, new ZclAttribute(this, ATTR_HISTORICALFREEZETIME, "Historical Freeze Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MAXNUMBEROFPERIODSDELIVERED, new ZclAttribute(this, ATTR_MAXNUMBEROFPERIODSDELIVERED, "Max Number Of Periods Delivered", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDEMANDDELIVERED, new ZclAttribute(this, ATTR_CURRENTDEMANDDELIVERED, "Current Demand Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEMANDLIMIT, new ZclAttribute(this, ATTR_DEMANDLIMIT, "Demand Limit", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEMANDINTEGRATIONPERIOD, new ZclAttribute(this, ATTR_DEMANDINTEGRATIONPERIOD, "Demand Integration Period", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NUMBEROFDEMANDSUBINTERVALS, new ZclAttribute(this, ATTR_NUMBEROFDEMANDSUBINTERVALS, "Number Of Demand Subintervals", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEMANDLIMITARMDURATION, new ZclAttribute(this, ATTR_DEMANDLIMITARMDURATION, "Demand Limit Arm Duration", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_LOADLIMITSUPPLYSTATE, new ZclAttribute(this, ATTR_LOADLIMITSUPPLYSTATE, "Load Limit Supply State", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_LOADLIMITCOUNTER, new ZclAttribute(this, ATTR_LOADLIMITCOUNTER, "Load Limit Counter", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_SUPPLYTAMPERSTATE, new ZclAttribute(this, ATTR_SUPPLYTAMPERSTATE, "Supply Tamper State", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_SUPPLYDEPLETIONSTATE, new ZclAttribute(this, ATTR_SUPPLYDEPLETIONSTATE, "Supply Depletion State", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_SUPPLYUNCONTROLLEDFLOWSTATE, new ZclAttribute(this, ATTR_SUPPLYUNCONTROLLEDFLOWSTATE, "Supply Uncontrolled Flow State", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK1SUMMATIONDELIVERED, "Current No Tier Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK2SUMMATIONDELIVERED, "Current No Tier Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK3SUMMATIONDELIVERED, "Current No Tier Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK4SUMMATIONDELIVERED, "Current No Tier Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK5SUMMATIONDELIVERED, "Current No Tier Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK6SUMMATIONDELIVERED, "Current No Tier Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK7SUMMATIONDELIVERED, "Current No Tier Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK8SUMMATIONDELIVERED, "Current No Tier Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK9SUMMATIONDELIVERED, "Current No Tier Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK10SUMMATIONDELIVERED, "Current No Tier Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK11SUMMATIONDELIVERED, "Current No Tier Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK12SUMMATIONDELIVERED, "Current No Tier Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK13SUMMATIONDELIVERED, "Current No Tier Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK14SUMMATIONDELIVERED, "Current No Tier Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK15SUMMATIONDELIVERED, "Current No Tier Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK16SUMMATIONDELIVERED, "Current No Tier Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK1SUMMATIONDELIVERED, "Current Tier 1 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK2SUMMATIONDELIVERED, "Current Tier 1 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK3SUMMATIONDELIVERED, "Current Tier 1 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK4SUMMATIONDELIVERED, "Current Tier 1 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK5SUMMATIONDELIVERED, "Current Tier 1 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK6SUMMATIONDELIVERED, "Current Tier 1 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK7SUMMATIONDELIVERED, "Current Tier 1 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK8SUMMATIONDELIVERED, "Current Tier 1 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK9SUMMATIONDELIVERED, "Current Tier 1 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK10SUMMATIONDELIVERED, "Current Tier 1 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK11SUMMATIONDELIVERED, "Current Tier 1 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK12SUMMATIONDELIVERED, "Current Tier 1 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK13SUMMATIONDELIVERED, "Current Tier 1 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK14SUMMATIONDELIVERED, "Current Tier 1 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK15SUMMATIONDELIVERED, "Current Tier 1 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK16SUMMATIONDELIVERED, "Current Tier 1 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK1SUMMATIONDELIVERED, "Current Tier 2 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK2SUMMATIONDELIVERED, "Current Tier 2 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK3SUMMATIONDELIVERED, "Current Tier 2 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK4SUMMATIONDELIVERED, "Current Tier 2 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK5SUMMATIONDELIVERED, "Current Tier 2 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK6SUMMATIONDELIVERED, "Current Tier 2 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK7SUMMATIONDELIVERED, "Current Tier 2 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK8SUMMATIONDELIVERED, "Current Tier 2 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK9SUMMATIONDELIVERED, "Current Tier 2 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK10SUMMATIONDELIVERED, "Current Tier 2 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK11SUMMATIONDELIVERED, "Current Tier 2 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK12SUMMATIONDELIVERED, "Current Tier 2 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK13SUMMATIONDELIVERED, "Current Tier 2 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK14SUMMATIONDELIVERED, "Current Tier 2 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK15SUMMATIONDELIVERED, "Current Tier 2 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK16SUMMATIONDELIVERED, "Current Tier 2 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK1SUMMATIONDELIVERED, "Current Tier 3 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK2SUMMATIONDELIVERED, "Current Tier 3 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK3SUMMATIONDELIVERED, "Current Tier 3 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK4SUMMATIONDELIVERED, "Current Tier 3 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK5SUMMATIONDELIVERED, "Current Tier 3 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK6SUMMATIONDELIVERED, "Current Tier 3 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK7SUMMATIONDELIVERED, "Current Tier 3 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK8SUMMATIONDELIVERED, "Current Tier 3 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK9SUMMATIONDELIVERED, "Current Tier 3 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK10SUMMATIONDELIVERED, "Current Tier 3 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK11SUMMATIONDELIVERED, "Current Tier 3 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK12SUMMATIONDELIVERED, "Current Tier 3 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK13SUMMATIONDELIVERED, "Current Tier 3 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK14SUMMATIONDELIVERED, "Current Tier 3 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK15SUMMATIONDELIVERED, "Current Tier 3 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK16SUMMATIONDELIVERED, "Current Tier 3 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK1SUMMATIONDELIVERED, "Current Tier 4 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK2SUMMATIONDELIVERED, "Current Tier 4 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK3SUMMATIONDELIVERED, "Current Tier 4 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK4SUMMATIONDELIVERED, "Current Tier 4 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK5SUMMATIONDELIVERED, "Current Tier 4 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK6SUMMATIONDELIVERED, "Current Tier 4 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK7SUMMATIONDELIVERED, "Current Tier 4 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK8SUMMATIONDELIVERED, "Current Tier 4 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK9SUMMATIONDELIVERED, "Current Tier 4 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK10SUMMATIONDELIVERED, "Current Tier 4 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK11SUMMATIONDELIVERED, "Current Tier 4 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK12SUMMATIONDELIVERED, "Current Tier 4 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK13SUMMATIONDELIVERED, "Current Tier 4 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK14SUMMATIONDELIVERED, "Current Tier 4 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK15SUMMATIONDELIVERED, "Current Tier 4 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK16SUMMATIONDELIVERED, "Current Tier 4 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK1SUMMATIONDELIVERED, "Current Tier 5 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK2SUMMATIONDELIVERED, "Current Tier 5 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK3SUMMATIONDELIVERED, "Current Tier 5 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK4SUMMATIONDELIVERED, "Current Tier 5 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK5SUMMATIONDELIVERED, "Current Tier 5 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK6SUMMATIONDELIVERED, "Current Tier 5 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK7SUMMATIONDELIVERED, "Current Tier 5 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK8SUMMATIONDELIVERED, "Current Tier 5 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK9SUMMATIONDELIVERED, "Current Tier 5 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK10SUMMATIONDELIVERED, "Current Tier 5 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK11SUMMATIONDELIVERED, "Current Tier 5 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK12SUMMATIONDELIVERED, "Current Tier 5 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK13SUMMATIONDELIVERED, "Current Tier 5 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK14SUMMATIONDELIVERED, "Current Tier 5 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK15SUMMATIONDELIVERED, "Current Tier 5 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK16SUMMATIONDELIVERED, "Current Tier 5 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK1SUMMATIONDELIVERED, "Current Tier 6 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK2SUMMATIONDELIVERED, "Current Tier 6 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK3SUMMATIONDELIVERED, "Current Tier 6 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK4SUMMATIONDELIVERED, "Current Tier 6 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK5SUMMATIONDELIVERED, "Current Tier 6 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK6SUMMATIONDELIVERED, "Current Tier 6 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK7SUMMATIONDELIVERED, "Current Tier 6 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK8SUMMATIONDELIVERED, "Current Tier 6 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK9SUMMATIONDELIVERED, "Current Tier 6 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK10SUMMATIONDELIVERED, "Current Tier 6 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK11SUMMATIONDELIVERED, "Current Tier 6 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK12SUMMATIONDELIVERED, "Current Tier 6 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK13SUMMATIONDELIVERED, "Current Tier 6 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK14SUMMATIONDELIVERED, "Current Tier 6 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK15SUMMATIONDELIVERED, "Current Tier 6 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK16SUMMATIONDELIVERED, "Current Tier 6 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK1SUMMATIONDELIVERED, "Current Tier 7 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK2SUMMATIONDELIVERED, "Current Tier 7 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK3SUMMATIONDELIVERED, "Current Tier 7 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK4SUMMATIONDELIVERED, "Current Tier 7 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK5SUMMATIONDELIVERED, "Current Tier 7 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK6SUMMATIONDELIVERED, "Current Tier 7 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK7SUMMATIONDELIVERED, "Current Tier 7 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK8SUMMATIONDELIVERED, "Current Tier 7 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK9SUMMATIONDELIVERED, "Current Tier 7 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK10SUMMATIONDELIVERED, "Current Tier 7 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK11SUMMATIONDELIVERED, "Current Tier 7 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK12SUMMATIONDELIVERED, "Current Tier 7 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK13SUMMATIONDELIVERED, "Current Tier 7 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK14SUMMATIONDELIVERED, "Current Tier 7 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK15SUMMATIONDELIVERED, "Current Tier 7 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK16SUMMATIONDELIVERED, "Current Tier 7 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK1SUMMATIONDELIVERED, "Current Tier 8 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK2SUMMATIONDELIVERED, "Current Tier 8 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK3SUMMATIONDELIVERED, "Current Tier 8 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK4SUMMATIONDELIVERED, "Current Tier 8 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK5SUMMATIONDELIVERED, "Current Tier 8 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK6SUMMATIONDELIVERED, "Current Tier 8 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK7SUMMATIONDELIVERED, "Current Tier 8 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK8SUMMATIONDELIVERED, "Current Tier 8 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK9SUMMATIONDELIVERED, "Current Tier 8 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK10SUMMATIONDELIVERED, "Current Tier 8 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK11SUMMATIONDELIVERED, "Current Tier 8 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK12SUMMATIONDELIVERED, "Current Tier 8 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK13SUMMATIONDELIVERED, "Current Tier 8 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK14SUMMATIONDELIVERED, "Current Tier 8 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK15SUMMATIONDELIVERED, "Current Tier 8 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK16SUMMATIONDELIVERED, "Current Tier 8 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK1SUMMATIONDELIVERED, "Current Tier 9 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK2SUMMATIONDELIVERED, "Current Tier 9 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK3SUMMATIONDELIVERED, "Current Tier 9 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK4SUMMATIONDELIVERED, "Current Tier 9 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK5SUMMATIONDELIVERED, "Current Tier 9 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK6SUMMATIONDELIVERED, "Current Tier 9 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK7SUMMATIONDELIVERED, "Current Tier 9 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK8SUMMATIONDELIVERED, "Current Tier 9 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK9SUMMATIONDELIVERED, "Current Tier 9 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK10SUMMATIONDELIVERED, "Current Tier 9 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK11SUMMATIONDELIVERED, "Current Tier 9 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK12SUMMATIONDELIVERED, "Current Tier 9 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK13SUMMATIONDELIVERED, "Current Tier 9 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK14SUMMATIONDELIVERED, "Current Tier 9 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK15SUMMATIONDELIVERED, "Current Tier 9 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK16SUMMATIONDELIVERED, "Current Tier 9 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK1SUMMATIONDELIVERED, "Current Tier 10 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK2SUMMATIONDELIVERED, "Current Tier 10 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK3SUMMATIONDELIVERED, "Current Tier 10 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK4SUMMATIONDELIVERED, "Current Tier 10 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK5SUMMATIONDELIVERED, "Current Tier 10 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK6SUMMATIONDELIVERED, "Current Tier 10 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK7SUMMATIONDELIVERED, "Current Tier 10 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK8SUMMATIONDELIVERED, "Current Tier 10 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK9SUMMATIONDELIVERED, "Current Tier 10 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK10SUMMATIONDELIVERED, "Current Tier 10 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK11SUMMATIONDELIVERED, "Current Tier 10 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK12SUMMATIONDELIVERED, "Current Tier 10 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK13SUMMATIONDELIVERED, "Current Tier 10 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK14SUMMATIONDELIVERED, "Current Tier 10 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK15SUMMATIONDELIVERED, "Current Tier 10 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK16SUMMATIONDELIVERED, "Current Tier 10 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK1SUMMATIONDELIVERED, "Current Tier 11 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK2SUMMATIONDELIVERED, "Current Tier 11 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK3SUMMATIONDELIVERED, "Current Tier 11 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK4SUMMATIONDELIVERED, "Current Tier 11 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK5SUMMATIONDELIVERED, "Current Tier 11 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK6SUMMATIONDELIVERED, "Current Tier 11 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK7SUMMATIONDELIVERED, "Current Tier 11 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK8SUMMATIONDELIVERED, "Current Tier 11 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK9SUMMATIONDELIVERED, "Current Tier 11 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK10SUMMATIONDELIVERED, "Current Tier 11 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK11SUMMATIONDELIVERED, "Current Tier 11 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK12SUMMATIONDELIVERED, "Current Tier 11 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK13SUMMATIONDELIVERED, "Current Tier 11 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK14SUMMATIONDELIVERED, "Current Tier 11 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK15SUMMATIONDELIVERED, "Current Tier 11 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK16SUMMATIONDELIVERED, "Current Tier 11 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK1SUMMATIONDELIVERED, "Current Tier 12 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK2SUMMATIONDELIVERED, "Current Tier 12 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK3SUMMATIONDELIVERED, "Current Tier 12 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK4SUMMATIONDELIVERED, "Current Tier 12 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK5SUMMATIONDELIVERED, "Current Tier 12 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK6SUMMATIONDELIVERED, "Current Tier 12 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK7SUMMATIONDELIVERED, "Current Tier 12 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK8SUMMATIONDELIVERED, "Current Tier 12 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK9SUMMATIONDELIVERED, "Current Tier 12 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK10SUMMATIONDELIVERED, "Current Tier 12 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK11SUMMATIONDELIVERED, "Current Tier 12 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK12SUMMATIONDELIVERED, "Current Tier 12 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK13SUMMATIONDELIVERED, "Current Tier 12 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK14SUMMATIONDELIVERED, "Current Tier 12 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK15SUMMATIONDELIVERED, "Current Tier 12 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK16SUMMATIONDELIVERED, "Current Tier 12 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK1SUMMATIONDELIVERED, "Current Tier 13 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK2SUMMATIONDELIVERED, "Current Tier 13 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK3SUMMATIONDELIVERED, "Current Tier 13 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK4SUMMATIONDELIVERED, "Current Tier 13 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK5SUMMATIONDELIVERED, "Current Tier 13 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK6SUMMATIONDELIVERED, "Current Tier 13 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK7SUMMATIONDELIVERED, "Current Tier 13 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK8SUMMATIONDELIVERED, "Current Tier 13 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK9SUMMATIONDELIVERED, "Current Tier 13 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK10SUMMATIONDELIVERED, "Current Tier 13 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK11SUMMATIONDELIVERED, "Current Tier 13 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK12SUMMATIONDELIVERED, "Current Tier 13 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK13SUMMATIONDELIVERED, "Current Tier 13 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK14SUMMATIONDELIVERED, "Current Tier 13 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK15SUMMATIONDELIVERED, "Current Tier 13 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK16SUMMATIONDELIVERED, "Current Tier 13 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK1SUMMATIONDELIVERED, "Current Tier 14 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK2SUMMATIONDELIVERED, "Current Tier 14 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK3SUMMATIONDELIVERED, "Current Tier 14 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK4SUMMATIONDELIVERED, "Current Tier 14 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK5SUMMATIONDELIVERED, "Current Tier 14 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK6SUMMATIONDELIVERED, "Current Tier 14 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK7SUMMATIONDELIVERED, "Current Tier 14 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK8SUMMATIONDELIVERED, "Current Tier 14 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK9SUMMATIONDELIVERED, "Current Tier 14 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK10SUMMATIONDELIVERED, "Current Tier 14 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK11SUMMATIONDELIVERED, "Current Tier 14 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK12SUMMATIONDELIVERED, "Current Tier 14 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK13SUMMATIONDELIVERED, "Current Tier 14 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK14SUMMATIONDELIVERED, "Current Tier 14 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK15SUMMATIONDELIVERED, "Current Tier 14 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK16SUMMATIONDELIVERED, "Current Tier 14 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK1SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK1SUMMATIONDELIVERED, "Current Tier 15 Block 1 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK2SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK2SUMMATIONDELIVERED, "Current Tier 15 Block 2 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK3SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK3SUMMATIONDELIVERED, "Current Tier 15 Block 3 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK4SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK4SUMMATIONDELIVERED, "Current Tier 15 Block 4 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK5SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK5SUMMATIONDELIVERED, "Current Tier 15 Block 5 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK6SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK6SUMMATIONDELIVERED, "Current Tier 15 Block 6 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK7SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK7SUMMATIONDELIVERED, "Current Tier 15 Block 7 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK8SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK8SUMMATIONDELIVERED, "Current Tier 15 Block 8 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK9SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK9SUMMATIONDELIVERED, "Current Tier 15 Block 9 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK10SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK10SUMMATIONDELIVERED, "Current Tier 15 Block 10 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK11SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK11SUMMATIONDELIVERED, "Current Tier 15 Block 11 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK12SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK12SUMMATIONDELIVERED, "Current Tier 15 Block 12 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK13SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK13SUMMATIONDELIVERED, "Current Tier 15 Block 13 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK14SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK14SUMMATIONDELIVERED, "Current Tier 15 Block 14 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK15SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK15SUMMATIONDELIVERED, "Current Tier 15 Block 15 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK16SUMMATIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK16SUMMATIONDELIVERED, "Current Tier 15 Block 16 Summation Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_GENERICALARMMASK, new ZclAttribute(this, ATTR_GENERICALARMMASK, "Generic Alarm Mask", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_ELECTRICITYALARMMASK, new ZclAttribute(this, ATTR_ELECTRICITYALARMMASK, "Electricity Alarm Mask", ZclDataType.Get(DataType.BITMAP_32_BIT), false, true, true, true));
            attributeMap.Add(ATTR_GENERICFLOWPRESSUREALARMMASK, new ZclAttribute(this, ATTR_GENERICFLOWPRESSUREALARMMASK, "Generic Flow /pressure Alarm Mask", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_WATERSPECIFICALARMMASK, new ZclAttribute(this, ATTR_WATERSPECIFICALARMMASK, "Water Specific Alarm Mask", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_HEATANDCOOLINGSPECIFICALARMMASK, new ZclAttribute(this, ATTR_HEATANDCOOLINGSPECIFICALARMMASK, "Heat And Cooling Specific Alarm Mask", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_GASSPECIFICALARMMASK, new ZclAttribute(this, ATTR_GASSPECIFICALARMMASK, "Gas Specific Alarm Mask", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_EXTENDEDGENERICALARMMASK, new ZclAttribute(this, ATTR_EXTENDEDGENERICALARMMASK, "Extended Generic Alarm Mask", ZclDataType.Get(DataType.BITMAP_48_BIT), false, true, true, true));
            attributeMap.Add(ATTR_MANUFACTUREALARMMASK, new ZclAttribute(this, ATTR_MANUFACTUREALARMMASK, "Manufacture Alarm Mask", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK1SUMMATIONRECEIVED, "Current No Tier Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK2SUMMATIONRECEIVED, "Current No Tier Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK3SUMMATIONRECEIVED, "Current No Tier Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK4SUMMATIONRECEIVED, "Current No Tier Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK5SUMMATIONRECEIVED, "Current No Tier Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK6SUMMATIONRECEIVED, "Current No Tier Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK7SUMMATIONRECEIVED, "Current No Tier Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK8SUMMATIONRECEIVED, "Current No Tier Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK9SUMMATIONRECEIVED, "Current No Tier Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK10SUMMATIONRECEIVED, "Current No Tier Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK11SUMMATIONRECEIVED, "Current No Tier Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK12SUMMATIONRECEIVED, "Current No Tier Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK13SUMMATIONRECEIVED, "Current No Tier Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK14SUMMATIONRECEIVED, "Current No Tier Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK15SUMMATIONRECEIVED, "Current No Tier Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTNOTIERBLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTNOTIERBLOCK16SUMMATIONRECEIVED, "Current No Tier Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK1SUMMATIONRECEIVED, "Current Tier 1 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK2SUMMATIONRECEIVED, "Current Tier 1 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK3SUMMATIONRECEIVED, "Current Tier 1 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK4SUMMATIONRECEIVED, "Current Tier 1 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK5SUMMATIONRECEIVED, "Current Tier 1 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK6SUMMATIONRECEIVED, "Current Tier 1 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK7SUMMATIONRECEIVED, "Current Tier 1 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK8SUMMATIONRECEIVED, "Current Tier 1 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK9SUMMATIONRECEIVED, "Current Tier 1 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK10SUMMATIONRECEIVED, "Current Tier 1 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK11SUMMATIONRECEIVED, "Current Tier 1 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK12SUMMATIONRECEIVED, "Current Tier 1 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK13SUMMATIONRECEIVED, "Current Tier 1 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK14SUMMATIONRECEIVED, "Current Tier 1 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK15SUMMATIONRECEIVED, "Current Tier 1 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER1BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER1BLOCK16SUMMATIONRECEIVED, "Current Tier 1 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK1SUMMATIONRECEIVED, "Current Tier 2 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK2SUMMATIONRECEIVED, "Current Tier 2 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK3SUMMATIONRECEIVED, "Current Tier 2 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK4SUMMATIONRECEIVED, "Current Tier 2 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK5SUMMATIONRECEIVED, "Current Tier 2 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK6SUMMATIONRECEIVED, "Current Tier 2 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK7SUMMATIONRECEIVED, "Current Tier 2 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK8SUMMATIONRECEIVED, "Current Tier 2 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK9SUMMATIONRECEIVED, "Current Tier 2 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK10SUMMATIONRECEIVED, "Current Tier 2 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK11SUMMATIONRECEIVED, "Current Tier 2 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK12SUMMATIONRECEIVED, "Current Tier 2 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK13SUMMATIONRECEIVED, "Current Tier 2 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK14SUMMATIONRECEIVED, "Current Tier 2 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK15SUMMATIONRECEIVED, "Current Tier 2 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER2BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER2BLOCK16SUMMATIONRECEIVED, "Current Tier 2 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK1SUMMATIONRECEIVED, "Current Tier 3 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK2SUMMATIONRECEIVED, "Current Tier 3 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK3SUMMATIONRECEIVED, "Current Tier 3 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK4SUMMATIONRECEIVED, "Current Tier 3 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK5SUMMATIONRECEIVED, "Current Tier 3 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK6SUMMATIONRECEIVED, "Current Tier 3 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK7SUMMATIONRECEIVED, "Current Tier 3 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK8SUMMATIONRECEIVED, "Current Tier 3 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK9SUMMATIONRECEIVED, "Current Tier 3 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK10SUMMATIONRECEIVED, "Current Tier 3 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK11SUMMATIONRECEIVED, "Current Tier 3 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK12SUMMATIONRECEIVED, "Current Tier 3 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK13SUMMATIONRECEIVED, "Current Tier 3 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK14SUMMATIONRECEIVED, "Current Tier 3 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK15SUMMATIONRECEIVED, "Current Tier 3 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER3BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER3BLOCK16SUMMATIONRECEIVED, "Current Tier 3 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK1SUMMATIONRECEIVED, "Current Tier 4 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK2SUMMATIONRECEIVED, "Current Tier 4 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK3SUMMATIONRECEIVED, "Current Tier 4 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK4SUMMATIONRECEIVED, "Current Tier 4 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK5SUMMATIONRECEIVED, "Current Tier 4 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK6SUMMATIONRECEIVED, "Current Tier 4 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK7SUMMATIONRECEIVED, "Current Tier 4 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK8SUMMATIONRECEIVED, "Current Tier 4 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK9SUMMATIONRECEIVED, "Current Tier 4 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK10SUMMATIONRECEIVED, "Current Tier 4 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK11SUMMATIONRECEIVED, "Current Tier 4 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK12SUMMATIONRECEIVED, "Current Tier 4 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK13SUMMATIONRECEIVED, "Current Tier 4 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK14SUMMATIONRECEIVED, "Current Tier 4 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK15SUMMATIONRECEIVED, "Current Tier 4 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER4BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER4BLOCK16SUMMATIONRECEIVED, "Current Tier 4 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK1SUMMATIONRECEIVED, "Current Tier 5 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK2SUMMATIONRECEIVED, "Current Tier 5 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK3SUMMATIONRECEIVED, "Current Tier 5 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK4SUMMATIONRECEIVED, "Current Tier 5 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK5SUMMATIONRECEIVED, "Current Tier 5 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK6SUMMATIONRECEIVED, "Current Tier 5 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK7SUMMATIONRECEIVED, "Current Tier 5 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK8SUMMATIONRECEIVED, "Current Tier 5 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK9SUMMATIONRECEIVED, "Current Tier 5 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK10SUMMATIONRECEIVED, "Current Tier 5 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK11SUMMATIONRECEIVED, "Current Tier 5 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK12SUMMATIONRECEIVED, "Current Tier 5 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK13SUMMATIONRECEIVED, "Current Tier 5 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK14SUMMATIONRECEIVED, "Current Tier 5 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK15SUMMATIONRECEIVED, "Current Tier 5 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER5BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER5BLOCK16SUMMATIONRECEIVED, "Current Tier 5 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK1SUMMATIONRECEIVED, "Current Tier 6 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK2SUMMATIONRECEIVED, "Current Tier 6 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK3SUMMATIONRECEIVED, "Current Tier 6 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK4SUMMATIONRECEIVED, "Current Tier 6 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK5SUMMATIONRECEIVED, "Current Tier 6 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK6SUMMATIONRECEIVED, "Current Tier 6 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK7SUMMATIONRECEIVED, "Current Tier 6 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK8SUMMATIONRECEIVED, "Current Tier 6 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK9SUMMATIONRECEIVED, "Current Tier 6 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK10SUMMATIONRECEIVED, "Current Tier 6 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK11SUMMATIONRECEIVED, "Current Tier 6 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK12SUMMATIONRECEIVED, "Current Tier 6 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK13SUMMATIONRECEIVED, "Current Tier 6 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK14SUMMATIONRECEIVED, "Current Tier 6 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK15SUMMATIONRECEIVED, "Current Tier 6 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER6BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER6BLOCK16SUMMATIONRECEIVED, "Current Tier 6 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK1SUMMATIONRECEIVED, "Current Tier 7 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK2SUMMATIONRECEIVED, "Current Tier 7 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK3SUMMATIONRECEIVED, "Current Tier 7 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK4SUMMATIONRECEIVED, "Current Tier 7 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK5SUMMATIONRECEIVED, "Current Tier 7 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK6SUMMATIONRECEIVED, "Current Tier 7 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK7SUMMATIONRECEIVED, "Current Tier 7 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK8SUMMATIONRECEIVED, "Current Tier 7 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK9SUMMATIONRECEIVED, "Current Tier 7 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK10SUMMATIONRECEIVED, "Current Tier 7 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK11SUMMATIONRECEIVED, "Current Tier 7 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK12SUMMATIONRECEIVED, "Current Tier 7 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK13SUMMATIONRECEIVED, "Current Tier 7 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK14SUMMATIONRECEIVED, "Current Tier 7 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK15SUMMATIONRECEIVED, "Current Tier 7 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER7BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER7BLOCK16SUMMATIONRECEIVED, "Current Tier 7 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK1SUMMATIONRECEIVED, "Current Tier 8 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK2SUMMATIONRECEIVED, "Current Tier 8 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK3SUMMATIONRECEIVED, "Current Tier 8 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK4SUMMATIONRECEIVED, "Current Tier 8 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK5SUMMATIONRECEIVED, "Current Tier 8 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK6SUMMATIONRECEIVED, "Current Tier 8 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK7SUMMATIONRECEIVED, "Current Tier 8 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK8SUMMATIONRECEIVED, "Current Tier 8 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK9SUMMATIONRECEIVED, "Current Tier 8 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK10SUMMATIONRECEIVED, "Current Tier 8 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK11SUMMATIONRECEIVED, "Current Tier 8 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK12SUMMATIONRECEIVED, "Current Tier 8 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK13SUMMATIONRECEIVED, "Current Tier 8 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK14SUMMATIONRECEIVED, "Current Tier 8 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK15SUMMATIONRECEIVED, "Current Tier 8 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER8BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER8BLOCK16SUMMATIONRECEIVED, "Current Tier 8 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK1SUMMATIONRECEIVED, "Current Tier 9 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK2SUMMATIONRECEIVED, "Current Tier 9 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK3SUMMATIONRECEIVED, "Current Tier 9 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK4SUMMATIONRECEIVED, "Current Tier 9 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK5SUMMATIONRECEIVED, "Current Tier 9 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK6SUMMATIONRECEIVED, "Current Tier 9 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK7SUMMATIONRECEIVED, "Current Tier 9 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK8SUMMATIONRECEIVED, "Current Tier 9 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK9SUMMATIONRECEIVED, "Current Tier 9 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK10SUMMATIONRECEIVED, "Current Tier 9 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK11SUMMATIONRECEIVED, "Current Tier 9 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK12SUMMATIONRECEIVED, "Current Tier 9 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK13SUMMATIONRECEIVED, "Current Tier 9 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK14SUMMATIONRECEIVED, "Current Tier 9 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK15SUMMATIONRECEIVED, "Current Tier 9 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER9BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER9BLOCK16SUMMATIONRECEIVED, "Current Tier 9 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK1SUMMATIONRECEIVED, "Current Tier 10 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK2SUMMATIONRECEIVED, "Current Tier 10 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK3SUMMATIONRECEIVED, "Current Tier 10 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK4SUMMATIONRECEIVED, "Current Tier 10 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK5SUMMATIONRECEIVED, "Current Tier 10 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK6SUMMATIONRECEIVED, "Current Tier 10 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK7SUMMATIONRECEIVED, "Current Tier 10 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK8SUMMATIONRECEIVED, "Current Tier 10 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK9SUMMATIONRECEIVED, "Current Tier 10 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK10SUMMATIONRECEIVED, "Current Tier 10 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK11SUMMATIONRECEIVED, "Current Tier 10 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK12SUMMATIONRECEIVED, "Current Tier 10 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK13SUMMATIONRECEIVED, "Current Tier 10 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK14SUMMATIONRECEIVED, "Current Tier 10 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK15SUMMATIONRECEIVED, "Current Tier 10 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER10BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER10BLOCK16SUMMATIONRECEIVED, "Current Tier 10 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK1SUMMATIONRECEIVED, "Current Tier 11 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK2SUMMATIONRECEIVED, "Current Tier 11 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK3SUMMATIONRECEIVED, "Current Tier 11 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK4SUMMATIONRECEIVED, "Current Tier 11 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK5SUMMATIONRECEIVED, "Current Tier 11 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK6SUMMATIONRECEIVED, "Current Tier 11 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK7SUMMATIONRECEIVED, "Current Tier 11 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK8SUMMATIONRECEIVED, "Current Tier 11 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK9SUMMATIONRECEIVED, "Current Tier 11 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK10SUMMATIONRECEIVED, "Current Tier 11 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK11SUMMATIONRECEIVED, "Current Tier 11 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK12SUMMATIONRECEIVED, "Current Tier 11 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK13SUMMATIONRECEIVED, "Current Tier 11 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK14SUMMATIONRECEIVED, "Current Tier 11 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK15SUMMATIONRECEIVED, "Current Tier 11 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER11BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER11BLOCK16SUMMATIONRECEIVED, "Current Tier 11 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK1SUMMATIONRECEIVED, "Current Tier 12 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK2SUMMATIONRECEIVED, "Current Tier 12 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK3SUMMATIONRECEIVED, "Current Tier 12 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK4SUMMATIONRECEIVED, "Current Tier 12 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK5SUMMATIONRECEIVED, "Current Tier 12 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK6SUMMATIONRECEIVED, "Current Tier 12 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK7SUMMATIONRECEIVED, "Current Tier 12 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK8SUMMATIONRECEIVED, "Current Tier 12 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK9SUMMATIONRECEIVED, "Current Tier 12 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK10SUMMATIONRECEIVED, "Current Tier 12 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK11SUMMATIONRECEIVED, "Current Tier 12 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK12SUMMATIONRECEIVED, "Current Tier 12 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK13SUMMATIONRECEIVED, "Current Tier 12 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK14SUMMATIONRECEIVED, "Current Tier 12 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK15SUMMATIONRECEIVED, "Current Tier 12 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER12BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER12BLOCK16SUMMATIONRECEIVED, "Current Tier 12 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK1SUMMATIONRECEIVED, "Current Tier 13 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK2SUMMATIONRECEIVED, "Current Tier 13 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK3SUMMATIONRECEIVED, "Current Tier 13 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK4SUMMATIONRECEIVED, "Current Tier 13 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK5SUMMATIONRECEIVED, "Current Tier 13 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK6SUMMATIONRECEIVED, "Current Tier 13 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK7SUMMATIONRECEIVED, "Current Tier 13 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK8SUMMATIONRECEIVED, "Current Tier 13 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK9SUMMATIONRECEIVED, "Current Tier 13 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK10SUMMATIONRECEIVED, "Current Tier 13 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK11SUMMATIONRECEIVED, "Current Tier 13 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK12SUMMATIONRECEIVED, "Current Tier 13 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK13SUMMATIONRECEIVED, "Current Tier 13 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK14SUMMATIONRECEIVED, "Current Tier 13 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK15SUMMATIONRECEIVED, "Current Tier 13 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER13BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER13BLOCK16SUMMATIONRECEIVED, "Current Tier 13 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK1SUMMATIONRECEIVED, "Current Tier 14 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK2SUMMATIONRECEIVED, "Current Tier 14 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK3SUMMATIONRECEIVED, "Current Tier 14 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK4SUMMATIONRECEIVED, "Current Tier 14 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK5SUMMATIONRECEIVED, "Current Tier 14 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK6SUMMATIONRECEIVED, "Current Tier 14 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK7SUMMATIONRECEIVED, "Current Tier 14 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK8SUMMATIONRECEIVED, "Current Tier 14 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK9SUMMATIONRECEIVED, "Current Tier 14 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK10SUMMATIONRECEIVED, "Current Tier 14 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK11SUMMATIONRECEIVED, "Current Tier 14 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK12SUMMATIONRECEIVED, "Current Tier 14 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK13SUMMATIONRECEIVED, "Current Tier 14 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK14SUMMATIONRECEIVED, "Current Tier 14 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK15SUMMATIONRECEIVED, "Current Tier 14 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER14BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER14BLOCK16SUMMATIONRECEIVED, "Current Tier 14 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK1SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK1SUMMATIONRECEIVED, "Current Tier 15 Block 1 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK2SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK2SUMMATIONRECEIVED, "Current Tier 15 Block 2 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK3SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK3SUMMATIONRECEIVED, "Current Tier 15 Block 3 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK4SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK4SUMMATIONRECEIVED, "Current Tier 15 Block 4 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK5SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK5SUMMATIONRECEIVED, "Current Tier 15 Block 5 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK6SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK6SUMMATIONRECEIVED, "Current Tier 15 Block 6 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK7SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK7SUMMATIONRECEIVED, "Current Tier 15 Block 7 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK8SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK8SUMMATIONRECEIVED, "Current Tier 15 Block 8 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK9SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK9SUMMATIONRECEIVED, "Current Tier 15 Block 9 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK10SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK10SUMMATIONRECEIVED, "Current Tier 15 Block 10 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK11SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK11SUMMATIONRECEIVED, "Current Tier 15 Block 11 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK12SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK12SUMMATIONRECEIVED, "Current Tier 15 Block 12 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK13SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK13SUMMATIONRECEIVED, "Current Tier 15 Block 13 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK14SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK14SUMMATIONRECEIVED, "Current Tier 15 Block 14 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK15SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK15SUMMATIONRECEIVED, "Current Tier 15 Block 15 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTTIER15BLOCK16SUMMATIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTTIER15BLOCK16SUMMATIONRECEIVED, "Current Tier 15 Block 16 Summation Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_BILLTODATEDELIVERED, new ZclAttribute(this, ATTR_BILLTODATEDELIVERED, "Bill To Date Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_BILLTODATETIMESTAMPDELIVERED, new ZclAttribute(this, ATTR_BILLTODATETIMESTAMPDELIVERED, "Bill To Date Time Stamp Delivered", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_PROJECTEDBILLDELIVERED, new ZclAttribute(this, ATTR_PROJECTEDBILLDELIVERED, "Projected Bill Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PROJECTEDBILLTIMESTAMPDELIVERED, new ZclAttribute(this, ATTR_PROJECTEDBILLTIMESTAMPDELIVERED, "Projected Bill Time Stamp Delivered", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_BILLDELIVEREDTRAILINGDIGIT, new ZclAttribute(this, ATTR_BILLDELIVEREDTRAILINGDIGIT, "Bill Delivered Trailing Digit", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_BILLTODATERECEIVED, new ZclAttribute(this, ATTR_BILLTODATERECEIVED, "Bill To Date Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_BILLTODATETIMESTAMPRECEIVED, new ZclAttribute(this, ATTR_BILLTODATETIMESTAMPRECEIVED, "Bill To Date Time Stamp Received", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_PROJECTEDBILLRECEIVED, new ZclAttribute(this, ATTR_PROJECTEDBILLRECEIVED, "Projected Bill Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PROJECTEDBILLTIMESTAMPRECEIVED, new ZclAttribute(this, ATTR_PROJECTEDBILLTIMESTAMPRECEIVED, "Projected Bill Time Stamp Received", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_BILLRECEIVEDTRAILINGDIGIT, new ZclAttribute(this, ATTR_BILLRECEIVEDTRAILINGDIGIT, "Bill Received Trailing Digit", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_PROPOSEDCHANGESUPPLYIMPLEMENTATIONTIME, new ZclAttribute(this, ATTR_PROPOSEDCHANGESUPPLYIMPLEMENTATIONTIME, "Proposed Change Supply Implementation Time", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_PROPOSEDCHANGESUPPLYSTATUS, new ZclAttribute(this, ATTR_PROPOSEDCHANGESUPPLYSTATUS, "Proposed Change Supply Status", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_UNCONTROLLEDFLOWTHRESHOLD, new ZclAttribute(this, ATTR_UNCONTROLLEDFLOWTHRESHOLD, "Uncontrolled Flow Threshold", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_UNCONTROLLEDFLOWTHRESHOLDUNITOFMEASURE, new ZclAttribute(this, ATTR_UNCONTROLLEDFLOWTHRESHOLDUNITOFMEASURE, "Uncontrolled Flow Threshold Unit Of Measure", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_UNCONTROLLEDFLOWTHRESHOLDMULTIPLIER, new ZclAttribute(this, ATTR_UNCONTROLLEDFLOWTHRESHOLDMULTIPLIER, "Uncontrolled Flow Threshold Multiplier", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_UNCONTROLLEDFLOWTHRESHOLDDIVISOR, new ZclAttribute(this, ATTR_UNCONTROLLEDFLOWTHRESHOLDDIVISOR, "Uncontrolled Flow Threshold Divisor", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_FLOWSTABILIZATIONPERIOD, new ZclAttribute(this, ATTR_FLOWSTABILIZATIONPERIOD, "Flow Stabilization Period", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_FLOWMEASUREMENTPERIOD, new ZclAttribute(this, ATTR_FLOWMEASUREMENTPERIOD, "Flow Measurement Period", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_ALTERNATIVEINSTANTANEOUSDEMAND, new ZclAttribute(this, ATTR_ALTERNATIVEINSTANTANEOUSDEMAND, "Alternative Instantaneous Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTDAYALTERNATIVECONSUMPTIONDELIVERED, "Current Day Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTDAYALTERNATIVECONSUMPTIONRECEIVED, "Current Day Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAYALTERNATIVECONSUMPTIONDELIVERED, "Previous Day Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAYALTERNATIVECONSUMPTIONRECEIVED, "Previous Day Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALSTARTTIMEDELIVERED, new ZclAttribute(this, ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALSTARTTIMEDELIVERED, "Current Alternative Partial Profile Interval Start Time Delivered", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALSTARTTIMERECEIVED, new ZclAttribute(this, ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALSTARTTIMERECEIVED, "Current Alternative Partial Profile Interval Start Time Received", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALVALUEDELIVERED, new ZclAttribute(this, ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALVALUEDELIVERED, "Current Alternative Partial Profile Interval Value Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALVALUERECEIVED, new ZclAttribute(this, ATTR_CURRENTALTERNATIVEPARTIALPROFILEINTERVALVALUERECEIVED, "Current Alternative Partial Profile Interval Value Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYALTERNATIVEMAXPRESSURE, new ZclAttribute(this, ATTR_CURRENTDAYALTERNATIVEMAXPRESSURE, "Current Day Alternative Max Pressure", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYALTERNATIVEMINPRESSURE, new ZclAttribute(this, ATTR_CURRENTDAYALTERNATIVEMINPRESSURE, "Current Day Alternative Min Pressure", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYALTERNATIVEMAXPRESSURE, new ZclAttribute(this, ATTR_PREVIOUSDAYALTERNATIVEMAXPRESSURE, "Previous Day Alternative Max Pressure", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYALTERNATIVEMINPRESSURE, new ZclAttribute(this, ATTR_PREVIOUSDAYALTERNATIVEMINPRESSURE, "Previous Day Alternative Min Pressure", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYALTERNATIVEMAXDEMAND, new ZclAttribute(this, ATTR_CURRENTDAYALTERNATIVEMAXDEMAND, "Current Day Alternative Max Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYALTERNATIVEMAXDEMAND, new ZclAttribute(this, ATTR_PREVIOUSDAYALTERNATIVEMAXDEMAND, "Previous Day Alternative Max Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMONTHALTERNATIVEMAXDEMAND, new ZclAttribute(this, ATTR_CURRENTMONTHALTERNATIVEMAXDEMAND, "Current Month Alternative Max Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTYEARALTERNATIVEMAXDEMAND, new ZclAttribute(this, ATTR_CURRENTYEARALTERNATIVEMAXDEMAND, "Current Year Alternative Max Demand", ZclDataType.Get(DataType.SIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY2ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY2ALTERNATIVECONSUMPTIONDELIVERED, "Previous Day 2 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY4ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY4ALTERNATIVECONSUMPTIONDELIVERED, "Previous Day 4 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY6ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY6ALTERNATIVECONSUMPTIONDELIVERED, "Previous Day 6 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY8ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY8ALTERNATIVECONSUMPTIONDELIVERED, "Previous Day 8 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY10ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY10ALTERNATIVECONSUMPTIONDELIVERED, "Previous Day 10 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY12ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY12ALTERNATIVECONSUMPTIONDELIVERED, "Previous Day 12 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY2ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY2ALTERNATIVECONSUMPTIONRECEIVED, "Previous Day 2 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY4ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY4ALTERNATIVECONSUMPTIONRECEIVED, "Previous Day 4 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY6ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY6ALTERNATIVECONSUMPTIONRECEIVED, "Previous Day 6 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY8ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY8ALTERNATIVECONSUMPTIONRECEIVED, "Previous Day 8 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY10ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY10ALTERNATIVECONSUMPTIONRECEIVED, "Previous Day 10 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY12ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY12ALTERNATIVECONSUMPTIONRECEIVED, "Previous Day 12 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTWEEKALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTWEEKALTERNATIVECONSUMPTIONDELIVERED, "Current Week Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTWEEKALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTWEEKALTERNATIVECONSUMPTIONRECEIVED, "Current Week Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK1ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK1ALTERNATIVECONSUMPTIONDELIVERED, "Previous Week 1 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK3ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK3ALTERNATIVECONSUMPTIONDELIVERED, "Previous Week 3 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK5ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK5ALTERNATIVECONSUMPTIONDELIVERED, "Previous Week 5 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK7ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK7ALTERNATIVECONSUMPTIONDELIVERED, "Previous Week 7 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK9ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK9ALTERNATIVECONSUMPTIONDELIVERED, "Previous Week 9 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK1ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK1ALTERNATIVECONSUMPTIONRECEIVED, "Previous Week 1 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK3ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK3ALTERNATIVECONSUMPTIONRECEIVED, "Previous Week 3 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK5ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK5ALTERNATIVECONSUMPTIONRECEIVED, "Previous Week 5 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK7ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK7ALTERNATIVECONSUMPTIONRECEIVED, "Previous Week 7 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK9ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK9ALTERNATIVECONSUMPTIONRECEIVED, "Previous Week 9 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMONTHALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTMONTHALTERNATIVECONSUMPTIONDELIVERED, "Current Month Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMONTHALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTMONTHALTERNATIVECONSUMPTIONRECEIVED, "Current Month Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH1ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH1ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 1 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH3ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH3ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 3 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH5ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH5ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 5 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH7ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH7ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 7 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH9ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH9ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 9 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH11ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH11ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 11 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH13ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH13ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 13 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH15ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH15ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 15 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH17ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH17ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 17 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH19ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH19ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 19 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH21ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH21ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 21 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH23ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH23ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 23 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH25ALTERNATIVECONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH25ALTERNATIVECONSUMPTIONDELIVERED, "Previous Month 25 Alternative Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH1ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH1ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 1 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH3ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH3ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 3 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH5ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH5ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 5 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH7ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH7ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 7 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH9ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH9ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 9 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH11ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH11ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 11 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH13ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH13ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 13 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH15ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH15ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 15 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH17ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH17ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 17 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH19ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH19ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 19 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH21ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH21ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 21 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH23ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH23ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 23 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH25ALTERNATIVECONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH25ALTERNATIVECONSUMPTIONRECEIVED, "Previous Month 25 Alternative Consumption Received", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(14);

            commandMap.Add(0x0000, () => new GetProfileResponse());
            commandMap.Add(0x0001, () => new RequestMirror());
            commandMap.Add(0x0002, () => new RemoveMirror());
            commandMap.Add(0x0003, () => new RequestFastPollModeResponse());
            commandMap.Add(0x0004, () => new ScheduleSnapshotResponse());
            commandMap.Add(0x0005, () => new TakeSnapshotResponse());
            commandMap.Add(0x0006, () => new PublishSnapshot());
            commandMap.Add(0x0007, () => new GetSampledDataResponse());
            commandMap.Add(0x0008, () => new ConfigureMirror());
            commandMap.Add(0x0009, () => new ConfigureNotificationScheme());
            commandMap.Add(0x000A, () => new ConfigureNotificationFlags());
            commandMap.Add(0x000B, () => new GetNotifiedMessage());
            commandMap.Add(0x000C, () => new SupplyStatusResponse());
            commandMap.Add(0x000D, () => new StartSamplingResponse());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(15);

            commandMap.Add(0x0000, () => new GetProfile());
            commandMap.Add(0x0001, () => new RequestMirrorResponse());
            commandMap.Add(0x0002, () => new MirrorRemoved());
            commandMap.Add(0x0003, () => new RequestFastPollMode());
            commandMap.Add(0x0004, () => new ScheduleSnapshot());
            commandMap.Add(0x0005, () => new TakeSnapshot());
            commandMap.Add(0x0006, () => new GetSnapshot());
            commandMap.Add(0x0007, () => new StartSampling());
            commandMap.Add(0x0008, () => new GetSampledData());
            commandMap.Add(0x0009, () => new MirrorReportAttributeResponse());
            commandMap.Add(0x000A, () => new ResetLoadLimitCounter());
            commandMap.Add(0x000B, () => new ChangeSupply());
            commandMap.Add(0x000C, () => new LocalChangeSupply());
            commandMap.Add(0x000D, () => new SetSupplyStatus());
            commandMap.Add(0x000E, () => new SetUncontrolledFlowThreshold());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Metering cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclMeteringCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Get Profile
        ///
        /// The GetProfile command is generated when a client device wishes to retrieve a list
        /// of captured Energy, Gas or water consumption for profiling purposes.
        ///
        /// <param name="intervalChannel" <see cref="byte"> Interval Channel</ param >
        /// <param name="endTime" <see cref="DateTime"> End Time</ param >
        /// <param name="numberOfPeriods" <see cref="byte"> Number Of Periods</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetProfile(byte intervalChannel, DateTime endTime, byte numberOfPeriods)
        {
            GetProfile command = new GetProfile();

            // Set the fields
            command.IntervalChannel = intervalChannel;
            command.EndTime = endTime;
            command.NumberOfPeriods = numberOfPeriods;

            return Send(command);
        }

        /// <summary>
        /// The Request Mirror Response
        ///
        /// The Request Mirror Response Command allows the ESI to inform a sleepy Metering
        /// Device it has the ability to store and mirror its data.
        ///
        /// <param name="endpointId" <see cref="ushort"> Endpoint ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RequestMirrorResponse(ushort endpointId)
        {
            RequestMirrorResponse command = new RequestMirrorResponse();

            // Set the fields
            command.EndpointId = endpointId;

            return Send(command);
        }

        /// <summary>
        /// The Mirror Removed
        ///
        /// The Mirror Removed Command allows the ESI to inform a sleepy Metering Device
        /// mirroring support has been removed or halted.
        ///
        /// <param name="removedEndpointId" <see cref="ushort"> Removed Endpoint ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MirrorRemoved(ushort removedEndpointId)
        {
            MirrorRemoved command = new MirrorRemoved();

            // Set the fields
            command.RemovedEndpointId = removedEndpointId;

            return Send(command);
        }

        /// <summary>
        /// The Request Fast Poll Mode
        ///
        /// The Request Fast Poll Mode command is generated when the metering client wishes to
        /// receive near real-time updates of InstantaneousDemand.
        ///
        /// <param name="fastPollUpdatePeriod" <see cref="byte"> Fast Poll Update Period</ param >
        /// <param name="duration" <see cref="byte"> Duration</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RequestFastPollMode(byte fastPollUpdatePeriod, byte duration)
        {
            RequestFastPollMode command = new RequestFastPollMode();

            // Set the fields
            command.FastPollUpdatePeriod = fastPollUpdatePeriod;
            command.Duration = duration;

            return Send(command);
        }

        /// <summary>
        /// The Schedule Snapshot
        ///
        /// This command is used to set up a schedule of when the device shall create snapshot
        /// data.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="commandIndex" <see cref="byte"> Command Index</ param >
        /// <param name="totalNumberOfCommands" <see cref="byte"> Total Number of Commands</ param >
        /// <param name="snapshotSchedulePayload" <see cref="SnapshotSchedulePayload"> Snapshot Schedule Payload</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ScheduleSnapshot(uint issuerEventId, byte commandIndex, byte totalNumberOfCommands, SnapshotSchedulePayload snapshotSchedulePayload)
        {
            ScheduleSnapshot command = new ScheduleSnapshot();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.CommandIndex = commandIndex;
            command.TotalNumberOfCommands = totalNumberOfCommands;
            command.SnapshotSchedulePayload = snapshotSchedulePayload;

            return Send(command);
        }

        /// <summary>
        /// The Take Snapshot
        ///
        /// This command is used to instruct the cluster server to take a single snapshot.
        ///
        /// <param name="snapshotCause" <see cref="int"> Snapshot Cause</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> TakeSnapshot(int snapshotCause)
        {
            TakeSnapshot command = new TakeSnapshot();

            // Set the fields
            command.SnapshotCause = snapshotCause;

            return Send(command);
        }

        /// <summary>
        /// The Get Snapshot
        ///
        /// This command is used to request snapshot data from the cluster server.
        ///
        /// <param name="earliestStartTime" <see cref="DateTime"> Earliest Start Time</ param >
        /// <param name="latestEndTime" <see cref="DateTime"> Latest End Time</ param >
        /// <param name="snapshotOffset" <see cref="byte"> Snapshot Offset</ param >
        /// <param name="snapshotCause" <see cref="int"> Snapshot Cause</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetSnapshot(DateTime earliestStartTime, DateTime latestEndTime, byte snapshotOffset, int snapshotCause)
        {
            GetSnapshot command = new GetSnapshot();

            // Set the fields
            command.EarliestStartTime = earliestStartTime;
            command.LatestEndTime = latestEndTime;
            command.SnapshotOffset = snapshotOffset;
            command.SnapshotCause = snapshotCause;

            return Send(command);
        }

        /// <summary>
        /// The Start Sampling
        ///
        /// The sampling mechanism allows a set of samples of the specified type of data to be
        /// taken, commencing at the stipulated start time. This mechanism may run
        /// concurrently with the capturing of profile data, and may refer the same
        /// parameters, albeit possibly at a different sampling rate.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="startSamplingTime" <see cref="DateTime"> Start Sampling Time</ param >
        /// <param name="sampleType" <see cref="byte"> Sample Type</ param >
        /// <param name="sampleRequestInterval" <see cref="ushort"> Sample Request Interval</ param >
        /// <param name="maxNumberOfSamples" <see cref="ushort"> Max Number Of Samples</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StartSampling(uint issuerEventId, DateTime startSamplingTime, byte sampleType, ushort sampleRequestInterval, ushort maxNumberOfSamples)
        {
            StartSampling command = new StartSampling();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.StartSamplingTime = startSamplingTime;
            command.SampleType = sampleType;
            command.SampleRequestInterval = sampleRequestInterval;
            command.MaxNumberOfSamples = maxNumberOfSamples;

            return Send(command);
        }

        /// <summary>
        /// The Get Sampled Data
        ///
        /// This command is used to request sampled data from the server. Note that it is the
        /// responsibility of the client to ensure that it does not request more samples than
        /// can be held in a single command payload.
        ///
        /// <param name="sampleId" <see cref="ushort"> Sample ID</ param >
        /// <param name="earliestSampleTime" <see cref="DateTime"> Earliest Sample Time</ param >
        /// <param name="sampleType" <see cref="byte"> Sample Type</ param >
        /// <param name="numberOfSamples" <see cref="ushort"> Number Of Samples</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetSampledData(ushort sampleId, DateTime earliestSampleTime, byte sampleType, ushort numberOfSamples)
        {
            GetSampledData command = new GetSampledData();

            // Set the fields
            command.SampleId = sampleId;
            command.EarliestSampleTime = earliestSampleTime;
            command.SampleType = sampleType;
            command.NumberOfSamples = numberOfSamples;

            return Send(command);
        }

        /// <summary>
        /// The Mirror Report Attribute Response
        ///
        /// FIXME: This command is sent in response to the ReportAttribute command when the
        /// MirrorReporting attribute is set.
        ///
        /// <param name="notificationScheme" <see cref="byte"> Notification Scheme</ param >
        /// <param name="notificationFlags" <see cref="int"> Notification Flags</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> MirrorReportAttributeResponse(byte notificationScheme, int notificationFlags)
        {
            MirrorReportAttributeResponse command = new MirrorReportAttributeResponse();

            // Set the fields
            command.NotificationScheme = notificationScheme;
            command.NotificationFlags = notificationFlags;

            return Send(command);
        }

        /// <summary>
        /// The Reset Load Limit Counter
        ///
        /// The ResetLoadLimitCounter command shall cause the LoadLimitCounter attribute
        /// to be reset.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ResetLoadLimitCounter(uint providerId, uint issuerEventId)
        {
            ResetLoadLimitCounter command = new ResetLoadLimitCounter();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;

            return Send(command);
        }

        /// <summary>
        /// The Change Supply
        ///
        /// This command is sent from the Head-end or ESI to the Metering Device to instruct it to
        /// change the status of the valve or load switch, i.e. the supply.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="requestDateTime" <see cref="DateTime"> Request Date Time</ param >
        /// <param name="implementationDateTime" <see cref="DateTime"> Implementation Date Time</ param >
        /// <param name="proposedSupplyStatus" <see cref="byte"> Proposed Supply Status</ param >
        /// <param name="supplyControlBits" <see cref="byte"> Supply Control Bits</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ChangeSupply(uint providerId, uint issuerEventId, DateTime requestDateTime, DateTime implementationDateTime, byte proposedSupplyStatus, byte supplyControlBits)
        {
            ChangeSupply command = new ChangeSupply();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.RequestDateTime = requestDateTime;
            command.ImplementationDateTime = implementationDateTime;
            command.ProposedSupplyStatus = proposedSupplyStatus;
            command.SupplyControlBits = supplyControlBits;

            return Send(command);
        }

        /// <summary>
        /// The Local Change Supply
        ///
        /// This command is a simplified version of the ChangeSupply command, intended to be
        /// sent from an IHD to a meter as the consequence of a user action on the IHD. Its purpose
        /// is to provide a local disconnection/reconnection button on the IHD in addition to
        /// the one on the meter.
        ///
        /// <param name="proposedSupplyStatus" <see cref="byte"> Proposed Supply Status</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> LocalChangeSupply(byte proposedSupplyStatus)
        {
            LocalChangeSupply command = new LocalChangeSupply();

            // Set the fields
            command.ProposedSupplyStatus = proposedSupplyStatus;

            return Send(command);
        }

        /// <summary>
        /// The Set Supply Status
        ///
        /// This command is used to specify the required status of the supply following the
        /// occurance of certain events on the meter.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="supplyTamperState" <see cref="byte"> Supply Tamper State</ param >
        /// <param name="supplyDepletionState" <see cref="byte"> Supply Depletion State</ param >
        /// <param name="supplyUncontrolledFlowState" <see cref="byte"> Supply Uncontrolled Flow State</ param >
        /// <param name="loadLimitSupplyState" <see cref="byte"> Load Limit Supply State</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SetSupplyStatus(uint issuerEventId, byte supplyTamperState, byte supplyDepletionState, byte supplyUncontrolledFlowState, byte loadLimitSupplyState)
        {
            SetSupplyStatus command = new SetSupplyStatus();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.SupplyTamperState = supplyTamperState;
            command.SupplyDepletionState = supplyDepletionState;
            command.SupplyUncontrolledFlowState = supplyUncontrolledFlowState;
            command.LoadLimitSupplyState = loadLimitSupplyState;

            return Send(command);
        }

        /// <summary>
        /// The Set Uncontrolled Flow Threshold
        ///
        /// This command is used to update the 'Uncontrolled Flow Rate' configuration data
        /// used by flow meters.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="uncontrolledFlowThreshold" <see cref="ushort"> Uncontrolled Flow Threshold</ param >
        /// <param name="unitOfMeasure" <see cref="byte"> Unit Of Measure</ param >
        /// <param name="multiplier" <see cref="ushort"> Multiplier</ param >
        /// <param name="divisor" <see cref="ushort"> Divisor</ param >
        /// <param name="stabilisationPeriod" <see cref="byte"> Stabilisation Period</ param >
        /// <param name="measurementPeriod" <see cref="ushort"> Measurement Period</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SetUncontrolledFlowThreshold(uint providerId, uint issuerEventId, ushort uncontrolledFlowThreshold, byte unitOfMeasure, ushort multiplier, ushort divisor, byte stabilisationPeriod, ushort measurementPeriod)
        {
            SetUncontrolledFlowThreshold command = new SetUncontrolledFlowThreshold();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.UncontrolledFlowThreshold = uncontrolledFlowThreshold;
            command.UnitOfMeasure = unitOfMeasure;
            command.Multiplier = multiplier;
            command.Divisor = divisor;
            command.StabilisationPeriod = stabilisationPeriod;
            command.MeasurementPeriod = measurementPeriod;

            return Send(command);
        }

        /// <summary>
        /// The Get Profile Response
        ///
        /// This command is sent when the Client command GetProfile is received.
        ///
        /// <param name="endTime" <see cref="DateTime"> End Time</ param >
        /// <param name="status" <see cref="byte"> Status</ param >
        /// <param name="profileIntervalPeriod" <see cref="byte"> Profile Interval Period</ param >
        /// <param name="numberOfPeriodsDelivered" <see cref="byte"> Number Of Periods Delivered</ param >
        /// <param name="intervals" <see cref="uint"> Intervals</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetProfileResponse(DateTime endTime, byte status, byte profileIntervalPeriod, byte numberOfPeriodsDelivered, uint intervals)
        {
            GetProfileResponse command = new GetProfileResponse();

            // Set the fields
            command.EndTime = endTime;
            command.Status = status;
            command.ProfileIntervalPeriod = profileIntervalPeriod;
            command.NumberOfPeriodsDelivered = numberOfPeriodsDelivered;
            command.Intervals = intervals;

            return Send(command);
        }

        /// <summary>
        /// The Request Mirror
        ///
        /// This command is used to request the ESI to mirror Metering Device data.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RequestMirror()
        {
            return Send(new RequestMirror());
        }

        /// <summary>
        /// The Remove Mirror
        ///
        /// This command is used to request the ESI to remove its mirror of Metering Device data.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RemoveMirror()
        {
            return Send(new RemoveMirror());
        }

        /// <summary>
        /// The Request Fast Poll Mode Response
        ///
        /// This command is generated when the client command Request Fast Poll Mode is
        /// received.
        ///
        /// <param name="appliedUpdatePeriod" <see cref="byte"> Applied Update Period</ param >
        /// <param name="fastPollModeEndtime" <see cref="DateTime"> Fast Poll Mode Endtime</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> RequestFastPollModeResponse(byte appliedUpdatePeriod, DateTime fastPollModeEndtime)
        {
            RequestFastPollModeResponse command = new RequestFastPollModeResponse();

            // Set the fields
            command.AppliedUpdatePeriod = appliedUpdatePeriod;
            command.FastPollModeEndtime = fastPollModeEndtime;

            return Send(command);
        }

        /// <summary>
        /// The Schedule Snapshot Response
        ///
        /// This command is generated in response to a ScheduleSnapshot command, and is sent to
        /// confirm whether the requested snapshot schedule has been set up.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="snapshotResponsePayload" <see cref="SnapshotResponsePayload"> Snapshot Response Payload</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ScheduleSnapshotResponse(uint issuerEventId, SnapshotResponsePayload snapshotResponsePayload)
        {
            ScheduleSnapshotResponse command = new ScheduleSnapshotResponse();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.SnapshotResponsePayload = snapshotResponsePayload;

            return Send(command);
        }

        /// <summary>
        /// The Take Snapshot Response
        ///
        /// This command is generated in response to a TakeSnapshot command, and is sent to
        /// confirm whether the requested snapshot has been accepted and successfully taken.
        ///
        /// <param name="snapshotId" <see cref="uint"> Snapshot ID</ param >
        /// <param name="snapshotConfirmation" <see cref="byte"> Snapshot Confirmation</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> TakeSnapshotResponse(uint snapshotId, byte snapshotConfirmation)
        {
            TakeSnapshotResponse command = new TakeSnapshotResponse();

            // Set the fields
            command.SnapshotId = snapshotId;
            command.SnapshotConfirmation = snapshotConfirmation;

            return Send(command);
        }

        /// <summary>
        /// The Publish Snapshot
        ///
        /// This command is generated in response to a GetSnapshot command. It is used to return
        /// a single snapshot to the client.
        ///
        /// <param name="snapshotId" <see cref="uint"> Snapshot ID</ param >
        /// <param name="snapshotTime" <see cref="DateTime"> Snapshot Time</ param >
        /// <param name="totalSnapshotsFound" <see cref="byte"> Total Snapshots Found</ param >
        /// <param name="commandIndex" <see cref="byte"> Command Index</ param >
        /// <param name="totalNumberOfCommands" <see cref="byte"> Total Number Of Commands</ param >
        /// <param name="snapshotCause" <see cref="int"> Snapshot Cause</ param >
        /// <param name="snapshotPayloadType" <see cref="byte"> Snapshot Payload Type</ param >
        /// <param name="snapshotPayload" <see cref="byte"> Snapshot Payload</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishSnapshot(uint snapshotId, DateTime snapshotTime, byte totalSnapshotsFound, byte commandIndex, byte totalNumberOfCommands, int snapshotCause, byte snapshotPayloadType, byte snapshotPayload)
        {
            PublishSnapshot command = new PublishSnapshot();

            // Set the fields
            command.SnapshotId = snapshotId;
            command.SnapshotTime = snapshotTime;
            command.TotalSnapshotsFound = totalSnapshotsFound;
            command.CommandIndex = commandIndex;
            command.TotalNumberOfCommands = totalNumberOfCommands;
            command.SnapshotCause = snapshotCause;
            command.SnapshotPayloadType = snapshotPayloadType;
            command.SnapshotPayload = snapshotPayload;

            return Send(command);
        }

        /// <summary>
        /// The Get Sampled Data Response
        ///
        /// FIXME: This command is used to send the requested sample data to the client. It is
        /// generated in response to a GetSampledData command.
        ///
        /// <param name="sampleId" <see cref="ushort"> Sample ID</ param >
        /// <param name="sampleStartTime" <see cref="DateTime"> Sample Start Time</ param >
        /// <param name="sampleType" <see cref="byte"> Sample Type</ param >
        /// <param name="sampleRequestInterval" <see cref="ushort"> Sample Request Interval</ param >
        /// <param name="numberOfSamples" <see cref="ushort"> Number Of Samples</ param >
        /// <param name="samples" <see cref="uint"> Samples</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetSampledDataResponse(ushort sampleId, DateTime sampleStartTime, byte sampleType, ushort sampleRequestInterval, ushort numberOfSamples, uint samples)
        {
            GetSampledDataResponse command = new GetSampledDataResponse();

            // Set the fields
            command.SampleId = sampleId;
            command.SampleStartTime = sampleStartTime;
            command.SampleType = sampleType;
            command.SampleRequestInterval = sampleRequestInterval;
            command.NumberOfSamples = numberOfSamples;
            command.Samples = samples;

            return Send(command);
        }

        /// <summary>
        /// The Configure Mirror
        ///
        /// FIXME: ConfigureMirror is sent to the mirror once the mirror has been created. The
        /// command deals with the operational configuration of the Mirror.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="reportingInterval" <see cref="uint"> Reporting Interval</ param >
        /// <param name="mirrorNotificationReporting" <see cref="bool"> Mirror Notification Reporting</ param >
        /// <param name="notificationScheme" <see cref="byte"> Notification Scheme</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ConfigureMirror(uint issuerEventId, uint reportingInterval, bool mirrorNotificationReporting, byte notificationScheme)
        {
            ConfigureMirror command = new ConfigureMirror();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.ReportingInterval = reportingInterval;
            command.MirrorNotificationReporting = mirrorNotificationReporting;
            command.NotificationScheme = notificationScheme;

            return Send(command);
        }

        /// <summary>
        /// The Configure Notification Scheme
        ///
        /// FIXME: The ConfigureNotificationScheme is sent to the mirror once the mirror has
        /// been created. The command deals with the operational configuration of the Mirror
        /// and the device that reports to the mirror. No default schemes are allowed to be
        /// overwritten.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="notificationScheme" <see cref="byte"> Notification Scheme</ param >
        /// <param name="notificationFlagOrder" <see cref="int"> Notification Flag Order</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ConfigureNotificationScheme(uint issuerEventId, byte notificationScheme, int notificationFlagOrder)
        {
            ConfigureNotificationScheme command = new ConfigureNotificationScheme();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.NotificationScheme = notificationScheme;
            command.NotificationFlagOrder = notificationFlagOrder;

            return Send(command);
        }

        /// <summary>
        /// The Configure Notification Flags
        ///
        /// The ConfigureNotificationFlags command is used to set the commands relating to
        /// the bit value for each NotificationFlags attribute that the scheme is proposing to
        /// use.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="notificationScheme" <see cref="byte"> Notification Scheme</ param >
        /// <param name="notificationFlagAttributeId" <see cref="ushort"> Notification Flag Attribute ID</ param >
        /// <param name="subPayload" <see cref="NotificationCommandSubPayload"> Sub Payload</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ConfigureNotificationFlags(uint issuerEventId, byte notificationScheme, ushort notificationFlagAttributeId, NotificationCommandSubPayload subPayload)
        {
            ConfigureNotificationFlags command = new ConfigureNotificationFlags();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.NotificationScheme = notificationScheme;
            command.NotificationFlagAttributeId = notificationFlagAttributeId;
            command.SubPayload = subPayload;

            return Send(command);
        }

        /// <summary>
        /// The Get Notified Message
        ///
        /// The GetNotifiedMessage command is used only when a BOMD is being mirrored. This
        /// command provides a method for the BOMD to notify the Mirror message queue that it
        /// wants to receive commands that the Mirror has queued. The Notification flags set
        /// within the command shall inform the mirror of the commands that the BOMD is
        /// requesting.
        ///
        /// <param name="notificationScheme" <see cref="byte"> Notification Scheme</ param >
        /// <param name="notificationFlagAttributeId" <see cref="ushort"> Notification Flag Attribute ID</ param >
        /// <param name="notificationFlagsN" <see cref="int"> Notification Flags N</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetNotifiedMessage(byte notificationScheme, ushort notificationFlagAttributeId, int notificationFlagsN)
        {
            GetNotifiedMessage command = new GetNotifiedMessage();

            // Set the fields
            command.NotificationScheme = notificationScheme;
            command.NotificationFlagAttributeId = notificationFlagAttributeId;
            command.NotificationFlagsN = notificationFlagsN;

            return Send(command);
        }

        /// <summary>
        /// The Supply Status Response
        ///
        /// This command is transmitted by a Metering Device in response to a ChangeSupply
        /// command.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="implementationDateTime" <see cref="DateTime"> Implementation Date Time</ param >
        /// <param name="supplyStatus" <see cref="byte"> Supply Status</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SupplyStatusResponse(uint providerId, uint issuerEventId, DateTime implementationDateTime, byte supplyStatus)
        {
            SupplyStatusResponse command = new SupplyStatusResponse();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.ImplementationDateTime = implementationDateTime;
            command.SupplyStatus = supplyStatus;

            return Send(command);
        }

        /// <summary>
        /// The Start Sampling Response
        ///
        /// This command is transmitted by a Metering Device in response to a StartSampling
        /// command.
        ///
        /// <param name="sampleId" <see cref="ushort"> Sample ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StartSamplingResponse(ushort sampleId)
        {
            StartSamplingResponse command = new StartSamplingResponse();

            // Set the fields
            command.SampleId = sampleId;

            return Send(command);
        }
    }
}
