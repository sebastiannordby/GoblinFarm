using GoblinFarm.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoblinFarm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelldusController : Controller
    {
        private readonly TelldusSensorService _telldusSensorService;

        public TelldusController(TelldusSensorService telldusSensorService)
        {
            _telldusSensorService = telldusSensorService;
        }

        [HttpGet("Devices")]
        public object GetDevices()
        {
            return _telldusSensorService.GetSensorList();
        }
    }
}
