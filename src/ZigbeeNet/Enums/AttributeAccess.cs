using System;
using System.Collections.Generic;
using System.Text;

namespace ZigbeeNet
{
    public enum AttributeAccess 
    {
        None = 0,
        /// <summary>
        /// global commands that read the attribute value
        /// </summary>
        Read = 1,
        /// <summary>
        /// global commands that write a new value to the attribute 
        /// </summary>
        Write = 2,
        /// <summary>
        /// global commands that report the value attribute or configure the attribute for reporting 
        /// </summary>
        Report = 3,
        /// <summary>
        /// if a Scenes cluster instance is on the same endpoint, then the attribute is accessed through a scene 
        /// </summary>
        Scene = 4
    }
}
