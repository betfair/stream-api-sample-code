using Betfair.ESASwagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Protocol
{
    public class StatusException : Exception
    {
        public readonly StatusMessage.ErrorCodeEnum ErrorCode;
        public readonly string ErrorMessage;

        public StatusException(StatusMessage message) : base(message.ErrorCode +": " +message.ErrorMessage)
        {
            ErrorCode = (StatusMessage.ErrorCodeEnum)message.ErrorCode;
            ErrorMessage = message.ErrorMessage;
        }
    }
}
