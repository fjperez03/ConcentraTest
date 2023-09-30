namespace ConcentraTest.Models;

public partial class Plate
{
    public string PlateId { get; set; } = null!;

    public int VehicleId { get; set; }

    public virtual ICollection<PlateRecord> PlateRecords { get; set; } = new List<PlateRecord>();

    public virtual Vehicle Vehicle { get; set; } = null!;
}
