using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System.Web.WebPages.Html;

namespace BusinessLogicLayer.Repositories
{
    public class UnitRepository : GenericRepository<Unit>, IUnitRepository
    {
        private readonly AppDbContext context;
        public UnitRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }


    }
}
