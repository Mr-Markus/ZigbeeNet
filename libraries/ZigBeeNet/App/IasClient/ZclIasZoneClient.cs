using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet;
using ZigBeeNet.App;
using ZigBeeNet.App.Discovery;
using ZigBeeNet.ZDO.Command;
using Serilog;
using ZigBeeNet.ZCL.Clusters;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Clusters.IasZone;

namespace ZigBeeNet.App.IasClient
{

    /// <summary>
    /// Implements the a minimal IAS client side functionality.The IAS(Intruder Alarm Systems) server clusters require
    /// support on the client side when enrolling.Once an IAS device joins the network, it looks for a CIE (Control and
    /// Indicating Equipment) which is implemented here.
    /// <p>
    /// There are three methods for enrolling IAS Zone server to an IAS CIE (i.e., IAS Zone client):
    /// <ul>
    /// <li>Trip-to-pair
    /// <li>Auto-Enroll-Response
    /// <li>Auto-Enroll-Request
    /// </ul>
    /// <p>
    /// IAS Zone clients SHALL support either:
    /// <ul>
    /// <li>Trip-to-pair AND Auto-Enroll-Response, OR
    /// <li>Auto-Enroll-Request
    /// </ul>
    /// <p>
    /// An IAS Zone client MAY support all enrollment methods.The Trip-to-Pair enrollment method is primarily intended to be
    /// used when there is a desire for an explicit enrollment method (e.g., when a GUI wizard or other commissioning tool is
    /// used by a user or installer to add multiple IAS Zone servers in an orderly fashion, assign names to them, configure
    /// them in the system).
    /// <p>
    /// The detailed requirements for each commissioning method follow:
    /// <p>
    /// <b>Trip-to-Pair</b>
    /// <ol>
    /// <li>After an IAS Zone server is commissioned to a network, the IAS CIE MAY perform service discovery.
    /// <li>If the IAS CIE determines it wants to enroll the IAS Zone server, it SHALL send a Write Attribute command on the
    /// IAS Zone server’s IAS_CIE_Address attribute with its IEEE address
    /// <li>The IAS Zone server MAY configure a binding table entry for the IAS CIE’s address because all of its
    /// communication will be directed to the IAS CIE.
    /// <li>Upon a user input determined by the manufacturer (e.g., a button, change to device’s ZoneStatus attribute that
    /// would result in a Zone Status Change Notification command) and the IAS Zone server’s ZoneState attribute equal to
    /// 0x00 (unenrolled), the IAS Zone server SHALL send a Zone Enroll Request command.
    /// <li>The IAS CIE SHALL send a Zone Enroll Response command, which assigns the IAS Zone server’s ZoneID attribute.
    /// <li>The IAS Zone server SHALL change its ZoneState attribute to 0x01 (enrolled).
    /// </ol>
    /// <p>
    /// <b>Auto-Enroll-Response</b>
    /// <ol>
    /// <li>After an IAS Zone server is commissioned to a network, the IAS CIE MAY perform service discovery.
    /// <li>If the IAS CIE determines it wants to enroll the IAS Zone server, it SHALL send a Write Attribute command
    /// on the IAS Zone server’s CIE_IAS_Address attribute with its IEEE address.
    /// <li>The IAS Zone server MAY configure a binding table entry for the IAS CIE’s address because all of its
    /// communication will be directed to the IAS CIE.
    /// <li>The IAS CIE SHALL send a Zone Enroll Response, which assigns the IAS Zone server’s ZoneID attribute.
    /// <li>The IAS Zone server SHALL change its ZoneState attribute to 0x01 (enrolled).
    /// </ol>
    /// <p>
    /// <b>Auto-Enroll-Request</b>
    /// <ol>
    /// <li>After an IAS Zone server is commissioned to a network, the IAS CIE MAY perform service discovery.
    /// <li>If the IAS CIE determines it wants to enroll the IAS Zone server, it SHALL send a Write Attribute command on the
    /// IAS Zone server’s IAS_CIE_Address attribute with its IEEE address.
    /// <li>The IAS Zone server MAY configure a binding table entry for the IAS CIE’s address because all of its
    /// communication will be directed to the IAS CIE.
    /// <li>The IAS Zone server SHALL send a Zone Enroll Request command.
    /// <li>The IAS CIE SHALL send a Zone Enroll Response command, which assigns the IAS Zone server’s ZoneID attribute.
    /// <li>The IAS Zone server SHALL change its ZoneState attribute to 0x01 (enrolled).
    /// </ol>
    /// <p>    /// </summary>
    public class ZclIasZoneClient : IZigBeeApplication
    {
        /// <summary>
        /// The default number of milliseconds to wait for a <see cref="ZoneEnrollRequestCommand"/>
        /// before sending the <see cref="ZoneEnrollResponse"/>
        /// </summary>
        private const int DEFAULT_AUTO_ENROLL_DELAY = 2000;


        /// <summary>
        /// The <see cref="ZigBeeNetworkManager>
        /// </summary>
        private ZigBeeNetworkManager _networkManager;

        /// <summary>
        /// The IAS cluster to which we're bound
        /// </summary>
        private ZclIasZoneCluster _iasZoneCluster;

        /// <summary>
        /// The <see cref="IeeeAddress"/> of the CIE server. Normally this should be the local address of the coordinator.
        /// </summary>
        private IeeeAddress _ieeeAddress;

        /// <summary>
        /// The time to wait for a <see cref="ZoneEnrollRequestCommand"/> before sending the <see cref="ZoneEnrollResponse"/>
        /// </summary>
        public int AutoEnrollDelay { get; set; }  = DEFAULT_AUTO_ENROLL_DELAY;

        /// <summary>
        /// The cancellation token for the auto enrollment task being run
        /// </summary>
        private CancellationTokenSource _autoEnrollmentCancellationToken;

        /// <summary>
        /// The IAS zone ID for this device
        /// </summary>
        public byte ZoneId { get; private set; }

        /// <summary>
        /// The zone type reported by the remote device during enrollment
        /// </summary>
        public ushort? ZoneType { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="networkManager">the  <see cref="ZigBeeNetworkManager"/> this belongs to</param>
        /// <param name="ieeeAddress"> <see cref="IeeeAddress"/> of the CIE</param>
        /// <param name="zoneId">the zone ID to use for this device</param>
        public ZclIasZoneClient(ZigBeeNetworkManager networkManager, IeeeAddress ieeeAddress, byte zoneId) 
        {
            _networkManager = networkManager;
            _ieeeAddress = ieeeAddress;
            ZoneId = zoneId;
        }


        /**
         * Gets the IAS zone type for this device. This is provided by the remote device during enrollment.
         *
         * @return the IAS zone type as {@link ZoneTypeEnum} or null if the zone type is unknown
         */
        //public ZoneTypeEnum getZoneType() {
        //    if (zoneType == null) {
        //        return null;
        //    }
        //    return ZoneTypeEnum.getByValue(zoneType);
        //}


        public ZigBeeStatus AppStartup(ZclCluster cluster) 
        {
            _iasZoneCluster = (ZclIasZoneCluster) cluster;

            Task.Run(Initialise).GetAwaiter().GetResult();

            return ZigBeeStatus.SUCCESS;
        }

        private async Task Initialise() 
        {
            byte? currentState = (byte?) await _iasZoneCluster.GetAttribute(ZclIasZoneCluster.ATTR_ZONESTATE).ReadValue(long.MaxValue);
            if (currentState.HasValue) 
            {
                ZoneStateEnum currentStateEnum = (ZoneStateEnum)currentState;
                Log.Debug("{Address}: IAS CIE state is currently {StateEnum}[{State}]", _iasZoneCluster.GetZigBeeAddress(), currentStateEnum, currentState);
                if (currentStateEnum == ZoneStateEnum.ENROLLED) 
                {
                    Log.Debug("{Address}: IAS CIE is already enrolled", _iasZoneCluster.GetZigBeeAddress());
                    return;
                }
            } 
            else 
            {
                Log.Debug("{Address}: IAS CIE failed to get state", _iasZoneCluster.GetZigBeeAddress());
            }

            ZclAttribute cieAddressAttribute = _iasZoneCluster.GetAttribute(ZclIasZoneCluster.ATTR_IASCIEADDRESS);
            IeeeAddress currentIeeeAddress = (IeeeAddress) await cieAddressAttribute.ReadValue(0);
            Log.Debug("{Address}: IAS CIE address is currently {Address2}", _iasZoneCluster.GetZigBeeAddress(), currentIeeeAddress);

            if (!_ieeeAddress.Equals(currentIeeeAddress)) 
            {
                // Set the CIE address in the remote device. This is where the device will send its reports.
                await cieAddressAttribute.WriteValue(_ieeeAddress);

                currentIeeeAddress = (IeeeAddress) await cieAddressAttribute.ReadValue(0);
                if (_ieeeAddress.Equals(currentIeeeAddress)) 
                {
                    Log.Debug("{Address}: IAS CIE address is confirmed {Address2}", _iasZoneCluster.GetZigBeeAddress(), currentIeeeAddress);
                } 
                else 
                {
                    Log.Warning("{Address}: IAS CIE address is NOT confirmed {Address2}", _iasZoneCluster.GetZigBeeAddress(), currentIeeeAddress);
                }
            }

            byte? currentZone = (byte?) await _iasZoneCluster.GetAttribute(ZclIasZoneCluster.ATTR_ZONEID).ReadValue(0);
            if (currentZone == null) 
            {
                Log.Debug("{Address}: IAS CIE zone ID request failed", _iasZoneCluster.GetZigBeeAddress());
            } 
            else 
            {
                Log.Debug("{Address}: IAS CIE zone ID is currently {ZoneId}", _iasZoneCluster.GetZigBeeAddress(), currentZone);
            }

            ZoneType = (ushort?) await _iasZoneCluster.GetAttribute(ZclIasZoneCluster.ATTR_ZONETYPE).ReadValue(long.MaxValue);
            if (ZoneType == null) 
            {
                Log.Debug("{Address}: IAS CIE zone type request failed", _iasZoneCluster.GetZigBeeAddress());
            } 
            else 
            {
                Log.Debug("{Address}: IAS CIE zone type is {ZoneTypeEnum} ({ZoneTypeValue})", _iasZoneCluster.GetZigBeeAddress(), ((ZoneTypeEnum)ZoneType), ZoneType.Value.ToString("X2"));
            }

            // Start the auto-enroll timer
            _autoEnrollmentCancellationToken = new CancellationTokenSource();
            _ = Task.Run(RunAutoEnrollmentTask, _autoEnrollmentCancellationToken.Token);
        }

        private async Task RunAutoEnrollmentTask()
        {
            try
            {
                await Task.Delay(AutoEnrollDelay, _autoEnrollmentCancellationToken.Token);
                await _iasZoneCluster.ZoneEnrollResponse((byte)IasEnrollResponseCodeEnum.SUCCESS, ZoneId);
            }
            catch(OperationCanceledException)
            {
                //task has been cancelled
            }
            catch(Exception ex)
            {
                Log.Warning("{Address}: IAS CIE Exception in RunAutoEnrollmentTask {Exception}", _iasZoneCluster.GetZigBeeAddress(), ex.Message);
            }
        }

        public void AppShutdown() 
        {
            if (_autoEnrollmentCancellationToken != null) 
            {
                _autoEnrollmentCancellationToken.Cancel();
            }
        }

        public int GetClusterId() 
        {
            return ZclIasZoneCluster.CLUSTER_ID;
        }

        /// <summary>
        ///  Handle the received <see cref="ZoneEnrollRequestCommand"/> and send the <see cref="ZoneEnrollResponse"/>. 
        ///  This will register the zone number specified in the constructor <see cref="ZigBeeIasCieExtension"/>.
        /// </summary>
        /// <param name="command">the received <see cref="ZoneEnrollRequestCommand"/></param>
        /// <returns></returns>
        private bool HandleZoneEnrollRequestCommand(ZoneEnrollRequestCommand command) 
        {
            if (_autoEnrollmentCancellationToken != null) 
            {
                _autoEnrollmentCancellationToken.Cancel();
            }

            ZoneType = command.ZoneType;
            _iasZoneCluster.ZoneEnrollResponse((byte)IasEnrollResponseCodeEnum.SUCCESS, ZoneId);
            return true;
        }


        public void CommandReceived(ZigBeeCommand command)
        {
            if (command is ZoneEnrollRequestCommand) 
            {
                HandleZoneEnrollRequestCommand((ZoneEnrollRequestCommand) command);
            }
        }

    }
}
