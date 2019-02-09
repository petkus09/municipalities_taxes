using Municipalities.Cmd.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Municipalities.Cmd.Service.Contracts
{
    public interface IImportCommand
    {
        bool Execute(Import args);
    }
}
