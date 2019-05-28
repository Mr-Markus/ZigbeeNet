//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol
{
    using System.Collections.Generic;
    
    
    /// <summary>
    /// Class to implement the XBee command " Transmit Request ".
    /// This frame causes the device to send payload data as an RF packet to a specific destination. n
    /// For broadcast transmissions, set the 64-bit destination address to 0x000000000000FFFF.
    /// Address the coordinator by either setting the 64-bit address to all 0x00s and the 16-bit
    /// address to 0xFFFE, or setting the 64-bit address to the coordinator's 64-bit address and the
    /// 16-bit address to 0x0000. n For all other transmissions, setting the 16-bit address to the
    /// correct 16-bit address helps improve performance when transmitting to multiple
    /// destinations. If you do not know a 16-bit address, set this field to 0xFFFE (unknown). If
    /// successful, the Transmit Status frame (0x8B) indicates the discovered 16-bit address. You
    /// can set the broadcast radius from 0 up to NH. If set to 0, the value of NH specifies the broadcast
    /// radius (recommended). This parameter is only used for broadcast transmissions. You can
    /// read the maximum number of payload bytes with the NP command. 
    /// This class provides methods for processing XBee API commands.
    /// </summary>
    public class XBeeTransmitRequestCommand : XBeeFrame, IXBeeCommand 
    {
        
        /// <summary>
        ///  The frame Id 
        /// </summary>
        private int _frameId;
        
        /// <summary>
        ///  64-bit destination address. MSB first, LSB last. Set to the 64-bit address of the
        /// destination device. Reserved 64-bit address for the coordinator = 0x0000000000000000
        /// Broadcast = 0x000000000000FFFF. 
        /// </summary>
        private IeeeAddress _ieeeAddress;
        
        /// <summary>
        ///  16-bit destination network address. Set to the 16-bit address of the destination device, if
        /// known. If the address is unknown or if sending a broadcast, set to 0xFFFE. 
        /// </summary>
        private int _networkAddress;
        
        /// <summary>
        ///  0x01 - Disable retries. 0x20 - Enable APS encryption (if EE=1). 0x40 - Use the extended
        /// transmission timeout for this destination. Enabling APS encryption decreases the maximum
        /// number of RF payload bytes by 4 (below the value reported by NP). Setting the extended timeout
        /// bit causes the stack to set the extended transmission timeout for the destination address.
        /// See Transmission, addressing, and routing. All unused and unsupported bits must be set to 0. 
        /// </summary>
        private List<TransmitOptions> _options = new List<TransmitOptions>();
        
        /// <summary>
        ///  Data sent to the destination device.
        /// </summary>
        private int[] _data;
        
        /// <summary>
        /// The frameId to set as </summary>
        /// <seecref="uint8"
        ///>
        ///  </see>
        public void SetFrameId(int frameId)
        {
            _frameId = frameId;
        }
        
        /// <summary>
        /// The ieeeAddress to set as </summary>
        /// <seecref="IeeeAddress"
        ///>
        ///  </see>
        public void SetIeeeAddress(IeeeAddress ieeeAddress)
        {
            _ieeeAddress = ieeeAddress;
        }
        
        /// <summary>
        /// The networkAddress to set as </summary>
        /// <seecref="uint16"
        ///>
        ///  </see>
        public void SetNetworkAddress(int networkAddress)
        {
            _networkAddress = networkAddress;
        }
        
        /// <summary>
        /// The options to add to the set as </summary>
        /// <seecref="TransmitOptions"
        ///>
        ///  </see>
        public void AddOptions(TransmitOptions options)
        {
            _options.Add(options);
        }
        
        /// <summary>
        /// The options to remove to the set as </summary>
        /// <seecref="TransmitOptions"
        ///>
        ///  </see>
        public void RemoveOptions(TransmitOptions options)
        {
            _options.Remove(options);
        }
        
        /// <summary>
        /// The options to set to the set as </summary>
        /// <seecref="TransmitOptions"
        ///>
        ///  </see>
        public void SetOptions(IEnumerable<TransmitOptions> options)
        {
            _options.AddRange(options);
        }
        
        /// <summary>
        /// The data to set as </summary>
        /// <seecref="uint8[]"
        ///>
        ///  </see>
        public void SetData(int[] data)
        {
            _data = data;
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public int[] Serialize()
        {
            SerializeCommand(16);
            SerializeInt8(_frameId);
            SerializeIeeeAddress(_ieeeAddress);
            SerializeInt16(_networkAddress);
            SerializeTransmitOptions(_options);
            SerializeData(_data);
            return GetPayload();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder(566);
            builder.Append("XBeeTransmitRequestCommand [frameId=");
            builder.Append(_frameId);
            builder.Append(", ieeeAddress=");
            builder.Append(_ieeeAddress);
            builder.Append(", networkAddress=");
            builder.Append(_networkAddress);
            builder.Append(", options=");
            builder.Append(_options);
            builder.Append(", data=");
            if (_data == null)
            {
                builder.Append("null");
            }
            else
            {
                for (int cnt = 0
                ; cnt < _data.Length; cnt++
                )
                {
                    if (cnt > 0)
                    {
                        builder.Append(' ');
                    }
                    builder.Append(string.Format("0x{0:X2}", _data[cnt]));
                }
            }
            builder.Append(']');
            return builder.ToString();
        }
    }
}