using Moq;
using SolarEnergyStore.Models.DevicePeriodActive;
using SolarEnergyStore.Models.DeviceTemperatureRecord;
using SolarEnergyStore.Services.DeviceTemperatureRecord;
using SolarEnergyStore.Services.Interfaces;

namespace SolarEnergyStore.Tests;

public class DeviceTemperatureRecordServiceTests
{
    private readonly Mock<IDeviceTemperatureRecordRepository> _mockRepository;
    private readonly DeviceTemperatureRecordService _service;

    public DeviceTemperatureRecordServiceTests()
    {
        _mockRepository = new Mock<IDeviceTemperatureRecordRepository>();
        _service = new DeviceTemperatureRecordService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllRecordsAsync_ReturnsAllRecords()
    {
        var records = new List<DeviceTemperatureRecordModel>
    {
        new() { Id = 1, DeviceId = 123, InternalTemperature = 25.5, ExternalTemperature = 30.0 },
        new() { Id = 2, DeviceId = 124, InternalTemperature = 20.0, ExternalTemperature = 28.0 }
    };

        _mockRepository.Setup(repo => repo.GetAllRecordsAsync())
            .ReturnsAsync(records);

        var result = await _service.GetAllRecordsAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(123, result[0].DeviceId);
        Assert.Equal(124, result[1].DeviceId);
    }

    [Fact]
    public async Task AddTemperatureRecordAsync_ValidatesActivePeriod_ReturnsTrue()
    {
        var deviceData = new DeviceTemperatureRecordData
        {
            DeviceId = 123,
            InternalTemperature = 25.5,
            ExternalTemperature = 30.0,
            MeasurementTime = DateTime.UtcNow
        };

        var activePeriod = new DevicePeriodActiveModel
        {
            Id = 1,
            DeviceId = 123,
            PeriodStart = DateTime.UtcNow.AddHours(-1),
            PeriodEnd = DateTime.UtcNow.AddHours(1)
        };

        _mockRepository.Setup(repo => repo.ValidateActivePeriod(It.IsAny<DeviceTemperatureRecordModel>()))
            .ReturnsAsync(activePeriod);

        _mockRepository.Setup(repo => repo.AddTemperatureRecordAsync(It.IsAny<DeviceTemperatureRecordModel>()))
            .Returns(Task.CompletedTask);

        var result = await _service.AddTemperatureRecordAsync(deviceData);

        Assert.True(result);
        _mockRepository.Verify(repo => repo.AddTemperatureRecordAsync(It.IsAny<DeviceTemperatureRecordModel>()), Times.Once);
    }

    [Fact]
    public async Task AddTemperatureRecordAsync_InvalidActivePeriod_ReturnsFalse()
    {
        var deviceData = new DeviceTemperatureRecordData
        {
            DeviceId = 123,
            InternalTemperature = 25.5,
            ExternalTemperature = 30.0,
            MeasurementTime = DateTime.UtcNow
        };

        _mockRepository.Setup(repo => repo.ValidateActivePeriod(It.IsAny<DeviceTemperatureRecordModel>()))
            .ReturnsAsync((DevicePeriodActiveModel)null);

        var result = await _service.AddTemperatureRecordAsync(deviceData);

        Assert.False(result);
        _mockRepository.Verify(repo => repo.AddTemperatureRecordAsync(It.IsAny<DeviceTemperatureRecordModel>()), Times.Never);
    }

}