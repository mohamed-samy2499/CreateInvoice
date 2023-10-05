using AutoMapper;
using DataAccessLayer.Entities;
using TechnicalTask.Models;

namespace TechnicalTask.Mappers
{
    public class InvoiceItemProfile:Profile
    {
        public InvoiceItemProfile()
        {
            CreateMap<InvoiceItem, InvoiceItemViewModel>().ReverseMap();
        }
    }
}
