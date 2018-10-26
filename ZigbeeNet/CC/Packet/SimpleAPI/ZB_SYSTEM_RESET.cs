using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.CC.Packet.SimpleAPI
{
    /// <summary>
    /// This command will reset the device by using a soft reset (i.e. a jump to the reset vector) 
    /// vice a hardware reset (i.e. watchdog reset.) This is especially useful in the CC2531, 
    /// for instance, so that the USB host does not have to contend with the USB H/W resetting 
    /// (and thus causing the USB host to re-enumerate the device which can cause an open virtual serial port to hang.) 
    /// </summary>
    public class ZB_SYSTEM_RESET : SynchronousRequest
    {
        public ZB_SYSTEM_RESET()
        {
            BuildPacket(CommandType.ZB_SYSTEM_RESET, new byte[0]);
        }

        public ZB_SYSTEM_RESET(byte[] framedata)
        {
            BuildPacket(CommandType.ZB_SYSTEM_RESET, framedata);
        }
    }
}
