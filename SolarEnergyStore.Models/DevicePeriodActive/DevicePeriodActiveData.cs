using System.Text.Json.Serialization;

namespace SolarEnergyStore.Models.DevicePeriodActive;
public class DevicePeriodActiveData
{
    [JsonIgnore]
    public int DeviceId { get; set; }

    [JsonPropertyName("periodStart")]
    public DateTime PeriodStart { get; set; }

    [JsonPropertyName("periodEnd")]
    public DateTime? PeriodEnd { get; set; }

    public DevicePeriodActiveData AddProperty(int deviceId)
    {
        DeviceId = deviceId;
        return this;
    }
}
