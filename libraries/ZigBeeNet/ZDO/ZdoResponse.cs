using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZDO
{
    public abstract class ZdoResponse : ZdoCommand
    {

        /// <summary>
         /// Source address;
         /// </summary>
        // protected int sourceAddress;

        /// <summary>
         /// The response status.
         /// </summary>
        public ZdoStatus Status { get; set; }

        /// <summary>
         /// Gets source address.
         ///
         /// <returns>the destination address</returns>
         /// </summary>
        // public int getSourceAddress() {
        // return sourceAddress;
        // }

        /// <summary>
         /// Set the source address
         ///
         /// <param name="sourceAddress">the source address as <see cref="int"></param>
         /// </summary>
        // public void setSourceAddress(int sourceAddress) {
        // this.sourceAddress = sourceAddress;
        // }

        /// <summary>
         /// Gets the response status
         ///
         /// <returns>the response status</returns>
         /// </summary>
        //public ZdoStatus GetStatus()
        //{
        //    return Status;
        //}

        ///// <summary>
        // /// Sets the response status
        // ///
        // /// <param name="status">the response status as <see cref="int"></param>
        // /// </summary>
        //public void SetStatus(ZdoStatus status)
        //{
        //    this.Status = status;
        //}

        // @Override
        // public String toString() {
        // final StringBuilder builder = new StringBuilder();
        // builder.append(": sourceAddress=");
        // builder.append(sourceAddress);
        // return builder.toString();
        // }
    }
}
