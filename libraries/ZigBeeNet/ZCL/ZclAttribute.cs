using System;
using System.Collections.Generic;
using System.Text;
using ZigBeeNet.DAO;
using ZigBeeNet.ZCL.Protocol;

namespace ZigBeeNet.ZCL
{
    public class ZclAttribute
    {
        /// <summary>
        /// </summary>
        private ZclCluster _cluster;

        /// <summary>
        /// The attribute identifier field is 16-bits in length and shall contain the
        /// identifier of the attribute that the reporting configuration details
        /// apply to.
        /// </summary>
        public ushort Id { get; set; }

        /// <summary>
        /// Stores the name of this attribute;
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Defines the ZigBee data type.
        /// </summary>
        public ZclDataType DataType { get; set; }

        /// <summary>
        /// Returns true if the implementation of this attribute in the cluster is
        /// mandatory as required by the ZigBee standard. <br>
        /// Note that this does not necessarily mean that the attribute is actually
        /// implemented in any device if it does not conform to the standard.
        ///
        /// return true if the attribute must be implemented
        /// </summary>
        public bool Mandatory { get; set; }

        /// <summary>
        /// Returns true if this attribute is supported by this device
        /// </summary>
        public bool Implemented { get; set; }

        /// <summary>
        /// True if this attribute is readable
        /// </summary>
        public bool Readable { get; set; }

        /// <summary>
        /// True if this attribute is writeable
        /// </summary>
        public bool Writable { get; set; }

        /// <summary>
        /// True if this attribute is reportable
        /// </summary>
        public bool Reportable { get; set; }

        /// <summary>
        /// The minimum reporting interval field is 16-bits in length and shall
        /// contain the minimum interval, in seconds, between issuing reports for the
        /// attribute specified in the attribute identifier field. If the minimum
        /// reporting interval has not been configured, this field shall contain the
        /// value 0xffff.
        /// </summary>
        public int MinimumReportingPeriod { get; set; }

        /// <summary>
        /// The maximum reporting interval field is 16-bits in length and shall
        /// contain the maximum interval, in seconds, between issuing reports for the
        /// attribute specified in the attribute identifier field. If the maximum
        /// reporting interval has not been configured, this field shall contain the
        /// value 0xffff.
        /// </summary>
        public int MaximumReportingPeriod { get; set; }

        /// <summary>
        /// The reportable change field shall contain the minimum change to the
        /// attribute that will result in a report being issued. For attributes with
        /// 'analog' data type the field has the same data type as the attribute. If
        /// the reportable change has not been configured, this field shall contain
        /// the invalid value for the relevant data type
        /// </summary>
        public object ReportingChange { get; set; }

        /// <summary>
        /// The timeout period field is 16-bits in length and shall contain the
        /// maximum expected time, in seconds, between received reports for the
        /// attribute specified in the attribute identifier field. If the timeout
        /// period has not been configured, this field shall contain the value
        /// 0xffff.
        /// </summary>
        public int ReportingTimeout { get; set; }

        /// <summary>
        /// The manufacturer code of this attribute. If null, the attribute is not manufacturer-specific.
        /// </summary>
        public int? ManufacturerCode { get; set; }

        /// <summary>
        /// Records the last time a report was received
        /// </summary>
        public DateTime LastReportTime { get; set; }

        /// <summary>
        /// Records the last value received
        /// </summary>
        public object LastValue { get; set; }

        /// <summary>
        /// Default ctor
        /// </summary>
        public ZclAttribute()
        {
        }

        /// <summary>
        /// Constructor used to set the static information (for non-manufacturer-specific attribute)
        /// </summary>
        public ZclAttribute(ZclCluster cluster, ushort id, string name, ZclDataType dataType,
                bool mandatory, bool readable, bool writeable, bool reportable)
        {
            this._cluster = cluster;
            this.Id = id;
            this.Name = name;
            this.DataType = dataType;
            this.Mandatory = mandatory;
            this.Readable = readable;
            this.Writable = writeable;
            this.Reportable = reportable;
        }

        ///<summary>
        /// Constructor used to set the static information (for manufacturer-specific attribute)
        ///
        /// <para>cluster the <see cref="ZclCluster"/> to which the attribute belongs </para>
        /// <para>id the attribute ID</para>
        /// <para>name the human readable name</para>
        /// <para>dataType the <see cref="Protocol.DataType"/> for this attribute</para>
        /// <para>mandatory true if this is defined as mandatory in the ZCL specification</para>
        /// <para>readable true if this is defined as readable in the ZCL specification</para>
        /// <para>writable true if this is defined as writable in the ZCL specification</para>
        /// <para>reportable true if this is defined as reportable in the ZCL specification</para>
        /// <para>manufacturerCode the code for the manufacturer specific cluster, for ex. 0x1234</para>
        ///</summary>
        public ZclAttribute(ZclCluster cluster, ushort id, string name, ZclDataType dataType,
                bool mandatory, bool readable, bool writable, bool reportable, int manufacturerCode)
        {
            this._cluster = cluster;
            this.Id = id;
            this.Name = name;
            this.DataType = dataType;
            this.Mandatory = mandatory;
            this.Readable = readable;
            this.Writable = writable;
            this.Reportable = reportable;
            this.ManufacturerCode = manufacturerCode;
        }

        /// <summary>
        /// <returns>whether this is a manufacturer-specific attribute</returns> 
        /// </summary>
        public bool IsManufacturerSpecific()
        {
            return ManufacturerCode != null;
        }

        /// <summary>
        /// Checks if the last value received for the attribute is still current.
        /// If the last update time is more recent than the allowedAge then this will return true. allowedAge is defined in
        /// milliseconds.
        ///
        /// <param name="allowedAge">the number of milliseconds to consider the value current</param>
        /// <returns>true if the last value can be considered current</returns>
        /// </summary>
        public bool IsLastValueCurrent(long allowedAge)
        {
            if (LastReportTime == null)
            {
                return false;
            }

            if (allowedAge > TimeSpan.MaxValue.TotalMilliseconds)
                return true;

            DateTime refreshTime = DateTime.UtcNow.Subtract(TimeSpan.FromMilliseconds(allowedAge));

            return LastReportTime > refreshTime;
        }

        /// <summary>
        /// Updates the attribute value This will also record the time of the last update
        ///
        /// @param attributeValue
        ///            the attribute value to be updated <see cref="Object">
        /// </summary>
        public void UpdateValue(object attributeValue)
        {
            LastValue = attributeValue;
            LastReportTime = DateTime.UtcNow;
        }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("ZclAttribute [cluster=")
                   .Append(_cluster)
                   .Append(", id=")
                   .Append(Id)
                   .Append(", name=")
                   .Append(Name)
                   .Append(", dataType=")
                   .Append(DataType)
                   .Append(", lastValue=")
                   .Append(LastValue);

            if (LastReportTime != null)
            {
                builder.Append(", lastReportTime=")
                       .Append(LastReportTime.ToLongTimeString());
            }

            builder.Append(']');

            return builder.ToString();
        }

        /// <summary>
        ///Sets the state of the attribute from a ZclAttributeDao which has been restored from a persisted state.
        /// @param dao the ZclAttributeDao to restore
        /// </summary>
        public void SetDao(ZclCluster cluster, ZclAttributeDao dao)
        {
            this._cluster = cluster;
            Id = dao.Id;
            Name = dao.Name;
            DataType = dao.DataType;
            Mandatory = dao.Mandatory;
            Implemented = dao.Implemented;
            Writable = dao.Writable;
            Readable = dao.Readable;
            Reportable = dao.Reportable;
            LastValue = dao.LastValue;
            LastReportTime = dao.LastReportTime;
            MinimumReportingPeriod = dao.MinimumReportingPeriod;
            MaximumReportingPeriod = dao.MaximumReportingPeriod;
            ReportingChange = dao.ReportingChange;
            ReportingTimeout = dao.ReportingTimeout;
        }

        /// <summary>
        ///Returns a Data Acquisition Object for this attribute. This is a clean class recording the state of the primary
        ///fields of the attribute for persistence purposes.
        ///
        ///@return the {@link ZclAttributeDao} from this {@link ZclAttribute}
        /// </summary>
        public ZclAttributeDao GetDao()
        {
            ZclAttributeDao dao = new ZclAttributeDao();

            dao.Id = Id;
            dao.DataType = DataType;
            dao.Name = Name;
            dao.Mandatory = Mandatory;
            dao.Implemented = Implemented;
            dao.MinimumReportingPeriod = MinimumReportingPeriod;
            dao.MaximumReportingPeriod = MaximumReportingPeriod;
            dao.Readable = Readable;
            dao.Writable = Writable;
            dao.Reportable = Reportable;
            dao.ReportingChange = ReportingChange;
            dao.ReportingTimeout = ReportingTimeout;
            dao.LastValue = LastValue;
            dao.LastReportTime = LastReportTime;

            return dao;
        }
    }
}
