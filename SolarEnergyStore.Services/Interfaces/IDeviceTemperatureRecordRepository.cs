using SolarEnergyStore.Models.DevicePeriodActive;
using SolarEnergyStore.Models.DeviceTemperatureRecord;

namespace SolarEnergyStore.Services.Interfaces;

public interface IDeviceTemperatureRecordRepository
{
    public Task<List<DeviceTemperatureRecordModel>> GetAllRecordsAsync();

    public Task AddTemperatureRecordAsync(DeviceTemperatureRecordModel record);

    public Task<DevicePeriodActiveModel> ValidateActivePeriod(DeviceTemperatureRecordModel record);
}
