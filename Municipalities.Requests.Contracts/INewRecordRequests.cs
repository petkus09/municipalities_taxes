using Municipalities.Cmd.Models;
using System;
using System.Threading;

namespace Municipalities.Requests.Contracts
{
    public interface INewRecordRequests
    {
        void RunNewEntryAppender(CancellationToken cancelToken);
        void AppendRequest(NewRecord record);
    }
}
