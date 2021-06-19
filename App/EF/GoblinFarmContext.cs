using Microsoft.EntityFrameworkCore;

namespace GoblinFarm.EF
{
    public class GoblinFarmContext : DbContext
    {
        public DbSet<TelldusAccumulatedSensorLog> TelldusAccumulatedSensorLogs { get; set; }

        public GoblinFarmContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}
