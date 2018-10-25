using BinarySerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using ZigbeeNet.CC.ZDO;

namespace ZigbeeNet.CC
{
    public class ZpiObject
    {
        public virtual MessageType Type { get; set; }

        public virtual SubSystem SubSystem { get; set; }

        public virtual byte CommandId { get; set; }

        public string Name { get; set; }
        
        private ZpiObject _indObject;
        public ZpiObject IndObject
        {
            get
            {
                if(_indObject == null)
                {
                    if (SubSystem == SubSystem.ZDO)
                    {
                        if (ZdoMeta.ZdoObjects.ContainsKey((ZdoCommand)CommandId))
                        {
                            ZdoMetaItem zdo = ZdoMeta.ZdoObjects[(ZdoCommand)CommandId];

                            _indObject = ZpiMeta.GetCommand(SubSystem.ZDO, (byte)zdo.ResponseInd);
                        }
                    }
                }
                return _indObject;
            }
            set
            {
                _indObject = value;
            }
        }

        public ArgumentCollection RequestArguments { get; set; }

        public event EventHandler<ZpiObject> OnParsed;
        public void Parsed(ZpiObject zpiObject)
        {
            OnParsed?.Invoke(this, zpiObject);
        }

        public event EventHandler<ZpiObject> OnResponse;
        public void Response(ZpiObject zpiObject)
        {
            OnResponse?.Invoke(this, zpiObject);
        }

        public ZpiObject()
        {
            RequestArguments = new ArgumentCollection();
        }

        public ZpiObject(CommandType commandType, MessageType type)
            : this(commandType)
        {
            Type = type;
        }

        public ZpiObject(CommandType commandType)
        {
            DoubleByte doubleByte = new DoubleByte((ushort)commandType);

            SubSystem = (SubSystem)doubleByte.GetMsb();
            CommandId = doubleByte.GetLsb();

            ZpiObject zpi = ZpiMeta.GetCommand(SubSystem, CommandId);

            if (zpi != null)
            {
                this.Type = zpi.Type;
                this.Name = zpi.Name;
                this.RequestArguments = zpi.RequestArguments;
            }
        }

        public byte[] Frame
        {
            get
            {
                if (this.RequestArguments != null)
                {
                    var serializer = new BinarySerializer();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.Serialize(stream, RequestArguments.Arguments);

                        return stream.ToArray();
                    }
                }

                return new byte[0];
            }
        }

        public async virtual void RequestAsync(IHardwareChannel znp)
        {
            byte[] data = await this.ToSerialPacket().ToFrame().ConfigureAwait(false);

            await znp.SendAsync(data).ConfigureAwait(false);
        }

        protected void ParseArguments(ArgumentCollection arguments, int length, byte[] buffer)
        {
            int index = 0;
            int preLen = -1;
            foreach (ZpiArgument argument in arguments.Arguments)
            {
                switch (argument.ParamType)
                {
                    case ParamType.uint8ZdoInd:
                        argument.Value = (byte)(index < buffer.Length ? (DeviceState)buffer[index] : 0);
                        break;
                    case ParamType.uint8:
                        argument.Value = (byte)(index < buffer.Length ? buffer[index] : 0);
                        index += 1;
                        break;
                    case ParamType.uint16:
                        argument.Value = BitConverter.ToUInt16(buffer, index);
                        index += 2;
                        break;
                    case ParamType.uint32:
                        argument.Value = BitConverter.ToUInt32(buffer, index);
                        index += 4;
                        break;
                    case ParamType.longaddr:
                        argument.Value = BitConverter.ToUInt64(buffer, index);
                        index += 8;
                        break;
                    case ParamType.buffer:
                        ZpiArgument argLen = RequestArguments.Arguments.SingleOrDefault(arg => arg.Name == "len");
                        if (argLen != null)
                        {
                            int len = (int)argLen.Value;
                            byte[] newValue = new byte[len];
                            Array.Copy(buffer, index, newValue, 0, len);
                            argument.Value = newValue;
                            index += len;
                        }
                        else
                        {
                            int len = length - 2;
                            byte[] newValue = new byte[length];
                            Array.Copy(buffer, index, newValue, 0, len);
                            argument.Value = newValue;
                            index += len;
                        }
                        break;
                    case ParamType.buffer8:
                        byte[] buf8 = new byte[8];
                        Array.Copy(buffer, index, buf8, 0, 8);
                        argument.Value = buf8;
                        index += 8;
                        break;
                    case ParamType.buffer16:
                        byte[] buf16 = new byte[16];
                        Array.Copy(buffer, index, buf16, 0, 8);
                        argument.Value = buf16;
                        index += 16;
                        break;
                    case ParamType.buffer18:
                        byte[] buf18 = new byte[18];
                        Array.Copy(buffer, index, buf18, 0, 8);
                        argument.Value = buf18;
                        index += 18;
                        break;
                    case ParamType.buffer32:
                        byte[] buf32 = new byte[32];
                        Array.Copy(buffer, index, buf32, 0, 8);
                        argument.Value = buf32;
                        index += 32;
                        break;
                    case ParamType.buffer42:
                        byte[] buf42 = new byte[32];
                        Array.Copy(buffer, index, buf42, 0, 8);
                        argument.Value = buf42;
                        index += 42;
                        break;
                    case ParamType.buffer100:
                        byte[] buf100 = new byte[100];
                        Array.Copy(buffer, index, buf100, 0, 8);
                        argument.Value = buf100;
                        index += 100;
                        break;
                    case ParamType.zdomsgcb:

                        break;
                    case ParamType._preLenUint8:
                        preLen = Convert.ToInt32(buffer[index]);

                        argument.Value = preLen;
                        index += 1;
                        break;
                    case ParamType._preLenUint16:
                        byte[] lenValue = new byte[2];
                        Array.Copy(buffer, index, lenValue, 0, 2);

                        preLen = BitConverter.ToInt32(lenValue, 0);
                        argument.Value = preLen;
                        index += 2;
                        break;
                    case ParamType.dynbuffer:
                        if (preLen >= 0)
                        {
                            byte[] dynbuf = new byte[preLen];
                            Array.Copy(buffer, index, dynbuf, 0, preLen);
                            argument.Value = dynbuf;
                            index += preLen;
                        }
                        break;
                    case ParamType.listbuffer:
                        if (preLen >= 0)
                        {
                            byte[] dynbuf = new byte[preLen * 2];
                            Array.Copy(buffer, index, dynbuf, 0, preLen * 2);
                            argument.Value = dynbuf;
                            index += preLen * 2;
                        }
                        break;
                    case ParamType.preLenList:
                        preLen = Convert.ToInt32(buffer[index]);
                        index += 1;
                        break;
                    case ParamType.devlistbuffer:
                        if (preLen >= 0)
                        {
                            List<ushort> ids = new List<ushort>();
                            for (int i = 0; i < preLen; i++)
                            {
                                byte[] id = new byte[2];
                                Array.Copy(buffer, index, id, 0, 2);

                                ushort idValue = BitConverter.ToUInt16(id, 0);

                                ids.Add(idValue);
                                index += 2;
                            }
                            argument.Value = ids;
                        }
                        break;
                    case ParamType.nwklistbuffer:
                        if (preLen >= 0)
                        {
                            List<Network> networkList = new List<Network>();
                            BinarySerializer serializer = new BinarySerializer();
                            for (int i = 0; i < preLen; i++)
                            {
                                byte[] nwk = new byte[6];
                                Array.Copy(buffer, index, nwk, 0, 6);

                                using (MemoryStream stream = new MemoryStream())
                                {
                                    Network network = serializer.Deserialize<Network>(nwk);
                                    networkList.Add(network);
                                }

                                index += 6;
                            }
                            argument.Value = networkList;
                        }
                        break;
                    default:
                        throw new Exception("Type not implemented");
                }
            }
        }

        public virtual void Parse(MessageType type, int length, byte[] buffer)
        {
            if(type != MessageType.SRSP)
            {
                ParseArguments(RequestArguments, length, buffer);
                
                Parsed(this);
            }
        }

        public SerialPacket ToSerialPacket()
        {
            return new SerialPacket(this.Type, this.SubSystem, this.CommandId, this.Frame);
        }

        public override string ToString()
        {
            return $"{SubSystem} - {Type} - {CommandId} {Name}";
        }
    }
}
