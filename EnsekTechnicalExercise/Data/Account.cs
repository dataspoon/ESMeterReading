namespace EnsekTechnicalExercise.Api.Data
{
    public class Account
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<MeterReading> MeterReading { get; } = new List<MeterReading>();
    }
}