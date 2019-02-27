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
         /// @return the destination address
         /// </summary>
        // public int getSourceAddress() {
        // return sourceAddress;
        // }

        /// <summary>
         /// Set the source address
         ///
         /// @param sourceAddress the source address as {@link int}
         /// </summary>
        // public void setSourceAddress(int sourceAddress) {
        // this.sourceAddress = sourceAddress;
        // }

        /// <summary>
         /// Gets the response status
         ///
         /// @return the response status
         /// </summary>
        //public ZdoStatus GetStatus()
        //{
        //    return Status;
        //}

        ///// <summary>
        // /// Sets the response status
        // ///
        // /// @param status the response status as {@link int}
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
