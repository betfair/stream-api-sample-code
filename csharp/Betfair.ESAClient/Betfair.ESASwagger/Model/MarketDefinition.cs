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
    public class MarketDefinition : IEquatable<MarketDefinition> {
        /// <summary>
        ///     Gets or Sets BettingType
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum BettingTypeEnum {
            [EnumMember(Value = "ODDS")]
            Odds,

            [EnumMember(Value = "LINE")]
            Line,

            [EnumMember(Value = "RANGE")]
            Range,

            [EnumMember(Value = "ASIAN_HANDICAP_DOUBLE_LINE")]
            AsianHandicapDoubleLine,

            [EnumMember(Value = "ASIAN_HANDICAP_SINGLE_LINE")]
            AsianHandicapSingleLine
        }


        /// <summary>
        ///     Gets or Sets Status
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum {
            [EnumMember(Value = "INACTIVE")]
            Inactive,

            [EnumMember(Value = "OPEN")]
            Open,

            [EnumMember(Value = "SUSPENDED")]
            Suspended,

            [EnumMember(Value = "CLOSED")]
            Closed
        }


        /// <summary>
        ///     Gets or Sets BettingType
        /// </summary>
        [DataMember(Name = "bettingType", EmitDefaultValue = false)]
        public BettingTypeEnum? BettingType { get; set; }

        /// <summary>
        ///     Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public StatusEnum? Status { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MarketDefinition" /> class.
        ///     Initializes a new instance of the <see cref="MarketDefinition" />class.
        /// </summary>
        /// <param name="Venue">Venue.</param>
        /// <param name="SettledTime">SettledTime.</param>
        /// <param name="Timezone">Timezone.</param>
        /// <param name="EachWayDivisor">EachWayDivisor.</param>
        /// <param name="Regulators">The market regulators..</param>
        /// <param name="MarketType">MarketType.</param>
        /// <param name="MarketBaseRate">MarketBaseRate.</param>
        /// <param name="NumberOfWinners">NumberOfWinners.</param>
        /// <param name="CountryCode">CountryCode.</param>
        /// <param name="InPlay">InPlay.</param>
        /// <param name="BetDelay">BetDelay.</param>
        /// <param name="BspMarket">BspMarket.</param>
        /// <param name="BettingType">BettingType.</param>
        /// <param name="NumberOfActiveRunners">NumberOfActiveRunners.</param>
        /// <param name="EventId">EventId.</param>
        /// <param name="CrossMatching">CrossMatching.</param>
        /// <param name="RunnersVoidable">RunnersVoidable.</param>
        /// <param name="TurnInPlayEnabled">TurnInPlayEnabled.</param>
        /// <param name="SuspendTime">SuspendTime.</param>
        /// <param name="DiscountAllowed">DiscountAllowed.</param>
        /// <param name="PersistenceEnabled">PersistenceEnabled.</param>
        /// <param name="Runners">Runners.</param>
        /// <param name="Version">Version.</param>
        /// <param name="EventTypeId">The Event Type the market is contained within..</param>
        /// <param name="Complete">Complete.</param>
        /// <param name="OpenDate">OpenDate.</param>
        /// <param name="MarketTime">MarketTime.</param>
        /// <param name="BspReconciled">BspReconciled.</param>
        /// <param name="Status">Status.</param>
        public MarketDefinition(
            string Venue = null,
            DateTime? SettledTime = null,
            string Timezone = null,
            double? EachWayDivisor = null,
            List<string> Regulators = null,
            string MarketType = null,
            double? MarketBaseRate = null,
            int? NumberOfWinners = null,
            string CountryCode = null,
            bool? InPlay = null,
            int? BetDelay = null,
            bool? BspMarket = null,
            BettingTypeEnum? BettingType = null,
            int? NumberOfActiveRunners = null,
            string EventId = null,
            bool? CrossMatching = null,
            bool? RunnersVoidable = null,
            bool? TurnInPlayEnabled = null,
            DateTime? SuspendTime = null,
            bool? DiscountAllowed = null,
            bool? PersistenceEnabled = null,
            List<RunnerDefinition> Runners = null,
            long? Version = null,
            string EventTypeId = null,
            bool? Complete = null,
            DateTime? OpenDate = null,
            DateTime? MarketTime = null,
            bool? BspReconciled = null,
            StatusEnum? Status = null) {
            this.Venue = Venue;
            this.SettledTime = SettledTime;
            this.Timezone = Timezone;
            this.EachWayDivisor = EachWayDivisor;
            this.Regulators = Regulators;
            this.MarketType = MarketType;
            this.MarketBaseRate = MarketBaseRate;
            this.NumberOfWinners = NumberOfWinners;
            this.CountryCode = CountryCode;
            this.InPlay = InPlay;
            this.BetDelay = BetDelay;
            this.BspMarket = BspMarket;
            this.BettingType = BettingType;
            this.NumberOfActiveRunners = NumberOfActiveRunners;
            this.EventId = EventId;
            this.CrossMatching = CrossMatching;
            this.RunnersVoidable = RunnersVoidable;
            this.TurnInPlayEnabled = TurnInPlayEnabled;
            this.SuspendTime = SuspendTime;
            this.DiscountAllowed = DiscountAllowed;
            this.PersistenceEnabled = PersistenceEnabled;
            this.Runners = Runners;
            this.Version = Version;
            this.EventTypeId = EventTypeId;
            this.Complete = Complete;
            this.OpenDate = OpenDate;
            this.MarketTime = MarketTime;
            this.BspReconciled = BspReconciled;
            this.Status = Status;
        }


        /// <summary>
        ///     Gets or Sets Venue
        /// </summary>
        [DataMember(Name = "venue", EmitDefaultValue = false)]
        public string Venue { get; set; }

        /// <summary>
        ///     Gets or Sets SettledTime
        /// </summary>
        [DataMember(Name = "settledTime", EmitDefaultValue = false)]
        public DateTime? SettledTime { get; set; }

        /// <summary>
        ///     Gets or Sets Timezone
        /// </summary>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }

        /// <summary>
        ///     Gets or Sets EachWayDivisor
        /// </summary>
        [DataMember(Name = "eachWayDivisor", EmitDefaultValue = false)]
        public double? EachWayDivisor { get; set; }

        /// <summary>
        ///     The market regulators.
        /// </summary>
        /// <value>The market regulators.</value>
        [DataMember(Name = "regulators", EmitDefaultValue = false)]
        public List<string> Regulators { get; set; }

        /// <summary>
        ///     Gets or Sets MarketType
        /// </summary>
        [DataMember(Name = "marketType", EmitDefaultValue = false)]
        public string MarketType { get; set; }

        /// <summary>
        ///     Gets or Sets MarketBaseRate
        /// </summary>
        [DataMember(Name = "marketBaseRate", EmitDefaultValue = false)]
        public double? MarketBaseRate { get; set; }

        /// <summary>
        ///     Gets or Sets NumberOfWinners
        /// </summary>
        [DataMember(Name = "numberOfWinners", EmitDefaultValue = false)]
        public int? NumberOfWinners { get; set; }

        /// <summary>
        ///     Gets or Sets CountryCode
        /// </summary>
        [DataMember(Name = "countryCode", EmitDefaultValue = false)]
        public string CountryCode { get; set; }

        /// <summary>
        ///     Gets or Sets InPlay
        /// </summary>
        [DataMember(Name = "inPlay", EmitDefaultValue = false)]
        public bool? InPlay { get; set; }

        /// <summary>
        ///     Gets or Sets BetDelay
        /// </summary>
        [DataMember(Name = "betDelay", EmitDefaultValue = false)]
        public int? BetDelay { get; set; }

        /// <summary>
        ///     Gets or Sets BspMarket
        /// </summary>
        [DataMember(Name = "bspMarket", EmitDefaultValue = false)]
        public bool? BspMarket { get; set; }

        /// <summary>
        ///     Gets or Sets NumberOfActiveRunners
        /// </summary>
        [DataMember(Name = "numberOfActiveRunners", EmitDefaultValue = false)]
        public int? NumberOfActiveRunners { get; set; }

        /// <summary>
        ///     Gets or Sets EventId
        /// </summary>
        [DataMember(Name = "eventId", EmitDefaultValue = false)]
        public string EventId { get; set; }

        /// <summary>
        ///     Gets or Sets CrossMatching
        /// </summary>
        [DataMember(Name = "crossMatching", EmitDefaultValue = false)]
        public bool? CrossMatching { get; set; }

        /// <summary>
        ///     Gets or Sets RunnersVoidable
        /// </summary>
        [DataMember(Name = "runnersVoidable", EmitDefaultValue = false)]
        public bool? RunnersVoidable { get; set; }

        /// <summary>
        ///     Gets or Sets TurnInPlayEnabled
        /// </summary>
        [DataMember(Name = "turnInPlayEnabled", EmitDefaultValue = false)]
        public bool? TurnInPlayEnabled { get; set; }

        /// <summary>
        ///     Gets or Sets SuspendTime
        /// </summary>
        [DataMember(Name = "suspendTime", EmitDefaultValue = false)]
        public DateTime? SuspendTime { get; set; }

        /// <summary>
        ///     Gets or Sets DiscountAllowed
        /// </summary>
        [DataMember(Name = "discountAllowed", EmitDefaultValue = false)]
        public bool? DiscountAllowed { get; set; }

        /// <summary>
        ///     Gets or Sets PersistenceEnabled
        /// </summary>
        [DataMember(Name = "persistenceEnabled", EmitDefaultValue = false)]
        public bool? PersistenceEnabled { get; set; }

        /// <summary>
        ///     Gets or Sets Runners
        /// </summary>
        [DataMember(Name = "runners", EmitDefaultValue = false)]
        public List<RunnerDefinition> Runners { get; set; }

        /// <summary>
        ///     Gets or Sets Version
        /// </summary>
        [DataMember(Name = "version", EmitDefaultValue = false)]
        public long? Version { get; set; }

        /// <summary>
        ///     The Event Type the market is contained within.
        /// </summary>
        /// <value>The Event Type the market is contained within.</value>
        [DataMember(Name = "eventTypeId", EmitDefaultValue = false)]
        public string EventTypeId { get; set; }

        /// <summary>
        ///     Gets or Sets Complete
        /// </summary>
        [DataMember(Name = "complete", EmitDefaultValue = false)]
        public bool? Complete { get; set; }

        /// <summary>
        ///     Gets or Sets OpenDate
        /// </summary>
        [DataMember(Name = "openDate", EmitDefaultValue = false)]
        public DateTime? OpenDate { get; set; }

        /// <summary>
        ///     Gets or Sets MarketTime
        /// </summary>
        [DataMember(Name = "marketTime", EmitDefaultValue = false)]
        public DateTime? MarketTime { get; set; }

        /// <summary>
        ///     Gets or Sets BspReconciled
        /// </summary>
        [DataMember(Name = "bspReconciled", EmitDefaultValue = false)]
        public bool? BspReconciled { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class MarketDefinition {\n");
            sb.Append("  Venue: ")
                .Append(Venue)
                .Append("\n");
            sb.Append("  SettledTime: ")
                .Append(SettledTime)
                .Append("\n");
            sb.Append("  Timezone: ")
                .Append(Timezone)
                .Append("\n");
            sb.Append("  EachWayDivisor: ")
                .Append(EachWayDivisor)
                .Append("\n");
            sb.Append("  Regulators: ")
                .Append(Regulators)
                .Append("\n");
            sb.Append("  MarketType: ")
                .Append(MarketType)
                .Append("\n");
            sb.Append("  MarketBaseRate: ")
                .Append(MarketBaseRate)
                .Append("\n");
            sb.Append("  NumberOfWinners: ")
                .Append(NumberOfWinners)
                .Append("\n");
            sb.Append("  CountryCode: ")
                .Append(CountryCode)
                .Append("\n");
            sb.Append("  InPlay: ")
                .Append(InPlay)
                .Append("\n");
            sb.Append("  BetDelay: ")
                .Append(BetDelay)
                .Append("\n");
            sb.Append("  BspMarket: ")
                .Append(BspMarket)
                .Append("\n");
            sb.Append("  BettingType: ")
                .Append(BettingType)
                .Append("\n");
            sb.Append("  NumberOfActiveRunners: ")
                .Append(NumberOfActiveRunners)
                .Append("\n");
            sb.Append("  EventId: ")
                .Append(EventId)
                .Append("\n");
            sb.Append("  CrossMatching: ")
                .Append(CrossMatching)
                .Append("\n");
            sb.Append("  RunnersVoidable: ")
                .Append(RunnersVoidable)
                .Append("\n");
            sb.Append("  TurnInPlayEnabled: ")
                .Append(TurnInPlayEnabled)
                .Append("\n");
            sb.Append("  SuspendTime: ")
                .Append(SuspendTime)
                .Append("\n");
            sb.Append("  DiscountAllowed: ")
                .Append(DiscountAllowed)
                .Append("\n");
            sb.Append("  PersistenceEnabled: ")
                .Append(PersistenceEnabled)
                .Append("\n");
            sb.Append("  Runners: ")
                .Append(Runners)
                .Append("\n");
            sb.Append("  Version: ")
                .Append(Version)
                .Append("\n");
            sb.Append("  EventTypeId: ")
                .Append(EventTypeId)
                .Append("\n");
            sb.Append("  Complete: ")
                .Append(Complete)
                .Append("\n");
            sb.Append("  OpenDate: ")
                .Append(OpenDate)
                .Append("\n");
            sb.Append("  MarketTime: ")
                .Append(MarketTime)
                .Append("\n");
            sb.Append("  BspReconciled: ")
                .Append(BspReconciled)
                .Append("\n");
            sb.Append("  Status: ")
                .Append(Status)
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
            return Equals(obj as MarketDefinition);
        }

        /// <summary>
        ///     Returns true if MarketDefinition instances are equal
        /// </summary>
        /// <param name="other">Instance of MarketDefinition to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MarketDefinition other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return (Venue == other.Venue || Venue != null && Venue.Equals(other.Venue)) &&
                   (SettledTime == other.SettledTime || SettledTime != null && SettledTime.Equals(other.SettledTime)) &&
                   (Timezone == other.Timezone || Timezone != null && Timezone.Equals(other.Timezone)) &&
                   (EachWayDivisor == other.EachWayDivisor || EachWayDivisor != null && EachWayDivisor.Equals(other.EachWayDivisor)) &&
                   (Regulators == other.Regulators || Regulators != null && Regulators.SequenceEqual(other.Regulators)) &&
                   (MarketType == other.MarketType || MarketType != null && MarketType.Equals(other.MarketType)) &&
                   (MarketBaseRate == other.MarketBaseRate || MarketBaseRate != null && MarketBaseRate.Equals(other.MarketBaseRate)) &&
                   (NumberOfWinners == other.NumberOfWinners || NumberOfWinners != null && NumberOfWinners.Equals(other.NumberOfWinners)) &&
                   (CountryCode == other.CountryCode || CountryCode != null && CountryCode.Equals(other.CountryCode)) &&
                   (InPlay == other.InPlay || InPlay != null && InPlay.Equals(other.InPlay)) &&
                   (BetDelay == other.BetDelay || BetDelay != null && BetDelay.Equals(other.BetDelay)) &&
                   (BspMarket == other.BspMarket || BspMarket != null && BspMarket.Equals(other.BspMarket)) &&
                   (BettingType == other.BettingType || BettingType != null && BettingType.Equals(other.BettingType)) &&
                   (NumberOfActiveRunners == other.NumberOfActiveRunners || NumberOfActiveRunners != null && NumberOfActiveRunners.Equals(other.NumberOfActiveRunners)) &&
                   (EventId == other.EventId || EventId != null && EventId.Equals(other.EventId)) &&
                   (CrossMatching == other.CrossMatching || CrossMatching != null && CrossMatching.Equals(other.CrossMatching)) &&
                   (RunnersVoidable == other.RunnersVoidable || RunnersVoidable != null && RunnersVoidable.Equals(other.RunnersVoidable)) &&
                   (TurnInPlayEnabled == other.TurnInPlayEnabled || TurnInPlayEnabled != null && TurnInPlayEnabled.Equals(other.TurnInPlayEnabled)) &&
                   (SuspendTime == other.SuspendTime || SuspendTime != null && SuspendTime.Equals(other.SuspendTime)) &&
                   (DiscountAllowed == other.DiscountAllowed || DiscountAllowed != null && DiscountAllowed.Equals(other.DiscountAllowed)) &&
                   (PersistenceEnabled == other.PersistenceEnabled || PersistenceEnabled != null && PersistenceEnabled.Equals(other.PersistenceEnabled)) &&
                   (Runners == other.Runners || Runners != null && Runners.SequenceEqual(other.Runners)) &&
                   (Version == other.Version || Version != null && Version.Equals(other.Version)) &&
                   (EventTypeId == other.EventTypeId || EventTypeId != null && EventTypeId.Equals(other.EventTypeId)) &&
                   (Complete == other.Complete || Complete != null && Complete.Equals(other.Complete)) &&
                   (OpenDate == other.OpenDate || OpenDate != null && OpenDate.Equals(other.OpenDate)) &&
                   (MarketTime == other.MarketTime || MarketTime != null && MarketTime.Equals(other.MarketTime)) &&
                   (BspReconciled == other.BspReconciled || BspReconciled != null && BspReconciled.Equals(other.BspReconciled)) &&
                   (Status == other.Status || Status != null && Status.Equals(other.Status));
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

                if (Venue != null)
                    hash = hash * 59 + Venue.GetHashCode();

                if (SettledTime != null)
                    hash = hash * 59 + SettledTime.GetHashCode();

                if (Timezone != null)
                    hash = hash * 59 + Timezone.GetHashCode();

                if (EachWayDivisor != null)
                    hash = hash * 59 + EachWayDivisor.GetHashCode();

                if (Regulators != null)
                    hash = hash * 59 + Regulators.GetHashCode();

                if (MarketType != null)
                    hash = hash * 59 + MarketType.GetHashCode();

                if (MarketBaseRate != null)
                    hash = hash * 59 + MarketBaseRate.GetHashCode();

                if (NumberOfWinners != null)
                    hash = hash * 59 + NumberOfWinners.GetHashCode();

                if (CountryCode != null)
                    hash = hash * 59 + CountryCode.GetHashCode();

                if (InPlay != null)
                    hash = hash * 59 + InPlay.GetHashCode();

                if (BetDelay != null)
                    hash = hash * 59 + BetDelay.GetHashCode();

                if (BspMarket != null)
                    hash = hash * 59 + BspMarket.GetHashCode();

                if (BettingType != null)
                    hash = hash * 59 + BettingType.GetHashCode();

                if (NumberOfActiveRunners != null)
                    hash = hash * 59 + NumberOfActiveRunners.GetHashCode();

                if (EventId != null)
                    hash = hash * 59 + EventId.GetHashCode();

                if (CrossMatching != null)
                    hash = hash * 59 + CrossMatching.GetHashCode();

                if (RunnersVoidable != null)
                    hash = hash * 59 + RunnersVoidable.GetHashCode();

                if (TurnInPlayEnabled != null)
                    hash = hash * 59 + TurnInPlayEnabled.GetHashCode();

                if (SuspendTime != null)
                    hash = hash * 59 + SuspendTime.GetHashCode();

                if (DiscountAllowed != null)
                    hash = hash * 59 + DiscountAllowed.GetHashCode();

                if (PersistenceEnabled != null)
                    hash = hash * 59 + PersistenceEnabled.GetHashCode();

                if (Runners != null)
                    hash = hash * 59 + Runners.GetHashCode();

                if (Version != null)
                    hash = hash * 59 + Version.GetHashCode();

                if (EventTypeId != null)
                    hash = hash * 59 + EventTypeId.GetHashCode();

                if (Complete != null)
                    hash = hash * 59 + Complete.GetHashCode();

                if (OpenDate != null)
                    hash = hash * 59 + OpenDate.GetHashCode();

                if (MarketTime != null)
                    hash = hash * 59 + MarketTime.GetHashCode();

                if (BspReconciled != null)
                    hash = hash * 59 + BspReconciled.GetHashCode();

                if (Status != null)
                    hash = hash * 59 + Status.GetHashCode();

                return hash;
            }
        }
    }
}
