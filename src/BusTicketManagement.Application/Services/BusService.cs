using BusTicketManagement.Application.DTOs.BusDTOs;
using BusTicketManagement.Application.Interfaces;
using BusTicketManagement.Domain.Entities;
using BusTicketManagement.Infrastructure.Repository;
using AutoMapper;

namespace BusTicketManagement.Application.Services;

public class BusService : IBusService
{
    private readonly IBusRepository _busRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<BusService> _logger;

    public BusService(IBusRepository busRepository, IMapper mapper, ILogger<BusService> logger)
    {
        _busRepository = busRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<BusDto>> GetAllBusesAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all buses");
            var buses = await _busRepository.GetAllAsync();
            return _mapper.Map<List<BusDto>>(buses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching buses");
            throw;
        }
    }

    public async Task<BusDto> GetBusByIdAsync(int busId)
    {
        try
        {
            _logger.LogInformation($"Fetching bus with ID: {busId}");
            var bus = await _busRepository.GetByIdAsync(busId);
            if (bus == null)
                throw new InvalidOperationException($"Bus with ID {busId} not found");
            
            return _mapper.Map<BusDto>(bus);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error fetching bus {busId}");
            throw;
        }
    }

    public async Task<BusDto> CreateBusAsync(CreateBusDto createBusDto)
    {
        try
        {
            _logger.LogInformation($"Creating new bus: {createBusDto.BusNo}");
            
            var bus = new Bus
            {
                BusNo = createBusDto.BusNo,
                BusTypeId = createBusDto.BusTypeId,
                TotalCapacity = createBusDto.TotalCapacity,
                RegistrationNumber = createBusDto.RegistrationNumber,
                ManufacturerModel = createBusDto.ManufacturerModel,
                YearOfManufacture = createBusDto.YearOfManufacture,
                IsActive = true
            };

            var createdBus = await _busRepository.CreateAsync(bus);
            _logger.LogInformation($"Bus created successfully with ID: {createdBus.BusId}");
            
            return _mapper.Map<BusDto>(createdBus);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating bus");
            throw;
        }
    }

    public async Task<BusDto> UpdateBusAsync(UpdateBusDto updateBusDto)
    {
        try
        {
            _logger.LogInformation($"Updating bus with ID: {updateBusDto.BusId}");
            
            var existingBus = await _busRepository.GetByIdAsync(updateBusDto.BusId);
            if (existingBus == null)
                throw new InvalidOperationException($"Bus with ID {updateBusDto.BusId} not found");

            if (!string.IsNullOrEmpty(updateBusDto.BusNo))
                existingBus.BusNo = updateBusDto.BusNo;
            if (updateBusDto.BusTypeId.HasValue)
                existingBus.BusTypeId = updateBusDto.BusTypeId.Value;
            if (updateBusDto.TotalCapacity.HasValue)
                existingBus.TotalCapacity = updateBusDto.TotalCapacity.Value;
            if (!string.IsNullOrEmpty(updateBusDto.RegistrationNumber))
                existingBus.RegistrationNumber = updateBusDto.RegistrationNumber;
            if (!string.IsNullOrEmpty(updateBusDto.ManufacturerModel))
                existingBus.ManufacturerModel = updateBusDto.ManufacturerModel;
            if (updateBusDto.YearOfManufacture.HasValue)
                existingBus.YearOfManufacture = updateBusDto.YearOfManufacture.Value;

            existingBus.UpdatedAt = DateTime.UtcNow;
            var updatedBus = await _busRepository.UpdateAsync(existingBus);
            
            _logger.LogInformation($"Bus updated successfully with ID: {updatedBus.BusId}");
            return _mapper.Map<BusDto>(updatedBus);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating bus");
            throw;
        }
    }

    public async Task<bool> DeleteBusAsync(int busId)
    {
        try
        {
            _logger.LogInformation($"Deleting bus with ID: {busId}");
            var result = await _busRepository.DeleteAsync(busId);
            
            if (result)
                _logger.LogInformation($"Bus deleted successfully with ID: {busId}");
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting bus");
            throw;
        }
    }
}
