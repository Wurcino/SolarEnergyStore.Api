using Microsoft.AspNetCore.Mvc;
using SolarEnergyStore.Models.DevicePeriodActive;
using SolarEnergyStore.Models.DeviceTemperatureRecord;
using SolarEnergyStore.Services.DevicePeriodActive;
using SolarEnergyStore.Services.DeviceTemperatureRecord;

namespace SolarEnergyStore.Api.Controllers;
[Route("api/device")]
[ApiController]
public class DeviceController : ControllerBase
{
    private readonly DevicePeriodActiveService _devicePeriodActiveService;
    private readonly DeviceTemperatureRecordService _deviceTemperatureRecordService;

    public DeviceController(
        DevicePeriodActiveService devicePeriodActiveService,
        DeviceTemperatureRecordService deviceTemperatureRecordService
    )
    {
        _devicePeriodActiveService = devicePeriodActiveService;
        _deviceTemperatureRecordService = deviceTemperatureRecordService;
    }

    [HttpGet("temperature-record")]
    public async Task<IActionResult> GetAll()
    {
        var records = await _deviceTemperatureRecordService.GetAllRecordsAsync();
        return Ok(records);
    }

    [HttpPost("temperature-record/{deviceId}")]
    public async Task<IActionResult> AddTemperatureRecord(
        [FromRoute] int deviceId,
        [FromBody] DeviceTemperatureRecordData record
    )
    {
        var response = await _deviceTemperatureRecordService.AddTemperatureRecordAsync(record.AddProperty(deviceId));
        return response ?
            Created($"temperature-record/{deviceId}", new { message = "Temperature record added successfully" })
            : NotFound("No active period found.");
    }

    [HttpGet("active-period/{deviceId}")]
    public async Task<IActionResult> GetById(
        [FromRoute] int deviceId
    )
    {
        var period = await _devicePeriodActiveService.GetActivePeriodByIdAsync(deviceId);
        return period == null ? NotFound() : Ok(period);
    }

    [HttpPost("active-period/{deviceId}")]
    public async Task<IActionResult> AddActivePeriod(
        [FromRoute] int deviceId,
        [FromBody] DevicePeriodActiveData period
    )
    {
        await _devicePeriodActiveService.AddActivePeriodAsync(period.AddProperty(deviceId));
        return Created($"active-period/{deviceId}", new { message = "Work period added successfully" });
    }

    [HttpPut("active-period/{id}")]
    public async Task<IActionResult> UpdateActivePeriod(
        [FromRoute] int id,
        [FromBody] UpdateDevicePeriodActiveData period
    )
    {
        await _devicePeriodActiveService.UpdateActivePeriodAsync(period.AddProperty(id));
        return Ok(new { message = "Work period updated successfully" });
    }

}
