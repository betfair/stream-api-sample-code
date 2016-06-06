using Betfair.ESASwagger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Betfair.ESAClient.Protocol
{
    /// <summary>
    /// Wraps a standard completion source to create a pairing of request message to status message
    /// </summary>
    public class RequestResponse
    {
        private readonly TaskCompletionSource<StatusMessage> _completionSource = new TaskCompletionSource<StatusMessage>();
        public readonly RequestMessage Request;
        public Action<RequestResponse> OnSuccess { get; set; }

        public RequestResponse(int id, RequestMessage request, Action<RequestResponse> onSuccess)
        {
            Id = id;
            Request = request;
            OnSuccess = onSuccess;
        }

        public void ProcesStatusMessage(StatusMessage statusMessage)
        {
            if(statusMessage.StatusCode == StatusMessage.StatusCodeEnum.Success)
            {
                if(OnSuccess != null) OnSuccess(this);
            }
            _completionSource.TrySetResult(statusMessage);
        }

        public StatusMessage Result
        {
            get
            {
                return _completionSource.Task.Result;
            }
        }

        public int Id { get; private set; }

        public Task<StatusMessage> Task
        {
            get
            {
                return _completionSource.Task;
            }
        }

        internal void Cancelled()
        {
            _completionSource.TrySetCanceled();
        }
    }
}
