using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Serialization;
using ZigBeeNet.ZCL;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZDO.Field
{
    /**
    * The node power descriptor gives a dynamic indication of the power status of the
    * node and is mandatory for each node. There shall be only one node power
    * descriptor in a node.
    */
    public class PowerDescriptor
    {
        public enum PowerSourceType
        {
            /**
             * Mains power - continuous
             */
            MAINS,
            /**
             * Rechargable battery
             */
            RECHARGABLE_BATTERY,
            /**
             * Disposable battery
             */
            DISPOSABLE_BATTERY,
            /**
             * Default for unknown
             */
            UNKNOWN
        }

        public enum PowerLevelType
        {
            /**
             * Power level is critical - battery should be changed immediately
             */
            CRITICAL,
            /**
             * Power is low (below 33%)
             */
            LOW,
            /**
             * Power is medium (33% to 66%)
             */
            MEDIUM,
            /**
             * Power is full (above 66%)
             */
            FULL,
            /**
             * Default for unknown
             */
            UNKNOWN
        }

        /**
         * The current power mode field of the node power descriptor is four bits in length
         * and specifies the current sleep/power-saving mode of the node.
         */
        public enum CurrentPowerModeType
        {
            /**
             * Receiver synchronized with the receiver on when idle subfield
             * of the node descriptor.
             */
            RECEIVER_ON_IDLE,
            /**
             * Receiver comes on periodically as defined by the node
             * power descriptor.
             */
            RECEIVER_ON_PERIODICALLY,
            /**
             * Receiver comes on when stimulated, e.g. by a user pressing a
             * button.
             */
            RECEIVER_ON_STIMULATED,
            /**
             * Default for unknown
             */
            UNKNOWN
        }

        public CurrentPowerModeType CurrentPowerMode { get; private set; } = CurrentPowerModeType.UNKNOWN;
        public List<PowerSourceType> AvailablePowerSources { get; private set; } = new List<PowerSourceType>();
        public PowerSourceType CurrentPowerSource { get; private set; } = PowerSourceType.UNKNOWN;
        public PowerLevelType PowerLevel { get; private set; } = PowerLevelType.UNKNOWN;

        public PowerDescriptor()
        {
            // Default constructor - does nothing
        }

        /**
         *
         * @param currentPowerMode {@link CurrentPowerModeType}
         * @param availablePowerSources {@link Set} of available {@link PowerSourceType}
         * @param currentPowerSource {@linkPowerSourceType }
         * @param powerLevel {@link PowerLevelType}
         */
        public PowerDescriptor(CurrentPowerModeType currentPowerMode, List<PowerSourceType> availablePowerSources, PowerSourceType currentPowerSource, PowerLevelType powerLevel)
        {
            this.CurrentPowerMode = currentPowerMode;
            this.AvailablePowerSources = availablePowerSources;
            this.CurrentPowerSource = currentPowerSource;
            this.PowerLevel = powerLevel;
        }

        /**
         * Creates a PowerDescriptor
         *
         * @param currentPowerMode
         * @param availablePowerSources
         * @param currentPowerSource
         * @param powerLevel
         */
        public PowerDescriptor(int currentPowerMode, int availablePowerSources, int currentPowerSource, int powerLevel)
        {
            setCurrentPowerMode(currentPowerMode);
            SetAvailablePowerSources(availablePowerSources);
            setCurrentPowerSource(currentPowerSource);
            SetCurrentPowerLevel(powerLevel);
        }

        public void setCurrentPowerSource(int currentPowerSource)
        {
            switch (currentPowerSource)
            {
                case 0x01:
                    this.CurrentPowerSource = PowerSourceType.MAINS;
                    break;
                case 0x02:
                    this.CurrentPowerSource = PowerSourceType.RECHARGABLE_BATTERY;
                    break;
                case 0x04:
                    this.CurrentPowerSource = PowerSourceType.DISPOSABLE_BATTERY;
                    break;
                default:
                    this.CurrentPowerSource = PowerSourceType.UNKNOWN;
                    break;
            }
        }

        public void SetAvailablePowerSources(int availablePowerSources)
        {
            this.AvailablePowerSources = new List<PowerSourceType>();
            if ((availablePowerSources & 0x01) != 0)
            {
                this.AvailablePowerSources.Add(PowerSourceType.MAINS);
            }
            if ((availablePowerSources & 0x02) != 0)
            {
                this.AvailablePowerSources.Add(PowerSourceType.RECHARGABLE_BATTERY);
            }
            if ((availablePowerSources & 0x04) != 0)
            {
                this.AvailablePowerSources.Add(PowerSourceType.DISPOSABLE_BATTERY);
            }
        }

        public void SetCurrentPowerLevel(int powerLevel)
        {
            switch (powerLevel)
            {
                case 0xc:
                    this.PowerLevel = PowerLevelType.FULL;
                    break;
                case 0x8:
                    this.PowerLevel = PowerLevelType.MEDIUM;
                    break;
                case 0x4:
                    this.PowerLevel = PowerLevelType.LOW;
                    break;
                case 0x0:
                    this.PowerLevel = PowerLevelType.CRITICAL;
                    break;
                default:
                    this.PowerLevel = PowerLevelType.UNKNOWN;
                    break;
            }
        }

        /**
         * Sets the current power mode for the descriptor
         *
         * @param currentPowerMode the {@link CurrentPowerModeType}
         */
        public void setCurrentPowerMode(int currentPowerMode)
        {
            switch (currentPowerMode)
            {
                case 0x00:
                    this.CurrentPowerMode = CurrentPowerModeType.RECEIVER_ON_IDLE;
                    break;
                case 0x01:
                    this.CurrentPowerMode = CurrentPowerModeType.RECEIVER_ON_PERIODICALLY;
                    break;
                case 0x02:
                    this.CurrentPowerMode = CurrentPowerModeType.RECEIVER_ON_STIMULATED;
                    break;
                default:
                    this.CurrentPowerMode = CurrentPowerModeType.UNKNOWN;
                    break;
            }
        }

        /**
         * Serialise the contents of the structure.
         *
         * @param serializer the {@link ZclFieldSerializer} used to serialize
         */
        public byte[] Serialize(ZclFieldSerializer serializer)
        {
            // Serialize the fields

            return serializer.Payload;
        }

        /**
         * Deserialise the contents of the structure.
         *
         * @param deserializer the {@link ZigBeeDeserializer} used to deserialize
         */
        public void Deserialize(IZigBeeDeserializer deserializer)
        {
            // Deserialize the fields
            byte byte1 = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);
            byte byte2 = deserializer.ReadZigBeeType<byte>(DataType.UNSIGNED_8_BIT_INTEGER);

            setCurrentPowerMode(byte1 & 0x0f);
            SetAvailablePowerSources(byte1 >> 4 & 0x0f);
            setCurrentPowerSource(byte2 & 0x0f);
            SetCurrentPowerLevel(byte2 >> 4 & 0x0f);
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + ((AvailablePowerSources == null) ? 0 : AvailablePowerSources.GetHashCode());
            result = prime * result + /*((CurrentPowerMode == null) ? 0 :*/ CurrentPowerMode.GetHashCode();
            result = prime * result + /*((CurrentPowerSource == null) ? 0 :*/ CurrentPowerSource.GetHashCode();
            result = prime * result + /*((PowerLevel == null) ? 0 :*/ PowerLevel.GetHashCode();
            return result;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            PowerDescriptor other = (PowerDescriptor)obj;
            if (AvailablePowerSources == null)
            {
                if (other.AvailablePowerSources != null)
                {
                    return false;
                }
            }
            else if (!AvailablePowerSources.Equals(other.AvailablePowerSources))
            {
                return false;
            }
            if (CurrentPowerMode != other.CurrentPowerMode)
            {
                return false;
            }
            if (CurrentPowerSource != other.CurrentPowerSource)
            {
                return false;
            }
            if (PowerLevel != other.PowerLevel)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return CurrentPowerMode + ", " + AvailablePowerSources + ", " + CurrentPowerSource + ", " + PowerLevel;
        }
    }
}
