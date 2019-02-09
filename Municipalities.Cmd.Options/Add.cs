using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Municipalities.Cmd.Options
{
    [Verb("add", HelpText = "Add new record of municipality taxes.")]
    public class Add
    {
        [Option('m', "municipality", HelpText = "Municipality name.", Required = true)]
        public string Municipality { get; set; }

        [Option('d', "date", HelpText = "Start Date time of municipality tax rate.", Required = true)]
        public string StartDate { get; set; }

        [Option('s', "scheduletype", HelpText = "Schedule Type of the tax rate (YEARLY, MONTHLY, WEEKLY, DAILY)", Required = true)]
        public string TaxType { get; set; }

        [Option('t', "tax", HelpText = "Tax rate of municipality.", Required = true)]
        public decimal TaxRate { get; set; }
    }
}
