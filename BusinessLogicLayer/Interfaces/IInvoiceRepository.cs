using DataAccessLayer.Entities;


namespace BusinessLogicLayer.Interfaces
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<IEnumerable<Invoice>> GetAllWithIncludes();
        Task<Invoice> GetDetailsById(int id);
    }
}
