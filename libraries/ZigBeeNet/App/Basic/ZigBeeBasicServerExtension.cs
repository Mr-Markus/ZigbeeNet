using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.App.Basic
{
    ///<summary>
    ///Extension to provide responses to basic client requests from remote devices. This provides a basic wrapper around the
    ///<see cref="ZclBasicServer"/> class by implementing the standard <see cref="IZigBeeNetworkExtension"/> interface.
    ///</summary>
    public class ZigBeeBasicServerExtension : IZigBeeNetworkExtension
    {
        private ZclBasicServer _basicServer;

        public ZigBeeStatus ExtensionInitialize(ZigBeeNetworkManager networkManager)
        {
            _basicServer = new ZclBasicServer(networkManager);
            return ZigBeeStatus.SUCCESS;
        }

        public ZigBeeStatus ExtensionStartup()
        {
            return ZigBeeStatus.SUCCESS;
        }

        public void ExtensionShutdown()
        {
            _basicServer.Shutdown();
        }

        ///<summary>
        ///Sets an attribute value in the basic server.
        ///
        ///<param name="attributeId">the attribute identifier to set</param>
        ///<param name="attributeValue"/>the value related to the attribute ID
        ///<returns>true if the attribute was set</returns> 
        ///</summary>
        public bool SetAttribute(ushort attributeId, object attributeValue)
        {
            return _basicServer.SetAttribute(attributeId, attributeValue);
        }
    }
}
