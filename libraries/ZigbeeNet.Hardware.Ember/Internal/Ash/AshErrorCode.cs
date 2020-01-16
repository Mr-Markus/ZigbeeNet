using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.Hardware.Ember.Internal.Ash
{

    /// <summary>
    /// Lists the codes returned by the NCP in the Reset Code byte of a RSTACK frame or in the Error Code byte of an ERROR frame.
    /// </summary>
    public class AshErrorCode 
    {
        public const int UNKNOWN = -1;
        public const int RESET_UNKNOWN = 0x00;
        public const int RESET_EXTERNAL = 0x01;
        public const int RESET_POWER_ON = 0x02;
        public const int RESET_WATCHDOG = 0x03;
        public const int RESET_ASSERT = 0x06;
        public const int RESET_BOOTLOADER = 0x09;
        public const int RESET_SOFTWARE = 0x0B;
        public const int RESET_TIMEOUT = 0x51;

        private static Dictionary<int, AshErrorCode> _codeDictionary = new Dictionary<int, AshErrorCode>()
        {

            {UNKNOWN,  new AshErrorCode(UNKNOWN, "Unknown") },
            {RESET_UNKNOWN,  new AshErrorCode(RESET_UNKNOWN, "Reset: Unknown reason")},
            {RESET_EXTERNAL,  new AshErrorCode(RESET_EXTERNAL, "Reset: External")},
            {RESET_POWER_ON,  new AshErrorCode(RESET_POWER_ON, "Reset: Power-on")},
            {RESET_WATCHDOG,  new AshErrorCode(RESET_WATCHDOG, "Reset: Watchdog")},
            {RESET_ASSERT,  new AshErrorCode(RESET_ASSERT, "Reset: Assert")},
            {RESET_BOOTLOADER,  new AshErrorCode(RESET_BOOTLOADER, "Reset: Boot loader")},
            {RESET_SOFTWARE,  new AshErrorCode(RESET_SOFTWARE, "Reset: Software")},
            {RESET_TIMEOUT,  new AshErrorCode(RESET_TIMEOUT, "Error: Exceeded maximum ACK timeout count")},
        };
        private int _errorCode;
        private string _description;

        AshErrorCode(int errorCode, String description) 
        {
            this._errorCode = errorCode;
            this._description = description;
        }

        /**
         * Gets an AshErrorCode enum given the ASH error code
         *
         * @param errorCode
         * @return
         */
        public static AshErrorCode GetAshErrorCode(int errorCode) 
        {
            AshErrorCode error = _codeDictionary[errorCode];
            if (error == null) 
            {
                return _codeDictionary[-1];
            }

            return error;
        }

        /**
         * Returns the ASH error code value
         */
        public int GetErrorCode() 
        {
            return _errorCode;
        }

        /**
         * Returns the UG101 documented description of the error code
         *
         * @return
         */
        public string GetDescription() 
        {
            return _description;
        }
    }
}
