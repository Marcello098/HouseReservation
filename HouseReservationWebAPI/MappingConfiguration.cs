using AutoMapper;
using HouseReservationWebAPI.Models;
using HouseReservationWebAPI.Models.DTO;

namespace HouseReservationWebAPI
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<House, HouseDto>();
            CreateMap<HouseDto, House>();

            CreateMap<House, HouseCreateDto>().ReverseMap();
            CreateMap<House, HouseUpdateDto>().ReverseMap();
        }
    }
}
