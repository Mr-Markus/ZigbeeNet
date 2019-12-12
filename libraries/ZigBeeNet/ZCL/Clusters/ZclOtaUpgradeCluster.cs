
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZigBeeNet.Security;
using ZigBeeNet.ZCL.Clusters.OtaUpgrade;
using ZigBeeNet.ZCL.Field;
using ZigBeeNet.ZCL.Protocol;


namespace ZigBeeNet.ZCL.Clusters
{
    /// <summary>
    /// Ota Upgrade cluster implementation (Cluster ID 0x0019).
    ///
    /// The cluster provides a standard way to upgrade devices in the network via OTA messages.
    /// Thus the upgrade process may be performed between two devices from different
    /// manufacturers. Devices are required to have application bootloader and additional
    /// memory space in order to successfully implement the cluster.
    /// It is the responsibility of the server to indicate to the clients when update images are
    /// available. The client may be upgraded or downgraded64. The upgrade server knows which
    /// client devices to upgrade and to what file version. The upgrade server may be notified of
    /// such information via the backend system. For ZR clients, the server may send a message to
    /// notify the device when an updated image is available. It is assumed that ZED clients will
    /// not be awake to receive an unsolicited notification of an available image. All clients
    /// (ZR and ZED) shall query (poll) the server periodically to determine whether the server
    /// has an image update for them. Image Notify is optional.
    /// The cluster is implemented in such a way that the client service works on both ZED and ZR
    /// devices. Being able to handle polling is mandatory for all server devices while being
    /// able to send a notify is optional. Hence, all client devices must be able to use a ‘poll’
    /// mechanism to send query message to the server in order to see if the server has any new file
    /// for it. The polling mechanism also puts fewer resources on the upgrade server. It is
    /// ideal to have the server maintain as little state as possible since this will scale when
    /// there are hundreds of clients in the network. The upgrade server is not required to keep
    /// track of what pieces of an image that a particular client has received; instead the
    /// client shall do that. Lastly poll makes more sense for devices that may need to perform
    /// special setup to get ready to receive an image, such as unlocking flash or allocating
    /// space for the new image.
    ///
    /// Code is auto-generated. Modifications may be overwritten!
    /// </summary>
    public class ZclOtaUpgradeCluster : ZclCluster
    {
        /// <summary>
        /// The ZigBee Cluster Library Cluster ID
        /// </summary>
        public const ushort CLUSTER_ID = 0x0019;

        /// <summary>
        /// The ZigBee Cluster Library Cluster Name
        /// </summary>
        public const string CLUSTER_NAME = "Ota Upgrade";

        // Attribute constants

        /// <summary>
        /// The attribute is used to store the IEEE address of the upgrade server resulted from
        /// the discovery of the up- grade server’s identity. If the value is set to a non-zero
        /// value and corresponds to an IEEE address of a device that is no longer accessible, a
        /// device may choose to discover a new Upgrade Server depending on its own security
        /// policies.
        /// </summary>
        public const ushort ATTR_UPGRADESERVERID = 0x0000;

        /// <summary>
        /// The parameter indicates the current location in the OTA upgrade image. It is
        /// essentially the (start of the) address of the image data that is being transferred
        /// from the OTA server to the client. The attribute is optional on the client and is made
        /// available in a case where the server wants to track the upgrade process of a
        /// particular client.
        /// </summary>
        public const ushort ATTR_FILEOFFSET = 0x0001;

        /// <summary>
        /// The file version of the running firmware image on the device. The information is
        /// available for the server to query via ZCL read attribute command. The attribute is
        /// optional on the client.
        /// </summary>
        public const ushort ATTR_CURRENTFILEVERSION = 0x0002;

        /// <summary>
        /// The ZigBee stack version of the running image on the device. The information is
        /// available for the server to query via ZCL read attribute command.
        /// </summary>
        public const ushort ATTR_CURRENTZIGBEESTACKVERSION = 0x0003;

        /// <summary>
        /// The file version of the downloaded image on additional memory space on the device.
        /// The information is available for the server to query via ZCL read attribute
        /// command. The information is useful for the OTA upgrade management, so the server
        /// may ensure that each client has downloaded the correct file version before
        /// initiate the upgrade. The attribute is optional on the client.
        /// </summary>
        public const ushort ATTR_DOWNLOADEDFILEVERSION = 0x0004;

        /// <summary>
        /// The ZigBee stack version of the downloaded image on additional memory space on the
        /// device. The information is available for the server to query via ZCL read attribute
        /// command. The information is useful for the OTA upgrade management, so the server
        /// shall ensure that each client has downloaded the correct ZigBee stack version
        /// before initiate the upgrade. The attribute is optional on the client.
        /// </summary>
        public const ushort ATTR_DOWNLOADEDZIGBEESTACKVERSION = 0x0005;

        /// <summary>
        /// The upgrade status of the client device. The status indicates where the client
        /// device is at in terms of the download and upgrade process. The status helps to
        /// indicate whether the client has completed the download process and whether it is
        /// ready to upgrade to the new image. The status may be queried by the server via ZCL read
        /// attribute command. Hence, the server may not be able to reliably query the status of
        /// ZED client since the device may have its radio off.
        /// </summary>
        public const ushort ATTR_IMAGEUPGRADESTATUS = 0x0006;

        /// <summary>
        /// This attribute shall reflect the ZigBee assigned value for the manufacturer of the
        /// device.
        /// </summary>
        public const ushort ATTR_MANUFACTURERID = 0x0007;

        /// <summary>
        /// This attribute shall indicate the image type identifier of the file that the client
        /// is currently downloading, or a file that has been completely downloaded but not
        /// upgraded to yet. The value of this attribute shall be 0xFFFF when the client is not
        /// downloading a file or is not waiting to apply an upgrade.
        /// </summary>
        public const ushort ATTR_IMAGETYPEID = 0x0008;

        /// <summary>
        /// This attribute acts as a rate limiting feature for the server to slow down the client
        /// download and prevent saturating the network with block requests. The attribute
        /// lives on the client but can be changed during a download if rate limiting is
        /// supported by both devices.
        /// </summary>
        public const ushort ATTR_MINIMUMBLOCKREQUESTPERIOD = 0x0009;

        /// <summary>
        /// This attribute acts as a second verification to identify the image in the case that
        /// sometimes developers of the application have forgotten to increase the firmware
        /// version attribute. It is a 32 bits value and has a valid range from 0x00000000 to
        /// 0xFFFFFFFF. This attribute value must be consistent during the lifetime of the
        /// same image and also must be unique for each different build of the image. This
        /// attribute value should not be hardcoded or generated by any manual process. This
        /// attribute value should be generated by performing a hash or checksum on the entire
        /// image. There are two possible methods to generate this checksum. It can be
        /// generated dynamically during runtime of the application or it can be generated
        /// during compile time of the application.
        /// </summary>
        public const ushort ATTR_IMAGESTAMP = 0x000A;

        protected override Dictionary<ushort, ZclAttribute> InitializeClientAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(11);

            attributeMap.Add(ATTR_UPGRADESERVERID, new ZclAttribute(this, ATTR_UPGRADESERVERID, "Upgrade Server ID", ZclDataType.Get(DataType.IEEE_ADDRESS), true, true, false, false));
            attributeMap.Add(ATTR_FILEOFFSET, new ZclAttribute(this, ATTR_FILEOFFSET, "File Offset", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTFILEVERSION, new ZclAttribute(this, ATTR_CURRENTFILEVERSION, "Current File Version", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_CURRENTZIGBEESTACKVERSION, new ZclAttribute(this, ATTR_CURRENTZIGBEESTACKVERSION, "Current ZigBee Stack Version", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DOWNLOADEDFILEVERSION, new ZclAttribute(this, ATTR_DOWNLOADEDFILEVERSION, "Downloaded File Version", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_DOWNLOADEDZIGBEESTACKVERSION, new ZclAttribute(this, ATTR_DOWNLOADEDZIGBEESTACKVERSION, "Downloaded ZigBee Stack Version", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_IMAGEUPGRADESTATUS, new ZclAttribute(this, ATTR_IMAGEUPGRADESTATUS, "Image Upgrade Status", ZclDataType.Get(DataType.ENUMERATION_8_BIT), true, true, false, false));
            attributeMap.Add(ATTR_MANUFACTURERID, new ZclAttribute(this, ATTR_MANUFACTURERID, "Manufacturer ID", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_IMAGETYPEID, new ZclAttribute(this, ATTR_IMAGETYPEID, "Image Type ID", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_MINIMUMBLOCKREQUESTPERIOD, new ZclAttribute(this, ATTR_MINIMUMBLOCKREQUESTPERIOD, "Minimum Block Request Period", ZclDataType.Get(DataType.UNSIGNED_16_BIT_INTEGER), true, true, false, false));
            attributeMap.Add(ATTR_IMAGESTAMP, new ZclAttribute(this, ATTR_IMAGESTAMP, "Image Stamp", ZclDataType.Get(DataType.UNSIGNED_32_BIT_INTEGER), true, true, false, false));

            return attributeMap;
        }

        protected override Dictionary<ushort, ZclAttribute> InitializeServerAttributes()
        {
            Dictionary<ushort, ZclAttribute> attributeMap = new Dictionary<ushort, ZclAttribute>(0);

            return attributeMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeServerCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(5);

            commandMap.Add(0x0000, () => new ImageNotifyCommand());
            commandMap.Add(0x0002, () => new QueryNextImageResponse());
            commandMap.Add(0x0005, () => new ImageBlockResponse());
            commandMap.Add(0x0007, () => new UpgradeEndResponse());
            commandMap.Add(0x0009, () => new QuerySpecificFileResponse());

            return commandMap;
        }

        protected override Dictionary<ushort, Func<ZclCommand>> InitializeClientCommands()
        {
            Dictionary<ushort, Func<ZclCommand>> commandMap = new Dictionary<ushort, Func<ZclCommand>>(5);

            commandMap.Add(0x0001, () => new QueryNextImageCommand());
            commandMap.Add(0x0003, () => new ImageBlockCommand());
            commandMap.Add(0x0004, () => new ImagePageCommand());
            commandMap.Add(0x0006, () => new UpgradeEndCommand());
            commandMap.Add(0x0008, () => new QuerySpecificFileCommand());

            return commandMap;
        }

        /// <summary>
        /// Default constructor to create a Ota Upgrade cluster.
        ///
        /// <param name="zigbeeEndpoint"> the ZigBeeEndpoint this cluster is contained within </param>
        /// </summary>
        public ZclOtaUpgradeCluster(ZigBeeEndpoint zigbeeEndpoint)
            :base(zigbeeEndpoint, CLUSTER_ID, CLUSTER_NAME)
        {
        }

        /// <summary>
        /// The Image Notify Command
        ///
        /// The purpose of sending Image Notify command is so the server has a way to notify
        /// client devices of when the OTA upgrade images are available for them. It eliminates
        /// the need for ZR client devices having to check with the server periodically of when
        /// the new images are available. However, all client devices still need to send in
        /// Query Next Image Request command in order to officially start the OTA upgrade
        /// process. <br> For ZR client devices, the upgrade server may send out a unicast,
        /// broadcast, or multicast indicating it has the next upgrade image, via an Image
        /// Notify command. Since the command may not have APS security (if it is broadcast or
        /// multicast), it is considered purely informational and not authoritative. Even in
        /// the case of a unicast, ZR shall continue to perform the query process described in
        /// later section. <br> When the command is sent with payload type value of zero, it
        /// generally means the server wishes to notify all clients disregard of their
        /// manufacturers, image types or file versions. Query jitter is needed to protect the
        /// server from being flooded with clients’ queries for next image.
        ///
        /// <param name="payloadType" <see cref="byte"> Payload Type</ param >
        /// <param name="queryJitter" <see cref="byte"> Query Jitter</ param >
        /// <param name="manufacturerCode" <see cref="ushort"> Manufacturer Code</ param >
        /// <param name="imageType" <see cref="ushort"> Image Type</ param >
        /// <param name="newFileVersion" <see cref="uint"> New File Version</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ImageNotifyCommand(byte payloadType, byte queryJitter, ushort manufacturerCode, ushort imageType, uint newFileVersion)
        {
            ImageNotifyCommand command = new ImageNotifyCommand();

            // Set the fields
            command.PayloadType = payloadType;
            command.QueryJitter = queryJitter;
            command.ManufacturerCode = manufacturerCode;
            command.ImageType = imageType;
            command.NewFileVersion = newFileVersion;

            return Send(command);
        }

        /// <summary>
        /// The Query Next Image Command
        ///
        /// Client devices shall send a Query Next Image Request command to the server to see if
        /// there is new OTA upgrade image available. ZR devices may send the command after
        /// receiving Image Notify command. ZED device shall periodically wake up and send the
        /// command to the upgrade server. Client devices query what the next image is, based on
        /// their own information. <br> The server takes the client’s information in the
        /// command and determines whether it has a suitable image for the particular client.
        /// The decision should be based on specific policy that is specific to the upgrade
        /// server and outside the scope of this document.. However, a recommended default
        /// policy is for the server to send back a response that indicates the availability of
        /// an image that matches the manufacturer code, image type, and the highest available
        /// file version of that image on the server. However, the server may choose to upgrade,
        /// downgrade, or reinstall clients’ image, as its policy dictates. If client’s
        /// hardware version is included in the command, the server shall examine the value
        /// against the minimum and maximum hardware versions included in the OTA file header.
        ///
        /// <param name="fieldControl" <see cref="byte"> Field Control</ param >
        /// <param name="manufacturerCode" <see cref="ushort"> Manufacturer Code</ param >
        /// <param name="imageType" <see cref="ushort"> Image Type</ param >
        /// <param name="fileVersion" <see cref="uint"> File Version</ param >
        /// <param name="hardwareVersion" <see cref="ushort"> Hardware Version</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> QueryNextImageCommand(byte fieldControl, ushort manufacturerCode, ushort imageType, uint fileVersion, ushort hardwareVersion)
        {
            QueryNextImageCommand command = new QueryNextImageCommand();

            // Set the fields
            command.FieldControl = fieldControl;
            command.ManufacturerCode = manufacturerCode;
            command.ImageType = imageType;
            command.FileVersion = fileVersion;
            command.HardwareVersion = hardwareVersion;

            return Send(command);
        }

        /// <summary>
        /// The Query Next Image Response
        ///
        /// The upgrade server sends a Query Next Image Response with one of the following
        /// status: SUCCESS, NO_IMAGE_AVAILABLE or NOT_AUTHORIZED. When a SUCCESS status is
        /// sent, it is considered to be the explicit authorization to a device by the upgrade
        /// server that the device may upgrade to a specific software image. <br> A status of
        /// NO_IMAGE_AVAILABLE indicates that the server is authorized to upgrade the client
        /// but it currently does not have the (new) OTA upgrade image available for the client.
        /// For all clients (both ZR and ZED)9 , they shall continue sending Query Next Image
        /// Requests to the server periodically until an image becomes available. <br> A
        /// status of NOT_AUTHORIZED indicates the server is not authorized to upgrade the
        /// client. In this case, the client may perform discovery again to find another
        /// upgrade server. The client may implement an intelligence to avoid querying the
        /// same unauthorized server.
        ///
        /// <param name="status" <see cref="ZclStatus"> Status</ param >
        /// <param name="manufacturerCode" <see cref="ushort"> Manufacturer Code</ param >
        /// <param name="imageType" <see cref="ushort"> Image Type</ param >
        /// <param name="fileVersion" <see cref="uint"> File Version</ param >
        /// <param name="imageSize" <see cref="uint"> Image Size</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> QueryNextImageResponse(ZclStatus status, ushort manufacturerCode, ushort imageType, uint fileVersion, uint imageSize)
        {
            QueryNextImageResponse command = new QueryNextImageResponse();

            // Set the fields
            command.Status = status;
            command.ManufacturerCode = manufacturerCode;
            command.ImageType = imageType;
            command.FileVersion = fileVersion;
            command.ImageSize = imageSize;

            return Send(command);
        }

        /// <summary>
        /// The Image Block Command
        ///
        /// The client device requests the image data at its leisure by sending Image Block
        /// Request command to the upgrade server. The client knows the total number of request
        /// commands it needs to send from the image size value received in Query Next Image
        /// Response command. <br> The client repeats Image Block Requests until it has
        /// successfully obtained all data. Manufacturer code, image type and file version
        /// are included in all further queries regarding that image. The information
        /// eliminates the need for the server to remember which OTA Upgrade Image is being used
        /// for each download process. <br> If the client supports the BlockRequestDelay
        /// attribute it shall include the value of the attribute as the BlockRequestDelay
        /// field of the Image Block Request message. The client shall ensure that it delays at
        /// least BlockRequestDelay milliseconds after the previous Image Block Request was
        /// sent before sending the next Image Block Request message. A client may delay its
        /// next Image Block Requests longer than its BlockRequestDelay attribute.
        ///
        /// <param name="fieldControl" <see cref="byte"> Field Control</ param >
        /// <param name="manufacturerCode" <see cref="ushort"> Manufacturer Code</ param >
        /// <param name="imageType" <see cref="ushort"> Image Type</ param >
        /// <param name="fileVersion" <see cref="uint"> File Version</ param >
        /// <param name="fileOffset" <see cref="uint"> File Offset</ param >
        /// <param name="maximumDataSize" <see cref="byte"> Maximum Data Size</ param >
        /// <param name="requestNodeAddress" <see cref="IeeeAddress"> Request Node Address</ param >
        /// <param name="blockRequestDelay" <see cref="ushort"> Block Request Delay</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ImageBlockCommand(byte fieldControl, ushort manufacturerCode, ushort imageType, uint fileVersion, uint fileOffset, byte maximumDataSize, IeeeAddress requestNodeAddress, ushort blockRequestDelay)
        {
            ImageBlockCommand command = new ImageBlockCommand();

            // Set the fields
            command.FieldControl = fieldControl;
            command.ManufacturerCode = manufacturerCode;
            command.ImageType = imageType;
            command.FileVersion = fileVersion;
            command.FileOffset = fileOffset;
            command.MaximumDataSize = maximumDataSize;
            command.RequestNodeAddress = requestNodeAddress;
            command.BlockRequestDelay = blockRequestDelay;

            return Send(command);
        }

        /// <summary>
        /// The Image Page Command
        ///
        /// The support for the command is optional. The client device may choose to request OTA
        /// upgrade data in one page size at a time from upgrade server. Using Image Page Request
        /// reduces the numbers of requests sent from the client to the upgrade server,
        /// compared to using Image Block Request command. In order to conserve battery life a
        /// device may use the Image Page Request command. Using the Image Page Request command
        /// eliminates the need for the client device to send Image Block Request command for
        /// every data block it needs; possibly saving the transmission of hundreds or
        /// thousands of messages depending on the image size. <br> The client keeps track of
        /// how much data it has received by keeping a cumulative count of each data size it has
        /// received in each Image Block Response. Once the count has reach the value of the page
        /// size requested, it shall repeat Image Page Requests until it has successfully
        /// obtained all pages. Note that the client may choose to switch between using Image
        /// Block Request and Image Page Request during the upgrade process. For example, if
        /// the client does not receive all data requested in one Image Page Request, the client
        /// may choose to request the missing block of data using Image Block Request command,
        /// instead of requesting the whole page again.
        ///
        /// <param name="fieldControl" <see cref="byte"> Field Control</ param >
        /// <param name="manufacturerCode" <see cref="ushort"> Manufacturer Code</ param >
        /// <param name="imageType" <see cref="ushort"> Image Type</ param >
        /// <param name="fileVersion" <see cref="uint"> File Version</ param >
        /// <param name="fileOffset" <see cref="uint"> File Offset</ param >
        /// <param name="maximumDataSize" <see cref="byte"> Maximum Data Size</ param >
        /// <param name="pageSize" <see cref="ushort"> Page Size</ param >
        /// <param name="responseSpacing" <see cref="ushort"> Response Spacing</ param >
        /// <param name="requestNodeAddress" <see cref="IeeeAddress"> Request Node Address</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ImagePageCommand(byte fieldControl, ushort manufacturerCode, ushort imageType, uint fileVersion, uint fileOffset, byte maximumDataSize, ushort pageSize, ushort responseSpacing, IeeeAddress requestNodeAddress)
        {
            ImagePageCommand command = new ImagePageCommand();

            // Set the fields
            command.FieldControl = fieldControl;
            command.ManufacturerCode = manufacturerCode;
            command.ImageType = imageType;
            command.FileVersion = fileVersion;
            command.FileOffset = fileOffset;
            command.MaximumDataSize = maximumDataSize;
            command.PageSize = pageSize;
            command.ResponseSpacing = responseSpacing;
            command.RequestNodeAddress = requestNodeAddress;

            return Send(command);
        }

        /// <summary>
        /// The Image Block Response
        ///
        /// Upon receipt of an Image Block Request command the server shall generate an Image
        /// Block Response. If the server is able to retrieve the data for the client and does not
        /// wish to change the image download rate, it will respond with a status of SUCCESS and
        /// it will include all the fields in the payload. The use of file offset allows the
        /// server to send packets with variable data size during the upgrade process. This
        /// allows the server to support a case when the network topology of a client may change
        /// during the upgrade process, for example, mobile client may move around during the
        /// upgrade process. If the client has moved a few hops away, the data size shall be
        /// smaller. Moreover, using file offset eliminates the need for data padding since
        /// each Image Block Response command may contain different data size. A simple server
        /// implementation may choose to only support largest possible data size for the
        /// worst-case scenario in order to avoid supporting sending packets with variable
        /// data size. <br> The server shall respect the maximum data size value requested by
        /// the client and shall not send the data with length greater than that value. The
        /// server may send the data with length smaller than the value depending on the network
        /// topology of the client. For example, the client may be able to receive 100 bytes of
        /// data at once so it sends the request with 100 as maximum data size. But after
        /// considering all the security headers (perhaps from both APS and network levels)
        /// and source routing overhead (for example, the client is five hops away), the
        /// largest possible data size that the server can send to the client shall be smaller
        /// than 100 bytes.
        ///
        /// <param name="status" <see cref="ZclStatus"> Status</ param >
        /// <param name="manufacturerCode" <see cref="ushort"> Manufacturer Code</ param >
        /// <param name="imageType" <see cref="ushort"> Image Type</ param >
        /// <param name="fileVersion" <see cref="uint"> File Version</ param >
        /// <param name="fileOffset" <see cref="uint"> File Offset</ param >
        /// <param name="imageData" <see cref="ByteArray"> Image Data</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> ImageBlockResponse(ZclStatus status, ushort manufacturerCode, ushort imageType, uint fileVersion, uint fileOffset, ByteArray imageData)
        {
            ImageBlockResponse command = new ImageBlockResponse();

            // Set the fields
            command.Status = status;
            command.ManufacturerCode = manufacturerCode;
            command.ImageType = imageType;
            command.FileVersion = fileVersion;
            command.FileOffset = fileOffset;
            command.ImageData = imageData;

            return Send(command);
        }

        /// <summary>
        /// The Upgrade End Command
        ///
        /// Upon reception all the image data, the client should verify the image to ensure its
        /// integrity and validity. If the device requires signed images it shall examine the
        /// image and verify the signature. Clients may perform additional manufacturer
        /// specific integrity checks to validate the image, for example, CRC check on the
        /// actual file data. <br> If the image fails any integrity checks, the client shall
        /// send an Upgrade End Request command to the upgrade server with a status of
        /// INVALID_IMAGE. In this case, the client may reinitiate the upgrade process in
        /// order to obtain a valid OTA upgrade image. The client shall not upgrade to the bad
        /// image and shall discard the downloaded image data. <br> If the image passes all
        /// integrity checks and the client does not require additional OTA upgrade image
        /// file, it shall send back an Upgrade End Request with a status of SUCCESS. However, if
        /// the client requires multiple OTA upgrade image files before performing an
        /// upgrade, it shall send an Upgrade End Request command with status
        /// REQUIRE_MORE_IMAGE. This shall indicate to the server that it cannot yet upgrade
        /// the image it received. <br> If the client decides to cancel the download process for
        /// any other reasons, it has the option of sending Upgrade End Request with status of
        /// ABORT at anytime during the download process. The client shall then try to
        /// reinitiate the download process again at a later time.
        ///
        /// <param name="status" <see cref="ZclStatus"> Status</ param >
        /// <param name="manufacturerCode" <see cref="ushort"> Manufacturer Code</ param >
        /// <param name="imageType" <see cref="ushort"> Image Type</ param >
        /// <param name="fileVersion" <see cref="uint"> File Version</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> UpgradeEndCommand(ZclStatus status, ushort manufacturerCode, ushort imageType, uint fileVersion)
        {
            UpgradeEndCommand command = new UpgradeEndCommand();

            // Set the fields
            command.Status = status;
            command.ManufacturerCode = manufacturerCode;
            command.ImageType = imageType;
            command.FileVersion = fileVersion;

            return Send(command);
        }

        /// <summary>
        /// The Upgrade End Response
        ///
        /// When an upgrade server receives an Upgrade End Request command with a status of
        /// INVALID_IMAGE, REQUIRE_MORE_IMAGE, or ABORT, no additional processing shall be
        /// done in its part. If the upgrade server receives an Upgrade End Request command with
        /// a status of SUCCESS, it shall generate an Upgrade End Response with the
        /// manufacturer code and image type received in the Upgrade End Request along with the
        /// times indicating when the device should upgrade to the new image. <br> The server
        /// may send an unsolicited Upgrade End Response command to the client. This may be used
        /// for example if the server wants to synchronize the upgrade on multiple clients
        /// simultaneously. For client devices, the upgrade server may unicast or broadcast
        /// Upgrade End Response command indicating a single client device or multiple client
        /// devices shall switch to using their new images. The command may not be reliably
        /// received by sleepy devices if it is sent unsolicited.
        ///
        /// <param name="manufacturerCode" <see cref="ushort"> Manufacturer Code</ param >
        /// <param name="imageType" <see cref="ushort"> Image Type</ param >
        /// <param name="fileVersion" <see cref="uint"> File Version</ param >
        /// <param name="currentTime" <see cref="uint"> Current Time</ param >
        /// <param name="upgradeTime" <see cref="uint"> Upgrade Time</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> UpgradeEndResponse(ushort manufacturerCode, ushort imageType, uint fileVersion, uint currentTime, uint upgradeTime)
        {
            UpgradeEndResponse command = new UpgradeEndResponse();

            // Set the fields
            command.ManufacturerCode = manufacturerCode;
            command.ImageType = imageType;
            command.FileVersion = fileVersion;
            command.CurrentTime = currentTime;
            command.UpgradeTime = upgradeTime;

            return Send(command);
        }

        /// <summary>
        /// The Query Specific File Command
        ///
        /// Client devices shall send a Query Specific File Request command to the server to
        /// request for a file that is specific and unique to it. Such file could contain
        /// non-firmware data such as security credential (needed for upgrading from Smart
        /// Energy 1.1 to Smart Energy 2.0), configuration or log. When the device decides to
        /// send the Query Specific File Request command is manufacturer specific. However,
        /// one example is during upgrading from SE 1.1 to 2.0 where the client may have already
        /// obtained new SE 2.0 image and now needs new SE 2.0 security credential data.
        ///
        /// <param name="requestNodeAddress" <see cref="IeeeAddress"> Request Node Address</ param >
        /// <param name="manufacturerCode" <see cref="ushort"> Manufacturer Code</ param >
        /// <param name="imageType" <see cref="ushort"> Image Type</ param >
        /// <param name="fileVersion" <see cref="uint"> File Version</ param >
        /// <param name="zigbeeStackVersion" <see cref="ushort"> Zigbee Stack Version</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> QuerySpecificFileCommand(IeeeAddress requestNodeAddress, ushort manufacturerCode, ushort imageType, uint fileVersion, ushort zigbeeStackVersion)
        {
            QuerySpecificFileCommand command = new QuerySpecificFileCommand();

            // Set the fields
            command.RequestNodeAddress = requestNodeAddress;
            command.ManufacturerCode = manufacturerCode;
            command.ImageType = imageType;
            command.FileVersion = fileVersion;
            command.ZigbeeStackVersion = zigbeeStackVersion;

            return Send(command);
        }

        /// <summary>
        /// The Query Specific File Response
        ///
        /// The server sends Query Specific File Response after receiving Query Specific File
        /// Request from a client. The server shall determine whether it first supports the
        /// Query Specific File Request command. Then it shall determine whether it has the
        /// specific file being requested by the client using all the information included in
        /// the request. The upgrade server sends a Query Specific File Response with one of the
        /// following status: SUCCESS, NO_IMAGE_AVAILABLE or NOT_AUTHORIZED. <br> A status
        /// of NO_IMAGE_AVAILABLE indicates that the server currently does not have the
        /// device specific file available for the client. A status of NOT_AUTHORIZED
        /// indicates the server is not authorized to send the file to the client.
        ///
        /// <param name="status" <see cref="ZclStatus"> Status</ param >
        /// <param name="manufacturerCode" <see cref="ushort"> Manufacturer Code</ param >
        /// <param name="imageType" <see cref="ushort"> Image Type</ param >
        /// <param name="fileVersion" <see cref="uint"> File Version</ param >
        /// <param name="imageSize" <see cref="uint"> Image Size</ param >
        /// <returns> the command result Task </returns>
        /// </summary>
        public Task<CommandResult> QuerySpecificFileResponse(ZclStatus status, ushort manufacturerCode, ushort imageType, uint fileVersion, uint imageSize)
        {
            QuerySpecificFileResponse command = new QuerySpecificFileResponse();

            // Set the fields
            command.Status = status;
            command.ManufacturerCode = manufacturerCode;
            command.ImageType = imageType;
            command.FileVersion = fileVersion;
            command.ImageSize = imageSize;

            return Send(command);
        }
    }
}
