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
    
    
    /// <summary>
    /// Class to implement the XBee command " Get Firmware Version ".
    /// AT Command <b>VR</b></p>Reads the firmware version on a device as a 4-digit hex number. 
    ///This class provides methods for processing XBee API commands.
    ///
    /// </summary>
    public class XBeeGetFirmwareVersionCommand : XBeeFrame, IXBeeCommand 
    {
        
        /// <summary>
        /// 
        /// </summary>
        private int _frameId;
        
        /// <summary>
        /// The frameId to set as <see cref="uint8"/>
        /// </summary>
        public void SetFrameId(int frameId)
        {
            this._frameId = frameId;
        }
    }
}