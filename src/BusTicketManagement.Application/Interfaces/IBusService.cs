using BusTicketManagement.Application.DTOs.BusDTOs;

namespace BusTicketManagement.Application.Interfaces;

public interface IBusService
{
    Task<List<BusDto>> GetAllBusesAsync();
    Task<BusDto> GetBusByIdAsync(int busId);
    Task<BusDto> CreateBusAsync(CreateBusDto createBusDto);
    Task<BusDto> UpdateBusAsync(UpdateBusDto updateBusDto);
    Task<bool> DeleteBusAsync(int busId);
}
