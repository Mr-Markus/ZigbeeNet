using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Command;

namespace ZigBeeNet.ZDO
{


    public class ZdoCommandType
    {
        private static Dictionary<ushort, ZdoCommandType> _idCommandTypeMapping;

        public ushort ClusterId { get; private set; }

        public ZdoCommandType.CommandType Type { get; private set; }


        private ZdoCommandType(ushort clusterId, ZdoCommandType.CommandType commandType)
        {
            this.ClusterId = clusterId;
            this.Type = commandType;
        }

        static ZdoCommandType()
        {
            _idCommandTypeMapping = new Dictionary<ushort, ZdoCommandType>
            {
                { 0x0005, new ZdoCommandType(0x0005, ZdoCommandType.CommandType.ACTIVE_ENDPOINTS_REQUEST) },
                { 0x8005, new ZdoCommandType(0x8005, ZdoCommandType.CommandType.ACTIVE_ENDPOINTS_RESPONSE) },
                { 0x0019, new ZdoCommandType(0x0019, ZdoCommandType.CommandType.ACTIVE_ENDPOINT_STORE_REQUEST) },
                { 0x8019, new ZdoCommandType(0x8019, ZdoCommandType.CommandType.ACTIVE_ENDPOINT_STORE_RESPONSE) },
                { 0x0027, new ZdoCommandType(0x0027, ZdoCommandType.CommandType.BACKUP_BIND_TABLE_REQUEST) },
                { 0x8027, new ZdoCommandType(0x8027, ZdoCommandType.CommandType.BACKUP_BIND_TABLE_RESPONSE) },
                { 0x0029, new ZdoCommandType(0x0029, ZdoCommandType.CommandType.BACKUP_SOURCE_BIND_REQUEST) },
                { 0x0023, new ZdoCommandType(0x0023, ZdoCommandType.CommandType.BIND_REGISTER) },
                { 0x8023, new ZdoCommandType(0x8023, ZdoCommandType.CommandType.BIND_REGISTER_RESPONSE) },
                { 0x0021, new ZdoCommandType(0x0021, ZdoCommandType.CommandType.BIND_REQUEST) },
                { 0x8021, new ZdoCommandType(0x8021, ZdoCommandType.CommandType.BIND_RESPONSE) },
                { 0x0037, new ZdoCommandType(0x0037, ZdoCommandType.CommandType.CACHE_REQUEST) },
                { 0x0010, new ZdoCommandType(0x0010, ZdoCommandType.CommandType.COMPLEX_DESCRIPTOR_REQUEST) },
                { 0x8010, new ZdoCommandType(0x8010, ZdoCommandType.CommandType.COMPLEX_DESCRIPTOR_RESPONSE) },
                { 0x0013, new ZdoCommandType(0x0013, ZdoCommandType.CommandType.DEVICE_ANNOUNCE) },
                { 0x0012, new ZdoCommandType(0x0012, ZdoCommandType.CommandType.DISCOVERY_CACHE_REQUEST) },
                { 0x8012, new ZdoCommandType(0x8012, ZdoCommandType.CommandType.DISCOVERY_CACHE_RESPONSE) },
                { 0x0016, new ZdoCommandType(0x0016, ZdoCommandType.CommandType.DISCOVERY_STORE_REQUEST_REQUEST) },
                { 0x8016, new ZdoCommandType(0x8016, ZdoCommandType.CommandType.DISCOVERY_STORE_RESPONSE) },
                { 0x0020, new ZdoCommandType(0x0020, ZdoCommandType.CommandType.END_DEVICE_BIND_REQUEST) },
                { 0x8020, new ZdoCommandType(0x8020, ZdoCommandType.CommandType.END_DEVICE_BIND_RESPONSE) },
                { 0x001E, new ZdoCommandType(0x001E, ZdoCommandType.CommandType.EXTENDED_ACTIVE_ENDPOINT_REQUEST) },
                { 0x801E, new ZdoCommandType(0x801E, ZdoCommandType.CommandType.EXTENDED_ACTIVE_ENDPOINT_RESPONSE) },
                { 0x001D, new ZdoCommandType(0x001D, ZdoCommandType.CommandType.EXTENDED_SIMPLE_DESCRIPTOR_REQUEST) },
                { 0x801D, new ZdoCommandType(0x801D, ZdoCommandType.CommandType.EXTENDED_SIMPLE_DESCRIPTOR_RESPONSE) },
                { 0x001C, new ZdoCommandType(0x001C, ZdoCommandType.CommandType.FIND_NODE_CACHE_REQUEST) },
                { 0x801C, new ZdoCommandType(0x801C, ZdoCommandType.CommandType.FIND_NODE_CACHE_RESPONSE) },
                { 0x0001, new ZdoCommandType(0x0001, ZdoCommandType.CommandType.IEEE_ADDRESS_REQUEST) },
                { 0x8001, new ZdoCommandType(0x8001, ZdoCommandType.CommandType.IEEE_ADDRESS_RESPONSE) },
                { 0x0033, new ZdoCommandType(0x0033, ZdoCommandType.CommandType.MANAGEMENT_BIND_REQUEST) },
                { 0x8033, new ZdoCommandType(0x8033, ZdoCommandType.CommandType.MANAGEMENT_BIND_RESPONSE) },
                { 0x8037, new ZdoCommandType(0x8037, ZdoCommandType.CommandType.MANAGEMENT_CACHE_RESPONSE) },
                { 0x0035, new ZdoCommandType(0x0035, ZdoCommandType.CommandType.MANAGEMENT_DIRECT_JOIN_REQUEST) },
                { 0x8035, new ZdoCommandType(0x8035, ZdoCommandType.CommandType.MANAGEMENT_DIRECT_JOIN_RESPONSE) },
                { 0x0034, new ZdoCommandType(0x0034, ZdoCommandType.CommandType.MANAGEMENT_LEAVE_REQUEST) },
                { 0x8034, new ZdoCommandType(0x8034, ZdoCommandType.CommandType.MANAGEMENT_LEAVE_RESPONSE) },
                { 0x0031, new ZdoCommandType(0x0031, ZdoCommandType.CommandType.MANAGEMENT_LQI_REQUEST) },
                { 0x8031, new ZdoCommandType(0x8031, ZdoCommandType.CommandType.MANAGEMENT_LQI_RESPONSE) },
                { 0x0030, new ZdoCommandType(0x0030, ZdoCommandType.CommandType.MANAGEMENT_NETWORK_DISCOVERY) },
                { 0x8030, new ZdoCommandType(0x8030, ZdoCommandType.CommandType.MANAGEMENT_NETWORK_DISCOVERY_RESPONSE) },
                { 0x8038, new ZdoCommandType(0x8038, ZdoCommandType.CommandType.MANAGEMENT_NETWORK_UPDATE_NOTIFY) },
                { 0x0036, new ZdoCommandType(0x0036, ZdoCommandType.CommandType.MANAGEMENT_PERMIT_JOINING_REQUEST) },
                { 0x8036, new ZdoCommandType(0x8036, ZdoCommandType.CommandType.MANAGEMENT_PERMIT_JOINING_RESPONSE) },
                { 0x0032, new ZdoCommandType(0x0032, ZdoCommandType.CommandType.MANAGEMENT_ROUTING_REQUEST) },
                { 0x8032, new ZdoCommandType(0x8032, ZdoCommandType.CommandType.MANAGEMENT_ROUTING_RESPONSE) },
                { 0x0006, new ZdoCommandType(0x0006, ZdoCommandType.CommandType.MATCH_DESCRIPTOR_REQUEST) },
                { 0x8006, new ZdoCommandType(0x8006, ZdoCommandType.CommandType.MATCH_DESCRIPTOR_RESPONSE) },
                { 0x0000, new ZdoCommandType(0x0000, ZdoCommandType.CommandType.NETWORK_ADDRESS_REQUEST) },
                { 0x8000, new ZdoCommandType(0x8000, ZdoCommandType.CommandType.NETWORK_ADDRESS_RESPONSE) },
                { 0x0038, new ZdoCommandType(0x0038, ZdoCommandType.CommandType.NETWORK_UPDATE_REQUEST) },
                { 0x0002, new ZdoCommandType(0x0002, ZdoCommandType.CommandType.NODE_DESCRIPTOR_REQUEST) },
                { 0x8002, new ZdoCommandType(0x8002, ZdoCommandType.CommandType.NODE_DESCRIPTOR_RESPONSE) },
                { 0x0017, new ZdoCommandType(0x0017, ZdoCommandType.CommandType.NODE_DESCRIPTOR_STORE_REQUEST) },
                { 0x8017, new ZdoCommandType(0x8017, ZdoCommandType.CommandType.NODE_DESCRIPTOR_STORE_RESPONSE) },
                { 0x0003, new ZdoCommandType(0x0003, ZdoCommandType.CommandType.POWER_DESCRIPTOR_REQUEST) },
                { 0x8003, new ZdoCommandType(0x8003, ZdoCommandType.CommandType.POWER_DESCRIPTOR_RESPONSE) },
                { 0x0018, new ZdoCommandType(0x0018, ZdoCommandType.CommandType.POWER_DESCRIPTOR_STORE_REQUEST) },
                { 0x8018, new ZdoCommandType(0x8018, ZdoCommandType.CommandType.POWER_DESCRIPTOR_STORE_RESPONSE) },
                { 0x0028, new ZdoCommandType(0x0028, ZdoCommandType.CommandType.RECOVER_BIND_TABLE_REQUEST) },
                { 0x8028, new ZdoCommandType(0x8028, ZdoCommandType.CommandType.RECOVER_BIND_TABLE_RESPONSE) },
                { 0x002A, new ZdoCommandType(0x002A, ZdoCommandType.CommandType.RECOVER_SOURCE_BIND_REQUEST) },
                { 0x8029, new ZdoCommandType(0x8029, ZdoCommandType.CommandType.RECOVER_SOURCE_BIND_RESPONSE) },
                { 0x8026, new ZdoCommandType(0x8026, ZdoCommandType.CommandType.REMOVE_BACKUP_BIND_ENTRY_RESPONSE) },
                { 0x0026, new ZdoCommandType(0x0026, ZdoCommandType.CommandType.REMOVE_BACKUP_BIND_TABLE_REQUEST) },
                { 0x801B, new ZdoCommandType(0x801B, ZdoCommandType.CommandType.REMOVE_NODE_CACHE) },
                { 0x001B, new ZdoCommandType(0x001B, ZdoCommandType.CommandType.REMOVE_NODE_CACHE_REQUEST) },
                { 0x0024, new ZdoCommandType(0x0024, ZdoCommandType.CommandType.REPLACE_DEVICE_REQUEST) },
                { 0x8024, new ZdoCommandType(0x8024, ZdoCommandType.CommandType.REPLACE_DEVICE_RESPONSE) },
                { 0x0004, new ZdoCommandType(0x0004, ZdoCommandType.CommandType.SIMPLE_DESCRIPTOR_REQUEST) },
                { 0x8004, new ZdoCommandType(0x8004, ZdoCommandType.CommandType.SIMPLE_DESCRIPTOR_RESPONSE) },
                { 0x001A, new ZdoCommandType(0x001A, ZdoCommandType.CommandType.SIMPLE_DESCRIPTOR_STORE) },
                { 0x801A, new ZdoCommandType(0x801A, ZdoCommandType.CommandType.SIMPLE_DESCRIPTOR_STORE_RESPONSE) },
                { 0x0025, new ZdoCommandType(0x0025, ZdoCommandType.CommandType.STORE_BACKUP_BIND_ENTRY_REQUEST) },
                { 0x8025, new ZdoCommandType(0x8025, ZdoCommandType.CommandType.STORE_BACKUP_BIND_ENTRY_RESPONSE) },
                { 0x0015, new ZdoCommandType(0x0015, ZdoCommandType.CommandType.SYSTEM_SERVER_DISCOVERY_REQUEST) },
                { 0x0022, new ZdoCommandType(0x0022, ZdoCommandType.CommandType.UNBIND_REQUEST) },
                { 0x8022, new ZdoCommandType(0x8022, ZdoCommandType.CommandType.UNBIND_RESPONSE) },
                { 0x8014, new ZdoCommandType(0x8014, ZdoCommandType.CommandType.USER_DESCRIPTOR_CONF) },
                { 0x0011, new ZdoCommandType(0x0011, ZdoCommandType.CommandType.USER_DESCRIPTOR_REQUEST) },
                { 0x8011, new ZdoCommandType(0x8011, ZdoCommandType.CommandType.USER_DESCRIPTOR_RESPONSE) },
                { 0x0014, new ZdoCommandType(0x0014, ZdoCommandType.CommandType.USER_DESCRIPTOR_SET_REQUEST) },
            };
        }

        public static ZdoCommandType GetValueById(ushort clusterId)
        {
            ZdoCommandType commandType = null;

            _idCommandTypeMapping.TryGetValue(clusterId, out commandType);

            return commandType;

            //if (_idCommandTypeMapping.ContainsKey(clusterId))
            //{
            //    return _idCommandTypeMapping[clusterId];
            //}

            //return null;
        }

        public static ZdoCommandType GetValueByType(ZdoCommandType.CommandType commandType)
        {
            return GetValueById((ushort)commandType);
        }

        public ZdoCommand GetZdoCommand()
        {
            switch (Type)
            {
                case ZdoCommandType.CommandType.ACTIVE_ENDPOINTS_REQUEST:
                    return new ActiveEndpointsRequest();
                case ZdoCommandType.CommandType.ACTIVE_ENDPOINTS_RESPONSE:
                    return new ActiveEndpointsResponse();
                case ZdoCommandType.CommandType.ACTIVE_ENDPOINT_STORE_REQUEST:
                    return new ActiveEndpointStoreRequest();
                case ZdoCommandType.CommandType.ACTIVE_ENDPOINT_STORE_RESPONSE:
                    return new ActiveEndpointStoreResponse();
                case ZdoCommandType.CommandType.BACKUP_BIND_TABLE_REQUEST:
                    return new BackupBindTableRequest();
                case ZdoCommandType.CommandType.BACKUP_BIND_TABLE_RESPONSE:
                    return new BackupBindTableResponse();
                case ZdoCommandType.CommandType.BACKUP_SOURCE_BIND_REQUEST:
                    return new BackupSourceBindRequest();
                case ZdoCommandType.CommandType.BIND_REGISTER:
                    return new BindRegister();
                case ZdoCommandType.CommandType.BIND_REGISTER_RESPONSE:
                    return new BindRegisterResponse();
                case ZdoCommandType.CommandType.BIND_REQUEST:
                    return new BindRequest();
                case ZdoCommandType.CommandType.BIND_RESPONSE:
                    return new BindResponse();
                case ZdoCommandType.CommandType.CACHE_REQUEST:
                    return new CacheRequest();
                case ZdoCommandType.CommandType.COMPLEX_DESCRIPTOR_REQUEST:
                    return new ComplexDescriptorRequest();
                case ZdoCommandType.CommandType.COMPLEX_DESCRIPTOR_RESPONSE:
                    return new ComplexDescriptorResponse();
                case ZdoCommandType.CommandType.DEVICE_ANNOUNCE:
                    return new DeviceAnnounce();
                case ZdoCommandType.CommandType.DISCOVERY_CACHE_REQUEST:
                    return new DiscoveryCacheRequest();
                case ZdoCommandType.CommandType.DISCOVERY_CACHE_RESPONSE:
                    return new DiscoveryCacheResponse();
                case ZdoCommandType.CommandType.DISCOVERY_STORE_REQUEST_REQUEST:
                    return new DiscoveryStoreRequestRequest();
                case ZdoCommandType.CommandType.DISCOVERY_STORE_RESPONSE:
                    return new DiscoveryStoreResponse();
                case ZdoCommandType.CommandType.END_DEVICE_BIND_REQUEST:
                    return new EndDeviceBindRequest();
                case ZdoCommandType.CommandType.END_DEVICE_BIND_RESPONSE:
                    return new EndDeviceBindResponse();
                case ZdoCommandType.CommandType.EXTENDED_ACTIVE_ENDPOINT_REQUEST:
                    return new ExtendedActiveEndpointRequest();
                case ZdoCommandType.CommandType.EXTENDED_ACTIVE_ENDPOINT_RESPONSE:
                    return new ExtendedActiveEndpointResponse();
                case ZdoCommandType.CommandType.EXTENDED_SIMPLE_DESCRIPTOR_REQUEST:
                    return new ExtendedSimpleDescriptorRequest();
                case ZdoCommandType.CommandType.EXTENDED_SIMPLE_DESCRIPTOR_RESPONSE:
                    return new ExtendedSimpleDescriptorResponse();
                case ZdoCommandType.CommandType.FIND_NODE_CACHE_REQUEST:
                    return new FindNodeCacheRequest();
                case ZdoCommandType.CommandType.FIND_NODE_CACHE_RESPONSE:
                    return new FindNodeCacheResponse();
                case ZdoCommandType.CommandType.IEEE_ADDRESS_REQUEST:
                    return new IeeeAddressRequest();
                case ZdoCommandType.CommandType.IEEE_ADDRESS_RESPONSE:
                    return new IeeeAddressResponse();
                case ZdoCommandType.CommandType.MANAGEMENT_BIND_REQUEST:
                    return new ManagementBindRequest();
                case ZdoCommandType.CommandType.MANAGEMENT_BIND_RESPONSE:
                    return new ManagementBindResponse();
                case ZdoCommandType.CommandType.MANAGEMENT_CACHE_RESPONSE:
                    return new ManagementCacheResponse();
                case ZdoCommandType.CommandType.MANAGEMENT_DIRECT_JOIN_REQUEST:
                    return new ManagementDirectJoinRequest();
                case ZdoCommandType.CommandType.MANAGEMENT_DIRECT_JOIN_RESPONSE:
                    return new ManagementDirectJoinResponse();
                case ZdoCommandType.CommandType.MANAGEMENT_LEAVE_REQUEST:
                    return new ManagementLeaveRequest();
                case ZdoCommandType.CommandType.MANAGEMENT_LEAVE_RESPONSE:
                    return new ManagementLeaveResponse();
                case ZdoCommandType.CommandType.MANAGEMENT_LQI_REQUEST:
                    return new ManagementLqiRequest();
                case ZdoCommandType.CommandType.MANAGEMENT_LQI_RESPONSE:
                    return new ManagementLqiResponse();
                case ZdoCommandType.CommandType.MANAGEMENT_NETWORK_DISCOVERY:
                    return new ManagementNetworkDiscovery();
                case ZdoCommandType.CommandType.MANAGEMENT_NETWORK_DISCOVERY_RESPONSE:
                    return new ManagementNetworkDiscoveryResponse();
                case ZdoCommandType.CommandType.MANAGEMENT_NETWORK_UPDATE_NOTIFY:
                    return new ManagementNetworkUpdateNotify();
                case ZdoCommandType.CommandType.MANAGEMENT_PERMIT_JOINING_REQUEST:
                    return new ManagementPermitJoiningRequest();
                case ZdoCommandType.CommandType.MANAGEMENT_PERMIT_JOINING_RESPONSE:
                    return new ManagementPermitJoiningResponse();
                case ZdoCommandType.CommandType.MANAGEMENT_ROUTING_REQUEST:
                    return new ManagementRoutingRequest();
                case ZdoCommandType.CommandType.MANAGEMENT_ROUTING_RESPONSE:
                    return new ManagementRoutingResponse();
                case ZdoCommandType.CommandType.MATCH_DESCRIPTOR_REQUEST:
                    return new MatchDescriptorRequest();
                case ZdoCommandType.CommandType.MATCH_DESCRIPTOR_RESPONSE:
                    return new MatchDescriptorResponse();
                case ZdoCommandType.CommandType.NETWORK_ADDRESS_REQUEST:
                    return new NetworkAddressRequest();
                case ZdoCommandType.CommandType.NETWORK_ADDRESS_RESPONSE:
                    return new NetworkAddressResponse();
                case ZdoCommandType.CommandType.NETWORK_UPDATE_REQUEST:
                    return new NetworkUpdateRequest();
                case ZdoCommandType.CommandType.NODE_DESCRIPTOR_REQUEST:
                    return new NodeDescriptorRequest();
                case ZdoCommandType.CommandType.NODE_DESCRIPTOR_RESPONSE:
                    return new NodeDescriptorResponse();
                case ZdoCommandType.CommandType.NODE_DESCRIPTOR_STORE_REQUEST:
                    return new NodeDescriptorStoreRequest();
                case ZdoCommandType.CommandType.NODE_DESCRIPTOR_STORE_RESPONSE:
                    return new NodeDescriptorStoreResponse();
                case ZdoCommandType.CommandType.POWER_DESCRIPTOR_REQUEST:
                    return new PowerDescriptorRequest();
                case ZdoCommandType.CommandType.POWER_DESCRIPTOR_RESPONSE:
                    return new PowerDescriptorResponse();
                case ZdoCommandType.CommandType.POWER_DESCRIPTOR_STORE_REQUEST:
                    return new PowerDescriptorStoreRequest();
                case ZdoCommandType.CommandType.POWER_DESCRIPTOR_STORE_RESPONSE:
                    return new PowerDescriptorStoreResponse();
                case ZdoCommandType.CommandType.RECOVER_BIND_TABLE_REQUEST:
                    return new RecoverBindTableRequest();
                case ZdoCommandType.CommandType.RECOVER_BIND_TABLE_RESPONSE:
                    return new RecoverBindTableResponse();
                case ZdoCommandType.CommandType.RECOVER_SOURCE_BIND_REQUEST:
                    return new RecoverSourceBindRequest();
                case ZdoCommandType.CommandType.RECOVER_SOURCE_BIND_RESPONSE:
                    return new RecoverSourceBindResponse();
                case ZdoCommandType.CommandType.REMOVE_BACKUP_BIND_ENTRY_RESPONSE:
                    return new RemoveBackupBindEntryResponse();
                case ZdoCommandType.CommandType.REMOVE_BACKUP_BIND_TABLE_REQUEST:
                    return new RemoveBackupBindTableRequest();
                case ZdoCommandType.CommandType.REMOVE_NODE_CACHE:
                    return new RemoveNodeCache();
                case ZdoCommandType.CommandType.REMOVE_NODE_CACHE_REQUEST:
                    return new RemoveNodeCacheRequest();
                case ZdoCommandType.CommandType.REPLACE_DEVICE_REQUEST:
                    return new ReplaceDeviceRequest();
                case ZdoCommandType.CommandType.REPLACE_DEVICE_RESPONSE:
                    return new ReplaceDeviceResponse();
                case ZdoCommandType.CommandType.SIMPLE_DESCRIPTOR_REQUEST:
                    return new SimpleDescriptorRequest();
                case ZdoCommandType.CommandType.SIMPLE_DESCRIPTOR_RESPONSE:
                    return new SimpleDescriptorResponse();
                case ZdoCommandType.CommandType.SIMPLE_DESCRIPTOR_STORE:
                    return new SimpleDescriptorStore();
                case ZdoCommandType.CommandType.SIMPLE_DESCRIPTOR_STORE_RESPONSE:
                    return new SimpleDescriptorStoreResponse();
                case ZdoCommandType.CommandType.STORE_BACKUP_BIND_ENTRY_REQUEST:
                    return new StoreBackupBindEntryRequest();
                case ZdoCommandType.CommandType.STORE_BACKUP_BIND_ENTRY_RESPONSE:
                    return new StoreBackupBindEntryResponse();
                case ZdoCommandType.CommandType.SYSTEM_SERVER_DISCOVERY_REQUEST:
                    return new SystemServerDiscoveryRequest();
                case ZdoCommandType.CommandType.UNBIND_REQUEST:
                    return new UnbindRequest();
                case ZdoCommandType.CommandType.UNBIND_RESPONSE:
                    return new UnbindResponse();
                case ZdoCommandType.CommandType.USER_DESCRIPTOR_CONF:
                    return new UserDescriptorConf();
                case ZdoCommandType.CommandType.USER_DESCRIPTOR_REQUEST:
                    return new UserDescriptorRequest();
                case ZdoCommandType.CommandType.USER_DESCRIPTOR_RESPONSE:
                    return new UserDescriptorResponse();
                case ZdoCommandType.CommandType.USER_DESCRIPTOR_SET_REQUEST:
                    return new UserDescriptorSetRequest();
                default:
                    throw new ArgumentException("Unknown ZdoCommandType: " + Type.ToString());
                    
            }
        }
        public enum CommandType : ushort
        {
            /**
             * Active Endpoints Request
             *
             * See {@link ActiveEndpointsRequest}
             */
            ACTIVE_ENDPOINTS_REQUEST = 0x0005,

            /**
            * Active Endpoints Response
            *
            * See {@link ActiveEndpointsResponse}
            */
            ACTIVE_ENDPOINTS_RESPONSE = 0x8005,

            /**
            * Active Endpoint Store Request
            *
            * See {@link ActiveEndpointStoreRequest}
            */
            ACTIVE_ENDPOINT_STORE_REQUEST = 0x0019,

            /**
            * Active Endpoint Store Response
            * 
            * See {@link ActiveEndpointStoreResponse}
            */
            ACTIVE_ENDPOINT_STORE_RESPONSE = 0x8019,

            /**
            * Backup Bind Table Request
            * 
            * See {@link BackupBindTableRequest}
            */
            BACKUP_BIND_TABLE_REQUEST = 0x0027,

            /**
            * Backup Bind Table Response
            * 
            * See {@link BackupBindTableResponse}
            */
            BACKUP_BIND_TABLE_RESPONSE = 0x8027,

            /**
            * Backup Source Bind Request
            * 
            * See {@link BackupSourceBindRequest}
            */
            BACKUP_SOURCE_BIND_REQUEST = 0x0029,

            /**
            * Bind Register
            * 
            * See {@link BindRegister}
            */
            BIND_REGISTER = 0x0023,

            /**
            * Bind Register Response
            * 
            * See {@link BindRegisterResponse}
            */
            BIND_REGISTER_RESPONSE = 0x8023,

            /**
            * Bind Request
            * 
            * See {@link BindRequest}
            */
            BIND_REQUEST = 0x0021,

            /**
            * Bind Response
            * 
            * See {@link BindResponse}
            */
            BIND_RESPONSE = 0x8021,

            /**
            * Cache Request
            * 
            * See {@link CacheRequest}
            */
            CACHE_REQUEST = 0x0037,

            /**
            * Complex Descriptor Request
            * 
            * See {@link ComplexDescriptorRequest}
            */
            COMPLEX_DESCRIPTOR_REQUEST = 0x0010,

            /**
            * Complex Descriptor Response
            * 
            * See {@link ComplexDescriptorResponse}
            */
            COMPLEX_DESCRIPTOR_RESPONSE = 0x8010,

            /**
            * Device Announce
            * 
            * See {@link DeviceAnnounce}
            */
            DEVICE_ANNOUNCE = 0x0013,

            /**
            * Discovery Cache Request
            * 
            * See {@link DiscoveryCacheRequest}
            */
            DISCOVERY_CACHE_REQUEST = 0x0012,

            /**
            * Discovery Cache Response
            * 
            * See {@link DiscoveryCacheResponse}
            */
            DISCOVERY_CACHE_RESPONSE = 0x8012,

            /**
            * Discovery Store Request Request
            * 
            * See {@link DiscoveryStoreRequestRequest}
            */
            DISCOVERY_STORE_REQUEST_REQUEST = 0x0016,

            /**
            * Discovery Store Response
            * 
            * See {@link DiscoveryStoreResponse}
            */
            DISCOVERY_STORE_RESPONSE = 0x8016,

            /**
            * End Device Bind Request
            * 
            * See {@link EndDeviceBindRequest}
            */
            END_DEVICE_BIND_REQUEST = 0x0020,

            /**
            * End Device Bind Response
            * 
            * See {@link EndDeviceBindResponse}
            */
            END_DEVICE_BIND_RESPONSE = 0x8020,

            /**
            * Extended Active Endpoint Request
            * 
            * See {@link ExtendedActiveEndpointRequest}
            */
            EXTENDED_ACTIVE_ENDPOINT_REQUEST = 0x001E,

            /**
            * Extended Active Endpoint Response
            * 
            * See {@link ExtendedActiveEndpointResponse}
            */
            EXTENDED_ACTIVE_ENDPOINT_RESPONSE = 0x801E,

            /**
            * Extended Simple Descriptor Request
            * 
            * See {@link ExtendedSimpleDescriptorRequest}
            */
            EXTENDED_SIMPLE_DESCRIPTOR_REQUEST = 0x001D,

            /**
            * Extended Simple Descriptor Response
            * 
            * See {@link ExtendedSimpleDescriptorResponse}
            */
            EXTENDED_SIMPLE_DESCRIPTOR_RESPONSE = 0x801D,

            /**
            * Find Node Cache Request
            * 
            * See {@link FindNodeCacheRequest}
            */
            FIND_NODE_CACHE_REQUEST = 0x001C,

            /**
            * Find Node Cache Response
            * 
            * See {@link FindNodeCacheResponse}
            */
            FIND_NODE_CACHE_RESPONSE = 0x801C,

            /**
            * IEEE Address Request
            * 
            * See {@link IeeeAddressRequest}
            */
            IEEE_ADDRESS_REQUEST = 0x0001,

            /**
            * IEEE Address Response
            * 
            * See {@link IeeeAddressResponse}
            */
            IEEE_ADDRESS_RESPONSE = 0x8001,

            /**
            * Management Bind Request
            * 
            * See {@link ManagementBindRequest}
            */
            MANAGEMENT_BIND_REQUEST = 0x0033,

            /**
            * Management Bind Response
            * 
            * See {@link ManagementBindResponse}
            */
            MANAGEMENT_BIND_RESPONSE = 0x8033,

            /**
            * Management Cache Response
            * 
            * See {@link ManagementCacheResponse}
            */
            MANAGEMENT_CACHE_RESPONSE = 0x8037,

            /**
            * Management Direct Join Request
            * 
            * See {@link ManagementDirectJoinRequest}
            */
            MANAGEMENT_DIRECT_JOIN_REQUEST = 0x0035,

            /**
            * Management Direct Join Response
            * 
            * See {@link ManagementDirectJoinResponse}
            */
            MANAGEMENT_DIRECT_JOIN_RESPONSE = 0x8035,

            /**
            * Management Leave Request
            * 
            * See {@link ManagementLeaveRequest}
            */
            MANAGEMENT_LEAVE_REQUEST = 0x0034,

            /**
            * Management Leave Response
            * 
            * See {@link ManagementLeaveResponse}
            */
            MANAGEMENT_LEAVE_RESPONSE = 0x8034,

            /**
            * Management LQI Request
            * 
            * See {@link ManagementLqiRequest}
            */
            MANAGEMENT_LQI_REQUEST = 0x0031,

            /**
            * Management LQI Response
            * 
            * See {@link ManagementLqiResponse}
            */
            MANAGEMENT_LQI_RESPONSE = 0x8031,

            /**
            * Management Network Discovery
            * 
            * See {@link ManagementNetworkDiscovery}
            */
            MANAGEMENT_NETWORK_DISCOVERY = 0x0030,

            /**
            * Management Network Discovery Response
            * 
            * See {@link ManagementNetworkDiscoveryResponse}
            */
            MANAGEMENT_NETWORK_DISCOVERY_RESPONSE = 0x8030,

            /**
            * Management Network Update Notify
            * 
            * See {@link ManagementNetworkUpdateNotify}
            */
            MANAGEMENT_NETWORK_UPDATE_NOTIFY = 0x8038,

            /**
            * Management Permit Joining Request
            * 
            * See {@link ManagementPermitJoiningRequest}
            */
            MANAGEMENT_PERMIT_JOINING_REQUEST = 0x0036,

            /**
            * Management Permit Joining Response
            * 
            * See {@link ManagementPermitJoiningResponse}
            */
            MANAGEMENT_PERMIT_JOINING_RESPONSE = 0x8036,

            /**
            * Management Routing Request
            * 
            * See {@link ManagementRoutingRequest}
            */
            MANAGEMENT_ROUTING_REQUEST = 0x0032,

            /**
            * Management Routing Response
            * 
            * See {@link ManagementRoutingResponse}
            */
            MANAGEMENT_ROUTING_RESPONSE = 0x8032,

            /**
            * Match Descriptor Request
            * 
            * See {@link MatchDescriptorRequest}
            */
            MATCH_DESCRIPTOR_REQUEST = 0x0006,

            /**
            * Match Descriptor Response
            * 
            * See {@link MatchDescriptorResponse}
            */
            MATCH_DESCRIPTOR_RESPONSE = 0x8006,

            /**
            * Network Address Request
            * 
            * See {@link NetworkAddressRequest}
            */
            NETWORK_ADDRESS_REQUEST = 0x0000,

            /**
            * Network Address Response
            * 
            * See {@link NetworkAddressResponse}
            */
            NETWORK_ADDRESS_RESPONSE = 0x8000,

            /**
            * Network Update Request
            * 
            * See {@link NetworkUpdateRequest}
            */
            NETWORK_UPDATE_REQUEST = 0x0038,

            /**
            * Node Descriptor Request
            * 
            * See {@link NodeDescriptorRequest}
            */
            NODE_DESCRIPTOR_REQUEST = 0x0002,

            /**
            * Node Descriptor Response
            * 
            * See {@link NodeDescriptorResponse}
            */
            NODE_DESCRIPTOR_RESPONSE = 0x8002,

            /**
            * Node Descriptor Store Request
            * 
            * See {@link NodeDescriptorStoreRequest}
            */
            NODE_DESCRIPTOR_STORE_REQUEST = 0x0017,

            /**
            * Node Descriptor Store Response
            * 
            * See {@link NodeDescriptorStoreResponse}
            */
            NODE_DESCRIPTOR_STORE_RESPONSE = 0x8017,

            /**
            * Power Descriptor Request
            * 
            * See {@link PowerDescriptorRequest}
            */
            POWER_DESCRIPTOR_REQUEST = 0x0003,

            /**
            * Power Descriptor Response
            * 
            * See {@link PowerDescriptorResponse}
            */
            POWER_DESCRIPTOR_RESPONSE = 0x8003,

            /**
            * Power Descriptor Store Request
            * 
            * See {@link PowerDescriptorStoreRequest}
            */
            POWER_DESCRIPTOR_STORE_REQUEST = 0x0018,

            /**
            * Power Descriptor Store Response
            * 
            * See {@link PowerDescriptorStoreResponse}
            */
            POWER_DESCRIPTOR_STORE_RESPONSE = 0x8018,

            /**
            * Recover Bind Table Request
            * 
            * See {@link RecoverBindTableRequest}
            */
            RECOVER_BIND_TABLE_REQUEST = 0x0028,

            /**
            * Recover Bind Table Response
            * 
            * See {@link RecoverBindTableResponse}
            */
            RECOVER_BIND_TABLE_RESPONSE = 0x8028,

            /**
            * Recover Source Bind Request
            * 
            * See {@link RecoverSourceBindRequest}
            */
            RECOVER_SOURCE_BIND_REQUEST = 0x002A,

            /**
            * Recover Source Bind Response
            * 
            * See {@link RecoverSourceBindResponse}
            */
            RECOVER_SOURCE_BIND_RESPONSE = 0x8029,

            /**
            * Remove Backup Bind Entry Response
            * 
            * See {@link RemoveBackupBindEntryResponse}
            */
            REMOVE_BACKUP_BIND_ENTRY_RESPONSE = 0x8026,

            /**
            * Remove Backup Bind Table Request
            * 
            * See {@link RemoveBackupBindTableRequest}
            */
            REMOVE_BACKUP_BIND_TABLE_REQUEST = 0x0026,

            /**
            * Remove Node Cache
            * 
            * See {@link RemoveNodeCache}
            */
            REMOVE_NODE_CACHE = 0x801B,

            /**
            * Remove Node Cache Request
            * 
            * See {@link RemoveNodeCacheRequest}
            */
            REMOVE_NODE_CACHE_REQUEST = 0x001B,

            /**
            * Replace Device Request
            * 
            * See {@link ReplaceDeviceRequest}
            */
            REPLACE_DEVICE_REQUEST = 0x0024,

            /**
            * Replace Device Response
            * 
            * See {@link ReplaceDeviceResponse}
            */
            REPLACE_DEVICE_RESPONSE = 0x8024,

            /**
            * Simple Descriptor Request
            * 
            * See {@link SimpleDescriptorRequest}
            */
            SIMPLE_DESCRIPTOR_REQUEST = 0x0004,

            /**
            * Simple Descriptor Response
            * 
            * See {@link SimpleDescriptorResponse}
            */
            SIMPLE_DESCRIPTOR_RESPONSE = 0x8004,

            /**
            * Simple Descriptor Store
            * 
            * See {@link SimpleDescriptorStore}
            */
            SIMPLE_DESCRIPTOR_STORE = 0x001A,

            /**
            * Simple Descriptor Store Response
            * 
            * See {@link SimpleDescriptorStoreResponse}
            */
            SIMPLE_DESCRIPTOR_STORE_RESPONSE = 0x801A,

            /**
            * Store Backup Bind Entry Request
            * 
            * See {@link StoreBackupBindEntryRequest}
            */
            STORE_BACKUP_BIND_ENTRY_REQUEST = 0x0025,

            /**
            * Store Backup Bind Entry Response
            * 
            * See {@link StoreBackupBindEntryResponse}
            */
            STORE_BACKUP_BIND_ENTRY_RESPONSE = 0x8025,

            /**
            * System Server Discovery Request
            * 
            * See {@link SystemServerDiscoveryRequest}
            */
            SYSTEM_SERVER_DISCOVERY_REQUEST = 0x0015,

            /**
            * Unbind Request
            * 
            * See {@link UnbindRequest}
            */
            UNBIND_REQUEST = 0x0022,

            /**
            * Unbind Response
            * 
            * See {@link UnbindResponse}
            */
            UNBIND_RESPONSE = 0x8022,

            /**
            * User Descriptor Conf
            * 
            * See {@link UserDescriptorConf}
            */
            USER_DESCRIPTOR_CONF = 0x8014,

            /**
            * User Descriptor Request
            * 
            * See {@link UserDescriptorRequest}
            */
            USER_DESCRIPTOR_REQUEST = 0x0011,

            /**
            * User Descriptor Response
            * 
            * See {@link UserDescriptorResponse}
            */
            USER_DESCRIPTOR_RESPONSE = 0x8011,

            /**
            * User Descriptor Set Request
            * 
            * See {@link UserDescriptorSetRequest}
            */
            USER_DESCRIPTOR_SET_REQUEST = 0x0014,
        }

    }

}

