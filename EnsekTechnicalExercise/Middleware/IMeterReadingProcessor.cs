using EnsekTechnicalExercise.Api.Data;

namespace EnsekTechnicalExercise.Api.Middleware
{
    public interface IMeterReadingProcessor
    {
        public ProcessMeterReadingsResults processMeterReadings(IFormFile file);
        public void saveReading(MeterReading reading);
    }
}
