using AutoMapper;
using DataAccessLayer.Entities;
using TechnicalTask.Models;

namespace TechnicalTask.Mappers
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<Store, StoreViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ReverseMap();
        }
    }
}
