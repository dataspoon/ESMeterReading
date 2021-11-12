using EnsekTechnicalExercise.Api.Data;

namespace EnsekTechnicalExercise.Api.Repository
{
    public class MeterReadingRepository : IMeterReadingRepository
    {
        private readonly MeterContext _context;
        public MeterReadingRepository(MeterContext meterContext)
        {
            _context = meterContext;
        }

        public void SaveReading(MeterReading reading)
        {
            _context.MeterReadings.Add(reading);
            _context.SaveChanges();
        }
    }
}
