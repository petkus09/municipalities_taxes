using Municipalities.Cmd.Models;
using Municipalities.Cmd.Options;
using System;

namespace Municipalities.Cmd.Service.Contracts
{
    public interface IAddCommand
    {
        bool Execute(Add args);
        event Action<NewRecord> NewRecordRequested;
    }
}
