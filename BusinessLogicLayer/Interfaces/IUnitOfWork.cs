

namespace BusinessLogicLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IInvoiceItemRepository InvoiceItemRepository { get; set; }
        public IInvoiceRepository InvoiceRepository { get; set; }
        public IStoreRepository StoreRepository { get; set; }
        public IItemRepository ItemRepository { get; set; }
        public IUnitRepository UnitRepository { get; set; }

    }
}
