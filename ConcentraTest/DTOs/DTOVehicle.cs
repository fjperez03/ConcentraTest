namespace ConcentraTest.DTOs
{
    public class DTOVehicle
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
    }
}
