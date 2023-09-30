using System;
using System.Collections.Generic;

namespace ConcentraTest.Models;

public partial class Client
{
    public string ClientId { get; set; } = null!;

    public int PersonTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime Birthdate { get; set; }
    public int UserId { get; set; }

    public int StatusId { get; set; }

    public DateTime Wdate { get; set; }

    public virtual PersonType PersonType { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
