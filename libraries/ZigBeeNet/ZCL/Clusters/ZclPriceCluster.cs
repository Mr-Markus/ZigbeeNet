
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Price;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Price cluster implementation (Cluster ID 0x0700).
    ///
    /// The Price Cluster provides the mechanism for communicating Gas, Energy, or Water
    /// pricing information within the premises. This pricing information is distributed to
    /// the ESI from either the utilities or from regional energy providers. The ESI conveys the
    /// information (via the Price Cluster mechanisms) to other Smart Energy devices.
    /// Events carried using this cluster include a timestamp with the assumption that target
    /// devices maintain a real time clock. Devices can acquire and synchronize their internal
    /// clocks via the ZCL Time server. If a device does not support a real time clock it is assumed
    /// that the device will interpret and utilize the “Start Now” value within the Time field.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclPriceCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0700;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Price";

        // Attribute constants

        /// <summary>
        /// The PriceIncreaseRandomizeMinutes attribute represents the maximum amount of
        /// time to be used when randomizing the response to a price increase. Note that
        /// although the granularity of the attribute is in minutes, it is recommended the
        /// granularity of the randomization used within a responding device be in seconds or
        /// smaller. If a device responds to a price increase it must choose a random amount of
        /// time, in seconds or smaller, between 0 and PriceIncreaseRandomizeMinutes
        /// minutes. The device must implement that random amount of time before or after the
        /// price change. How and if a device will respond to a price increase is up to the
        /// manufacturer. Whether to respond before or after the price increase is also up to
        /// the manufacturer.
        /// As an example, a water heater with a PriceIncreaseRandomizeMinutes set to 6 could
        /// choose to lower its set point 315 seconds (but not more than 360 seconds) before the
        /// price increases.
        /// The valid range for this attribute is 0x00 to 0x3C.
        /// If PriceIncreaseRandomizeMinutes or PriceDecreaseRandomizeMinutes
        /// attributes are not supported by the CLIENT, then it should use the default values
        /// for the attributes as specified in the Price CLIENT Cluster Attribute table.
        /// </summary>
        public const ushort ATTR_PRICEINCREASERANDOMIZEMINUTES = 0x0000;

        /// <summary>
        /// The PriceDecreaseRandomizeMinutes attribute represents the maximum number of
        /// minutes to be used when randomizing the response to a price decrease. Note that
        /// although the granularity of the attribute is in minutes, it is recommended the
        /// granularity of the randomization used within a responding device be in seconds or
        /// smaller. If a device responds to a price decrease it must choose a random amount of
        /// time, in seconds or smaller, between 0 and PriceDecreaseRandomizeMinutes
        /// minutes and implement that random amount of time before or after the price change.
        /// How and if a device will respond to a price decrease is up to the manufacturer.
        /// Whether to respond before or after the price increase is also up to the
        /// manufacturer.
        /// As an example, a dishwasher with a PriceDecreaseRandomizeMinutes set to 15 could
        /// choose to start its wash cycle 723 seconds (but not more than 900 seconds) after the
        /// price decreases. The valid range for this attribute is 0x00 to 0x3C.
        /// </summary>
        public const ushort ATTR_PRICEDECREASERANDOMIZEMINUTES = 0x0001;

        /// <summary>
        /// CommodityType provides a label for identifying the type of pricing client
        /// present. The attribute is an enumerated value representing the commodity. The
        /// defined values are represented by the non-mirrored values (0-127) in the
        /// MeteringDeviceType attribute enumerations.
        /// </summary>
        public const ushort ATTR_COMMODITYTYPECLIENT = 0x0002;
        public const ushort ATTR_TIER1PRICELABEL = 0x0000;
        public const ushort ATTR_TIER2PRICELABEL = 0x0001;
        public const ushort ATTR_TIER3PRICELABEL = 0x0002;
        public const ushort ATTR_TIER4PRICELABEL = 0x0003;
        public const ushort ATTR_TIER5PRICELABEL = 0x0004;
        public const ushort ATTR_TIER6PRICELABEL = 0x0005;
        public const ushort ATTR_TIER7PRICELABEL = 0x0006;
        public const ushort ATTR_TIER8PRICELABEL = 0x0007;
        public const ushort ATTR_TIER9PRICELABEL = 0x0008;
        public const ushort ATTR_TIER10PRICELABEL = 0x0009;
        public const ushort ATTR_TIER11PRICELABEL = 0x000A;
        public const ushort ATTR_TIER12PRICELABEL = 0x000B;
        public const ushort ATTR_TIER13PRICELABEL = 0x000C;
        public const ushort ATTR_TIER14PRICELABEL = 0x000D;
        public const ushort ATTR_TIER15PRICELABEL = 0x000E;
        public const ushort ATTR_TIER16PRICELABEL = 0x000F;
        public const ushort ATTR_TIER17PRICELABEL = 0x0010;
        public const ushort ATTR_TIER18PRICELABEL = 0x0011;
        public const ushort ATTR_TIER19PRICELABEL = 0x0012;
        public const ushort ATTR_TIER20PRICELABEL = 0x0013;
        public const ushort ATTR_TIER21PRICELABEL = 0x0014;
        public const ushort ATTR_TIER22PRICELABEL = 0x0015;
        public const ushort ATTR_TIER23PRICELABEL = 0x0016;
        public const ushort ATTR_TIER24PRICELABEL = 0x0017;
        public const ushort ATTR_TIER25PRICELABEL = 0x0018;
        public const ushort ATTR_TIER26PRICELABEL = 0x0019;
        public const ushort ATTR_TIER27PRICELABEL = 0x001A;
        public const ushort ATTR_TIER28PRICELABEL = 0x001B;
        public const ushort ATTR_TIER29PRICELABEL = 0x001C;
        public const ushort ATTR_TIER30PRICELABEL = 0x001D;
        public const ushort ATTR_TIER31PRICELABEL = 0x001E;
        public const ushort ATTR_TIER32PRICELABEL = 0x001F;
        public const ushort ATTR_TIER33PRICELABEL = 0x0020;
        public const ushort ATTR_TIER34PRICELABEL = 0x0021;
        public const ushort ATTR_TIER35PRICELABEL = 0x0022;
        public const ushort ATTR_TIER36PRICELABEL = 0x0023;
        public const ushort ATTR_TIER37PRICELABEL = 0x0024;
        public const ushort ATTR_TIER38PRICELABEL = 0x0025;
        public const ushort ATTR_TIER39PRICELABEL = 0x0026;
        public const ushort ATTR_TIER40PRICELABEL = 0x0027;
        public const ushort ATTR_TIER41PRICELABEL = 0x0028;
        public const ushort ATTR_TIER42PRICELABEL = 0x0029;
        public const ushort ATTR_TIER43PRICELABEL = 0x002A;
        public const ushort ATTR_TIER44PRICELABEL = 0x002B;
        public const ushort ATTR_TIER45PRICELABEL = 0x002C;
        public const ushort ATTR_TIER46PRICELABEL = 0x002D;
        public const ushort ATTR_TIER47PRICELABEL = 0x002E;
        public const ushort ATTR_TIER48PRICELABEL = 0x002F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK1THRESHOLD = 0x0100;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK2THRESHOLD = 0x0101;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK3THRESHOLD = 0x0102;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK4THRESHOLD = 0x0103;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK5THRESHOLD = 0x0104;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK6THRESHOLD = 0x0105;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK7THRESHOLD = 0x0106;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK8THRESHOLD = 0x0107;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK9THRESHOLD = 0x0108;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK10THRESHOLD = 0x0109;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK11THRESHOLD = 0x010A;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK12THRESHOLD = 0x010B;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK13THRESHOLD = 0x010C;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK14THRESHOLD = 0x010D;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_BLOCK15THRESHOLD = 0x010E;

        /// <summary>
        /// Where a single set of thresholds is used, the BlockThresholdCount attribute
        /// indicates the number of applicable BlockNThresholds. Where more than one set of
        /// thresholds is used, each set will be accompanied by an appropriate
        /// TierNBlockThresholdCount attribute.
        /// </summary>
        public const ushort ATTR_BLOCKTHRESHOLDCOUNT = 0x010F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK1THRESHOLD = 0x0110;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK2THRESHOLD = 0x0111;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK3THRESHOLD = 0x0112;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK4THRESHOLD = 0x0113;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK5THRESHOLD = 0x0114;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK6THRESHOLD = 0x0115;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK7THRESHOLD = 0x0116;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK8THRESHOLD = 0x0117;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK9THRESHOLD = 0x0118;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK10THRESHOLD = 0x0119;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK11THRESHOLD = 0x011A;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK12THRESHOLD = 0x011B;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK13THRESHOLD = 0x011C;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK14THRESHOLD = 0x011D;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER1BLOCK15THRESHOLD = 0x011E;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER1BLOCKTHRESHOLDCOUNT = 0x011F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK1THRESHOLD = 0x0120;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK2THRESHOLD = 0x0121;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK3THRESHOLD = 0x0122;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK4THRESHOLD = 0x0123;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK5THRESHOLD = 0x0124;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK6THRESHOLD = 0x0125;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK7THRESHOLD = 0x0126;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK8THRESHOLD = 0x0127;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK9THRESHOLD = 0x0128;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK10THRESHOLD = 0x0129;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK11THRESHOLD = 0x012A;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK12THRESHOLD = 0x012B;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK13THRESHOLD = 0x012C;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK14THRESHOLD = 0x012D;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER2BLOCK15THRESHOLD = 0x012E;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER2BLOCKTHRESHOLDCOUNT = 0x012F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK1THRESHOLD = 0x0130;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK2THRESHOLD = 0x0131;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK3THRESHOLD = 0x0132;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK4THRESHOLD = 0x0133;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK5THRESHOLD = 0x0134;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK6THRESHOLD = 0x0135;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK7THRESHOLD = 0x0136;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK8THRESHOLD = 0x0137;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK9THRESHOLD = 0x0138;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK10THRESHOLD = 0x0139;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK11THRESHOLD = 0x013A;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK12THRESHOLD = 0x013B;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK13THRESHOLD = 0x013C;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK14THRESHOLD = 0x013D;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER3BLOCK15THRESHOLD = 0x013E;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER3BLOCKTHRESHOLDCOUNT = 0x013F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK1THRESHOLD = 0x0140;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK2THRESHOLD = 0x0141;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK3THRESHOLD = 0x0142;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK4THRESHOLD = 0x0143;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK5THRESHOLD = 0x0144;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK6THRESHOLD = 0x0145;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK7THRESHOLD = 0x0146;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK8THRESHOLD = 0x0147;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK9THRESHOLD = 0x0148;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK10THRESHOLD = 0x0149;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK11THRESHOLD = 0x014A;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK12THRESHOLD = 0x014B;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK13THRESHOLD = 0x014C;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK14THRESHOLD = 0x014D;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER4BLOCK15THRESHOLD = 0x014E;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER4BLOCKTHRESHOLDCOUNT = 0x014F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK1THRESHOLD = 0x0150;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK2THRESHOLD = 0x0151;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK3THRESHOLD = 0x0152;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK4THRESHOLD = 0x0153;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK5THRESHOLD = 0x0154;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK6THRESHOLD = 0x0155;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK7THRESHOLD = 0x0156;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK8THRESHOLD = 0x0157;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK9THRESHOLD = 0x0158;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK10THRESHOLD = 0x0159;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK11THRESHOLD = 0x015A;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK12THRESHOLD = 0x015B;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK13THRESHOLD = 0x015C;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK14THRESHOLD = 0x015D;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER5BLOCK15THRESHOLD = 0x015E;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER5BLOCKTHRESHOLDCOUNT = 0x015F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK1THRESHOLD = 0x0160;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK2THRESHOLD = 0x0161;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK3THRESHOLD = 0x0162;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK4THRESHOLD = 0x0163;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK5THRESHOLD = 0x0164;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK6THRESHOLD = 0x0165;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK7THRESHOLD = 0x0166;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK8THRESHOLD = 0x0167;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK9THRESHOLD = 0x0168;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK10THRESHOLD = 0x0169;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK11THRESHOLD = 0x016A;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK12THRESHOLD = 0x016B;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK13THRESHOLD = 0x016C;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK14THRESHOLD = 0x016D;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER6BLOCK15THRESHOLD = 0x016E;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER6BLOCKTHRESHOLDCOUNT = 0x016F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK1THRESHOLD = 0x0170;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK2THRESHOLD = 0x0171;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK3THRESHOLD = 0x0172;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK4THRESHOLD = 0x0173;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK5THRESHOLD = 0x0174;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK6THRESHOLD = 0x0175;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK7THRESHOLD = 0x0176;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK8THRESHOLD = 0x0177;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK9THRESHOLD = 0x0178;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK10THRESHOLD = 0x0179;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK11THRESHOLD = 0x017A;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK12THRESHOLD = 0x017B;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK13THRESHOLD = 0x017C;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK14THRESHOLD = 0x017D;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER7BLOCK15THRESHOLD = 0x017E;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER7BLOCKTHRESHOLDCOUNT = 0x017F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK1THRESHOLD = 0x0180;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK2THRESHOLD = 0x0181;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK3THRESHOLD = 0x0182;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK4THRESHOLD = 0x0183;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK5THRESHOLD = 0x0184;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK6THRESHOLD = 0x0185;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK7THRESHOLD = 0x0186;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK8THRESHOLD = 0x0187;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK9THRESHOLD = 0x0188;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK10THRESHOLD = 0x0189;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK11THRESHOLD = 0x018A;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK12THRESHOLD = 0x018B;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK13THRESHOLD = 0x018C;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK14THRESHOLD = 0x018D;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER8BLOCK15THRESHOLD = 0x018E;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER8BLOCKTHRESHOLDCOUNT = 0x018F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK1THRESHOLD = 0x0190;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK2THRESHOLD = 0x0191;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK3THRESHOLD = 0x0192;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK4THRESHOLD = 0x0193;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK5THRESHOLD = 0x0194;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK6THRESHOLD = 0x0195;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK7THRESHOLD = 0x0196;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK8THRESHOLD = 0x0197;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK9THRESHOLD = 0x0198;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK10THRESHOLD = 0x0199;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK11THRESHOLD = 0x019A;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK12THRESHOLD = 0x019B;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK13THRESHOLD = 0x019C;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK14THRESHOLD = 0x019D;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER9BLOCK15THRESHOLD = 0x019E;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER9BLOCKTHRESHOLDCOUNT = 0x019F;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK1THRESHOLD = 0x01A0;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK2THRESHOLD = 0x01A1;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK3THRESHOLD = 0x01A2;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK4THRESHOLD = 0x01A3;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK5THRESHOLD = 0x01A4;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK6THRESHOLD = 0x01A5;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK7THRESHOLD = 0x01A6;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK8THRESHOLD = 0x01A7;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK9THRESHOLD = 0x01A8;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK10THRESHOLD = 0x01A9;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK11THRESHOLD = 0x01AA;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK12THRESHOLD = 0x01AB;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK13THRESHOLD = 0x01AC;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK14THRESHOLD = 0x01AD;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER10BLOCK15THRESHOLD = 0x01AE;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER10BLOCKTHRESHOLDCOUNT = 0x01AF;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK1THRESHOLD = 0x01B0;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK2THRESHOLD = 0x01B1;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK3THRESHOLD = 0x01B2;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK4THRESHOLD = 0x01B3;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK5THRESHOLD = 0x01B4;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK6THRESHOLD = 0x01B5;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK7THRESHOLD = 0x01B6;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK8THRESHOLD = 0x01B7;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK9THRESHOLD = 0x01B8;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK10THRESHOLD = 0x01B9;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK11THRESHOLD = 0x01BA;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK12THRESHOLD = 0x01BB;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK13THRESHOLD = 0x01BC;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK14THRESHOLD = 0x01BD;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER11BLOCK15THRESHOLD = 0x01BE;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER11BLOCKTHRESHOLDCOUNT = 0x01BF;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK1THRESHOLD = 0x01C0;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK2THRESHOLD = 0x01C1;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK3THRESHOLD = 0x01C2;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK4THRESHOLD = 0x01C3;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK5THRESHOLD = 0x01C4;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK6THRESHOLD = 0x01C5;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK7THRESHOLD = 0x01C6;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK8THRESHOLD = 0x01C7;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK9THRESHOLD = 0x01C8;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK10THRESHOLD = 0x01C9;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK11THRESHOLD = 0x01CA;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK12THRESHOLD = 0x01CB;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK13THRESHOLD = 0x01CC;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK14THRESHOLD = 0x01CD;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER12BLOCK15THRESHOLD = 0x01CE;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER12BLOCKTHRESHOLDCOUNT = 0x01CF;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK1THRESHOLD = 0x01D0;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK2THRESHOLD = 0x01D1;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK3THRESHOLD = 0x01D2;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK4THRESHOLD = 0x01D3;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK5THRESHOLD = 0x01D4;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK6THRESHOLD = 0x01D5;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK7THRESHOLD = 0x01D6;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK8THRESHOLD = 0x01D7;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK9THRESHOLD = 0x01D8;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK10THRESHOLD = 0x01D9;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK11THRESHOLD = 0x01DA;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK12THRESHOLD = 0x01DB;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK13THRESHOLD = 0x01DC;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK14THRESHOLD = 0x01DD;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER13BLOCK15THRESHOLD = 0x01DE;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER13BLOCKTHRESHOLDCOUNT = 0x01DF;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK1THRESHOLD = 0x01E0;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK2THRESHOLD = 0x01E1;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK3THRESHOLD = 0x01E2;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK4THRESHOLD = 0x01E3;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK5THRESHOLD = 0x01E4;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK6THRESHOLD = 0x01E5;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK7THRESHOLD = 0x01E6;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK8THRESHOLD = 0x01E7;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK9THRESHOLD = 0x01E8;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK10THRESHOLD = 0x01E9;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK11THRESHOLD = 0x01EA;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK12THRESHOLD = 0x01EB;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK13THRESHOLD = 0x01EC;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK14THRESHOLD = 0x01ED;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER14BLOCK15THRESHOLD = 0x01EE;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER14BLOCKTHRESHOLDCOUNT = 0x01EF;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK1THRESHOLD = 0x01F0;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK2THRESHOLD = 0x01F1;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK3THRESHOLD = 0x01F2;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK4THRESHOLD = 0x01F3;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK5THRESHOLD = 0x01F4;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK6THRESHOLD = 0x01F5;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK7THRESHOLD = 0x01F6;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK8THRESHOLD = 0x01F7;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK9THRESHOLD = 0x01F8;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK10THRESHOLD = 0x01F9;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK11THRESHOLD = 0x01FA;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK12THRESHOLD = 0x01FB;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK13THRESHOLD = 0x01FC;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK14THRESHOLD = 0x01FD;

        /// <summary>
        /// Attributes Block1Threshold through Block15Threshold represent the block
        /// threshold values for a given period (typically the billing cycle). These values
        /// may be updated by the utility on a seasonal or annual basis. The thresholds are
        /// established such that crossing the threshold of energy consumption for the
        /// present block activates the next higher block, which can affect the energy rate in a
        /// positive or negative manner. The values are absolute and always increasing. The
        /// values represent the threshold at the end of a block. The Unit of Measure will be
        /// based on the
     /// </summary>
        public const ushort ATTR_TIER15BLOCK15THRESHOLD = 0x01FE;

        /// <summary>
        /// The TierNBlockThresholdCount attributes hold the number of block thresholds
        /// applicable to a given tier. These attributes are used in the case when a combination
        /// (TOU/Hybrid) tariff has a separate set of thresholds for each TOU tier. Unused
        /// TierNBlockThresholdCount attributes shall be set to zero.
        /// </summary>
        public const ushort ATTR_TIER15BLOCKTHRESHOLDCOUNT = 0x01FF;

        /// <summary>
        /// The StartofBlockPeriod attribute represents the start time of the current block
        /// tariff period. A change indicates that a new Block Period is in effect.
        /// </summary>
        public const ushort ATTR_STARTOFBLOCKPERIOD = 0x0200;

        /// <summary>
        /// The BlockPeriodDuration attribute represents the current block tariff period
        /// duration in units defined by the BlockPeriodDurationType attribute. A change
        /// indicates that only the duration of the current Block Period has been modified. A
        /// client device shall expect a new Block Period following the expiration of the new
        /// duration.
        /// </summary>
        public const ushort ATTR_BLOCKPERIODDURATION = 0x0201;

        /// <summary>
        /// ThresholdMultiplier provides a value to be multiplied against Threshold
        /// attributes. If present, this attribute must be applied to all Block Threshold
        /// values to derive values that can be compared against the
        /// CurrentBlockPeriodConsumptionDelivered attribute within the Metering
        /// cluster. This attribute must be used in conjunction with the ThresholdDivisor
        /// attribute. An attribute value of zero shall result in a unitary multiplier
        /// (0x000001).
        /// </summary>
        public const ushort ATTR_THRESHOLDMULTIPLIER = 0x0202;

        /// <summary>
        /// ThresholdDivisor provides a value to divide the result of applying the
        /// ThresholdMultiplier attribute to Block Threshold values to derive values that
        /// can be compared against the CurrentBlockPeriodConsumptionDelivered attribute
        /// within the Metering cluster. This attribute must be used in conjunction with the
        /// ThresholdMultiplier attribute. An attribute value of zero shall result in a
        /// unitary divisor (0x000001).
        /// </summary>
        public const ushort ATTR_THRESHOLDDIVISOR = 0x0203;

        /// <summary>
        /// The BlockPeriodDurationType attribute indicates the timebase used for the
        /// BlockPeriodDuration attribute. A default value of 0x00 (Minutes) shall be
        /// assumed if this attribute is not present.
        /// </summary>
        public const ushort ATTR_BLOCKPERIODDURATIONTYPE = 0x0204;

        /// <summary>
        /// CommodityType provides a label for identifying the type of pricing CLIENT
        /// present. The attribute is an enumerated value representing the commodity.
        /// </summary>
        public const ushort ATTR_COMMODITYTYPESERVER = 0x0300;

        /// <summary>
        /// The value of the Standing Charge is a daily fixed charge associated with supplying
        /// the commodity, measured in base unit of Currency with the decimal point located as
        /// indicated by the Trailing Digits field of a Publish Price command or
        /// PriceTrailingDigit attribute. A value of 0xFFFFFFFF indicates attribute not
        /// used.
        /// </summary>
        public const ushort ATTR_STANDINGCHARGE = 0x0301;
        public const ushort ATTR_CONVERSIONFACTOR = 0x0302;
        public const ushort ATTR_CONVERSIONFACTORTRAILINGDIGIT = 0x0303;
        public const ushort ATTR_CALORIFICVALUE = 0x0304;
        public const ushort ATTR_CALORIFICVALUEUNIT = 0x0305;
        public const ushort ATTR_CALORIFICVALUETRAILINGDIGIT = 0x0306;
        public const ushort ATTR_NOTIERBLOCK1PRICE = 0x0400;
        public const ushort ATTR_NOTIERBLOCK2PRICE = 0x0401;
        public const ushort ATTR_NOTIERBLOCK3PRICE = 0x0402;
        public const ushort ATTR_NOTIERBLOCK4PRICE = 0x0403;
        public const ushort ATTR_NOTIERBLOCK5PRICE = 0x0404;
        public const ushort ATTR_NOTIERBLOCK6PRICE = 0x0405;
        public const ushort ATTR_NOTIERBLOCK7PRICE = 0x0406;
        public const ushort ATTR_NOTIERBLOCK8PRICE = 0x0407;
        public const ushort ATTR_NOTIERBLOCK9PRICE = 0x0408;
        public const ushort ATTR_NOTIERBLOCK10PRICE = 0x0409;
        public const ushort ATTR_NOTIERBLOCK11PRICE = 0x040A;
        public const ushort ATTR_NOTIERBLOCK12PRICE = 0x040B;
        public const ushort ATTR_NOTIERBLOCK13PRICE = 0x040C;
        public const ushort ATTR_NOTIERBLOCK14PRICE = 0x040D;
        public const ushort ATTR_NOTIERBLOCK15PRICE = 0x040E;
        public const ushort ATTR_NOTIERBLOCK16PRICE = 0x040F;
        public const ushort ATTR_TIER1BLOCK1PRICE = 0x0410;
        public const ushort ATTR_TIER1BLOCK2PRICE = 0x0411;
        public const ushort ATTR_TIER1BLOCK3PRICE = 0x0412;
        public const ushort ATTR_TIER1BLOCK4PRICE = 0x0413;
        public const ushort ATTR_TIER1BLOCK5PRICE = 0x0414;
        public const ushort ATTR_TIER1BLOCK6PRICE = 0x0415;
        public const ushort ATTR_TIER1BLOCK7PRICE = 0x0416;
        public const ushort ATTR_TIER1BLOCK8PRICE = 0x0417;
        public const ushort ATTR_TIER1BLOCK9PRICE = 0x0418;
        public const ushort ATTR_TIER1BLOCK10PRICE = 0x0419;
        public const ushort ATTR_TIER1BLOCK11PRICE = 0x041A;
        public const ushort ATTR_TIER1BLOCK12PRICE = 0x041B;
        public const ushort ATTR_TIER1BLOCK13PRICE = 0x041C;
        public const ushort ATTR_TIER1BLOCK14PRICE = 0x041D;
        public const ushort ATTR_TIER1BLOCK15PRICE = 0x041E;
        public const ushort ATTR_TIER1BLOCK16PRICE = 0x041F;
        public const ushort ATTR_TIER2BLOCK1PRICE = 0x0420;
        public const ushort ATTR_TIER2BLOCK2PRICE = 0x0421;
        public const ushort ATTR_TIER2BLOCK3PRICE = 0x0422;
        public const ushort ATTR_TIER2BLOCK4PRICE = 0x0423;
        public const ushort ATTR_TIER2BLOCK5PRICE = 0x0424;
        public const ushort ATTR_TIER2BLOCK6PRICE = 0x0425;
        public const ushort ATTR_TIER2BLOCK7PRICE = 0x0426;
        public const ushort ATTR_TIER2BLOCK8PRICE = 0x0427;
        public const ushort ATTR_TIER2BLOCK9PRICE = 0x0428;
        public const ushort ATTR_TIER2BLOCK10PRICE = 0x0429;
        public const ushort ATTR_TIER2BLOCK11PRICE = 0x042A;
        public const ushort ATTR_TIER2BLOCK12PRICE = 0x042B;
        public const ushort ATTR_TIER2BLOCK13PRICE = 0x042C;
        public const ushort ATTR_TIER2BLOCK14PRICE = 0x042D;
        public const ushort ATTR_TIER2BLOCK15PRICE = 0x042E;
        public const ushort ATTR_TIER2BLOCK16PRICE = 0x042F;
        public const ushort ATTR_TIER3BLOCK1PRICE = 0x0430;
        public const ushort ATTR_TIER3BLOCK2PRICE = 0x0431;
        public const ushort ATTR_TIER3BLOCK3PRICE = 0x0432;
        public const ushort ATTR_TIER3BLOCK4PRICE = 0x0433;
        public const ushort ATTR_TIER3BLOCK5PRICE = 0x0434;
        public const ushort ATTR_TIER3BLOCK6PRICE = 0x0435;
        public const ushort ATTR_TIER3BLOCK7PRICE = 0x0436;
        public const ushort ATTR_TIER3BLOCK8PRICE = 0x0437;
        public const ushort ATTR_TIER3BLOCK9PRICE = 0x0438;
        public const ushort ATTR_TIER3BLOCK10PRICE = 0x0439;
        public const ushort ATTR_TIER3BLOCK11PRICE = 0x043A;
        public const ushort ATTR_TIER3BLOCK12PRICE = 0x043B;
        public const ushort ATTR_TIER3BLOCK13PRICE = 0x043C;
        public const ushort ATTR_TIER3BLOCK14PRICE = 0x043D;
        public const ushort ATTR_TIER3BLOCK15PRICE = 0x043E;
        public const ushort ATTR_TIER3BLOCK16PRICE = 0x043F;
        public const ushort ATTR_TIER4BLOCK1PRICE = 0x0440;
        public const ushort ATTR_TIER4BLOCK2PRICE = 0x0441;
        public const ushort ATTR_TIER4BLOCK3PRICE = 0x0442;
        public const ushort ATTR_TIER4BLOCK4PRICE = 0x0443;
        public const ushort ATTR_TIER4BLOCK5PRICE = 0x0444;
        public const ushort ATTR_TIER4BLOCK6PRICE = 0x0445;
        public const ushort ATTR_TIER4BLOCK7PRICE = 0x0446;
        public const ushort ATTR_TIER4BLOCK8PRICE = 0x0447;
        public const ushort ATTR_TIER4BLOCK9PRICE = 0x0448;
        public const ushort ATTR_TIER4BLOCK10PRICE = 0x0449;
        public const ushort ATTR_TIER4BLOCK11PRICE = 0x044A;
        public const ushort ATTR_TIER4BLOCK12PRICE = 0x044B;
        public const ushort ATTR_TIER4BLOCK13PRICE = 0x044C;
        public const ushort ATTR_TIER4BLOCK14PRICE = 0x044D;
        public const ushort ATTR_TIER4BLOCK15PRICE = 0x044E;
        public const ushort ATTR_TIER4BLOCK16PRICE = 0x044F;
        public const ushort ATTR_TIER5BLOCK1PRICE = 0x0450;
        public const ushort ATTR_TIER5BLOCK2PRICE = 0x0451;
        public const ushort ATTR_TIER5BLOCK3PRICE = 0x0452;
        public const ushort ATTR_TIER5BLOCK4PRICE = 0x0453;
        public const ushort ATTR_TIER5BLOCK5PRICE = 0x0454;
        public const ushort ATTR_TIER5BLOCK6PRICE = 0x0455;
        public const ushort ATTR_TIER5BLOCK7PRICE = 0x0456;
        public const ushort ATTR_TIER5BLOCK8PRICE = 0x0457;
        public const ushort ATTR_TIER5BLOCK9PRICE = 0x0458;
        public const ushort ATTR_TIER5BLOCK10PRICE = 0x0459;
        public const ushort ATTR_TIER5BLOCK11PRICE = 0x045A;
        public const ushort ATTR_TIER5BLOCK12PRICE = 0x045B;
        public const ushort ATTR_TIER5BLOCK13PRICE = 0x045C;
        public const ushort ATTR_TIER5BLOCK14PRICE = 0x045D;
        public const ushort ATTR_TIER5BLOCK15PRICE = 0x045E;
        public const ushort ATTR_TIER5BLOCK16PRICE = 0x045F;
        public const ushort ATTR_TIER6BLOCK1PRICE = 0x0460;
        public const ushort ATTR_TIER6BLOCK2PRICE = 0x0461;
        public const ushort ATTR_TIER6BLOCK3PRICE = 0x0462;
        public const ushort ATTR_TIER6BLOCK4PRICE = 0x0463;
        public const ushort ATTR_TIER6BLOCK5PRICE = 0x0464;
        public const ushort ATTR_TIER6BLOCK6PRICE = 0x0465;
        public const ushort ATTR_TIER6BLOCK7PRICE = 0x0466;
        public const ushort ATTR_TIER6BLOCK8PRICE = 0x0467;
        public const ushort ATTR_TIER6BLOCK9PRICE = 0x0468;
        public const ushort ATTR_TIER6BLOCK10PRICE = 0x0469;
        public const ushort ATTR_TIER6BLOCK11PRICE = 0x046A;
        public const ushort ATTR_TIER6BLOCK12PRICE = 0x046B;
        public const ushort ATTR_TIER6BLOCK13PRICE = 0x046C;
        public const ushort ATTR_TIER6BLOCK14PRICE = 0x046D;
        public const ushort ATTR_TIER6BLOCK15PRICE = 0x046E;
        public const ushort ATTR_TIER6BLOCK16PRICE = 0x046F;
        public const ushort ATTR_TIER7BLOCK1PRICE = 0x0470;
        public const ushort ATTR_TIER7BLOCK2PRICE = 0x0471;
        public const ushort ATTR_TIER7BLOCK3PRICE = 0x0472;
        public const ushort ATTR_TIER7BLOCK4PRICE = 0x0473;
        public const ushort ATTR_TIER7BLOCK5PRICE = 0x0474;
        public const ushort ATTR_TIER7BLOCK6PRICE = 0x0475;
        public const ushort ATTR_TIER7BLOCK7PRICE = 0x0476;
        public const ushort ATTR_TIER7BLOCK8PRICE = 0x0477;
        public const ushort ATTR_TIER7BLOCK9PRICE = 0x0478;
        public const ushort ATTR_TIER7BLOCK10PRICE = 0x0479;
        public const ushort ATTR_TIER7BLOCK11PRICE = 0x047A;
        public const ushort ATTR_TIER7BLOCK12PRICE = 0x047B;
        public const ushort ATTR_TIER7BLOCK13PRICE = 0x047C;
        public const ushort ATTR_TIER7BLOCK14PRICE = 0x047D;
        public const ushort ATTR_TIER7BLOCK15PRICE = 0x047E;
        public const ushort ATTR_TIER7BLOCK16PRICE = 0x047F;
        public const ushort ATTR_TIER8BLOCK1PRICE = 0x0480;
        public const ushort ATTR_TIER8BLOCK2PRICE = 0x0481;
        public const ushort ATTR_TIER8BLOCK3PRICE = 0x0482;
        public const ushort ATTR_TIER8BLOCK4PRICE = 0x0483;
        public const ushort ATTR_TIER8BLOCK5PRICE = 0x0484;
        public const ushort ATTR_TIER8BLOCK6PRICE = 0x0485;
        public const ushort ATTR_TIER8BLOCK7PRICE = 0x0486;
        public const ushort ATTR_TIER8BLOCK8PRICE = 0x0487;
        public const ushort ATTR_TIER8BLOCK9PRICE = 0x0488;
        public const ushort ATTR_TIER8BLOCK10PRICE = 0x0489;
        public const ushort ATTR_TIER8BLOCK11PRICE = 0x048A;
        public const ushort ATTR_TIER8BLOCK12PRICE = 0x048B;
        public const ushort ATTR_TIER8BLOCK13PRICE = 0x048C;
        public const ushort ATTR_TIER8BLOCK14PRICE = 0x048D;
        public const ushort ATTR_TIER8BLOCK15PRICE = 0x048E;
        public const ushort ATTR_TIER8BLOCK16PRICE = 0x048F;
        public const ushort ATTR_TIER9BLOCK1PRICE = 0x0490;
        public const ushort ATTR_TIER9BLOCK2PRICE = 0x0491;
        public const ushort ATTR_TIER9BLOCK3PRICE = 0x0492;
        public const ushort ATTR_TIER9BLOCK4PRICE = 0x0493;
        public const ushort ATTR_TIER9BLOCK5PRICE = 0x0494;
        public const ushort ATTR_TIER9BLOCK6PRICE = 0x0495;
        public const ushort ATTR_TIER9BLOCK7PRICE = 0x0496;
        public const ushort ATTR_TIER9BLOCK8PRICE = 0x0497;
        public const ushort ATTR_TIER9BLOCK9PRICE = 0x0498;
        public const ushort ATTR_TIER9BLOCK10PRICE = 0x0499;
        public const ushort ATTR_TIER9BLOCK11PRICE = 0x049A;
        public const ushort ATTR_TIER9BLOCK12PRICE = 0x049B;
        public const ushort ATTR_TIER9BLOCK13PRICE = 0x049C;
        public const ushort ATTR_TIER9BLOCK14PRICE = 0x049D;
        public const ushort ATTR_TIER9BLOCK15PRICE = 0x049E;
        public const ushort ATTR_TIER9BLOCK16PRICE = 0x049F;
        public const ushort ATTR_TIER10BLOCK1PRICE = 0x04A0;
        public const ushort ATTR_TIER10BLOCK2PRICE = 0x04A1;
        public const ushort ATTR_TIER10BLOCK3PRICE = 0x04A2;
        public const ushort ATTR_TIER10BLOCK4PRICE = 0x04A3;
        public const ushort ATTR_TIER10BLOCK5PRICE = 0x04A4;
        public const ushort ATTR_TIER10BLOCK6PRICE = 0x04A5;
        public const ushort ATTR_TIER10BLOCK7PRICE = 0x04A6;
        public const ushort ATTR_TIER10BLOCK8PRICE = 0x04A7;
        public const ushort ATTR_TIER10BLOCK9PRICE = 0x04A8;
        public const ushort ATTR_TIER10BLOCK10PRICE = 0x04A9;
        public const ushort ATTR_TIER10BLOCK11PRICE = 0x04AA;
        public const ushort ATTR_TIER10BLOCK12PRICE = 0x04AB;
        public const ushort ATTR_TIER10BLOCK13PRICE = 0x04AC;
        public const ushort ATTR_TIER10BLOCK14PRICE = 0x04AD;
        public const ushort ATTR_TIER10BLOCK15PRICE = 0x04AE;
        public const ushort ATTR_TIER10BLOCK16PRICE = 0x04AF;
        public const ushort ATTR_TIER11BLOCK1PRICE = 0x04B0;
        public const ushort ATTR_TIER11BLOCK2PRICE = 0x04B1;
        public const ushort ATTR_TIER11BLOCK3PRICE = 0x04B2;
        public const ushort ATTR_TIER11BLOCK4PRICE = 0x04B3;
        public const ushort ATTR_TIER11BLOCK5PRICE = 0x04B4;
        public const ushort ATTR_TIER11BLOCK6PRICE = 0x04B5;
        public const ushort ATTR_TIER11BLOCK7PRICE = 0x04B6;
        public const ushort ATTR_TIER11BLOCK8PRICE = 0x04B7;
        public const ushort ATTR_TIER11BLOCK9PRICE = 0x04B8;
        public const ushort ATTR_TIER11BLOCK10PRICE = 0x04B9;
        public const ushort ATTR_TIER11BLOCK11PRICE = 0x04BA;
        public const ushort ATTR_TIER11BLOCK12PRICE = 0x04BB;
        public const ushort ATTR_TIER11BLOCK13PRICE = 0x04BC;
        public const ushort ATTR_TIER11BLOCK14PRICE = 0x04BD;
        public const ushort ATTR_TIER11BLOCK15PRICE = 0x04BE;
        public const ushort ATTR_TIER11BLOCK16PRICE = 0x04BF;
        public const ushort ATTR_TIER12BLOCK1PRICE = 0x04C0;
        public const ushort ATTR_TIER12BLOCK2PRICE = 0x04C1;
        public const ushort ATTR_TIER12BLOCK3PRICE = 0x04C2;
        public const ushort ATTR_TIER12BLOCK4PRICE = 0x04C3;
        public const ushort ATTR_TIER12BLOCK5PRICE = 0x04C4;
        public const ushort ATTR_TIER12BLOCK6PRICE = 0x04C5;
        public const ushort ATTR_TIER12BLOCK7PRICE = 0x04C6;
        public const ushort ATTR_TIER12BLOCK8PRICE = 0x04C7;
        public const ushort ATTR_TIER12BLOCK9PRICE = 0x04C8;
        public const ushort ATTR_TIER12BLOCK10PRICE = 0x04C9;
        public const ushort ATTR_TIER12BLOCK11PRICE = 0x04CA;
        public const ushort ATTR_TIER12BLOCK12PRICE = 0x04CB;
        public const ushort ATTR_TIER12BLOCK13PRICE = 0x04CC;
        public const ushort ATTR_TIER12BLOCK14PRICE = 0x04CD;
        public const ushort ATTR_TIER12BLOCK15PRICE = 0x04CE;
        public const ushort ATTR_TIER12BLOCK16PRICE = 0x04CF;
        public const ushort ATTR_TIER13BLOCK1PRICE = 0x04D0;
        public const ushort ATTR_TIER13BLOCK2PRICE = 0x04D1;
        public const ushort ATTR_TIER13BLOCK3PRICE = 0x04D2;
        public const ushort ATTR_TIER13BLOCK4PRICE = 0x04D3;
        public const ushort ATTR_TIER13BLOCK5PRICE = 0x04D4;
        public const ushort ATTR_TIER13BLOCK6PRICE = 0x04D5;
        public const ushort ATTR_TIER13BLOCK7PRICE = 0x04D6;
        public const ushort ATTR_TIER13BLOCK8PRICE = 0x04D7;
        public const ushort ATTR_TIER13BLOCK9PRICE = 0x04D8;
        public const ushort ATTR_TIER13BLOCK10PRICE = 0x04D9;
        public const ushort ATTR_TIER13BLOCK11PRICE = 0x04DA;
        public const ushort ATTR_TIER13BLOCK12PRICE = 0x04DB;
        public const ushort ATTR_TIER13BLOCK13PRICE = 0x04DC;
        public const ushort ATTR_TIER13BLOCK14PRICE = 0x04DD;
        public const ushort ATTR_TIER13BLOCK15PRICE = 0x04DE;
        public const ushort ATTR_TIER13BLOCK16PRICE = 0x04DF;
        public const ushort ATTR_TIER14BLOCK1PRICE = 0x04E0;
        public const ushort ATTR_TIER14BLOCK2PRICE = 0x04E1;
        public const ushort ATTR_TIER14BLOCK3PRICE = 0x04E2;
        public const ushort ATTR_TIER14BLOCK4PRICE = 0x04E3;
        public const ushort ATTR_TIER14BLOCK5PRICE = 0x04E4;
        public const ushort ATTR_TIER14BLOCK6PRICE = 0x04E5;
        public const ushort ATTR_TIER14BLOCK7PRICE = 0x04E6;
        public const ushort ATTR_TIER14BLOCK8PRICE = 0x04E7;
        public const ushort ATTR_TIER14BLOCK9PRICE = 0x04E8;
        public const ushort ATTR_TIER14BLOCK10PRICE = 0x04E9;
        public const ushort ATTR_TIER14BLOCK11PRICE = 0x04EA;
        public const ushort ATTR_TIER14BLOCK12PRICE = 0x04EB;
        public const ushort ATTR_TIER14BLOCK13PRICE = 0x04EC;
        public const ushort ATTR_TIER14BLOCK14PRICE = 0x04ED;
        public const ushort ATTR_TIER14BLOCK15PRICE = 0x04EE;
        public const ushort ATTR_TIER14BLOCK16PRICE = 0x04EF;
        public const ushort ATTR_TIER15BLOCK1PRICE = 0x04F0;
        public const ushort ATTR_TIER15BLOCK2PRICE = 0x04F1;
        public const ushort ATTR_TIER15BLOCK3PRICE = 0x04F2;
        public const ushort ATTR_TIER15BLOCK4PRICE = 0x04F3;
        public const ushort ATTR_TIER15BLOCK5PRICE = 0x04F4;
        public const ushort ATTR_TIER15BLOCK6PRICE = 0x04F5;
        public const ushort ATTR_TIER15BLOCK7PRICE = 0x04F6;
        public const ushort ATTR_TIER15BLOCK8PRICE = 0x04F7;
        public const ushort ATTR_TIER15BLOCK9PRICE = 0x04F8;
        public const ushort ATTR_TIER15BLOCK10PRICE = 0x04F9;
        public const ushort ATTR_TIER15BLOCK11PRICE = 0x04FA;
        public const ushort ATTR_TIER15BLOCK12PRICE = 0x04FB;
        public const ushort ATTR_TIER15BLOCK13PRICE = 0x04FC;
        public const ushort ATTR_TIER15BLOCK14PRICE = 0x04FD;
        public const ushort ATTR_TIER15BLOCK15PRICE = 0x04FE;
        public const ushort ATTR_TIER15BLOCK16PRICE = 0x04FF;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER16 = 0x050F;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER17 = 0x0510;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER18 = 0x0511;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER19 = 0x0512;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER20 = 0x0513;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER21 = 0x0514;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER22 = 0x0515;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER23 = 0x0516;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER24 = 0x0517;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER25 = 0x0518;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER26 = 0x0519;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER27 = 0x051A;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER28 = 0x051B;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER29 = 0x051C;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER30 = 0x051D;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER31 = 0x051E;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER32 = 0x051F;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER33 = 0x0520;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER34 = 0x0521;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER35 = 0x0522;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER36 = 0x0523;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER37 = 0x0524;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER38 = 0x0525;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER39 = 0x0526;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER40 = 0x0527;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER41 = 0x0528;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER42 = 0x0529;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER43 = 0x052A;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER44 = 0x052B;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER45 = 0x052C;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER46 = 0x052D;

        /// <summary>
        /// Attributes PriceTier16 through PriceTier48 represent the price of Energy, Gas,
        /// or Water delivered to the premises (i.e. delivered to the customer from the
        /// utility) at a specific price tier.
     /// </summary>
        public const ushort ATTR_PRICETIER47 = 0x052E;

        /// <summary>
        /// Attribute CPP1 Price represents the price of Energy, Gas, or Water delivered to the
        /// premises (i.e. delivered to the customer from the utility) while Critical Peak
        /// Pricing ‘CPP1’ is being applied.
        /// </summary>
        public const ushort ATTR_CPP1PRICE = 0x05FE;

        /// <summary>
        /// Attribute CPP2 Price represents the price of Energy, Gas, or Water delivered to the
        /// premises (i.e. delivered to the customer from the utility) while Critical Peak
        /// Pricing ‘CPP2’ is being applied.
        /// </summary>
        public const ushort ATTR_CPP2PRICE = 0x05FF;
        public const ushort ATTR_TARIFFLABEL = 0x0610;
        public const ushort ATTR_NUMBEROFPRICETIERSINUSE = 0x0611;
        public const ushort ATTR_NUMBEROFBLOCKTHRESHOLDSINUSE = 0x0612;
        public const ushort ATTR_TIERBLOCKMODE = 0x0613;
        public const ushort ATTR_UNITOFMEASURE = 0x0615;
        public const ushort ATTR_CURRENCY = 0x0616;
        public const ushort ATTR_PRICETRAILINGDIGIT = 0x0617;

        /// <summary>
        /// An 8 bit enumeration identifying the resolution period for Block Tariff
        /// </summary>
        public const ushort ATTR_TARIFFRESOLUTIONPERIOD = 0x0619;

        /// <summary>
        /// Used to calculate the amount of carbon dioxide (CO2) produced from energy use.
        /// Natural gas has a conversion factor of about 0.185, e.g. 1,000 kWh of gas used is
        /// responsible for the production of 185kg CO2 (0.185 x 1000 kWh). The CO2 attribute
        /// represents the current active value
        /// </summary>
        public const ushort ATTR_CO2 = 0x0620;

        /// <summary>
        /// This attribute is an 8-bit enumeration which defines the unit for the CO2
        /// attribute. The values and descriptions for this attribute are listed in Table D-83
        /// below. The CO2Unit attribute represents the current active value.
        /// </summary>
        public const ushort ATTR_CO2UNIT = 0x0621;
        public const ushort ATTR_CO2TRAILINGDIGIT = 0x0622;

        /// <summary>
        /// The CurrentBillingPeriodStart attribute represents the start time of the
        /// current billing period.
        /// </summary>
        public const ushort ATTR_CURRENTBILLINGPERIODSTART = 0x0700;

        /// <summary>
        /// The CurrentBillingPeriodDuration attribute represents the current billing
        /// period duration in minutes.
        /// </summary>
        public const ushort ATTR_CURRENTBILLINGPERIODDURATION = 0x0701;

        /// <summary>
        /// The LastBillingPeriodStart attribute represents the start time of the last
        /// billing period.
        /// </summary>
        public const ushort ATTR_LASTBILLINGPERIODSTART = 0x0702;

        /// <summary>
        /// The LastBillingPeriodDuration attribute is the duration of the last billing
        /// period in minutes (start to end of last billing period).
        /// </summary>
        public const ushort ATTR_LASTBILLINGPERIODDURATION = 0x0703;

        /// <summary>
        /// The LastBillingPeriodConsolidatedBill attribute is an amount for the cost of the
        /// energy supplied from the date of the LastBillingPeriodStart attribute and until
        /// the duration of the LastBillingPeriodDuration attribute expires, measured in
        /// base unit of Currency with the decimal point located as indicated by the Trailing
        /// Digits attribute.
        /// </summary>
        public const ushort ATTR_LASTBILLINGPERIODCONSOLIDATEDBILL = 0x0704;

        /// <summary>
        /// The CreditPaymentDueDate attribute indicates the date and time when the next
        /// credit payment is due to be paid by the consumer to the supplier.
        /// </summary>
        public const ushort ATTR_CREDITPAYMENTDUEDATE = 0x0800;

        /// <summary>
        /// The CreditPaymentStatus attribute indicates the current status of the last
        /// payment.
        /// </summary>
        public const ushort ATTR_CREDITPAYMENTSTATUS = 0x0801;

        /// <summary>
        /// This is the total of the consolidated bill amounts accumulated since the last
        /// payment.
        /// </summary>
        public const ushort ATTR_CREDITPAYMENTOVERDUEAMOUNT = 0x0802;

        /// <summary>
        /// The PaymentDiscount attribute indicates the discount that the energy supplier
        /// has applied to the consolidated bill.
        /// </summary>
        public const ushort ATTR_PAYMENTDISCOUNT = 0x080A;

        /// <summary>
        /// The PaymentDiscountPeriod attribute indicates the period for which this
        /// discount shall be applied for.
        /// </summary>
        public const ushort ATTR_PAYMENTDISCOUNTPERIOD = 0x080B;
        public const ushort ATTR_CREDITCARDPAYMENT1 = 0x0810;
        public const ushort ATTR_CREDITCARDPAYMENTDATE1 = 0x0811;
        public const ushort ATTR_CREDITCARDPAYMENTREF1 = 0x0812;
        public const ushort ATTR_CREDITCARDPAYMENT2 = 0x0820;
        public const ushort ATTR_CREDITCARDPAYMENTDATE2 = 0x0821;
        public const ushort ATTR_CREDITCARDPAYMENTREF2 = 0x0822;
        public const ushort ATTR_CREDITCARDPAYMENT3 = 0x0830;
        public const ushort ATTR_CREDITCARDPAYMENTDATE3 = 0x0831;
        public const ushort ATTR_CREDITCARDPAYMENTREF3 = 0x0832;
        public const ushort ATTR_CREDITCARDPAYMENT4 = 0x0840;
        public const ushort ATTR_CREDITCARDPAYMENTDATE4 = 0x0841;
        public const ushort ATTR_CREDITCARDPAYMENTREF4 = 0x0842;
        public const ushort ATTR_CREDITCARDPAYMENT5 = 0x0850;
        public const ushort ATTR_CREDITCARDPAYMENTDATE5 = 0x0851;
        public const ushort ATTR_CREDITCARDPAYMENTREF5 = 0x0852;
        public const ushort ATTR_RECEIVEDTIER1PRICELABEL = 0x8000;
        public const ushort ATTR_RECEIVEDTIER2PRICELABEL = 0x8001;
        public const ushort ATTR_RECEIVEDTIER3PRICELABEL = 0x8002;
        public const ushort ATTR_RECEIVEDTIER4PRICELABEL = 0x8003;
        public const ushort ATTR_RECEIVEDTIER5PRICELABEL = 0x8004;
        public const ushort ATTR_RECEIVEDTIER6PRICELABEL = 0x8005;
        public const ushort ATTR_RECEIVEDTIER7PRICELABEL = 0x8006;
        public const ushort ATTR_RECEIVEDTIER8PRICELABEL = 0x8007;
        public const ushort ATTR_RECEIVEDTIER9PRICELABEL = 0x8008;
        public const ushort ATTR_RECEIVEDTIER10PRICELABEL = 0x8009;
        public const ushort ATTR_RECEIVEDTIER11PRICELABEL = 0x800A;
        public const ushort ATTR_RECEIVEDTIER12PRICELABEL = 0x800B;
        public const ushort ATTR_RECEIVEDTIER13PRICELABEL = 0x800C;
        public const ushort ATTR_RECEIVEDTIER14PRICELABEL = 0x800D;
        public const ushort ATTR_RECEIVEDTIER15PRICELABEL = 0x800E;
        public const ushort ATTR_RECEIVEDTIER16PRICELABEL = 0x800F;
        public const ushort ATTR_RECEIVEDTIER17PRICELABEL = 0x8010;
        public const ushort ATTR_RECEIVEDTIER18PRICELABEL = 0x8011;
        public const ushort ATTR_RECEIVEDTIER19PRICELABEL = 0x8012;
        public const ushort ATTR_RECEIVEDTIER20PRICELABEL = 0x8013;
        public const ushort ATTR_RECEIVEDTIER21PRICELABEL = 0x8014;
        public const ushort ATTR_RECEIVEDTIER22PRICELABEL = 0x8015;
        public const ushort ATTR_RECEIVEDTIER23PRICELABEL = 0x8016;
        public const ushort ATTR_RECEIVEDTIER24PRICELABEL = 0x8017;
        public const ushort ATTR_RECEIVEDTIER25PRICELABEL = 0x8018;
        public const ushort ATTR_RECEIVEDTIER26PRICELABEL = 0x8019;
        public const ushort ATTR_RECEIVEDTIER27PRICELABEL = 0x801A;
        public const ushort ATTR_RECEIVEDTIER28PRICELABEL = 0x801B;
        public const ushort ATTR_RECEIVEDTIER29PRICELABEL = 0x801C;
        public const ushort ATTR_RECEIVEDTIER30PRICELABEL = 0x801D;
        public const ushort ATTR_RECEIVEDTIER31PRICELABEL = 0x801E;
        public const ushort ATTR_RECEIVEDTIER32PRICELABEL = 0x801F;
        public const ushort ATTR_RECEIVEDTIER33PRICELABEL = 0x8020;
        public const ushort ATTR_RECEIVEDTIER34PRICELABEL = 0x8021;
        public const ushort ATTR_RECEIVEDTIER35PRICELABEL = 0x8022;
        public const ushort ATTR_RECEIVEDTIER36PRICELABEL = 0x8023;
        public const ushort ATTR_RECEIVEDTIER37PRICELABEL = 0x8024;
        public const ushort ATTR_RECEIVEDTIER38PRICELABEL = 0x8025;
        public const ushort ATTR_RECEIVEDTIER39PRICELABEL = 0x8026;
        public const ushort ATTR_RECEIVEDTIER40PRICELABEL = 0x8027;
        public const ushort ATTR_RECEIVEDTIER41PRICELABEL = 0x8028;
        public const ushort ATTR_RECEIVEDTIER42PRICELABEL = 0x8029;
        public const ushort ATTR_RECEIVEDTIER43PRICELABEL = 0x802A;
        public const ushort ATTR_RECEIVEDTIER44PRICELABEL = 0x802B;
        public const ushort ATTR_RECEIVEDTIER45PRICELABEL = 0x802C;
        public const ushort ATTR_RECEIVEDTIER46PRICELABEL = 0x802D;
        public const ushort ATTR_RECEIVEDTIER47PRICELABEL = 0x802E;
        public const ushort ATTR_RECEIVEDTIER48PRICELABEL = 0x802F;
        public const ushort ATTR_RECEIVEDBLOCK1THRESHOLD = 0x8100;
        public const ushort ATTR_RECEIVEDBLOCK2THRESHOLD = 0x8101;
        public const ushort ATTR_RECEIVEDBLOCK3THRESHOLD = 0x8102;
        public const ushort ATTR_RECEIVEDBLOCK4THRESHOLD = 0x8103;
        public const ushort ATTR_RECEIVEDBLOCK5THRESHOLD = 0x8104;
        public const ushort ATTR_RECEIVEDBLOCK6THRESHOLD = 0x8105;
        public const ushort ATTR_RECEIVEDBLOCK7THRESHOLD = 0x8106;
        public const ushort ATTR_RECEIVEDBLOCK8THRESHOLD = 0x8107;
        public const ushort ATTR_RECEIVEDBLOCK9THRESHOLD = 0x8108;
        public const ushort ATTR_RECEIVEDBLOCK10THRESHOLD = 0x8109;
        public const ushort ATTR_RECEIVEDBLOCK11THRESHOLD = 0x810A;
        public const ushort ATTR_RECEIVEDBLOCK12THRESHOLD = 0x810B;
        public const ushort ATTR_RECEIVEDBLOCK13THRESHOLD = 0x810C;
        public const ushort ATTR_RECEIVEDBLOCK14THRESHOLD = 0x810D;
        public const ushort ATTR_RECEIVEDBLOCK15THRESHOLD = 0x810E;
        public const ushort ATTR_RECEIVEDBLOCK16THRESHOLD = 0x810F;
        public const ushort ATTR_RECEIVEDSTARTOFBLOCKPERIOD = 0x8200;
        public const ushort ATTR_RECEIVEDBLOCKPERIODDURATION = 0x8201;
        public const ushort ATTR_RECEIVEDTHRESHOLDMULTIPLIER = 0x8202;
        public const ushort ATTR_RECEIVEDTHRESHOLDDIVISOR = 0x8203;
        public const ushort ATTR_RXNOTIERBLOCK1PRICE = 0x8400;
        public const ushort ATTR_RXNOTIERBLOCK2PRICE = 0x8401;
        public const ushort ATTR_RXNOTIERBLOCK3PRICE = 0x8402;
        public const ushort ATTR_RXNOTIERBLOCK4PRICE = 0x8403;
        public const ushort ATTR_RXNOTIERBLOCK5PRICE = 0x8404;
        public const ushort ATTR_RXNOTIERBLOCK6PRICE = 0x8405;
        public const ushort ATTR_RXNOTIERBLOCK7PRICE = 0x8406;
        public const ushort ATTR_RXNOTIERBLOCK8PRICE = 0x8407;
        public const ushort ATTR_RXNOTIERBLOCK9PRICE = 0x8408;
        public const ushort ATTR_RXNOTIERBLOCK10PRICE = 0x8409;
        public const ushort ATTR_RXNOTIERBLOCK11PRICE = 0x840A;
        public const ushort ATTR_RXNOTIERBLOCK12PRICE = 0x840B;
        public const ushort ATTR_RXNOTIERBLOCK13PRICE = 0x840C;
        public const ushort ATTR_RXNOTIERBLOCK14PRICE = 0x840D;
        public const ushort ATTR_RXNOTIERBLOCK15PRICE = 0x840E;
        public const ushort ATTR_RXNOTIERBLOCK16PRICE = 0x840F;
        public const ushort ATTR_RXTIER1BLOCK1PRICE = 0x8410;
        public const ushort ATTR_RXTIER1BLOCK2PRICE = 0x8411;
        public const ushort ATTR_RXTIER1BLOCK3PRICE = 0x8412;
        public const ushort ATTR_RXTIER1BLOCK4PRICE = 0x8413;
        public const ushort ATTR_RXTIER1BLOCK5PRICE = 0x8414;
        public const ushort ATTR_RXTIER1BLOCK6PRICE = 0x8415;
        public const ushort ATTR_RXTIER1BLOCK7PRICE = 0x8416;
        public const ushort ATTR_RXTIER1BLOCK8PRICE = 0x8417;
        public const ushort ATTR_RXTIER1BLOCK9PRICE = 0x8418;
        public const ushort ATTR_RXTIER1BLOCK10PRICE = 0x8419;
        public const ushort ATTR_RXTIER1BLOCK11PRICE = 0x841A;
        public const ushort ATTR_RXTIER1BLOCK12PRICE = 0x841B;
        public const ushort ATTR_RXTIER1BLOCK13PRICE = 0x841C;
        public const ushort ATTR_RXTIER1BLOCK14PRICE = 0x841D;
        public const ushort ATTR_RXTIER1BLOCK15PRICE = 0x841E;
        public const ushort ATTR_RXTIER1BLOCK16PRICE = 0x841F;
        public const ushort ATTR_RXTIER2BLOCK1PRICE = 0x8420;
        public const ushort ATTR_RXTIER2BLOCK2PRICE = 0x8421;
        public const ushort ATTR_RXTIER2BLOCK3PRICE = 0x8422;
        public const ushort ATTR_RXTIER2BLOCK4PRICE = 0x8423;
        public const ushort ATTR_RXTIER2BLOCK5PRICE = 0x8424;
        public const ushort ATTR_RXTIER2BLOCK6PRICE = 0x8425;
        public const ushort ATTR_RXTIER2BLOCK7PRICE = 0x8426;
        public const ushort ATTR_RXTIER2BLOCK8PRICE = 0x8427;
        public const ushort ATTR_RXTIER2BLOCK9PRICE = 0x8428;
        public const ushort ATTR_RXTIER2BLOCK10PRICE = 0x8429;
        public const ushort ATTR_RXTIER2BLOCK11PRICE = 0x842A;
        public const ushort ATTR_RXTIER2BLOCK12PRICE = 0x842B;
        public const ushort ATTR_RXTIER2BLOCK13PRICE = 0x842C;
        public const ushort ATTR_RXTIER2BLOCK14PRICE = 0x842D;
        public const ushort ATTR_RXTIER2BLOCK15PRICE = 0x842E;
        public const ushort ATTR_RXTIER2BLOCK16PRICE = 0x842F;
        public const ushort ATTR_RXTIER3BLOCK1PRICE = 0x8430;
        public const ushort ATTR_RXTIER3BLOCK2PRICE = 0x8431;
        public const ushort ATTR_RXTIER3BLOCK3PRICE = 0x8432;
        public const ushort ATTR_RXTIER3BLOCK4PRICE = 0x8433;
        public const ushort ATTR_RXTIER3BLOCK5PRICE = 0x8434;
        public const ushort ATTR_RXTIER3BLOCK6PRICE = 0x8435;
        public const ushort ATTR_RXTIER3BLOCK7PRICE = 0x8436;
        public const ushort ATTR_RXTIER3BLOCK8PRICE = 0x8437;
        public const ushort ATTR_RXTIER3BLOCK9PRICE = 0x8438;
        public const ushort ATTR_RXTIER3BLOCK10PRICE = 0x8439;
        public const ushort ATTR_RXTIER3BLOCK11PRICE = 0x843A;
        public const ushort ATTR_RXTIER3BLOCK12PRICE = 0x843B;
        public const ushort ATTR_RXTIER3BLOCK13PRICE = 0x843C;
        public const ushort ATTR_RXTIER3BLOCK14PRICE = 0x843D;
        public const ushort ATTR_RXTIER3BLOCK15PRICE = 0x843E;
        public const ushort ATTR_RXTIER3BLOCK16PRICE = 0x843F;
        public const ushort ATTR_RXTIER4BLOCK1PRICE = 0x8440;
        public const ushort ATTR_RXTIER4BLOCK2PRICE = 0x8441;
        public const ushort ATTR_RXTIER4BLOCK3PRICE = 0x8442;
        public const ushort ATTR_RXTIER4BLOCK4PRICE = 0x8443;
        public const ushort ATTR_RXTIER4BLOCK5PRICE = 0x8444;
        public const ushort ATTR_RXTIER4BLOCK6PRICE = 0x8445;
        public const ushort ATTR_RXTIER4BLOCK7PRICE = 0x8446;
        public const ushort ATTR_RXTIER4BLOCK8PRICE = 0x8447;
        public const ushort ATTR_RXTIER4BLOCK9PRICE = 0x8448;
        public const ushort ATTR_RXTIER4BLOCK10PRICE = 0x8449;
        public const ushort ATTR_RXTIER4BLOCK11PRICE = 0x844A;
        public const ushort ATTR_RXTIER4BLOCK12PRICE = 0x844B;
        public const ushort ATTR_RXTIER4BLOCK13PRICE = 0x844C;
        public const ushort ATTR_RXTIER4BLOCK14PRICE = 0x844D;
        public const ushort ATTR_RXTIER4BLOCK15PRICE = 0x844E;
        public const ushort ATTR_RXTIER4BLOCK16PRICE = 0x844F;
        public const ushort ATTR_RXTIER5BLOCK1PRICE = 0x8450;
        public const ushort ATTR_RXTIER5BLOCK2PRICE = 0x8451;
        public const ushort ATTR_RXTIER5BLOCK3PRICE = 0x8452;
        public const ushort ATTR_RXTIER5BLOCK4PRICE = 0x8453;
        public const ushort ATTR_RXTIER5BLOCK5PRICE = 0x8454;
        public const ushort ATTR_RXTIER5BLOCK6PRICE = 0x8455;
        public const ushort ATTR_RXTIER5BLOCK7PRICE = 0x8456;
        public const ushort ATTR_RXTIER5BLOCK8PRICE = 0x8457;
        public const ushort ATTR_RXTIER5BLOCK9PRICE = 0x8458;
        public const ushort ATTR_RXTIER5BLOCK10PRICE = 0x8459;
        public const ushort ATTR_RXTIER5BLOCK11PRICE = 0x845A;
        public const ushort ATTR_RXTIER5BLOCK12PRICE = 0x845B;
        public const ushort ATTR_RXTIER5BLOCK13PRICE = 0x845C;
        public const ushort ATTR_RXTIER5BLOCK14PRICE = 0x845D;
        public const ushort ATTR_RXTIER5BLOCK15PRICE = 0x845E;
        public const ushort ATTR_RXTIER5BLOCK16PRICE = 0x845F;
        public const ushort ATTR_RXTIER6BLOCK1PRICE = 0x8460;
        public const ushort ATTR_RXTIER6BLOCK2PRICE = 0x8461;
        public const ushort ATTR_RXTIER6BLOCK3PRICE = 0x8462;
        public const ushort ATTR_RXTIER6BLOCK4PRICE = 0x8463;
        public const ushort ATTR_RXTIER6BLOCK5PRICE = 0x8464;
        public const ushort ATTR_RXTIER6BLOCK6PRICE = 0x8465;
        public const ushort ATTR_RXTIER6BLOCK7PRICE = 0x8466;
        public const ushort ATTR_RXTIER6BLOCK8PRICE = 0x8467;
        public const ushort ATTR_RXTIER6BLOCK9PRICE = 0x8468;
        public const ushort ATTR_RXTIER6BLOCK10PRICE = 0x8469;
        public const ushort ATTR_RXTIER6BLOCK11PRICE = 0x846A;
        public const ushort ATTR_RXTIER6BLOCK12PRICE = 0x846B;
        public const ushort ATTR_RXTIER6BLOCK13PRICE = 0x846C;
        public const ushort ATTR_RXTIER6BLOCK14PRICE = 0x846D;
        public const ushort ATTR_RXTIER6BLOCK15PRICE = 0x846E;
        public const ushort ATTR_RXTIER6BLOCK16PRICE = 0x846F;
        public const ushort ATTR_RXTIER7BLOCK1PRICE = 0x8470;
        public const ushort ATTR_RXTIER7BLOCK2PRICE = 0x8471;
        public const ushort ATTR_RXTIER7BLOCK3PRICE = 0x8472;
        public const ushort ATTR_RXTIER7BLOCK4PRICE = 0x8473;
        public const ushort ATTR_RXTIER7BLOCK5PRICE = 0x8474;
        public const ushort ATTR_RXTIER7BLOCK6PRICE = 0x8475;
        public const ushort ATTR_RXTIER7BLOCK7PRICE = 0x8476;
        public const ushort ATTR_RXTIER7BLOCK8PRICE = 0x8477;
        public const ushort ATTR_RXTIER7BLOCK9PRICE = 0x8478;
        public const ushort ATTR_RXTIER7BLOCK10PRICE = 0x8479;
        public const ushort ATTR_RXTIER7BLOCK11PRICE = 0x847A;
        public const ushort ATTR_RXTIER7BLOCK12PRICE = 0x847B;
        public const ushort ATTR_RXTIER7BLOCK13PRICE = 0x847C;
        public const ushort ATTR_RXTIER7BLOCK14PRICE = 0x847D;
        public const ushort ATTR_RXTIER7BLOCK15PRICE = 0x847E;
        public const ushort ATTR_RXTIER7BLOCK16PRICE = 0x847F;
        public const ushort ATTR_RXTIER8BLOCK1PRICE = 0x8480;
        public const ushort ATTR_RXTIER8BLOCK2PRICE = 0x8481;
        public const ushort ATTR_RXTIER8BLOCK3PRICE = 0x8482;
        public const ushort ATTR_RXTIER8BLOCK4PRICE = 0x8483;
        public const ushort ATTR_RXTIER8BLOCK5PRICE = 0x8484;
        public const ushort ATTR_RXTIER8BLOCK6PRICE = 0x8485;
        public const ushort ATTR_RXTIER8BLOCK7PRICE = 0x8486;
        public const ushort ATTR_RXTIER8BLOCK8PRICE = 0x8487;
        public const ushort ATTR_RXTIER8BLOCK9PRICE = 0x8488;
        public const ushort ATTR_RXTIER8BLOCK10PRICE = 0x8489;
        public const ushort ATTR_RXTIER8BLOCK11PRICE = 0x848A;
        public const ushort ATTR_RXTIER8BLOCK12PRICE = 0x848B;
        public const ushort ATTR_RXTIER8BLOCK13PRICE = 0x848C;
        public const ushort ATTR_RXTIER8BLOCK14PRICE = 0x848D;
        public const ushort ATTR_RXTIER8BLOCK15PRICE = 0x848E;
        public const ushort ATTR_RXTIER8BLOCK16PRICE = 0x848F;
        public const ushort ATTR_RXTIER9BLOCK1PRICE = 0x8490;
        public const ushort ATTR_RXTIER9BLOCK2PRICE = 0x8491;
        public const ushort ATTR_RXTIER9BLOCK3PRICE = 0x8492;
        public const ushort ATTR_RXTIER9BLOCK4PRICE = 0x8493;
        public const ushort ATTR_RXTIER9BLOCK5PRICE = 0x8494;
        public const ushort ATTR_RXTIER9BLOCK6PRICE = 0x8495;
        public const ushort ATTR_RXTIER9BLOCK7PRICE = 0x8496;
        public const ushort ATTR_RXTIER9BLOCK8PRICE = 0x8497;
        public const ushort ATTR_RXTIER9BLOCK9PRICE = 0x8498;
        public const ushort ATTR_RXTIER9BLOCK10PRICE = 0x8499;
        public const ushort ATTR_RXTIER9BLOCK11PRICE = 0x849A;
        public const ushort ATTR_RXTIER9BLOCK12PRICE = 0x849B;
        public const ushort ATTR_RXTIER9BLOCK13PRICE = 0x849C;
        public const ushort ATTR_RXTIER9BLOCK14PRICE = 0x849D;
        public const ushort ATTR_RXTIER9BLOCK15PRICE = 0x849E;
        public const ushort ATTR_RXTIER9BLOCK16PRICE = 0x849F;
        public const ushort ATTR_RXTIER10BLOCK1PRICE = 0x84A0;
        public const ushort ATTR_RXTIER10BLOCK2PRICE = 0x84A1;
        public const ushort ATTR_RXTIER10BLOCK3PRICE = 0x84A2;
        public const ushort ATTR_RXTIER10BLOCK4PRICE = 0x84A3;
        public const ushort ATTR_RXTIER10BLOCK5PRICE = 0x84A4;
        public const ushort ATTR_RXTIER10BLOCK6PRICE = 0x84A5;
        public const ushort ATTR_RXTIER10BLOCK7PRICE = 0x84A6;
        public const ushort ATTR_RXTIER10BLOCK8PRICE = 0x84A7;
        public const ushort ATTR_RXTIER10BLOCK9PRICE = 0x84A8;
        public const ushort ATTR_RXTIER10BLOCK10PRICE = 0x84A9;
        public const ushort ATTR_RXTIER10BLOCK11PRICE = 0x84AA;
        public const ushort ATTR_RXTIER10BLOCK12PRICE = 0x84AB;
        public const ushort ATTR_RXTIER10BLOCK13PRICE = 0x84AC;
        public const ushort ATTR_RXTIER10BLOCK14PRICE = 0x84AD;
        public const ushort ATTR_RXTIER10BLOCK15PRICE = 0x84AE;
        public const ushort ATTR_RXTIER10BLOCK16PRICE = 0x84AF;
        public const ushort ATTR_RXTIER11BLOCK1PRICE = 0x84B0;
        public const ushort ATTR_RXTIER11BLOCK2PRICE = 0x84B1;
        public const ushort ATTR_RXTIER11BLOCK3PRICE = 0x84B2;
        public const ushort ATTR_RXTIER11BLOCK4PRICE = 0x84B3;
        public const ushort ATTR_RXTIER11BLOCK5PRICE = 0x84B4;
        public const ushort ATTR_RXTIER11BLOCK6PRICE = 0x84B5;
        public const ushort ATTR_RXTIER11BLOCK7PRICE = 0x84B6;
        public const ushort ATTR_RXTIER11BLOCK8PRICE = 0x84B7;
        public const ushort ATTR_RXTIER11BLOCK9PRICE = 0x84B8;
        public const ushort ATTR_RXTIER11BLOCK10PRICE = 0x84B9;
        public const ushort ATTR_RXTIER11BLOCK11PRICE = 0x84BA;
        public const ushort ATTR_RXTIER11BLOCK12PRICE = 0x84BB;
        public const ushort ATTR_RXTIER11BLOCK13PRICE = 0x84BC;
        public const ushort ATTR_RXTIER11BLOCK14PRICE = 0x84BD;
        public const ushort ATTR_RXTIER11BLOCK15PRICE = 0x84BE;
        public const ushort ATTR_RXTIER11BLOCK16PRICE = 0x84BF;
        public const ushort ATTR_RXTIER12BLOCK1PRICE = 0x84C0;
        public const ushort ATTR_RXTIER12BLOCK2PRICE = 0x84C1;
        public const ushort ATTR_RXTIER12BLOCK3PRICE = 0x84C2;
        public const ushort ATTR_RXTIER12BLOCK4PRICE = 0x84C3;
        public const ushort ATTR_RXTIER12BLOCK5PRICE = 0x84C4;
        public const ushort ATTR_RXTIER12BLOCK6PRICE = 0x84C5;
        public const ushort ATTR_RXTIER12BLOCK7PRICE = 0x84C6;
        public const ushort ATTR_RXTIER12BLOCK8PRICE = 0x84C7;
        public const ushort ATTR_RXTIER12BLOCK9PRICE = 0x84C8;
        public const ushort ATTR_RXTIER12BLOCK10PRICE = 0x84C9;
        public const ushort ATTR_RXTIER12BLOCK11PRICE = 0x84CA;
        public const ushort ATTR_RXTIER12BLOCK12PRICE = 0x84CB;
        public const ushort ATTR_RXTIER12BLOCK13PRICE = 0x84CC;
        public const ushort ATTR_RXTIER12BLOCK14PRICE = 0x84CD;
        public const ushort ATTR_RXTIER12BLOCK15PRICE = 0x84CE;
        public const ushort ATTR_RXTIER12BLOCK16PRICE = 0x84CF;
        public const ushort ATTR_RXTIER13BLOCK1PRICE = 0x84D0;
        public const ushort ATTR_RXTIER13BLOCK2PRICE = 0x84D1;
        public const ushort ATTR_RXTIER13BLOCK3PRICE = 0x84D2;
        public const ushort ATTR_RXTIER13BLOCK4PRICE = 0x84D3;
        public const ushort ATTR_RXTIER13BLOCK5PRICE = 0x84D4;
        public const ushort ATTR_RXTIER13BLOCK6PRICE = 0x84D5;
        public const ushort ATTR_RXTIER13BLOCK7PRICE = 0x84D6;
        public const ushort ATTR_RXTIER13BLOCK8PRICE = 0x84D7;
        public const ushort ATTR_RXTIER13BLOCK9PRICE = 0x84D8;
        public const ushort ATTR_RXTIER13BLOCK10PRICE = 0x84D9;
        public const ushort ATTR_RXTIER13BLOCK11PRICE = 0x84DA;
        public const ushort ATTR_RXTIER13BLOCK12PRICE = 0x84DB;
        public const ushort ATTR_RXTIER13BLOCK13PRICE = 0x84DC;
        public const ushort ATTR_RXTIER13BLOCK14PRICE = 0x84DD;
        public const ushort ATTR_RXTIER13BLOCK15PRICE = 0x84DE;
        public const ushort ATTR_RXTIER13BLOCK16PRICE = 0x84DF;
        public const ushort ATTR_RXTIER14BLOCK1PRICE = 0x84E0;
        public const ushort ATTR_RXTIER14BLOCK2PRICE = 0x84E1;
        public const ushort ATTR_RXTIER14BLOCK3PRICE = 0x84E2;
        public const ushort ATTR_RXTIER14BLOCK4PRICE = 0x84E3;
        public const ushort ATTR_RXTIER14BLOCK5PRICE = 0x84E4;
        public const ushort ATTR_RXTIER14BLOCK6PRICE = 0x84E5;
        public const ushort ATTR_RXTIER14BLOCK7PRICE = 0x84E6;
        public const ushort ATTR_RXTIER14BLOCK8PRICE = 0x84E7;
        public const ushort ATTR_RXTIER14BLOCK9PRICE = 0x84E8;
        public const ushort ATTR_RXTIER14BLOCK10PRICE = 0x84E9;
        public const ushort ATTR_RXTIER14BLOCK11PRICE = 0x84EA;
        public const ushort ATTR_RXTIER14BLOCK12PRICE = 0x84EB;
        public const ushort ATTR_RXTIER14BLOCK13PRICE = 0x84EC;
        public const ushort ATTR_RXTIER14BLOCK14PRICE = 0x84ED;
        public const ushort ATTR_RXTIER14BLOCK15PRICE = 0x84EE;
        public const ushort ATTR_RXTIER14BLOCK16PRICE = 0x84EF;
        public const ushort ATTR_RXTIER15BLOCK1PRICE = 0x84F0;
        public const ushort ATTR_RXTIER15BLOCK2PRICE = 0x84F1;
        public const ushort ATTR_RXTIER15BLOCK3PRICE = 0x84F2;
        public const ushort ATTR_RXTIER15BLOCK4PRICE = 0x84F3;
        public const ushort ATTR_RXTIER15BLOCK5PRICE = 0x84F4;
        public const ushort ATTR_RXTIER15BLOCK6PRICE = 0x84F5;
        public const ushort ATTR_RXTIER15BLOCK7PRICE = 0x84F6;
        public const ushort ATTR_RXTIER15BLOCK8PRICE = 0x84F7;
        public const ushort ATTR_RXTIER15BLOCK9PRICE = 0x84F8;
        public const ushort ATTR_RXTIER15BLOCK10PRICE = 0x84F9;
        public const ushort ATTR_RXTIER15BLOCK11PRICE = 0x84FA;
        public const ushort ATTR_RXTIER15BLOCK12PRICE = 0x84FB;
        public const ushort ATTR_RXTIER15BLOCK13PRICE = 0x84FC;
        public const ushort ATTR_RXTIER15BLOCK14PRICE = 0x84FD;
        public const ushort ATTR_RXTIER15BLOCK15PRICE = 0x84FE;
        public const ushort ATTR_RXTIER15BLOCK16PRICE = 0x84FF;
        public const ushort ATTR_RECEIVEDPRICETIER16 = 0x850F;
        public const ushort ATTR_RECEIVEDPRICETIER17 = 0x8510;
        public const ushort ATTR_RECEIVEDPRICETIER18 = 0x8511;
        public const ushort ATTR_RECEIVEDPRICETIER19 = 0x8512;
        public const ushort ATTR_RECEIVEDPRICETIER20 = 0x8513;
        public const ushort ATTR_RECEIVEDPRICETIER21 = 0x8514;
        public const ushort ATTR_RECEIVEDPRICETIER22 = 0x8515;
        public const ushort ATTR_RECEIVEDPRICETIER23 = 0x8516;
        public const ushort ATTR_RECEIVEDPRICETIER24 = 0x8517;
        public const ushort ATTR_RECEIVEDPRICETIER25 = 0x8518;
        public const ushort ATTR_RECEIVEDPRICETIER26 = 0x8519;
        public const ushort ATTR_RECEIVEDPRICETIER27 = 0x851A;
        public const ushort ATTR_RECEIVEDPRICETIER28 = 0x851B;
        public const ushort ATTR_RECEIVEDPRICETIER29 = 0x851C;
        public const ushort ATTR_RECEIVEDPRICETIER30 = 0x851D;
        public const ushort ATTR_RECEIVEDPRICETIER31 = 0x851E;
        public const ushort ATTR_RECEIVEDPRICETIER32 = 0x851F;
        public const ushort ATTR_RECEIVEDPRICETIER33 = 0x8520;
        public const ushort ATTR_RECEIVEDPRICETIER34 = 0x8521;
        public const ushort ATTR_RECEIVEDPRICETIER35 = 0x8522;
        public const ushort ATTR_RECEIVEDPRICETIER36 = 0x8523;
        public const ushort ATTR_RECEIVEDPRICETIER37 = 0x8524;
        public const ushort ATTR_RECEIVEDPRICETIER38 = 0x8525;
        public const ushort ATTR_RECEIVEDPRICETIER39 = 0x8526;
        public const ushort ATTR_RECEIVEDPRICETIER40 = 0x8527;
        public const ushort ATTR_RECEIVEDPRICETIER41 = 0x8528;
        public const ushort ATTR_RECEIVEDPRICETIER42 = 0x8529;
        public const ushort ATTR_RECEIVEDPRICETIER43 = 0x852A;
        public const ushort ATTR_RECEIVEDPRICETIER44 = 0x852B;
        public const ushort ATTR_RECEIVEDPRICETIER45 = 0x852C;
        public const ushort ATTR_RECEIVEDPRICETIER46 = 0x852D;
        public const ushort ATTR_RECEIVEDPRICETIER47 = 0x852E;
        public const ushort ATTR_RECEIVEDPRICETIER48 = 0x852F;
        public const ushort ATTR_RECEIVEDPRICETIER49 = 0x8530;
        public const ushort ATTR_RECEIVEDPRICETIER50 = 0x8531;
        public const ushort ATTR_RECEIVEDPRICETIER51 = 0x8532;
        public const ushort ATTR_RECEIVEDPRICETIER52 = 0x8533;
        public const ushort ATTR_RECEIVEDPRICETIER53 = 0x8534;
        public const ushort ATTR_RECEIVEDPRICETIER54 = 0x8535;
        public const ushort ATTR_RECEIVEDPRICETIER55 = 0x8536;
        public const ushort ATTR_RECEIVEDPRICETIER56 = 0x8537;
        public const ushort ATTR_RECEIVEDPRICETIER57 = 0x8538;
        public const ushort ATTR_RECEIVEDPRICETIER58 = 0x8539;
        public const ushort ATTR_RECEIVEDPRICETIER59 = 0x853A;
        public const ushort ATTR_RECEIVEDPRICETIER60 = 0x853B;
        public const ushort ATTR_RECEIVEDPRICETIER61 = 0x853C;
        public const ushort ATTR_RECEIVEDPRICETIER62 = 0x853D;
        public const ushort ATTR_RECEIVEDPRICETIER63 = 0x853E;
        public const ushort ATTR_RECEIVEDTARIFFLABEL = 0x8610;
        public const ushort ATTR_RECEIVEDNUMBEROFPRICETIERSINUSE = 0x8611;
        public const ushort ATTR_RECEIVEDNUMBEROFBLOCKTHRESHOLDSINUSE = 0x8612;
        public const ushort ATTR_RECEIVEDTIERBLOCKMODE = 0x8613;
        public const ushort ATTR_RECEIVEDCO2 = 0x8625;
        public const ushort ATTR_RECEIVEDCO2UNIT = 0x8626;
        public const ushort ATTR_RECEIVEDCO2TRAILINGDIGIT = 0x8627;
        public const ushort ATTR_RECEIVEDCURRENTBILLINGPERIODSTART = 0x8700;
        public const ushort ATTR_RECEIVEDCURRENTBILLINGPERIODDURATION = 0x8701;
        public const ushort ATTR_RECEIVEDLASTBILLINGPERIODSTART = 0x8702;
        public const ushort ATTR_RECEIVEDLASTBILLINGPERIODDURATION = 0x8703;
        public const ushort ATTR_RECEIVEDLASTBILLINGPERIODCONSOLIDATEDBILL = 0x8704;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(3);

            attributeMap.Add(ATTR_PRICEINCREASERANDOMIZEMINUTES, new ZclAttribute(this, ATTR_PRICEINCREASERANDOMIZEMINUTES, "Price Increase Randomize Minutes", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_PRICEDECREASERANDOMIZEMINUTES, new ZclAttribute(this, ATTR_PRICEDECREASERANDOMIZEMINUTES, "Price Decrease Randomize Minutes", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, true, false));
            attributeMap.Add(ATTR_COMMODITYTYPECLIENT, new ZclAttribute(this, ATTR_COMMODITYTYPECLIENT, "Commodity Type Client", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(135);

            attributeMap.Add(ATTR_TIER1PRICELABEL, new ZclAttribute(this, ATTR_TIER1PRICELABEL, "Tier 1 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER2PRICELABEL, new ZclAttribute(this, ATTR_TIER2PRICELABEL, "Tier 2 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER3PRICELABEL, new ZclAttribute(this, ATTR_TIER3PRICELABEL, "Tier 3 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER4PRICELABEL, new ZclAttribute(this, ATTR_TIER4PRICELABEL, "Tier 4 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER5PRICELABEL, new ZclAttribute(this, ATTR_TIER5PRICELABEL, "Tier 5 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER6PRICELABEL, new ZclAttribute(this, ATTR_TIER6PRICELABEL, "Tier 6 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER7PRICELABEL, new ZclAttribute(this, ATTR_TIER7PRICELABEL, "Tier 7 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER8PRICELABEL, new ZclAttribute(this, ATTR_TIER8PRICELABEL, "Tier 8 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER9PRICELABEL, new ZclAttribute(this, ATTR_TIER9PRICELABEL, "Tier 9 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER10PRICELABEL, new ZclAttribute(this, ATTR_TIER10PRICELABEL, "Tier 10 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER11PRICELABEL, new ZclAttribute(this, ATTR_TIER11PRICELABEL, "Tier 11 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER12PRICELABEL, new ZclAttribute(this, ATTR_TIER12PRICELABEL, "Tier 12 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER13PRICELABEL, new ZclAttribute(this, ATTR_TIER13PRICELABEL, "Tier 13 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER14PRICELABEL, new ZclAttribute(this, ATTR_TIER14PRICELABEL, "Tier 14 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER15PRICELABEL, new ZclAttribute(this, ATTR_TIER15PRICELABEL, "Tier 15 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER16PRICELABEL, new ZclAttribute(this, ATTR_TIER16PRICELABEL, "Tier 16 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER17PRICELABEL, new ZclAttribute(this, ATTR_TIER17PRICELABEL, "Tier 17 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER18PRICELABEL, new ZclAttribute(this, ATTR_TIER18PRICELABEL, "Tier 18 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER19PRICELABEL, new ZclAttribute(this, ATTR_TIER19PRICELABEL, "Tier 19 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER20PRICELABEL, new ZclAttribute(this, ATTR_TIER20PRICELABEL, "Tier 20 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER21PRICELABEL, new ZclAttribute(this, ATTR_TIER21PRICELABEL, "Tier 21 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER22PRICELABEL, new ZclAttribute(this, ATTR_TIER22PRICELABEL, "Tier 22 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER23PRICELABEL, new ZclAttribute(this, ATTR_TIER23PRICELABEL, "Tier 23 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER24PRICELABEL, new ZclAttribute(this, ATTR_TIER24PRICELABEL, "Tier 24 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER25PRICELABEL, new ZclAttribute(this, ATTR_TIER25PRICELABEL, "Tier 25 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER26PRICELABEL, new ZclAttribute(this, ATTR_TIER26PRICELABEL, "Tier 26 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER27PRICELABEL, new ZclAttribute(this, ATTR_TIER27PRICELABEL, "Tier 27 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER28PRICELABEL, new ZclAttribute(this, ATTR_TIER28PRICELABEL, "Tier 28 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER29PRICELABEL, new ZclAttribute(this, ATTR_TIER29PRICELABEL, "Tier 29 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER30PRICELABEL, new ZclAttribute(this, ATTR_TIER30PRICELABEL, "Tier 30 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER31PRICELABEL, new ZclAttribute(this, ATTR_TIER31PRICELABEL, "Tier 31 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER32PRICELABEL, new ZclAttribute(this, ATTR_TIER32PRICELABEL, "Tier 32 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER33PRICELABEL, new ZclAttribute(this, ATTR_TIER33PRICELABEL, "Tier 33 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER34PRICELABEL, new ZclAttribute(this, ATTR_TIER34PRICELABEL, "Tier 34 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER35PRICELABEL, new ZclAttribute(this, ATTR_TIER35PRICELABEL, "Tier 35 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER36PRICELABEL, new ZclAttribute(this, ATTR_TIER36PRICELABEL, "Tier 36 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER37PRICELABEL, new ZclAttribute(this, ATTR_TIER37PRICELABEL, "Tier 37 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER38PRICELABEL, new ZclAttribute(this, ATTR_TIER38PRICELABEL, "Tier 38 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER39PRICELABEL, new ZclAttribute(this, ATTR_TIER39PRICELABEL, "Tier 39 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER40PRICELABEL, new ZclAttribute(this, ATTR_TIER40PRICELABEL, "Tier 40 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER41PRICELABEL, new ZclAttribute(this, ATTR_TIER41PRICELABEL, "Tier 41 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER42PRICELABEL, new ZclAttribute(this, ATTR_TIER42PRICELABEL, "Tier 42 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER43PRICELABEL, new ZclAttribute(this, ATTR_TIER43PRICELABEL, "Tier 43 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER44PRICELABEL, new ZclAttribute(this, ATTR_TIER44PRICELABEL, "Tier 44 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER45PRICELABEL, new ZclAttribute(this, ATTR_TIER45PRICELABEL, "Tier 45 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER46PRICELABEL, new ZclAttribute(this, ATTR_TIER46PRICELABEL, "Tier 46 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER47PRICELABEL, new ZclAttribute(this, ATTR_TIER47PRICELABEL, "Tier 47 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_TIER48PRICELABEL, new ZclAttribute(this, ATTR_TIER48PRICELABEL, "Tier 48 Price Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_BLOCK1THRESHOLD, "Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_BLOCK2THRESHOLD, "Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_BLOCK3THRESHOLD, "Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_BLOCK4THRESHOLD, "Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_BLOCK5THRESHOLD, "Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_BLOCK6THRESHOLD, "Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_BLOCK7THRESHOLD, "Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_BLOCK8THRESHOLD, "Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_BLOCK9THRESHOLD, "Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_BLOCK10THRESHOLD, "Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_BLOCK11THRESHOLD, "Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_BLOCK12THRESHOLD, "Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_BLOCK13THRESHOLD, "Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_BLOCK14THRESHOLD, "Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_BLOCK15THRESHOLD, "Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_BLOCKTHRESHOLDCOUNT, "Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK1THRESHOLD, "Tier 1 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK2THRESHOLD, "Tier 1 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK3THRESHOLD, "Tier 1 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK4THRESHOLD, "Tier 1 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK5THRESHOLD, "Tier 1 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK6THRESHOLD, "Tier 1 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK7THRESHOLD, "Tier 1 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK8THRESHOLD, "Tier 1 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK9THRESHOLD, "Tier 1 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK10THRESHOLD, "Tier 1 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK11THRESHOLD, "Tier 1 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK12THRESHOLD, "Tier 1 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK13THRESHOLD, "Tier 1 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK14THRESHOLD, "Tier 1 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER1BLOCK15THRESHOLD, "Tier 1 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER1BLOCKTHRESHOLDCOUNT, "Tier 1 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK1THRESHOLD, "Tier 2 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK2THRESHOLD, "Tier 2 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK3THRESHOLD, "Tier 2 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK4THRESHOLD, "Tier 2 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK5THRESHOLD, "Tier 2 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK6THRESHOLD, "Tier 2 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK7THRESHOLD, "Tier 2 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK8THRESHOLD, "Tier 2 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK9THRESHOLD, "Tier 2 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK10THRESHOLD, "Tier 2 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK11THRESHOLD, "Tier 2 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK12THRESHOLD, "Tier 2 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK13THRESHOLD, "Tier 2 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK14THRESHOLD, "Tier 2 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER2BLOCK15THRESHOLD, "Tier 2 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER2BLOCKTHRESHOLDCOUNT, "Tier 2 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK1THRESHOLD, "Tier 3 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK2THRESHOLD, "Tier 3 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK3THRESHOLD, "Tier 3 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK4THRESHOLD, "Tier 3 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK5THRESHOLD, "Tier 3 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK6THRESHOLD, "Tier 3 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK7THRESHOLD, "Tier 3 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK8THRESHOLD, "Tier 3 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK9THRESHOLD, "Tier 3 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK10THRESHOLD, "Tier 3 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK11THRESHOLD, "Tier 3 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK12THRESHOLD, "Tier 3 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK13THRESHOLD, "Tier 3 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK14THRESHOLD, "Tier 3 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER3BLOCK15THRESHOLD, "Tier 3 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER3BLOCKTHRESHOLDCOUNT, "Tier 3 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK1THRESHOLD, "Tier 4 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK2THRESHOLD, "Tier 4 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK3THRESHOLD, "Tier 4 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK4THRESHOLD, "Tier 4 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK5THRESHOLD, "Tier 4 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK6THRESHOLD, "Tier 4 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK7THRESHOLD, "Tier 4 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK8THRESHOLD, "Tier 4 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK9THRESHOLD, "Tier 4 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK10THRESHOLD, "Tier 4 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK11THRESHOLD, "Tier 4 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK12THRESHOLD, "Tier 4 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK13THRESHOLD, "Tier 4 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK14THRESHOLD, "Tier 4 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER4BLOCK15THRESHOLD, "Tier 4 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER4BLOCKTHRESHOLDCOUNT, "Tier 4 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK1THRESHOLD, "Tier 5 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK2THRESHOLD, "Tier 5 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK3THRESHOLD, "Tier 5 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK4THRESHOLD, "Tier 5 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK5THRESHOLD, "Tier 5 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK6THRESHOLD, "Tier 5 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK7THRESHOLD, "Tier 5 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK8THRESHOLD, "Tier 5 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK9THRESHOLD, "Tier 5 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK10THRESHOLD, "Tier 5 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK11THRESHOLD, "Tier 5 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK12THRESHOLD, "Tier 5 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK13THRESHOLD, "Tier 5 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK14THRESHOLD, "Tier 5 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER5BLOCK15THRESHOLD, "Tier 5 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER5BLOCKTHRESHOLDCOUNT, "Tier 5 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK1THRESHOLD, "Tier 6 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK2THRESHOLD, "Tier 6 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK3THRESHOLD, "Tier 6 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK4THRESHOLD, "Tier 6 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK5THRESHOLD, "Tier 6 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK6THRESHOLD, "Tier 6 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK7THRESHOLD, "Tier 6 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK8THRESHOLD, "Tier 6 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK9THRESHOLD, "Tier 6 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK10THRESHOLD, "Tier 6 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK11THRESHOLD, "Tier 6 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK12THRESHOLD, "Tier 6 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK13THRESHOLD, "Tier 6 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK14THRESHOLD, "Tier 6 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER6BLOCK15THRESHOLD, "Tier 6 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER6BLOCKTHRESHOLDCOUNT, "Tier 6 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK1THRESHOLD, "Tier 7 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK2THRESHOLD, "Tier 7 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK3THRESHOLD, "Tier 7 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK4THRESHOLD, "Tier 7 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK5THRESHOLD, "Tier 7 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK6THRESHOLD, "Tier 7 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK7THRESHOLD, "Tier 7 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK8THRESHOLD, "Tier 7 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK9THRESHOLD, "Tier 7 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK10THRESHOLD, "Tier 7 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK11THRESHOLD, "Tier 7 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK12THRESHOLD, "Tier 7 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK13THRESHOLD, "Tier 7 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK14THRESHOLD, "Tier 7 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER7BLOCK15THRESHOLD, "Tier 7 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER7BLOCKTHRESHOLDCOUNT, "Tier 7 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK1THRESHOLD, "Tier 8 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK2THRESHOLD, "Tier 8 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK3THRESHOLD, "Tier 8 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK4THRESHOLD, "Tier 8 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK5THRESHOLD, "Tier 8 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK6THRESHOLD, "Tier 8 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK7THRESHOLD, "Tier 8 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK8THRESHOLD, "Tier 8 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK9THRESHOLD, "Tier 8 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK10THRESHOLD, "Tier 8 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK11THRESHOLD, "Tier 8 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK12THRESHOLD, "Tier 8 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK13THRESHOLD, "Tier 8 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK14THRESHOLD, "Tier 8 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER8BLOCK15THRESHOLD, "Tier 8 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER8BLOCKTHRESHOLDCOUNT, "Tier 8 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK1THRESHOLD, "Tier 9 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK2THRESHOLD, "Tier 9 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK3THRESHOLD, "Tier 9 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK4THRESHOLD, "Tier 9 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK5THRESHOLD, "Tier 9 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK6THRESHOLD, "Tier 9 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK7THRESHOLD, "Tier 9 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK8THRESHOLD, "Tier 9 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK9THRESHOLD, "Tier 9 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK10THRESHOLD, "Tier 9 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK11THRESHOLD, "Tier 9 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK12THRESHOLD, "Tier 9 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK13THRESHOLD, "Tier 9 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK14THRESHOLD, "Tier 9 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER9BLOCK15THRESHOLD, "Tier 9 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER9BLOCKTHRESHOLDCOUNT, "Tier 9 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK1THRESHOLD, "Tier 10 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK2THRESHOLD, "Tier 10 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK3THRESHOLD, "Tier 10 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK4THRESHOLD, "Tier 10 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK5THRESHOLD, "Tier 10 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK6THRESHOLD, "Tier 10 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK7THRESHOLD, "Tier 10 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK8THRESHOLD, "Tier 10 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK9THRESHOLD, "Tier 10 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK10THRESHOLD, "Tier 10 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK11THRESHOLD, "Tier 10 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK12THRESHOLD, "Tier 10 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK13THRESHOLD, "Tier 10 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK14THRESHOLD, "Tier 10 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER10BLOCK15THRESHOLD, "Tier 10 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER10BLOCKTHRESHOLDCOUNT, "Tier 10 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK1THRESHOLD, "Tier 11 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK2THRESHOLD, "Tier 11 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK3THRESHOLD, "Tier 11 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK4THRESHOLD, "Tier 11 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK5THRESHOLD, "Tier 11 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK6THRESHOLD, "Tier 11 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK7THRESHOLD, "Tier 11 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK8THRESHOLD, "Tier 11 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK9THRESHOLD, "Tier 11 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK10THRESHOLD, "Tier 11 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK11THRESHOLD, "Tier 11 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK12THRESHOLD, "Tier 11 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK13THRESHOLD, "Tier 11 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK14THRESHOLD, "Tier 11 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER11BLOCK15THRESHOLD, "Tier 11 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER11BLOCKTHRESHOLDCOUNT, "Tier 11 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK1THRESHOLD, "Tier 12 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK2THRESHOLD, "Tier 12 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK3THRESHOLD, "Tier 12 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK4THRESHOLD, "Tier 12 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK5THRESHOLD, "Tier 12 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK6THRESHOLD, "Tier 12 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK7THRESHOLD, "Tier 12 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK8THRESHOLD, "Tier 12 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK9THRESHOLD, "Tier 12 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK10THRESHOLD, "Tier 12 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK11THRESHOLD, "Tier 12 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK12THRESHOLD, "Tier 12 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK13THRESHOLD, "Tier 12 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK14THRESHOLD, "Tier 12 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER12BLOCK15THRESHOLD, "Tier 12 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER12BLOCKTHRESHOLDCOUNT, "Tier 12 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK1THRESHOLD, "Tier 13 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK2THRESHOLD, "Tier 13 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK3THRESHOLD, "Tier 13 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK4THRESHOLD, "Tier 13 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK5THRESHOLD, "Tier 13 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK6THRESHOLD, "Tier 13 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK7THRESHOLD, "Tier 13 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK8THRESHOLD, "Tier 13 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK9THRESHOLD, "Tier 13 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK10THRESHOLD, "Tier 13 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK11THRESHOLD, "Tier 13 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK12THRESHOLD, "Tier 13 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK13THRESHOLD, "Tier 13 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK14THRESHOLD, "Tier 13 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER13BLOCK15THRESHOLD, "Tier 13 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER13BLOCKTHRESHOLDCOUNT, "Tier 13 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK1THRESHOLD, "Tier 14 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK2THRESHOLD, "Tier 14 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK3THRESHOLD, "Tier 14 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK4THRESHOLD, "Tier 14 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK5THRESHOLD, "Tier 14 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK6THRESHOLD, "Tier 14 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK7THRESHOLD, "Tier 14 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK8THRESHOLD, "Tier 14 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK9THRESHOLD, "Tier 14 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK10THRESHOLD, "Tier 14 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK11THRESHOLD, "Tier 14 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK12THRESHOLD, "Tier 14 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK13THRESHOLD, "Tier 14 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK14THRESHOLD, "Tier 14 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER14BLOCK15THRESHOLD, "Tier 14 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER14BLOCKTHRESHOLDCOUNT, "Tier 14 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK1THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK1THRESHOLD, "Tier 15 Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK2THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK2THRESHOLD, "Tier 15 Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK3THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK3THRESHOLD, "Tier 15 Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK4THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK4THRESHOLD, "Tier 15 Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK5THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK5THRESHOLD, "Tier 15 Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK6THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK6THRESHOLD, "Tier 15 Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK7THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK7THRESHOLD, "Tier 15 Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK8THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK8THRESHOLD, "Tier 15 Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK9THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK9THRESHOLD, "Tier 15 Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK10THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK10THRESHOLD, "Tier 15 Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK11THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK11THRESHOLD, "Tier 15 Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK12THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK12THRESHOLD, "Tier 15 Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK13THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK13THRESHOLD, "Tier 15 Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK14THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK14THRESHOLD, "Tier 15 Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK15THRESHOLD, new ZclAttribute(this, ATTR_TIER15BLOCK15THRESHOLD, "Tier 15 Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCKTHRESHOLDCOUNT, new ZclAttribute(this, ATTR_TIER15BLOCKTHRESHOLDCOUNT, "Tier 15 Block Threshold Count", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_STARTOFBLOCKPERIOD, new ZclAttribute(this, ATTR_STARTOFBLOCKPERIOD, "Start of Block Period", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_BLOCKPERIODDURATION, new ZclAttribute(this, ATTR_BLOCKPERIODDURATION, "Block Period Duration", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_THRESHOLDMULTIPLIER, new ZclAttribute(this, ATTR_THRESHOLDMULTIPLIER, "Threshold Multiplier", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_THRESHOLDDIVISOR, new ZclAttribute(this, ATTR_THRESHOLDDIVISOR, "Threshold Divisor", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_BLOCKPERIODDURATIONTYPE, new ZclAttribute(this, ATTR_BLOCKPERIODDURATIONTYPE, "Block Period Duration Type", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_COMMODITYTYPESERVER, new ZclAttribute(this, ATTR_COMMODITYTYPESERVER, "Commodity Type Server", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_STANDINGCHARGE, new ZclAttribute(this, ATTR_STANDINGCHARGE, "Standing Charge", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CONVERSIONFACTOR, new ZclAttribute(this, ATTR_CONVERSIONFACTOR, "Conversion Factor", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CONVERSIONFACTORTRAILINGDIGIT, new ZclAttribute(this, ATTR_CONVERSIONFACTORTRAILINGDIGIT, "Conversion Factor Trailing Digit", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_CALORIFICVALUE, new ZclAttribute(this, ATTR_CALORIFICVALUE, "Calorific Value", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CALORIFICVALUEUNIT, new ZclAttribute(this, ATTR_CALORIFICVALUEUNIT, "Calorific Value Unit", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_CALORIFICVALUETRAILINGDIGIT, new ZclAttribute(this, ATTR_CALORIFICVALUETRAILINGDIGIT, "Calorific Value Trailing Digit", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK1PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK1PRICE, "No Tier Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK2PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK2PRICE, "No Tier Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK3PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK3PRICE, "No Tier Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK4PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK4PRICE, "No Tier Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK5PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK5PRICE, "No Tier Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK6PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK6PRICE, "No Tier Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK7PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK7PRICE, "No Tier Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK8PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK8PRICE, "No Tier Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK9PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK9PRICE, "No Tier Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK10PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK10PRICE, "No Tier Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK11PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK11PRICE, "No Tier Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK12PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK12PRICE, "No Tier Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK13PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK13PRICE, "No Tier Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK14PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK14PRICE, "No Tier Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK15PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK15PRICE, "No Tier Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NOTIERBLOCK16PRICE, new ZclAttribute(this, ATTR_NOTIERBLOCK16PRICE, "No Tier Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK1PRICE, "Tier 1 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK2PRICE, "Tier 1 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK3PRICE, "Tier 1 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK4PRICE, "Tier 1 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK5PRICE, "Tier 1 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK6PRICE, "Tier 1 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK7PRICE, "Tier 1 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK8PRICE, "Tier 1 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK9PRICE, "Tier 1 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK10PRICE, "Tier 1 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK11PRICE, "Tier 1 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK12PRICE, "Tier 1 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK13PRICE, "Tier 1 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK14PRICE, "Tier 1 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK15PRICE, "Tier 1 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER1BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER1BLOCK16PRICE, "Tier 1 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK1PRICE, "Tier 2 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK2PRICE, "Tier 2 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK3PRICE, "Tier 2 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK4PRICE, "Tier 2 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK5PRICE, "Tier 2 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK6PRICE, "Tier 2 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK7PRICE, "Tier 2 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK8PRICE, "Tier 2 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK9PRICE, "Tier 2 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK10PRICE, "Tier 2 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK11PRICE, "Tier 2 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK12PRICE, "Tier 2 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK13PRICE, "Tier 2 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK14PRICE, "Tier 2 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK15PRICE, "Tier 2 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER2BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER2BLOCK16PRICE, "Tier 2 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK1PRICE, "Tier 3 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK2PRICE, "Tier 3 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK3PRICE, "Tier 3 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK4PRICE, "Tier 3 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK5PRICE, "Tier 3 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK6PRICE, "Tier 3 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK7PRICE, "Tier 3 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK8PRICE, "Tier 3 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK9PRICE, "Tier 3 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK10PRICE, "Tier 3 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK11PRICE, "Tier 3 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK12PRICE, "Tier 3 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK13PRICE, "Tier 3 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK14PRICE, "Tier 3 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK15PRICE, "Tier 3 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER3BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER3BLOCK16PRICE, "Tier 3 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK1PRICE, "Tier 4 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK2PRICE, "Tier 4 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK3PRICE, "Tier 4 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK4PRICE, "Tier 4 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK5PRICE, "Tier 4 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK6PRICE, "Tier 4 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK7PRICE, "Tier 4 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK8PRICE, "Tier 4 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK9PRICE, "Tier 4 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK10PRICE, "Tier 4 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK11PRICE, "Tier 4 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK12PRICE, "Tier 4 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK13PRICE, "Tier 4 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK14PRICE, "Tier 4 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK15PRICE, "Tier 4 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER4BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER4BLOCK16PRICE, "Tier 4 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK1PRICE, "Tier 5 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK2PRICE, "Tier 5 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK3PRICE, "Tier 5 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK4PRICE, "Tier 5 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK5PRICE, "Tier 5 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK6PRICE, "Tier 5 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK7PRICE, "Tier 5 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK8PRICE, "Tier 5 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK9PRICE, "Tier 5 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK10PRICE, "Tier 5 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK11PRICE, "Tier 5 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK12PRICE, "Tier 5 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK13PRICE, "Tier 5 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK14PRICE, "Tier 5 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK15PRICE, "Tier 5 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER5BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER5BLOCK16PRICE, "Tier 5 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK1PRICE, "Tier 6 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK2PRICE, "Tier 6 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK3PRICE, "Tier 6 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK4PRICE, "Tier 6 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK5PRICE, "Tier 6 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK6PRICE, "Tier 6 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK7PRICE, "Tier 6 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK8PRICE, "Tier 6 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK9PRICE, "Tier 6 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK10PRICE, "Tier 6 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK11PRICE, "Tier 6 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK12PRICE, "Tier 6 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK13PRICE, "Tier 6 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK14PRICE, "Tier 6 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK15PRICE, "Tier 6 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER6BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER6BLOCK16PRICE, "Tier 6 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK1PRICE, "Tier 7 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK2PRICE, "Tier 7 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK3PRICE, "Tier 7 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK4PRICE, "Tier 7 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK5PRICE, "Tier 7 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK6PRICE, "Tier 7 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK7PRICE, "Tier 7 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK8PRICE, "Tier 7 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK9PRICE, "Tier 7 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK10PRICE, "Tier 7 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK11PRICE, "Tier 7 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK12PRICE, "Tier 7 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK13PRICE, "Tier 7 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK14PRICE, "Tier 7 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK15PRICE, "Tier 7 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER7BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER7BLOCK16PRICE, "Tier 7 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK1PRICE, "Tier 8 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK2PRICE, "Tier 8 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK3PRICE, "Tier 8 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK4PRICE, "Tier 8 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK5PRICE, "Tier 8 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK6PRICE, "Tier 8 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK7PRICE, "Tier 8 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK8PRICE, "Tier 8 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK9PRICE, "Tier 8 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK10PRICE, "Tier 8 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK11PRICE, "Tier 8 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK12PRICE, "Tier 8 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK13PRICE, "Tier 8 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK14PRICE, "Tier 8 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK15PRICE, "Tier 8 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER8BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER8BLOCK16PRICE, "Tier 8 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK1PRICE, "Tier 9 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK2PRICE, "Tier 9 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK3PRICE, "Tier 9 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK4PRICE, "Tier 9 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK5PRICE, "Tier 9 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK6PRICE, "Tier 9 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK7PRICE, "Tier 9 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK8PRICE, "Tier 9 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK9PRICE, "Tier 9 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK10PRICE, "Tier 9 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK11PRICE, "Tier 9 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK12PRICE, "Tier 9 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK13PRICE, "Tier 9 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK14PRICE, "Tier 9 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK15PRICE, "Tier 9 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER9BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER9BLOCK16PRICE, "Tier 9 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK1PRICE, "Tier 10 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK2PRICE, "Tier 10 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK3PRICE, "Tier 10 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK4PRICE, "Tier 10 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK5PRICE, "Tier 10 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK6PRICE, "Tier 10 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK7PRICE, "Tier 10 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK8PRICE, "Tier 10 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK9PRICE, "Tier 10 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK10PRICE, "Tier 10 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK11PRICE, "Tier 10 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK12PRICE, "Tier 10 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK13PRICE, "Tier 10 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK14PRICE, "Tier 10 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK15PRICE, "Tier 10 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER10BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER10BLOCK16PRICE, "Tier 10 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK1PRICE, "Tier 11 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK2PRICE, "Tier 11 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK3PRICE, "Tier 11 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK4PRICE, "Tier 11 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK5PRICE, "Tier 11 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK6PRICE, "Tier 11 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK7PRICE, "Tier 11 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK8PRICE, "Tier 11 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK9PRICE, "Tier 11 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK10PRICE, "Tier 11 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK11PRICE, "Tier 11 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK12PRICE, "Tier 11 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK13PRICE, "Tier 11 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK14PRICE, "Tier 11 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK15PRICE, "Tier 11 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER11BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER11BLOCK16PRICE, "Tier 11 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK1PRICE, "Tier 12 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK2PRICE, "Tier 12 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK3PRICE, "Tier 12 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK4PRICE, "Tier 12 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK5PRICE, "Tier 12 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK6PRICE, "Tier 12 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK7PRICE, "Tier 12 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK8PRICE, "Tier 12 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK9PRICE, "Tier 12 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK10PRICE, "Tier 12 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK11PRICE, "Tier 12 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK12PRICE, "Tier 12 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK13PRICE, "Tier 12 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK14PRICE, "Tier 12 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK15PRICE, "Tier 12 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER12BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER12BLOCK16PRICE, "Tier 12 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK1PRICE, "Tier 13 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK2PRICE, "Tier 13 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK3PRICE, "Tier 13 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK4PRICE, "Tier 13 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK5PRICE, "Tier 13 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK6PRICE, "Tier 13 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK7PRICE, "Tier 13 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK8PRICE, "Tier 13 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK9PRICE, "Tier 13 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK10PRICE, "Tier 13 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK11PRICE, "Tier 13 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK12PRICE, "Tier 13 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK13PRICE, "Tier 13 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK14PRICE, "Tier 13 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK15PRICE, "Tier 13 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER13BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER13BLOCK16PRICE, "Tier 13 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK1PRICE, "Tier 14 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK2PRICE, "Tier 14 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK3PRICE, "Tier 14 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK4PRICE, "Tier 14 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK5PRICE, "Tier 14 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK6PRICE, "Tier 14 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK7PRICE, "Tier 14 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK8PRICE, "Tier 14 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK9PRICE, "Tier 14 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK10PRICE, "Tier 14 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK11PRICE, "Tier 14 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK12PRICE, "Tier 14 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK13PRICE, "Tier 14 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK14PRICE, "Tier 14 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK15PRICE, "Tier 14 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER14BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER14BLOCK16PRICE, "Tier 14 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK1PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK1PRICE, "Tier 15 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK2PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK2PRICE, "Tier 15 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK3PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK3PRICE, "Tier 15 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK4PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK4PRICE, "Tier 15 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK5PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK5PRICE, "Tier 15 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK6PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK6PRICE, "Tier 15 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK7PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK7PRICE, "Tier 15 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK8PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK8PRICE, "Tier 15 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK9PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK9PRICE, "Tier 15 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK10PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK10PRICE, "Tier 15 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK11PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK11PRICE, "Tier 15 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK12PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK12PRICE, "Tier 15 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK13PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK13PRICE, "Tier 15 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK14PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK14PRICE, "Tier 15 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK15PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK15PRICE, "Tier 15 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIER15BLOCK16PRICE, new ZclAttribute(this, ATTR_TIER15BLOCK16PRICE, "Tier 15 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER16, new ZclAttribute(this, ATTR_PRICETIER16, "Price Tier 16", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER17, new ZclAttribute(this, ATTR_PRICETIER17, "Price Tier 17", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER18, new ZclAttribute(this, ATTR_PRICETIER18, "Price Tier 18", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER19, new ZclAttribute(this, ATTR_PRICETIER19, "Price Tier 19", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER20, new ZclAttribute(this, ATTR_PRICETIER20, "Price Tier 20", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER21, new ZclAttribute(this, ATTR_PRICETIER21, "Price Tier 21", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER22, new ZclAttribute(this, ATTR_PRICETIER22, "Price Tier 22", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER23, new ZclAttribute(this, ATTR_PRICETIER23, "Price Tier 23", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER24, new ZclAttribute(this, ATTR_PRICETIER24, "Price Tier 24", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER25, new ZclAttribute(this, ATTR_PRICETIER25, "Price Tier 25", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER26, new ZclAttribute(this, ATTR_PRICETIER26, "Price Tier 26", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER27, new ZclAttribute(this, ATTR_PRICETIER27, "Price Tier 27", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER28, new ZclAttribute(this, ATTR_PRICETIER28, "Price Tier 28", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER29, new ZclAttribute(this, ATTR_PRICETIER29, "Price Tier 29", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER30, new ZclAttribute(this, ATTR_PRICETIER30, "Price Tier 30", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER31, new ZclAttribute(this, ATTR_PRICETIER31, "Price Tier 31", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER32, new ZclAttribute(this, ATTR_PRICETIER32, "Price Tier 32", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER33, new ZclAttribute(this, ATTR_PRICETIER33, "Price Tier 33", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER34, new ZclAttribute(this, ATTR_PRICETIER34, "Price Tier 34", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER35, new ZclAttribute(this, ATTR_PRICETIER35, "Price Tier 35", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER36, new ZclAttribute(this, ATTR_PRICETIER36, "Price Tier 36", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER37, new ZclAttribute(this, ATTR_PRICETIER37, "Price Tier 37", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER38, new ZclAttribute(this, ATTR_PRICETIER38, "Price Tier 38", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER39, new ZclAttribute(this, ATTR_PRICETIER39, "Price Tier 39", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER40, new ZclAttribute(this, ATTR_PRICETIER40, "Price Tier 40", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER41, new ZclAttribute(this, ATTR_PRICETIER41, "Price Tier 41", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER42, new ZclAttribute(this, ATTR_PRICETIER42, "Price Tier 42", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER43, new ZclAttribute(this, ATTR_PRICETIER43, "Price Tier 43", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER44, new ZclAttribute(this, ATTR_PRICETIER44, "Price Tier 44", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER45, new ZclAttribute(this, ATTR_PRICETIER45, "Price Tier 45", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER46, new ZclAttribute(this, ATTR_PRICETIER46, "Price Tier 46", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETIER47, new ZclAttribute(this, ATTR_PRICETIER47, "Price Tier 47", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CPP1PRICE, new ZclAttribute(this, ATTR_CPP1PRICE, "Cpp 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CPP2PRICE, new ZclAttribute(this, ATTR_CPP2PRICE, "Cpp 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TARIFFLABEL, new ZclAttribute(this, ATTR_TARIFFLABEL, "Tariff Label", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, false, false));
            attributeMap.Add(ATTR_NUMBEROFPRICETIERSINUSE, new ZclAttribute(this, ATTR_NUMBEROFPRICETIERSINUSE, "Numberof Price Tiers In Use", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_NUMBEROFBLOCKTHRESHOLDSINUSE, new ZclAttribute(this, ATTR_NUMBEROFBLOCKTHRESHOLDSINUSE, "Numberof Block Thresholds In Use", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_TIERBLOCKMODE, new ZclAttribute(this, ATTR_TIERBLOCKMODE, "Tier Block Mode", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_UNITOFMEASURE, new ZclAttribute(this, ATTR_UNITOFMEASURE, "Unit Of Measure", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_CURRENCY, new ZclAttribute(this, ATTR_CURRENCY, "Currency", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PRICETRAILINGDIGIT, new ZclAttribute(this, ATTR_PRICETRAILINGDIGIT, "Price Trailing Digit", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, false, false));
            attributeMap.Add(ATTR_TARIFFRESOLUTIONPERIOD, new ZclAttribute(this, ATTR_TARIFFRESOLUTIONPERIOD, "Tariff Resolution Period", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_CO2, new ZclAttribute(this, ATTR_CO2, "CO2", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CO2UNIT, new ZclAttribute(this, ATTR_CO2UNIT, "CO2 Unit", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_CO2TRAILINGDIGIT, new ZclAttribute(this, ATTR_CO2TRAILINGDIGIT, "CO2 Trailing Digit", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTBILLINGPERIODSTART, new ZclAttribute(this, ATTR_CURRENTBILLINGPERIODSTART, "Current Billing Period Start", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_CURRENTBILLINGPERIODDURATION, new ZclAttribute(this, ATTR_CURRENTBILLINGPERIODDURATION, "Current Billing Period Duration", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_LASTBILLINGPERIODSTART, new ZclAttribute(this, ATTR_LASTBILLINGPERIODSTART, "Last Billing Period Start", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_LASTBILLINGPERIODDURATION, new ZclAttribute(this, ATTR_LASTBILLINGPERIODDURATION, "Last Billing Period Duration", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_LASTBILLINGPERIODCONSOLIDATEDBILL, new ZclAttribute(this, ATTR_LASTBILLINGPERIODCONSOLIDATEDBILL, "Last Billing Period Consolidated Bill", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CREDITPAYMENTDUEDATE, new ZclAttribute(this, ATTR_CREDITPAYMENTDUEDATE, "Credit Payment Due Date", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_CREDITPAYMENTSTATUS, new ZclAttribute(this, ATTR_CREDITPAYMENTSTATUS, "Credit Payment Status", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_CREDITPAYMENTOVERDUEAMOUNT, new ZclAttribute(this, ATTR_CREDITPAYMENTOVERDUEAMOUNT, "Credit Payment Over Due Amount", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PAYMENTDISCOUNT, new ZclAttribute(this, ATTR_PAYMENTDISCOUNT, "Payment Discount", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_PAYMENTDISCOUNTPERIOD, new ZclAttribute(this, ATTR_PAYMENTDISCOUNTPERIOD, "Payment Discount Period", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENT1, new ZclAttribute(this, ATTR_CREDITCARDPAYMENT1, "Credit Card Payment 1", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENTDATE1, new ZclAttribute(this, ATTR_CREDITCARDPAYMENTDATE1, "Credit Card Payment Date 1", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENTREF1, new ZclAttribute(this, ATTR_CREDITCARDPAYMENTREF1, "Credit Card Payment Ref 1", ZclDataType.Get(DataType.OCTET_STRING), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENT2, new ZclAttribute(this, ATTR_CREDITCARDPAYMENT2, "Credit Card Payment 2", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENTDATE2, new ZclAttribute(this, ATTR_CREDITCARDPAYMENTDATE2, "Credit Card Payment Date 2", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENTREF2, new ZclAttribute(this, ATTR_CREDITCARDPAYMENTREF2, "Credit Card Payment Ref 2", ZclDataType.Get(DataType.OCTET_STRING), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENT3, new ZclAttribute(this, ATTR_CREDITCARDPAYMENT3, "Credit Card Payment 3", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENTDATE3, new ZclAttribute(this, ATTR_CREDITCARDPAYMENTDATE3, "Credit Card Payment Date 3", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENTREF3, new ZclAttribute(this, ATTR_CREDITCARDPAYMENTREF3, "Credit Card Payment Ref 3", ZclDataType.Get(DataType.OCTET_STRING), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENT4, new ZclAttribute(this, ATTR_CREDITCARDPAYMENT4, "Credit Card Payment 4", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENTDATE4, new ZclAttribute(this, ATTR_CREDITCARDPAYMENTDATE4, "Credit Card Payment Date 4", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENTREF4, new ZclAttribute(this, ATTR_CREDITCARDPAYMENTREF4, "Credit Card Payment Ref 4", ZclDataType.Get(DataType.OCTET_STRING), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENT5, new ZclAttribute(this, ATTR_CREDITCARDPAYMENT5, "Credit Card Payment 5", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENTDATE5, new ZclAttribute(this, ATTR_CREDITCARDPAYMENTDATE5, "Credit Card Payment Date 5", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_CREDITCARDPAYMENTREF5, new ZclAttribute(this, ATTR_CREDITCARDPAYMENTREF5, "Credit Card Payment Ref 5", ZclDataType.Get(DataType.OCTET_STRING), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDTIER1PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER1PRICELABEL, "Received Tier 1 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER2PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER2PRICELABEL, "Received Tier 2 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER3PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER3PRICELABEL, "Received Tier 3 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER4PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER4PRICELABEL, "Received Tier 4 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER5PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER5PRICELABEL, "Received Tier 5 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER6PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER6PRICELABEL, "Received Tier 6 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER7PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER7PRICELABEL, "Received Tier 7 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER8PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER8PRICELABEL, "Received Tier 8 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER9PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER9PRICELABEL, "Received Tier 9 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER10PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER10PRICELABEL, "Received Tier 10 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER11PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER11PRICELABEL, "Received Tier 11 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER12PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER12PRICELABEL, "Received Tier 12 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER13PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER13PRICELABEL, "Received Tier 13 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER14PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER14PRICELABEL, "Received Tier 14 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER15PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER15PRICELABEL, "Received Tier 15 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER16PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER16PRICELABEL, "Received Tier 16 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER17PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER17PRICELABEL, "Received Tier 17 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER18PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER18PRICELABEL, "Received Tier 18 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER19PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER19PRICELABEL, "Received Tier 19 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER20PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER20PRICELABEL, "Received Tier 20 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER21PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER21PRICELABEL, "Received Tier 21 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER22PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER22PRICELABEL, "Received Tier 22 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER23PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER23PRICELABEL, "Received Tier 23 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER24PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER24PRICELABEL, "Received Tier 24 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER25PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER25PRICELABEL, "Received Tier 25 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER26PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER26PRICELABEL, "Received Tier 26 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER27PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER27PRICELABEL, "Received Tier 27 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER28PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER28PRICELABEL, "Received Tier 28 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER29PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER29PRICELABEL, "Received Tier 29 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER30PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER30PRICELABEL, "Received Tier 30 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER31PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER31PRICELABEL, "Received Tier 31 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER32PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER32PRICELABEL, "Received Tier 32 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER33PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER33PRICELABEL, "Received Tier 33 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER34PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER34PRICELABEL, "Received Tier 34 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER35PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER35PRICELABEL, "Received Tier 35 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER36PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER36PRICELABEL, "Received Tier 36 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER37PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER37PRICELABEL, "Received Tier 37 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER38PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER38PRICELABEL, "Received Tier 38 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER39PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER39PRICELABEL, "Received Tier 39 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER40PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER40PRICELABEL, "Received Tier 40 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER41PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER41PRICELABEL, "Received Tier 41 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER42PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER42PRICELABEL, "Received Tier 42 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER43PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER43PRICELABEL, "Received Tier 43 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER44PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER44PRICELABEL, "Received Tier 44 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER45PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER45PRICELABEL, "Received Tier 45 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER46PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER46PRICELABEL, "Received Tier 46 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER47PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER47PRICELABEL, "Received Tier 47 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDTIER48PRICELABEL, new ZclAttribute(this, ATTR_RECEIVEDTIER48PRICELABEL, "Received Tier 48 Price Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK1THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK1THRESHOLD, "Received Block 1 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK2THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK2THRESHOLD, "Received Block 2 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK3THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK3THRESHOLD, "Received Block 3 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK4THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK4THRESHOLD, "Received Block 4 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK5THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK5THRESHOLD, "Received Block 5 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK6THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK6THRESHOLD, "Received Block 6 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK7THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK7THRESHOLD, "Received Block 7 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK8THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK8THRESHOLD, "Received Block 8 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK9THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK9THRESHOLD, "Received Block 9 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK10THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK10THRESHOLD, "Received Block 10 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK11THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK11THRESHOLD, "Received Block 11 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK12THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK12THRESHOLD, "Received Block 12 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK13THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK13THRESHOLD, "Received Block 13 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK14THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK14THRESHOLD, "Received Block 14 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK15THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK15THRESHOLD, "Received Block 15 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCK16THRESHOLD, new ZclAttribute(this, ATTR_RECEIVEDBLOCK16THRESHOLD, "Received Block 16 Threshold", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDSTARTOFBLOCKPERIOD, new ZclAttribute(this, ATTR_RECEIVEDSTARTOFBLOCKPERIOD, "Received Start Of Block Period", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDBLOCKPERIODDURATION, new ZclAttribute(this, ATTR_RECEIVEDBLOCKPERIODDURATION, "Received Block Period Duration", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDTHRESHOLDMULTIPLIER, new ZclAttribute(this, ATTR_RECEIVEDTHRESHOLDMULTIPLIER, "Received Threshold Multiplier", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDTHRESHOLDDIVISOR, new ZclAttribute(this, ATTR_RECEIVEDTHRESHOLDDIVISOR, "Received Threshold Divisor", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK1PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK1PRICE, "Rx No Tier Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK2PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK2PRICE, "Rx No Tier Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK3PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK3PRICE, "Rx No Tier Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK4PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK4PRICE, "Rx No Tier Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK5PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK5PRICE, "Rx No Tier Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK6PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK6PRICE, "Rx No Tier Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK7PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK7PRICE, "Rx No Tier Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK8PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK8PRICE, "Rx No Tier Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK9PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK9PRICE, "Rx No Tier Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK10PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK10PRICE, "Rx No Tier Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK11PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK11PRICE, "Rx No Tier Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK12PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK12PRICE, "Rx No Tier Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK13PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK13PRICE, "Rx No Tier Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK14PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK14PRICE, "Rx No Tier Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK15PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK15PRICE, "Rx No Tier Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXNOTIERBLOCK16PRICE, new ZclAttribute(this, ATTR_RXNOTIERBLOCK16PRICE, "Rx No Tier Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK1PRICE, "Rx Tier 1 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK2PRICE, "Rx Tier 1 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK3PRICE, "Rx Tier 1 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK4PRICE, "Rx Tier 1 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK5PRICE, "Rx Tier 1 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK6PRICE, "Rx Tier 1 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK7PRICE, "Rx Tier 1 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK8PRICE, "Rx Tier 1 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK9PRICE, "Rx Tier 1 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK10PRICE, "Rx Tier 1 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK11PRICE, "Rx Tier 1 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK12PRICE, "Rx Tier 1 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK13PRICE, "Rx Tier 1 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK14PRICE, "Rx Tier 1 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK15PRICE, "Rx Tier 1 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER1BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER1BLOCK16PRICE, "Rx Tier 1 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK1PRICE, "Rx Tier 2 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK2PRICE, "Rx Tier 2 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK3PRICE, "Rx Tier 2 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK4PRICE, "Rx Tier 2 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK5PRICE, "Rx Tier 2 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK6PRICE, "Rx Tier 2 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK7PRICE, "Rx Tier 2 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK8PRICE, "Rx Tier 2 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK9PRICE, "Rx Tier 2 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK10PRICE, "Rx Tier 2 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK11PRICE, "Rx Tier 2 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK12PRICE, "Rx Tier 2 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK13PRICE, "Rx Tier 2 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK14PRICE, "Rx Tier 2 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK15PRICE, "Rx Tier 2 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER2BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER2BLOCK16PRICE, "Rx Tier 2 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK1PRICE, "Rx Tier 3 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK2PRICE, "Rx Tier 3 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK3PRICE, "Rx Tier 3 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK4PRICE, "Rx Tier 3 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK5PRICE, "Rx Tier 3 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK6PRICE, "Rx Tier 3 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK7PRICE, "Rx Tier 3 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK8PRICE, "Rx Tier 3 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK9PRICE, "Rx Tier 3 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK10PRICE, "Rx Tier 3 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK11PRICE, "Rx Tier 3 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK12PRICE, "Rx Tier 3 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK13PRICE, "Rx Tier 3 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK14PRICE, "Rx Tier 3 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK15PRICE, "Rx Tier 3 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER3BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER3BLOCK16PRICE, "Rx Tier 3 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK1PRICE, "Rx Tier 4 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK2PRICE, "Rx Tier 4 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK3PRICE, "Rx Tier 4 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK4PRICE, "Rx Tier 4 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK5PRICE, "Rx Tier 4 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK6PRICE, "Rx Tier 4 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK7PRICE, "Rx Tier 4 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK8PRICE, "Rx Tier 4 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK9PRICE, "Rx Tier 4 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK10PRICE, "Rx Tier 4 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK11PRICE, "Rx Tier 4 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK12PRICE, "Rx Tier 4 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK13PRICE, "Rx Tier 4 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK14PRICE, "Rx Tier 4 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK15PRICE, "Rx Tier 4 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER4BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER4BLOCK16PRICE, "Rx Tier 4 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK1PRICE, "Rx Tier 5 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK2PRICE, "Rx Tier 5 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK3PRICE, "Rx Tier 5 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK4PRICE, "Rx Tier 5 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK5PRICE, "Rx Tier 5 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK6PRICE, "Rx Tier 5 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK7PRICE, "Rx Tier 5 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK8PRICE, "Rx Tier 5 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK9PRICE, "Rx Tier 5 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK10PRICE, "Rx Tier 5 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK11PRICE, "Rx Tier 5 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK12PRICE, "Rx Tier 5 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK13PRICE, "Rx Tier 5 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK14PRICE, "Rx Tier 5 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK15PRICE, "Rx Tier 5 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER5BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER5BLOCK16PRICE, "Rx Tier 5 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK1PRICE, "Rx Tier 6 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK2PRICE, "Rx Tier 6 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK3PRICE, "Rx Tier 6 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK4PRICE, "Rx Tier 6 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK5PRICE, "Rx Tier 6 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK6PRICE, "Rx Tier 6 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK7PRICE, "Rx Tier 6 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK8PRICE, "Rx Tier 6 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK9PRICE, "Rx Tier 6 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK10PRICE, "Rx Tier 6 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK11PRICE, "Rx Tier 6 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK12PRICE, "Rx Tier 6 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK13PRICE, "Rx Tier 6 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK14PRICE, "Rx Tier 6 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK15PRICE, "Rx Tier 6 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER6BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER6BLOCK16PRICE, "Rx Tier 6 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK1PRICE, "Rx Tier 7 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK2PRICE, "Rx Tier 7 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK3PRICE, "Rx Tier 7 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK4PRICE, "Rx Tier 7 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK5PRICE, "Rx Tier 7 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK6PRICE, "Rx Tier 7 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK7PRICE, "Rx Tier 7 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK8PRICE, "Rx Tier 7 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK9PRICE, "Rx Tier 7 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK10PRICE, "Rx Tier 7 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK11PRICE, "Rx Tier 7 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK12PRICE, "Rx Tier 7 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK13PRICE, "Rx Tier 7 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK14PRICE, "Rx Tier 7 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK15PRICE, "Rx Tier 7 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER7BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER7BLOCK16PRICE, "Rx Tier 7 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK1PRICE, "Rx Tier 8 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK2PRICE, "Rx Tier 8 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK3PRICE, "Rx Tier 8 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK4PRICE, "Rx Tier 8 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK5PRICE, "Rx Tier 8 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK6PRICE, "Rx Tier 8 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK7PRICE, "Rx Tier 8 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK8PRICE, "Rx Tier 8 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK9PRICE, "Rx Tier 8 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK10PRICE, "Rx Tier 8 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK11PRICE, "Rx Tier 8 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK12PRICE, "Rx Tier 8 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK13PRICE, "Rx Tier 8 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK14PRICE, "Rx Tier 8 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK15PRICE, "Rx Tier 8 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER8BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER8BLOCK16PRICE, "Rx Tier 8 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK1PRICE, "Rx Tier 9 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK2PRICE, "Rx Tier 9 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK3PRICE, "Rx Tier 9 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK4PRICE, "Rx Tier 9 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK5PRICE, "Rx Tier 9 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK6PRICE, "Rx Tier 9 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK7PRICE, "Rx Tier 9 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK8PRICE, "Rx Tier 9 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK9PRICE, "Rx Tier 9 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK10PRICE, "Rx Tier 9 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK11PRICE, "Rx Tier 9 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK12PRICE, "Rx Tier 9 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK13PRICE, "Rx Tier 9 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK14PRICE, "Rx Tier 9 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK15PRICE, "Rx Tier 9 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER9BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER9BLOCK16PRICE, "Rx Tier 9 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK1PRICE, "Rx Tier 10 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK2PRICE, "Rx Tier 10 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK3PRICE, "Rx Tier 10 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK4PRICE, "Rx Tier 10 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK5PRICE, "Rx Tier 10 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK6PRICE, "Rx Tier 10 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK7PRICE, "Rx Tier 10 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK8PRICE, "Rx Tier 10 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK9PRICE, "Rx Tier 10 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK10PRICE, "Rx Tier 10 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK11PRICE, "Rx Tier 10 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK12PRICE, "Rx Tier 10 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK13PRICE, "Rx Tier 10 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK14PRICE, "Rx Tier 10 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK15PRICE, "Rx Tier 10 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER10BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER10BLOCK16PRICE, "Rx Tier 10 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK1PRICE, "Rx Tier 11 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK2PRICE, "Rx Tier 11 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK3PRICE, "Rx Tier 11 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK4PRICE, "Rx Tier 11 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK5PRICE, "Rx Tier 11 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK6PRICE, "Rx Tier 11 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK7PRICE, "Rx Tier 11 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK8PRICE, "Rx Tier 11 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK9PRICE, "Rx Tier 11 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK10PRICE, "Rx Tier 11 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK11PRICE, "Rx Tier 11 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK12PRICE, "Rx Tier 11 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK13PRICE, "Rx Tier 11 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK14PRICE, "Rx Tier 11 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK15PRICE, "Rx Tier 11 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER11BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER11BLOCK16PRICE, "Rx Tier 11 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK1PRICE, "Rx Tier 12 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK2PRICE, "Rx Tier 12 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK3PRICE, "Rx Tier 12 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK4PRICE, "Rx Tier 12 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK5PRICE, "Rx Tier 12 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK6PRICE, "Rx Tier 12 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK7PRICE, "Rx Tier 12 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK8PRICE, "Rx Tier 12 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK9PRICE, "Rx Tier 12 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK10PRICE, "Rx Tier 12 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK11PRICE, "Rx Tier 12 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK12PRICE, "Rx Tier 12 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK13PRICE, "Rx Tier 12 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK14PRICE, "Rx Tier 12 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK15PRICE, "Rx Tier 12 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER12BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER12BLOCK16PRICE, "Rx Tier 12 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK1PRICE, "Rx Tier 13 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK2PRICE, "Rx Tier 13 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK3PRICE, "Rx Tier 13 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK4PRICE, "Rx Tier 13 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK5PRICE, "Rx Tier 13 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK6PRICE, "Rx Tier 13 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK7PRICE, "Rx Tier 13 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK8PRICE, "Rx Tier 13 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK9PRICE, "Rx Tier 13 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK10PRICE, "Rx Tier 13 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK11PRICE, "Rx Tier 13 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK12PRICE, "Rx Tier 13 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK13PRICE, "Rx Tier 13 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK14PRICE, "Rx Tier 13 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK15PRICE, "Rx Tier 13 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER13BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER13BLOCK16PRICE, "Rx Tier 13 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK1PRICE, "Rx Tier 14 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK2PRICE, "Rx Tier 14 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK3PRICE, "Rx Tier 14 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK4PRICE, "Rx Tier 14 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK5PRICE, "Rx Tier 14 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK6PRICE, "Rx Tier 14 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK7PRICE, "Rx Tier 14 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK8PRICE, "Rx Tier 14 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK9PRICE, "Rx Tier 14 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK10PRICE, "Rx Tier 14 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK11PRICE, "Rx Tier 14 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK12PRICE, "Rx Tier 14 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK13PRICE, "Rx Tier 14 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK14PRICE, "Rx Tier 14 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK15PRICE, "Rx Tier 14 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER14BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER14BLOCK16PRICE, "Rx Tier 14 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK1PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK1PRICE, "Rx Tier 15 Block 1 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK2PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK2PRICE, "Rx Tier 15 Block 2 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK3PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK3PRICE, "Rx Tier 15 Block 3 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK4PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK4PRICE, "Rx Tier 15 Block 4 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK5PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK5PRICE, "Rx Tier 15 Block 5 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK6PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK6PRICE, "Rx Tier 15 Block 6 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK7PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK7PRICE, "Rx Tier 15 Block 7 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK8PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK8PRICE, "Rx Tier 15 Block 8 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK9PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK9PRICE, "Rx Tier 15 Block 9 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK10PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK10PRICE, "Rx Tier 15 Block 10 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK11PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK11PRICE, "Rx Tier 15 Block 11 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK12PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK12PRICE, "Rx Tier 15 Block 12 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK13PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK13PRICE, "Rx Tier 15 Block 13 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK14PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK14PRICE, "Rx Tier 15 Block 14 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK15PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK15PRICE, "Rx Tier 15 Block 15 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RXTIER15BLOCK16PRICE, new ZclAttribute(this, ATTR_RXTIER15BLOCK16PRICE, "Rx Tier 15 Block 16 Price", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER16, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER16, "Received Price Tier 16", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER17, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER17, "Received Price Tier 17", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER18, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER18, "Received Price Tier 18", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER19, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER19, "Received Price Tier 19", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER20, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER20, "Received Price Tier 20", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER21, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER21, "Received Price Tier 21", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER22, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER22, "Received Price Tier 22", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER23, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER23, "Received Price Tier 23", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER24, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER24, "Received Price Tier 24", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER25, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER25, "Received Price Tier 25", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER26, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER26, "Received Price Tier 26", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER27, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER27, "Received Price Tier 27", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER28, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER28, "Received Price Tier 28", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER29, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER29, "Received Price Tier 29", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER30, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER30, "Received Price Tier 30", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER31, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER31, "Received Price Tier 31", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER32, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER32, "Received Price Tier 32", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER33, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER33, "Received Price Tier 33", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER34, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER34, "Received Price Tier 34", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER35, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER35, "Received Price Tier 35", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER36, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER36, "Received Price Tier 36", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER37, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER37, "Received Price Tier 37", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER38, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER38, "Received Price Tier 38", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER39, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER39, "Received Price Tier 39", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER40, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER40, "Received Price Tier 40", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER41, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER41, "Received Price Tier 41", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER42, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER42, "Received Price Tier 42", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER43, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER43, "Received Price Tier 43", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER44, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER44, "Received Price Tier 44", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER45, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER45, "Received Price Tier 45", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER46, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER46, "Received Price Tier 46", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER47, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER47, "Received Price Tier 47", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER48, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER48, "Received Price Tier 48", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER49, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER49, "Received Price Tier 49", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER50, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER50, "Received Price Tier 50", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER51, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER51, "Received Price Tier 51", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER52, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER52, "Received Price Tier 52", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER53, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER53, "Received Price Tier 53", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER54, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER54, "Received Price Tier 54", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER55, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER55, "Received Price Tier 55", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER56, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER56, "Received Price Tier 56", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER57, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER57, "Received Price Tier 57", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER58, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER58, "Received Price Tier 58", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER59, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER59, "Received Price Tier 59", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER60, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER60, "Received Price Tier 60", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER61, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER61, "Received Price Tier 61", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER62, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER62, "Received Price Tier 62", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDPRICETIER63, new ZclAttribute(this, ATTR_RECEIVEDPRICETIER63, "Received Price Tier 63", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDTARIFFLABEL, new ZclAttribute(this, ATTR_RECEIVEDTARIFFLABEL, "Received Tariff Label", ZclDataType.Get(DataType.OCTET_STRING), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDNUMBEROFPRICETIERSINUSE, new ZclAttribute(this, ATTR_RECEIVEDNUMBEROFPRICETIERSINUSE, "Received Number Of Price Tiers In Use", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDNUMBEROFBLOCKTHRESHOLDSINUSE, new ZclAttribute(this, ATTR_RECEIVEDNUMBEROFBLOCKTHRESHOLDSINUSE, "Received Number Of Block Thresholds In Use", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDTIERBLOCKMODE, new ZclAttribute(this, ATTR_RECEIVEDTIERBLOCKMODE, "Received Tier Block Mode", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDCO2, new ZclAttribute(this, ATTR_RECEIVEDCO2, "Received CO2", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDCO2UNIT, new ZclAttribute(this, ATTR_RECEIVEDCO2UNIT, "Received CO2 Unit", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDCO2TRAILINGDIGIT, new ZclAttribute(this, ATTR_RECEIVEDCO2TRAILINGDIGIT, "Received CO2 Trailing Digit", ZclDataType.Get(DataType.BITMAP_8_BIT), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDCURRENTBILLINGPERIODSTART, new ZclAttribute(this, ATTR_RECEIVEDCURRENTBILLINGPERIODSTART, "Received Current Billing Period Start", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDCURRENTBILLINGPERIODDURATION, new ZclAttribute(this, ATTR_RECEIVEDCURRENTBILLINGPERIODDURATION, "Received Current Billing Period Duration", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDLASTBILLINGPERIODSTART, new ZclAttribute(this, ATTR_RECEIVEDLASTBILLINGPERIODSTART, "Received Last Billing Period Start", ZclDataType.Get(DataType.UTCTIME), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDLASTBILLINGPERIODDURATION, new ZclAttribute(this, ATTR_RECEIVEDLASTBILLINGPERIODDURATION, "Received Last Billing Period Duration", ZclDataType.Get(DataType.UNSIGNED_24_BIT_INTEGER), false, true, false, false));
            attributeMap.Add(ATTR_RECEIVEDLASTBILLINGPERIODCONSOLIDATEDBILL, new ZclAttribute(this, ATTR_RECEIVEDLASTBILLINGPERIODCONSOLIDATEDBILL, "Received Last Billing Period Consolidated Bill", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(15);

            commandMap.Add(0x0000, () => new PublishPriceCommand());
            commandMap.Add(0x0001, () => new PublishBlockPeriodCommand());
            commandMap.Add(0x0002, () => new PublishConversionFactorCommand());
            commandMap.Add(0x0003, () => new PublishCalorificValueCommand());
            commandMap.Add(0x0004, () => new PublishTariffInformationCommand());
            commandMap.Add(0x0005, () => new PublishPriceMatrixCommand());
            commandMap.Add(0x0006, () => new PublishBlockThresholdsCommand());
            commandMap.Add(0x0007, () => new PublishCo2ValueCommand());
            commandMap.Add(0x0008, () => new PublishTierLabelsCommand());
            commandMap.Add(0x0009, () => new PublishBillingPeriodCommand());
            commandMap.Add(0x000A, () => new PublishConsolidatedBillCommand());
            commandMap.Add(0x000B, () => new PublishCppEventCommand());
            commandMap.Add(0x000C, () => new PublishCreditPaymentCommand());
            commandMap.Add(0x000D, () => new PublishCurrencyConversionCommand());
            commandMap.Add(0x000E, () => new CancelTariffCommand());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(17);

            commandMap.Add(0x0000, () => new GetCurrentPriceCommand());
            commandMap.Add(0x0001, () => new GetScheduledPricesCommand());
            commandMap.Add(0x0002, () => new PriceAcknowledgementCommand());
            commandMap.Add(0x0003, () => new GetBlockPeriodCommand());
            commandMap.Add(0x0004, () => new GetConversionFactorCommand());
            commandMap.Add(0x0005, () => new GetCalorificValueCommand());
            commandMap.Add(0x0006, () => new GetTariffInformationCommand());
            commandMap.Add(0x0007, () => new GetPriceMatrixCommand());
            commandMap.Add(0x0008, () => new GetBlockThresholdsCommand());
            commandMap.Add(0x0009, () => new GetCo2ValueCommand());
            commandMap.Add(0x000A, () => new GetTierLabelsCommand());
            commandMap.Add(0x000B, () => new GetBillingPeriodCommand());
            commandMap.Add(0x000C, () => new GetConsolidatedBillCommand());
            commandMap.Add(0x000D, () => new CppEventResponse());
            commandMap.Add(0x000E, () => new GetCreditPaymentCommand());
            commandMap.Add(0x000F, () => new GetCurrencyConversionCommand());
            commandMap.Add(0x0010, () => new GetTariffCancellationCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Price cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclPriceCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Get Current Price Command
        ///
        /// This command initiates a PublishPrice command for the current time. On receipt of
        /// this command, the device shall send a PublishPrice command for the currently
        /// scheduled time.
        ///
        /// <param name="commandOptions" <see cref="byte"> Command Options</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetCurrentPriceCommand(byte commandOptions)
        {
            GetCurrentPriceCommand command = new GetCurrentPriceCommand();

            // Set the fields
            command.CommandOptions = commandOptions;

            return Send(command);
        }

        /// <summary>
        /// The Get Scheduled Prices Command
        ///
        /// This command initiates a PublishPrice command for available price events. A
        /// server device shall be capable of storing five price events at a minimum On receipt
        /// of this command, the device shall send a PublishPrice command for the currently
        /// scheduled time.
        ///
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="numberOfEvents" <see cref="byte"> Number Of Events</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetScheduledPricesCommand(DateTime startTime, byte numberOfEvents)
        {
            GetScheduledPricesCommand command = new GetScheduledPricesCommand();

            // Set the fields
            command.StartTime = startTime;
            command.NumberOfEvents = numberOfEvents;

            return Send(command);
        }

        /// <summary>
        /// The Price Acknowledgement Command
        ///
        /// The PriceAcknowledgement command provides the ability to acknowledge a
        /// previously sent PublishPrice command. It is mandatory for 1.1 and later devices.
        /// For SE 1.0 devices, the command is optional.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="byte"> Issuer Event ID</ param >
        /// <param name="priceAckTime" <see cref="DateTime"> Price Ack Time</ param >
        /// <param name="control" <see cref="byte"> Control</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PriceAcknowledgementCommand(uint providerId, byte issuerEventId, DateTime priceAckTime, byte control)
        {
            PriceAcknowledgementCommand command = new PriceAcknowledgementCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.PriceAckTime = priceAckTime;
            command.Control = control;

            return Send(command);
        }

        /// <summary>
        /// The Get Block Period Command
        ///
        /// This command initiates a PublishBlockPeriod command for the currently scheduled
        /// block periods. A server device shall be capable of storing at least two commands,
        /// the current period and a period to be activated in the near future. <br> A ZCL Default
        /// response with status NOT_FOUND shall be returned if there are no events available.
        ///
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="numberOfEvents" <see cref="byte"> Number Of Events</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetBlockPeriodCommand(DateTime startTime, byte numberOfEvents, byte tariffType)
        {
            GetBlockPeriodCommand command = new GetBlockPeriodCommand();

            // Set the fields
            command.StartTime = startTime;
            command.NumberOfEvents = numberOfEvents;
            command.TariffType = tariffType;

            return Send(command);
        }

        /// <summary>
        /// The Get Conversion Factor Command
        ///
        /// This command initiates a PublishConversionFactor command(s) for scheduled
        /// conversion factor updates. A server device shall be capable of storing at least two
        /// instances, the current and (if available) next instance to be activated in the
        /// future. <br> A ZCL Default response with status NOT_FOUND shall be returned if
        /// there are no conversion factor updates available
        ///
        /// <param name="earliestStartTime" <see cref="DateTime"> Earliest Start Time</ param >
        /// <param name="minIssuerEventId" <see cref="uint"> Min . Issuer Event ID</ param >
        /// <param name="numberOfCommands" <see cref="byte"> Number Of Commands</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetConversionFactorCommand(DateTime earliestStartTime, uint minIssuerEventId, byte numberOfCommands)
        {
            GetConversionFactorCommand command = new GetConversionFactorCommand();

            // Set the fields
            command.EarliestStartTime = earliestStartTime;
            command.MinIssuerEventId = minIssuerEventId;
            command.NumberOfCommands = numberOfCommands;

            return Send(command);
        }

        /// <summary>
        /// The Get Calorific Value Command
        ///
        /// This command initiates a PublishCalorificValue command(s) for scheduled
        /// calorific value updates. A server device shall be capable of storing at least two
        /// instances, the current and (if available) next instance to be activated in the
        /// future. <br> A ZCL Default response with status NOT_FOUND shall be returned if
        /// there are no conversion factor updates available
        ///
        /// <param name="earliestStartTime" <see cref="DateTime"> Earliest Start Time</ param >
        /// <param name="minIssuerEventId" <see cref="uint"> Min . Issuer Event ID</ param >
        /// <param name="numberOfCommands" <see cref="byte"> Number Of Commands</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetCalorificValueCommand(DateTime earliestStartTime, uint minIssuerEventId, byte numberOfCommands)
        {
            GetCalorificValueCommand command = new GetCalorificValueCommand();

            // Set the fields
            command.EarliestStartTime = earliestStartTime;
            command.MinIssuerEventId = minIssuerEventId;
            command.NumberOfCommands = numberOfCommands;

            return Send(command);
        }

        /// <summary>
        /// The Get Tariff Information Command
        ///
        /// This command initiates PublishTariffInformation command(s) for scheduled
        /// tariff updates. A server device shall be capable of storing at least two instances,
        /// current and the next instance to be activated in the future. <br> One or more
        /// PublishTariffInformation commands are sent in response to this command. To
        /// obtain the complete tariff details, further GetPriceMatrix and
        /// GetBlockThesholds commands must be sent using the start time and IssuerTariffID
        /// obtained from the appropriate PublishTariffInformation command.
        ///
        /// <param name="earliestStartTime" <see cref="DateTime"> Earliest Start Time</ param >
        /// <param name="minIssuerEventId" <see cref="uint"> Min . Issuer Event ID</ param >
        /// <param name="numberOfCommands" <see cref="byte"> Number Of Commands</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetTariffInformationCommand(DateTime earliestStartTime, uint minIssuerEventId, byte numberOfCommands, byte tariffType)
        {
            GetTariffInformationCommand command = new GetTariffInformationCommand();

            // Set the fields
            command.EarliestStartTime = earliestStartTime;
            command.MinIssuerEventId = minIssuerEventId;
            command.NumberOfCommands = numberOfCommands;
            command.TariffType = tariffType;

            return Send(command);
        }

        /// <summary>
        /// The Get Price Matrix Command
        ///
        /// This command initiates a PublishPriceMatrix command for the scheduled Price
        /// Matrix updates. A server device shall be capable of storing at least two instances,
        /// current and next instance to be activated in the future. <br> A ZCL Default response
        /// with status NOT_FOUND shall be returned if there are no Price Matrix updates
        /// available.
        ///
        /// <param name="issuerTariffId" <see cref="uint"> Issuer Tariff ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetPriceMatrixCommand(uint issuerTariffId)
        {
            GetPriceMatrixCommand command = new GetPriceMatrixCommand();

            // Set the fields
            command.IssuerTariffId = issuerTariffId;

            return Send(command);
        }

        /// <summary>
        /// The Get Block Thresholds Command
        ///
        /// This command initiates a PublishBlockThreshold command for the scheduled Block
        /// Threshold updates. A server device shall be capable of storing at least two
        /// instances, current and next instance to be activated in the future. <br> A ZCL
        /// Default response with status NOT_FOUND shall be returned if there are no Price
        /// Matrix updates available.
        ///
        /// <param name="issuerTariffId" <see cref="uint"> Issuer Tariff ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetBlockThresholdsCommand(uint issuerTariffId)
        {
            GetBlockThresholdsCommand command = new GetBlockThresholdsCommand();

            // Set the fields
            command.IssuerTariffId = issuerTariffId;

            return Send(command);
        }

        /// <summary>
        /// The Get CO2 Value Command
        ///
        /// This command initiates PublishCO2Value command(s) for scheduled CO2 conversion
        /// factor updates. A server device shall be capable of storing at least two instances,
        /// current and (if available) next instance to be activated in the future.
        ///
        /// <param name="earliestStartTime" <see cref="DateTime"> Earliest Start Time</ param >
        /// <param name="minIssuerEventId" <see cref="uint"> Min . Issuer Event ID</ param >
        /// <param name="numberOfCommands" <see cref="byte"> Number Of Commands</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetCo2ValueCommand(DateTime earliestStartTime, uint minIssuerEventId, byte numberOfCommands, byte tariffType)
        {
            GetCo2ValueCommand command = new GetCo2ValueCommand();

            // Set the fields
            command.EarliestStartTime = earliestStartTime;
            command.MinIssuerEventId = minIssuerEventId;
            command.NumberOfCommands = numberOfCommands;
            command.TariffType = tariffType;

            return Send(command);
        }

        /// <summary>
        /// The Get Tier Labels Command
        ///
        /// This command allows a CLIENT to retrieve the tier labels associated with a given
        /// tariff; this command initiates a PublishTierLabels command from the server.
        ///
        /// <param name="issuerTariffId" <see cref="uint"> Issuer Tariff ID</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetTierLabelsCommand(uint issuerTariffId)
        {
            GetTierLabelsCommand command = new GetTierLabelsCommand();

            // Set the fields
            command.IssuerTariffId = issuerTariffId;

            return Send(command);
        }

        /// <summary>
        /// The Get Billing Period Command
        ///
        /// This command initiates one or more PublishBillingPeriod commands for currently
        /// scheduled billing periods.
        ///
        /// <param name="earliestStartTime" <see cref="DateTime"> Earliest Start Time</ param >
        /// <param name="minIssuerEventId" <see cref="uint"> Min . Issuer Event ID</ param >
        /// <param name="numberOfCommands" <see cref="byte"> Number Of Commands</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetBillingPeriodCommand(DateTime earliestStartTime, uint minIssuerEventId, byte numberOfCommands, byte tariffType)
        {
            GetBillingPeriodCommand command = new GetBillingPeriodCommand();

            // Set the fields
            command.EarliestStartTime = earliestStartTime;
            command.MinIssuerEventId = minIssuerEventId;
            command.NumberOfCommands = numberOfCommands;
            command.TariffType = tariffType;

            return Send(command);
        }

        /// <summary>
        /// The Get Consolidated Bill Command
        ///
        /// This command initiates one or more PublishConsolidatedBill commands with the
        /// requested billing information.
        ///
        /// <param name="earliestStartTime" <see cref="DateTime"> Earliest Start Time</ param >
        /// <param name="minIssuerEventId" <see cref="uint"> Min . Issuer Event ID</ param >
        /// <param name="numberOfCommands" <see cref="byte"> Number Of Commands</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetConsolidatedBillCommand(DateTime earliestStartTime, uint minIssuerEventId, byte numberOfCommands, byte tariffType)
        {
            GetConsolidatedBillCommand command = new GetConsolidatedBillCommand();

            // Set the fields
            command.EarliestStartTime = earliestStartTime;
            command.MinIssuerEventId = minIssuerEventId;
            command.NumberOfCommands = numberOfCommands;
            command.TariffType = tariffType;

            return Send(command);
        }

        /// <summary>
        /// The Cpp Event Response
        ///
        /// The CPPEventResponse command is sent from a CLIENT (IHD) to the ESI to notify it of a
        /// Critical Peak Pricing event authorization.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="cppAuth" <see cref="byte"> Cpp Auth</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CppEventResponse(uint issuerEventId, byte cppAuth)
        {
            CppEventResponse command = new CppEventResponse();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.CppAuth = cppAuth;

            return Send(command);
        }

        /// <summary>
        /// The Get Credit Payment Command
        ///
        /// This command initiates PublishCreditPayment commands for the requested credit
        /// payment information.
        ///
        /// <param name="latestEndTime" <see cref="DateTime"> Latest End Time</ param >
        /// <param name="numberOfRecords" <see cref="byte"> Number Of Records</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetCreditPaymentCommand(DateTime latestEndTime, byte numberOfRecords)
        {
            GetCreditPaymentCommand command = new GetCreditPaymentCommand();

            // Set the fields
            command.LatestEndTime = latestEndTime;
            command.NumberOfRecords = numberOfRecords;

            return Send(command);
        }

        /// <summary>
        /// The Get Currency Conversion Command
        ///
        /// This command initiates a PublishCurrencyConversion command for the currency
        /// conversion factor updates. A server shall be capable of storing both the old and the
        /// new currencies. <br> A ZCL Default response with status NOT_FOUND shall be
        /// returned if there are no currency conversion factor updates available
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetCurrencyConversionCommand()
        {
            return Send(new GetCurrencyConversionCommand());
        }

        /// <summary>
        /// The Get Tariff Cancellation Command
        ///
        /// This command initiates the return of the last CancelTariff command held on the
        /// associated server. <br> A ZCL Default response with status NOT_FOUND shall be
        /// returned if there is no CancelTariff command available.
        ///
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetTariffCancellationCommand()
        {
            return Send(new GetTariffCancellationCommand());
        }

        /// <summary>
        /// The Publish Price Command
        ///
        /// The Publish Price command is generated in response to receiving a Get Current Price
        /// command, in response to a Get Scheduled Prices command, and when an update to the
        /// pricing information is available from the commodity provider, either before or
        /// when a TOU price becomes active. Additionally the Publish Price Command is
        /// generated when Block Pricing is in effect. When a Get Current Price or Get Scheduled
        /// Prices command is received over a ZigBee Smart Energy network, the Publish Price
        /// command should be sent unicast to the requester. In the case of an update to the
        /// pricing information from the commodity provider, the Publish Price command
        /// should be unicast to all individually registered devices implementing the Price
        /// Cluster on the ZigBee Smart Energy network. <br> Devices capable of receiving this
        /// command must be capable of storing and supporting at least two pricing information
        /// instances, the current active price and the next price. By supporting at least two
        /// pricing information instances, receiving devices will allow the Publish Price
        /// command generator to publish the next pricing information during the current
        /// pricing period. <br> Nested and overlapping Publish Price commands are not
        /// allowed. The current active price will be replaced if new price information is
        /// received by the ESI. In the case of overlapping events, the event with the newer
        /// Issuer Event ID takes priority over all nested and overlapping events. All
        /// existing events that overlap, even partially, should be removed. The only
        /// exception to this is that if an event with a newer Issuer Event ID overlaps with the
        /// end of the current active price but is not yet active, the active price is not deleted
        /// but its duration is modified to 0xFFFF (until changed) so that the active price ends
        /// when the new event begins.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="rateLabel" <see cref="ByteArray"> Rate Label</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="currentTime" <see cref="DateTime"> Current Time</ param >
        /// <param name="unitOfMeasure" <see cref="byte"> Unit Of Measure</ param >
        /// <param name="currency" <see cref="ushort"> Currency</ param >
        /// <param name="priceTrailingDigitAndTier" <see cref="byte"> Price Trailing Digit And Tier</ param >
        /// <param name="numberOfPriceTiers" <see cref="byte"> Number Of Price Tiers</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="duration" <see cref="ushort"> Duration</ param >
        /// <param name="price" <see cref="uint"> Price</ param >
        /// <param name="priceRatio" <see cref="byte"> Price Ratio</ param >
        /// <param name="generationPrice" <see cref="uint"> Generation Price</ param >
        /// <param name="generationPriceRatio" <see cref="uint"> Generation Price Ratio</ param >
        /// <param name="alternateCostDelivered" <see cref="uint"> Alternate Cost Delivered</ param >
        /// <param name="alternateCostUnit" <see cref="byte"> Alternate Cost Unit</ param >
        /// <param name="alternateCostTrailingDigit" <see cref="byte"> Alternate Cost Trailing Digit</ param >
        /// <param name="numberOfBlockThresholds" <see cref="byte"> Number Of Block Thresholds</ param >
        /// <param name="priceControl" <see cref="byte"> Price Control</ param >
        /// <param name="numberOfGenerationTiers" <see cref="byte"> Number Of Generation Tiers</ param >
        /// <param name="generationTier" <see cref="byte"> Generation Tier</ param >
        /// <param name="extendedNumberOfPriceTiers" <see cref="byte"> Extended Number Of Price Tiers</ param >
        /// <param name="extendedPriceTier" <see cref="byte"> Extended Price Tier</ param >
        /// <param name="extendedRegisterTier" <see cref="byte"> Extended Register Tier</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishPriceCommand(uint providerId, ByteArray rateLabel, uint issuerEventId, DateTime currentTime, byte unitOfMeasure, ushort currency, byte priceTrailingDigitAndTier, byte numberOfPriceTiers, DateTime startTime, ushort duration, uint price, byte priceRatio, uint generationPrice, uint generationPriceRatio, uint alternateCostDelivered, byte alternateCostUnit, byte alternateCostTrailingDigit, byte numberOfBlockThresholds, byte priceControl, byte numberOfGenerationTiers, byte generationTier, byte extendedNumberOfPriceTiers, byte extendedPriceTier, byte extendedRegisterTier)
        {
            PublishPriceCommand command = new PublishPriceCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.RateLabel = rateLabel;
            command.IssuerEventId = issuerEventId;
            command.CurrentTime = currentTime;
            command.UnitOfMeasure = unitOfMeasure;
            command.Currency = currency;
            command.PriceTrailingDigitAndTier = priceTrailingDigitAndTier;
            command.NumberOfPriceTiers = numberOfPriceTiers;
            command.StartTime = startTime;
            command.Duration = duration;
            command.Price = price;
            command.PriceRatio = priceRatio;
            command.GenerationPrice = generationPrice;
            command.GenerationPriceRatio = generationPriceRatio;
            command.AlternateCostDelivered = alternateCostDelivered;
            command.AlternateCostUnit = alternateCostUnit;
            command.AlternateCostTrailingDigit = alternateCostTrailingDigit;
            command.NumberOfBlockThresholds = numberOfBlockThresholds;
            command.PriceControl = priceControl;
            command.NumberOfGenerationTiers = numberOfGenerationTiers;
            command.GenerationTier = generationTier;
            command.ExtendedNumberOfPriceTiers = extendedNumberOfPriceTiers;
            command.ExtendedPriceTier = extendedPriceTier;
            command.ExtendedRegisterTier = extendedRegisterTier;

            return Send(command);
        }

        /// <summary>
        /// The Publish Block Period Command
        ///
        /// The Publish Block Period command is generated in response to receiving a Get Block
        /// Period(s) command or when an update to the block tariff schedule is available from
        /// the commodity provider. When the Get Block Period(s) command is received over the
        /// ZigBee Smart Energy network, the Publish Block Period command(s) should be sent
        /// unicast to the requestor. In the case of an update to the block tariff schedule from
        /// the commodity provider, the Publish Block Period command should be unicast to all
        /// individually registered devices implementing the Price Cluster on the ZigBee
        /// Smart Energy network. <br> Devices capable of receiving this command must be
        /// capable of storing and supporting two block periods, the current active block and
        /// the next block. By supporting two block periods, receiving devices will allow the
        /// Publish Block Period command generator to publish the next block information
        /// during the current block period.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="blockPeriodStartTime" <see cref="DateTime"> Block Period Start Time</ param >
        /// <param name="blockPeriodDuration" <see cref="uint"> Block Period Duration</ param >
        /// <param name="blockPeriodControl" <see cref="byte"> Block Period Control</ param >
        /// <param name="blockPeriodDurationType" <see cref="byte"> Block Period Duration Type</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <param name="tariffResolutionPeriod" <see cref="byte"> Tariff Resolution Period</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishBlockPeriodCommand(uint providerId, uint issuerEventId, DateTime blockPeriodStartTime, uint blockPeriodDuration, byte blockPeriodControl, byte blockPeriodDurationType, byte tariffType, byte tariffResolutionPeriod)
        {
            PublishBlockPeriodCommand command = new PublishBlockPeriodCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.BlockPeriodStartTime = blockPeriodStartTime;
            command.BlockPeriodDuration = blockPeriodDuration;
            command.BlockPeriodControl = blockPeriodControl;
            command.BlockPeriodDurationType = blockPeriodDurationType;
            command.TariffType = tariffType;
            command.TariffResolutionPeriod = tariffResolutionPeriod;

            return Send(command);
        }

        /// <summary>
        /// The Publish Conversion Factor Command
        ///
        /// The PublishConversionFactor command is sent in response to a
        /// GetConversionFactor command or if a new conversion factor is available. Clients
        /// shall be capable of storing at least two instances of the Conversion Factor, the
        /// currently active one and the next one.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="conversionFactor" <see cref="uint"> Conversion Factor</ param >
        /// <param name="conversionFactorTrailingDigit" <see cref="byte"> Conversion Factor Trailing Digit</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishConversionFactorCommand(uint issuerEventId, DateTime startTime, uint conversionFactor, byte conversionFactorTrailingDigit)
        {
            PublishConversionFactorCommand command = new PublishConversionFactorCommand();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.StartTime = startTime;
            command.ConversionFactor = conversionFactor;
            command.ConversionFactorTrailingDigit = conversionFactorTrailingDigit;

            return Send(command);
        }

        /// <summary>
        /// The Publish Calorific Value Command
        ///
        /// The PublishCalorificValue command is sent in response to a GetCalorificValue
        /// command or if a new calorific value is available. Clients shall be capable of
        /// storing at least two instances of the Calorific Value, the currently active one and
        /// the next one.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="calorificValue" <see cref="uint"> Calorific Value</ param >
        /// <param name="calorificValueUnit" <see cref="byte"> Calorific Value Unit</ param >
        /// <param name="calorificValueTrailingDigit" <see cref="byte"> Calorific Value Trailing Digit</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishCalorificValueCommand(uint issuerEventId, DateTime startTime, uint calorificValue, byte calorificValueUnit, byte calorificValueTrailingDigit)
        {
            PublishCalorificValueCommand command = new PublishCalorificValueCommand();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.StartTime = startTime;
            command.CalorificValue = calorificValue;
            command.CalorificValueUnit = calorificValueUnit;
            command.CalorificValueTrailingDigit = calorificValueTrailingDigit;

            return Send(command);
        }

        /// <summary>
        /// The Publish Tariff Information Command
        ///
        /// The PublishTariffInformation command is sent in response to a
        /// GetTariffInformation command or if new tariff information is available
        /// (including Price Matrix and Block Thresholds). Clients should be capable of
        /// storing at least two instances of the Tariff Information, the currently active and
        /// the next one. Note that there may be separate tariff information for consumption
        /// delivered and received. <br> Note that the payload for this command could be up to 61
        /// bytes in length, therefore fragmentation may be required. <br> If the CLIENT is
        /// unable to store this PublishTariffInformation command, the device should
        /// respond using a ZCL Default Response with a status of INSUFFICIENT_SPACE.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="issuerTariffId" <see cref="uint"> Issuer Tariff ID</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <param name="tariffLabel" <see cref="ByteArray"> Tariff Label</ param >
        /// <param name="numberOfPriceTiers" <see cref="byte"> Number Of Price Tiers</ param >
        /// <param name="numberOfBlockThresholds" <see cref="byte"> Number Of Block Thresholds</ param >
        /// <param name="unitOfMeasure" <see cref="byte"> Unit Of Measure</ param >
        /// <param name="currency" <see cref="ushort"> Currency</ param >
        /// <param name="priceTrailingDigit" <see cref="byte"> Price Trailing Digit</ param >
        /// <param name="standingCharge" <see cref="uint"> Standing Charge</ param >
        /// <param name="tierBlockMode" <see cref="byte"> Tier Block Mode</ param >
        /// <param name="blockThresholdMultiplier" <see cref="uint"> Block Threshold Multiplier</ param >
        /// <param name="blockThresholdDivisor" <see cref="uint"> Block Threshold Divisor</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishTariffInformationCommand(uint providerId, uint issuerEventId, uint issuerTariffId, DateTime startTime, byte tariffType, ByteArray tariffLabel, byte numberOfPriceTiers, byte numberOfBlockThresholds, byte unitOfMeasure, ushort currency, byte priceTrailingDigit, uint standingCharge, byte tierBlockMode, uint blockThresholdMultiplier, uint blockThresholdDivisor)
        {
            PublishTariffInformationCommand command = new PublishTariffInformationCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.IssuerTariffId = issuerTariffId;
            command.StartTime = startTime;
            command.TariffType = tariffType;
            command.TariffLabel = tariffLabel;
            command.NumberOfPriceTiers = numberOfPriceTiers;
            command.NumberOfBlockThresholds = numberOfBlockThresholds;
            command.UnitOfMeasure = unitOfMeasure;
            command.Currency = currency;
            command.PriceTrailingDigit = priceTrailingDigit;
            command.StandingCharge = standingCharge;
            command.TierBlockMode = tierBlockMode;
            command.BlockThresholdMultiplier = blockThresholdMultiplier;
            command.BlockThresholdDivisor = blockThresholdDivisor;

            return Send(command);
        }

        /// <summary>
        /// The Publish Price Matrix Command
        ///
        /// The PublishPriceMatrix command is used to publish the Block Price Information Set
        /// (up to 15 tiers x 15 blocks) and the Extended Price Information Set (up to 48 tiers).
        /// The PublishPriceMatrix command is sent in response to a GetPriceMatrix command.
        /// Clients should be capable of storing at least two instances of the Price Matrix, the
        /// currently active and the next one. There may be a separate Price Matrix for
        /// consumption delivered and received; in this case, each Price Matrix will be
        /// identified by a different IssuerTariffId value. The Price server shall send only
        /// the number of tiers and blocks as defined in the corresponding
        /// PublishTariffInformation command (NumberofPriceTiersinUse,
        /// NumberofBlockThresholdsinUse+1) <br> The maximum application payload may not
        /// be sufficient to transfer all Price Matrix elements in one command. Therefore the
        /// ESI may send as many PublishPriceMatrix commands as needed. In this case the first
        /// command shall have CommandIndex set to 0, the second to 1 and so on; all associated
        /// commands shall use the same value of Issuer Event ID. Note that, in this case, it is
        /// the client’s responsibility to ensure that it receives all associated
        /// PublishPriceMatrix commands before any of the payloads can be used.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="issuerTariffId" <see cref="uint"> Issuer Tariff ID</ param >
        /// <param name="commandIndex" <see cref="byte"> Command Index</ param >
        /// <param name="totalNumberOfCommands" <see cref="byte"> Total Number Of Commands</ param >
        /// <param name="subPayloadControl" <see cref="byte"> Sub Payload Control</ param >
        /// <param name="priceMatrixSubPayload" <see cref="PriceMatrixSubPayload"> Price Matrix Sub Payload</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishPriceMatrixCommand(uint providerId, uint issuerEventId, DateTime startTime, uint issuerTariffId, byte commandIndex, byte totalNumberOfCommands, byte subPayloadControl, PriceMatrixSubPayload priceMatrixSubPayload)
        {
            PublishPriceMatrixCommand command = new PublishPriceMatrixCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.StartTime = startTime;
            command.IssuerTariffId = issuerTariffId;
            command.CommandIndex = commandIndex;
            command.TotalNumberOfCommands = totalNumberOfCommands;
            command.SubPayloadControl = subPayloadControl;
            command.PriceMatrixSubPayload = priceMatrixSubPayload;

            return Send(command);
        }

        /// <summary>
        /// The Publish Block Thresholds Command
        ///
        /// The PublishBlockThresholds command is sent in response to a GetBlockThresholds
        /// command. Clients should be capable of storing at least two instances of the Block
        /// Thresholds, the currently active and the next one. <br> There may be a separate set
        /// of Block Thresholds for consumption delivered and received; in this case, each set
        /// of Block Thresholds will be identified by a different IssuerTariffId value. <br>
        /// The price server shall send only the number of block thresholds in use
        /// (NumberofBlockThresholdsInUse) as defined in the PublishTariffInformation
        /// command. <br> The maximum application payload may not be sufficient to transfer
        /// all thresholds in one command. In this case the Price server may send two
        /// consecutive PublishBlockThreshold commands (CommandIndex set to 0 and 1
        /// respectively); both commands shall use the same value of Issuer Event ID. Note
        /// that, in this case, it is the client’s responsibility to ensure that it receives all
        /// associated PublishBlockThreshold commands before any of the payloads can be
        /// used.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="issuerTariffId" <see cref="uint"> Issuer Tariff ID</ param >
        /// <param name="commandIndex" <see cref="byte"> Command Index</ param >
        /// <param name="totalNumberOfCommands" <see cref="byte"> Total Number Of Commands</ param >
        /// <param name="subPayloadControl" <see cref="byte"> Sub Payload Control</ param >
        /// <param name="blockThresholdSubPayload" <see cref="BlockThresholdSubPayload"> Block Threshold Sub Payload</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishBlockThresholdsCommand(uint providerId, uint issuerEventId, DateTime startTime, uint issuerTariffId, byte commandIndex, byte totalNumberOfCommands, byte subPayloadControl, BlockThresholdSubPayload blockThresholdSubPayload)
        {
            PublishBlockThresholdsCommand command = new PublishBlockThresholdsCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.StartTime = startTime;
            command.IssuerTariffId = issuerTariffId;
            command.CommandIndex = commandIndex;
            command.TotalNumberOfCommands = totalNumberOfCommands;
            command.SubPayloadControl = subPayloadControl;
            command.BlockThresholdSubPayload = blockThresholdSubPayload;

            return Send(command);
        }

        /// <summary>
        /// The Publish CO2 Value Command
        ///
        /// The PublishCO2Value command is sent in response to a GetCO2Value command or if a new
        /// CO2 conversion factor is available. Clients should be capable of storing at least
        /// two instances of the CO2 conversion factor, the currently active and the next one.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <param name="co2Value" <see cref="uint"> CO2 Value</ param >
        /// <param name="co2ValueUnit" <see cref="byte"> CO2 Value Unit</ param >
        /// <param name="co2ValueTrailingDigit" <see cref="byte"> CO2 Value Trailing Digit</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishCo2ValueCommand(uint providerId, uint issuerEventId, DateTime startTime, byte tariffType, uint co2Value, byte co2ValueUnit, byte co2ValueTrailingDigit)
        {
            PublishCo2ValueCommand command = new PublishCo2ValueCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.StartTime = startTime;
            command.TariffType = tariffType;
            command.Co2Value = co2Value;
            command.Co2ValueUnit = co2ValueUnit;
            command.Co2ValueTrailingDigit = co2ValueTrailingDigit;

            return Send(command);
        }

        /// <summary>
        /// The Publish Tier Labels Command
        ///
        /// The PublishTierLabels command is generated in response to receiving a
        /// GetTierLabels command or when there is a tier label change
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="issuerTariffId" <see cref="uint"> Issuer Tariff ID</ param >
        /// <param name="commandIndex" <see cref="byte"> Command Index</ param >
        /// <param name="totalNumberOfCommands" <see cref="byte"> Total Number Of Commands</ param >
        /// <param name="numberOfLabels" <see cref="byte"> Number Of Labels</ param >
        /// <param name="tierId" <see cref="byte"> Tier ID</ param >
        /// <param name="tierLabel" <see cref="ByteArray"> Tier Label</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishTierLabelsCommand(uint providerId, uint issuerEventId, uint issuerTariffId, byte commandIndex, byte totalNumberOfCommands, byte numberOfLabels, byte tierId, ByteArray tierLabel)
        {
            PublishTierLabelsCommand command = new PublishTierLabelsCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.IssuerTariffId = issuerTariffId;
            command.CommandIndex = commandIndex;
            command.TotalNumberOfCommands = totalNumberOfCommands;
            command.NumberOfLabels = numberOfLabels;
            command.TierId = tierId;
            command.TierLabel = tierLabel;

            return Send(command);
        }

        /// <summary>
        /// The Publish Billing Period Command
        ///
        /// The PublishBillingPeriod command is generated in response to receiving a
        /// GetBillingPeriod(s) command or when an update to the Billing schedule is
        /// available from the commodity supplier. Nested and overlapping
        /// PublishBillingPeriod commands are not allowed. In the case of overlapping
        /// billing periods, the period with the newer IssuerEventID takes priority over all
        /// nested and overlapping periods. All existing periods that overlap, even
        /// partially, should be removed. Note however that there may be separate billing
        /// schedules for consumption delivered and received.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="billingPeriodStartTime" <see cref="DateTime"> Billing Period Start Time</ param >
        /// <param name="billingPeriodDuration" <see cref="uint"> Billing Period Duration</ param >
        /// <param name="billingPeriodDurationType" <see cref="byte"> Billing Period Duration Type</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishBillingPeriodCommand(uint providerId, uint issuerEventId, DateTime billingPeriodStartTime, uint billingPeriodDuration, byte billingPeriodDurationType, byte tariffType)
        {
            PublishBillingPeriodCommand command = new PublishBillingPeriodCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.BillingPeriodStartTime = billingPeriodStartTime;
            command.BillingPeriodDuration = billingPeriodDuration;
            command.BillingPeriodDurationType = billingPeriodDurationType;
            command.TariffType = tariffType;

            return Send(command);
        }

        /// <summary>
        /// The Publish Consolidated Bill Command
        ///
        /// The PublishConsolidatedBill command is used to make consolidated billing
        /// information from previous billing periods available to other end devices. This
        /// command is issued in response to a GetConsolidatedBill command or if new billing
        /// information is available. Nested and overlapping PublishConsolidatedBill
        /// commands are not allowed. In the case of overlapping consolidated bills, the bill
        /// with the newer IssuerEventID takes priority over all nested and overlapping
        /// bills. All existing bills that overlap, even partially, should be removed. <br>
        /// Note however that there may be separate consolidated bills for consumption
        /// delivered and received.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="billingPeriodStartTime" <see cref="DateTime"> Billing Period Start Time</ param >
        /// <param name="billingPeriodDuration" <see cref="uint"> Billing Period Duration</ param >
        /// <param name="billingPeriodDurationType" <see cref="byte"> Billing Period Duration Type</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <param name="consolidatedBill" <see cref="uint"> Consolidated Bill</ param >
        /// <param name="currency" <see cref="ushort"> Currency</ param >
        /// <param name="billTrailingDigit" <see cref="byte"> Bill Trailing Digit</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishConsolidatedBillCommand(uint providerId, uint issuerEventId, DateTime billingPeriodStartTime, uint billingPeriodDuration, byte billingPeriodDurationType, byte tariffType, uint consolidatedBill, ushort currency, byte billTrailingDigit)
        {
            PublishConsolidatedBillCommand command = new PublishConsolidatedBillCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.BillingPeriodStartTime = billingPeriodStartTime;
            command.BillingPeriodDuration = billingPeriodDuration;
            command.BillingPeriodDurationType = billingPeriodDurationType;
            command.TariffType = tariffType;
            command.ConsolidatedBill = consolidatedBill;
            command.Currency = currency;
            command.BillTrailingDigit = billTrailingDigit;

            return Send(command);
        }

        /// <summary>
        /// The Publish Cpp Event Command
        ///
        /// The PublishCPPEvent command is sent from an ESI to its Price clients to notify them
        /// of a Critical Peak Pricing (CPP) event. <br> When the PublishCPPEvent command is
        /// received, the IHD or Meter shall act in one of two ways: 1. It shall notify the
        /// consumer that there is a CPP event that requires acknowledgment. The
        /// acknowledgement shall be either to accept the CPPEvent or reject the CPPEvent (in
        /// which case it shall send the CPPEventResponse command, with the CPPAuth parameter
        /// set to Accepted or Rejected). It is recommended that the CPP event is ignored until a
        /// consumer either accepts or rejects the event. 2. The CPPAuth parameter is set to
        /// “Forced”, in which case the CPPEvent has been accepted.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="durationInMinutes" <see cref="ushort"> Duration In Minutes</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <param name="cppPriceTier" <see cref="byte"> Cpp Price Tier</ param >
        /// <param name="cppAuth" <see cref="byte"> Cpp Auth</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishCppEventCommand(uint providerId, uint issuerEventId, DateTime startTime, ushort durationInMinutes, byte tariffType, byte cppPriceTier, byte cppAuth)
        {
            PublishCppEventCommand command = new PublishCppEventCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.StartTime = startTime;
            command.DurationInMinutes = durationInMinutes;
            command.TariffType = tariffType;
            command.CppPriceTier = cppPriceTier;
            command.CppAuth = cppAuth;

            return Send(command);
        }

        /// <summary>
        /// The Publish Credit Payment Command
        ///
        /// The PublishCreditPayment command is used to update the credit payment
        /// information when available. <br> Nested and overlapping PublishCreditPayment
        /// commands are not allowed. In the case of overlapping credit payments, the payment
        /// with the newer Issuer Event ID takes priority over all nested and overlapping
        /// payments. All existing payments that overlap, even partially, should be removed.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="creditPaymentDueDate" <see cref="DateTime"> Credit Payment Due Date</ param >
        /// <param name="creditPaymentOverdueAmount" <see cref="uint"> Credit Payment Overdue Amount</ param >
        /// <param name="creditPaymentStatus" <see cref="byte"> Credit Payment Status</ param >
        /// <param name="creditPayment" <see cref="uint"> Credit Payment</ param >
        /// <param name="creditPaymentDate" <see cref="DateTime"> Credit Payment Date</ param >
        /// <param name="creditPaymentRef" <see cref="ByteArray"> Credit Payment Ref</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishCreditPaymentCommand(uint providerId, uint issuerEventId, DateTime creditPaymentDueDate, uint creditPaymentOverdueAmount, byte creditPaymentStatus, uint creditPayment, DateTime creditPaymentDate, ByteArray creditPaymentRef)
        {
            PublishCreditPaymentCommand command = new PublishCreditPaymentCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.CreditPaymentDueDate = creditPaymentDueDate;
            command.CreditPaymentOverdueAmount = creditPaymentOverdueAmount;
            command.CreditPaymentStatus = creditPaymentStatus;
            command.CreditPayment = creditPayment;
            command.CreditPaymentDate = creditPaymentDate;
            command.CreditPaymentRef = creditPaymentRef;

            return Send(command);
        }

        /// <summary>
        /// The Publish Currency Conversion Command
        ///
        /// The PublishCurrencyConversion command is sent in response to a
        /// GetCurrencyConversion command or when a new currency becomes available.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="oldCurrency" <see cref="ushort"> Old Currency</ param >
        /// <param name="newCurrency" <see cref="ushort"> New Currency</ param >
        /// <param name="conversionFactor" <see cref="uint"> Conversion Factor</ param >
        /// <param name="conversionFactorTrailingDigit" <see cref="byte"> Conversion Factor Trailing Digit</ param >
        /// <param name="currencyChangeControlFlags" <see cref="uint"> Currency Change Control Flags</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishCurrencyConversionCommand(uint providerId, uint issuerEventId, DateTime startTime, ushort oldCurrency, ushort newCurrency, uint conversionFactor, byte conversionFactorTrailingDigit, uint currencyChangeControlFlags)
        {
            PublishCurrencyConversionCommand command = new PublishCurrencyConversionCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.StartTime = startTime;
            command.OldCurrency = oldCurrency;
            command.NewCurrency = newCurrency;
            command.ConversionFactor = conversionFactor;
            command.ConversionFactorTrailingDigit = conversionFactorTrailingDigit;
            command.CurrencyChangeControlFlags = currencyChangeControlFlags;

            return Send(command);
        }

        /// <summary>
        /// The Cancel Tariff Command
        ///
        /// The CancelTariff command indicates that all data associated with a particular
        /// tariff instance should be discarded. <br> In markets where permanently active
        /// price information is required for billing purposes, it is recommended that
        /// replacement/superseding PublishTariffInformation, PublishPriceMatrix,
        /// PublishBlockThresholds and PublishTierLabels commands are used in place of a
        /// CancelTariff command.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerTariffId" <see cref="uint"> Issuer Tariff ID</ param >
        /// <param name="tariffType" <see cref="byte"> Tariff Type</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CancelTariffCommand(uint providerId, uint issuerTariffId, byte tariffType)
        {
            CancelTariffCommand command = new CancelTariffCommand();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerTariffId = issuerTariffId;
            command.TariffType = tariffType;

            return Send(command);
        }
    }
}
