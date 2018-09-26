using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    /// <summary>
    /// General command frames are used for manipulating attributes and other general tasks that are not specific to an individual cluster. 
    /// 
    /// Each command frame SHALL be constructed with the frame type sub-field of the frame control field set to Global (0x00). 
    /// </summary>
    public enum GlobalCommands : byte
    {
        ReadAttributes = 0x00,
        ReadAttributesResponse = 0x01,
        WriteAttributes = 0x02,
        WriteAttributesUndivided = 0x03,
        WriteAttributesResponse = 0x04,
        WriteAttributesNoResponse = 0x05,
        ConfigureReporting = 0x06,
        ConfigureReportingResponse = 0x07,
        ReadReportingConfiguration = 0x08,
        ReadReportingConfigurationResponse = 0x09,
        ReportAttributes = 0x0a,
        DefaultResponse = 0x0b,
        DiscoverAttributes = 0x0c,
        DiscoverAttributesResponse = 0x0d,
        ReadAttributesStructured = 0x0e,
        WriteAttributesStructured = 0x0f,
        WriteAttributesStructuredResponse = 0x10,
        DiscoverCommandsReceived = 0x11,
        DiscoverCommandsReceivedResponse = 0x12,
        DiscoverCommandsGenerated = 0x13,
        DiscoverCommandsGeneratedResponse = 0x14,
        DiscoverAttributesExtended = 0x15,
        DiscoverAttributesExtendedResponse = 0x16
    }
}
