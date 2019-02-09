using Municipalities.Cmd.Models;
using System;
using System.Collections.Generic;

namespace Municipalities.Data.Models
{
    public class MunicipalitiesTaxRates
    {
        public Dictionary<string, List<MunicipalityTaxRate>> MunicipalitiesAndTheirRates { get; set; }
    }

    public class MunicipalityTaxRate
    {
        public TaxScheduleType ScheduleType { get; set; }
        public decimal TaxRate { get; set; }
        public DateTime StartDate { get; set; }
    }
}
