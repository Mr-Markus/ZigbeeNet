using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC.Packet.ZDO
{
    /**
 * Response for requesting the network to switch channel or change PAN PROFILE_ID_HOME_AUTOMATION.
 *
 */
    public class ZDO_MGMT_NWK_UPDATE_REQ_SRSP : ZToolPacket
    {
        public int Status { get; private set; }

        public ZDO_MGMT_NWK_UPDATE_REQ_SRSP(byte[] framedata)
        {
            this.Status = framedata[0];
            BuildPacket(new DoubleByte(ZToolCMD.ZDO_MGMT_NWK_UPDATE_REQ_SRSP), framedata);
        }

        public override string ToString()
        {
            return "ZDO_MGMT_NWK_UPDATE_REQ_SRSP{" + "Status=" + Status + '}';
        }
    }
}
