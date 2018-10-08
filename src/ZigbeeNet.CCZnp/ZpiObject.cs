using BinarySerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ZigbeeNet.ZCL;
using System.Linq;

namespace ZigbeeNet.CC
{
    public class ZpiObject
    {
        public virtual MessageType Type { get; set; }

        public virtual SubSystem SubSystem { get; set; }

        public virtual byte CommandId { get; set; }

        public string Name { get; set; }

        public ArgumentCollection RequestArguments { get; set; }

        public ArgumentCollection ResponseArguments { get; set; }

        public ZpiObject()
        {
            RequestArguments = new ArgumentCollection();
            ResponseArguments = new ArgumentCollection();
        }

        public ZpiObject(SubSystem subSystem, byte cmdId)
        {
            SubSystem = subSystem;
            CommandId = cmdId;

            ZpiObject zpi = ZpiMeta.GetCommand(subSystem, cmdId);

            if (zpi != null)
            {
                this.Type = zpi.Type;
                this.Name = zpi.Name;
                this.RequestArguments = zpi.RequestArguments;
                this.ResponseArguments = zpi.ResponseArguments;
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

        public void Parse(MessageType type, int length, byte[] buffer, Action result = null)
        {
            ArgumentCollection arguments = new ArgumentCollection();
            if (type == MessageType.SRSP)
            {
                if(ResponseArguments != null)
                    arguments = ResponseArguments;
            }
            else if (type == MessageType.AREQ)
            {
                if(RequestArguments != null)
                    arguments = RequestArguments;
            }

            int index = 0;
            int preLen = -1;
            foreach (ZpiArgument argument in arguments.Arguments)
            {
                switch (argument.ParamType)
                {
                    case ParamType.uint8:
                        argument.Value = buffer[index];
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
                        argument.Value = BitConverter.ToInt64(buffer, index);
                        index += 8;
                        break;
                    case ParamType.buffer:
                        ZpiArgument argLen = ResponseArguments.Arguments.SingleOrDefault(arg => arg.Name == "len");
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
                        index += 1;
                        break;
                    case ParamType._preLenUint16:
                        byte[] lenValue = new byte[2];
                        Array.Copy(buffer, index, lenValue, 0, 2);

                        preLen = BitConverter.ToInt32(lenValue, 0);
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
                    case ParamType.preLenList:
                        preLen = Convert.ToInt32(buffer[index]);
                        index += 1;
                        break;
                    case ParamType.listbuffer:
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
            result();
        }

        public override string ToString()
        {
            return $"{SubSystem} - {Type} - {CommandId} {Name}";
        }
    }
}
