using BinarySerialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public class ZclCommand
    {
        [Ignore()]
        public byte Id { get; set; }

        [Ignore()]
        public string Name { get; set; }
        
        [FieldOrder(0)]
        public List<ZclCommandParam> Params { get; set; }

        public event EventHandler<ZclCommand> OnParsed;
        public void Parsed(ZclCommand zclCommand)
        {
            OnParsed?.Invoke(this, zclCommand);
        }

        public event EventHandler<ZclCommand> OnResponse;
        public void Response(ZclCommand zclCommand)
        {
            OnResponse?.Invoke(this, zclCommand);
        }

        public ZclCommand()
        {
            Params = new List<ZclCommandParam>();
        }

        public byte[] Frame
        {
            get
            {
                if (this.Params != null)
                {
                    var serializer = new BinarySerializer();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        serializer.Serialize(stream, Params);

                        return stream.ToArray();
                    }
                }

                return null;
            }
        }

        public virtual void Parse(byte[] buffer)
        {
            throw new NotImplementedException();

            foreach (ZclCommandParam param in Params)
            {

            }

            Parsed(this);
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }
}
