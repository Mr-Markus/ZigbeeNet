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

namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Multistate Input (Basic)cluster implementation (Cluster ID 0x0012).
    ///
    /// The Multistate Input (Basic) cluster provides an interface for reading the value of a
    /// multistate measurement and accessing various characteristics of that measurement. The
    /// cluster is typically used to implement a sensor that measures a physical quantity that
    /// can take on one of a number of discrete states.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclMultistateInputBasicCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0012;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Multistate Input (Basic)";

        /* Attribute constants */

        /// <summary>
        /// This  attribute, of type Array of Character strings, holds descriptions of all possible
        /// states of a multistate PresentValue.  The number of descriptions matches the number of states
        /// defined in the NumberOfStates property. The PresentValue, interpreted as an integer, serves as
        /// an index into the array. If the size of this array is changed, the NumberOfStates property SHALL
        /// also be changed to the same value. The character set used SHALL be ASCII, and the attribute
        /// SHALL contain a maximum of 16 characters, which SHALL be printable but are otherwise unrestricted.
        /// </summary>
        public const ushort ATTR_STATETEXT = 0x000E;

        /// <summary>
        /// The Description attribute, of type Character string, MAY be used to hold a description
        /// of the usage of the input, output or value, as appropriate to the cluster. The character
        /// set used SHALL be ASCII, and the attribute SHALL contain a maximum of 16 characters,
        /// which SHALL be printable but are otherwise unrestricted.
        /// </summary>
        public const ushort ATTR_DESCRIPTION = 0x001C;

        /// <summary>
        /// This attribute, of type Unsigned 16-bit integer, defines the number of states that a multistate
        /// PresentValue MAY have. The NumberOfStates property SHALL always have a value greater than zero.
        /// If the value of this property is changed, the size of the StateText array, if present, SHALL also
        /// be changed to the same value. The states are numbered consecutively, starting with 1.
        /// </summary>
        public const ushort ATTR_NUMBEROFSTATES = 0x004A;

        /// <summary>
        /// The OutOfService attribute, of type Boolean, indicates whether (TRUE) or not (FALSE) the physical
        /// input, output or value that the cluster represents is not in service. For an Input cluster, when
        /// OutOfService is TRUE the PresentValue attribute is decoupled from the physical input and  will
        /// not track changes to the  physical input. For an Output cluster, when OutOfService is TRUE the
        /// PresentValue attribute is decoupled from the physical output, so changes to PresentValue will not
        /// affect the physical output. For a Value cluster, when OutOfService is TRUE the PresentValue attribute
        /// MAY be written to freely by software local to the device that the cluster resides on.
        /// </summary>
        public const ushort ATTR_OUTOFSERVICE = 0x0051;

        /// <summary>
        /// The PresentValue attribute indicates the current value of the input, output or
        /// value, as appropriate  for the cluster. For Analog clusters it is of type single precision, for Binary
        /// clusters it is of type  Boolean, and for multistate clusters it is of type Unsigned 16-bit integer. The
        /// PresentValue attribute of an input cluster SHALL be writable when OutOfService is TRUE. When the PriorityArray
        /// attribute is implemented, writing to PresentValue SHALL be equivalent to writing to element 16 of PriorityArray,
        /// i.e., with a priority of 16.
        /// </summary>
        public const ushort ATTR_PRESENTVALUE = 0x0055;

        /// <summary>
        /// The Reliability attribute, of type 8-bit enumeration, provides an indication of whether
        /// the PresentValueor the operation of the physical input, output or value in question (as
        /// appropriate for the cluster) is “reliable” as far as can be determined and, if not, why
        /// not. The Reliability attribute MAY have any of the following values:
        /// 
        /// NO-FAULT-DETECTED (0)
        /// OVER-RANGE (2)
        /// UNDER-RANGE (3)
        /// OPEN-LOOP (4)
        /// SHORTED-LOOP (5)
        /// UNRELIABLE-OTHER (7)
        /// PROCESS-ERROR (8)
        /// MULTI-STATE-FAULT (9)
        /// CONFIGURATION-ERROR (10)
        /// </summary>
        public const ushort ATTR_RELIABILITY = 0x0067;

        /// <summary>
        /// This attribute, of type bitmap, represents four Boolean flags that indicate the general “health”
        /// of the analog sensor. Three of the flags are associated with the values of other optional attributes
        /// of this cluster. A more detailed status could be determined by reading the optional attributes (if
        /// supported) that are linked to these flags. The relationship between individual flags is not defined.
        /// 
        /// The four flags are Bit 0 = IN_ALARM, Bit 1 = FAULT, Bit 2 = OVERRIDDEN, Bit 3 = OUT OF SERVICE
        /// 
        /// where:
        /// 
        /// IN_ALARM -Logical FALSE (0) if the EventStateattribute has a value of NORMAL, otherwise logical TRUE (1).
        /// This bit is always 0 unless the cluster implementing the EventState attribute is implemented on the same
        /// endpoint.
        /// 
        /// FAULT -Logical TRUE (1) if the Reliability attribute is present and does not have a value of NO FAULT DETECTED,
        /// otherwise logical FALSE (0).
        /// 
        /// OVERRIDDEN -Logical TRUE (1) if the cluster has been overridden by some  mechanism local to the device.
        /// Otherwise, the value is logical FALSE (0). In this context, for an input cluster, “overridden” is taken
        /// to mean that the PresentValue and Reliability(optional) attributes are no longer tracking changes to the
        /// physical input. For an Output cluster, “overridden” is taken to mean that the physical output is no longer
        /// tracking changes to the PresentValue attribute and the Reliability attribute is no longer a reflection of
        /// the physical output. For a Value cluster, “overridden” is taken to mean that the PresentValue attribute is
        /// not writeable.
        /// 
        /// OUT OF SERVICE -Logical TRUE (1) if the OutOfService attribute has a value of TRUE, otherwise
        /// logical FALSE (0).
        /// </summary>
        public const ushort ATTR_STATUSFLAGS = 0x006F;

        /// <summary>
        /// The ApplicationType attribute is an unsigned 32 bit integer that indicates the specific
        /// application usage for this cluster. (Note: This attribute has no BACnet equivalent).
        /// ApplicationType is subdivided into Group, Type and an Index number, as follows.
        /// 
        /// Group = Bits 24-31 An indication of the cluster this attribute is part of.
        /// 
        /// Type = Bits 16-23 For Analog clusters, the physical quantity that the Present Value attribute
        /// of the cluster represents. For Binary and Multistate clusters, the application usage domain.
        /// 
        /// Index = Bits 0-15The specific application usage of the cluster.
        /// </summary>
        public const ushort ATTR_APPLICATIONTYPE = 0x0100;


        // Attribute initialisation
        protected override Dictionary<ushort, ZclAttribute> InitializeAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(8);

            ZclClusterType multistateInputBasic = ZclClusterType.GetValueById(ClusterType.MULTISTATE_INPUT__BASIC);

            attributeMap.Add(ATTR_STATETEXT, new ZclAttribute(multistateInputBasic, ATTR_STATETEXT, "StateText", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_DESCRIPTION, new ZclAttribute(multistateInputBasic, ATTR_DESCRIPTION, "Description", ZclDataType.Get(DataType.CHARACTER_STRING), false, true, true, false));
            attributeMap.Add(ATTR_NUMBEROFSTATES, new ZclAttribute(multistateInputBasic, ATTR_NUMBEROFSTATES, "NumberOfStates", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, true, false));
            attributeMap.Add(ATTR_OUTOFSERVICE, new ZclAttribute(multistateInputBasic, ATTR_OUTOFSERVICE, "OutOfService", ZclDataType.Get(DataType.BOOLEAN), true, true, true, false));
            attributeMap.Add(ATTR_PRESENTVALUE, new ZclAttribute(multistateInputBasic, ATTR_PRESENTVALUE, "PresentValue", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, true, false));
            attributeMap.Add(ATTR_RELIABILITY, new ZclAttribute(multistateInputBasic, ATTR_RELIABILITY, "Reliability", ZclDataType.Get(DataType.ENUMERATION_8_BIT), false, true, true, false));
            attributeMap.Add(ATTR_STATUSFLAGS, new ZclAttribute(multistateInputBasic, ATTR_STATUSFLAGS, "StatusFlags", ZclDataType.Get(DataType.BITMAP_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_APPLICATIONTYPE, new ZclAttribute(multistateInputBasic, ATTR_APPLICATIONTYPE, "ApplicationType", ZclDataType.Get(DataType.SIGNED_32_BIT_INTEGER), false, true, false, false));

            return attributeMap;
        }

        /// <summary>
        /// Default constructor to create a Multistate Input (Basic) cluster.
        ///
        /// <param name ="zigbeeEndpoint">The ZigBeeEndpoint</param>
        /// </summary>
        public ZclMultistateInputBasicCluster(ZigBeeEndpoint zigbeeEndpoint)
            : base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }


        /// <summary>
        /// Set the StateText attribute [attribute ID14].
        ///
        /// This  attribute, of type Array of Character strings, holds descriptions of all possible
        /// states of a multistate PresentValue.  The number of descriptions matches the number of states
        /// defined in the NumberOfStates property. The PresentValue, interpreted as an integer, serves as
        /// an index into the array. If the size of this array is changed, the NumberOfStates property SHALL
        /// also be changed to the same value. The character set used SHALL be ASCII, and the attribute
        /// SHALL contain a maximum of 16 characters, which SHALL be printable but are otherwise unrestricted.
        ///
        /// The attribute is of type string.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="stateText">The string attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetStateText(object value)
        {
            return Write(_attributes[ATTR_STATETEXT], value);
        }


        /// <summary>
        /// Get the StateText attribute [attribute ID14].
        ///
        /// This  attribute, of type Array of Character strings, holds descriptions of all possible
        /// states of a multistate PresentValue.  The number of descriptions matches the number of states
        /// defined in the NumberOfStates property. The PresentValue, interpreted as an integer, serves as
        /// an index into the array. If the size of this array is changed, the NumberOfStates property SHALL
        /// also be changed to the same value. The character set used SHALL be ASCII, and the attribute
        /// SHALL contain a maximum of 16 characters, which SHALL be printable but are otherwise unrestricted.
        ///
        /// The attribute is of type string.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetStateTextAsync()
        {
            return Read(_attributes[ATTR_STATETEXT]);
        }

        /// <summary>
        /// Synchronously Get the StateText attribute [attribute ID14].
        ///
        /// This  attribute, of type Array of Character strings, holds descriptions of all possible
        /// states of a multistate PresentValue.  The number of descriptions matches the number of states
        /// defined in the NumberOfStates property. The PresentValue, interpreted as an integer, serves as
        /// an index into the array. If the size of this array is changed, the NumberOfStates property SHALL
        /// also be changed to the same value. The character set used SHALL be ASCII, and the attribute
        /// SHALL contain a maximum of 16 characters, which SHALL be printable but are otherwise unrestricted.
        ///
        /// The attribute is of type string.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public string GetStateText(long refreshPeriod)
        {
            if (_attributes[ATTR_STATETEXT].IsLastValueCurrent(refreshPeriod))
            {
                return (string)_attributes[ATTR_STATETEXT].LastValue;
            }

            return (string)ReadSync(_attributes[ATTR_STATETEXT]);
        }


        /// <summary>
        /// Set the Description attribute [attribute ID28].
        ///
        /// The Description attribute, of type Character string, MAY be used to hold a description
        /// of the usage of the input, output or value, as appropriate to the cluster. The character
        /// set used SHALL be ASCII, and the attribute SHALL contain a maximum of 16 characters,
        /// which SHALL be printable but are otherwise unrestricted.
        ///
        /// The attribute is of type string.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="description">The string attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetDescription(object value)
        {
            return Write(_attributes[ATTR_DESCRIPTION], value);
        }


        /// <summary>
        /// Get the Description attribute [attribute ID28].
        ///
        /// The Description attribute, of type Character string, MAY be used to hold a description
        /// of the usage of the input, output or value, as appropriate to the cluster. The character
        /// set used SHALL be ASCII, and the attribute SHALL contain a maximum of 16 characters,
        /// which SHALL be printable but are otherwise unrestricted.
        ///
        /// The attribute is of type string.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetDescriptionAsync()
        {
            return Read(_attributes[ATTR_DESCRIPTION]);
        }

        /// <summary>
        /// Synchronously Get the Description attribute [attribute ID28].
        ///
        /// The Description attribute, of type Character string, MAY be used to hold a description
        /// of the usage of the input, output or value, as appropriate to the cluster. The character
        /// set used SHALL be ASCII, and the attribute SHALL contain a maximum of 16 characters,
        /// which SHALL be printable but are otherwise unrestricted.
        ///
        /// The attribute is of type string.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public string GetDescription(long refreshPeriod)
        {
            if (_attributes[ATTR_DESCRIPTION].IsLastValueCurrent(refreshPeriod))
            {
                return (string)_attributes[ATTR_DESCRIPTION].LastValue;
            }

            return (string)ReadSync(_attributes[ATTR_DESCRIPTION]);
        }


        /// <summary>
        /// Set the NumberOfStates attribute [attribute ID74].
        ///
        /// This attribute, of type Unsigned 16-bit integer, defines the number of states that a multistate
        /// PresentValue MAY have. The NumberOfStates property SHALL always have a value greater than zero.
        /// If the value of this property is changed, the size of the StateText array, if present, SHALL also
        /// be changed to the same value. The states are numbered consecutively, starting with 1.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="numberOfStates">The ushort attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetNumberOfStates(object value)
        {
            return Write(_attributes[ATTR_NUMBEROFSTATES], value);
        }


        /// <summary>
        /// Get the NumberOfStates attribute [attribute ID74].
        ///
        /// This attribute, of type Unsigned 16-bit integer, defines the number of states that a multistate
        /// PresentValue MAY have. The NumberOfStates property SHALL always have a value greater than zero.
        /// If the value of this property is changed, the size of the StateText array, if present, SHALL also
        /// be changed to the same value. The states are numbered consecutively, starting with 1.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetNumberOfStatesAsync()
        {
            return Read(_attributes[ATTR_NUMBEROFSTATES]);
        }

        /// <summary>
        /// Synchronously Get the NumberOfStates attribute [attribute ID74].
        ///
        /// This attribute, of type Unsigned 16-bit integer, defines the number of states that a multistate
        /// PresentValue MAY have. The NumberOfStates property SHALL always have a value greater than zero.
        /// If the value of this property is changed, the size of the StateText array, if present, SHALL also
        /// be changed to the same value. The states are numbered consecutively, starting with 1.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetNumberOfStates(long refreshPeriod)
        {
            if (_attributes[ATTR_NUMBEROFSTATES].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_NUMBEROFSTATES].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_NUMBEROFSTATES]);
        }


        /// <summary>
        /// Set the OutOfService attribute [attribute ID81].
        ///
        /// The OutOfService attribute, of type Boolean, indicates whether (TRUE) or not (FALSE) the physical
        /// input, output or value that the cluster represents is not in service. For an Input cluster, when
        /// OutOfService is TRUE the PresentValue attribute is decoupled from the physical input and  will
        /// not track changes to the  physical input. For an Output cluster, when OutOfService is TRUE the
        /// PresentValue attribute is decoupled from the physical output, so changes to PresentValue will not
        /// affect the physical output. For a Value cluster, when OutOfService is TRUE the PresentValue attribute
        /// MAY be written to freely by software local to the device that the cluster resides on.
        ///
        /// The attribute is of type bool.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="outOfService">The bool attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetOutOfService(object value)
        {
            return Write(_attributes[ATTR_OUTOFSERVICE], value);
        }


        /// <summary>
        /// Get the OutOfService attribute [attribute ID81].
        ///
        /// The OutOfService attribute, of type Boolean, indicates whether (TRUE) or not (FALSE) the physical
        /// input, output or value that the cluster represents is not in service. For an Input cluster, when
        /// OutOfService is TRUE the PresentValue attribute is decoupled from the physical input and  will
        /// not track changes to the  physical input. For an Output cluster, when OutOfService is TRUE the
        /// PresentValue attribute is decoupled from the physical output, so changes to PresentValue will not
        /// affect the physical output. For a Value cluster, when OutOfService is TRUE the PresentValue attribute
        /// MAY be written to freely by software local to the device that the cluster resides on.
        ///
        /// The attribute is of type bool.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetOutOfServiceAsync()
        {
            return Read(_attributes[ATTR_OUTOFSERVICE]);
        }

        /// <summary>
        /// Synchronously Get the OutOfService attribute [attribute ID81].
        ///
        /// The OutOfService attribute, of type Boolean, indicates whether (TRUE) or not (FALSE) the physical
        /// input, output or value that the cluster represents is not in service. For an Input cluster, when
        /// OutOfService is TRUE the PresentValue attribute is decoupled from the physical input and  will
        /// not track changes to the  physical input. For an Output cluster, when OutOfService is TRUE the
        /// PresentValue attribute is decoupled from the physical output, so changes to PresentValue will not
        /// affect the physical output. For a Value cluster, when OutOfService is TRUE the PresentValue attribute
        /// MAY be written to freely by software local to the device that the cluster resides on.
        ///
        /// The attribute is of type bool.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public bool GetOutOfService(long refreshPeriod)
        {
            if (_attributes[ATTR_OUTOFSERVICE].IsLastValueCurrent(refreshPeriod))
            {
                return (bool)_attributes[ATTR_OUTOFSERVICE].LastValue;
            }

            return (bool)ReadSync(_attributes[ATTR_OUTOFSERVICE]);
        }


        /// <summary>
        /// Set the PresentValue attribute [attribute ID85].
        ///
        /// The PresentValue attribute indicates the current value of the input, output or
        /// value, as appropriate  for the cluster. For Analog clusters it is of type single precision, for Binary
        /// clusters it is of type  Boolean, and for multistate clusters it is of type Unsigned 16-bit integer. The
        /// PresentValue attribute of an input cluster SHALL be writable when OutOfService is TRUE. When the PriorityArray
        /// attribute is implemented, writing to PresentValue SHALL be equivalent to writing to element 16 of PriorityArray,
        /// i.e., with a priority of 16.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <param name="presentValue">The ushort attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetPresentValue(object value)
        {
            return Write(_attributes[ATTR_PRESENTVALUE], value);
        }


        /// <summary>
        /// Get the PresentValue attribute [attribute ID85].
        ///
        /// The PresentValue attribute indicates the current value of the input, output or
        /// value, as appropriate  for the cluster. For Analog clusters it is of type single precision, for Binary
        /// clusters it is of type  Boolean, and for multistate clusters it is of type Unsigned 16-bit integer. The
        /// PresentValue attribute of an input cluster SHALL be writable when OutOfService is TRUE. When the PriorityArray
        /// attribute is implemented, writing to PresentValue SHALL be equivalent to writing to element 16 of PriorityArray,
        /// i.e., with a priority of 16.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetPresentValueAsync()
        {
            return Read(_attributes[ATTR_PRESENTVALUE]);
        }

        /// <summary>
        /// Synchronously Get the PresentValue attribute [attribute ID85].
        ///
        /// The PresentValue attribute indicates the current value of the input, output or
        /// value, as appropriate  for the cluster. For Analog clusters it is of type single precision, for Binary
        /// clusters it is of type  Boolean, and for multistate clusters it is of type Unsigned 16-bit integer. The
        /// PresentValue attribute of an input cluster SHALL be writable when OutOfService is TRUE. When the PriorityArray
        /// attribute is implemented, writing to PresentValue SHALL be equivalent to writing to element 16 of PriorityArray,
        /// i.e., with a priority of 16.
        ///
        /// The attribute is of type ushort.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public ushort GetPresentValue(long refreshPeriod)
        {
            if (_attributes[ATTR_PRESENTVALUE].IsLastValueCurrent(refreshPeriod))
            {
                return (ushort)_attributes[ATTR_PRESENTVALUE].LastValue;
            }

            return (ushort)ReadSync(_attributes[ATTR_PRESENTVALUE]);
        }


        /// <summary>
        /// Set the Reliability attribute [attribute ID103].
        ///
        /// The Reliability attribute, of type 8-bit enumeration, provides an indication of whether
        /// the PresentValueor the operation of the physical input, output or value in question (as
        /// appropriate for the cluster) is “reliable” as far as can be determined and, if not, why
        /// not. The Reliability attribute MAY have any of the following values:
        /// 
        /// NO-FAULT-DETECTED (0)
        /// OVER-RANGE (2)
        /// UNDER-RANGE (3)
        /// OPEN-LOOP (4)
        /// SHORTED-LOOP (5)
        /// UNRELIABLE-OTHER (7)
        /// PROCESS-ERROR (8)
        /// MULTI-STATE-FAULT (9)
        /// CONFIGURATION-ERROR (10)
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <param name="reliability">The byte attribute value to be set</param>
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> SetReliability(object value)
        {
            return Write(_attributes[ATTR_RELIABILITY], value);
        }


        /// <summary>
        /// Get the Reliability attribute [attribute ID103].
        ///
        /// The Reliability attribute, of type 8-bit enumeration, provides an indication of whether
        /// the PresentValueor the operation of the physical input, output or value in question (as
        /// appropriate for the cluster) is “reliable” as far as can be determined and, if not, why
        /// not. The Reliability attribute MAY have any of the following values:
        /// 
        /// NO-FAULT-DETECTED (0)
        /// OVER-RANGE (2)
        /// UNDER-RANGE (3)
        /// OPEN-LOOP (4)
        /// SHORTED-LOOP (5)
        /// UNRELIABLE-OTHER (7)
        /// PROCESS-ERROR (8)
        /// MULTI-STATE-FAULT (9)
        /// CONFIGURATION-ERROR (10)
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetReliabilityAsync()
        {
            return Read(_attributes[ATTR_RELIABILITY]);
        }

        /// <summary>
        /// Synchronously Get the Reliability attribute [attribute ID103].
        ///
        /// The Reliability attribute, of type 8-bit enumeration, provides an indication of whether
        /// the PresentValueor the operation of the physical input, output or value in question (as
        /// appropriate for the cluster) is “reliable” as far as can be determined and, if not, why
        /// not. The Reliability attribute MAY have any of the following values:
        /// 
        /// NO-FAULT-DETECTED (0)
        /// OVER-RANGE (2)
        /// UNDER-RANGE (3)
        /// OPEN-LOOP (4)
        /// SHORTED-LOOP (5)
        /// UNRELIABLE-OTHER (7)
        /// PROCESS-ERROR (8)
        /// MULTI-STATE-FAULT (9)
        /// CONFIGURATION-ERROR (10)
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetReliability(long refreshPeriod)
        {
            if (_attributes[ATTR_RELIABILITY].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_RELIABILITY].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_RELIABILITY]);
        }


        /// <summary>
        /// Get the StatusFlags attribute [attribute ID111].
        ///
        /// This attribute, of type bitmap, represents four Boolean flags that indicate the general “health”
        /// of the analog sensor. Three of the flags are associated with the values of other optional attributes
        /// of this cluster. A more detailed status could be determined by reading the optional attributes (if
        /// supported) that are linked to these flags. The relationship between individual flags is not defined.
        /// 
        /// The four flags are Bit 0 = IN_ALARM, Bit 1 = FAULT, Bit 2 = OVERRIDDEN, Bit 3 = OUT OF SERVICE
        /// 
        /// where:
        /// 
        /// IN_ALARM -Logical FALSE (0) if the EventStateattribute has a value of NORMAL, otherwise logical TRUE (1).
        /// This bit is always 0 unless the cluster implementing the EventState attribute is implemented on the same
        /// endpoint.
        /// 
        /// FAULT -Logical TRUE (1) if the Reliability attribute is present and does not have a value of NO FAULT DETECTED,
        /// otherwise logical FALSE (0).
        /// 
        /// OVERRIDDEN -Logical TRUE (1) if the cluster has been overridden by some  mechanism local to the device.
        /// Otherwise, the value is logical FALSE (0). In this context, for an input cluster, “overridden” is taken
        /// to mean that the PresentValue and Reliability(optional) attributes are no longer tracking changes to the
        /// physical input. For an Output cluster, “overridden” is taken to mean that the physical output is no longer
        /// tracking changes to the PresentValue attribute and the Reliability attribute is no longer a reflection of
        /// the physical output. For a Value cluster, “overridden” is taken to mean that the PresentValue attribute is
        /// not writeable.
        /// 
        /// OUT OF SERVICE -Logical TRUE (1) if the OutOfService attribute has a value of TRUE, otherwise
        /// logical FALSE (0).
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetStatusFlagsAsync()
        {
            return Read(_attributes[ATTR_STATUSFLAGS]);
        }

        /// <summary>
        /// Synchronously Get the StatusFlags attribute [attribute ID111].
        ///
        /// This attribute, of type bitmap, represents four Boolean flags that indicate the general “health”
        /// of the analog sensor. Three of the flags are associated with the values of other optional attributes
        /// of this cluster. A more detailed status could be determined by reading the optional attributes (if
        /// supported) that are linked to these flags. The relationship between individual flags is not defined.
        /// 
        /// The four flags are Bit 0 = IN_ALARM, Bit 1 = FAULT, Bit 2 = OVERRIDDEN, Bit 3 = OUT OF SERVICE
        /// 
        /// where:
        /// 
        /// IN_ALARM -Logical FALSE (0) if the EventStateattribute has a value of NORMAL, otherwise logical TRUE (1).
        /// This bit is always 0 unless the cluster implementing the EventState attribute is implemented on the same
        /// endpoint.
        /// 
        /// FAULT -Logical TRUE (1) if the Reliability attribute is present and does not have a value of NO FAULT DETECTED,
        /// otherwise logical FALSE (0).
        /// 
        /// OVERRIDDEN -Logical TRUE (1) if the cluster has been overridden by some  mechanism local to the device.
        /// Otherwise, the value is logical FALSE (0). In this context, for an input cluster, “overridden” is taken
        /// to mean that the PresentValue and Reliability(optional) attributes are no longer tracking changes to the
        /// physical input. For an Output cluster, “overridden” is taken to mean that the physical output is no longer
        /// tracking changes to the PresentValue attribute and the Reliability attribute is no longer a reflection of
        /// the physical output. For a Value cluster, “overridden” is taken to mean that the PresentValue attribute is
        /// not writeable.
        /// 
        /// OUT OF SERVICE -Logical TRUE (1) if the OutOfService attribute has a value of TRUE, otherwise
        /// logical FALSE (0).
        ///
        /// The attribute is of type byte.
        ///
        /// The implementation of this attribute by a device is MANDATORY
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public byte GetStatusFlags(long refreshPeriod)
        {
            if (_attributes[ATTR_STATUSFLAGS].IsLastValueCurrent(refreshPeriod))
            {
                return (byte)_attributes[ATTR_STATUSFLAGS].LastValue;
            }

            return (byte)ReadSync(_attributes[ATTR_STATUSFLAGS]);
        }


        /// <summary>
        /// Get the ApplicationType attribute [attribute ID256].
        ///
        /// The ApplicationType attribute is an unsigned 32 bit integer that indicates the specific
        /// application usage for this cluster. (Note: This attribute has no BACnet equivalent).
        /// ApplicationType is subdivided into Group, Type and an Index number, as follows.
        /// 
        /// Group = Bits 24-31 An indication of the cluster this attribute is part of.
        /// 
        /// Type = Bits 16-23 For Analog clusters, the physical quantity that the Present Value attribute
        /// of the cluster represents. For Binary and Multistate clusters, the application usage domain.
        /// 
        /// Index = Bits 0-15The specific application usage of the cluster.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public Task<CommandResult> GetApplicationTypeAsync()
        {
            return Read(_attributes[ATTR_APPLICATIONTYPE]);
        }

        /// <summary>
        /// Synchronously Get the ApplicationType attribute [attribute ID256].
        ///
        /// The ApplicationType attribute is an unsigned 32 bit integer that indicates the specific
        /// application usage for this cluster. (Note: This attribute has no BACnet equivalent).
        /// ApplicationType is subdivided into Group, Type and an Index number, as follows.
        /// 
        /// Group = Bits 24-31 An indication of the cluster this attribute is part of.
        /// 
        /// Type = Bits 16-23 For Analog clusters, the physical quantity that the Present Value attribute
        /// of the cluster represents. For Binary and Multistate clusters, the application usage domain.
        /// 
        /// Index = Bits 0-15The specific application usage of the cluster.
        ///
        /// The attribute is of type int.
        ///
        /// The implementation of this attribute by a device is OPTIONAL
        ///
        /// <returns>The Task<CommandResult> command result Task</returns>
        /// </summary>
        public int GetApplicationType(long refreshPeriod)
        {
            if (_attributes[ATTR_APPLICATIONTYPE].IsLastValueCurrent(refreshPeriod))
            {
                return (int)_attributes[ATTR_APPLICATIONTYPE].LastValue;
            }

            return (int)ReadSync(_attributes[ATTR_APPLICATIONTYPE]);
        }

    }
}
