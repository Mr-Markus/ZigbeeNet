// License text here

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.DAO;
using ZigBeeNet.ZCL.Protocol;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Clusters.General;

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Generalcluster implementation (Cluster ID 0xFFFF).
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclGeneralCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0xFFFF;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "General";

        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a General cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclGeneralCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// The Read Attributes Command
        ///
        /// The read attributes command is generated when a device wishes to determine the
        /// values of one or more attributes located on another device. Each attribute
        /// identifier field shall contain the identifier of the attribute to be read.
        ///
        /// <param name="identifiers"><see cref="List<ushort>"/> Identifiers</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ReadAttributesCommand(List<ushort> identifiers)
        {
            ReadAttributesCommand command = new ReadAttributesCommand();

            // Set the fields
            command.Identifiers = identifiers;

            return Send(command);
        }

        /// <summary>
        /// The Read Attributes Response
        ///
        /// The read attributes response command is generated in response to a read attributes
        /// or read attributes structured command. The command frame shall contain a read
        /// attribute status record for each attribute identifier specified in the original read
        /// attributes or read attributes structured command. For each read attribute status
        /// record, the attribute identifier field shall contain the identifier specified in the
        /// original read attributes or read attributes structured command.
        ///
        /// <param name="records"><see cref="List<ReadAttributeStatusRecord>"/> Records</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ReadAttributesResponse(List<ReadAttributeStatusRecord> records)
        {
            ReadAttributesResponse command = new ReadAttributesResponse();

            // Set the fields
            command.Records = records;

            return Send(command);
        }

        /// <summary>
        /// The Write Attributes Command
        ///
        /// The write attributes command is generated when a device wishes to change the
        /// values of one or more attributes located on another device. Each write attribute
        /// record shall contain the identifier and the actual value of the attribute to be
        /// written.
        ///
        /// <param name="records"><see cref="List<WriteAttributeRecord>"/> Records</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> WriteAttributesCommand(List<WriteAttributeRecord> records)
        {
            WriteAttributesCommand command = new WriteAttributesCommand();

            // Set the fields
            command.Records = records;

            return Send(command);
        }

        /// <summary>
        /// The Write Attributes Undivided Command
        ///
        /// The write attributes undivided command is generated when a device wishes to
        /// change the values of one or more attributes located on another device, in such a
        /// way that if any attribute cannot be written (e.g. if an attribute is not implemented
        /// on the device, or a value to be written is outside its valid range), no attribute
        /// values are changed.
        /// <br>
        /// In all other respects, including generation of a write attributes response command,
        /// the format and operation of the command is the same as that of the write attributes
        /// command, except that the command identifier field shall be set to indicate the
        /// write attributes undivided command.
        ///
        /// <param name="records"><see cref="List<WriteAttributeRecord>"/> Records</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> WriteAttributesUndividedCommand(List<WriteAttributeRecord> records)
        {
            WriteAttributesUndividedCommand command = new WriteAttributesUndividedCommand();

            // Set the fields
            command.Records = records;

            return Send(command);
        }

        /// <summary>
        /// The Write Attributes Response
        ///
        /// The write attributes response command is generated in response to a write
        /// attributes command.
        ///
        /// <param name="records"><see cref="List<WriteAttributeStatusRecord>"/> Records</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> WriteAttributesResponse(List<WriteAttributeStatusRecord> records)
        {
            WriteAttributesResponse command = new WriteAttributesResponse();

            // Set the fields
            command.Records = records;

            return Send(command);
        }

        /// <summary>
        /// The Write Attributes No Response
        ///
        /// The write attributes no response command is generated when a device wishes to
        /// change the value of one or more attributes located on another device but does not
        /// require a response. Each write attribute record shall contain the identifier and the
        /// actual value of the attribute to be written.
        ///
        /// <param name="records"><see cref="List<WriteAttributeRecord>"/> Records</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> WriteAttributesNoResponse(List<WriteAttributeRecord> records)
        {
            WriteAttributesNoResponse command = new WriteAttributesNoResponse();

            // Set the fields
            command.Records = records;

            return Send(command);
        }

        /// <summary>
        /// The Configure Reporting Command
        ///
        /// The Configure Reporting command is used to configure the reporting mechanism
        /// for one or more of the attributes of a cluster.
        /// <br>
        /// The individual cluster definitions specify which attributes shall be available to this
        /// reporting mechanism, however specific implementations of a cluster may make
        /// additional attributes available.
        ///
        /// <param name="records"><see cref="List<AttributeReportingConfigurationRecord>"/> Records</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ConfigureReportingCommand(List<AttributeReportingConfigurationRecord> records)
        {
            ConfigureReportingCommand command = new ConfigureReportingCommand();

            // Set the fields
            command.Records = records;

            return Send(command);
        }

        /// <summary>
        /// The Configure Reporting Response
        ///
        /// The Configure Reporting Response command is generated in response to a
        /// Configure Reporting command.
        ///
        /// <param name="status"><see cref="ZclStatus"/> Status</param>
        /// <param name="records"><see cref="List<AttributeStatusRecord>"/> Records</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ConfigureReportingResponse(ZclStatus status, List<AttributeStatusRecord> records)
        {
            ConfigureReportingResponse command = new ConfigureReportingResponse();

            // Set the fields
            command.Status = status;
            command.Records = records;

            return Send(command);
        }

        /// <summary>
        /// The Read Reporting Configuration Command
        ///
        /// The Read Reporting Configuration command is used to read the configuration
        /// details of the reporting mechanism for one or more of the attributes of a cluster.
        ///
        /// <param name="records"><see cref="List<AttributeRecord>"/> Records</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ReadReportingConfigurationCommand(List<AttributeRecord> records)
        {
            ReadReportingConfigurationCommand command = new ReadReportingConfigurationCommand();

            // Set the fields
            command.Records = records;

            return Send(command);
        }

        /// <summary>
        /// The Read Reporting Configuration Response
        ///
        /// The Read Reporting Configuration Response command is used to respond to a
        /// Read Reporting Configuration command.
        ///
        /// <param name="records"><see cref="List<AttributeReportingConfigurationRecord>"/> Records</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ReadReportingConfigurationResponse(List<AttributeReportingConfigurationRecord> records)
        {
            ReadReportingConfigurationResponse command = new ReadReportingConfigurationResponse();

            // Set the fields
            command.Records = records;

            return Send(command);
        }

        /// <summary>
        /// The Report Attributes Command
        ///
        /// The report attributes command is used by a device to report the values of one or
        /// more of its attributes to another device, bound a priori. Individual clusters, defined
        /// elsewhere in the ZCL, define which attributes are to be reported and at what
        /// interval.
        ///
        /// <param name="reports"><see cref="List<AttributeReport>"/> Reports</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ReportAttributesCommand(List<AttributeReport> reports)
        {
            ReportAttributesCommand command = new ReportAttributesCommand();

            // Set the fields
            command.Reports = reports;

            return Send(command);
        }

        /// <summary>
        /// The Default Response
        ///
        /// The default response command is generated when a device receives a unicast
        /// command, there is no other relevant response specified for the command, and
        /// either an error results or the Disable default response bit of its Frame control field
        /// is set to 0.
        ///
        /// <param name="commandIdentifier"><see cref="byte"/> Command identifier</param>
        /// <param name="statusCode"><see cref="ZclStatus"/> Status code</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> DefaultResponse(byte commandIdentifier, ZclStatus statusCode)
        {
            DefaultResponse command = new DefaultResponse();

            // Set the fields
            command.CommandIdentifier = commandIdentifier;
            command.StatusCode = statusCode;

            return Send(command);
        }

        /// <summary>
        /// The Discover Attributes Command
        ///
        /// The discover attributes command is generated when a remote device wishes to
        /// discover the identifiers and types of the attributes on a device which are supported
        /// within the cluster to which this command is directed.
        ///
        /// <param name="startAttributeIdentifier"><see cref="ushort"/> Start attribute identifier</param>
        /// <param name="maximumAttributeIdentifiers"><see cref="byte"/> Maximum attribute identifiers</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> DiscoverAttributesCommand(ushort startAttributeIdentifier, byte maximumAttributeIdentifiers)
        {
            DiscoverAttributesCommand command = new DiscoverAttributesCommand();

            // Set the fields
            command.StartAttributeIdentifier = startAttributeIdentifier;
            command.MaximumAttributeIdentifiers = maximumAttributeIdentifiers;

            return Send(command);
        }

        /// <summary>
        /// The Discover Attributes Response
        ///
        /// The discover attributes response command is generated in response to a discover
        /// attributes command.
        ///
        /// <param name="discoveryComplete"><see cref="bool"/> Discovery Complete</param>
        /// <param name="attributeInformation"><see cref="List<AttributeInformation>"/> Attribute Information</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> DiscoverAttributesResponse(bool discoveryComplete, List<AttributeInformation> attributeInformation)
        {
            DiscoverAttributesResponse command = new DiscoverAttributesResponse();

            // Set the fields
            command.DiscoveryComplete = discoveryComplete;
            command.AttributeInformation = attributeInformation;

            return Send(command);
        }

        /// <summary>
        /// The Read Attributes Structured Command
        ///
        /// The read attributes command is generated when a device wishes to determine the
        /// values of one or more attributes, or elements of attributes, located on another
        /// device. Each attribute identifier field shall contain the identifier of the attribute to
        /// be read.
        ///
        /// <param name="attributeSelectors"><see cref="object"/> Attribute selectors</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> ReadAttributesStructuredCommand(object attributeSelectors)
        {
            ReadAttributesStructuredCommand command = new ReadAttributesStructuredCommand();

            // Set the fields
            command.AttributeSelectors = attributeSelectors;

            return Send(command);
        }

        /// <summary>
        /// The Write Attributes Structured Command
        ///
        /// The write attributes structured command is generated when a device wishes to
        /// change the values of one or more attributes located on another device. Each write
        /// attribute record shall contain the identifier and the actual value of the attribute, or
        /// element thereof, to be written.
        ///
        /// <param name="status"><see cref="ZclStatus"/> Status</param>
        /// <param name="attributeSelectors"><see cref="object"/> Attribute selectors</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> WriteAttributesStructuredCommand(ZclStatus status, object attributeSelectors)
        {
            WriteAttributesStructuredCommand command = new WriteAttributesStructuredCommand();

            // Set the fields
            command.Status = status;
            command.AttributeSelectors = attributeSelectors;

            return Send(command);
        }

        /// <summary>
        /// The Write Attributes Structured Response
        ///
        /// The write attributes structured response command is generated in response to a
        /// write attributes structured command.
        ///
        /// <param name="status"><see cref="ZclStatus"/> Status</param>
        /// <param name="records"><see cref="List<WriteAttributeStatusRecord>"/> Records</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> WriteAttributesStructuredResponse(ZclStatus status, List<WriteAttributeStatusRecord> records)
        {
            WriteAttributesStructuredResponse command = new WriteAttributesStructuredResponse();

            // Set the fields
            command.Status = status;
            command.Records = records;

            return Send(command);
        }

        /// <summary>
        /// The Discover Commands Received
        ///
        /// The Discover Commands Received command is generated when a remote device wishes to discover the
        /// optional and mandatory commands the cluster to which this command is sent can process.
        ///
        /// <param name="startCommandIdentifier"><see cref="byte"/> Start command identifier</param>
        /// <param name="maximumCommandIdentifiers"><see cref="byte"/> Maximum command identifiers</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> DiscoverCommandsReceived(byte startCommandIdentifier, byte maximumCommandIdentifiers)
        {
            DiscoverCommandsReceived command = new DiscoverCommandsReceived();

            // Set the fields
            command.StartCommandIdentifier = startCommandIdentifier;
            command.MaximumCommandIdentifiers = maximumCommandIdentifiers;

            return Send(command);
        }

        /// <summary>
        /// The Discover Commands Received Response
        ///
        /// The Discover Commands Received Response is generated in response to a Discover Commands Received
        /// command.
        ///
        /// <param name="discoveryComplete"><see cref="bool"/> Discovery complete</param>
        /// <param name="commandIdentifiers"><see cref="List<byte>"/> Command identifiers</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> DiscoverCommandsReceivedResponse(bool discoveryComplete, List<byte> commandIdentifiers)
        {
            DiscoverCommandsReceivedResponse command = new DiscoverCommandsReceivedResponse();

            // Set the fields
            command.DiscoveryComplete = discoveryComplete;
            command.CommandIdentifiers = commandIdentifiers;

            return Send(command);
        }

        /// <summary>
        /// The Discover Commands Generated
        ///
        /// The Discover Commands Generated command is generated when a remote device wishes to discover the
        /// commands that a cluster may generate on the device to which this command is directed.
        ///
        /// <param name="startCommandIdentifier"><see cref="byte"/> Start command identifier</param>
        /// <param name="maximumCommandIdentifiers"><see cref="byte"/> Maximum command identifiers</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> DiscoverCommandsGenerated(byte startCommandIdentifier, byte maximumCommandIdentifiers)
        {
            DiscoverCommandsGenerated command = new DiscoverCommandsGenerated();

            // Set the fields
            command.StartCommandIdentifier = startCommandIdentifier;
            command.MaximumCommandIdentifiers = maximumCommandIdentifiers;

            return Send(command);
        }

        /// <summary>
        /// The Discover Commands Generated Response
        ///
        /// The Discover Commands Generated Response is generated in response to a Discover Commands Generated
        /// command.
        ///
        /// <param name="discoveryComplete"><see cref="bool"/> Discovery complete</param>
        /// <param name="commandIdentifiers"><see cref="List<byte>"/> Command identifiers</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> DiscoverCommandsGeneratedResponse(bool discoveryComplete, List<byte> commandIdentifiers)
        {
            DiscoverCommandsGeneratedResponse command = new DiscoverCommandsGeneratedResponse();

            // Set the fields
            command.DiscoveryComplete = discoveryComplete;
            command.CommandIdentifiers = commandIdentifiers;

            return Send(command);
        }

        /// <summary>
        /// The Discover Attributes Extended
        ///
        /// The Discover Attributes Extended command is generated when a remote device wishes to discover the
        /// identifiers and types of the attributes on a device which are supported within the cluster to which this
        /// command is directed, including whether the attribute is readable, writeable or reportable.
        ///
        /// <param name="startAttributeIdentifier"><see cref="ushort"/> Start attribute identifier</param>
        /// <param name="maximumAttributeIdentifiers"><see cref="byte"/> Maximum attribute identifiers</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> DiscoverAttributesExtended(ushort startAttributeIdentifier, byte maximumAttributeIdentifiers)
        {
            DiscoverAttributesExtended command = new DiscoverAttributesExtended();

            // Set the fields
            command.StartAttributeIdentifier = startAttributeIdentifier;
            command.MaximumAttributeIdentifiers = maximumAttributeIdentifiers;

            return Send(command);
        }

        /// <summary>
        /// The Discover Attributes Extended Response
        ///
        /// The Discover Attributes Extended Response command is generated in response to a Discover Attributes
        /// Extended command.
        ///
        /// <param name="discoveryComplete"><see cref="bool"/> Discovery complete</param>
        /// <param name="attributeInformation"><see cref="List<ExtendedAttributeInformation>"/> Attribute Information</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> DiscoverAttributesExtendedResponse(bool discoveryComplete, List<ExtendedAttributeInformation> attributeInformation)
        {
            DiscoverAttributesExtendedResponse command = new DiscoverAttributesExtendedResponse();

            // Set the fields
            command.DiscoveryComplete = discoveryComplete;
            command.AttributeInformation = attributeInformation;

            return Send(command);
        }

        public override ZclCommand GetCommandFromId(int commandId)
        {
            switch (commandId)
            {
                case 0: // READ_ATTRIBUTES_COMMAND
                    return new ReadAttributesCommand();
                case 1: // READ_ATTRIBUTES_RESPONSE
                    return new ReadAttributesResponse();
                case 2: // WRITE_ATTRIBUTES_COMMAND
                    return new WriteAttributesCommand();
                case 3: // WRITE_ATTRIBUTES_UNDIVIDED_COMMAND
                    return new WriteAttributesUndividedCommand();
                case 4: // WRITE_ATTRIBUTES_RESPONSE
                    return new WriteAttributesResponse();
                case 5: // WRITE_ATTRIBUTES_NO_RESPONSE
                    return new WriteAttributesNoResponse();
                case 6: // CONFIGURE_REPORTING_COMMAND
                    return new ConfigureReportingCommand();
                case 7: // CONFIGURE_REPORTING_RESPONSE
                    return new ConfigureReportingResponse();
                case 8: // READ_REPORTING_CONFIGURATION_COMMAND
                    return new ReadReportingConfigurationCommand();
                case 9: // READ_REPORTING_CONFIGURATION_RESPONSE
                    return new ReadReportingConfigurationResponse();
                case 10: // REPORT_ATTRIBUTES_COMMAND
                    return new ReportAttributesCommand();
                case 11: // DEFAULT_RESPONSE
                    return new DefaultResponse();
                case 12: // DISCOVER_ATTRIBUTES_COMMAND
                    return new DiscoverAttributesCommand();
                case 13: // DISCOVER_ATTRIBUTES_RESPONSE
                    return new DiscoverAttributesResponse();
                case 14: // READ_ATTRIBUTES_STRUCTURED_COMMAND
                    return new ReadAttributesStructuredCommand();
                case 15: // WRITE_ATTRIBUTES_STRUCTURED_COMMAND
                    return new WriteAttributesStructuredCommand();
                case 16: // WRITE_ATTRIBUTES_STRUCTURED_RESPONSE
                    return new WriteAttributesStructuredResponse();
                case 17: // DISCOVER_COMMANDS_RECEIVED
                    return new DiscoverCommandsReceived();
                case 18: // DISCOVER_COMMANDS_RECEIVED_RESPONSE
                    return new DiscoverCommandsReceivedResponse();
                case 19: // DISCOVER_COMMANDS_GENERATED
                    return new DiscoverCommandsGenerated();
                case 20: // DISCOVER_COMMANDS_GENERATED_RESPONSE
                    return new DiscoverCommandsGeneratedResponse();
                case 21: // DISCOVER_ATTRIBUTES_EXTENDED
                    return new DiscoverAttributesExtended();
                case 22: // DISCOVER_ATTRIBUTES_EXTENDED_RESPONSE
                    return new DiscoverAttributesExtendedResponse();
                    default:
                        return null;
            }
        }
    }
}
