# ZigbeeNet [![Build status](https://ci.appveyor.com/api/projects/status/2c0c15ta3ow8pfib?svg=true)](https://ci.appveyor.com/project/Mr-Markus/zigbeenet)

ZigbeeNET is a implementation of the Zigbee 3.0 Cluster Library for .NET Standard, .NET Core and more .NET platforms. 

With ZigbeeNet you can develop your own .NET application which communicates with zigbee devices

## What is Zigbee?

Zigbee is a worldwide standard for low power, self-healing, mesh networks offering a complete and interoperable IoT solution for home and building automation:
- The industry-proven worldwide standard for robust mesh networking
- Capable of supporting hundreds of nodes
- Interoperability across certified Zigbee devices
- Enhanced security

<img src="http://www.ti.com/content/dam/ticom/images/products/ic/wireless-connectivity/diagram/zigbee-network-topology.png" width="800px">

Further information here: [https://www.zigbee.org/zigbee-for-developers/zigbee-3-0/](https://www.zigbee.org/zigbee-for-developers/zigbee-3-0/)

### Smart Home

With Zigbee 3.0 you can also build your own Smart Home solution and control Zigbee devices from different manufactures like Philips with Philips Hue and IKEA with Tradfri at the same time in the same network. So you are very flexible and the components are very cheap.

## Zigbee Stacks
Because Zigbee is just a specification you need a stack of a manufacturer that implements it. ZigbeeNet is developed with a strict seperation of the Zigbee Cluster Library (ZCL) and the various manufacturer stacks. Because of that it is possible to use different hardware for your Zigbee solution 

### Texas Instruments ( Z-Stack )
The first stack that is implemented in ZigbeeNet is Z-Stack 3.0 from Texas Instruments

Z-Stack 3.0.x is TI's Zigbee 3.0 compliant protocol suite for the CC2530, CC2531, and CC2538 Wireless MCU.
Z-Stack comunicates through TI's Unified Network Processor Interface (NPI) which is used for establishing a serial data link between a TI SoC and external MCUs or PCs. UNPI is also implemented in this project and is also implemented for different plattforms.

The easiest solution is the CC2531 USB Stick with the Znp (Zigbee Network Processor) Image, so that it works as an Zigbee gateway via serial port

Source: [http://www.ti.com/tool/z-stack](http://www.ti.com/tool/z-stack)

## Important

This library is still under development. Breaking changes are possible at all time. A wiki will be comming ASAP

## Usage

```
using System;

namespace ZigbeeNet.PlayGround
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var zigbeeService = new ZigbeeService(new Options { Baudrate = 115200, Port = "COM3" });
                zigbeeService.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
```

## Contributing

Feel free to open an issue if you have any idea or enhancement. If you want to implement on code create a fork and open a pull request

## License
ZigbeeNET is provided under [The MIT License](https://github.com/Mr-Markus/ZigbeeNet/blob/master/LICENSE).

## Contributor

 [@Mr-Markus](https://github.com/Mr-Markus)
 
 [@nicolaiw](https://github.com/nicolaiw)
