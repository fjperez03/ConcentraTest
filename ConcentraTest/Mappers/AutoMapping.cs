using AutoMapper;
using ConcentraTest.DTOs;

namespace ConcentraTest.Mappers
{
    public class AutoMapping:Profile
    {
        public AutoMapping() {
            CreateMap<DTOUser, User>().ReverseMap();
            CreateMap<DTOBrand, Brand>().ReverseMap();
            CreateMap<DTOClient, Client>().ReverseMap();
            CreateMap<DTOModel, Model>().ReverseMap();
            CreateMap<DTOPersonType, PersonType>().ReverseMap();
            CreateMap<DTOPlate, Plate>().ReverseMap();
            CreateMap<DTOPlateRecord, PlateRecord>().ReverseMap();
            CreateMap<DTOVehicle, Vehicle>().ReverseMap();
            CreateMap<DTOVehicleType, VehicleType>().ReverseMap();
            CreateMap<DTOStatus, Status>().ReverseMap();
        }    
    }
}
