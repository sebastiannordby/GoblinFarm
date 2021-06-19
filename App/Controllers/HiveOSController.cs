using App.Classes;
using App.Services;
using GoblinFarm.Models;
using GoblinFarm.Models.HiveOS;
using GoblinFarm.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace GoblinFarm.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HiveOSController : Controller
    {
        private IHiveOSService _hiveOSService;
        private IHiveOnService _hiveOnService;
        private ICoinbaseService _coinbaseService;

        public HiveOSController(IHiveOSService hiveOSService,
            ICoinbaseService coinbaseService,
            IHiveOnService hiveOnService)
        {
            _hiveOSService = hiveOSService;
            _coinbaseService = coinbaseService;
            _hiveOnService = hiveOnService;
        }

        [HttpGet("Summary")]
        public ActionResult Summary()
        {
            var farms = _hiveOSService.GetFarms();
            var workers = new List<HiveOSWorker>();
            var wallets = new List<HiveOSWallet>();

            farms.ForEach(x => {
                workers.AddRange(_hiveOSService.GetWorkers(x.Id));
                wallets.AddRange(_hiveOSService.GetWallets(x.Id));
            });

            return Ok(new { 
                workers = workers,
                wallets = wallets 
            });
        }

    }
}
