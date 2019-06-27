using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    /// <summary>
     /// Attribute Status Record field.
     /// <p>
     /// <b>minInterval</b>:
     /// The minimum reporting interval field is 16 bits in length and shall contain the
     /// minimum interval, in seconds, between issuing reports of the specified attribute.
     /// If minInterval is set to 0x0000, then there is no minimum limit, unless one is
     /// imposed by the specification of the cluster using this reporting mechanism or by
     /// the applicable profile.
     /// <p>
     /// <b>maxInterval</b>:
     /// The maximum reporting interval field is 16 bits in length and shall contain the
     /// maximum interval, in seconds, between issuing reports of the specified attribute.
     /// If maxInterval is set to 0xffff, then the device shall not issue reports for the specified
     /// attribute, and the configuration information for that attribute need not be
     /// maintained.
     /// <p>
     /// <b>reportableChange</b>:
     /// The reportable change field shall contain the minimum change to the attribute that
     /// will result in a report being issued. This field is of variable length. For attributes
     /// with 'analog' data type the field has the same data type as the attribute. The sign (if any) of the reportable
     /// change field is ignored.
     /// <p>
     /// <b>timeout</b>:
     /// The timeout period field is 16 bits in length and shall contain the maximum
     /// expected time, in seconds, between received reports for the attribute specified in
     /// the attribute identifier field. If more time than this elapses between reports, this
     /// may be an indication that there is a problem with reporting.
     /// If timeout is set to 0x0000, reports of the attribute are not subject to timeout.
     /// Note that, for a server/client connection to work properly using automatic
     /// reporting, the timeout value set for attribute reports to be received by the client (or
     /// server) cluster must be set somewhat higher than the maximum reporting interval
     /// set for the attribute on the server (or client) cluster.
     ///
     ///
     /// </summary>
    public class AttributeReportingConfigurationRecord : IZclListItemField
    {
        /// <summary>
         /// The direction.
         /// <p>
         /// The direction field specifies whether values of the attribute are be reported, or
         /// whether reports of the attribute are to be received.
         /// <p>
         /// If this value is set to 0x00, then the attribute data type field, the minimum
         /// reporting interval field, the maximum reporting interval field and the reportable
         /// change field are included in the payload, and the timeout period field is omitted.
         /// The record is sent to a cluster server (or client) to configure how it sends reports to
         /// a client (or server) of the same cluster.
         /// <p>
         /// If this value is set to 0x01, then the timeout period field is included in the payload,
         /// and the attribute data type field, the minimum reporting interval field, the
         /// maximum reporting interval field and the reportable change field are omitted. The
         /// record is sent to a cluster client (or server) to configure how it should expect
         /// reports from a server (or client) of the same cluster.
         /// </summary>
        public byte Direction { get; set; }
        /// <summary>
         /// The attribute identifier.
         /// </summary>
        public ushort AttributeIdentifier { get; set; }
        /// <summary>
         /// The attribute data type.
         /// </summary>
        public ZclDataType AttributeDataType { get; set; }
        /// <summary>
         /// The minimum reporting interval.
         /// <p>
         /// The minimum reporting interval field is 16 bits in length and shall contain the
         /// minimum interval, in seconds, between issuing reports of the specified attribute.
         /// If minInterval is set to 0x0000, then there is no minimum limit, unless one is
         /// imposed by the specification of the cluster using this reporting mechanism or by
         /// the applicable profile.
         /// </summary>
        public ushort MinimumReportingInterval { get; set; }
        /// <summary>
         /// The maximum reporting interval.
         /// <p>
         /// The maximum reporting interval field is 16 bits in length and shall contain the
         /// maximum interval, in seconds, between issuing reports of the specified attribute.
         /// If maxInterval is set to 0xffff, then the device shall not issue reports for the specified
         /// attribute, and the configuration information for that attribute need not be
         /// maintained.
         /// </summary>
        public ushort MaximumReportingInterval { get; set; }
        /// <summary>
         /// The reportable change.
         /// <p>
         /// The reportable change field shall contain the minimum change to the attribute that
         /// will result in a report being issued. This field is of variable length. For attributes
         /// with 'analog' data type the field has the same data type as the attribute. The sign (if any) of the reportable
         /// change field is ignored.
         /// </summary>
        public object ReportableChange { get; set; }
        /// <summary>
         /// The maximum reporting interval.
         /// <p>
         /// The timeout period field is 16 bits in length and shall contain the maximum
         /// expected time, in seconds, between received reports for the attribute specified in
         /// the attribute identifier field. If more time than this elapses between reports, this
         /// may be an indication that there is a problem with reporting.
         /// If timeout is set to 0x0000, reports of the attribute are not subject to timeout.
         /// Note that, for a server/client connection to work properly using automatic
         /// reporting, the timeout value set for attribute reports to be received by the client (or
         /// server) cluster must be set somewhat higher than the maximum reporting interval
         /// set for the attribute on the server (or client) cluster.
         /// </summary>
        public ushort TimeoutPeriod { get; set; }

        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(Direction, DataType.UNSIGNED_8_BIT_INTEGER);
            serializer.AppendZigBeeType(AttributeIdentifier, DataType.UNSIGNED_16_BIT_INTEGER);

            if (Direction == 1)
            {
                // If direction is set to 0x01, then the timeout period field is included in the payload,
                // and the attribute data type field, the minimum reporting interval field, the
                // maximum reporting interval field and the reportable change field are omitted. The
                // record is sent to a cluster client (or server) to configure how it should expect
                // reports from a server (or client) of the same cluster.
                serializer.AppendZigBeeType(TimeoutPeriod, DataType.UNSIGNED_16_BIT_INTEGER);
            }
            else
            {
                // If direction is set to 0x00, then the attribute data type field, the minimum
                // reporting interval field, the maximum reporting interval field and the reportable
                // change field are included in the payload, and the timeout period field is omitted.
                // The record is sent to a cluster server (or client) to configure how it sends reports to
                // a client (or server) of the same cluster.
                serializer.AppendZigBeeType(AttributeDataType.Id, DataType.UNSIGNED_8_BIT_INTEGER);
                serializer.AppendZigBeeType(MinimumReportingInterval, DataType.UNSIGNED_16_BIT_INTEGER);
                serializer.AppendZigBeeType(MaximumReportingInterval, DataType.UNSIGNED_16_BIT_INTEGER);

                // The reportable change field shall contain the minimum change to the attribute that
                // will result in a report being issued. This field is of variable length. For attributes
                // with 'analog' data typethe field has the same data type as the attribute. The sign (if any) of the
                // reportable change field is ignored. For attributes of 'discrete' data type this field is omitted.
                if (AttributeDataType.IsAnalog)
                {
                    serializer.AppendZigBeeType(ReportableChange, AttributeDataType.DataType);
                }
            }
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            Direction = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            AttributeIdentifier = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            if (Direction == 1)
            {
                // If direction is set to 0x01, then the timeout period field is included in the payload,
                // and the attribute data type field, the minimum reporting interval field, the
                // maximum reporting interval field and the reportable change field are omitted. The
                // record is sent to a cluster client (or server) to configure how it should expect
                // reports from a server (or client) of the same cluster.
                TimeoutPeriod = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
            }
            else
            {
                // If direction is set to 0x00, then the attribute data type field, the minimum
                // reporting interval field, the maximum reporting interval field and the reportable
                // change field are included in the payload, and the timeout period field is omitted.
                // The record is sent to a cluster server (or client) to configure how it sends reports to
                // a client (or server) of the same cluster.
                AttributeDataType = ZclDataType.Get(deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER));
                MinimumReportingInterval = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
                MaximumReportingInterval = deserializer.ReadZigBeeType<ushort>(DataType.UNSIGNED_16_BIT_INTEGER);
                if (AttributeDataType.IsAnalog)
                {
                    // The reportable change field shall contain the minimum change to the attribute that
                    // will result in a report being issued. This field is of variable length. For attributes
                    // with 'analog' data typethe field has the same data type as the attribute. The sign (if any) of the
                    // reportable change field is ignored. For attributes of 'discrete' data type this field is omitted.
                    ReportableChange = deserializer.ReadZigBeeType<object>(AttributeDataType.DataType);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(220);

            builder.Append("AttributeReportingConfigurationRecord: [attributeDataType=")
                   .Append(AttributeDataType)
                   .Append(", attributeIdentifier=")
                   .Append(AttributeIdentifier)
                   .Append(", direction=")
                   .Append(Direction);

            if (Direction == 0)
            {
                builder.Append(", minimumReportingInterval=")
                       .Append(MinimumReportingInterval)
                       .Append(", maximumReportingInterval=")
                       .Append(MaximumReportingInterval);
                if (AttributeDataType.IsAnalog)
                {
                    builder.Append(", reportableChange=")
                           .Append(ReportableChange);
                }
            }
            else if (Direction == 1)
            {
                builder.Append(", timeoutPeriod=")
                       .Append(TimeoutPeriod);
            }

            builder.Append(']');

            return builder.ToString();
        }
    }
}
