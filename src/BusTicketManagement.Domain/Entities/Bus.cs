namespace BusTicketManagement.Domain.Entities;

public class Bus
{
    public int BusId { get; set; }
    public string BusNo { get; set; } = string.Empty;
    public int BusTypeId { get; set; }
    public int TotalCapacity { get; set; }
    public int CurrentOccupancy { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string ManufacturerModel { get; set; } = string.Empty;
    public int YearOfManufacture { get; set; }
    public DateTime PurchaseDate { get; set; }
    public DateTime LastMaintenanceDate { get; set; }
    public DateTime NextMaintenanceDate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
