namespace BusTicketManagement.Application.DTOs.BusDTOs;

public class BusDto
{
    public int BusId { get; set; }
    public string BusNo { get; set; } = string.Empty;
    public int BusTypeId { get; set; }
    public int TotalCapacity { get; set; }
    public int CurrentOccupancy { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string ManufacturerModel { get; set; } = string.Empty;
    public int YearOfManufacture { get; set; }
    public bool IsActive { get; set; }
}

public class CreateBusDto
{
    public string BusNo { get; set; } = string.Empty;
    public int BusTypeId { get; set; }
    public int TotalCapacity { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string ManufacturerModel { get; set; } = string.Empty;
    public int YearOfManufacture { get; set; }
}

public class UpdateBusDto
{
    public int BusId { get; set; }
    public string? BusNo { get; set; }
    public int? BusTypeId { get; set; }
    public int? TotalCapacity { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? ManufacturerModel { get; set; }
    public int? YearOfManufacture { get; set; }
}
