using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Betfair.ESASwagger.Model {
    /// <summary>
    /// </summary>
    [DataContract]
    public class RunnerChange : IEquatable<RunnerChange> {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RunnerChange" /> class.
        ///     Initializes a new instance of the <see cref="RunnerChange" />class.
        /// </summary>
        /// <param name="Tv">The total amount matched. This value is truncated at 2dp..</param>
        /// <param name="Batb">Best Available To Back - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove).</param>
        /// <param name="Spb">Starting Price Back - PriceVol tuple delta of price changes (0 vol is remove).</param>
        /// <param name="Bdatl">Best Display Available To Lay (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove).</param>
        /// <param name="Trd">Traded - PriceVol tuple delta of price changes (0 vol is remove).</param>
        /// <param name="Spf">Starting Price Far - The far starting price (or null if un-changed).</param>
        /// <param name="Ltp">Last Traded Price - The last traded price (or null if un-changed).</param>
        /// <param name="Atb">Available To Back - PriceVol tuple delta of price changes (0 vol is remove).</param>
        /// <param name="Spl">Starting Price Lay - PriceVol tuple delta of price changes (0 vol is remove).</param>
        /// <param name="Spn">Starting Price Near - The far starting price (or null if un-changed).</param>
        /// <param name="Atl">Available To Lay - PriceVol tuple delta of price changes (0 vol is remove).</param>
        /// <param name="Batl">Best Available To Lay - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove).</param>
        /// <param name="Id">Selection Id - the id of the runner (selection).</param>
        /// <param name="Hc">Handicap - the handicap of the runner (selection) (null if not applicable).</param>
        /// <param name="Bdatb">Best Display Available To Back (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove).</param>
        public RunnerChange(
            double? Tv = null,
            List<List<double?>> Batb = null,
            List<List<double?>> Spb = null,
            List<List<double?>> Bdatl = null,
            List<List<double?>> Trd = null,
            double? Spf = null,
            double? Ltp = null,
            List<List<double?>> Atb = null,
            List<List<double?>> Spl = null,
            double? Spn = null,
            List<List<double?>> Atl = null,
            List<List<double?>> Batl = null,
            long? Id = null,
            double? Hc = null,
            List<List<double?>> Bdatb = null) {
            this.Tv = Tv;
            this.Batb = Batb;
            this.Spb = Spb;
            this.Bdatl = Bdatl;
            this.Trd = Trd;
            this.Spf = Spf;
            this.Ltp = Ltp;
            this.Atb = Atb;
            this.Spl = Spl;
            this.Spn = Spn;
            this.Atl = Atl;
            this.Batl = Batl;
            this.Id = Id;
            this.Hc = Hc;
            this.Bdatb = Bdatb;
        }


        /// <summary>
        ///     The total amount matched. This value is truncated at 2dp.
        /// </summary>
        /// <value>The total amount matched. This value is truncated at 2dp.</value>
        [DataMember(Name = "tv", EmitDefaultValue = false)]
        public double? Tv { get; set; }

        /// <summary>
        ///     Best Available To Back - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
        /// </summary>
        /// <value>Best Available To Back - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)</value>
        [DataMember(Name = "batb", EmitDefaultValue = false)]
        public List<List<double?>> Batb { get; set; }

        /// <summary>
        ///     Starting Price Back - PriceVol tuple delta of price changes (0 vol is remove)
        /// </summary>
        /// <value>Starting Price Back - PriceVol tuple delta of price changes (0 vol is remove)</value>
        [DataMember(Name = "spb", EmitDefaultValue = false)]
        public List<List<double?>> Spb { get; set; }

        /// <summary>
        ///     Best Display Available To Lay (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
        /// </summary>
        /// <value>Best Display Available To Lay (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)</value>
        [DataMember(Name = "bdatl", EmitDefaultValue = false)]
        public List<List<double?>> Bdatl { get; set; }

        /// <summary>
        ///     Traded - PriceVol tuple delta of price changes (0 vol is remove)
        /// </summary>
        /// <value>Traded - PriceVol tuple delta of price changes (0 vol is remove)</value>
        [DataMember(Name = "trd", EmitDefaultValue = false)]
        public List<List<double?>> Trd { get; set; }

        /// <summary>
        ///     Starting Price Far - The far starting price (or null if un-changed)
        /// </summary>
        /// <value>Starting Price Far - The far starting price (or null if un-changed)</value>
        [DataMember(Name = "spf", EmitDefaultValue = false)]
        public double? Spf { get; set; }

        /// <summary>
        ///     Last Traded Price - The last traded price (or null if un-changed)
        /// </summary>
        /// <value>Last Traded Price - The last traded price (or null if un-changed)</value>
        [DataMember(Name = "ltp", EmitDefaultValue = false)]
        public double? Ltp { get; set; }

        /// <summary>
        ///     Available To Back - PriceVol tuple delta of price changes (0 vol is remove)
        /// </summary>
        /// <value>Available To Back - PriceVol tuple delta of price changes (0 vol is remove)</value>
        [DataMember(Name = "atb", EmitDefaultValue = false)]
        public List<List<double?>> Atb { get; set; }

        /// <summary>
        ///     Starting Price Lay - PriceVol tuple delta of price changes (0 vol is remove)
        /// </summary>
        /// <value>Starting Price Lay - PriceVol tuple delta of price changes (0 vol is remove)</value>
        [DataMember(Name = "spl", EmitDefaultValue = false)]
        public List<List<double?>> Spl { get; set; }

        /// <summary>
        ///     Starting Price Near - The far starting price (or null if un-changed)
        /// </summary>
        /// <value>Starting Price Near - The far starting price (or null if un-changed)</value>
        [DataMember(Name = "spn", EmitDefaultValue = false)]
        public double? Spn { get; set; }

        /// <summary>
        ///     Available To Lay - PriceVol tuple delta of price changes (0 vol is remove)
        /// </summary>
        /// <value>Available To Lay - PriceVol tuple delta of price changes (0 vol is remove)</value>
        [DataMember(Name = "atl", EmitDefaultValue = false)]
        public List<List<double?>> Atl { get; set; }

        /// <summary>
        ///     Best Available To Lay - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
        /// </summary>
        /// <value>Best Available To Lay - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)</value>
        [DataMember(Name = "batl", EmitDefaultValue = false)]
        public List<List<double?>> Batl { get; set; }

        /// <summary>
        ///     Selection Id - the id of the runner (selection)
        /// </summary>
        /// <value>Selection Id - the id of the runner (selection)</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        ///     Handicap - the handicap of the runner (selection) (null if not applicable)
        /// </summary>
        /// <value>Handicap - the handicap of the runner (selection) (null if not applicable)</value>
        [DataMember(Name = "hc", EmitDefaultValue = false)]
        public double? Hc { get; set; }

        /// <summary>
        ///     Best Display Available To Back (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
        /// </summary>
        /// <value>Best Display Available To Back (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)</value>
        [DataMember(Name = "bdatb", EmitDefaultValue = false)]
        public List<List<double?>> Bdatb { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class RunnerChange {\n");
            sb.Append("  Tv: ")
                .Append(Tv)
                .Append("\n");
            sb.Append("  Batb: ")
                .Append(Batb)
                .Append("\n");
            sb.Append("  Spb: ")
                .Append(Spb)
                .Append("\n");
            sb.Append("  Bdatl: ")
                .Append(Bdatl)
                .Append("\n");
            sb.Append("  Trd: ")
                .Append(Trd)
                .Append("\n");
            sb.Append("  Spf: ")
                .Append(Spf)
                .Append("\n");
            sb.Append("  Ltp: ")
                .Append(Ltp)
                .Append("\n");
            sb.Append("  Atb: ")
                .Append(Atb)
                .Append("\n");
            sb.Append("  Spl: ")
                .Append(Spl)
                .Append("\n");
            sb.Append("  Spn: ")
                .Append(Spn)
                .Append("\n");
            sb.Append("  Atl: ")
                .Append(Atl)
                .Append("\n");
            sb.Append("  Batl: ")
                .Append(Batl)
                .Append("\n");
            sb.Append("  Id: ")
                .Append(Id)
                .Append("\n");
            sb.Append("  Hc: ")
                .Append(Hc)
                .Append("\n");
            sb.Append("  Bdatb: ")
                .Append(Bdatb)
                .Append("\n");

            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        ///     Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        ///     Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj) {
            // credit: http://stackoverflow.com/a/10454552/677735
            return Equals(obj as RunnerChange);
        }

        /// <summary>
        ///     Returns true if RunnerChange instances are equal
        /// </summary>
        /// <param name="other">Instance of RunnerChange to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RunnerChange other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return (Tv == other.Tv || Tv != null && Tv.Equals(other.Tv)) &&
                   (Batb == other.Batb || Batb != null && Batb.SequenceEqual(other.Batb)) &&
                   (Spb == other.Spb || Spb != null && Spb.SequenceEqual(other.Spb)) &&
                   (Bdatl == other.Bdatl || Bdatl != null && Bdatl.SequenceEqual(other.Bdatl)) &&
                   (Trd == other.Trd || Trd != null && Trd.SequenceEqual(other.Trd)) &&
                   (Spf == other.Spf || Spf != null && Spf.Equals(other.Spf)) &&
                   (Ltp == other.Ltp || Ltp != null && Ltp.Equals(other.Ltp)) &&
                   (Atb == other.Atb || Atb != null && Atb.SequenceEqual(other.Atb)) &&
                   (Spl == other.Spl || Spl != null && Spl.SequenceEqual(other.Spl)) &&
                   (Spn == other.Spn || Spn != null && Spn.Equals(other.Spn)) &&
                   (Atl == other.Atl || Atl != null && Atl.SequenceEqual(other.Atl)) &&
                   (Batl == other.Batl || Batl != null && Batl.SequenceEqual(other.Batl)) &&
                   (Id == other.Id || Id != null && Id.Equals(other.Id)) &&
                   (Hc == other.Hc || Hc != null && Hc.Equals(other.Hc)) &&
                   (Bdatb == other.Bdatb || Bdatb != null && Bdatb.SequenceEqual(other.Bdatb));
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

                if (Tv != null)
                    hash = hash * 59 + Tv.GetHashCode();

                if (Batb != null)
                    hash = hash * 59 + Batb.GetHashCode();

                if (Spb != null)
                    hash = hash * 59 + Spb.GetHashCode();

                if (Bdatl != null)
                    hash = hash * 59 + Bdatl.GetHashCode();

                if (Trd != null)
                    hash = hash * 59 + Trd.GetHashCode();

                if (Spf != null)
                    hash = hash * 59 + Spf.GetHashCode();

                if (Ltp != null)
                    hash = hash * 59 + Ltp.GetHashCode();

                if (Atb != null)
                    hash = hash * 59 + Atb.GetHashCode();

                if (Spl != null)
                    hash = hash * 59 + Spl.GetHashCode();

                if (Spn != null)
                    hash = hash * 59 + Spn.GetHashCode();

                if (Atl != null)
                    hash = hash * 59 + Atl.GetHashCode();

                if (Batl != null)
                    hash = hash * 59 + Batl.GetHashCode();

                if (Id != null)
                    hash = hash * 59 + Id.GetHashCode();

                if (Hc != null)
                    hash = hash * 59 + Hc.GetHashCode();

                if (Bdatb != null)
                    hash = hash * 59 + Bdatb.GetHashCode();

                return hash;
            }
        }
    }
}
