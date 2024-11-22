using System.Text.Json.Serialization;

namespace SolarEnergyStore.Models.DevicePeriodActive;
public class UpdateDevicePeriodActiveData
{
    [JsonIgnore]
    public int Id { get; set; }

    [JsonPropertyName("deviceId")]
    public int DeviceId { get; set; }

    [JsonPropertyName("periodStart")]
    public DateTime PeriodStart { get; set; }

    [JsonPropertyName("periodEnd")]
    public DateTime? PeriodEnd { get; set; }

    public UpdateDevicePeriodActiveData AddProperty(int id)
    {
        Id = id;
        return this;
    }
}
