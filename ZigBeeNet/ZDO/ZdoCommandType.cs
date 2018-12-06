﻿using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.ZDO;
using ZigBeeNet.ZDO.Command;

namespace ZigBeeNet.ZDO
{

    public enum CommandType
    {
        /**
         * Active Endpoints Request
         *
         * See {@link ActiveEndpointsRequest}
         */
        ACTIVE_ENDPOINTS_REQUEST,

        /**
        * Active Endpoints Response
        *
        * See {@link ActiveEndpointsResponse}
        */
        ACTIVE_ENDPOINTS_RESPONSE,

        /**
        * Active Endpoint Store Request
        *
        * See {@link ActiveEndpointStoreRequest}
        */
        ACTIVE_ENDPOINT_STORE_REQUEST,

        /**
        * Active Endpoint Store Response
        * 
        * See {@link ActiveEndpointStoreResponse}
        */
        ACTIVE_ENDPOINT_STORE_RESPONSE,

        /**
        * Backup Bind Table Request
        * 
        * See {@link BackupBindTableRequest}
        */
        BACKUP_BIND_TABLE_REQUEST,

        /**
        * Backup Bind Table Response
        * 
        * See {@link BackupBindTableResponse}
        */
        BACKUP_BIND_TABLE_RESPONSE,

        /**
        * Backup Source Bind Request
        * 
        * See {@link BackupSourceBindRequest}
        */
        BACKUP_SOURCE_BIND_REQUEST,

        /**
        * Bind Register
        * 
        * See {@link BindRegister}
        */
        BIND_REGISTER,

        /**
        * Bind Register Response
        * 
        * See {@link BindRegisterResponse}
        */
        BIND_REGISTER_RESPONSE,

        /**
        * Bind Request
        * 
        * See {@link BindRequest}
        */
        BIND_REQUEST,

        /**
        * Bind Response
        * 
        * See {@link BindResponse}
        */
        BIND_RESPONSE,

        /**
        * Cache Request
        * 
        * See {@link CacheRequest}
        */
        CACHE_REQUEST,

        /**
        * Complex Descriptor Request
        * 
        * See {@link ComplexDescriptorRequest}
        */
        COMPLEX_DESCRIPTOR_REQUEST,

        /**
        * Complex Descriptor Response
        * 
        * See {@link ComplexDescriptorResponse}
        */
        COMPLEX_DESCRIPTOR_RESPONSE,

        /**
        * Device Announce
        * 
        * See {@link DeviceAnnounce}
        */
        DEVICE_ANNOUNCE,

        /**
        * Discovery Cache Request
        * 
        * See {@link DiscoveryCacheRequest}
        */
        DISCOVERY_CACHE_REQUEST,

        /**
        * Discovery Cache Response
        * 
        * See {@link DiscoveryCacheResponse}
        */
        DISCOVERY_CACHE_RESPONSE,

        /**
        * Discovery Store Request Request
        * 
        * See {@link DiscoveryStoreRequestRequest}
        */
        DISCOVERY_STORE_REQUEST_REQUEST,

        /**
        * Discovery Store Response
        * 
        * See {@link DiscoveryStoreResponse}
        */
        DISCOVERY_STORE_RESPONSE,

        /**
        * End Device Bind Request
        * 
        * See {@link EndDeviceBindRequest}
        */
        END_DEVICE_BIND_REQUEST,

        /**
        * End Device Bind Response
        * 
        * See {@link EndDeviceBindResponse}
        */
        END_DEVICE_BIND_RESPONSE,

        /**
        * Extended Active Endpoint Request
        * 
        * See {@link ExtendedActiveEndpointRequest}
        */
        EXTENDED_ACTIVE_ENDPOINT_REQUEST,

        /**
        * Extended Active Endpoint Response
        * 
        * See {@link ExtendedActiveEndpointResponse}
        */
        EXTENDED_ACTIVE_ENDPOINT_RESPONSE,

        /**
        * Extended Simple Descriptor Request
        * 
        * See {@link ExtendedSimpleDescriptorRequest}
        */
        EXTENDED_SIMPLE_DESCRIPTOR_REQUEST,

        /**
        * Extended Simple Descriptor Response
        * 
        * See {@link ExtendedSimpleDescriptorResponse}
        */
        EXTENDED_SIMPLE_DESCRIPTOR_RESPONSE,

        /**
        * Find Node Cache Request
        * 
        * See {@link FindNodeCacheRequest}
        */
        FIND_NODE_CACHE_REQUEST,

        /**
        * Find Node Cache Response
        * 
        * See {@link FindNodeCacheResponse}
        */
        FIND_NODE_CACHE_RESPONSE,

        /**
        * IEEE Address Request
        * 
        * See {@link IeeeAddressRequest}
        */
        IEEE_ADDRESS_REQUEST,

        /**
        * IEEE Address Response
        * 
        * See {@link IeeeAddressResponse}
        */
        IEEE_ADDRESS_RESPONSE,

        /**
        * Management Bind Request
        * 
        * See {@link ManagementBindRequest}
        */
        MANAGEMENT_BIND_REQUEST,

        /**
        * Management Bind Response
        * 
        * See {@link ManagementBindResponse}
        */
        MANAGEMENT_BIND_RESPONSE,

        /**
        * Management Cache Response
        * 
        * See {@link ManagementCacheResponse}
        */
        MANAGEMENT_CACHE_RESPONSE,

        /**
        * Management Direct Join Request
        * 
        * See {@link ManagementDirectJoinRequest}
        */
        MANAGEMENT_DIRECT_JOIN_REQUEST,

        /**
        * Management Direct Join Response
        * 
        * See {@link ManagementDirectJoinResponse}
        */
        MANAGEMENT_DIRECT_JOIN_RESPONSE,

        /**
        * Management Leave Request
        * 
        * See {@link ManagementLeaveRequest}
        */
        MANAGEMENT_LEAVE_REQUEST,

        /**
        * Management Leave Response
        * 
        * See {@link ManagementLeaveResponse}
        */
        MANAGEMENT_LEAVE_RESPONSE,

        /**
        * Management LQI Request
        * 
        * See {@link ManagementLqiRequest}
        */
        MANAGEMENT_LQI_REQUEST,

        /**
        * Management LQI Response
        * 
        * See {@link ManagementLqiResponse}
        */
        MANAGEMENT_LQI_RESPONSE,

        /**
        * Management Network Discovery
        * 
        * See {@link ManagementNetworkDiscovery}
        */
        MANAGEMENT_NETWORK_DISCOVERY,

        /**
        * Management Network Discovery Response
        * 
        * See {@link ManagementNetworkDiscoveryResponse}
        */
        MANAGEMENT_NETWORK_DISCOVERY_RESPONSE,

        /**
        * Management Network Update Notify
        * 
        * See {@link ManagementNetworkUpdateNotify}
        */
        MANAGEMENT_NETWORK_UPDATE_NOTIFY,

        /**
        * Management Permit Joining Request
        * 
        * See {@link ManagementPermitJoiningRequest}
        */
        MANAGEMENT_PERMIT_JOINING_REQUEST,

        /**
        * Management Permit Joining Response
        * 
        * See {@link ManagementPermitJoiningResponse}
        */
        MANAGEMENT_PERMIT_JOINING_RESPONSE,

        /**
        * Management Routing Request
        * 
        * See {@link ManagementRoutingRequest}
        */
        MANAGEMENT_ROUTING_REQUEST,

        /**
        * Management Routing Response
        * 
        * See {@link ManagementRoutingResponse}
        */
        MANAGEMENT_ROUTING_RESPONSE,

        /**
        * Match Descriptor Request
        * 
        * See {@link MatchDescriptorRequest}
        */
        MATCH_DESCRIPTOR_REQUEST,

        /**
        * Match Descriptor Response
        * 
        * See {@link MatchDescriptorResponse}
        */
        MATCH_DESCRIPTOR_RESPONSE,

        /**
        * Network Address Request
        * 
        * See {@link NetworkAddressRequest}
        */
        NETWORK_ADDRESS_REQUEST,

        /**
        * Network Address Response
        * 
        * See {@link NetworkAddressResponse}
        */
        NETWORK_ADDRESS_RESPONSE,

        /**
        * Network Update Request
        * 
        * See {@link NetworkUpdateRequest}
        */
        NETWORK_UPDATE_REQUEST,

        /**
        * Node Descriptor Request
        * 
        * See {@link NodeDescriptorRequest}
        */
        NODE_DESCRIPTOR_REQUEST,

        /**
        * Node Descriptor Response
        * 
        * See {@link NodeDescriptorResponse}
        */
        NODE_DESCRIPTOR_RESPONSE,

        /**
        * Node Descriptor Store Request
        * 
        * See {@link NodeDescriptorStoreRequest}
        */
        NODE_DESCRIPTOR_STORE_REQUEST,

        /**
        * Node Descriptor Store Response
        * 
        * See {@link NodeDescriptorStoreResponse}
        */
        NODE_DESCRIPTOR_STORE_RESPONSE,

        /**
        * Power Descriptor Request
        * 
        * See {@link PowerDescriptorRequest}
        */
        POWER_DESCRIPTOR_REQUEST,

        /**
        * Power Descriptor Response
        * 
        * See {@link PowerDescriptorResponse}
        */
        POWER_DESCRIPTOR_RESPONSE,

        /**
        * Power Descriptor Store Request
        * 
        * See {@link PowerDescriptorStoreRequest}
        */
        POWER_DESCRIPTOR_STORE_REQUEST,

        /**
        * Power Descriptor Store Response
        * 
        * See {@link PowerDescriptorStoreResponse}
        */
        POWER_DESCRIPTOR_STORE_RESPONSE,

        /**
        * Recover Bind Table Request
        * 
        * See {@link RecoverBindTableRequest}
        */
        RECOVER_BIND_TABLE_REQUEST,

        /**
        * Recover Bind Table Response
        * 
        * See {@link RecoverBindTableResponse}
        */
        RECOVER_BIND_TABLE_RESPONSE,

        /**
        * Recover Source Bind Request
        * 
        * See {@link RecoverSourceBindRequest}
        */
        RECOVER_SOURCE_BIND_REQUEST,

        /**
        * Recover Source Bind Response
        * 
        * See {@link RecoverSourceBindResponse}
        */
        RECOVER_SOURCE_BIND_RESPONSE,

        /**
        * Remove Backup Bind Entry Response
        * 
        * See {@link RemoveBackupBindEntryResponse}
        */
        REMOVE_BACKUP_BIND_ENTRY_RESPONSE,

        /**
        * Remove Backup Bind Table Request
        * 
        * See {@link RemoveBackupBindTableRequest}
        */
        REMOVE_BACKUP_BIND_TABLE_REQUEST,

        /**
        * Remove Node Cache
        * 
        * See {@link RemoveNodeCache}
        */
        REMOVE_NODE_CACHE,

        /**
        * Remove Node Cache Request
        * 
        * See {@link RemoveNodeCacheRequest}
        */
        REMOVE_NODE_CACHE_REQUEST,

        /**
        * Replace Device Request
        * 
        * See {@link ReplaceDeviceRequest}
        */
        REPLACE_DEVICE_REQUEST,

        /**
        * Replace Device Response
        * 
        * See {@link ReplaceDeviceResponse}
        */
        REPLACE_DEVICE_RESPONSE,

        /**
        * Simple Descriptor Request
        * 
        * See {@link SimpleDescriptorRequest}
        */
        SIMPLE_DESCRIPTOR_REQUEST,

        /**
        * Simple Descriptor Response
        * 
        * See {@link SimpleDescriptorResponse}
        */
        SIMPLE_DESCRIPTOR_RESPONSE,

        /**
        * Simple Descriptor Store
        * 
        * See {@link SimpleDescriptorStore}
        */
        SIMPLE_DESCRIPTOR_STORE,

        /**
        * Simple Descriptor Store Response
        * 
        * See {@link SimpleDescriptorStoreResponse}
        */
        SIMPLE_DESCRIPTOR_STORE_RESPONSE,

        /**
        * Store Backup Bind Entry Request
        * 
        * See {@link StoreBackupBindEntryRequest}
        */
        STORE_BACKUP_BIND_ENTRY_REQUEST,

        /**
        * Store Backup Bind Entry Response
        * 
        * See {@link StoreBackupBindEntryResponse}
        */
        STORE_BACKUP_BIND_ENTRY_RESPONSE,

        /**
        * System Server Discovery Request
        * 
        * See {@link SystemServerDiscoveryRequest}
        */
        SYSTEM_SERVER_DISCOVERY_REQUEST,

        /**
        * Unbind Request
        * 
        * See {@link UnbindRequest}
        */
        UNBIND_REQUEST,

        /**
        * Unbind Response
        * 
        * See {@link UnbindResponse}
        */
        UNBIND_RESPONSE,

        /**
        * User Descriptor Conf
        * 
        * See {@link UserDescriptorConf}
        */
        USER_DESCRIPTOR_CONF,

        /**
        * User Descriptor Request
        * 
        * See {@link UserDescriptorRequest}
        */
        USER_DESCRIPTOR_REQUEST,

        /**
        * User Descriptor Response
        * 
        * See {@link UserDescriptorResponse}
        */
        USER_DESCRIPTOR_RESPONSE,

        /**
        * User Descriptor Set Request
        * 
        * See {@link UserDescriptorSetRequest}
        */
        USER_DESCRIPTOR_SET_REQUEST,
    }

    public class ZdoCommandType
    {
        private static Dictionary<int, ZdoCommandType> _idCommandTypeMapping;

        public int ClusterId { get; private set; }

        public CommandType CommandType { get; private set; }


        private ZdoCommandType(int clusterId, CommandType commandType)
        {
            this.ClusterId = clusterId;
            this.CommandType = commandType;
        }

        static ZdoCommandType()
        {
            _idCommandTypeMapping = new Dictionary<int, ZdoCommandType>
            {
                { 0x0005, new ZdoCommandType(0x0005, CommandType.ACTIVE_ENDPOINTS_REQUEST) },
                { 0x8005, new ZdoCommandType(0x8005, CommandType.ACTIVE_ENDPOINTS_RESPONSE) },
                { 0x0019, new ZdoCommandType(0x0019, CommandType.ACTIVE_ENDPOINT_STORE_REQUEST) },
                { 0x8019, new ZdoCommandType(0x8019, CommandType.ACTIVE_ENDPOINT_STORE_RESPONSE) },
                { 0x0027, new ZdoCommandType(0x0027, CommandType.BACKUP_BIND_TABLE_REQUEST) },
                { 0x8027, new ZdoCommandType(0x8027, CommandType.BACKUP_BIND_TABLE_RESPONSE) },
                { 0x0029, new ZdoCommandType(0x0029, CommandType.BACKUP_SOURCE_BIND_REQUEST) },
                { 0x0023, new ZdoCommandType(0x0023, CommandType.BIND_REGISTER) },
                { 0x8023, new ZdoCommandType(0x8023, CommandType.BIND_REGISTER_RESPONSE) },
                { 0x0021, new ZdoCommandType(0x0021, CommandType.BIND_REQUEST) },
                { 0x8021, new ZdoCommandType(0x8021, CommandType.BIND_RESPONSE) },
                { 0x0037, new ZdoCommandType(0x0037, CommandType.CACHE_REQUEST) },
                { 0x0010, new ZdoCommandType(0x0010, CommandType.COMPLEX_DESCRIPTOR_REQUEST) },
                { 0x8010, new ZdoCommandType(0x8010, CommandType.COMPLEX_DESCRIPTOR_RESPONSE) },
                { 0x0013, new ZdoCommandType(0x0013, CommandType.DEVICE_ANNOUNCE) },
                { 0x0012, new ZdoCommandType(0x0012, CommandType.DISCOVERY_CACHE_REQUEST) },
                { 0x8012, new ZdoCommandType(0x8012, CommandType.DISCOVERY_CACHE_RESPONSE) },
                { 0x0016, new ZdoCommandType(0x0016, CommandType.DISCOVERY_STORE_REQUEST_REQUEST) },
                { 0x8016, new ZdoCommandType(0x8016, CommandType.DISCOVERY_STORE_RESPONSE) },
                { 0x0020, new ZdoCommandType(0x0020, CommandType.END_DEVICE_BIND_REQUEST) },
                { 0x8020, new ZdoCommandType(0x8020, CommandType.END_DEVICE_BIND_RESPONSE) },
                { 0x001E, new ZdoCommandType(0x001E, CommandType.EXTENDED_ACTIVE_ENDPOINT_REQUEST) },
                { 0x801E, new ZdoCommandType(0x801E, CommandType.EXTENDED_ACTIVE_ENDPOINT_RESPONSE) },
                { 0x001D, new ZdoCommandType(0x001D, CommandType.EXTENDED_SIMPLE_DESCRIPTOR_REQUEST) },
                { 0x801D, new ZdoCommandType(0x801D, CommandType.EXTENDED_SIMPLE_DESCRIPTOR_RESPONSE) },
                { 0x001C, new ZdoCommandType(0x001C, CommandType.FIND_NODE_CACHE_REQUEST) },
                { 0x801C, new ZdoCommandType(0x801C, CommandType.FIND_NODE_CACHE_RESPONSE) },
                { 0x0001, new ZdoCommandType(0x0001, CommandType.IEEE_ADDRESS_REQUEST) },
                { 0x8001, new ZdoCommandType(0x8001, CommandType.IEEE_ADDRESS_RESPONSE) },
                { 0x0033, new ZdoCommandType(0x0033, CommandType.MANAGEMENT_BIND_REQUEST) },
                { 0x8033, new ZdoCommandType(0x8033, CommandType.MANAGEMENT_BIND_RESPONSE) },
                { 0x8037, new ZdoCommandType(0x8037, CommandType.MANAGEMENT_CACHE_RESPONSE) },
                { 0x0035, new ZdoCommandType(0x0035, CommandType.MANAGEMENT_DIRECT_JOIN_REQUEST) },
                { 0x8035, new ZdoCommandType(0x8035, CommandType.MANAGEMENT_DIRECT_JOIN_RESPONSE) },
                { 0x0034, new ZdoCommandType(0x0034, CommandType.MANAGEMENT_LEAVE_REQUEST) },
                { 0x8034, new ZdoCommandType(0x8034, CommandType.MANAGEMENT_LEAVE_RESPONSE) },
                { 0x0031, new ZdoCommandType(0x0031, CommandType.MANAGEMENT_LQI_REQUEST) },
                { 0x8031, new ZdoCommandType(0x8031, CommandType.MANAGEMENT_LQI_RESPONSE) },
                { 0x0030, new ZdoCommandType(0x0030, CommandType.MANAGEMENT_NETWORK_DISCOVERY) },
                { 0x8030, new ZdoCommandType(0x8030, CommandType.MANAGEMENT_NETWORK_DISCOVERY_RESPONSE) },
                { 0x8038, new ZdoCommandType(0x8038, CommandType.MANAGEMENT_NETWORK_UPDATE_NOTIFY) },
                { 0x0036, new ZdoCommandType(0x0036, CommandType.MANAGEMENT_PERMIT_JOINING_REQUEST) },
                { 0x8036, new ZdoCommandType(0x8036, CommandType.MANAGEMENT_PERMIT_JOINING_RESPONSE) },
                { 0x0032, new ZdoCommandType(0x0032, CommandType.MANAGEMENT_ROUTING_REQUEST) },
                { 0x8032, new ZdoCommandType(0x8032, CommandType.MANAGEMENT_ROUTING_RESPONSE) },
                { 0x0006, new ZdoCommandType(0x0006, CommandType.MATCH_DESCRIPTOR_REQUEST) },
                { 0x8006, new ZdoCommandType(0x8006, CommandType.MATCH_DESCRIPTOR_RESPONSE) },
                { 0x0000, new ZdoCommandType(0x0000, CommandType.NETWORK_ADDRESS_REQUEST) },
                { 0x8000, new ZdoCommandType(0x8000, CommandType.NETWORK_ADDRESS_RESPONSE) },
                { 0x0038, new ZdoCommandType(0x0038, CommandType.NETWORK_UPDATE_REQUEST) },
                { 0x0002, new ZdoCommandType(0x0002, CommandType.NODE_DESCRIPTOR_REQUEST) },
                { 0x8002, new ZdoCommandType(0x8002, CommandType.NODE_DESCRIPTOR_RESPONSE) },
                { 0x0017, new ZdoCommandType(0x0017, CommandType.NODE_DESCRIPTOR_STORE_REQUEST) },
                { 0x8017, new ZdoCommandType(0x8017, CommandType.NODE_DESCRIPTOR_STORE_RESPONSE) },
                { 0x0003, new ZdoCommandType(0x0003, CommandType.POWER_DESCRIPTOR_REQUEST) },
                { 0x8003, new ZdoCommandType(0x8003, CommandType.POWER_DESCRIPTOR_RESPONSE) },
                { 0x0018, new ZdoCommandType(0x0018, CommandType.POWER_DESCRIPTOR_STORE_REQUEST) },
                { 0x8018, new ZdoCommandType(0x8018, CommandType.POWER_DESCRIPTOR_STORE_RESPONSE) },
                { 0x0028, new ZdoCommandType(0x0028, CommandType.RECOVER_BIND_TABLE_REQUEST) },
                { 0x8028, new ZdoCommandType(0x8028, CommandType.RECOVER_BIND_TABLE_RESPONSE) },
                { 0x002A, new ZdoCommandType(0x002A, CommandType.RECOVER_SOURCE_BIND_REQUEST) },
                { 0x8029, new ZdoCommandType(0x8029, CommandType.RECOVER_SOURCE_BIND_RESPONSE) },
                { 0x8026, new ZdoCommandType(0x8026, CommandType.REMOVE_BACKUP_BIND_ENTRY_RESPONSE) },
                { 0x0026, new ZdoCommandType(0x0026, CommandType.REMOVE_BACKUP_BIND_TABLE_REQUEST) },
                { 0x801B, new ZdoCommandType(0x801B, CommandType.REMOVE_NODE_CACHE) },
                { 0x001B, new ZdoCommandType(0x001B, CommandType.REMOVE_NODE_CACHE_REQUEST) },
                { 0x0024, new ZdoCommandType(0x0024, CommandType.REPLACE_DEVICE_REQUEST) },
                { 0x8024, new ZdoCommandType(0x8024, CommandType.REPLACE_DEVICE_RESPONSE) },
                { 0x0004, new ZdoCommandType(0x0004, CommandType.SIMPLE_DESCRIPTOR_REQUEST) },
                { 0x8004, new ZdoCommandType(0x8004, CommandType.SIMPLE_DESCRIPTOR_RESPONSE) },
                { 0x001A, new ZdoCommandType(0x001A, CommandType.SIMPLE_DESCRIPTOR_STORE) },
                { 0x801A, new ZdoCommandType(0x801A, CommandType.SIMPLE_DESCRIPTOR_STORE_RESPONSE) },
                { 0x0025, new ZdoCommandType(0x0025, CommandType.STORE_BACKUP_BIND_ENTRY_REQUEST) },
                { 0x8025, new ZdoCommandType(0x8025, CommandType.STORE_BACKUP_BIND_ENTRY_RESPONSE) },
                { 0x0015, new ZdoCommandType(0x0015, CommandType.SYSTEM_SERVER_DISCOVERY_REQUEST) },
                { 0x0022, new ZdoCommandType(0x0022, CommandType.UNBIND_REQUEST) },
                { 0x8022, new ZdoCommandType(0x8022, CommandType.UNBIND_RESPONSE) },
                { 0x8014, new ZdoCommandType(0x8014, CommandType.USER_DESCRIPTOR_CONF) },
                { 0x0011, new ZdoCommandType(0x0011, CommandType.USER_DESCRIPTOR_REQUEST) },
                { 0x8011, new ZdoCommandType(0x8011, CommandType.USER_DESCRIPTOR_RESPONSE) },
                { 0x0014, new ZdoCommandType(0x0014, CommandType.USER_DESCRIPTOR_SET_REQUEST) },
            };
        }

        public static ZdoCommandType GetValueById(int clusterId)
        {
            if (_idCommandTypeMapping.ContainsKey(clusterId))
            {
                return _idCommandTypeMapping[clusterId];
            }

            return null;
        }

        public static ZdoCommandType GetValueByType(CommandType commandType)
        {
            return GetValueById((int)commandType);
        }

        public ZdoCommand GetZdoCommand()
        {
            switch (CommandType)
            {
                case CommandType.ACTIVE_ENDPOINTS_REQUEST:
                    return new ActiveEndpointsRequest();
                case CommandType.ACTIVE_ENDPOINTS_RESPONSE:
                    return new ActiveEndpointsResponse();
                case CommandType.ACTIVE_ENDPOINT_STORE_REQUEST:
                    return new ActiveEndpointStoreRequest();
                case CommandType.ACTIVE_ENDPOINT_STORE_RESPONSE:
                    return new ActiveEndpointStoreResponse();
                case CommandType.BACKUP_BIND_TABLE_REQUEST:
                    return new BackupBindTableRequest();
                case CommandType.BACKUP_BIND_TABLE_RESPONSE:
                    return new BackupBindTableResponse();
                case CommandType.BACKUP_SOURCE_BIND_REQUEST:
                    return new BackupSourceBindRequest();
                case CommandType.BIND_REGISTER:
                    return new BindRegister();
                case CommandType.BIND_REGISTER_RESPONSE:
                    return new BindRegisterResponse();
                case CommandType.BIND_REQUEST:
                    return new BindRequest();
                case CommandType.BIND_RESPONSE:
                    return new BindResponse();
                case CommandType.CACHE_REQUEST:
                    return new CacheRequest();
                case CommandType.COMPLEX_DESCRIPTOR_REQUEST:
                    return new ComplexDescriptorRequest();
                case CommandType.COMPLEX_DESCRIPTOR_RESPONSE:
                    return new ComplexDescriptorResponse();
                case CommandType.DEVICE_ANNOUNCE:
                    return new DeviceAnnounce();
                case CommandType.DISCOVERY_CACHE_REQUEST:
                    return new DiscoveryCacheRequest();
                case CommandType.DISCOVERY_CACHE_RESPONSE:
                    return new DiscoveryCacheResponse();
                case CommandType.DISCOVERY_STORE_REQUEST_REQUEST:
                    return new DiscoveryStoreRequestRequest();
                case CommandType.DISCOVERY_STORE_RESPONSE:
                    return new DiscoveryStoreResponse();
                case CommandType.END_DEVICE_BIND_REQUEST:
                    return new EndDeviceBindRequest();
                case CommandType.END_DEVICE_BIND_RESPONSE:
                    return new EndDeviceBindResponse();
                case CommandType.EXTENDED_ACTIVE_ENDPOINT_REQUEST:
                    return new ExtendedActiveEndpointRequest();
                case CommandType.EXTENDED_ACTIVE_ENDPOINT_RESPONSE:
                    return new ExtendedActiveEndpointResponse();
                case CommandType.EXTENDED_SIMPLE_DESCRIPTOR_REQUEST:
                    return new ExtendedSimpleDescriptorRequest();
                case CommandType.EXTENDED_SIMPLE_DESCRIPTOR_RESPONSE:
                    return new ExtendedSimpleDescriptorResponse();
                case CommandType.FIND_NODE_CACHE_REQUEST:
                    return new FindNodeCacheRequest();
                case CommandType.FIND_NODE_CACHE_RESPONSE:
                    return new FindNodeCacheResponse();
                case CommandType.IEEE_ADDRESS_REQUEST:
                    return new IeeeAddressRequest();
                case CommandType.IEEE_ADDRESS_RESPONSE:
                    return new IeeeAddressResponse();
                case CommandType.MANAGEMENT_BIND_REQUEST:
                    return new ManagementBindRequest();
                case CommandType.MANAGEMENT_BIND_RESPONSE:
                    return new ManagementBindResponse();
                case CommandType.MANAGEMENT_CACHE_RESPONSE:
                    return new ManagementCacheResponse();
                case CommandType.MANAGEMENT_DIRECT_JOIN_REQUEST:
                    return new ManagementDirectJoinRequest();
                case CommandType.MANAGEMENT_DIRECT_JOIN_RESPONSE:
                    return new ManagementDirectJoinResponse();
                case CommandType.MANAGEMENT_LEAVE_REQUEST:
                    return new ManagementLeaveRequest();
                case CommandType.MANAGEMENT_LEAVE_RESPONSE:
                    return new ManagementLeaveResponse();
                case CommandType.MANAGEMENT_LQI_REQUEST:
                    return new ManagementLqiRequest();
                case CommandType.MANAGEMENT_LQI_RESPONSE:
                    return new ManagementLqiResponse();
                case CommandType.MANAGEMENT_NETWORK_DISCOVERY:
                    return new ManagementNetworkDiscovery();
                case CommandType.MANAGEMENT_NETWORK_DISCOVERY_RESPONSE:
                    return new ManagementNetworkDiscoveryResponse();
                case CommandType.MANAGEMENT_NETWORK_UPDATE_NOTIFY:
                    return new ManagementNetworkUpdateNotify();
                case CommandType.MANAGEMENT_PERMIT_JOINING_REQUEST:
                    return new ManagementPermitJoiningRequest();
                case CommandType.MANAGEMENT_PERMIT_JOINING_RESPONSE:
                    return new ManagementPermitJoiningResponse();
                case CommandType.MANAGEMENT_ROUTING_REQUEST:
                    return new ManagementRoutingRequest();
                case CommandType.MANAGEMENT_ROUTING_RESPONSE:
                    return new ManagementRoutingResponse();
                case CommandType.MATCH_DESCRIPTOR_REQUEST:
                    return new MatchDescriptorRequest();
                case CommandType.MATCH_DESCRIPTOR_RESPONSE:
                    return new MatchDescriptorResponse();
                case CommandType.NETWORK_ADDRESS_REQUEST:
                    return new NetworkAddressRequest();
                case CommandType.NETWORK_ADDRESS_RESPONSE:
                    return new NetworkAddressResponse();
                case CommandType.NETWORK_UPDATE_REQUEST:
                    return new NetworkUpdateRequest();
                case CommandType.NODE_DESCRIPTOR_REQUEST:
                    return new NodeDescriptorRequest();
                case CommandType.NODE_DESCRIPTOR_RESPONSE:
                    return new NodeDescriptorResponse();
                case CommandType.NODE_DESCRIPTOR_STORE_REQUEST:
                    return new NodeDescriptorStoreRequest();
                case CommandType.NODE_DESCRIPTOR_STORE_RESPONSE:
                    return new NodeDescriptorStoreResponse();
                case CommandType.POWER_DESCRIPTOR_REQUEST:
                    return new PowerDescriptorRequest();
                case CommandType.POWER_DESCRIPTOR_RESPONSE:
                    return new PowerDescriptorResponse();
                case CommandType.POWER_DESCRIPTOR_STORE_REQUEST:
                    return new PowerDescriptorStoreRequest();
                case CommandType.POWER_DESCRIPTOR_STORE_RESPONSE:
                    return new PowerDescriptorStoreResponse();
                case CommandType.RECOVER_BIND_TABLE_REQUEST:
                    return new RecoverBindTableRequest();
                case CommandType.RECOVER_BIND_TABLE_RESPONSE:
                    return new RecoverBindTableResponse();
                case CommandType.RECOVER_SOURCE_BIND_REQUEST:
                    return new RecoverSourceBindRequest();
                case CommandType.RECOVER_SOURCE_BIND_RESPONSE:
                    return new RecoverSourceBindResponse();
                case CommandType.REMOVE_BACKUP_BIND_ENTRY_RESPONSE:
                    return new RemoveBackupBindEntryResponse();
                case CommandType.REMOVE_BACKUP_BIND_TABLE_REQUEST:
                    return new RemoveBackupBindTableRequest();
                case CommandType.REMOVE_NODE_CACHE:
                    return new RemoveNodeCache();
                case CommandType.REMOVE_NODE_CACHE_REQUEST:
                    return new RemoveNodeCacheRequest();
                case CommandType.REPLACE_DEVICE_REQUEST:
                    return new ReplaceDeviceRequest();
                case CommandType.REPLACE_DEVICE_RESPONSE:
                    return new ReplaceDeviceResponse();
                case CommandType.SIMPLE_DESCRIPTOR_REQUEST:
                    return new SimpleDescriptorRequest();
                case CommandType.SIMPLE_DESCRIPTOR_RESPONSE:
                    return new SimpleDescriptorResponse();
                case CommandType.SIMPLE_DESCRIPTOR_STORE:
                    return new SimpleDescriptorStore();
                case CommandType.SIMPLE_DESCRIPTOR_STORE_RESPONSE:
                    return new SimpleDescriptorStoreResponse();
                case CommandType.STORE_BACKUP_BIND_ENTRY_REQUEST:
                    return new StoreBackupBindEntryRequest();
                case CommandType.STORE_BACKUP_BIND_ENTRY_RESPONSE:
                    return new StoreBackupBindEntryResponse();
                case CommandType.SYSTEM_SERVER_DISCOVERY_REQUEST:
                    return new SystemServerDiscoveryRequest();
                case CommandType.UNBIND_REQUEST:
                    return new UnbindRequest();
                case CommandType.UNBIND_RESPONSE:
                    return new UnbindResponse();
                case CommandType.USER_DESCRIPTOR_CONF:
                    return new UserDescriptorConf();
                case CommandType.USER_DESCRIPTOR_REQUEST:
                    return new UserDescriptorRequest();
                case CommandType.USER_DESCRIPTOR_RESPONSE:
                    return new UserDescriptorResponse();
                case CommandType.USER_DESCRIPTOR_SET_REQUEST:
                    return new UserDescriptorSetRequest();
                default:
                    throw new ArgumentException("Unknown ZdoCommandType");
                    
            }
        }
    }

}

