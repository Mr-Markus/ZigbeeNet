
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.IASWD;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// IAS WD cluster implementation (Cluster ID 0x0502).
    ///
    /// The IAS WD cluster provides an interface to the functionality of any Warning Device
    /// equipment of the IAS system. Using this cluster, a ZigBee enabled CIE device can access a
    /// ZigBee enabled IAS WD device and issue alarm warning indications (siren, strobe
    /// lighting, etc.) when a system alarm condition is detected.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclIasWdCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0502;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "IAS WD";

        // Attribute constants

        /// <summary>
        /// The MaxDuration attribute specifies the maximum time in seconds that the siren
        /// will sound continuously, regardless of start/stop commands.
        /// </summary>
        public const ushort ATTR_MAXDURATION = 0x0000;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(1);

            attributeMap.Add(ATTR_MAXDURATION, new ZclAttribute(this, ATTR_MAXDURATION, "Max Duration", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, true, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(2);

            commandMap.Add(0x0000, () => new StartWarningCommand());
            commandMap.Add(0x0001, () => new Squawk());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a IAS WD cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclIasWdCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Start Warning Command
        ///
        /// This command starts the WD operation. The WD alerts the surrounding area by audible
        /// (siren) and visual (strobe) signals. <br> A Start Warning command shall always
        /// terminate the effect of any previous command that is still current.
        ///
        /// <param name="header" <see cref="byte"> Header</ param >
        /// <param name="warningDuration" <see cref="ushort"> Warning Duration</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> StartWarningCommand(byte header, ushort warningDuration)
        {
            StartWarningCommand command = new StartWarningCommand();

            // Set the fields
            command.Header = header;
            command.WarningDuration = warningDuration;

            return Send(command);
        }

        /// <summary>
        /// The Squawk
        ///
        /// This command uses the WD capabilities to emit a quick audible/visible pulse called
        /// a "squawk". The squawk command has no effect if the WD is currently active (warning
        /// in progress).
        ///
        /// <param name="squawkInfo" <see cref="byte"> Squawk Info</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> Squawk(byte squawkInfo)
        {
            Squawk command = new Squawk();

            // Set the fields
            command.SquawkInfo = squawkInfo;

            return Send(command);
        }
    }
}
