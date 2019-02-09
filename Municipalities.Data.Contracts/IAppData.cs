using Municipalities.Cmd.Models;
using System;

namespace Municipalities.Data.Contracts
{
    public interface IAppData
    {
        void AddTaxRate(NewRecord entry);
    }
}
