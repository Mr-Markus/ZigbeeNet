using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    public class ZclAttribute
    {
        /**
         * Defines a Cluster Library Attribute
         */
        private readonly ZclClusterType cluster;

        /**
         * The attribute identifier field is 16-bits in length and shall contain the
         * identifier of the attribute that the reporting configuration details
         * apply to.
         */
        public ushort Id { get; private set; }

        /**
         * Stores the name of this attribute;
         */
        public string Name { get; private set; }

        /**
         * Defines the ZigBee data type.
         */
        public ZclDataType ZclDataType { get; private set; }

        /**
        * Returns true if the implementation of this attribute in the cluster is
        * mandatory as required by the ZigBee standard. <br>
        * Note that this does not necessarily mean that the attribute is actually
        * implemented in any device if it does not conform to the standard.
        *
        * return true if the attribute must be implemented
        */
        public bool Mandatory { get; private set; }

        /**
         * Returns true if this attribute is supported by this device
         */
        public bool Implemented { get; private set; }

        /**
         * True if this attribute is readable
         */
        public bool Readable { get; private set; }

        /**
         * True if this attribute is writeable
         */
        public bool Writeable { get; private set; }

        /**
         * True if this attribute is reportable
         */
        public bool Reportable { get; private set; }

        /**
         * The minimum reporting interval field is 16-bits in length and shall
         * contain the minimum interval, in seconds, between issuing reports for the
         * attribute specified in the attribute identifier field. If the minimum
         * reporting interval has not been configured, this field shall contain the
         * value 0xffff.
         */
        public int MinimumReportingPeriod { get; private set; }

        /**
         * The maximum reporting interval field is 16-bits in length and shall
         * contain the maximum interval, in seconds, between issuing reports for the
         * attribute specified in the attribute identifier field. If the maximum
         * reporting interval has not been configured, this field shall contain the
         * value 0xffff.
         */
        public int MaximumReportingPeriod { get; private set; }

        /**
         * The reportable change field shall contain the minimum change to the
         * attribute that will result in a report being issued. For attributes with
         * 'analog' data type the field has the same data type as the attribute. If
         * the reportable change has not been configured, this field shall contain
         * the invalid value for the relevant data type
         */
        public object ReportingChange { get; private set; }

        /**
         * The timeout period field is 16-bits in length and shall contain the
         * maximum expected time, in seconds, between received reports for the
         * attribute specified in the attribute identifier field. If the timeout
         * period has not been configured, this field shall contain the value
         * 0xffff.
         */
        public int ReportingTimeout { get; private set; }

        /**
         * Records the last time a report was received
         */
        public DateTime LastReportTime { get; private set; }

        /**
         * Records the last value received
         */
        public object LastValue { get; private set; }

        /**
         * Constructor used to set the static information
         */
        public ZclAttribute(ZclClusterType cluster, ushort id, string name, ZclDataType dataType,
                bool mandatory, bool readable, bool writeable, bool reportable)
        {
            this.cluster = cluster;
            this.Id = id;
            this.Name = name;
            this.ZclDataType = dataType;
            this.Mandatory = mandatory;
            this.Readable = readable;
            this.Writeable = writeable;
            this.Reportable = reportable;
        }


        /**
         * Checks if the last value received for the attribute is still current.
         * If the last update time is more recent than the allowedAge then this will return true. allowedAge is defined in
         * milliseconds.
         *
         * @param allowedAge the number of milliseconds to consider the value current
         * @return true if the last value can be considered current
         */
        public bool IsLastValueCurrent(long allowedAge)
        {
            if (LastReportTime == null)
            {
                return false;
            }

            long refreshTime = DateTime.UtcNow.Millisecond - allowedAge;
            if (refreshTime < 0)
            {
                return true;
            }
            return LastReportTime.Millisecond > refreshTime;
        }

        /**
         * Updates the attribute value This will also record the time of the last update
         *
         * @param attributeValue
         *            the attribute value to be updated {@link Object}
         */
        public void UpdateValue(object attributeValue)
        {
            LastValue = attributeValue;
            LastReportTime = DateTime.UtcNow;
        }


        public string SoString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ZclAttribute [cluster=")
                   .Append(cluster)
                   .Append(", id=")
                   .Append(Id)
                   .Append(", name=")
                   .Append(Name)
                   .Append(", dataType=")
                   .Append(ZclDataType)
                   .Append(", lastValue=")
                   .Append(LastValue);

            if (LastReportTime != null)
            {
                builder.Append(", lastReportTime=")
                       .Append(LastReportTime.ToLongTimeString());
            }

            builder.Append(']');

            return builder.ToString();
        }
    }
}
