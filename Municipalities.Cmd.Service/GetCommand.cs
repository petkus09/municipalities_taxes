using Municipalities.Cmd.Options;
using Municipalities.Cmd.Service.Contracts;
using Municipalities.Logging.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Municipalities.Cmd.Service
{
    public class GetCommand : IGetCommand
    {
        private readonly IProgramLogger _log;

        public GetCommand(IProgramLogger log)
        {
            _log = log;
        }

        public bool Execute(Get args)
        {
            return true;
        }
    }
}
