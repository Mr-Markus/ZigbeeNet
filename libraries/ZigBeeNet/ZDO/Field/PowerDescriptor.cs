using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Field
{
    /// <summary>
    /// The node power descriptor gives a dynamic indication of the power status of the
    /// node and is mandatory for each node. There shall be only one node power
    /// descriptor in a node.
    /// </summary>
    public class PowerDescriptor
    {
        [Flags]
        public enum PowerSourceType : byte
        {
            /// <summary>
             /// Mains power - continuous
             /// </summary>
            MAINS = 0x01,
            /// <summary>
             /// Rechargable battery
             /// </summary>
            RECHARGABLE_BATTERY = 0x02,
            /// <summary>
             /// Disposable battery
             /// </summary>
            DISPOSABLE_BATTERY = 0x04,
            /// <summary>
             /// Default for unknown
             /// </summary>
            UNKNOWN = 0xff,
        }

        public enum PowerLevelType : byte
        {
            /// <summary>
             /// Power level is critical - battery should be changed immediately
             /// </summary>
            CRITICAL = 0x0,
            /// <summary>
             /// Power is low (below 33%)
             /// </summary>
            LOW = 0x04,
            /// <summary>
             /// Power is medium (33% to 66%)
             /// </summary>
            MEDIUM = 0x8,
            /// <summary>
             /// Power is full (above 66%)
             /// </summary>
            FULL = 0xc,
            /// <summary>
             /// Default for unknown
             /// </summary>
            UNKNOWN = 0xff
        }

        /// <summary>
         /// The current power mode field of the node power descriptor is four bits in length
         /// and specifies the current sleep/power-saving mode of the node.
         /// </summary>
        public enum CurrentPowerModeType : byte
        {
            /// <summary>
             /// Receiver synchronized with the receiver on when idle subfield
             /// of the node descriptor.
             /// </summary>
            RECEIVER_ON_IDLE = 0x0,
            /// <summary>
             /// Receiver comes on periodically as defined by the node
             /// power descriptor.
             /// </summary>
            RECEIVER_ON_PERIODICALLY = 0x1,
            /// <summary>
             /// Receiver comes on when stimulated, e.g. by a user pressing a
             /// button.
             /// </summary>
            RECEIVER_ON_STIMULATED = 0x2,
            /// <summary>
             /// Default for unknown
             /// </summary>
            UNKNOWN = 0xff
        }

        public CurrentPowerModeType CurrentPowerMode { get; private set; } = CurrentPowerModeType.UNKNOWN;
        public PowerSourceType AvailablePowerSources { get; private set; } = default;
        public PowerSourceType CurrentPowerSource { get; private set; } = PowerSourceType.UNKNOWN;
        public PowerLevelType PowerLevel { get; private set; } = PowerLevelType.UNKNOWN;

        public PowerDescriptor()
        {
            // Default constructor - does nothing
        }

        /// <summary>
         ///
         /// <param name="currentPowerMode"><see cref="CurrentPowerModeType"></param>
         /// <param name="availablePowerSources"><see cref="Set"> of available <see cref="PowerSourceType"></param>
         /// <param name="currentPowerSource">{@linkPowerSourceType }</param>
         /// <param name="powerLevel"><see cref="PowerLevelType"></param>
         /// </summary>
        public PowerDescriptor(CurrentPowerModeType currentPowerMode, PowerSourceType availablePowerSources, PowerSourceType currentPowerSource, PowerLevelType powerLevel)
        {
            CurrentPowerMode = currentPowerMode;
            AvailablePowerSources = availablePowerSources;
            CurrentPowerSource = currentPowerSource;
            PowerLevel = powerLevel;
        }

        /// <summary>
         /// Creates a PowerDescriptor
         ///
         /// @param currentPowerMode
         /// @param availablePowerSources
         /// @param currentPowerSource
         /// @param powerLevel
         /// </summary>
        public PowerDescriptor(byte currentPowerMode, byte availablePowerSources, byte currentPowerSource, byte powerLevel)
        {
            SetCurrentPowerMode(currentPowerMode);
            SetAvailablePowerSources(availablePowerSources);
            SetCurrentPowerSource(currentPowerSource);
            SetCurrentPowerLevel(powerLevel);
        }

        public void SetCurrentPowerSource(byte currentPowerSource) // 4bits used
        {
            CurrentPowerSource = (PowerSourceType)(currentPowerSource&0x0f);
            if (CurrentPowerSource!=PowerSourceType.MAINS && 
                CurrentPowerSource!=PowerSourceType.RECHARGABLE_BATTERY &&
                CurrentPowerSource!=PowerSourceType.DISPOSABLE_BATTERY) 
                CurrentPowerSource = PowerSourceType.UNKNOWN;
        }

        public void SetAvailablePowerSources(byte availablePowerSources)
        {
            AvailablePowerSources = (PowerSourceType)(availablePowerSources & 0x0f);
        }

        public void SetCurrentPowerLevel(byte powerLevel)
        {
            PowerLevel = (PowerLevelType)powerLevel;
            if (PowerLevel != PowerLevelType.FULL &&
                PowerLevel != PowerLevelType.MEDIUM &&
                PowerLevel != PowerLevelType.LOW &&
                PowerLevel != PowerLevelType.CRITICAL)
                PowerLevel = PowerLevelType.UNKNOWN;
        }

        /// <summary>
         /// Sets the current power mode for the descriptor
         ///
         /// <param name="currentPowerMode">the <see cref="CurrentPowerModeType"></param>
         /// </summary>
        public void SetCurrentPowerMode(byte currentPowerMode)
        {
            CurrentPowerMode = currentPowerMode>0x02 ? CurrentPowerModeType.UNKNOWN : (CurrentPowerModeType)currentPowerMode;
        }

        /// <summary>
         /// Serialise the contents of the structure.
         ///
         /// <param name="serializer">the <see cref="ZclFieldSerializer"> used to serialize</param>
         /// </summary>
        public byte[] Serialize(ZclFieldSerializer serializer)
        {
            // Serialize the fields

            return serializer.Payload;
        }

        /// <summary>
         /// Deserialise the contents of the structure.
         ///
         /// <param name="deserializer">the <see cref="ZigBeeDeserializer"> used to deserialize</param>
         /// </summary>
        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            // Deserialize the fields
            byte byte1 = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            byte byte2 = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);

            SetCurrentPowerMode((byte)(byte1 & 0x0f));
            SetAvailablePowerSources((byte)(byte1 >> 4));
            SetCurrentPowerSource((byte)(byte2 & 0x0f));
            SetCurrentPowerLevel((byte)(byte2 >> 4));
        }

        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result += prime * AvailablePowerSources.GetHashCode();
            result += prime * CurrentPowerMode.GetHashCode();
            result += prime * CurrentPowerSource.GetHashCode();
            result += prime * PowerLevel.GetHashCode();
            return result;
        }

        public override bool Equals(object obj)
        {
            return (this == obj)
                || !(obj is null)
                    && (obj is PowerDescriptor other)
                    && AvailablePowerSources==other.AvailablePowerSources
                    && CurrentPowerMode == other.CurrentPowerMode
                    && CurrentPowerSource == other.CurrentPowerSource
                    && PowerLevel == other.PowerLevel;
        }

        public override string ToString()
        {
            return CurrentPowerMode + ", " +AvailablePowerSources + ", " + CurrentPowerSource + ", " + PowerLevel;
        }
    }
}
