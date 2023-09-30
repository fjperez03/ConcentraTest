namespace ConcentraTest.Models;

public partial class PlateRecord
{
    public int PlateRecordId { get; set; }

    public string PlateId { get; set; } = null!;

    public decimal PlateValue { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public DateTime Wdate { get; set; }

    public virtual Plate Plate { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
