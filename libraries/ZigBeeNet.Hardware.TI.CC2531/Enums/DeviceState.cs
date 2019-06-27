using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.TI.CC2531
{
    public enum DeviceState : byte
    {
        Initialized_Not_Started_automatically = 0x00,
        Initialized_not_connected_to_anything = 0x01,
        Discovering_PANs_to_join = 0x02,
        Joining_a_PAN = 0x03,
        Rejoining_a_PAN_only_for_end_devices = 0x04,
        Joined_but_not_yet_authenticated_by_trust_center = 0x05,
        Started_as_device_after_authentication = 0x06,
        Device_joined_authenticated_and_is_a_router = 0x07,
        Starting_as_ZigBee_Coordinator = 0x08,
        Started_as_ZigBee_Coordinator = 0x09,
        Device_has_lost_information_about_its_parent  = 0x0A
    }
}
