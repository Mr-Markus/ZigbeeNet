//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:3.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZigBeeNet.Hardware.Ember.Ezsp.Command
{
    using ZigBeeNet.Hardware.Ember.Internal.Serializer;
    
    
    /// <summary>
    /// Class to implement the Ember EZSP command " getNodeId ".
    /// Returns the 16-bit node ID of the local node.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspGetNodeIdResponse : EzspFrameResponse
    {
        
        public const int FRAME_ID = 39;
        
        /// <summary>
        ///  The 16-bit ID.
        /// </summary>
        private int _nodeId;
        
        public EzspGetNodeIdResponse(int[] inputBuffer) : 
                base(inputBuffer)
        {
            _nodeId = deserializer.DeserializeUInt16();
        }
        
        /// <summary>
        /// The nodeId to set as <see cref="EmberNodeId"/> </summary>
        public void SetNodeId(int nodeId)
        {
            _nodeId = nodeId;
        }
        
        /// <summary>
        ///  The 16-bit ID.
        /// Return the nodeId as <see cref="System.Int32"/>
        /// </summary>
        public int GetNodeId()
        {
            return _nodeId;
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspGetNodeIdResponse [nodeId=");
            builder.Append(string.Format("0x{0:X04}", _nodeId));
            builder.Append(']');
            return builder.ToString();
        }
    }
}