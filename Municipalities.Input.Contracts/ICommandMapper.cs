using System;
using System.Collections.Generic;
using System.Text;

namespace Municipalities.Input.Contracts
{
    public interface ICommandMapper
    {
        void ExecuteCommand(string[] args);
    }
}
