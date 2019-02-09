using Municipalities.Cmd.Models;
using Municipalities.Data.Contracts;
using Municipalities.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Municipalities.Data
{
    public class AppData : IAppData
    {
        private MunicipalitiesTaxRates TaxRates { get; set; }
        private readonly DirectoryInfo _applicationDataFolder;
        private readonly FileInfo _dataFile;
        private readonly object _fileLock = new object();

        public AppData()
        {
            _applicationDataFolder = new DirectoryInfo(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Municipalities"
                )
            );
            _dataFile = new FileInfo(Path.Combine(_applicationDataFolder.FullName, "data.json"));
            LoadDataFromFile();
        }

        public void AddTaxRate(NewRecord entry)
        {
            if (!TaxRates.MunicipalitiesAndTheirRates.ContainsKey(entry.Municipality))
            {
                TaxRates.MunicipalitiesAndTheirRates[entry.Municipality] = new List<MunicipalityTaxRate>();
            }
            TaxRates.MunicipalitiesAndTheirRates[entry.Municipality].Add(new MunicipalityTaxRate() { ScheduleType = entry.TaxType, StartDate = entry.StartDate, TaxRate = entry.TaxRate});
            SaveDataToFile();
        }

        private void LoadDataFromFile()
        {
            lock (_fileLock)
            {
                if (!_dataFile.Exists)
                {
                    _dataFile.Create().Dispose();
                }
                var dataAsJsonString = File.ReadAllText(_dataFile.FullName);
                TaxRates = JsonConvert.DeserializeObject<MunicipalitiesTaxRates>(dataAsJsonString);
                if (TaxRates == null)
                {
                    TaxRates = new MunicipalitiesTaxRates();
                }
                if (TaxRates.MunicipalitiesAndTheirRates == null)
                {
                    TaxRates.MunicipalitiesAndTheirRates = new Dictionary<string, List<MunicipalityTaxRate>>();
                }
            }
        }

        private void SaveDataToFile()
        {
            lock (_fileLock)
            {
                if (!_dataFile.Exists)
                {
                    _dataFile.Create().Dispose();
                }
                var dataAsJsonString = JsonConvert.SerializeObject(TaxRates);
                File.WriteAllText(_dataFile.FullName, dataAsJsonString);
            }
        }
    }
}
