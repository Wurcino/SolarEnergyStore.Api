using Microsoft.EntityFrameworkCore;
using SolarEnergyStore.Models.DevicePeriodActive;
using SolarEnergyStore.Models.DeviceTemperatureRecord;
using SolarEnergyStore.Services.Interfaces;

namespace SolarEnergyStore.Repositories.DeviceTemperatureRecord;

public class DeviceTemperatureRecordRepository : IDeviceTemperatureRecordRepository
{
    private readonly ApplicantDbContext _context;

    public DeviceTemperatureRecordRepository(ApplicantDbContext context)
    {
        _context = context;
    }

    public async Task AddTemperatureRecordAsync(DeviceTemperatureRecordModel record)
    {


        _context.TemperatureRecords.Add(record);
        await _context.SaveChangesAsync();
    }

    public async Task<DevicePeriodActiveModel> ValidateActivePeriod(DeviceTemperatureRecordModel record)
    {
        var activePeriod = await _context.PeriodActive
            .Where(p => p.DeviceId == record.DeviceId
                && p.PeriodStart <= record.MeasurementTime
                && (p.PeriodEnd == null || p.PeriodEnd >= record.MeasurementTime)
            )
            .FirstOrDefaultAsync();

        return activePeriod;
    }

    public async Task<List<DeviceTemperatureRecordModel>> GetAllRecordsAsync()
    {
        return await _context.TemperatureRecords.Include(x => x.DevicePeriodActive).ToListAsync();
    }
}
