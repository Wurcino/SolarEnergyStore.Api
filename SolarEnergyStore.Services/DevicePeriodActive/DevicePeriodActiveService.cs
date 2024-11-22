using SolarEnergyStore.Models.DevicePeriodActive;
using SolarEnergyStore.Services.Interfaces;

namespace SolarEnergyStore.Services.DevicePeriodActive;

public class DevicePeriodActiveService
{
    private readonly IDevicePeriodActiveRepository _repository;

    public DevicePeriodActiveService(IDevicePeriodActiveRepository repository)
    {
        _repository = repository;
    }

    public async Task AddActivePeriodAsync(DevicePeriodActiveData devicePeriodActive)
    {
        var devicePeriodActiveModel = new DevicePeriodActiveModel()
        {
            DeviceId = devicePeriodActive.DeviceId,
            PeriodEnd = devicePeriodActive.PeriodEnd,
            PeriodStart = devicePeriodActive.PeriodStart,
        };

        await _repository.AddActivePeriodAsync(devicePeriodActiveModel);
    }

    public async Task UpdateActivePeriodAsync(UpdateDevicePeriodActiveData devicePeriodActive)
    {
        var devicePeriodActiveModel = new DevicePeriodActiveModel()
        {
            DeviceId = devicePeriodActive.DeviceId,
            PeriodEnd = devicePeriodActive.PeriodEnd,
            PeriodStart = devicePeriodActive.PeriodStart,
        };

        await _repository.UpdateActivePeriodAsync(devicePeriodActiveModel);
    }

    public async Task<List<DevicePeriodActiveModel>> GetActivePeriodByIdAsync(int id)
    {
        return await _repository.GetActivePeriodByIdAsync(id);
    }
}
