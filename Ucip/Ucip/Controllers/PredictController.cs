using Microsoft.AspNetCore.Mvc;
using Ucip.Services.Interfaces;

namespace Ucip.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        private readonly ILogger<PredictController> _logger;
        private readonly IPredictService _predictService;

        public PredictController(ILogger<PredictController> logger, IPredictService predictService)
        {
            _logger = logger;
            _predictService = predictService;
        }

        [HttpGet]
        public IActionResult GetParams()
        {
            var dict = new Dictionary<int, string>
            {
                { 1, "Menor a 1 año" },
                { 2, "De 1 año a 3 años" },
                { 3, "De 4 años a 5 años" },
                { 4, "De 6 años a 12 años" }
            };

            return Ok(dict);
        }

        [HttpPost]
        public async Task<IActionResult> GetPrediction([FromQuery] string group, IFormFile file)
        {
            var groupInt = Convert.ToInt32(group);

            try
            {
                var result = await _predictService.GetPrediction(groupInt, file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(message: ex.Message, exception: ex, args: ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
    }
}
