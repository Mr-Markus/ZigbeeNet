using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZDO.Field
{
    public class ComplexDescriptor
    {
        public string ManufacturerName { get; private set; }
        public string ModelName { get; private set; }
        public string SerialNumber { get; private set; }

        public ComplexDescriptor(string manufacturerName, string modelName, string serialNumber)
        {
            this.ManufacturerName = manufacturerName;
            this.ModelName = modelName;
            this.SerialNumber = serialNumber;
        }

        public override string ToString()
        {
            return "ComplexDescriptor [manufacturerName=" + ManufacturerName + ", modelName=" + ModelName
                    + ", serialNumber=" + SerialNumber + "]";
        }
    }
}
