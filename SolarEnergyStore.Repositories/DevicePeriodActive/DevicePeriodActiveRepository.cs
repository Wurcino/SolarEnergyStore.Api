using Microsoft.EntityFrameworkCore;
using SolarEnergyStore.Models.DevicePeriodActive;
using SolarEnergyStore.Services.Interfaces;

namespace SolarEnergyStore.Repositories.DevicePeriodActive;
public class DevicePeriodActiveRepository : IDevicePeriodActiveRepository
{
    private readonly ApplicantDbContext _context;

    public DevicePeriodActiveRepository(ApplicantDbContext context)
    {
        _context = context;
    }

    public async Task AddActivePeriodAsync(DevicePeriodActiveModel period)
    {
        _context.PeriodActive.Add(period);
        await _context.SaveChangesAsync();
    }

    public async Task<List<DevicePeriodActiveModel>> GetActivePeriodByIdAsync(int id)
    {
        return await _context.PeriodActive.Include(x => x.TemperatureRecords).Where(x => x.DeviceId.Equals(id)).ToListAsync();
    }

    public async Task<List<DevicePeriodActiveModel>> GetAllActivePeriodAsync()
    {
        return await _context.PeriodActive.ToListAsync();
    }

    public async Task UpdateActivePeriodAsync(DevicePeriodActiveModel period)
    {
        _context.PeriodActive.Update(period);
        await _context.SaveChangesAsync();
    }
}
