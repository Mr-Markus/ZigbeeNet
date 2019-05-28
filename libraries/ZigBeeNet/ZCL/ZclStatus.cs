using System;
using System.Collections.Generic;
using System.Text;

namespace ZigBeeNet.ZCL
{
    public enum ZclStatus
    {
        /// <summary>
        /// Operation was successful
        /// </summary>
        SUCCESS = 0,
        /// <summary>
        /// Operation was not successful
        /// </summary>
        FAILURE = 1 ,
        /// <summary>
        /// The sender of the command does not have authorization to carry out this command
        /// </summary>
        NOT_AUTHORIZED = 126,
        /// <summary>
        /// A reserved field/subfield/bit contains a non-zero value
        /// </summary>
        RESERVED_FIELD_NOT_ZERO =127,
        /// <summary>
        /// The command appears to contain the wrong fields, as detected either by the presence of one or more invalid field entries or by there being missing fields. 
        /// Command not carried out. Implementer has discretion as to whether to return this error or INVALID_FIELD. 
        /// </summary>
        MALFORMED_COMMAND = 128,
        /// <summary>
        /// The specified cluster command is not supported on the device. Command not carried out. 
        /// </summary>
        UNSUP_CLUSTER_COMMAND = 129 ,
        /// <summary>
        /// The specified general ZCL command is not supported on the device.  
        /// </summary>
        UNSUP_GENERAL_COMMAND = 130,
        /// <summary>
        /// A manufacturer specific unicast, cluster specific command was received with an unknown manufacturer code, or the manufacturer code 
        /// was recognized but the command is not supported.  
        /// </summary>
        UNSUP_MANUF_CLUSTER_COMMAND = 131,
        /// <summary>
        /// A manufacturer specific unicast, ZCL specific command was received with an unknown manufacturer code, or the manufacturer 
        /// code was recognized but the command is not supported.  
        /// </summary>
        UNSUP_MANUF_GENERAL_COMMAND = 132 ,
        /// <summary>
        /// At least one field of the command contains an incorrect value, according to the specification the device is implemented to.  
        /// </summary>
        INVALID_FIELD = 133,
        /// <summary>
        /// The specified attribute does not exist on the device. 
        /// </summary>
        UNSUPPORTED_ATTRIBUTE = 134,
        /// <summary>
        /// Out of range error, or set to a reserved value. Attribute keeps its old value. 
        /// 
        /// Note that an attribute value may be out of range if an attribute is related to another, e.g., with minimum and maximum attributes. 
        /// See the individual attribute descriptions for specific details
        /// </summary>
        INVALID_VALUE = 135,
        /// <summary>
        /// Attempt to write a read only attribute.
        /// </summary>
        READ_ONLY = 136 ,
        /// <summary>
        /// An operation failed due to an insufficient amount of free space available11. 
        /// </summary>
        INSUFFICIENT_SPACE = 137,
        /// <summary>
        /// An attempt to create an entry in a table failed due to a duplicate entry already being present in the table. 
        /// </summary>
        DUPLICATE_EXISTS = 138,
        /// <summary>
        /// The requested information (e.g., table entry) could not be found. 
        /// </summary>
        NOT_FOUND = 139,
        /// <summary>
        /// Periodic reports cannot be issued for this attribute. 
        /// </summary>
        UNREPORTABLE_ATTRIBUTE = 140,
        /// <summary>
        /// The data type given for an attribute is incorrect. Command not carried out.
        /// </summary>
        INVALID_DATA_TYPE = 141,
        /// <summary>
        /// The selector for an attribute is incorrect. 
        /// </summary>
        INVALID_SELECTOR = 142,
        /// <summary>
        /// A request has been made to read an attribute that the requestor is not authorized to read. No action taken. 
        /// </summary>
        WRITE_ONLY = 143,
        /// <summary>
        /// Setting the requested values would put the device in an inconsistent state on startup. No action taken. 
        /// </summary>
        INCONSISTENT_STARTUP_STATE = 144,
        /// <summary>
        /// An attempt has been made to write an attribute that is present but is defined using an out-of-band method and not over the air.
        /// </summary>
        DEFINED_OUT_OF_BAND = 145,
        /// <summary>
        /// The supplied values(e.g., contents of table cells) are inconsistent.
        /// </summary>
        INCONSISTENT = 146,
        /// <summary>
        /// The credentials presented by the device sending the command are not sufficient to perform this action.
        /// </summary>
        ACTION_DENIED = 147,
        /// <summary>
        /// The exchange was aborted due to excessive response time.
        /// </summary>
        TIMEOUT = 148,
        /// <summary>
        /// Failed case when a client or a server decides to abort the upgrade process. 
        /// </summary>
        ABORT = 149,
        /// <summary>
        ///Invalid OTA upgrade image (ex.failed signature validation or signer information check or CRC check). 
        /// </summary>
        INVALID_IMAGE = 150,
        /// <summary>
        /// Server does not have data block available yet.
        /// </summary>
        WAIT_FOR_DATA = 151,
        /// <summary>
        /// No OTA upgrade image available for a particular client.
        /// </summary>
        NO_IMAGE_AVAILABLE = 152,
        /// <summary>
        /// The client still requires more OTA upgrade image
        /// files in order to successfully upgrade.
        /// </summary>
        REQUIRE_MORE_IMAGE = 153,
        /// <summary>
        /// The command has been received and is being processed. 
        /// </summary>
        NOTIFICATION_PENDING = 154,
        /// <summary>
        /// An operation was unsuccessful due to a hardware failure. 
        /// </summary>
        HARDWARE_FAILURE = 192,
        /// <summary>
        /// An operation was unsuccessful due to a software failure. 
        /// </summary>
        SOFTWARE_FAILURE = 193,
        /// <summary>
        /// An error occurred during calibration. 
        /// </summary>
        CALIBRATION_ERROR = 194,
        /// <summary>
        /// The cluster is not supported
        /// </summary>
        UNSUPPORTED_CLUSTER = 195
    }
}
