using EnsekTechnicalExercise.Api.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace EnsekTechnicalExercise.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeterReadingController : ControllerBase
    {
        private readonly IMeterReadingProcessor _meterReadingProcessor;

        public MeterReadingController(IMeterReadingProcessor meterReadingProcessor)
        {
            _meterReadingProcessor = meterReadingProcessor;
        }

        [HttpPost("/meter-reading-uploads")]
        public async Task<IActionResult> MeterReadingUpload(IFormFile file)
        {
            try
            {
                var result = _meterReadingProcessor.processMeterReadings(file);
                
                return Ok(new { successful = result.SuccessfulReadings, failed = result.FailedReadings });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}