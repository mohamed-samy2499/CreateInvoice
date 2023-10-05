using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System.Web.WebPages.Html;

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
