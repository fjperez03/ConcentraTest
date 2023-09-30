namespace ConcentraTest.Models;

public partial class Brand
{
    public int BrandId { get; set; }

    public string Brand1 { get; set; } = null!;

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public DateTime Wdate { get; set; }

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
