using Municipalities.Cmd.Options;
using Municipalities.Cmd.Service.Contracts;
using Municipalities.Logging.Contracts;
using System;

namespace Municipalities.Cmd.Service
{
    public class AddCommand : IAddCommand
    {
        private readonly IProgramLogger _log;

        public AddCommand(IProgramLogger log)
        {
            _log = log;
        }

        public bool Execute(Add args)
        {
            _log.Info("Executing command 'Add'...");
            return true;
        }
    }
}
