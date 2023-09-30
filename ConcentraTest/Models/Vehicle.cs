namespace ConcentraTest.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public int VehicleTypeId { get; set; }

    public int BrandId { get; set; }

    public int ModelId { get; set; }

    public string Color { get; set; } = null!;

    public int Year { get; set; }

    public string ClientId { get; set; } = null!;

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public DateTime Wdate { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual Model Model { get; set; } = null!;

    public virtual ICollection<Plate> Plates { get; set; } = new List<Plate>();

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual VehicleType VehicleType { get; set; } = null!;
}
