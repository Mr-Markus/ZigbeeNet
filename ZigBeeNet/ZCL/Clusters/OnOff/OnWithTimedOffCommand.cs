using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL.Clusters.OnOff
{
    /**
     * On With Timed Off Command value object class.
     * <p>
     * Cluster: <b>On/Off</b>. Command is sent <b>TO</b> the server.
     * This command is a <b>specific</b> command used for the On/Off cluster.
     * <p>
     * The On With Timed Off command allows devices to be turned on for a specific duration
     * with a guarded off duration so that SHOULD the device be subsequently switched off,
     * further On With Timed Off commands, received during this time, are prevented from
     * turning the devices back on. Note that the device can be periodically re-kicked by
     * subsequent On With Timed Off commands, e.g., from an on/off sensor.
     * <p>
     * Code is auto-generated. Modifications may be overwritten!
     */
    public class OnWithTimedOffCommand : ZclCommand
    {
        /**
         * On Off Control command message field.
         */
        public byte OnOffControl { get; set; }

        /**
         * On Time command message field.
         */
        public ushort OnTime { get; set; }

        /**
         * Off Wait Time command message field.
         */
        public ushort OffWaitTime { get; set; }

        /**
         * Default constructor.
         */
        public OnWithTimedOffCommand()
        {
            GenericCommand = false;
            ClusterId = 6;
            CommandId = 66;
            CommandDirection = ZclCommandDirection.CLIENT_TO_SERVER;
        }        

        public override void Serialize(ZclFieldSerializer serializer)
        {
            serializer.Serialize(OnOffControl, ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            serializer.Serialize(OnTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            serializer.Serialize(OffWaitTime, ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            OnOffControl = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_8_BIT_INTEGER));
            OnTime = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
            OffWaitTime = (ushort)deserializer.Deserialize(ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder()
                .Append("OnWithTimedOffCommand [")
                .Append(base.ToString())
                .Append(", onOffControl=")
                .Append(OnOffControl)
                .Append(", onTime=")
                .Append(OnTime)
                .Append(", offWaitTime=")
                .Append(OffWaitTime)
                .Append(']');

            return builder.ToString();
        }

    }
}
