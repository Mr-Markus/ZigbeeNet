# ZigBeeNet [![Build status](https://ci.appveyor.com/api/projects/status/o2x3lg7eo46jl2j5?svg=true)](https://ci.appveyor.com/project/Mr-Markus/zigbeenet-mlw5f) [![Twitter Follow](https://img.shields.io/twitter/follow/Zigbee_Net.svg?style=social)](https://twitter.com/Zigbee_Net) 

ZigBeeNet is a implementation of the Zigbee 3.0 Cluster Library for .NET Standard, .NET Core and more .NET platforms. 

With ZigBeeNet you can develop your own .NET application which communicates with zigbee devices.

## Free beer !!
No. Sorry. But now that I have your attention .. we need your help! Since it is hardly possible to test all devices, let us know with which devices you have already successfully tested ZigBeeNet or where you encountered problems. Just open an Issue.
[See our related Issue.](https://github.com/zigbeenet/ZigbeeNet/issues/47)

Many thanks!

### Smart Home
With Zigbee 3.0 you can also build your own Smart Home solution and control Zigbee devices from different manufactures like Philips with Philips Hue and IKEA with Tradfri at the same time in the same network. So you are very flexible and the components are very cheap.

## Zigbee Stacks
Because Zigbee is just a specification you need a stack of a manufacturer that implements it. ZigBeeNet is developed with a strict seperation of the Zigbee Cluster Library (ZCL) and the various manufacturer stacks. Because of that it is possible to use different hardware for your Zigbee solution 

### Texas Instruments ( Z-Stack )
The first stack that is implemented in ZigBeeNet is Z-Stack 3.0 from Texas Instruments

Z-Stack 3.0.x is TI's Zigbee 3.0 compliant protocol suite for the CC2530, CC2531, and CC2538 Wireless MCU.
Z-Stack comunicates through TI's Unified Network Processor Interface (NPI) which is used for establishing a serial data link between a TI SoC and external MCUs or PCs. UNPI is also implemented in this project and is also implemented for different plattforms.

The easiest solution is the CC2531 USB Stick with the Znp (Zigbee Network Processor) Image, so that it works as an Zigbee gateway via serial port

Source: [http://www.ti.com/tool/z-stack](http://www.ti.com/tool/z-stack)

## Important

This library is still under development. Breaking changes are possible at all time. A wiki will be comming ASAP

## Usage

For a detailed description about how to use ZigBeeNet see our [wiki page](https://github.com/Mr-Markus/ZigbeeNet/wiki) or
have a look at our [PlayGround App](https://github.com/zigbeenet/ZigbeeNet/blob/master/Samples/core2.2/ZigBeeNet.PlayGround/Program.cs).

A basic example here:

```
using System;

namespace ZigBeeNet.PlayGround
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ZigBeeSerialPort zigbeePort = new ZigBeeSerialPort("COM4");

                IZigBeeTransportTransmit dongle = new ZigBeeDongleTiCc2531(zigbeePort);

                ZigBeeNetworkManager networkManager = new ZigBeeNetworkManager(dongle);

                // Initialise the network
                networkManager.Initialize();

                networkManager.AddSupportedCluster(0x06);

                ZigBeeStatus startupSucceded = networkManager.Startup(false);

                if (startupSucceded == ZigBeeStatus.SUCCESS)
                {
                    Log.Logger.Information("ZigBee console starting up ... [OK]");
                }
                else
                {
                    Log.Logger.Information("ZigBee console starting up ... [FAIL]");
                    return;
                }
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
## Hint
This project is highly inspired by https://github.com/zsmartsystems/com.zsmartsystems.zigbee and many ideas were adopted (almost a java -> c# port).

## Contributing

Feel free to open an issue if you have any idea or enhancement. If you want to implement on code create a fork and open a pull request

## Coding guidelines

For a cleaner code we have some coding guidelines. you can find them [here](https://github.com/Mr-Markus/ZigbeeNet/blob/master/docs/coding-guidelines).

## License
ZigBeeNet is provided under [The MIT License](https://github.com/Mr-Markus/ZigBeeNet/blob/master/LICENSE).

## Contributor

 [@Mr-Markus](https://github.com/Mr-Markus)
 
 [@nicolaiw](https://github.com/nicolaiw)
