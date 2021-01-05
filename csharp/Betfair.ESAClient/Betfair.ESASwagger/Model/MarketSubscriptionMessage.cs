using System;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Betfair.ESASwagger.Model {
    /// <summary>
    /// </summary>
    [DataContract]
    public class MarketSubscriptionMessage : RequestMessage, IEquatable<MarketSubscriptionMessage> {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MarketSubscriptionMessage" /> class.
        ///     Initializes a new instance of the <see cref="MarketSubscriptionMessage" />class.
        /// </summary>
        /// <param name="Op">The operation type.</param>
        /// <param name="Id">Client generated unique id to link request with response (like json rpc).</param>
        /// <param name="SegmentationEnabled">Segmentation Enabled - allow the server to send large sets of data in segments, instead of a single block.</param>
        /// <param name="Clk">Token value delta (received in MarketChangeMessage) that should be passed to resume a subscription.</param>
        /// <param name="HeartbeatMs">Heartbeat Milliseconds - the heartbeat rate (looped back on initial image after validation: bounds are 500 to 30000).</param>
        /// <param name="InitialClk">Token value (received in initial MarketChangeMessage) that should be passed to resume a subscription.</param>
        /// <param name="MarketFilter">MarketFilter.</param>
        /// <param name="ConflateMs">Conflate Milliseconds - the conflation rate (looped back on initial image after validation: bounds are 0 to 120000).</param>
        /// <param name="MarketDataFilter">MarketDataFilter.</param>
        public MarketSubscriptionMessage(
            string Op = null,
            int? Id = null,
            bool? SegmentationEnabled = null,
            string Clk = null,
            long? HeartbeatMs = null,
            string InitialClk = null,
            MarketFilter MarketFilter = null,
            long? ConflateMs = null,
            MarketDataFilter MarketDataFilter = null) {
            this.Op = Op;
            this.Id = Id;
            this.SegmentationEnabled = SegmentationEnabled;
            this.Clk = Clk;
            this.HeartbeatMs = HeartbeatMs;
            this.InitialClk = InitialClk;
            this.MarketFilter = MarketFilter;
            this.ConflateMs = ConflateMs;
            this.MarketDataFilter = MarketDataFilter;
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
        ///     Segmentation Enabled - allow the server to send large sets of data in segments, instead of a single block
        /// </summary>
        /// <value>Segmentation Enabled - allow the server to send large sets of data in segments, instead of a single block</value>
        [DataMember(Name = "segmentationEnabled", EmitDefaultValue = false)]
        public bool? SegmentationEnabled { get; set; }

        /// <summary>
        ///     Token value delta (received in MarketChangeMessage) that should be passed to resume a subscription
        /// </summary>
        /// <value>Token value delta (received in MarketChangeMessage) that should be passed to resume a subscription</value>
        [DataMember(Name = "clk", EmitDefaultValue = false)]
        public string Clk { get; set; }

        /// <summary>
        ///     Heartbeat Milliseconds - the heartbeat rate (looped back on initial image after validation: bounds are 500 to 30000)
        /// </summary>
        /// <value>Heartbeat Milliseconds - the heartbeat rate (looped back on initial image after validation: bounds are 500 to 30000)</value>
        [DataMember(Name = "heartbeatMs", EmitDefaultValue = false)]
        public long? HeartbeatMs { get; set; }

        /// <summary>
        ///     Token value (received in initial MarketChangeMessage) that should be passed to resume a subscription
        /// </summary>
        /// <value>Token value (received in initial MarketChangeMessage) that should be passed to resume a subscription</value>
        [DataMember(Name = "initialClk", EmitDefaultValue = false)]
        public string InitialClk { get; set; }

        /// <summary>
        ///     Gets or Sets MarketFilter
        /// </summary>
        [DataMember(Name = "marketFilter", EmitDefaultValue = false)]
        public MarketFilter MarketFilter { get; set; }

        /// <summary>
        ///     Conflate Milliseconds - the conflation rate (looped back on initial image after validation: bounds are 0 to 120000)
        /// </summary>
        /// <value>Conflate Milliseconds - the conflation rate (looped back on initial image after validation: bounds are 0 to 120000)</value>
        [DataMember(Name = "conflateMs", EmitDefaultValue = false)]
        public long? ConflateMs { get; set; }

        /// <summary>
        ///     Gets or Sets MarketDataFilter
        /// </summary>
        [DataMember(Name = "marketDataFilter", EmitDefaultValue = false)]
        public MarketDataFilter MarketDataFilter { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class MarketSubscriptionMessage {\n");
            sb.Append("  Op: ")
                .Append(Op)
                .Append("\n");
            sb.Append("  Id: ")
                .Append(Id)
                .Append("\n");
            sb.Append("  SegmentationEnabled: ")
                .Append(SegmentationEnabled)
                .Append("\n");
            sb.Append("  Clk: ")
                .Append(Clk)
                .Append("\n");
            sb.Append("  HeartbeatMs: ")
                .Append(HeartbeatMs)
                .Append("\n");
            sb.Append("  InitialClk: ")
                .Append(InitialClk)
                .Append("\n");
            sb.Append("  MarketFilter: ")
                .Append(MarketFilter)
                .Append("\n");
            sb.Append("  ConflateMs: ")
                .Append(ConflateMs)
                .Append("\n");
            sb.Append("  MarketDataFilter: ")
                .Append(MarketDataFilter)
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
            return Equals(obj as MarketSubscriptionMessage);
        }

        /// <summary>
        ///     Returns true if MarketSubscriptionMessage instances are equal
        /// </summary>
        /// <param name="other">Instance of MarketSubscriptionMessage to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MarketSubscriptionMessage other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return (Op == other.Op || Op != null && Op.Equals(other.Op)) &&
                   (Id == other.Id || Id != null && Id.Equals(other.Id)) &&
                   (SegmentationEnabled == other.SegmentationEnabled || SegmentationEnabled != null && SegmentationEnabled.Equals(other.SegmentationEnabled)) &&
                   (Clk == other.Clk || Clk != null && Clk.Equals(other.Clk)) &&
                   (HeartbeatMs == other.HeartbeatMs || HeartbeatMs != null && HeartbeatMs.Equals(other.HeartbeatMs)) &&
                   (InitialClk == other.InitialClk || InitialClk != null && InitialClk.Equals(other.InitialClk)) &&
                   (MarketFilter == other.MarketFilter || MarketFilter != null && MarketFilter.Equals(other.MarketFilter)) &&
                   (ConflateMs == other.ConflateMs || ConflateMs != null && ConflateMs.Equals(other.ConflateMs)) &&
                   (MarketDataFilter == other.MarketDataFilter || MarketDataFilter != null && MarketDataFilter.Equals(other.MarketDataFilter));
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

                if (SegmentationEnabled != null)
                    hash = hash * 59 + SegmentationEnabled.GetHashCode();

                if (Clk != null)
                    hash = hash * 59 + Clk.GetHashCode();

                if (HeartbeatMs != null)
                    hash = hash * 59 + HeartbeatMs.GetHashCode();

                if (InitialClk != null)
                    hash = hash * 59 + InitialClk.GetHashCode();

                if (MarketFilter != null)
                    hash = hash * 59 + MarketFilter.GetHashCode();

                if (ConflateMs != null)
                    hash = hash * 59 + ConflateMs.GetHashCode();

                if (MarketDataFilter != null)
                    hash = hash * 59 + MarketDataFilter.GetHashCode();

                return hash;
            }
        }
    }
}
