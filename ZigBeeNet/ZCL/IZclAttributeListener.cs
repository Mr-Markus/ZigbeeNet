using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL
{
    public interface IZclAttributeListener
    {
        void AttributeUpdated(ZclAttribute attribute);
    }
}
