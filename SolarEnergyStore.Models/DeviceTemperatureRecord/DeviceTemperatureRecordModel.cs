using SolarEnergyStore.Models.DevicePeriodActive;

namespace SolarEnergyStore.Models.DeviceTemperatureRecord;
public class DeviceTemperatureRecordModel
{
    public int Id { get; set; }

    public int DeviceId { get; set; }

    public double InternalTemperature { get; set; }

    public double ExternalTemperature { get; set; }

    public DateTime MeasurementTime { get; set; }

    public int DevicePeriodActiveId { get; set; }
    public DevicePeriodActiveModel DevicePeriodActive { get; set; }
}
