using CommandLine;

namespace Municipalities.Cmd.Options
{
    [Verb("import", HelpText = "Import Municipalities taxes data file.")]
    public class Import
    {
        [Option('f', "file", HelpText = "File path of .json file.", Required = true)]
        public string File { get; set; }
    }
}
