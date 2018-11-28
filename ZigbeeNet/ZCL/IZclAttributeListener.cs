using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet.ZCL
{
    public interface IZclAttributeListener
    {
        void AttributeUpdated(ZclAttribute attribute);
    }
}
