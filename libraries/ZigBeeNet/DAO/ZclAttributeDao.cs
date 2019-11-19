using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.DAO
{
    /**
    * This class provides a clean class to hold a data object for serialisation of a {@link ZclAttribute}
    *
    */
    public class ZclAttributeDao
    {
        /**
         * The attribute identifier field is 16-bits in length and shall contain the
         * identifier of the attribute that the reporting configuration details
         * apply to.
         */
        public ushort Id { get; set; }

        /**
         * Stores the name of this attribute;
         */
        public string Name { get; set; }

        /**
         * Defines the ZigBee data type.
         */
        public ZclDataType DataType { get; set; }

        /**
         * Defines if this attribute is mandatory to be implemented
         */
        public bool Mandatory { get; set; }

        /**
         * Defines if the attribute is implemented by the device
         */
        public bool Implemented { get; set; }

        /**
         * True if this attribute is readable
         */
        public bool Readable { get; set; }

        /**
         * True if this attribute is writable
         */
        public bool Writable { get; set; }

        /**
         * True if this attribute is reportable
         */
        public bool Reportable { get; set; }

        /**
         * The minimum reporting interval field is 16-bits in length and shall
         * contain the minimum interval, in seconds, between issuing reports for the
         * attribute specified in the attribute identifier field. If the minimum
         * reporting interval has not been configured, this field shall contain the
         * value 0xffff.
         */
        public int MinimumReportingPeriod { get; set; }

        /**
         * The maximum reporting interval field is 16-bits in length and shall
         * contain the maximum interval, in seconds, between issuing reports for the
         * attribute specified in the attribute identifier field. If the maximum
         * reporting interval has not been configured, this field shall contain the
         * value 0xffff.
         */
        public int MaximumReportingPeriod { get; set; }

        /**
         * The reportable change field shall contain the minimum change to the
         * attribute that will result in a report being issued. For attributes with
         * 'analog' data type the field has the same data type as the attribute. If
         * the reportable change has not been configured, this field shall contain
         * the invalid value for the relevant data type
         */
        public object ReportingChange { get; set; }

        /**
         * The timeout period field is 16-bits in length and shall contain the
         * maximum expected time, in seconds, between received reports for the
         * attribute specified in the attribute identifier field. If the timeout
         * period has not been configured, this field shall contain the value
         * 0xffff.
         */
        public int ReportingTimeout { get; set; }

        /**
         * Records the last time a report was received
         */
        public DateTime LastReportTime { get; set; }

        /**
         * Records the last value received
         */
        public object LastValue { get; set; }
    }
}
