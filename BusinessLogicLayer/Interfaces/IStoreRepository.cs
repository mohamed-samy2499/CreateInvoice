using DataAccessLayer.Entities;


namespace BusinessLogicLayer.Interfaces
{
    public interface IStoreRepository : IGenericRepository<Store>
    {
        Task<IEnumerable<Store>> GetAllWithInvoicesAsync();
        Task<Store> GetDetailsById(int id);
    }
}
