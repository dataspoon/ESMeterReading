using EnsekTechnicalExercise.Api.Data;

namespace EnsekTechnicalExercise.Api.Repository
{
    public interface IMeterReadingRepository
    {
        void SaveReading(MeterReading reading);
    }
}
