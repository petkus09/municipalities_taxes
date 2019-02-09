using System;
using System.Collections.Generic;
using System.Text;

namespace Municipalities.Cmd.Models
{
    public abstract class CmdCommandExecuteArgumentException : Exception {
        public string ValidationMessage { get; }

        public CmdCommandExecuteArgumentException(string message)
        {
            ValidationMessage = message;
        }
    }

    public class MunicipalityEmptyException : CmdCommandExecuteArgumentException
    {
        public MunicipalityEmptyException(string message) : base(message){}
    }
    public class TexRateBelow0Exception : CmdCommandExecuteArgumentException
    {
        public TexRateBelow0Exception(string message) : base(message){}
    }
    public class TaxTypeEmptyException : CmdCommandExecuteArgumentException
    {
        public TaxTypeEmptyException(string message) : base(message) { }
    }
    public class TaxTypeNotFoundException : CmdCommandExecuteArgumentException
    {
        public TaxTypeNotFoundException(string message) : base(message) { }
    }
    public class TaxRateStartDateEmptyException : CmdCommandExecuteArgumentException
    {
        public TaxRateStartDateEmptyException(string message) : base(message) { }
    }
    public class TaxRateStartDateInvalidFormatException : CmdCommandExecuteArgumentException
    {
        public TaxRateStartDateInvalidFormatException(string message) : base(message) { }
    }
}
