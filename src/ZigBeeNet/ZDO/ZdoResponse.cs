using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZDO
{
    public abstract class ZdoResponse : ZdoCommand
    {

        /**
         * Source address;
         */
        // protected int sourceAddress;

        /**
         * The response status.
         */
        public ZdoStatus Status { get; set; }

        /**
         * Gets source address.
         *
         * @return the destination address
         */
        // public int getSourceAddress() {
        // return sourceAddress;
        // }

        /**
         * Set the source address
         *
         * @param sourceAddress the source address as {@link int}
         */
        // public void setSourceAddress(int sourceAddress) {
        // this.sourceAddress = sourceAddress;
        // }

        /**
         * Gets the response status
         *
         * @return the response status
         */
        //public ZdoStatus GetStatus()
        //{
        //    return Status;
        //}

        ///**
        // * Sets the response status
        // *
        // * @param status the response status as {@link int}
        // */
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
