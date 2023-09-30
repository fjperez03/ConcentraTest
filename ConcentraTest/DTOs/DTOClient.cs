namespace ConcentraTest.DTOs
{
    public class DTOClient
    {
        public string ClientId { get; set; } = null!;

        public int PersonTypeId { get; set; }

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime Birthdate { get; set; }
        public int UserId { get; set; }

        public int StatusId { get; set; }

        public DateTime Wdate { get; set; }
    }
}
