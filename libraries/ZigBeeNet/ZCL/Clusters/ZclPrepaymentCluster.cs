
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.Prepayment;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Prepayment cluster implementation (Cluster ID 0x0705).
    ///
    /// The Prepayment Cluster provides the facility to pass messages relating to the
    /// accounting functionality of a meter between devices on the HAN. It allows for the
    /// implementation of a system conforming to the set of standards relating to Payment
    /// Electricity Meters (IEC 62055) and also for the case where the accounting function is
    /// remote from the meter. Prepayment is used in situations where the supply of a service may
    /// be interrupted or enabled under the control of the meter or system in relation to a
    /// payment tariff. The accounting process may be within the meter or elsewhere in the
    /// system. The amount of available credit is decremented as the service is consumed and is
    /// incremented through payments made by the consumer. Such a system allows the consumer to
    /// better manage their energy consumption and reduces the risk of bad debt owing to the
    /// supplier.
    /// In the case where the accounting process resides within the meter, credit updates are
    /// sent to the meter from the ESI. Such messages are out of scope of this cluster. The cluster
    /// allows credit status to be made available to other devices on the HAN for example to
    /// enable the consumers to view their status on an IHD. It also allows them to select
    /// emergency credit if running low and also, where local markets allow, restoring their
    /// supply remotely from within the HAN.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclPrepaymentCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0705;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Prepayment";

        // Attribute constants

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PAYMENTCONTROLCONFIGURATION = 0x0000;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CREDITREMAINING = 0x0001;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_EMERGENCYCREDITREMAINING = 0x0002;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CREDITSTATUS = 0x0003;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CREDITREMAININGTIMESTAMP = 0x0004;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_ACCUMULATEDDEBT = 0x0005;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_OVERALLDEBTCAP = 0x0006;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_EMERGENCYCREDITLIMITALLOWANCE = 0x0010;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_EMERGENCYCREDITTHRESHOLD = 0x0011;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOTALCREDITADDED = 0x0020;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_MAXCREDITLIMIT = 0x0021;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_MAXCREDITPERTOPUP = 0x0022;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_FRIENDLYCREDITWARNING = 0x0030;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_LOWCREDITWARNINGLEVEL = 0x0031;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_IHDLOWCREDITWARNINGLEVEL = 0x0032;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_INTERRUPTSUSPENDTIME = 0x0033;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_REMAININGFRIENDLYCREDITTIME = 0x0034;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_NEXTFRIENDLYCREDITPERIOD = 0x0035;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CUTOFFVALUE = 0x0040;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOKENCARRIERID = 0x0080;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPDATETIME1 = 0x0100;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPAMOUNT1 = 0x0101;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPORIGINATINGDEVICE1 = 0x0102;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPCODE1 = 0x0103;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPDATETIME2 = 0x0110;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPAMOUNT2 = 0x0111;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPORIGINATINGDEVICE2 = 0x0112;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPCODE2 = 0x0113;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPDATETIME3 = 0x0120;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPAMOUNT3 = 0x0121;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPORIGINATINGDEVICE3 = 0x0122;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPCODE3 = 0x0123;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPDATETIME4 = 0x0130;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPAMOUNT4 = 0x0131;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPORIGINATINGDEVICE4 = 0x0132;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPCODE4 = 0x0133;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPDATETIME5 = 0x0140;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPAMOUNT5 = 0x0141;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPORIGINATINGDEVICE5 = 0x0142;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_TOPUPCODE5 = 0x0143;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTLABEL1 = 0x0210;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTAMOUNT1 = 0x0211;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYMETHOD1 = 0x0212;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYSTARTTIME1 = 0x0213;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYCOLLECTIONTIME1 = 0x0214;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYFREQUENCY1 = 0x0216;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYAMOUNT1 = 0x0217;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYTOPUPPERCENTAGE1 = 0x0219;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTLABEL2 = 0x0220;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTAMOUNT2 = 0x0221;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYMETHOD2 = 0x0222;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYSTARTTIME2 = 0x0223;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYCOLLECTIONTIME2 = 0x0224;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYFREQUENCY2 = 0x0226;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYAMOUNT2 = 0x0227;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYTOPUPPERCENTAGE2 = 0x0229;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTLABEL3 = 0x0230;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTAMOUNT3 = 0x0231;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYMETHOD3 = 0x0232;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYSTARTTIME3 = 0x0233;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYCOLLECTIONTIME3 = 0x0234;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYFREQUENCY3 = 0x0236;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYAMOUNT3 = 0x0237;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_DEBTRECOVERYTOPUPPERCENTAGE3 = 0x0239;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREPAYMENTALARMSTATUS = 0x0400;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREPAYGENERICALARMMASK = 0x0401;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREPAYSWITCHALARMMASK = 0x0402;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREPAYEVENTALARMMASK = 0x0403;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_HISTORICALCOSTCONSUMPTIONFORMATTING = 0x0500;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CONSUMPTIONUNITOFMEASUREMENT = 0x0501;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CURRENCYSCALINGFACTOR = 0x0502;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CURRENCY = 0x0503;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CURRENTDAYCOSTCONSUMPTIONDELIVERED = 0x051C;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CURRENTDAYCOSTCONSUMPTIONRECEIVED = 0x051D;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYCOSTCONSUMPTIONDELIVERED = 0x051E;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAYCOSTCONSUMPTIONRECEIVED = 0x051F;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY2COSTCONSUMPTIONDELIVERED = 0x0520;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY2COSTCONSUMPTIONRECEIVED = 0x0521;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY3COSTCONSUMPTIONDELIVERED = 0x0522;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY3COSTCONSUMPTIONRECEIVED = 0x0523;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY4COSTCONSUMPTIONDELIVERED = 0x0524;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY4COSTCONSUMPTIONRECEIVED = 0x0525;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY5COSTCONSUMPTIONDELIVERED = 0x0526;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY5COSTCONSUMPTIONRECEIVED = 0x0527;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY6COSTCONSUMPTIONDELIVERED = 0x0528;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY6COSTCONSUMPTIONRECEIVED = 0x0529;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY7COSTCONSUMPTIONDELIVERED = 0x052A;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY7COSTCONSUMPTIONRECEIVED = 0x052B;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY8COSTCONSUMPTIONDELIVERED = 0x052C;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSDAY8COSTCONSUMPTIONRECEIVED = 0x052D;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CURRENTWEEKCOSTCONSUMPTIONDELIVERED = 0x0530;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CURRENTWEEKCOSTCONSUMPTIONRECEIVED = 0x0531;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSWEEKCOSTCONSUMPTIONDELIVERED = 0x0532;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSWEEKCOSTCONSUMPTIONRECEIVED = 0x0533;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSWEEK2COSTCONSUMPTIONDELIVERED = 0x0534;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSWEEK2COSTCONSUMPTIONRECEIVED = 0x0535;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSWEEK3COSTCONSUMPTIONDELIVERED = 0x0536;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSWEEK3COSTCONSUMPTIONRECEIVED = 0x0537;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSWEEK4COSTCONSUMPTIONDELIVERED = 0x0538;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSWEEK4COSTCONSUMPTIONRECEIVED = 0x0539;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSWEEK5COSTCONSUMPTIONDELIVERED = 0x053A;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSWEEK5COSTCONSUMPTIONRECEIVED = 0x053B;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CURRENTMONTHCOSTCONSUMPTIONDELIVERED = 0x0540;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_CURRENTMONTHCOSTCONSUMPTIONRECEIVED = 0x0541;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTHCOSTCONSUMPTIONDELIVERED = 0x0542;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTHCOSTCONSUMPTIONRECEIVED = 0x0543;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH2COSTCONSUMPTIONDELIVERED = 0x0544;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH2COSTCONSUMPTIONRECEIVED = 0x0545;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH3COSTCONSUMPTIONDELIVERED = 0x0546;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH3COSTCONSUMPTIONRECEIVED = 0x0547;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH4COSTCONSUMPTIONDELIVERED = 0x0548;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH4COSTCONSUMPTIONRECEIVED = 0x0549;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH5COSTCONSUMPTIONDELIVERED = 0x054A;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH5COSTCONSUMPTIONRECEIVED = 0x054B;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH6COSTCONSUMPTIONDELIVERED = 0x054C;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH6COSTCONSUMPTIONRECEIVED = 0x054D;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH7COSTCONSUMPTIONDELIVERED = 0x054E;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH7COSTCONSUMPTIONRECEIVED = 0x054F;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH8COSTCONSUMPTIONDELIVERED = 0x0550;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH8COSTCONSUMPTIONRECEIVED = 0x0551;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH9COSTCONSUMPTIONDELIVERED = 0x0552;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH9COSTCONSUMPTIONRECEIVED = 0x0553;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH10COSTCONSUMPTIONDELIVERED = 0x0554;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH10COSTCONSUMPTIONRECEIVED = 0x0555;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH11COSTCONSUMPTIONDELIVERED = 0x0556;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH11COSTCONSUMPTIONRECEIVED = 0x0557;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH12COSTCONSUMPTIONDELIVERED = 0x0558;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH12COSTCONSUMPTIONRECEIVED = 0x0559;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH13COSTCONSUMPTIONDELIVERED = 0x055A;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_PREVIOUSMONTH13COSTCONSUMPTIONRECEIVED = 0x055B;

        /// <summary>
        /// ADDME
        /// </summary>
        public const ushort ATTR_HISTORICALFREEZETIME = 0x055C;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(131);

            attributeMap.Add(ATTR_PAYMENTCONTROLCONFIGURATION, new ZclAttribute(this, ATTR_PAYMENTCONTROLCONFIGURATION, "Payment Control Configuration", ZclDataType.Get(DataType.BITMAP_16_BIT), true, true, false, false));
            attributeMap.Add(ATTR_CREDITREMAINING, new ZclAttribute(this, ATTR_CREDITREMAINING, "Credit Remaining", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_EMERGENCYCREDITREMAINING, new ZclAttribute(this, ATTR_EMERGENCYCREDITREMAINING, "Emergency Credit Remaining", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CREDITSTATUS, new ZclAttribute(this, ATTR_CREDITSTATUS, "Credit Status", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_CREDITREMAININGTIMESTAMP, new ZclAttribute(this, ATTR_CREDITREMAININGTIMESTAMP, "Credit Remaining Timestamp", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_ACCUMULATEDDEBT, new ZclAttribute(this, ATTR_ACCUMULATEDDEBT, "Accumulated Debt", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_OVERALLDEBTCAP, new ZclAttribute(this, ATTR_OVERALLDEBTCAP, "Overall Debt Cap", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_EMERGENCYCREDITLIMITALLOWANCE, new ZclAttribute(this, ATTR_EMERGENCYCREDITLIMITALLOWANCE, "Emergency Credit Limit Allowance", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_EMERGENCYCREDITTHRESHOLD, new ZclAttribute(this, ATTR_EMERGENCYCREDITTHRESHOLD, "Emergency Credit Threshold", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOTALCREDITADDED, new ZclAttribute(this, ATTR_TOTALCREDITADDED, "Total Credit Added", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MAXCREDITLIMIT, new ZclAttribute(this, ATTR_MAXCREDITLIMIT, "Max Credit Limit", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MAXCREDITPERTOPUP, new ZclAttribute(this, ATTR_MAXCREDITPERTOPUP, "Max Credit Per Top Up", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_FRIENDLYCREDITWARNING, new ZclAttribute(this, ATTR_FRIENDLYCREDITWARNING, "Friendly Credit Warning", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_LOWCREDITWARNINGLEVEL, new ZclAttribute(this, ATTR_LOWCREDITWARNINGLEVEL, "Low Credit Warning Level", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_IHDLOWCREDITWARNINGLEVEL, new ZclAttribute(this, ATTR_IHDLOWCREDITWARNINGLEVEL, "Ihd Low Credit Warning Level", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), false, true, true, true));
            attributeMap.Add(ATTR_INTERRUPTSUSPENDTIME, new ZclAttribute(this, ATTR_INTERRUPTSUSPENDTIME, "Interrupt Suspend Time", ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_REMAININGFRIENDLYCREDITTIME, new ZclAttribute(this, ATTR_REMAININGFRIENDLYCREDITTIME, "Remaining Friendly Credit Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_NEXTFRIENDLYCREDITPERIOD, new ZclAttribute(this, ATTR_NEXTFRIENDLYCREDITPERIOD, "Next Friendly Credit Period", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_CUTOFFVALUE, new ZclAttribute(this, ATTR_CUTOFFVALUE, "Cut Off Value", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOKENCARRIERID, new ZclAttribute(this, ATTR_TOKENCARRIERID, "Token Carrier ID", ZclDataType.Get(DataType.OCTET_STRING), false, true, true, true));
            attributeMap.Add(ATTR_TOPUPDATETIME1, new ZclAttribute(this, ATTR_TOPUPDATETIME1, "Top Up Date / time #1", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPAMOUNT1, new ZclAttribute(this, ATTR_TOPUPAMOUNT1, "Top Up Amount #1", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPORIGINATINGDEVICE1, new ZclAttribute(this, ATTR_TOPUPORIGINATINGDEVICE1, "Top Up Originating Device #1", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPCODE1, new ZclAttribute(this, ATTR_TOPUPCODE1, "Top Up Code #1", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPDATETIME2, new ZclAttribute(this, ATTR_TOPUPDATETIME2, "Top Up Date /time #2", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPAMOUNT2, new ZclAttribute(this, ATTR_TOPUPAMOUNT2, "Top Up Amount #2", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPORIGINATINGDEVICE2, new ZclAttribute(this, ATTR_TOPUPORIGINATINGDEVICE2, "Top Up Originating Device #2", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPCODE2, new ZclAttribute(this, ATTR_TOPUPCODE2, "Top Up Code #2", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPDATETIME3, new ZclAttribute(this, ATTR_TOPUPDATETIME3, "Top Up Date /time #3", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPAMOUNT3, new ZclAttribute(this, ATTR_TOPUPAMOUNT3, "Top Up Amount #3", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPORIGINATINGDEVICE3, new ZclAttribute(this, ATTR_TOPUPORIGINATINGDEVICE3, "Top Up Originating Device #3", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPCODE3, new ZclAttribute(this, ATTR_TOPUPCODE3, "Top Up Code #3", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPDATETIME4, new ZclAttribute(this, ATTR_TOPUPDATETIME4, "Top Up Date /time #4", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPAMOUNT4, new ZclAttribute(this, ATTR_TOPUPAMOUNT4, "Top Up Amount #4", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPORIGINATINGDEVICE4, new ZclAttribute(this, ATTR_TOPUPORIGINATINGDEVICE4, "Top Up Originating Device #4", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPCODE4, new ZclAttribute(this, ATTR_TOPUPCODE4, "Top Up Code #4", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPDATETIME5, new ZclAttribute(this, ATTR_TOPUPDATETIME5, "Top Up Date /time #5", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPAMOUNT5, new ZclAttribute(this, ATTR_TOPUPAMOUNT5, "Top Up Amount #5", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPORIGINATINGDEVICE5, new ZclAttribute(this, ATTR_TOPUPORIGINATINGDEVICE5, "Top Up Originating Device #5", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_TOPUPCODE5, new ZclAttribute(this, ATTR_TOPUPCODE5, "Top Up Code #5", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_DEBTLABEL1, new ZclAttribute(this, ATTR_DEBTLABEL1, "Debt Label 1", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_DEBTAMOUNT1, new ZclAttribute(this, ATTR_DEBTAMOUNT1, "Debt Amount 1", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYMETHOD1, new ZclAttribute(this, ATTR_DEBTRECOVERYMETHOD1, "Debt Recovery Method 1", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYSTARTTIME1, new ZclAttribute(this, ATTR_DEBTRECOVERYSTARTTIME1, "Debt Recovery Start Time 1", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYCOLLECTIONTIME1, new ZclAttribute(this, ATTR_DEBTRECOVERYCOLLECTIONTIME1, "Debt Recovery Collection Time 1", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYFREQUENCY1, new ZclAttribute(this, ATTR_DEBTRECOVERYFREQUENCY1, "Debt Recovery Frequency 1", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYAMOUNT1, new ZclAttribute(this, ATTR_DEBTRECOVERYAMOUNT1, "Debt Recovery Amount 1", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYTOPUPPERCENTAGE1, new ZclAttribute(this, ATTR_DEBTRECOVERYTOPUPPERCENTAGE1, "Debt Recovery Top Up Percentage 1", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTLABEL2, new ZclAttribute(this, ATTR_DEBTLABEL2, "Debt Label 2", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_DEBTAMOUNT2, new ZclAttribute(this, ATTR_DEBTAMOUNT2, "Debt Amount 2", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYMETHOD2, new ZclAttribute(this, ATTR_DEBTRECOVERYMETHOD2, "Debt Recovery Method 2", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYSTARTTIME2, new ZclAttribute(this, ATTR_DEBTRECOVERYSTARTTIME2, "Debt Recovery Start Time 2", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYCOLLECTIONTIME2, new ZclAttribute(this, ATTR_DEBTRECOVERYCOLLECTIONTIME2, "Debt Recovery Collection Time 2", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYFREQUENCY2, new ZclAttribute(this, ATTR_DEBTRECOVERYFREQUENCY2, "Debt Recovery Frequency 2", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYAMOUNT2, new ZclAttribute(this, ATTR_DEBTRECOVERYAMOUNT2, "Debt Recovery Amount 2", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYTOPUPPERCENTAGE2, new ZclAttribute(this, ATTR_DEBTRECOVERYTOPUPPERCENTAGE2, "Debt Recovery Top Up Percentage 2", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTLABEL3, new ZclAttribute(this, ATTR_DEBTLABEL3, "Debt Label 3", ZclDataType.Get(DataType.OCTET_STRING), true, true, false, false));
            attributeMap.Add(ATTR_DEBTAMOUNT3, new ZclAttribute(this, ATTR_DEBTAMOUNT3, "Debt Amount 3", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYMETHOD3, new ZclAttribute(this, ATTR_DEBTRECOVERYMETHOD3, "Debt Recovery Method 3", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYSTARTTIME3, new ZclAttribute(this, ATTR_DEBTRECOVERYSTARTTIME3, "Debt Recovery Start Time 3", ZclDataType.Get(DataType.UTCTIME), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYCOLLECTIONTIME3, new ZclAttribute(this, ATTR_DEBTRECOVERYCOLLECTIONTIME3, "Debt Recovery Collection Time 3", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYFREQUENCY3, new ZclAttribute(this, ATTR_DEBTRECOVERYFREQUENCY3, "Debt Recovery Frequency 3", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYAMOUNT3, new ZclAttribute(this, ATTR_DEBTRECOVERYAMOUNT3, "Debt Recovery Amount 3", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DEBTRECOVERYTOPUPPERCENTAGE3, new ZclAttribute(this, ATTR_DEBTRECOVERYTOPUPPERCENTAGE3, "Debt Recovery Top Up Percentage 3", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREPAYMENTALARMSTATUS, new ZclAttribute(this, ATTR_PREPAYMENTALARMSTATUS, "Prepayment Alarm Status", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_PREPAYGENERICALARMMASK, new ZclAttribute(this, ATTR_PREPAYGENERICALARMMASK, "Prepay Generic Alarm Mask", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_PREPAYSWITCHALARMMASK, new ZclAttribute(this, ATTR_PREPAYSWITCHALARMMASK, "Prepay Switch Alarm Mask", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_PREPAYEVENTALARMMASK, new ZclAttribute(this, ATTR_PREPAYEVENTALARMMASK, "Prepay Event Alarm Mask", ZclDataType.Get(DataType.BITMAP_16_BIT), false, true, true, true));
            attributeMap.Add(ATTR_HISTORICALCOSTCONSUMPTIONFORMATTING, new ZclAttribute(this, ATTR_HISTORICALCOSTCONSUMPTIONFORMATTING, "Historical Cost Consumption Formatting", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_CONSUMPTIONUNITOFMEASUREMENT, new ZclAttribute(this, ATTR_CONSUMPTIONUNITOFMEASUREMENT, "Consumption Unit Of Measurement", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_CURRENCYSCALINGFACTOR, new ZclAttribute(this, ATTR_CURRENCYSCALINGFACTOR, "Currency Scaling Factor", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_CURRENCY, new ZclAttribute(this, ATTR_CURRENCY, "Currency", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYCOSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTDAYCOSTCONSUMPTIONDELIVERED, "Current Day Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTDAYCOSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTDAYCOSTCONSUMPTIONRECEIVED, "Current Day Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYCOSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAYCOSTCONSUMPTIONDELIVERED, "Previous Day Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAYCOSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAYCOSTCONSUMPTIONRECEIVED, "Previous Day Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY2COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY2COSTCONSUMPTIONDELIVERED, "Previous Day 2 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY2COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY2COSTCONSUMPTIONRECEIVED, "Previous Day 2 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY3COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY3COSTCONSUMPTIONDELIVERED, "Previous Day 3 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY3COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY3COSTCONSUMPTIONRECEIVED, "Previous Day 3 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY4COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY4COSTCONSUMPTIONDELIVERED, "Previous Day 4 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY4COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY4COSTCONSUMPTIONRECEIVED, "Previous Day 4 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY5COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY5COSTCONSUMPTIONDELIVERED, "Previous Day 5 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY5COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY5COSTCONSUMPTIONRECEIVED, "Previous Day 5 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY6COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY6COSTCONSUMPTIONDELIVERED, "Previous Day 6 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY6COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY6COSTCONSUMPTIONRECEIVED, "Previous Day 6 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY7COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY7COSTCONSUMPTIONDELIVERED, "Previous Day 7 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY7COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY7COSTCONSUMPTIONRECEIVED, "Previous Day 7 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY8COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSDAY8COSTCONSUMPTIONDELIVERED, "Previous Day 8 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSDAY8COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSDAY8COSTCONSUMPTIONRECEIVED, "Previous Day 8 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTWEEKCOSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTWEEKCOSTCONSUMPTIONDELIVERED, "Current Week Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTWEEKCOSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTWEEKCOSTCONSUMPTIONRECEIVED, "Current Week Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEKCOSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEKCOSTCONSUMPTIONDELIVERED, "Previous Week Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEKCOSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEKCOSTCONSUMPTIONRECEIVED, "Previous Week Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK2COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK2COSTCONSUMPTIONDELIVERED, "Previous Week 2 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK2COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK2COSTCONSUMPTIONRECEIVED, "Previous Week 2 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK3COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK3COSTCONSUMPTIONDELIVERED, "Previous Week 3 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK3COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK3COSTCONSUMPTIONRECEIVED, "Previous Week 3 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK4COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK4COSTCONSUMPTIONDELIVERED, "Previous Week 4 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK4COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK4COSTCONSUMPTIONRECEIVED, "Previous Week 4 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK5COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSWEEK5COSTCONSUMPTIONDELIVERED, "Previous Week 5 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSWEEK5COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSWEEK5COSTCONSUMPTIONRECEIVED, "Previous Week 5 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMONTHCOSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_CURRENTMONTHCOSTCONSUMPTIONDELIVERED, "Current Month Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTMONTHCOSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_CURRENTMONTHCOSTCONSUMPTIONRECEIVED, "Current Month Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTHCOSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTHCOSTCONSUMPTIONDELIVERED, "Previous Month Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTHCOSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTHCOSTCONSUMPTIONRECEIVED, "Previous Month Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH2COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH2COSTCONSUMPTIONDELIVERED, "Previous Month 2 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH2COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH2COSTCONSUMPTIONRECEIVED, "Previous Month 2 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH3COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH3COSTCONSUMPTIONDELIVERED, "Previous Month 3 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH3COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH3COSTCONSUMPTIONRECEIVED, "Previous Month 3 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH4COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH4COSTCONSUMPTIONDELIVERED, "Previous Month 4 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH4COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH4COSTCONSUMPTIONRECEIVED, "Previous Month 4 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH5COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH5COSTCONSUMPTIONDELIVERED, "Previous Month 5 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH5COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH5COSTCONSUMPTIONRECEIVED, "Previous Month 5 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH6COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH6COSTCONSUMPTIONDELIVERED, "Previous Month 6 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH6COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH6COSTCONSUMPTIONRECEIVED, "Previous Month 6 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH7COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH7COSTCONSUMPTIONDELIVERED, "Previous Month 7 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH7COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH7COSTCONSUMPTIONRECEIVED, "Previous Month 7 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH8COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH8COSTCONSUMPTIONDELIVERED, "Previous Month 8 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH8COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH8COSTCONSUMPTIONRECEIVED, "Previous Month 8 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH9COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH9COSTCONSUMPTIONDELIVERED, "Previous Month 9 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH9COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH9COSTCONSUMPTIONRECEIVED, "Previous Month 9 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH10COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH10COSTCONSUMPTIONDELIVERED, "Previous Month 10 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH10COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH10COSTCONSUMPTIONRECEIVED, "Previous Month 10 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH11COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH11COSTCONSUMPTIONDELIVERED, "Previous Month 11 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH11COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH11COSTCONSUMPTIONRECEIVED, "Previous Month 11 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH12COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH12COSTCONSUMPTIONDELIVERED, "Previous Month 12 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH12COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH12COSTCONSUMPTIONRECEIVED, "Previous Month 12 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH13COSTCONSUMPTIONDELIVERED, new ZclAttribute(this, ATTR_PREVIOUSMONTH13COSTCONSUMPTIONDELIVERED, "Previous Month 13 Cost Consumption Delivered", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_PREVIOUSMONTH13COSTCONSUMPTIONRECEIVED, new ZclAttribute(this, ATTR_PREVIOUSMONTH13COSTCONSUMPTIONRECEIVED, "Previous Month 13 Cost Consumption Received", ZclDataType.Get(DataType.UNSIGNED_48_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_HISTORICALFREEZETIME, new ZclAttribute(this, ATTR_HISTORICALFREEZETIME, "Historical Freeze Time", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(5);

            commandMap.Add(0x0001, () => new PublishPrepaySnapshot());
            commandMap.Add(0x0002, () => new ChangePaymentModeResponse());
            commandMap.Add(0x0003, () => new ConsumerTopUpResponse());
            commandMap.Add(0x0005, () => new PublishTopUpLog());
            commandMap.Add(0x0006, () => new PublishDebtLog());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(12);

            commandMap.Add(0x0000, () => new SelectAvailableEmergencyCredit());
            commandMap.Add(0x0002, () => new ChangeDebt());
            commandMap.Add(0x0003, () => new EmergencyCreditSetup());
            commandMap.Add(0x0004, () => new ConsumerTopUp());
            commandMap.Add(0x0005, () => new CreditAdjustment());
            commandMap.Add(0x0006, () => new ChangePaymentMode());
            commandMap.Add(0x0007, () => new GetPrepaySnapshot());
            commandMap.Add(0x0008, () => new GetTopUpLog());
            commandMap.Add(0x0009, () => new SetLowCreditWarningLevel());
            commandMap.Add(0x000A, () => new GetDebtRepaymentLog());
            commandMap.Add(0x000B, () => new SetMaximumCreditLimit());
            commandMap.Add(0x000C, () => new SetOverallDebtCap());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Prepayment cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclPrepaymentCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Select Available Emergency Credit
        ///
        /// FIXME: This command is sent to the Metering Device to activate the use of any
        /// Emergency Credit available on the Metering Device.
        ///
        /// <param name="commandIssueDateTime" <see cref="DateTime"> Command Issue Date Time</ param >
        /// <param name="originatingDevice" <see cref="byte"> Originating Device</ param >
        /// <param name="siteId" <see cref="ByteArray"> Site ID</ param >
        /// <param name="meterSerialNumber" <see cref="ByteArray"> Meter Serial Number</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SelectAvailableEmergencyCredit(DateTime commandIssueDateTime, byte originatingDevice, ByteArray siteId, ByteArray meterSerialNumber)
        {
            SelectAvailableEmergencyCredit command = new SelectAvailableEmergencyCredit();

            // Set the fields
            command.CommandIssueDateTime = commandIssueDateTime;
            command.OriginatingDevice = originatingDevice;
            command.SiteId = siteId;
            command.MeterSerialNumber = meterSerialNumber;

            return Send(command);
        }

        /// <summary>
        /// The Change Debt
        ///
        /// FIXME: The ChangeDebt command is send to the Metering Device to change the fuel or
        /// Non fuel debt values.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="debtLabel" <see cref="ByteArray"> Debt Label</ param >
        /// <param name="debtAmount" <see cref="uint"> Debt Amount</ param >
        /// <param name="debtRecoveryMethod" <see cref="byte"> Debt Recovery Method</ param >
        /// <param name="debtAmountType" <see cref="byte"> Debt Amount Type</ param >
        /// <param name="debtRecoveryStartTime" <see cref="DateTime"> Debt Recovery Start Time</ param >
        /// <param name="debtRecoveryCollectionTime" <see cref="ushort"> Debt Recovery Collection Time</ param >
        /// <param name="debtRecoveryFrequency" <see cref="byte"> Debt Recovery Frequency</ param >
        /// <param name="debtRecoveryAmount" <see cref="uint"> Debt Recovery Amount</ param >
        /// <param name="debtRecoveryBalancePercentage" <see cref="ushort"> Debt Recovery Balance Percentage</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ChangeDebt(uint issuerEventId, ByteArray debtLabel, uint debtAmount, byte debtRecoveryMethod, byte debtAmountType, DateTime debtRecoveryStartTime, ushort debtRecoveryCollectionTime, byte debtRecoveryFrequency, uint debtRecoveryAmount, ushort debtRecoveryBalancePercentage)
        {
            ChangeDebt command = new ChangeDebt();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.DebtLabel = debtLabel;
            command.DebtAmount = debtAmount;
            command.DebtRecoveryMethod = debtRecoveryMethod;
            command.DebtAmountType = debtAmountType;
            command.DebtRecoveryStartTime = debtRecoveryStartTime;
            command.DebtRecoveryCollectionTime = debtRecoveryCollectionTime;
            command.DebtRecoveryFrequency = debtRecoveryFrequency;
            command.DebtRecoveryAmount = debtRecoveryAmount;
            command.DebtRecoveryBalancePercentage = debtRecoveryBalancePercentage;

            return Send(command);
        }

        /// <summary>
        /// The Emergency Credit Setup
        ///
        /// FIXME: This command is a method to set up the parameters for the emergency credit.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="emergencyCreditLimit" <see cref="uint"> Emergency Credit Limit</ param >
        /// <param name="emergencyCreditThreshold" <see cref="uint"> Emergency Credit Threshold</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> EmergencyCreditSetup(uint issuerEventId, DateTime startTime, uint emergencyCreditLimit, uint emergencyCreditThreshold)
        {
            EmergencyCreditSetup command = new EmergencyCreditSetup();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.StartTime = startTime;
            command.EmergencyCreditLimit = emergencyCreditLimit;
            command.EmergencyCreditThreshold = emergencyCreditThreshold;

            return Send(command);
        }

        /// <summary>
        /// The Consumer Top Up
        ///
        /// FIXME: The ConsumerTopUp command is used by the IPD and the ESI as a method of
        /// applying credit top up values to the prepayment meter.
        ///
        /// <param name="originatingDevice" <see cref="byte"> Originating Device</ param >
        /// <param name="topUpCode" <see cref="ByteArray"> Top Up Code</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ConsumerTopUp(byte originatingDevice, ByteArray topUpCode)
        {
            ConsumerTopUp command = new ConsumerTopUp();

            // Set the fields
            command.OriginatingDevice = originatingDevice;
            command.TopUpCode = topUpCode;

            return Send(command);
        }

        /// <summary>
        /// The Credit Adjustment
        ///
        /// FIXME: The CreditAdjustment command is sent to update the accounting base for the
        /// Prepayment meter.
        ///
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="startTime" <see cref="DateTime"> Start Time</ param >
        /// <param name="creditAdjustmentType" <see cref="byte"> Credit Adjustment Type</ param >
        /// <param name="creditAdjustmentValue" <see cref="uint"> Credit Adjustment Value</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> CreditAdjustment(uint issuerEventId, DateTime startTime, byte creditAdjustmentType, uint creditAdjustmentValue)
        {
            CreditAdjustment command = new CreditAdjustment();

            // Set the fields
            command.IssuerEventId = issuerEventId;
            command.StartTime = startTime;
            command.CreditAdjustmentType = creditAdjustmentType;
            command.CreditAdjustmentValue = creditAdjustmentValue;

            return Send(command);
        }

        /// <summary>
        /// The Change Payment Mode
        ///
        /// FIXME: This command is sent to a Metering Device to instruct it to change its mode of
        /// operation. i.e. from Credit to Prepayment.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="implementationDateTime" <see cref="DateTime"> Implementation Date Time</ param >
        /// <param name="proposedPaymentControlConfiguration" <see cref="ushort"> Proposed Payment Control Configuration</ param >
        /// <param name="cutOffValue" <see cref="uint"> Cut Off Value</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ChangePaymentMode(uint providerId, uint issuerEventId, DateTime implementationDateTime, ushort proposedPaymentControlConfiguration, uint cutOffValue)
        {
            ChangePaymentMode command = new ChangePaymentMode();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.ImplementationDateTime = implementationDateTime;
            command.ProposedPaymentControlConfiguration = proposedPaymentControlConfiguration;
            command.CutOffValue = cutOffValue;

            return Send(command);
        }

        /// <summary>
        /// The Get Prepay Snapshot
        ///
        /// FIXME: This command is used to request the cluster server for snapshot data.
        ///
        /// <param name="earliestStartTime" <see cref="DateTime"> Earliest Start Time</ param >
        /// <param name="latestEndTime" <see cref="DateTime"> Latest End Time</ param >
        /// <param name="snapshotOffset" <see cref="byte"> Snapshot Offset</ param >
        /// <param name="snapshotCause" <see cref="int"> Snapshot Cause</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetPrepaySnapshot(DateTime earliestStartTime, DateTime latestEndTime, byte snapshotOffset, int snapshotCause)
        {
            GetPrepaySnapshot command = new GetPrepaySnapshot();

            // Set the fields
            command.EarliestStartTime = earliestStartTime;
            command.LatestEndTime = latestEndTime;
            command.SnapshotOffset = snapshotOffset;
            command.SnapshotCause = snapshotCause;

            return Send(command);
        }

        /// <summary>
        /// The Get Top Up Log
        ///
        /// FIXME: This command is sent to the Metering Device to retrieve the log of Top Up codes
        /// received by the meter.
        ///
        /// <param name="latestEndTime" <see cref="DateTime"> Latest End Time</ param >
        /// <param name="numberOfRecords" <see cref="byte"> Number Of Records</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetTopUpLog(DateTime latestEndTime, byte numberOfRecords)
        {
            GetTopUpLog command = new GetTopUpLog();

            // Set the fields
            command.LatestEndTime = latestEndTime;
            command.NumberOfRecords = numberOfRecords;

            return Send(command);
        }

        /// <summary>
        /// The Set Low Credit Warning Level
        ///
        /// FIXME: This command is sent from client to a Prepayment server to set the warning
        /// level for low credit.
        ///
        /// <param name="lowCreditWarningLevel" <see cref="uint"> Low Credit Warning Level</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SetLowCreditWarningLevel(uint lowCreditWarningLevel)
        {
            SetLowCreditWarningLevel command = new SetLowCreditWarningLevel();

            // Set the fields
            command.LowCreditWarningLevel = lowCreditWarningLevel;

            return Send(command);
        }

        /// <summary>
        /// The Get Debt Repayment Log
        ///
        /// FIXME: This command is used to request the contents of the repayment log.
        ///
        /// <param name="latestEndTime" <see cref="DateTime"> Latest End Time</ param >
        /// <param name="numberOfDebts" <see cref="byte"> Number Of Debts</ param >
        /// <param name="debtType" <see cref="byte"> Debt Type</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> GetDebtRepaymentLog(DateTime latestEndTime, byte numberOfDebts, byte debtType)
        {
            GetDebtRepaymentLog command = new GetDebtRepaymentLog();

            // Set the fields
            command.LatestEndTime = latestEndTime;
            command.NumberOfDebts = numberOfDebts;
            command.DebtType = debtType;

            return Send(command);
        }

        /// <summary>
        /// The Set Maximum Credit Limit
        ///
        /// FIXME: This command is sent from a client to the Prepayment server to set the maximum
        /// credit level allowed in the meter.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="implementationDateTime" <see cref="DateTime"> Implementation Date Time</ param >
        /// <param name="maximumCreditLevel" <see cref="uint"> Maximum Credit Level</ param >
        /// <param name="maximumCreditPerTopUp" <see cref="uint"> Maximum Credit Per Top Up</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SetMaximumCreditLimit(uint providerId, uint issuerEventId, DateTime implementationDateTime, uint maximumCreditLevel, uint maximumCreditPerTopUp)
        {
            SetMaximumCreditLimit command = new SetMaximumCreditLimit();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.ImplementationDateTime = implementationDateTime;
            command.MaximumCreditLevel = maximumCreditLevel;
            command.MaximumCreditPerTopUp = maximumCreditPerTopUp;

            return Send(command);
        }

        /// <summary>
        /// The Set Overall Debt Cap
        ///
        /// FIXME: This command is sent from a client to the Prepayment server to set the overall
        /// debt cap allowed in the meter.
        ///
        /// <param name="providerId" <see cref="uint"> Provider ID</ param >
        /// <param name="issuerEventId" <see cref="uint"> Issuer Event ID</ param >
        /// <param name="implementationDateTime" <see cref="DateTime"> Implementation Date Time</ param >
        /// <param name="overallDebtCap" <see cref="uint"> Overall Debt Cap</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> SetOverallDebtCap(uint providerId, uint issuerEventId, DateTime implementationDateTime, uint overallDebtCap)
        {
            SetOverallDebtCap command = new SetOverallDebtCap();

            // Set the fields
            command.ProviderId = providerId;
            command.IssuerEventId = issuerEventId;
            command.ImplementationDateTime = implementationDateTime;
            command.OverallDebtCap = overallDebtCap;

            return Send(command);
        }

        /// <summary>
        /// The Publish Prepay Snapshot
        ///
        /// FIXME: This command is generated in response to a GetPrepaySnapshot command. It is
        /// used to return a single snapshot to the client.
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
        public Task<CommandResult> PublishPrepaySnapshot(uint snapshotId, DateTime snapshotTime, byte totalSnapshotsFound, byte commandIndex, byte totalNumberOfCommands, int snapshotCause, byte snapshotPayloadType, byte snapshotPayload)
        {
            PublishPrepaySnapshot command = new PublishPrepaySnapshot();

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
        /// The Change Payment Mode Response
        ///
        /// FIXME: This command is send in response to the ChangePaymentMode Command.
        ///
        /// <param name="friendlyCredit" <see cref="byte"> Friendly Credit</ param >
        /// <param name="friendlyCreditCalendarId" <see cref="uint"> Friendly Credit Calendar ID</ param >
        /// <param name="emergencyCreditLimit" <see cref="uint"> Emergency Credit Limit</ param >
        /// <param name="emergencyCreditThreshold" <see cref="uint"> Emergency Credit Threshold</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ChangePaymentModeResponse(byte friendlyCredit, uint friendlyCreditCalendarId, uint emergencyCreditLimit, uint emergencyCreditThreshold)
        {
            ChangePaymentModeResponse command = new ChangePaymentModeResponse();

            // Set the fields
            command.FriendlyCredit = friendlyCredit;
            command.FriendlyCreditCalendarId = friendlyCreditCalendarId;
            command.EmergencyCreditLimit = emergencyCreditLimit;
            command.EmergencyCreditThreshold = emergencyCreditThreshold;

            return Send(command);
        }

        /// <summary>
        /// The Consumer Top Up Response
        ///
        /// FIXME: This command is send in response to the ConsumerTopUp Command.
        ///
        /// <param name="resultType" <see cref="byte"> Result Type</ param >
        /// <param name="topUpValue" <see cref="uint"> Top Up Value</ param >
        /// <param name="sourceOfTopUp" <see cref="byte"> Source Of Top Up</ param >
        /// <param name="creditRemaining" <see cref="uint"> Credit Remaining</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ConsumerTopUpResponse(byte resultType, uint topUpValue, byte sourceOfTopUp, uint creditRemaining)
        {
            ConsumerTopUpResponse command = new ConsumerTopUpResponse();

            // Set the fields
            command.ResultType = resultType;
            command.TopUpValue = topUpValue;
            command.SourceOfTopUp = sourceOfTopUp;
            command.CreditRemaining = creditRemaining;

            return Send(command);
        }

        /// <summary>
        /// The Publish Top Up Log
        ///
        /// FIXME: This command is used to send the Top Up Code Log entries to the client.
        ///
        /// <param name="commandIndex" <see cref="byte"> Command Index</ param >
        /// <param name="totalNumberOfCommands" <see cref="byte"> Total Number Of Commands</ param >
        /// <param name="topUpPayload" <see cref="TopUpPayload"> Top Up Payload</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishTopUpLog(byte commandIndex, byte totalNumberOfCommands, TopUpPayload topUpPayload)
        {
            PublishTopUpLog command = new PublishTopUpLog();

            // Set the fields
            command.CommandIndex = commandIndex;
            command.TotalNumberOfCommands = totalNumberOfCommands;
            command.TopUpPayload = topUpPayload;

            return Send(command);
        }

        /// <summary>
        /// The Publish Debt Log
        ///
        /// FIXME: This command is used to send the contents of the Repayment Log.
        ///
        /// <param name="commandIndex" <see cref="byte"> Command Index</ param >
        /// <param name="totalNumberOfCommands" <see cref="byte"> Total Number Of Commands</ param >
        /// <param name="debtPayload" <see cref="DebtPayload"> Debt Payload</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> PublishDebtLog(byte commandIndex, byte totalNumberOfCommands, DebtPayload debtPayload)
        {
            PublishDebtLog command = new PublishDebtLog();

            // Set the fields
            command.CommandIndex = commandIndex;
            command.TotalNumberOfCommands = totalNumberOfCommands;
            command.DebtPayload = debtPayload;

            return Send(command);
        }
    }
}
