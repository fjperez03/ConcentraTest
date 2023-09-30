namespace ConcentraTest.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string Status1 { get; set; } = null!;

    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    public virtual ICollection<PersonType> PersonTypes { get; set; } = new List<PersonType>();

    public virtual ICollection<PlateRecord> PlateRecords { get; set; } = new List<PlateRecord>();

    public virtual ICollection<VehicleType> VehicleTypes { get; set; } = new List<VehicleType>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
