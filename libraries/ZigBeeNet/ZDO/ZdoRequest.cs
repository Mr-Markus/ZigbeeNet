using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZDO
{
    /// <summary>
     /// Abstract class for ZDO response commands.
     /// </summary>
    public abstract class ZdoRequest : ZdoCommand
    {
        /// <summary>
         /// Destination address;
         /// </summary>
        // protected int destinationAddress;

        /// <summary>
         /// Gets destination address.
         ///
         /// <returns>the destination address</returns>
         /// </summary>
        // public int getDestinationAddress() {
        // return destinationAddress;
        // }

        /// <summary>
         /// Set the destination address
         ///
         /// <param name="destinationAddress">the destination address as <see cref="int"></param>
         /// </summary>
        // public void setDestinationAddress(int destinationAddress) {
        // this.destinationAddress = destinationAddress;
        // }

        // @Override
        // public String toString() {
        // final StringBuilder builder = new StringBuilder();
        // builder.append(": destinationAddress=");
        // builder.append(destinationAddress);
        // return builder.toString();
        // }
    }
}
