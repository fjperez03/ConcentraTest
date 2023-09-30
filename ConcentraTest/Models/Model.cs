namespace ConcentraTest.Models;

public partial class Model
{
    public int ModelId { get; set; }

    public string Model1 { get; set; } = null!;

    public int BrandId { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public DateTime Wdate { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
