using CommandLine;
using Municipalities.Cmd.Options;
using Municipalities.Cmd.Service.Contracts;
using Municipalities.Input.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Municipalities.Input
{
    public class CommandMapper : ICommandMapper
    {
        private readonly IGetCommand _get;
        private readonly IAddCommand _add;
        private readonly IImportCommand _import;

        public CommandMapper(IGetCommand get, IAddCommand add, IImportCommand import)
        {
            _get = get;
            _add = add;
            _import = import;
        }

        public void ExecuteCommand(string[] args)
        {
            var arguments = Parser.Default.ParseArguments<
                Add,
                Get,
                Import>(args);
            arguments.MapResult(
                (Add arg) => _add.Execute(arg),
                (Get arg) => _get.Execute(arg),
                (Import arg) => _import.Execute(arg),
                errs => false
            );
        }
    }
}
