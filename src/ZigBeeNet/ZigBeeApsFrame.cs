using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public class ZigBeeApsFrame
    {
        /// <summary>
        /// The type of destination address supplied by the DstAddr parameter
        /// </summary>
        public ZigBeeNwkAddressMode AddressMode { get; set; }

        /// <summary>
        /// Destination address.
        /// </summary>
        public ushort DestinationAddress { get; set; }

        /// <summary>
        /// Destination address.
        /// </summary>
        public IeeeAddress DestinationIeeeAddress { get; set; }

        /// <summary>
        /// Source address
        /// </summary>
        public ushort SourceAddress { get; set; }

        /// <summary>
        /// The distance, in hops, that a frame will be allowed to travel through the network.
        /// </summary>
        public int Radius { get; set; }

        /// <summary>
        /// The distance, in hops, that a multicast frame will be relayed by nodes not a member of the group. A value of 0x07 is treated as infinity.
        /// </summary>
        public int NonMemberRadius { get; set; }

        /// <summary>
        /// The SecurityEnable parameter may be used to enable NWK layer security processing for the current frame. If the
        /// nwkSecurityLevel attribute of the NIB has a value of 0, meaning no security, then this parameter will be ignored.
        /// Otherwise, a value of TRUE denotes that the security processing specified by the security level will be applied,
        /// and a value of FALSE denotes that no security processing will be applied.
        /// </summary>
        public bool SecurityEnabled { get; set; }

        /// <summary>
        /// The destination endpoint field is 8-bits in length and specifies the endpoint of the final recipient of the
        /// frame.This field shall be included in the frame only if the delivery mode sub-field of the frame control field
        /// is set to 0b00 (normal unicast delivery), 0b01 (indirect delivery where the indirect address mode sub-field of
        /// the frame control field is also set to 0), or 0b10 (broadcast delivery). In the case of broadcast delivery, the
        /// frame shall be delivered to the destination endpoint specified within the range 0x01-0xf0 or to all active
        /// endpoints if specified as 0xff.
        ///
        /// A destination endpoint value of 0x00 addresses the frame to the ZigBee device object (ZDO), resident in each
        /// device.A destination endpoint value of 0x01-0xf0 addresses the frame to an application operating on that
        /// endpoint. A destination endpoint value of 0xff addresses the frame to all active endpoints except endpoint 0x00.
        ///
        /// All other endpoints (0xf1-0xfe) are reserved.
        /// </summary>
        public byte DestinationEndpoint { get; set; }

        /// <summary>
        /// The cluster identifier field is 16 bits in length and specifies the identifier of the cluster to which the frame
        /// relates and which shall be made available for filtering and interpretation of messages at each device that takes
        /// delivery of the frame.
        ///
        /// This field shall be present only for data or acknowledgement frames.
        /// </summary>
        public ushort Cluster { get; set; }

        /// <summary>
        /// The profile identifier is two octets in length and specifies the ZigBee profile identifier for which the frame is
        /// intended and shall be used during the filtering of messages at each device that takes delivery of the frame.
        ///
        /// This field shall be present only for data or acknowledgement frames.
        /// </summary>
        public ushort Profile { get; set; }

        /// <summary>
        /// The source endpoint field is eight-bits in length and specifies the endpoint of the initial originator of the
        /// frame.A source endpoint value of 0x00 indicates that the frame originated from the ZigBee device object (ZDO)
        /// resident in each device.A source endpoint value of 0x01-0xf0 indicates that the frame originated from an
        /// application operating on that endpoint. All other endpoints (0xf1-0xff) are reserved.
        /// </summary>
        public byte SourceEndpoint { get; set; }

        /// <summary>
        /// The group address field is 16 bits in length and will only be present if the delivery mode sub-field of the frame
        /// control has a value of 0b11. In this case, the destination endpoint shall not be present.
        /// </summary>
        public ushort GroupAddress { get; set; }

        /// <summary>
        /// This field is eight bits in length and is used to prevent the reception of duplicate frames. This value shall be
        /// incremented by one for each new transmission.
        /// </summary>
        public byte ApsCounter { get; set; }

        /// <summary>
        /// The APS payload.
        /// 
        /// This is defined as the application payload as defined in the ZigBee standard.This could include a ZCL cluster
        /// starting with the ZCL header, or a ZDO frame etc.
        /// </summary>
        public byte[] Payload { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ZigBeeApsFrame [sourceAddress=");
            builder.Append(SourceAddress);
            builder.Append("/");
            builder.Append(SourceEndpoint);
            builder.Append(", destinationAddress=");
            builder.Append(DestinationAddress);
            builder.Append("/");
            builder.Append(DestinationEndpoint);
            builder.Append(", profile=");
            builder.Append(Profile);
            builder.Append(", cluster=");
            builder.Append(Cluster);
            builder.Append(", addressMode=");
            builder.Append(AddressMode);
            builder.Append(", radius=");
            builder.Append(Radius);
            builder.Append(", apsCounter=");
            builder.Append(ApsCounter);
            builder.Append(", payload=");

            if (Payload != null)
            {
                for (int c = 0; c < Payload.Length; c++)
                {
                    if (c != 0)
                    {
                        builder.Append(" ");
                    }

                    builder.Append(Payload[c]);
                }
            }

            builder.Append("]");

            return builder.ToString();
        }
    }
}
