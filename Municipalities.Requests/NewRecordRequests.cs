using Municipalities.Cmd.Models;
using Municipalities.Data.Contracts;
using Municipalities.Requests.Contracts;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Municipalities.Requests
{
    public class NewRecordRequests : INewRecordRequests
    {
        private readonly BlockingCollection<NewRecord> _requests = new BlockingCollection<NewRecord>();
        private readonly IAppData _data;

        public NewRecordRequests(IAppData data)
        {
            _data = data;
        }

        public void AppendRequest(NewRecord record)
        {
            _requests.Add(record);
        }

        public void RunNewEntryAppender(CancellationToken cancelToken)
        {
            while (!_requests.IsCompleted)
            {
                var request = _requests.Take(cancelToken);
                _data.AddTaxRate(request);
            }
            _requests.Dispose();
        }
    }
}
