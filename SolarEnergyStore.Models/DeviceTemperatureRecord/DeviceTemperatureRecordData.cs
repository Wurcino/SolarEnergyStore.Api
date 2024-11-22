using System.Text.Json.Serialization;

namespace SolarEnergyStore.Models.DeviceTemperatureRecord;

public class DeviceTemperatureRecordData
{
    [JsonIgnore]
    public int DeviceId { get; set; }

    [JsonPropertyName("internalTemperature")]
    public double InternalTemperature { get; set; }

    [JsonPropertyName("externalTemperature")]
    public double ExternalTemperature { get; set; }

    [JsonPropertyName("measurementTime")]
    public DateTime MeasurementTime { get; set; }

    public DeviceTemperatureRecordData AddProperty(int deviceId)
    {
        DeviceId = deviceId;
        return this;
    }
}
