namespace ConcentraTest.DTOs
{
    public class DTOVehicleType
    {
        public int VehicleTypeId { get; set; }

        public string VehicleType1 { get; set; } = null!;

        public string PlateType { get; set; } = null!;

        public decimal PlatePrice { get; set; }

        public int UserId { get; set; }

        public int StatusId { get; set; }

        public DateTime Wdate { get; set; }
    }
}
