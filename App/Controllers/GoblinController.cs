using App.Classes;
using App.Models.Coinbase;
using App.Services;
using Coinbase;
using Coinbase.Models;
using GoblinFarm.Models;
using GoblinFarm.Models.HiveOS;
using GoblinFarm.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static App.Controllers.GoblinSummary;
using static App.Controllers.GoblinWorkerSummary;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoblinController : Controller
    {
        private IHiveOSService _hiveOSService;
        private IHiveOnService _hiveOnService;
        private ICoinbaseService _coinbaseService;
        private ITelldusSensorService _telldusSensorService;
        private IFjordkraftService _fjordkraftService;

        public GoblinController(IHiveOSService hiveOSService,
            ICoinbaseService coinbaseService,
            IHiveOnService hiveOnService,
            ITelldusSensorService telldusSensorService,
            IFjordkraftService fjordkraftService)
        {
            _hiveOSService = hiveOSService;
            _coinbaseService = coinbaseService;
            _hiveOnService = hiveOnService;
            _telldusSensorService = telldusSensorService;
            _fjordkraftService = fjordkraftService;
        }

        private async Task<List<Account>> GetAccounts()
        {
            var client = new CoinbaseClient(new ApiKeyConfig
            {
                ApiKey = SystemVariables.CoinbaseSettings.ApiKey,
                ApiSecret = SystemVariables.CoinbaseSettings.ApiSecret
            });

            return (await client.Accounts.ListAccountsAsync())?.Data?.ToList();
        }


        [HttpGet("ElectricityCost")]
        public ActionResult ElectricityCost()
        {
            var sensorList = _telldusSensorService.GetSensorList();
            var accumulatedWattageUse = 0m;

            sensorList.Sensor.ForEach(sensor => {
                var sensorData = sensor.Data.FirstOrDefault(x => x.Unit == "W");

                if(sensorData != null)
                {
                    accumulatedWattageUse += sensorData.Value;
                }
            });

            var kiloWattPerHour = accumulatedWattageUse / 1000;
            var electricityCost = _fjordkraftService.GetElectricityPrice("1923");
            var totalElectricityCostOre = electricityCost.Price * kiloWattPerHour;
            var totalElectricityCostKrone = totalElectricityCostOre / 100;

            return Ok(new {
                accumulatedWattageUse = decimal.Round(accumulatedWattageUse, 2),
                totalElectricityCostOre = decimal.Round(totalElectricityCostOre, 2),
                totalElectricityCostKrone = decimal.Round(totalElectricityCostKrone, 2)
            });
        }

        private List<GoblinWorkerSummary> GetWorkerSummaries(Dictionary<string, CoinbaseExchangeRate> exchangeRates)
        {
            var farms = _hiveOSService.GetFarms();
            var workers = new List<HiveOSWorker>();
            var wallets = new List<HiveOSWallet>();
            var workerSummaries = new List<GoblinWorkerSummary>();

            farms.ForEach(x => {
                workers.AddRange(_hiveOSService.GetWorkers(x.Id));
                wallets.AddRange(_hiveOSService.GetWallets(x.Id));
            });

            workers.ForEach(x => {
                var itemsCount = x.FlightSheet.Items.Count();
                decimal expectedCurrencyAccumulated = 0m;
                var summaryResult = new GoblinWorkerSummary()
                {
                    WorkerName = x.Name
                };

                var walletSummaries = new List<GoblinWalletSummary>();

                x.FlightSheet?.Items.ForEach(flightSheetItem => {
                    if (!walletSummaries.Any(summary => summary.WalletId == flightSheetItem.WalletId))
                    {
                        var wallet = _hiveOSService.GetWallet(flightSheetItem.WalletId);
                        var earnings = _hiveOnService.GetEarnings(wallet.Wal, flightSheetItem.Coin);
                        var nokExchangeRate = exchangeRates[flightSheetItem.Coin].Rates["NOK"];
                        var expected24HNok = earnings.ExpectedReward24H * nokExchangeRate;

                        expectedCurrencyAccumulated += expected24HNok;

                        walletSummaries.Add(new GoblinWalletSummary()
                        {
                            WalletName = wallet.Name,
                            WalletAddress = wallet.Wal,
                            WalletId = wallet.Id,
                            Coin = wallet.Coin,
                            NokExchangeRate = nokExchangeRate,
                            Estimated24HNOK = decimal.Round(expected24HNok, 2)
                        }); ;
                    }
                });

                summaryResult.WalletSummaries = walletSummaries;
                summaryResult.Estimated24HNOK = decimal.Round(expectedCurrencyAccumulated, 2);

                workerSummaries.Add(summaryResult);
            });

            return workerSummaries;
        }

        private Dictionary<string, CoinbaseExchangeRate> FetchExchangeRates()
        {
            var exchangeRates = new Dictionary<string, CoinbaseExchangeRate>();
            SystemVariables.SupportedCoins.ForEach(x => {
                exchangeRates.Add(x, _coinbaseService.ExchangeRate(x));
            });

            return exchangeRates;
        }

        private async Task<List<GoblinMoney>> GetCoinbaseWalletSummaries(Dictionary<string, CoinbaseExchangeRate> exchangeRates)
        {
            var allCoinbaseAccounts = await GetAccounts();
            var coinbaseAccounts = allCoinbaseAccounts.Where(x =>
                SystemVariables.SupportedCoins.Contains(x.Currency.Code));

            return coinbaseAccounts.Select(x => {
                return new GoblinMoney(x.Balance, exchangeRates[x.Currency.Code].Rates["NOK"]);
            })?.ToList();
        }

        [HttpGet("Summary")]
        public async Task<ActionResult> Summary()
        {
            var exchangeRates = FetchExchangeRates();

            return Ok(new GoblinSummary() { 
                WorkerSummaries = GetWorkerSummaries(exchangeRates),
                MoneySummaries = await GetCoinbaseWalletSummaries(exchangeRates)
            });
        }
    }

    public class GoblinSummary
    {
        public List<GoblinMoney> MoneySummaries { get; set; } = new List<GoblinMoney>();
        public List<GoblinWorkerSummary> WorkerSummaries { get; set; } = new List<GoblinWorkerSummary>();
    }

    public class GoblinWorkerSummary
    {
        public string WorkerName { get; set; }
        public decimal Estimated24HNOK { get; set; }
        public List<GoblinWalletSummary> WalletSummaries { get; set; }

        public class GoblinWalletSummary
        {
            public string WalletName { get; set; }
            public int WalletId { get; set; }
            public string WalletAddress { get; set; }
            public string Coin { get; set; }
            public decimal NokExchangeRate { get; set; }
            public decimal Estimated24HNOK { get; set; }
            public GoblinMoney Money { get; set; }
        }
    }

    public class GoblinElectricitySummary
    {

    }

    public class GoblinMoney : Money
    {
        public GoblinMoney(Money money, decimal nokExchangeRate)
        {
            this.Base = money.Base;
            this.Amount = decimal.Round(money.Amount, 4);
            this.Currency = money.Currency;
            this.AmountInNOK = decimal.Round(Amount * nokExchangeRate, 2);
        }

        public decimal AmountInNOK { get; set; }
    }
}
