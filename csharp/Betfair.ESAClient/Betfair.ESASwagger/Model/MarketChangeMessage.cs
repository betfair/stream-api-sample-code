using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Betfair.ESASwagger.Model {
    /// <summary>
    /// </summary>
    [DataContract]
    public class MarketChangeMessage : ResponseMessage, IEquatable<MarketChangeMessage> {
        /// <summary>
        ///     Change Type - set to indicate the type of change - if null this is a delta)
        /// </summary>
        /// <value>Change Type - set to indicate the type of change - if null this is a delta)</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CtEnum {
            [EnumMember(Value = "SUB_IMAGE")]
            SubImage,

            [EnumMember(Value = "RESUB_DELTA")]
            ResubDelta,

            [EnumMember(Value = "HEARTBEAT")]
            Heartbeat
        }


        /// <summary>
        ///     Segment Type - if the change is split into multiple segments, this denotes the beginning and end of a change, and segments in between. Will be null if data is not segmented
        /// </summary>
        /// <value>Segment Type - if the change is split into multiple segments, this denotes the beginning and end of a change, and segments in between. Will be null if data is not segmented</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SegmentTypeEnum {
            [EnumMember(Value = "SEG_START")]
            SegStart,

            [EnumMember(Value = "SEG")]
            Seg,

            [EnumMember(Value = "SEG_END")]
            SegEnd
        }


        /// <summary>
        ///     Change Type - set to indicate the type of change - if null this is a delta)
        /// </summary>
        /// <value>Change Type - set to indicate the type of change - if null this is a delta)</value>
        [DataMember(Name = "ct", EmitDefaultValue = false)]
        public CtEnum? Ct { get; set; }

        /// <summary>
        ///     Segment Type - if the change is split into multiple segments, this denotes the beginning and end of a change, and segments in between. Will be null if data is not segmented
        /// </summary>
        /// <value>Segment Type - if the change is split into multiple segments, this denotes the beginning and end of a change, and segments in between. Will be null if data is not segmented</value>
        [DataMember(Name = "segmentType", EmitDefaultValue = false)]
        public SegmentTypeEnum? SegmentType { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MarketChangeMessage" /> class.
        ///     Initializes a new instance of the <see cref="MarketChangeMessage" />class.
        /// </summary>
        /// <param name="Op">The operation type.</param>
        /// <param name="Id">Client generated unique id to link request with response (like json rpc).</param>
        /// <param name="Ct">Change Type - set to indicate the type of change - if null this is a delta).</param>
        /// <param name="Clk">Token value (non-null) should be stored and passed in a MarketSubscriptionMessage to resume subscription (in case of disconnect).</param>
        /// <param name="HeartbeatMs">Heartbeat Milliseconds - the heartbeat rate (may differ from requested: bounds are 500 to 30000).</param>
        /// <param name="Pt">Publish Time (in millis since epoch) that the changes were generated.</param>
        /// <param name="InitialClk">Token value (non-null) should be stored and passed in a MarketSubscriptionMessage to resume subscription (in case of disconnect).</param>
        /// <param name="Mc">MarketChanges - the modifications to markets (will be null on a heartbeat.</param>
        /// <param name="ConflateMs">Conflate Milliseconds - the conflation rate (may differ from that requested if subscription is delayed).</param>
        /// <param name="SegmentType">
        ///     Segment Type - if the change is split into multiple segments, this denotes the beginning and end of a change, and segments in between. Will be null if
        ///     data is not segmented.
        /// </param>
        public MarketChangeMessage(
            string Op = null,
            int? Id = null,
            CtEnum? Ct = null,
            string Clk = null,
            long? HeartbeatMs = null,
            long? Pt = null,
            string InitialClk = null,
            List<MarketChange> Mc = null,
            long? ConflateMs = null,
            SegmentTypeEnum? SegmentType = null) {
            this.Op = Op;
            this.Id = Id;
            this.Ct = Ct;
            this.Clk = Clk;
            this.HeartbeatMs = HeartbeatMs;
            this.Pt = Pt;
            this.InitialClk = InitialClk;
            this.Mc = Mc;
            this.ConflateMs = ConflateMs;
            this.SegmentType = SegmentType;
        }


        /// <summary>
        ///     The operation type
        /// </summary>
        /// <value>The operation type</value>
        [DataMember(Name = "op", EmitDefaultValue = false)]
        public string Op { get; set; }

        /// <summary>
        ///     Client generated unique id to link request with response (like json rpc)
        /// </summary>
        /// <value>Client generated unique id to link request with response (like json rpc)</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int? Id { get; set; }

        /// <summary>
        ///     Token value (non-null) should be stored and passed in a MarketSubscriptionMessage to resume subscription (in case of disconnect)
        /// </summary>
        /// <value>Token value (non-null) should be stored and passed in a MarketSubscriptionMessage to resume subscription (in case of disconnect)</value>
        [DataMember(Name = "clk", EmitDefaultValue = false)]
        public string Clk { get; set; }

        /// <summary>
        ///     Heartbeat Milliseconds - the heartbeat rate (may differ from requested: bounds are 500 to 30000)
        /// </summary>
        /// <value>Heartbeat Milliseconds - the heartbeat rate (may differ from requested: bounds are 500 to 30000)</value>
        [DataMember(Name = "heartbeatMs", EmitDefaultValue = false)]
        public long? HeartbeatMs { get; set; }

        /// <summary>
        ///     Publish Time (in millis since epoch) that the changes were generated
        /// </summary>
        /// <value>Publish Time (in millis since epoch) that the changes were generated</value>
        [DataMember(Name = "pt", EmitDefaultValue = false)]
        public long? Pt { get; set; }

        /// <summary>
        ///     Token value (non-null) should be stored and passed in a MarketSubscriptionMessage to resume subscription (in case of disconnect)
        /// </summary>
        /// <value>Token value (non-null) should be stored and passed in a MarketSubscriptionMessage to resume subscription (in case of disconnect)</value>
        [DataMember(Name = "initialClk", EmitDefaultValue = false)]
        public string InitialClk { get; set; }

        /// <summary>
        ///     MarketChanges - the modifications to markets (will be null on a heartbeat
        /// </summary>
        /// <value>MarketChanges - the modifications to markets (will be null on a heartbeat</value>
        [DataMember(Name = "mc", EmitDefaultValue = false)]
        public List<MarketChange> Mc { get; set; }

        /// <summary>
        ///     Conflate Milliseconds - the conflation rate (may differ from that requested if subscription is delayed)
        /// </summary>
        /// <value>Conflate Milliseconds - the conflation rate (may differ from that requested if subscription is delayed)</value>
        [DataMember(Name = "conflateMs", EmitDefaultValue = false)]
        public long? ConflateMs { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class MarketChangeMessage {\n");
            sb.Append("  Op: ")
                .Append(Op)
                .Append("\n");
            sb.Append("  Id: ")
                .Append(Id)
                .Append("\n");
            sb.Append("  Ct: ")
                .Append(Ct)
                .Append("\n");
            sb.Append("  Clk: ")
                .Append(Clk)
                .Append("\n");
            sb.Append("  HeartbeatMs: ")
                .Append(HeartbeatMs)
                .Append("\n");
            sb.Append("  Pt: ")
                .Append(Pt)
                .Append("\n");
            sb.Append("  InitialClk: ")
                .Append(InitialClk)
                .Append("\n");
            sb.Append("  Mc: ")
                .Append(Mc)
                .Append("\n");
            sb.Append("  ConflateMs: ")
                .Append(ConflateMs)
                .Append("\n");
            sb.Append("  SegmentType: ")
                .Append(SegmentType)
                .Append("\n");

            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        ///     Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public new string ToJson() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        ///     Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj) {
            // credit: http://stackoverflow.com/a/10454552/677735
            return Equals(obj as MarketChangeMessage);
        }

        /// <summary>
        ///     Returns true if MarketChangeMessage instances are equal
        /// </summary>
        /// <param name="other">Instance of MarketChangeMessage to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MarketChangeMessage other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return (Op == other.Op || Op != null && Op.Equals(other.Op)) &&
                   (Id == other.Id || Id != null && Id.Equals(other.Id)) &&
                   (Ct == other.Ct || Ct != null && Ct.Equals(other.Ct)) &&
                   (Clk == other.Clk || Clk != null && Clk.Equals(other.Clk)) &&
                   (HeartbeatMs == other.HeartbeatMs || HeartbeatMs != null && HeartbeatMs.Equals(other.HeartbeatMs)) &&
                   (Pt == other.Pt || Pt != null && Pt.Equals(other.Pt)) &&
                   (InitialClk == other.InitialClk || InitialClk != null && InitialClk.Equals(other.InitialClk)) &&
                   (Mc == other.Mc || Mc != null && Mc.SequenceEqual(other.Mc)) &&
                   (ConflateMs == other.ConflateMs || ConflateMs != null && ConflateMs.Equals(other.ConflateMs)) &&
                   (SegmentType == other.SegmentType || SegmentType != null && SegmentType.Equals(other.SegmentType));
        }

        /// <summary>
        ///     Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode() {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                var hash = 41;
                // Suitable nullity checks etc, of course :)

                if (Op != null)
                    hash = hash * 59 + Op.GetHashCode();

                if (Id != null)
                    hash = hash * 59 + Id.GetHashCode();

                if (Ct != null)
                    hash = hash * 59 + Ct.GetHashCode();

                if (Clk != null)
                    hash = hash * 59 + Clk.GetHashCode();

                if (HeartbeatMs != null)
                    hash = hash * 59 + HeartbeatMs.GetHashCode();

                if (Pt != null)
                    hash = hash * 59 + Pt.GetHashCode();

                if (InitialClk != null)
                    hash = hash * 59 + InitialClk.GetHashCode();

                if (Mc != null)
                    hash = hash * 59 + Mc.GetHashCode();

                if (ConflateMs != null)
                    hash = hash * 59 + ConflateMs.GetHashCode();

                if (SegmentType != null)
                    hash = hash * 59 + SegmentType.GetHashCode();

                return hash;
            }
        }
    }
}
