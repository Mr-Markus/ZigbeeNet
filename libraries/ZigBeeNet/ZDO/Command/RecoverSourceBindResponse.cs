using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZDO.Field;


namespace ZigBeeNet.ZDO.Command
{
    /// <summary>
    /// Recover Source Bind Response value object class.
    ///
    ///
    /// The Recover_Source_Bind_rsp is generated from a backup binding table cache device in
    /// response to a Recover_Source_Bind_req from a primary binding table cache and contains
    /// the status of the request. This command shall be unicast to the requesting device. If the
    /// responding device is not a backup binding table cache, it shall return a status of
    /// NOT_SUPPORTED. If the originator of the request is not recognized as a primary binding
    /// table cache, it shall return a status of INV_REQUESTTYPE. Otherwise, the backup
    /// binding table cache shall prepare a list of binding table entries from its backup
    /// beginning with StartIndex. It will fit in as many entries as possible into a
    /// Recover_Source_Bind_rsp command and return a status of SUCCESS. If StartIndex is more
    /// than the number of entries in the Source table, a status of NO_ENTRY is returned. For a
    /// successful response, SourceTableEntries is the total number of entries in the backup
    /// source table, and SourceTableListCount is the number of entries which is being
    /// returned in the response.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class RecoverSourceBindResponse : ZdoResponse
    {
        /// <summary>
        /// The ZDO cluster ID.
        /// </summary>
        public const ushort CLUSTER_ID = 0x8029;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public RecoverSourceBindResponse()
        {
            ClusterId = CLUSTER_ID;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("RecoverSourceBindResponse [");
            builder.Append(base.ToString());
            builder.Append(']');

            return builder.ToString();
        }
    }
}
