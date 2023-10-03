using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Repositories
{
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        private readonly AppDbContext context;
        public ItemRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
