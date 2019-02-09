using System;

namespace Municipalities.Cmd.Models
{
    public class NewRecord
    {
        public string Municipality { get; set; }
        public DateTime StartDate { get; set; }
        public TaxScheduleType TaxType { get; set; }
        public decimal TaxRate { get; set; }
    }
}
