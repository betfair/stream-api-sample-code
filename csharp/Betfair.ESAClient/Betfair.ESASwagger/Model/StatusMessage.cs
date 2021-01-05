using System;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Betfair.ESASwagger.Model {
    /// <summary>
    /// </summary>
    [DataContract]
    public class StatusMessage : ResponseMessage, IEquatable<StatusMessage> {
        /// <summary>
        ///     The type of error in case of a failure
        /// </summary>
        /// <value>The type of error in case of a failure</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ErrorCodeEnum {
            [EnumMember(Value = "NO_APP_KEY")]
            NoAppKey,

            [EnumMember(Value = "INVALID_APP_KEY")]
            InvalidAppKey,

            [EnumMember(Value = "NO_SESSION")]
            NoSession,

            [EnumMember(Value = "INVALID_SESSION_INFORMATION")]
            InvalidSessionInformation,

            [EnumMember(Value = "NOT_AUTHORIZED")]
            NotAuthorized,

            [EnumMember(Value = "INVALID_INPUT")]
            InvalidInput,

            [EnumMember(Value = "INVALID_CLOCK")]
            InvalidClock,

            [EnumMember(Value = "UNEXPECTED_ERROR")]
            UnexpectedError,

            [EnumMember(Value = "TIMEOUT")]
            Timeout,

            [EnumMember(Value = "SUBSCRIPTION_LIMIT_EXCEEDED")]
            SubscriptionLimitExceeded,

            [EnumMember(Value = "INVALID_REQUEST")]
            InvalidRequest,

            [EnumMember(Value = "CONNECTION_FAILED")]
            ConnectionFailed,

            [EnumMember(Value = "MAX_CONNECTION_LIMIT_EXCEEDED")]
            MaxConnectionLimitExceeded
        }


        /// <summary>
        ///     The status of the last request
        /// </summary>
        /// <value>The status of the last request</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusCodeEnum {
            [EnumMember(Value = "SUCCESS")]
            Success,

            [EnumMember(Value = "FAILURE")]
            Failure
        }


        /// <summary>
        ///     The type of error in case of a failure
        /// </summary>
        /// <value>The type of error in case of a failure</value>
        [DataMember(Name = "errorCode", EmitDefaultValue = false)]
        public ErrorCodeEnum? ErrorCode { get; set; }

        /// <summary>
        ///     The status of the last request
        /// </summary>
        /// <value>The status of the last request</value>
        [DataMember(Name = "statusCode", EmitDefaultValue = false)]
        public StatusCodeEnum? StatusCode { get; set; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StatusMessage" /> class.
        ///     Initializes a new instance of the <see cref="StatusMessage" />class.
        /// </summary>
        /// <param name="Op">The operation type.</param>
        /// <param name="Id">Client generated unique id to link request with response (like json rpc).</param>
        /// <param name="ErrorMessage">Additional message in case of a failure.</param>
        /// <param name="ErrorCode">The type of error in case of a failure.</param>
        /// <param name="ConnectionId">The connection id.</param>
        /// <param name="ConnectionClosed">Is the connection now closed.</param>
        /// <param name="StatusCode">The status of the last request.</param>
        public StatusMessage(
            string Op = null,
            int? Id = null,
            string ErrorMessage = null,
            ErrorCodeEnum? ErrorCode = null,
            string ConnectionId = null,
            bool? ConnectionClosed = null,
            StatusCodeEnum? StatusCode = null) {
            this.Op = Op;
            this.Id = Id;
            this.ErrorMessage = ErrorMessage;
            this.ErrorCode = ErrorCode;
            this.ConnectionId = ConnectionId;
            this.ConnectionClosed = ConnectionClosed;
            this.StatusCode = StatusCode;
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
        ///     Additional message in case of a failure
        /// </summary>
        /// <value>Additional message in case of a failure</value>
        [DataMember(Name = "errorMessage", EmitDefaultValue = false)]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     The connection id
        /// </summary>
        /// <value>The connection id</value>
        [DataMember(Name = "connectionId", EmitDefaultValue = false)]
        public string ConnectionId { get; set; }

        /// <summary>
        ///     Is the connection now closed
        /// </summary>
        /// <value>Is the connection now closed</value>
        [DataMember(Name = "connectionClosed", EmitDefaultValue = false)]
        public bool? ConnectionClosed { get; set; }

        /// <summary>
        ///     Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString() {
            var sb = new StringBuilder();
            sb.Append("class StatusMessage {\n");
            sb.Append("  Op: ")
                .Append(Op)
                .Append("\n");
            sb.Append("  Id: ")
                .Append(Id)
                .Append("\n");
            sb.Append("  ErrorMessage: ")
                .Append(ErrorMessage)
                .Append("\n");
            sb.Append("  ErrorCode: ")
                .Append(ErrorCode)
                .Append("\n");
            sb.Append("  ConnectionId: ")
                .Append(ConnectionId)
                .Append("\n");
            sb.Append("  ConnectionClosed: ")
                .Append(ConnectionClosed)
                .Append("\n");
            sb.Append("  StatusCode: ")
                .Append(StatusCode)
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
            return Equals(obj as StatusMessage);
        }

        /// <summary>
        ///     Returns true if StatusMessage instances are equal
        /// </summary>
        /// <param name="other">Instance of StatusMessage to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StatusMessage other) {
            // credit: http://stackoverflow.com/a/10454552/677735
            if (other == null)
                return false;

            return (Op == other.Op || Op != null && Op.Equals(other.Op)) &&
                   (Id == other.Id || Id != null && Id.Equals(other.Id)) &&
                   (ErrorMessage == other.ErrorMessage || ErrorMessage != null && ErrorMessage.Equals(other.ErrorMessage)) &&
                   (ErrorCode == other.ErrorCode || ErrorCode != null && ErrorCode.Equals(other.ErrorCode)) &&
                   (ConnectionId == other.ConnectionId || ConnectionId != null && ConnectionId.Equals(other.ConnectionId)) &&
                   (ConnectionClosed == other.ConnectionClosed || ConnectionClosed != null && ConnectionClosed.Equals(other.ConnectionClosed)) &&
                   (StatusCode == other.StatusCode || StatusCode != null && StatusCode.Equals(other.StatusCode));
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

                if (ErrorMessage != null)
                    hash = hash * 59 + ErrorMessage.GetHashCode();

                if (ErrorCode != null)
                    hash = hash * 59 + ErrorCode.GetHashCode();

                if (ConnectionId != null)
                    hash = hash * 59 + ConnectionId.GetHashCode();

                if (ConnectionClosed != null)
                    hash = hash * 59 + ConnectionClosed.GetHashCode();

                if (StatusCode != null)
                    hash = hash * 59 + StatusCode.GetHashCode();

                return hash;
            }
        }
    }
}
