using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.Security;

namespace ZigBeeNet.Hardware.Digi.XBee.Internal.Protocol
{
    /// <summary>
    /// Base class for all XBee frames. Provides methods to serialize and deserialize data.
    /// </summary>
    public class XBeeFrame
    {
        protected int[] _buffer = new int[131];
        protected int _length = 0;
        protected int _position = 0;
        protected int _pushPosition = 0;
        private int _frameType;

        /// <summary>
        /// Initializes the deserializer. This sets the data for the deserializer methods to use.
        /// </summary>
        /// <param name="data">The incoming data to deserialize.</param>
        protected void InitializeDeserializer(int[] data)
        {
            _buffer = data;
            _length = data[0] << 8 + data[1];
            _frameType = data[2];
            _position = 3;
        }

        /// <summary>
        /// Determines whether this instance is complete.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is complete; otherwise, <c>false</c>.
        /// </returns>
        protected bool IsComplete()
        {
            return (_position >= (_buffer.Length - 1));
        }

        /// <summary>
        /// Serializes the command. This should be the first method called when serializing a command. In addition to
        /// serializing the command, it initializes internal variables used to properly create the command packet.
        /// </summary>
        /// <param name="command">The command ID to serialize.</param>
        protected void SerializeCommand(int command)
        {
            _length = 3;
            // buffer[0] = START_DELIMITER;
            _buffer[0] = 0;
            _buffer[1] = 0;
            _buffer[2] = command;
        }

        /// <summary>
        /// Serializes an 8 bit integer in hexadecimal (2 characters long)
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        protected void SerializeInt8(int value)
        {
            _buffer[_length++] = value & 0xFF;
        }

        /// <summary>
        /// Deserializes an 8 bit integer.
        /// </summary>
        /// <returns>Dezerialized value</returns>
        protected int DeserializeInt8()
        {
            return _buffer[_position++];
        }

        /// <summary>
        /// Serializes an 16 bit integer.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        protected void SerializeInt16(int value)
        {
            _buffer[_length++] = (value >> 8) & 0xFF;
            _buffer[_length++] = value & 0xFF;
        }

        /// <summary>
        /// Deserializes an 16 bit integer.
        /// </summary>
        /// <returns>The deserialized value</returns>
        protected int DeserializeInt16()
        {
            return (_buffer[_position++] << 8) + _buffer[_position++];
        }

        /// <summary>
        /// Serializes a boolean.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        protected void SerializeBoolean(bool value)
        {
            _buffer[_length++] = value ? 1 : 0;
        }

        /// <summary>
        /// Deserializes a boolean.
        /// </summary>
        /// <returns>The deserialized value</returns>
        protected bool DeserializeBoolean()
        {
            return _buffer[_position++] != 0;
        }

        /// <summary>
        /// Serializes a string. Used by other serialize methods to commit data to the output packet.
        /// </summary>
        /// <param name="value">The string to serialize.</param>
        protected void SerializeAtCommand(string value)
        {
            char[] valueChars = value.ToCharArray();
            for (int cnt = 0; cnt < value.Length; cnt++)
            {
                _buffer[_length++] = valueChars[cnt];
            }
        }

        /// <summary>
        /// Deserializes at command.
        /// </summary>
        /// <returns>The deserialized at command.</returns>
        protected string DeserializeAtCommand()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append((char)_buffer[_position++]);
            stringBuilder.Append((char)_buffer[_position++]);

            return stringBuilder.ToString();
        }

        ///// <summary>
        ///// Serializes the extended pan identifier.
        ///// </summary>
        ///// <param name="epanId">The epan identifier.</param>
        //protected void SerializeExtendedPanId(ExtendedPanId epanId)
        //{
        //    SerializeUpperCaseString(epanId.ToString());
        //}

        /// <summary>
        /// Deserializes an ExtendedPanId object <seealso cref="ExtendedPanId"/>
        /// </summary>
        /// <returns>The ExtendePanId object <seealso cref="ExtendedPanId"/></returns>
        protected ExtendedPanId DeserializeExtendedPanId()
        {
            byte[] ePanId = new byte[8];
            for (int cnt = 7; cnt >= 0; cnt--)
            {
                ePanId[cnt] = Convert.ToByte(_buffer[_position++]);
            }
            return new ExtendedPanId(ePanId);
        }

        /// <summary>
        /// Serializes an integer data array
        /// </summary>
        /// <param name="values">The int[] array to send</param>
        protected void SerializeData(int[] values)
        {
            if (values == null)
            {
                return;
            }
            foreach (var value in values)
            {
                _buffer[_length++] = value;
            }
        }

        /// <summary>
        /// Deserializes binary data
        /// </summary>
        /// <returns>The int[] array containing the received data</returns>
        protected int[] DeserializeData()
        {
            if (_position == _buffer.Length - 1)
            {
                return null;
            }

            int[] data = new int[_buffer.Length - _position - 1];
            for (int cnt = 0; cnt < data.Length; cnt++)
            {
                data[cnt] = _buffer[_position++];
            }

            return data;
        }

        /// <summary>
        /// Serializes an IeeeAddress <seealso cref="IeeeAddress"/>
        /// </summary>
        /// <param name="address">The IeeeAddress <seealso cref="IeeeAddress"/></param>
        protected void SerializeIeeeAddress(IeeeAddress address)
        {
            for (int cnt = 7; cnt >= 0; cnt--)
            {
                _buffer[_length++] = address.GetAddress()[cnt];
            }
        }

        /// <summary>
        /// Deserializes a IeeeAddress <seealso cref="IeeeAddress"/>
        /// </summary>
        /// <returns>The IeeeAddress <seealso cref="IeeeAddress"/></returns>
        protected IeeeAddress DeserializeIeeeAddress()
        {
            byte[] address = new byte[8];
            for (int cnt = 7; cnt >= 0; cnt--)
            {
                address[cnt] = (byte)_buffer[_position++];
            }

            return new IeeeAddress(address);
        }

        /// <summary>
        /// Serializes a ZigBeeKey <seealso cref="ZigBeeKey"/>
        /// </summary>
        /// <param name="key">The ZigBeeKey <seealso cref="ZigBeeKey"/></param>
        protected void SerializeZigBeeKey(ZigBeeKey key)
        {
            for (int cnt = 0; cnt <= 15; cnt++)
            {
                _buffer[_length++] = key.Key[cnt];
            }
        }

        /// <summary>
        /// Deserializes a ZigBeeKey <see cref="ZigBeeKey"/>
        /// </summary>
        /// <returns>The ZigBeeKey <see cref="ZigBeeKey"/></returns>
        protected ZigBeeKey DeserializeZigBeeKey()
        {
            byte[] address = new byte[16];
            for (int cnt = 7; cnt >= 0; cnt--)
            {
                address[cnt] = (byte)_buffer[_position++];
            }
            return new ZigBeeKey(address);
        }

        /// <summary>
        /// Serializes a TransmitOptions <seealso cref="TransmitOptions"/>
        /// </summary>
        /// <param name="options">The list of Transmitoptions to serialize <seealso cref="TransmitOptions"/></param>
        protected void SerializeTransmitOptions(List<TransmitOptions> options)
        {
            int value = 0;
            foreach (TransmitOptions option in options)
            {
                value += (int)option;
            }
            _buffer[_length++] = value;
        }

        /// <summary>
        /// Deserializes a ReceiveOptions <seealso cref="ReceiveOptions"/>
        /// </summary>
        /// <returns>The ReceiveOptions <seealso cref="ReceiveOptions"/></returns>
        protected ReceiveOptions DeserializeReceiveOptions()
        {
            int value = DeserializeInt8();
            //return ReceiveOptions.GetReceiveOptions();
            return (ReceiveOptions)value;
        }

        protected List<EncryptionOptions> DeserializeEncryptionOptions()
        {
            List<EncryptionOptions> options = new List<EncryptionOptions>();
            foreach (EncryptionOptions option in Enum.GetValues(typeof(EncryptionOptions)))
            {
                if ((_buffer[_position] & (int)option) != 0)
                {
                    options.Add(option);
                }
            }
            _position++;

            return options;
        }

        /// <summary>
        /// Serializes a EncryptionOptions <seealso cref="EncryptionOptions"/>
        /// </summary>
        /// <param name="options">List of EncryptionOptions to serialize <seealso cref="EncryptionOptions"/></param>
        protected void SerializeEncryptionOptions(List<EncryptionOptions> options)
        {
            int value = 0;
            foreach (EncryptionOptions option in options)
            {
                value += (int)option;
            }
            _buffer[_length++] = value;
        }

        /// <summary>
        /// Deserializes the modem status <seealso cref="ModemStatus"/>.
        /// </summary>
        /// <returns>The ModemStatus <seealso cref="ModemStatus"/></returns>
        protected ModemStatus DeserializeModemStatus()
        {
            int value = DeserializeInt8();
            return (ModemStatus)value;
        }

        /// <summary>
        /// Deserializes a DiscoveryStatus <seealso cref="DiscoveryStatus"/>
        /// </summary>
        /// <returns>The DiscoveryStatus <seealso cref="DiscoveryStatus"/></returns>
        protected DiscoveryStatus DeserializeDiscoveryStatus()
        {
            int value = DeserializeInt8();
            return (DiscoveryStatus)value;
        }

        /// <summary>
        /// Deserializes a CommandStatus <seealso cref="CommandStatus"/>
        /// </summary>
        /// <returns>The CommandStatus <seealso cref="CommandStatus"/></returns>
        protected CommandStatus DeserializeCommandStatus()
        {
            int value = DeserializeInt8();

            return (CommandStatus)value;
        }

        /// <summary>
        /// Deserializes a DeliveryStatus <seealso cref="DeliveryStatus"/>
        /// </summary>
        /// <returns>The DeliveryStatus <seealso cref="DeliveryStatus"/></returns>
        protected DeliveryStatus DeserializeDeliveryStatus()
        {
            int value = DeserializeInt8();
            return (DeliveryStatus)value;
        }

        protected void SerializeInt16Array(int[] array)
        {
            foreach (int value in array)
            {
                SerializeInt16(value);
            }
        }

        protected int[] DeserializeInt16Array(int size)
        {
            int[] array = new int[size];
            for (int cnt = 0; cnt < size; cnt++)
            {
                array[cnt] = DeserializeInt16();
            }

            return array;
        }

        public void SetFrameType(int frameType)
        {
            _frameType = frameType;
        }

        public int GetFrameType()
        {
            return _frameType;
        }

        /// <summary>
        /// Returns the serialized data. This is called at the send of the desrialization process is complete to allow
        /// sending of the data
        /// </summary>
        /// <returns>The int array to send over the wire</returns>
        protected int[] GetPayload()
        {
            // Update the length
            int dataLen = _length - 2;
            _buffer[0] = (dataLen >> 8) & 0xff;
            _buffer[1] = dataLen & 0xff;

            int checksum = 0;
            for (int cnt = 2; cnt < _length; cnt++)
            {
                checksum += _buffer[cnt];
            }

            _buffer[_length++] = 0xff - (checksum & 0xff);

            Array payLoad = Array.CreateInstance(typeof(int), _length);
            Array.Copy(_buffer, payLoad, _length);
            return (int[])payLoad;
        }
    }
}
