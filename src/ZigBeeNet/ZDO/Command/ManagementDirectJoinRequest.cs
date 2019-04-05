using System;
using System.Text;
using ZigBeeNet.Transaction;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Management Direct Join Request value object class.
    /// 
    /// The Mgmt_Direct_Join_req is generated from a Local Device requesting that a
    /// Remote Device permit a device designated by DeviceAddress to join the network
    /// directly. The Mgmt_Direct_Join_req is generated by a management application
    /// which directs the request to a Remote Device where the NLME-DIRECTJOIN.request
    /// is to be executed using the parameter supplied by
    /// Mgmt_Direct_Join_req.
    /// 
/// </summary>
    public class ManagementDirectJoinRequest : ZdoRequest
    {
        /// <summary>
        /// DeviceAddress command message field.
/// </summary>
        public IeeeAddress DeviceAddress { get; set; }

        /// <summary>
        /// CapabilityInformation command message field.
/// </summary>
        public byte CapabilityInformation { get; set; }

        /// <summary>
        /// Default constructor.
/// </summary>
        public ManagementDirectJoinRequest()
        {
            ClusterId = 0x0035;
        }

        /// <summary>
        /// Gets DeviceAddress.
        ///
        /// <returns>the DeviceAddress</returns>
/// </summary>
        public IeeeAddress getDeviceAddress()
        {
            return DeviceAddress;
        }

        /// <summary>
        /// Sets DeviceAddress.
        ///
        /// <param name="deviceAddress">the DeviceAddress</param>
/// </summary>
        public void setDeviceAddress(IeeeAddress deviceAddress)
        {
            this.DeviceAddress = deviceAddress;
        }

        /// <summary>
        /// Gets CapabilityInformation.
        ///
        /// <returns>the CapabilityInformation</returns>
/// </summary>
        public int getCapabilityInformation()
        {
            return CapabilityInformation;
        }

        /// <summary>
        /// Sets CapabilityInformation.
        ///
        /// <param name="capabilityInformation">the CapabilityInformation</param>
/// </summary>
        public void SetCapabilityInformation(byte capabilityInformation)
        {
            this.CapabilityInformation = capabilityInformation;
        }

        public override void Serialize(ZclFieldSerializer serializer)
        {
            base.Serialize(serializer);

            serializer.Serialize(DeviceAddress, ZclDataType.Get(DataType.IEEE_ADDRESS));
            serializer.Serialize(CapabilityInformation, ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override void Deserialize(ZclFieldDeserializer deserializer)
        {
            base.Deserialize(deserializer);

            DeviceAddress = (IeeeAddress)deserializer.Deserialize(ZclDataType.Get(DataType.IEEE_ADDRESS));
            CapabilityInformation = (byte)deserializer.Deserialize(ZclDataType.Get(DataType.BITMAP_8_BIT));
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ManagementDirectJoinRequest [")
                   .Append(base.ToString())
                   .Append(", deviceAddress=")
                   .Append(DeviceAddress)
                   .Append(", capabilityInformation=")
                   .Append(CapabilityInformation)
                   .Append(']');

            return builder.ToString();
        }

    }
}
