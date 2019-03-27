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
    /// Class to implement the XBee command " Set Encryption Enable ".
    /// AT Command <b>EE</b></p>Set or read the encryption enable setting.
    ///This class provides methods for processing XBee API commands.
    ///
    /// </summary>
    public class XBeeSetEncryptionEnableCommand : XBeeFrame, IXBeeCommand 
    {
        
        /// <summary>
        /// 
        /// </summary>
        private int _frameId;
        
        /// <summary>
        /// 
        /// </summary>
        private bool _enableEncryption;
        
        /// <summary>
        /// The frameId to set as <see cref="uint8"/>
        /// </summary>
        public void SetFrameId(int frameId)
        {
            this._frameId = frameId;
        }
        
        /// <summary>
        /// The enableEncryption to set as <see cref="Boolean"/>
        /// </summary>
        public void SetEnableEncryption(bool enableEncryption)
        {
            this._enableEncryption = enableEncryption;
        }
    }
}