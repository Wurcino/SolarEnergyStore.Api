using SolarEnergyStore.Models.DeviceTemperatureRecord;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolarEnergyStore.Models.DevicePeriodActive;

public class DevicePeriodActiveModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int DeviceId { get; set; }

    public DateTime PeriodStart { get; set; }

    public DateTime? PeriodEnd { get; set; }

    public ICollection<DeviceTemperatureRecordModel> TemperatureRecords { get; set; }
}
