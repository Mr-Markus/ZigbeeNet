using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    public class ZclAttribute
    {
        /// <summary>
        /// Defines a Cluster Library Attribute
        /// </summary>
        private readonly ZclClusterType cluster;

        /// <summary>
        /// The attribute identifier field is 16-bits in length and shall contain the
        /// identifier of the attribute that the reporting configuration details
        /// apply to.
        /// </summary>
        public ushort Id { get; set; }

        /// <summary>
        /// Stores the name of this attribute;
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Defines the ZigBee data type.
        /// </summary>
        public ZclDataType ZclDataType { get; set; }

        /// <summary>
        /// Returns true if the implementation of this attribute in the cluster is
        /// mandatory as required by the ZigBee standard. <br>
        /// Note that this does not necessarily mean that the attribute is actually
        /// implemented in any device if it does not conform to the standard.
        ///
        /// return true if the attribute must be implemented
        /// </summary>
        public bool Mandatory { get; set; }

        /// <summary>
        /// Returns true if this attribute is supported by this device
        /// </summary>
        public bool Implemented { get; set; }

        /// <summary>
        /// True if this attribute is readable
        /// </summary>
        public bool Readable { get; set; }

        /// <summary>
        /// True if this attribute is writeable
        /// </summary>
        public bool Writeable { get; set; }

        /// <summary>
        /// True if this attribute is reportable
        /// </summary>
        public bool Reportable { get; set; }

        /// <summary>
        /// The minimum reporting interval field is 16-bits in length and shall
        /// contain the minimum interval, in seconds, between issuing reports for the
        /// attribute specified in the attribute identifier field. If the minimum
        /// reporting interval has not been configured, this field shall contain the
        /// value 0xffff.
        /// </summary>
        public int MinimumReportingPeriod { get; set; }

        /// <summary>
        /// The maximum reporting interval field is 16-bits in length and shall
        /// contain the maximum interval, in seconds, between issuing reports for the
        /// attribute specified in the attribute identifier field. If the maximum
        /// reporting interval has not been configured, this field shall contain the
        /// value 0xffff.
        /// </summary>
        public int MaximumReportingPeriod { get; set; }

        /// <summary>
        /// The reportable change field shall contain the minimum change to the
        /// attribute that will result in a report being issued. For attributes with
        /// 'analog' data type the field has the same data type as the attribute. If
        /// the reportable change has not been configured, this field shall contain
        /// the invalid value for the relevant data type
        /// </summary>
        public object ReportingChange { get; set; }

        /// <summary>
        /// The timeout period field is 16-bits in length and shall contain the
        /// maximum expected time, in seconds, between received reports for the
        /// attribute specified in the attribute identifier field. If the timeout
        /// period has not been configured, this field shall contain the value
        /// 0xffff.
        /// </summary>
        public int ReportingTimeout { get; set; }

        /// <summary>
        /// Records the last time a report was received
        /// </summary>
        public DateTime LastReportTime { get; set; }

        /// <summary>
        /// Records the last value received
        /// </summary>
        public object LastValue { get; set; }

        /// <summary>
        /// Constructor used to set the static information
        /// </summary>
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


        /// <summary>
        /// Checks if the last value received for the attribute is still current.
        /// If the last update time is more recent than the allowedAge then this will return true. allowedAge is defined in
        /// milliseconds.
        ///
        /// <param name="allowedAge">the number of milliseconds to consider the value current</param>
        /// <returns>true if the last value can be considered current</returns>
        /// </summary>
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

        /// <summary>
        /// Updates the attribute value This will also record the time of the last update
        ///
        /// @param attributeValue
        ///            the attribute value to be updated <see cref="Object">
        /// </summary>
        public void UpdateValue(object attributeValue)
        {
            LastValue = attributeValue;
            LastReportTime = DateTime.UtcNow;
        }


        public override string ToString()
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
