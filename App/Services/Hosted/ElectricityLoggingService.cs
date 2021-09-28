using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoblinFarm.EF;
using GoblinFarm.Services;
using Timer = System.Threading.Timer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App.Services.Hosted
{
    public class ElectricityLoggingService : IHostedService, IDisposable
    {
        private static readonly string POSTAL_CODE = "1923";
        private static readonly string WATT_UNIT = "W";
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public ElectricityLoggingService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        private void SaveData(object state)
        {
            using(var scope = _scopeFactory.CreateScope())
            {
                var telldusSensorService = scope.ServiceProvider.GetRequiredService<ITelldusSensorService>();
                var fjordkraftService = scope.ServiceProvider.GetRequiredService<IFjordkraftService>();
                var dbContext = scope.ServiceProvider.GetRequiredService<GoblinFarmContext>();

                var client = telldusSensorService.GetSensorList();
                var electricityPrice_KwH_Ore = fjordkraftService.GetElectricityPrice(POSTAL_CODE).Price;
                var wattData = client?.Sensor?.Select(x => x.Data.FirstOrDefault(x => x.Unit.Equals(WATT_UNIT)));
                var wattValueAccumulated = wattData.Sum(x => x.Value);

                dbContext.TelldusAccumulatedSensorLogs.Add(new TelldusAccumulatedSensorLog()
                {
                    DataUnit = WATT_UNIT,
                    DataValue = wattValueAccumulated,
                    ElectricityCost_KwH_Ore = electricityPrice_KwH_Ore
                });

                dbContext.SaveChanges();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(SaveData, null, TimeSpan.FromMinutes(2), TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
