using AutoMapper;
using DataAccessLayer.Entities;
using TechnicalTask.Models;

namespace TechnicalTask.Mappers
{
    public class InvoiceItemProfile:Profile
    {
        public InvoiceItemProfile()
        {
            CreateMap<InvoiceItemViewModel, InvoiceItem>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Unit))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
           .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
           .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.InvoiceItemTotal))
           .ForMember(dest => dest.Net, opt => opt.MapFrom(src => src.InvoiceItemNet))
           .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(src => src.InvoiceViewModelId))
           .ForMember(dest => dest.Invoice, opt => opt.MapFrom(src => src.InvoiceViewModel))
           .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.ItemId))
           .ForMember(dest => dest.Item, opt => opt.MapFrom(src => src.Item))
           .ReverseMap();
        }
    }
}
