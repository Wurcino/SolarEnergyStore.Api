using Microsoft.EntityFrameworkCore;
using SolarEnergyStore.Models.DevicePeriodActive;
using SolarEnergyStore.Models.DeviceTemperatureRecord;

namespace SolarEnergyStore.Repositories;

public class ApplicantDbContext : DbContext
{
    public ApplicantDbContext(DbContextOptions<ApplicantDbContext> options) : base(options) { }

    public DbSet<DeviceTemperatureRecordModel> TemperatureRecords { get; set; }
    public DbSet<DevicePeriodActiveModel> PeriodActive { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeviceTemperatureRecordModel>()
            .HasOne(tr => tr.DevicePeriodActive)
            .WithMany(pa => pa.TemperatureRecords)
            .HasForeignKey(tr => tr.DevicePeriodActiveId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
