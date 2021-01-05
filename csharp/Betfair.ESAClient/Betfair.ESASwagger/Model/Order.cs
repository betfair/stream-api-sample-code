using System;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Betfair.ESASwagger.Model {
    /// <summary>
    /// </summary>
    [DataContract]
    public class Order : IEquatable<Order> {
        /// <summary>
        ///     Side - the side of the order
        /// </summary>
        /// <value>Side - the side of the order</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum SideEnum {
            [EnumMember(Value = "B")]
            B,

            [EnumMember(Value = "L")]
            L
        }


        /// <summary>
        ///     Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close)
        /// </summary>
        /// <value>Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close)</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PtEnum {
            [EnumMember(Value = "L")]
            L,

            [EnumMember(Value = "P")]
            P,

            [EnumMember(Value = "MOC")]
            Moc
        }


        /// <summary>
        ///     Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE)
        /// </summary>
        /// <value>Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE)</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum OtEnum {
            [EnumMember(Value = "L")]
            L,

            [EnumMember(Value = "LOC")]
            Loc,

            [EnumMember(Value = "MOC")]
            Moc
        }


        /// <summary>
        ///     Status - the status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE)
        /// </summary>
        /// <value>Status - the status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE)</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum {
            [EnumMember(Value = "E")]
            E,

            [EnumMember(Value = "EC")]
            Ec
        }


        /// <summary>
        ///     Side - the side of the order
        /// </summary>
        /// <value>Side - the side of the order</value>
        [DataMember(Name = "side", EmitDefaultValue = false)]
        public SideEnum? Side { get; set; }

        /// <summary>
        ///     Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close)
        /// </summary>
        /// <value>Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close)</value>
        [DataMember(Name = "pt", EmitDefaultValue = false)]
        public PtEnum? Pt { get; set; }

        /// <summary>
        ///     Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE)
        /// </summary>
        /// <value>Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE)</value>
        [DataMember(Name = "ot", EmitDefaultValue = false)]
        public OtEnum? Ot { get; set; }

        /// <summary>
        ///     Status - the status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE)
        /// </summary>
        /// <value>Status - the status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE)</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public StatusEnum? Status { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Order" /> class.
        ///     Initializes a new instance of the <see cref="Order" />class.
        /// </summary>
        /// <param name="Side">Side - the side of the order.</param>
        /// <param name="Sv">Size Voided - the amount of the order that has been voided.</param>
        /// <param name="Pt">Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close).</param>
        /// <param name="Ot">Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE).</param>
        /// <param name="P">Price - the original placed price of the order.</param>
        /// <param name="Sc">Size Cancelled - the amount of the order that has been cancelled.</param>
        /// <param name="Rc">Regulator Code - the regulator of the order.</param>
        /// <param name="S">Size - the original placed size of the order.</param>
        /// <param name="Pd">Placed Date - the date the order was placed.</param>
        /// <param name="Rac">Regulator Auth Code - the auth code returned by the regulator.</param>
        /// <param name="Md">Matched Date - the date the order was matched (null if the order is not matched).</param>
        /// <param name="Sl">Size Lapsed - the amount of the order that has been lapsed.</param>
        /// <param name="Avp">Average Price Matched - the average price the order was matched at (null if the order is not matched.</param>
        /// <param name="Sm">Size Matched - the amount of the order that has been matched.</param>
        /// <param name="Id">Bet Id - the id of the order.</param>
        /// <param name="Bsp">BSP Liability - the BSP liability of the order (null if the order is not a BSP order).</param>
        /// <param name="Status">Status - the status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE).</param>
        /// <param name="Sr">Size Remaining - the amount of the order that is remaining unmatched.</param>
        /// <param name="Cd">Cancelled Date - the date the order was cancelled (null if the order is not cancelled).</param>
        public Order(
            SideEnum? Side = null,
            double? Sv = null,
            PtEnum? Pt = null,
            OtEnum? Ot = null,
            double? P = null,
            double? Sc = null,
            string Rc = null,
            double? S = null,
            long? Pd = null,
            string Rac = null,
            long? Md = null,
            double? Sl = null,
            double? Avp = null,
            double? Sm = null,
            string Id = null,
            double? Bsp = null,
            StatusEnum? Status = null,
            double? Sr = null,
            long? Cd = null) {
            this.Side = Side;
            this.Sv = Sv;
            this.Pt = Pt;
            this.Ot = Ot;
            this.P = P;
            this.Sc = Sc;
            this.Rc = Rc;
            this.S = S;
            this.Pd = Pd;
            this.Rac = Rac;
            this.Md = Md;
            this.Sl = Sl;
            this.Avp = Avp;
            this.Sm = Sm;
            this.Id = Id;
            this.Bsp = Bsp;
            this.Status = Status;
            this.Sr = Sr;
            this.Cd = Cd;
        }


        /// <summary>
        ///     Size Voided - the amount of the order that has been voided
        /// </summary>
        /// <value>Size Voided - the amount of the order that has been voided</value>
        [DataMember(Name = "sv", EmitDefaultValue = false)]
        public double? Sv { get; set; }

        /// <summary>
        ///     Price - the original placed price of the order
        /// </summary>
        /// <value>Price - the original placed price of the order</value>
        [DataMember(Name = "p", EmitDefaultValue = false)]
        public double? P { get; set; }

        /// <summary>
        ///     Size Cancelled - the amount of the order that has been cancelled
        /// </summary>
        /// <value>Size Cancelled - the amount of the order that has been cancelled</value>
        [DataMember(Name = "sc", EmitDefaultValue = false)]
        public double? Sc { get; set; }

        /// <summary>
        ///     Regulator Code - the regulator of the order
        /// </summary>
        /// <value>Regulator Code - the regulator of the order</value>
        [DataMember(Name = "rc", EmitDefaultValue = false)]
        public string Rc { get; set; }

        /// <summary>
        ///     Size - the original placed size of the order
        /// </summary>
        /// <value>Size - the original placed size of the order</value>
        [DataMember(Name = "s", EmitDefaultValue = false)]
        public double? S { get; set; }

        /// <summary>
        ///     Placed Date - the date the order was placed
        /// </summary>
        /// <value>Placed Date - the date the order was placed</value>
        [DataMember(Name = "pd", EmitDefaultValue = false)]
        public long? Pd { get; set; }

        /// <summary>
        ///     Regulator Auth Code - the auth code returned by the regulator
        /// </summary>
        /// <value>Regulator Auth Code - the auth code returned by the regulator</value>
        [DataMember(Name = "rac", EmitDefaultValue = false)]
        public string Rac { get; set; }

        /// <summary>
        ///     Matched Date - the date the order was matched (null if the order is not matched)
        /// </summary>
        /// <value>Matched Date - the date the order was matched (null if the order is not matched)</value>
        [DataMember(Name = "md", EmitDefaultValue = false)]
        public long? Md { get; set; }

        /// <summary>
        ///     Size Lapsed - the amount of the order that has been lapsed
        /// </summary>
        /// <value>Size Lapsed - the amount of the order that has been lapsed</value>
        [DataMember(Name = "sl", EmitDefaultValue = false)]
        public double? Sl { get; set; }

        /// <summary>
        ///     Average Price Matched - the average price the order was matched at (null if the order is not matched
        /// </summary>
        /// <value>Average Price Matched - the average price the order was matched at (null if the order is not matched</value>
        [DataMember(Name = "avp", EmitDefaultValue = false)]
        public double? Avp { get; set; }

        /// <summary>
        ///     Size Matched - the amount of the order that has been matched
        /// </summary>
        /// <value>Size Matched - the amount of the order that has been matched</value>
        [DataMember(Name = "sm", EmitDefaultValue = false)]
        public double? Sm { get; set; }

        /// <summary>
        ///     Bet Id - the id of the order
        /// </summary>
        /// <value>Bet Id - the id of the order</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        ///     BSP Liability - the BSP liability of the order (null if the order is not a BSP order)
        /// </summary>
        /// <value>BSP Liability - the BSP liability of the order (null if the order is not a BSP order)</value>
        [DataMember(Name = "bsp", EmitDefaultValue = false)]
        public double? Bsp { get; set; }

        /// <summary>
        ///     Size Remaining - the amount of the order that is remaining unmatched
        /// </summary>
        /// <value>Size Remaining - the amount of the order that is remaining unmatched</value>
        [DataMember(Name = "sr", EmitDefaultValue = false)]
        public double? Sr { get; set; }

        /// <summary>
        ///     Cancelled Date - the date the order was cancelled (null if the order is not cancelled)
        /// </summary>
        /// <value>Cancelled Date - the date the order was cancelled (null if the order is not cancelled)</value>
        [DataMember(Name = "cd", EmitDefaultValue = false)]
        public long? Cd { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class Order {\n");
            sb.Append("  Side: ")
                .Append(Side)
                .Append("\n");
            sb.Append("  Sv: ")
                .Append(Sv)
                .Append("\n");
            sb.Append("  Pt: ")
                .Append(Pt)
                .Append("\n");
            sb.Append("  Ot: ")
                .Append(Ot)
                .Append("\n");
            sb.Append("  P: ")
                .Append(P)
                .Append("\n");
            sb.Append("  Sc: ")
                .Append(Sc)
                .Append("\n");
            sb.Append("  Rc: ")
                .Append(Rc)
                .Append("\n");
            sb.Append("  S: ")
                .Append(S)
                .Append("\n");
            sb.Append("  Pd: ")
                .Append(Pd)
                .Append("\n");
            sb.Append("  Rac: ")
                .Append(Rac)
                .Append("\n");
            sb.Append("  Md: ")
                .Append(Md)
                .Append("\n");
            sb.Append("  Sl: ")
                .Append(Sl)
                .Append("\n");
            sb.Append("  Avp: ")
                .Append(Avp)
                .Append("\n");
            sb.Append("  Sm: ")
                .Append(Sm)
                .Append("\n");
            sb.Append("  Id: ")
                .Append(Id)
                .Append("\n");
            sb.Append("  Bsp: ")
                .Append(Bsp)
                .Append("\n");
            sb.Append("  Status: ")
                .Append(Status)
                .Append("\n");
            sb.Append("  Sr: ")
                .Append(Sr)
                .Append("\n");
            sb.Append("  Cd: ")
                .Append(Cd)
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
            return Equals(obj as Order);
        }

        /// <summary>
        ///     Returns true if Order instances are equal
        /// </summary>
        /// <param name="other">Instance of Order to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Order other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return (Side == other.Side || Side != null && Side.Equals(other.Side)) &&
                   (Sv == other.Sv || Sv != null && Sv.Equals(other.Sv)) &&
                   (Pt == other.Pt || Pt != null && Pt.Equals(other.Pt)) &&
                   (Ot == other.Ot || Ot != null && Ot.Equals(other.Ot)) &&
                   (P == other.P || P != null && P.Equals(other.P)) &&
                   (Sc == other.Sc || Sc != null && Sc.Equals(other.Sc)) &&
                   (Rc == other.Rc || Rc != null && Rc.Equals(other.Rc)) &&
                   (S == other.S || S != null && S.Equals(other.S)) &&
                   (Pd == other.Pd || Pd != null && Pd.Equals(other.Pd)) &&
                   (Rac == other.Rac || Rac != null && Rac.Equals(other.Rac)) &&
                   (Md == other.Md || Md != null && Md.Equals(other.Md)) &&
                   (Sl == other.Sl || Sl != null && Sl.Equals(other.Sl)) &&
                   (Avp == other.Avp || Avp != null && Avp.Equals(other.Avp)) &&
                   (Sm == other.Sm || Sm != null && Sm.Equals(other.Sm)) &&
                   (Id == other.Id || Id != null && Id.Equals(other.Id)) &&
                   (Bsp == other.Bsp || Bsp != null && Bsp.Equals(other.Bsp)) &&
                   (Status == other.Status || Status != null && Status.Equals(other.Status)) &&
                   (Sr == other.Sr || Sr != null && Sr.Equals(other.Sr)) &&
                   (Cd == other.Cd || Cd != null && Cd.Equals(other.Cd));
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

                if (Side != null)
                    hash = hash * 59 + Side.GetHashCode();

                if (Sv != null)
                    hash = hash * 59 + Sv.GetHashCode();

                if (Pt != null)
                    hash = hash * 59 + Pt.GetHashCode();

                if (Ot != null)
                    hash = hash * 59 + Ot.GetHashCode();

                if (P != null)
                    hash = hash * 59 + P.GetHashCode();

                if (Sc != null)
                    hash = hash * 59 + Sc.GetHashCode();

                if (Rc != null)
                    hash = hash * 59 + Rc.GetHashCode();

                if (S != null)
                    hash = hash * 59 + S.GetHashCode();

                if (Pd != null)
                    hash = hash * 59 + Pd.GetHashCode();

                if (Rac != null)
                    hash = hash * 59 + Rac.GetHashCode();

                if (Md != null)
                    hash = hash * 59 + Md.GetHashCode();

                if (Sl != null)
                    hash = hash * 59 + Sl.GetHashCode();

                if (Avp != null)
                    hash = hash * 59 + Avp.GetHashCode();

                if (Sm != null)
                    hash = hash * 59 + Sm.GetHashCode();

                if (Id != null)
                    hash = hash * 59 + Id.GetHashCode();

                if (Bsp != null)
                    hash = hash * 59 + Bsp.GetHashCode();

                if (Status != null)
                    hash = hash * 59 + Status.GetHashCode();

                if (Sr != null)
                    hash = hash * 59 + Sr.GetHashCode();

                if (Cd != null)
                    hash = hash * 59 + Cd.GetHashCode();

                return hash;
            }
        }
    }
}
