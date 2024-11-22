using Moq;
using SolarEnergyStore.Models.DevicePeriodActive;
using SolarEnergyStore.Services.DevicePeriodActive;
using SolarEnergyStore.Services.Interfaces;

namespace SolarEnergyStore.Tests;

public class DevicePeriodActiveServiceTests
{
    private readonly Mock<IDevicePeriodActiveRepository> _mockRepository;
    private readonly DevicePeriodActiveService _service;

    public DevicePeriodActiveServiceTests()
    {
        _mockRepository = new Mock<IDevicePeriodActiveRepository>();
        _service = new DevicePeriodActiveService(_mockRepository.Object);
    }

    [Fact]
    public async Task AddActivePeriodAsync_CallsRepositoryMethod()
    {
        var period = new DevicePeriodActiveData
        {
            DeviceId = 123,
            PeriodStart = DateTime.UtcNow.AddHours(-1),
            PeriodEnd = DateTime.UtcNow.AddHours(1)
        };

        _mockRepository.Setup(repo => repo.AddActivePeriodAsync(It.IsAny<DevicePeriodActiveModel>()))
            .Returns(Task.CompletedTask);

        await _service.AddActivePeriodAsync(period);

        _mockRepository.Verify(repo => repo.AddActivePeriodAsync(It.IsAny<DevicePeriodActiveModel>()), Times.Once);
    }

    [Fact]
    public async Task UpdateActivePeriodAsync_CallsRepositoryMethod()
    {
        var period = new UpdateDevicePeriodActiveData
        {
            DeviceId = 123,
            PeriodStart = DateTime.UtcNow.AddHours(-1),
            PeriodEnd = DateTime.UtcNow.AddHours(1)
        };

        _mockRepository.Setup(repo => repo.UpdateActivePeriodAsync(It.IsAny<DevicePeriodActiveModel>()))
            .Returns(Task.CompletedTask);

        await _service.UpdateActivePeriodAsync(period);

        _mockRepository.Verify(repo => repo.UpdateActivePeriodAsync(It.IsAny<DevicePeriodActiveModel>()), Times.Once);
    }

    [Fact]
    public async Task GetActivePeriodByIdAsync_ReturnsActivePeriods()
    {
        var periods = new List<DevicePeriodActiveModel>
    {
        new() { Id = 1, DeviceId = 123, PeriodStart = DateTime.UtcNow.AddHours(-2), PeriodEnd = null }
    };

        _mockRepository.Setup(repo => repo.GetActivePeriodByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(periods);

        var result = await _service.GetActivePeriodByIdAsync(1);

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(123, result[0].DeviceId);
    }

}