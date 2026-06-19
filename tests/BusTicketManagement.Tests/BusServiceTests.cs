using Xunit;
using Moq;
using BusTicketManagement.Application.Services;
using BusTicketManagement.Infrastructure.Repository;
using BusTicketManagement.Application.DTOs.BusDTOs;
using AutoMapper;
using BusTicketManagement.Domain.Entities;

namespace BusTicketManagement.Tests;

public class BusServiceTests
{
    private readonly Mock<IBusRepository> _mockBusRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<BusService>> _mockLogger;
    private readonly BusService _busService;

    public BusServiceTests()
    {
        _mockBusRepository = new Mock<IBusRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<BusService>>();
        _busService = new BusService(_mockBusRepository.Object, _mockMapper.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAllBusesAsync_ShouldReturnListOfBuses()
    {
        // Arrange
        var buses = new List<Bus>
        {
            new Bus { BusId = 1, BusNo = "BUS001", TotalCapacity = 50 },
            new Bus { BusId = 2, BusNo = "BUS002", TotalCapacity = 45 }
        };
        var busesDto = new List<BusDto>
        {
            new BusDto { BusId = 1, BusNo = "BUS001", TotalCapacity = 50 },
            new BusDto { BusId = 2, BusNo = "BUS002", TotalCapacity = 45 }
        };

        _mockBusRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(buses);
        _mockMapper.Setup(m => m.Map<List<BusDto>>(buses)).Returns(busesDto);

        // Act
        var result = await _busService.GetAllBusesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _mockBusRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetBusByIdAsync_WithValidId_ShouldReturnBus()
    {
        // Arrange
        var bus = new Bus { BusId = 1, BusNo = "BUS001", TotalCapacity = 50 };
        var busDto = new BusDto { BusId = 1, BusNo = "BUS001", TotalCapacity = 50 };

        _mockBusRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(bus);
        _mockMapper.Setup(m => m.Map<BusDto>(bus)).Returns(busDto);

        // Act
        var result = await _busService.GetBusByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.BusId);
        Assert.Equal("BUS001", result.BusNo);
    }

    [Fact]
    public async Task CreateBusAsync_WithValidData_ShouldCreateBus()
    {
        // Arrange
        var createBusDto = new CreateBusDto
        {
            BusNo = "BUS003",
            BusTypeId = 1,
            TotalCapacity = 50,
            RegistrationNumber = "XYZ-123",
            ManufacturerModel = "Mercedes",
            YearOfManufacture = 2023
        };

        var bus = new Bus
        {
            BusId = 1,
            BusNo = createBusDto.BusNo,
            BusTypeId = createBusDto.BusTypeId,
            TotalCapacity = createBusDto.TotalCapacity
        };

        var busDto = new BusDto
        {
            BusId = 1,
            BusNo = "BUS003",
            TotalCapacity = 50
        };

        _mockBusRepository.Setup(r => r.CreateAsync(It.IsAny<Bus>())).ReturnsAsync(bus);
        _mockMapper.Setup(m => m.Map<BusDto>(bus)).Returns(busDto);

        // Act
        var result = await _busService.CreateBusAsync(createBusDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("BUS003", result.BusNo);
        _mockBusRepository.Verify(r => r.CreateAsync(It.IsAny<Bus>()), Times.Once);
    }
}
