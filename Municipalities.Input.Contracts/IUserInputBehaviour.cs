using System;
using System.Threading;
using System.Threading.Tasks;

namespace Municipalities.Input.Contracts
{
    public interface IUserInputBehaviour
    {
        void StartListening(CancellationToken cancelToken);
    }
}
