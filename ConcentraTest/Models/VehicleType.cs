namespace ConcentraTest.Models;

public partial class VehicleType
{
    public int VehicleTypeId { get; set; }

    public string VehicleType1 { get; set; } = null!;

    public string PlateType { get; set; } = null!;

    public decimal PlatePrice { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public DateTime Wdate { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
