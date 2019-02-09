using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Municipalities.Requests.Contracts
{
    public interface IRequestsFacade
    {
        void ListenForRequests(CancellationToken cancelToken);
    }
}
