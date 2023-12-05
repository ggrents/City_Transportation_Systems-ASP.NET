using AutoMapper;
using City_Transportation_Systems.DTO;
using City_Transportation_Systems.Models;
using Route = City_Transportation_Systems.Models.Route;


namespace City_Transportation_Systems.Data
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {

            CreateMap<Bus, BusDTO>();
            CreateMap<BusDTO, Bus>();
            CreateMap<CreateBusDTO, Bus>();
            CreateMap<CreateRouteDTO, Route>();
            CreateMap<RouteDTO, Route>();
            CreateMap<Route, RouteDTO>();
            CreateMap<Driver, DriverDTO>();
            CreateMap<CreateDriverDTO, Driver>();
            CreateMap<Station, StationDTO>();
            CreateMap<StationDTO, Station>();
            CreateMap<CreateStationDTO, Station>();

        }
    }
}
