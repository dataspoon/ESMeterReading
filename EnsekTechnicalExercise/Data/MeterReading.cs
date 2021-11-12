namespace EnsekTechnicalExercise.Api.Data
{
    public class MeterReading
    {
        public int MeterReadingId { get; set; }
        public string Value { get; set; }
        public DateTime ReadDateTime { get; set; }        

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}