namespace ConcentraTest.Models
{
    public class ClientData
    {
        public string ClientId { get; set; } = null!;

        public string PersonType { get; set; }

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime Birthdate { get; set; }
    }
}
