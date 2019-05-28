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
            /// <summary>
             /// Active Endpoints Request
             ///
             /// See <see cref="ActiveEndpointsRequest">
             /// </summary>
            ACTIVE_ENDPOINTS_REQUEST = 0x0005,

            /// <summary>
            /// Active Endpoints Response
            ///
            /// See <see cref="ActiveEndpointsResponse">
            /// </summary>
            ACTIVE_ENDPOINTS_RESPONSE = 0x8005,

            /// <summary>
            /// Active Endpoint Store Request
            ///
            /// See <see cref="ActiveEndpointStoreRequest">
            /// </summary>
            ACTIVE_ENDPOINT_STORE_REQUEST = 0x0019,

            /// <summary>
            /// Active Endpoint Store Response
            /// 
            /// See <see cref="ActiveEndpointStoreResponse">
            /// </summary>
            ACTIVE_ENDPOINT_STORE_RESPONSE = 0x8019,

            /// <summary>
            /// Backup Bind Table Request
            /// 
            /// See <see cref="BackupBindTableRequest">
            /// </summary>
            BACKUP_BIND_TABLE_REQUEST = 0x0027,

            /// <summary>
            /// Backup Bind Table Response
            /// 
            /// See <see cref="BackupBindTableResponse">
            /// </summary>
            BACKUP_BIND_TABLE_RESPONSE = 0x8027,

            /// <summary>
            /// Backup Source Bind Request
            /// 
            /// See <see cref="BackupSourceBindRequest">
            /// </summary>
            BACKUP_SOURCE_BIND_REQUEST = 0x0029,

            /// <summary>
            /// Bind Register
            /// 
            /// See <see cref="BindRegister">
            /// </summary>
            BIND_REGISTER = 0x0023,

            /// <summary>
            /// Bind Register Response
            /// 
            /// See <see cref="BindRegisterResponse">
            /// </summary>
            BIND_REGISTER_RESPONSE = 0x8023,

            /// <summary>
            /// Bind Request
            /// 
            /// See <see cref="BindRequest">
            /// </summary>
            BIND_REQUEST = 0x0021,

            /// <summary>
            /// Bind Response
            /// 
            /// See <see cref="BindResponse">
            /// </summary>
            BIND_RESPONSE = 0x8021,

            /// <summary>
            /// Cache Request
            /// 
            /// See <see cref="CacheRequest">
            /// </summary>
            CACHE_REQUEST = 0x0037,

            /// <summary>
            /// Complex Descriptor Request
            /// 
            /// See <see cref="ComplexDescriptorRequest">
            /// </summary>
            COMPLEX_DESCRIPTOR_REQUEST = 0x0010,

            /// <summary>
            /// Complex Descriptor Response
            /// 
            /// See <see cref="ComplexDescriptorResponse">
            /// </summary>
            COMPLEX_DESCRIPTOR_RESPONSE = 0x8010,

            /// <summary>
            /// Device Announce
            /// 
            /// See <see cref="DeviceAnnounce">
            /// </summary>
            DEVICE_ANNOUNCE = 0x0013,

            /// <summary>
            /// Discovery Cache Request
            /// 
            /// See <see cref="DiscoveryCacheRequest">
            /// </summary>
            DISCOVERY_CACHE_REQUEST = 0x0012,

            /// <summary>
            /// Discovery Cache Response
            /// 
            /// See <see cref="DiscoveryCacheResponse">
            /// </summary>
            DISCOVERY_CACHE_RESPONSE = 0x8012,

            /// <summary>
            /// Discovery Store Request Request
            /// 
            /// See <see cref="DiscoveryStoreRequestRequest">
            /// </summary>
            DISCOVERY_STORE_REQUEST_REQUEST = 0x0016,

            /// <summary>
            /// Discovery Store Response
            /// 
            /// See <see cref="DiscoveryStoreResponse">
            /// </summary>
            DISCOVERY_STORE_RESPONSE = 0x8016,

            /// <summary>
            /// End Device Bind Request
            /// 
            /// See <see cref="EndDeviceBindRequest">
            /// </summary>
            END_DEVICE_BIND_REQUEST = 0x0020,

            /// <summary>
            /// End Device Bind Response
            /// 
            /// See <see cref="EndDeviceBindResponse">
            /// </summary>
            END_DEVICE_BIND_RESPONSE = 0x8020,

            /// <summary>
            /// Extended Active Endpoint Request
            /// 
            /// See <see cref="ExtendedActiveEndpointRequest">
            /// </summary>
            EXTENDED_ACTIVE_ENDPOINT_REQUEST = 0x001E,

            /// <summary>
            /// Extended Active Endpoint Response
            /// 
            /// See <see cref="ExtendedActiveEndpointResponse">
            /// </summary>
            EXTENDED_ACTIVE_ENDPOINT_RESPONSE = 0x801E,

            /// <summary>
            /// Extended Simple Descriptor Request
            /// 
            /// See <see cref="ExtendedSimpleDescriptorRequest">
            /// </summary>
            EXTENDED_SIMPLE_DESCRIPTOR_REQUEST = 0x001D,

            /// <summary>
            /// Extended Simple Descriptor Response
            /// 
            /// See <see cref="ExtendedSimpleDescriptorResponse">
            /// </summary>
            EXTENDED_SIMPLE_DESCRIPTOR_RESPONSE = 0x801D,

            /// <summary>
            /// Find Node Cache Request
            /// 
            /// See <see cref="FindNodeCacheRequest">
            /// </summary>
            FIND_NODE_CACHE_REQUEST = 0x001C,

            /// <summary>
            /// Find Node Cache Response
            /// 
            /// See <see cref="FindNodeCacheResponse">
            /// </summary>
            FIND_NODE_CACHE_RESPONSE = 0x801C,

            /// <summary>
            /// IEEE Address Request
            /// 
            /// See <see cref="IeeeAddressRequest">
            /// </summary>
            IEEE_ADDRESS_REQUEST = 0x0001,

            /// <summary>
            /// IEEE Address Response
            /// 
            /// See <see cref="IeeeAddressResponse">
            /// </summary>
            IEEE_ADDRESS_RESPONSE = 0x8001,

            /// <summary>
            /// Management Bind Request
            /// 
            /// See <see cref="ManagementBindRequest">
            /// </summary>
            MANAGEMENT_BIND_REQUEST = 0x0033,

            /// <summary>
            /// Management Bind Response
            /// 
            /// See <see cref="ManagementBindResponse">
            /// </summary>
            MANAGEMENT_BIND_RESPONSE = 0x8033,

            /// <summary>
            /// Management Cache Response
            /// 
            /// See <see cref="ManagementCacheResponse">
            /// </summary>
            MANAGEMENT_CACHE_RESPONSE = 0x8037,

            /// <summary>
            /// Management Direct Join Request
            /// 
            /// See <see cref="ManagementDirectJoinRequest">
            /// </summary>
            MANAGEMENT_DIRECT_JOIN_REQUEST = 0x0035,

            /// <summary>
            /// Management Direct Join Response
            /// 
            /// See <see cref="ManagementDirectJoinResponse">
            /// </summary>
            MANAGEMENT_DIRECT_JOIN_RESPONSE = 0x8035,

            /// <summary>
            /// Management Leave Request
            /// 
            /// See <see cref="ManagementLeaveRequest">
            /// </summary>
            MANAGEMENT_LEAVE_REQUEST = 0x0034,

            /// <summary>
            /// Management Leave Response
            /// 
            /// See <see cref="ManagementLeaveResponse">
            /// </summary>
            MANAGEMENT_LEAVE_RESPONSE = 0x8034,

            /// <summary>
            /// Management LQI Request
            /// 
            /// See <see cref="ManagementLqiRequest">
            /// </summary>
            MANAGEMENT_LQI_REQUEST = 0x0031,

            /// <summary>
            /// Management LQI Response
            /// 
            /// See <see cref="ManagementLqiResponse">
            /// </summary>
            MANAGEMENT_LQI_RESPONSE = 0x8031,

            /// <summary>
            /// Management Network Discovery
            /// 
            /// See <see cref="ManagementNetworkDiscovery">
            /// </summary>
            MANAGEMENT_NETWORK_DISCOVERY = 0x0030,

            /// <summary>
            /// Management Network Discovery Response
            /// 
            /// See <see cref="ManagementNetworkDiscoveryResponse">
            /// </summary>
            MANAGEMENT_NETWORK_DISCOVERY_RESPONSE = 0x8030,

            /// <summary>
            /// Management Network Update Notify
            /// 
            /// See <see cref="ManagementNetworkUpdateNotify">
            /// </summary>
            MANAGEMENT_NETWORK_UPDATE_NOTIFY = 0x8038,

            /// <summary>
            /// Management Permit Joining Request
            /// 
            /// See <see cref="ManagementPermitJoiningRequest">
            /// </summary>
            MANAGEMENT_PERMIT_JOINING_REQUEST = 0x0036,

            /// <summary>
            /// Management Permit Joining Response
            /// 
            /// See <see cref="ManagementPermitJoiningResponse">
            /// </summary>
            MANAGEMENT_PERMIT_JOINING_RESPONSE = 0x8036,

            /// <summary>
            /// Management Routing Request
            /// 
            /// See <see cref="ManagementRoutingRequest">
            /// </summary>
            MANAGEMENT_ROUTING_REQUEST = 0x0032,

            /// <summary>
            /// Management Routing Response
            /// 
            /// See <see cref="ManagementRoutingResponse">
            /// </summary>
            MANAGEMENT_ROUTING_RESPONSE = 0x8032,

            /// <summary>
            /// Match Descriptor Request
            /// 
            /// See <see cref="MatchDescriptorRequest">
            /// </summary>
            MATCH_DESCRIPTOR_REQUEST = 0x0006,

            /// <summary>
            /// Match Descriptor Response
            /// 
            /// See <see cref="MatchDescriptorResponse">
            /// </summary>
            MATCH_DESCRIPTOR_RESPONSE = 0x8006,

            /// <summary>
            /// Network Address Request
            /// 
            /// See <see cref="NetworkAddressRequest">
            /// </summary>
            NETWORK_ADDRESS_REQUEST = 0x0000,

            /// <summary>
            /// Network Address Response
            /// 
            /// See <see cref="NetworkAddressResponse">
            /// </summary>
            NETWORK_ADDRESS_RESPONSE = 0x8000,

            /// <summary>
            /// Network Update Request
            /// 
            /// See <see cref="NetworkUpdateRequest">
            /// </summary>
            NETWORK_UPDATE_REQUEST = 0x0038,

            /// <summary>
            /// Node Descriptor Request
            /// 
            /// See <see cref="NodeDescriptorRequest">
            /// </summary>
            NODE_DESCRIPTOR_REQUEST = 0x0002,

            /// <summary>
            /// Node Descriptor Response
            /// 
            /// See <see cref="NodeDescriptorResponse">
            /// </summary>
            NODE_DESCRIPTOR_RESPONSE = 0x8002,

            /// <summary>
            /// Node Descriptor Store Request
            /// 
            /// See <see cref="NodeDescriptorStoreRequest">
            /// </summary>
            NODE_DESCRIPTOR_STORE_REQUEST = 0x0017,

            /// <summary>
            /// Node Descriptor Store Response
            /// 
            /// See <see cref="NodeDescriptorStoreResponse">
            /// </summary>
            NODE_DESCRIPTOR_STORE_RESPONSE = 0x8017,

            /// <summary>
            /// Power Descriptor Request
            /// 
            /// See <see cref="PowerDescriptorRequest">
            /// </summary>
            POWER_DESCRIPTOR_REQUEST = 0x0003,

            /// <summary>
            /// Power Descriptor Response
            /// 
            /// See <see cref="PowerDescriptorResponse">
            /// </summary>
            POWER_DESCRIPTOR_RESPONSE = 0x8003,

            /// <summary>
            /// Power Descriptor Store Request
            /// 
            /// See <see cref="PowerDescriptorStoreRequest">
            /// </summary>
            POWER_DESCRIPTOR_STORE_REQUEST = 0x0018,

            /// <summary>
            /// Power Descriptor Store Response
            /// 
            /// See <see cref="PowerDescriptorStoreResponse">
            /// </summary>
            POWER_DESCRIPTOR_STORE_RESPONSE = 0x8018,

            /// <summary>
            /// Recover Bind Table Request
            /// 
            /// See <see cref="RecoverBindTableRequest">
            /// </summary>
            RECOVER_BIND_TABLE_REQUEST = 0x0028,

            /// <summary>
            /// Recover Bind Table Response
            /// 
            /// See <see cref="RecoverBindTableResponse">
            /// </summary>
            RECOVER_BIND_TABLE_RESPONSE = 0x8028,

            /// <summary>
            /// Recover Source Bind Request
            /// 
            /// See <see cref="RecoverSourceBindRequest">
            /// </summary>
            RECOVER_SOURCE_BIND_REQUEST = 0x002A,

            /// <summary>
            /// Recover Source Bind Response
            /// 
            /// See <see cref="RecoverSourceBindResponse">
            /// </summary>
            RECOVER_SOURCE_BIND_RESPONSE = 0x8029,

            /// <summary>
            /// Remove Backup Bind Entry Response
            /// 
            /// See <see cref="RemoveBackupBindEntryResponse">
            /// </summary>
            REMOVE_BACKUP_BIND_ENTRY_RESPONSE = 0x8026,

            /// <summary>
            /// Remove Backup Bind Table Request
            /// 
            /// See <see cref="RemoveBackupBindTableRequest">
            /// </summary>
            REMOVE_BACKUP_BIND_TABLE_REQUEST = 0x0026,

            /// <summary>
            /// Remove Node Cache
            /// 
            /// See <see cref="RemoveNodeCache">
            /// </summary>
            REMOVE_NODE_CACHE = 0x801B,

            /// <summary>
            /// Remove Node Cache Request
            /// 
            /// See <see cref="RemoveNodeCacheRequest">
            /// </summary>
            REMOVE_NODE_CACHE_REQUEST = 0x001B,

            /// <summary>
            /// Replace Device Request
            /// 
            /// See <see cref="ReplaceDeviceRequest">
            /// </summary>
            REPLACE_DEVICE_REQUEST = 0x0024,

            /// <summary>
            /// Replace Device Response
            /// 
            /// See <see cref="ReplaceDeviceResponse">
            /// </summary>
            REPLACE_DEVICE_RESPONSE = 0x8024,

            /// <summary>
            /// Simple Descriptor Request
            /// 
            /// See <see cref="SimpleDescriptorRequest">
            /// </summary>
            SIMPLE_DESCRIPTOR_REQUEST = 0x0004,

            /// <summary>
            /// Simple Descriptor Response
            /// 
            /// See <see cref="SimpleDescriptorResponse">
            /// </summary>
            SIMPLE_DESCRIPTOR_RESPONSE = 0x8004,

            /// <summary>
            /// Simple Descriptor Store
            /// 
            /// See <see cref="SimpleDescriptorStore">
            /// </summary>
            SIMPLE_DESCRIPTOR_STORE = 0x001A,

            /// <summary>
            /// Simple Descriptor Store Response
            /// 
            /// See <see cref="SimpleDescriptorStoreResponse">
            /// </summary>
            SIMPLE_DESCRIPTOR_STORE_RESPONSE = 0x801A,

            /// <summary>
            /// Store Backup Bind Entry Request
            /// 
            /// See <see cref="StoreBackupBindEntryRequest">
            /// </summary>
            STORE_BACKUP_BIND_ENTRY_REQUEST = 0x0025,

            /// <summary>
            /// Store Backup Bind Entry Response
            /// 
            /// See <see cref="StoreBackupBindEntryResponse">
            /// </summary>
            STORE_BACKUP_BIND_ENTRY_RESPONSE = 0x8025,

            /// <summary>
            /// System Server Discovery Request
            /// 
            /// See <see cref="SystemServerDiscoveryRequest">
            /// </summary>
            SYSTEM_SERVER_DISCOVERY_REQUEST = 0x0015,

            /// <summary>
            /// Unbind Request
            /// 
            /// See <see cref="UnbindRequest">
            /// </summary>
            UNBIND_REQUEST = 0x0022,

            /// <summary>
            /// Unbind Response
            /// 
            /// See <see cref="UnbindResponse">
            /// </summary>
            UNBIND_RESPONSE = 0x8022,

            /// <summary>
            /// User Descriptor Conf
            /// 
            /// See <see cref="UserDescriptorConf">
            /// </summary>
            USER_DESCRIPTOR_CONF = 0x8014,

            /// <summary>
            /// User Descriptor Request
            /// 
            /// See <see cref="UserDescriptorRequest">
            /// </summary>
            USER_DESCRIPTOR_REQUEST = 0x0011,

            /// <summary>
            /// User Descriptor Response
            /// 
            /// See <see cref="UserDescriptorResponse">
            /// </summary>
            USER_DESCRIPTOR_RESPONSE = 0x8011,

            /// <summary>
            /// User Descriptor Set Request
            /// 
            /// See <see cref="UserDescriptorSetRequest">
            /// </summary>
            USER_DESCRIPTOR_SET_REQUEST = 0x0014,
        }

    }

}

