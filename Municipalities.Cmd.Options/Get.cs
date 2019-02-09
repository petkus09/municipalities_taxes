using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Municipalities.Cmd.Options
{
    [Verb("get", HelpText = "Get record of municipality tax rate on the requested date.")]
    public class Get
    {
        [Option('m', "municipality", HelpText = "Municipality name.", Required = true)]
        public string Municipality { get; set; }

        [Option('d', "date", HelpText = "Date time of municipality tax rate.", Required = true)]
        public string Date { get; set; }
       
    }
}
