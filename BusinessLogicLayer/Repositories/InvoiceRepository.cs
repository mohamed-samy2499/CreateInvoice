using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;


namespace BusinessLogicLayer.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly AppDbContext context;
        public InvoiceRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
