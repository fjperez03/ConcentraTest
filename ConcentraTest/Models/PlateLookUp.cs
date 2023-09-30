using ConcentraTest.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConcentraTest.Models
{
    public class PlateLookUp
    {
		public string PlateType { get; set; } = null!;
        public string Plate { get; set; } = null!;
        public ClientData clientData { get; set; } = null!;
		public DateTime wDate { get; set; }
		public decimal plateValue { get; set; }
    }
}
