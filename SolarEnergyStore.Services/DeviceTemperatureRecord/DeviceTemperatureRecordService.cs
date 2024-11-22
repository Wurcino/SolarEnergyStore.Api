using SolarEnergyStore.Models.DeviceTemperatureRecord;
using SolarEnergyStore.Services.Interfaces;

namespace SolarEnergyStore.Services.DeviceTemperatureRecord;

public class DeviceTemperatureRecordService
{
    private readonly IDeviceTemperatureRecordRepository _repository;

    public DeviceTemperatureRecordService(IDeviceTemperatureRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DeviceTemperatureRecordModel>> GetAllRecordsAsync()
    {
        return await _repository.GetAllRecordsAsync();
    }

    public async Task<bool> AddTemperatureRecordAsync(DeviceTemperatureRecordData deviceData)
    {
        var recordToValidate = new DeviceTemperatureRecordModel()
        {
            DeviceId = deviceData.DeviceId,
            ExternalTemperature = deviceData.ExternalTemperature,
            InternalTemperature = deviceData.InternalTemperature,
            MeasurementTime = deviceData.MeasurementTime
        };

        var activePeriod = await _repository.ValidateActivePeriod(recordToValidate);

        if (activePeriod is null)
        {
            return false;
        }

        var record = new DeviceTemperatureRecordModel()
        {
            DeviceId = deviceData.DeviceId,
            ExternalTemperature = deviceData.ExternalTemperature,
            InternalTemperature = deviceData.InternalTemperature,
            MeasurementTime = deviceData.MeasurementTime,
            DevicePeriodActiveId = activePeriod.Id,
        };

        await _repository.AddTemperatureRecordAsync(record);

        return true;
    }
}
