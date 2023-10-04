using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Repositories
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
    {
        private readonly AppDbContext context;
        public StoreRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Store>> GetAllWithInvoicesAsync()
        {
            return await context.Stores.Include(i => i.Invoices)
                .ToListAsync();
        }
        public async Task<Store> GetDetailsById(int id)
        {
            return await context.Stores.Include(i => i.Invoices)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}