using Municipalities.Cmd.Options;
using Municipalities.Cmd.Service.Contracts;
using Municipalities.Logging.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Municipalities.Cmd.Service
{
    public class ImportCommand : IImportCommand
    {
        private readonly IProgramLogger _log;

        public ImportCommand(IProgramLogger log)
        {
            _log = log;
        }

        public bool Execute(Import args)
        {
            _log.Info("Executing command 'Import'...");
            return true;
        }
    }
}
