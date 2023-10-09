using DataAccessLayer.Entities;


namespace BusinessLogicLayer.Interfaces
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> GetAllWithIncludes();
        Task<Invoice> GetByIdWithItemsInclude(int id);
        Task<Invoice> GetDetailsById(int id);
    }
}
