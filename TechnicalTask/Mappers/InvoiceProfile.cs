using AutoMapper;
using DataAccessLayer.Entities;
using TechnicalTask.Models;

namespace TechnicalTask.Mappers
{
    public class InvoiceProfile:Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceViewModel>().ReverseMap();
        }
    }
}
