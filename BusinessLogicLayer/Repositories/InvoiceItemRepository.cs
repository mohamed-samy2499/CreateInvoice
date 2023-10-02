using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Repositories
{
    public class InvoiceItemRepository : GenericRepository<InvoiceItem>, IInvoiceItemRepository
    {
        private readonly AppDbContext context;
        public InvoiceItemRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
