using CsvHelper;
using EnsekTechnicalExercise.Api.Data;
using EnsekTechnicalExercise.Api.Repository;
using System.Globalization;

namespace EnsekTechnicalExercise.Api.Middleware
{
    public class MeterReadingProcessor : IMeterReadingProcessor
    {
        private readonly IMeterReadingRepository _meterReadingRepository;
        private readonly IAccountRepository _accountRepository;
        private const int expectedColumnCount = 3;

        public MeterReadingProcessor(IMeterReadingRepository meterReadingRepository, IAccountRepository accountRepository)
        {
            _meterReadingRepository = meterReadingRepository;
            _accountRepository = accountRepository;
        }

        public ProcessMeterReadingsResults processMeterReadings(IFormFile file)
        {
            var validReadings = new List<MeterReading>();
            var failedReadings = new List<string>();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    if (csv.Parser.Count != expectedColumnCount)
                    {
                        failedReadings.Add(csv.Parser.RawRecord);
                        continue;
                    }

                    var record = new MeterReading();
                    var accountId = csv.GetField<int>("AccountId");
                    if (_accountRepository.IsValidAccount(accountId))
                    {
                        record.AccountId = accountId;
                    }
                    else
                    {
                        failedReadings.Add(csv.Parser.RawRecord);
                        continue;
                    }

                    record.ReadDateTime = DateTime.Parse(csv.GetField<String>("MeterReadingDateTime"));
                    var MeterReadValue = csv.GetField("MeterReadValue");
                    if (int.TryParse(MeterReadValue, out var value) && value >= 0 && value < 100000)
                    {
                        record.Value = MeterReadValue;
                    }
                    else
                    {
                        failedReadings.Add(csv.Parser.RawRecord);
                        continue;
                    }

                    if (validReadings.Any(r => r.AccountId == record.AccountId))
                    {
                        failedReadings.Add(csv.Parser.RawRecord);
                        continue;
                    }

                    validReadings.Add(record);
                }
            }

            foreach (var reading in validReadings)
            {
                saveReading(reading);
            }

            return new ProcessMeterReadingsResults
            {
                SuccessfulReadings = validReadings.Count,
                FailedReadings = failedReadings.Count
            };
        }

        public void saveReading(MeterReading reading)
        {
            _meterReadingRepository.SaveReading(reading);
        }
    }

    public class ProcessMeterReadingsResults
    {
        public int SuccessfulReadings { get; set; }
        public int FailedReadings { get; set; }

    }
}
