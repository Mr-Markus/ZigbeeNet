<img src="https://github.com/Mr-Markus/ZigbeeNet/blob/master/icon.png" width="150px" />

# ZigBeeNet [![Build status](https://ci.appveyor.com/api/projects/status/o2x3lg7eo46jl2j5/branch/master?svg=true)](https://ci.appveyor.com/project/Mr-Markus/zigbeenet-mlw5f/branch/master) [![NuGet Status](https://img.shields.io/nuget/v/ZigBeeNet.svg?style=flat)](https://www.nuget.org/packages/ZigBeeNet) [![Twitter Follow](https://img.shields.io/twitter/follow/Zigbee_Net.svg?style=social)](https://twitter.com/Zigbee_Net)

ZigBeeNet is a implementation of the Zigbee 3.0 Cluster Library for .NET Standard.

It is fully implemented in Netstandard 2.0 and NET Core 3.0 so that it runs on multiple platform

## Smart Home

With Zigbee 3.0 you can also build your own Smart Home solution and control Zigbee devices from different manufactures like Philips with Philips Hue and IKEA with Tradfri at the same time in the same network. So you are very flexible and the components are very cheap.

## Usage

For a detailed description about how to use ZigBeeNet see our [wiki page](https://github.com/Mr-Markus/ZigbeeNet/wiki):

- [Getting Started](https://github.com/Mr-Markus/ZigbeeNet/wiki/Getting-started)
- [Get node and endpoint address](https://github.com/Mr-Markus/ZigbeeNet/wiki/Get-node-and-endpoint-address)
- [Read attributes](https://github.com/Mr-Markus/ZigbeeNet/wiki/Read-attributes)
- [How to control devices](https://github.com/Mr-Markus/ZigbeeNet/wiki/How-to-control-devices)

You can also take a look at the [Playground Demo project](https://github.com/Mr-Markus/ZigbeeNet/blob/develop/samples/ZigBeeNet.PlayGround/Program.cs)

If you need further information we will help you whenever you need it. Just open an new issue for it.

A basic example here:

```cs
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

## Zigbee Stacks

Because Zigbee is just a specification you need a stack of a manufacturer that implements it. ZigBeeNet is developed with a strict seperation of the Zigbee Cluster Library (ZCL) and the various manufacturer stacks. Because of that it is possible to use different hardware for your Zigbee solution

If there is a manufacturer solution missing, feel free to open an issue or take part of it's implementation

### Texas Instruments ( Z-Stack )

Z-Stack 3.0.x is TI's Zigbee 3.0 compliant protocol suite for the CC2530, CC2531, and CC2538 Wireless MCU.
Z-Stack comunicates through TI's Unified Network Processor Interface (NPI) which is used for establishing a serial data link between a TI SoC and external MCUs or PCs. UNPI is also implemented in this project and is also implemented for different plattforms.

The easiest solution is the CC2531 USB Stick with the Znp (Zigbee Network Processor) Image, so that it works as an Zigbee gateway via serial port

Source: [http://www.ti.com/tool/z-stack](http://www.ti.com/tool/z-stack)

### Digi XBee

Digi XBee is the brand name of a family of form factor compatible radio modules from Digi International.

Source: [https://en.wikipedia.org/wiki/XBee](https://en.wikipedia.org/wiki/XBee)

We have tested it with the XBee ZigBee S2C chip

## Free beer !!

No. Sorry. But now that I have your attention .. we need your help! Since it is hardly possible to test all devices, let us know with which devices you have already successfully tested ZigBeeNet or where you encountered problems. Just open an Issue.

See also:

[Related Issue](https://github.com/Mr-Markus/ZigbeeNet/issues/47)  
[Wiki](https://github.com/zigbeenet/Mr-Markus/wiki/Supported-devices)

Many thanks!

## Hint

This project is highly inspired by https://github.com/zsmartsystems/com.zsmartsystems.zigbee and many ideas were adopted (almost a java -> c# port).

## Contributing

Feel free to open an issue if you have any idea or enhancement. If you want to implement on code create a fork and open a pull request.

## Coding guidelines

For a cleaner code we have some coding guidelines. you can find them [here](https://github.com/Mr-Markus/ZigbeeNet/blob/master/docs/coding-guidelines).

## License and Copyright

ZigBeeNet is licensed under the [Eclipse Public License](https://www.eclipse.org/legal/epl-v10.html). Refer to the [license file](LICENSE) for further information.

Some parts of this project are converted to c# from [com.zsmartsystems.zigbee](https://github.com/zsmartsystems/com.zsmartsystems.zigbee).

[com.zsmartsystems.zigbee](https://github.com/zsmartsystems/com.zsmartsystems.zigbee) use code from [zigbee4java](https://github.com/tlaukkan/zigbee4java) which in turn is derived from [ZB4O](http://zb4osgi.aaloa.org/) projects which are licensed under the [Apache-2 license](https://www.apache.org/licenses/LICENSE-2.0).

## ZigBee Documentation

Some documentation used to create parts of this framework is copyright © ZigBee Alliance, Inc. All rights Reserved. The following copyright notice is copied from the ZigBee documentation.

Elements of ZigBee Alliance specifications may be subject to third party intellectual property rights, including without limitation, patent, copyright or trademark rights (such a third party may or may not be a member of ZigBee). ZigBee is not responsible and shall not be held responsible in any manner for identifying or failing to identify any or all such third party intellectual property rights.

No right to use any ZigBee name, logo or trademark is conferred herein. Use of any ZigBee name, logo or trademark requires membership in the ZigBee Alliance and compliance with the ZigBee Logo and Trademark Policy and related ZigBee policies.

This document and the information contained herein are provided on an “AS IS” basis and ZigBee DISCLAIMS ALL WARRANTIES EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO (A) ANY WARRANTY THAT THE USE OF THE INFORMATION HEREIN WILL NOT INFRINGE ANY RIGHTS OF THIRD PARTIES (INCLUDING WITHOUT LIMITATION ANY INTELLECTUAL PROPERTY RIGHTS INCLUDING PATENT, COPYRIGHT OR TRADEMARK RIGHTS) OR (B) ANY IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, TITLE OR NONINFRINGEMENT. IN NO EVENT WILL ZIGBEE BE LIABLE FOR ANY LOSS OF PROFITS, LOSS OF BUSINESS, LOSS OF USE OF DATA, INTERRUPTION OF BUSINESS, OR FOR ANY OTHER DIRECT, INDIRECT, SPECIAL OR EXEMPLARY, INCIDENTIAL, PUNITIVE OR CONSEQUENTIAL DAMAGES OF ANY KIND, IN CONTRACT OR IN TORT, IN CONNECTION WITH THIS DOCUMENT OR THE INFORMATION CONTAINED HEREIN, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH LOSS OR DAMAGE.

All Company, brand and product names may be trademarks that are the sole property of their respective owners.

## Dongle Documentation

Some documentation used to implement dongle drivers is copywrite to the respective holders, including Silicon Labs, Texas Instruments, Dresden Electronics, Digi International.

## Contributor

[@Mr-Markus](https://github.com/Mr-Markus) (ZigBeeNet, CC2531)

[@nicolaiw](https://github.com/nicolaiw) (ZigBeeNet)

[@andreasfedermann](https://github.com/andreasfedermann) (XBee)
