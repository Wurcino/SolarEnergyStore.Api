using SolarEnergyStore.Models.DevicePeriodActive;

namespace SolarEnergyStore.Services.Interfaces;
public interface IDevicePeriodActiveRepository
{
    public Task AddActivePeriodAsync(DevicePeriodActiveModel period);

    public Task UpdateActivePeriodAsync(DevicePeriodActiveModel period);

    public Task<List<DevicePeriodActiveModel>> GetAllActivePeriodAsync();

    public Task<List<DevicePeriodActiveModel>> GetActivePeriodByIdAsync(int id);

}
