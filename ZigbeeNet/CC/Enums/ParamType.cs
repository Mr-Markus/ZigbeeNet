using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.CC
{
    public enum ParamType {
        uint8 = 0,
        uint16 = 1,
        uint32 = 2,
        longaddr = 3,
        zdomsgcb = 4,
        devlistbuffer = 5,
        nwklistbuffer = 6,
        _preLenUint8 = 7,
        _preLenUint16 = 8,
        preLenList = 9,
        preLenBeaconlist = 10,
        dynbuffer = 11,
        listbuffer = 12,
        buffer = 13,
        buffer8 = 14,
        buffer16 = 15,
        buffer18 = 16,
        buffer32 = 17,
        buffer42 = 18,
        buffer100 = 19,
        uint8ZdoInd = 20,
        uint32be = 21,
        unkown = 255
    }
}
