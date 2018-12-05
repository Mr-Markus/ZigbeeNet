﻿using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Field
{
    /**
     * Attribute Status Record field.
     * <p>
     * <b>minInterval</b>:
     * The minimum reporting interval field is 16 bits in length and shall contain the
     * minimum interval, in seconds, between issuing reports of the specified attribute.
     * If minInterval is set to 0x0000, then there is no minimum limit, unless one is
     * imposed by the specification of the cluster using this reporting mechanism or by
     * the applicable profile.
     * <p>
     * <b>maxInterval</b>:
     * The maximum reporting interval field is 16 bits in length and shall contain the
     * maximum interval, in seconds, between issuing reports of the specified attribute.
     * If maxInterval is set to 0xffff, then the device shall not issue reports for the specified
     * attribute, and the configuration information for that attribute need not be
     * maintained.
     * <p>
     * <b>reportableChange</b>:
     * The reportable change field shall contain the minimum change to the attribute that
     * will result in a report being issued. This field is of variable length. For attributes
     * with 'analog' data type the field has the same data type as the attribute. The sign (if any) of the reportable
     * change field is ignored.
     * <p>
     * <b>timeout</b>:
     * The timeout period field is 16 bits in length and shall contain the maximum
     * expected time, in seconds, between received reports for the attribute specified in
     * the attribute identifier field. If more time than this elapses between reports, this
     * may be an indication that there is a problem with reporting.
     * If timeout is set to 0x0000, reports of the attribute are not subject to timeout.
     * Note that, for a server/client connection to work properly using automatic
     * reporting, the timeout value set for attribute reports to be received by the client (or
     * server) cluster must be set somewhat higher than the maximum reporting interval
     * set for the attribute on the server (or client) cluster.
     *
     *
     */
    public class AttributeReportingConfigurationRecord : IZclListItemField
    {
        /**
         * The direction.
         * <p>
         * The direction field specifies whether values of the attribute are be reported, or
         * whether reports of the attribute are to be received.
         * <p>
         * If this value is set to 0x00, then the attribute data type field, the minimum
         * reporting interval field, the maximum reporting interval field and the reportable
         * change field are included in the payload, and the timeout period field is omitted.
         * The record is sent to a cluster server (or client) to configure how it sends reports to
         * a client (or server) of the same cluster.
         * <p>
         * If this value is set to 0x01, then the timeout period field is included in the payload,
         * and the attribute data type field, the minimum reporting interval field, the
         * maximum reporting interval field and the reportable change field are omitted. The
         * record is sent to a cluster client (or server) to configure how it should expect
         * reports from a server (or client) of the same cluster.
         */
        public int Direction { get; private set; }
        /**
         * The attribute identifier.
         */
        public int AttributeIdentifier { get; private set; }
        /**
         * The attribute data type.
         */
        public ZclDataType AttributeDataType { get; private set; }
        /**
         * The minimum reporting interval.
         * <p>
         * The minimum reporting interval field is 16 bits in length and shall contain the
         * minimum interval, in seconds, between issuing reports of the specified attribute.
         * If minInterval is set to 0x0000, then there is no minimum limit, unless one is
         * imposed by the specification of the cluster using this reporting mechanism or by
         * the applicable profile.
         */
        public int MinimumReportingInterval { get; private set; }
        /**
         * The maximum reporting interval.
         * <p>
         * The maximum reporting interval field is 16 bits in length and shall contain the
         * maximum interval, in seconds, between issuing reports of the specified attribute.
         * If maxInterval is set to 0xffff, then the device shall not issue reports for the specified
         * attribute, and the configuration information for that attribute need not be
         * maintained.
         */
        public int MaximumReportingInterval { get; private set; }
        /**
         * The reportable change.
         * <p>
         * The reportable change field shall contain the minimum change to the attribute that
         * will result in a report being issued. This field is of variable length. For attributes
         * with 'analog' data type the field has the same data type as the attribute. The sign (if any) of the reportable
         * change field is ignored.
         */
        public object ReportableChange { get; private set; }
        /**
         * The maximum reporting interval.
         * <p>
         * The timeout period field is 16 bits in length and shall contain the maximum
         * expected time, in seconds, between received reports for the attribute specified in
         * the attribute identifier field. If more time than this elapses between reports, this
         * may be an indication that there is a problem with reporting.
         * If timeout is set to 0x0000, reports of the attribute are not subject to timeout.
         * Note that, for a server/client connection to work properly using automatic
         * reporting, the timeout value set for attribute reports to be received by the client (or
         * server) cluster must be set somewhat higher than the maximum reporting interval
         * set for the attribute on the server (or client) cluster.
         */
        public int TimeoutPeriod { get; private set; }

        public void Serialize(IZigBeeSerializer serializer)
        {
            serializer.AppendZigBeeType(Direction, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.AppendZigBeeType(AttributeIdentifier, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));

            if (Direction == 1)
            {
                // If direction is set to 0x01, then the timeout period field is included in the payload,
                // and the attribute data type field, the minimum reporting interval field, the
                // maximum reporting interval field and the reportable change field are omitted. The
                // record is sent to a cluster client (or server) to configure how it should expect
                // reports from a server (or client) of the same cluster.
                serializer.AppendZigBeeType(TimeoutPeriod, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
            else
            {
                // If direction is set to 0x00, then the attribute data type field, the minimum
                // reporting interval field, the maximum reporting interval field and the reportable
                // change field are included in the payload, and the timeout period field is omitted.
                // The record is sent to a cluster server (or client) to configure how it sends reports to
                // a client (or server) of the same cluster.
                serializer.AppendZigBeeType(AttributeDataType.Id, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
                serializer.AppendZigBeeType(MinimumReportingInterval, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
                serializer.AppendZigBeeType(MaximumReportingInterval, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));

                // The reportable change field shall contain the minimum change to the attribute that
                // will result in a report being issued. This field is of variable length. For attributes
                // with 'analog' data typethe field has the same data type as the attribute. The sign (if any) of the
                // reportable change field is ignored. For attributes of 'discrete' data type this field is omitted.
                if (AttributeDataType.IsAnalog)
                {
                    serializer.AppendZigBeeType(ReportableChange, AttributeDataType);
                }
            }
        }

        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            Direction = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            AttributeIdentifier = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            if (Direction == 1)
            {
                // If direction is set to 0x01, then the timeout period field is included in the payload,
                // and the attribute data type field, the minimum reporting interval field, the
                // maximum reporting interval field and the reportable change field are omitted. The
                // record is sent to a cluster client (or server) to configure how it should expect
                // reports from a server (or client) of the same cluster.
                TimeoutPeriod = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            }
            else
            {
                // If direction is set to 0x00, then the attribute data type field, the minimum
                // reporting interval field, the maximum reporting interval field and the reportable
                // change field are included in the payload, and the timeout period field is omitted.
                // The record is sent to a cluster server (or client) to configure how it sends reports to
                // a client (or server) of the same cluster.
                AttributeDataType = ZclDataType.Get((int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER)));
                MinimumReportingInterval = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
                MaximumReportingInterval = (int)deserializer.ReadZigBeeType(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
                if (AttributeDataType.IsAnalog)
                {
                    // The reportable change field shall contain the minimum change to the attribute that
                    // will result in a report being issued. This field is of variable length. For attributes
                    // with 'analog' data typethe field has the same data type as the attribute. The sign (if any) of the
                    // reportable change field is ignored. For attributes of 'discrete' data type this field is omitted.
                    ReportableChange = deserializer.ReadZigBeeType(AttributeDataType);
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
