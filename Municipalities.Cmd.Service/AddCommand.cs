using Municipalities.Cmd.Models;
using Municipalities.Cmd.Options;
using Municipalities.Cmd.Service.Contracts;
using Municipalities.Logging.Contracts;
using System;

namespace Municipalities.Cmd.Service
{
    public class AddCommand : IAddCommand
    {
        private readonly IProgramLogger _log;

        public AddCommand(IProgramLogger log)
        {
            _log = log;
        }

        public event Action<NewRecord> NewRecordRequested;

        public bool Execute(Add args)
        {
            var res = new NewRecord();

            if (string.IsNullOrEmpty(args.Municipality) || string.IsNullOrEmpty(args.Municipality.Trim()))
                throw new MunicipalityEmptyException("Municipality name cannot be empty!");
            res.Municipality = args.Municipality;

            if (args.TaxRate < 0)
                throw new TexRateBelow0Exception("Tax rate cannot be below 0!");
            res.TaxRate = args.TaxRate;

            if (string.IsNullOrEmpty(args.TaxType) || string.IsNullOrEmpty(args.TaxType.Trim()))
                throw new TaxTypeEmptyException("Tax type cannot be empty!");
            switch (args.TaxType.ToLower())
            {
                case "yearly":
                    res.TaxType = TaxScheduleType.Yearly;
                    break;
                case "monthly":
                    res.TaxType = TaxScheduleType.Monthly;
                    break;
                case "weekly":
                    res.TaxType = TaxScheduleType.Weekly;
                    break;
                case "daily":
                    res.TaxType = TaxScheduleType.Daily;
                    break;
                default:
                    throw new TaxTypeNotFoundException("Tax type is not supported. Please check tax type again!");
            }

            if (string.IsNullOrEmpty(args.StartDate) || string.IsNullOrEmpty(args.StartDate.Trim()))
                throw new TaxRateStartDateEmptyException("Tax rate date cannot be empty!");

            try
            {
                res.StartDate = Convert.ToDateTime(args.StartDate);
            }
            catch(FormatException)
            {
                throw new TaxRateStartDateInvalidFormatException("Tax rate date was not recognised as valid date format");
            }

            NewRecordRequested?.Invoke(res);

            return true;
        }
    }
}
