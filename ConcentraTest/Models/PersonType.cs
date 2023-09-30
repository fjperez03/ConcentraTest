namespace ConcentraTest.Models;

public partial class PersonType
{
    public int PersonTypeId { get; set; }

    public string PersonType1 { get; set; } = null!;

    public int? UserId { get; set; }

    public int? StatusId { get; set; }

    public DateTime? Wdate { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual Status? Status { get; set; }

    public virtual User? User { get; set; }
}
