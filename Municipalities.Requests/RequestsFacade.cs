using Municipalities.Cmd.Models;
using Municipalities.Cmd.Service.Contracts;
using Municipalities.Requests.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Municipalities.Requests
{
    public class RequestsFacade : IRequestsFacade
    {
        private readonly INewRecordRequests _newRecord;

        public RequestsFacade(INewRecordRequests newRecord, IAddCommand newRecordCommand)
        {
            _newRecord = newRecord;
            newRecordCommand.NewRecordRequested += _newRecord.AppendRequest;
        }

        public void ListenForRequests(CancellationToken cancelToken)
        {
            var newRecordThread = new Thread(() => _newRecord.RunNewEntryAppender(cancelToken))
            {
                IsBackground = true
            };
            newRecordThread.Start();

            WaitHandle.WaitAny(new[] { cancelToken.WaitHandle });
        }
    }
}
