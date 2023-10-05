using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly AppDbContext context;
        public InvoiceRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllWithIncludes()
        {
            return await context.Set<Invoice>()
                .Include(i => i.Store).ToListAsync();
        }

        public async Task<Invoice> GetDetailsById(int id)
        {
            return await context.Invoices.Include(i => i.InvoiceItems).Include( i=> i.Store)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
