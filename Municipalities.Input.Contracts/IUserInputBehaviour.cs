using System;
using System.Threading.Tasks;

namespace Municipalities.Input.Contracts
{
    public interface IUserInputBehaviour
    {
        event Action RequestSoftwareExit;
        void StartListening();
    }
}
