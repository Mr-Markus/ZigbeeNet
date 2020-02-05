using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet
{
    public interface IZigBeeAnnounceListener
    {
        /// <summary>
        /// Called when a new device is heard on the network.
        /// </summary>
        /// <param name="deviceStatus"> the ZigBeeNodeStatus of the newly announced device</param>
        /// <param name="networkAddress"> the network address of the newly announced device</param>
        /// <param name="ieeeAddress"> the IeeeAddress of the newly announced device</param>
        void DeviceStatusUpdate(ZigBeeNodeStatus deviceStatus, ushort networkAddress, IeeeAddress ieeeAddress);

        ///<summary>
        /// Called when a device is heard that is unknown to the system. This will generally mean that the device is not
        /// known to the Network Manager, however it is joined to the network and should be rediscovered.
        ///</summary>
        ///<param name="networkAddress">the network address of the unknown device</param>
        void AnnounceUnknownDevice(ushort networkAddress);
    }
}
