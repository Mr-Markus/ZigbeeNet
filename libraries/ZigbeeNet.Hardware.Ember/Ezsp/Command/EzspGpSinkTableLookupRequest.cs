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
    using ZigBeeNet.Hardware.Ember.Ezsp.Structure;
    
    
    /// <summary>
    /// Class to implement the Ember EZSP command " gpSinkTableLookup ".
    /// Finds the index of the passed address in the gp table.
    /// This class provides methods for processing EZSP commands.
    /// </summary>
    public class EzspGpSinkTableLookupRequest : EzspFrameRequest
    {
        
        public const int FRAME_ID = 222;
        
        /// <summary>
        ///  The address to search for.
        /// </summary>
        private EmberGpAddress _addr;
        
        private EzspSerializer _serializer;
        
        public EzspGpSinkTableLookupRequest()
        {
            _frameId = FRAME_ID;
            _serializer = new EzspSerializer();
        }
        
        /// <summary>
        /// The addr to set as <see cref="EmberGpAddress"/> </summary>
        public void SetAddr(EmberGpAddress addr)
        {
            _addr = addr;
        }
        
        /// <summary>
        ///  The address to search for.
        /// Return the addr as <see cref="EmberGpAddress"/>
        /// </summary>
        public EmberGpAddress GetAddr()
        {
            return _addr;
        }
        
        /// <summary>
        /// Method for serializing the command fields </summary>
        public override int[] Serialize()
        {
            SerializeHeader(_serializer);
            _serializer.SerializeEmberGpAddress(_addr);
            return _serializer.GetPayload();
        }
        
        public override string ToString()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            builder.Append("EzspGpSinkTableLookupRequest [addr=");
            builder.Append(_addr);
            builder.Append(']');
            return builder.ToString();
        }
    }
}
