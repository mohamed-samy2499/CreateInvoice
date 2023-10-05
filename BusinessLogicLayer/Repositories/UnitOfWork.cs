using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        public IInvoiceRepository? InvoiceRepository { get; set; }
        public IInvoiceItemRepository? InvoiceItemRepository { get; set; }
        public IStoreRepository? StoreRepository { get; set; }
        public IItemRepository? ItemRepository { get; set; }
        public IUnitRepository? UnitRepository { get; set; }


        public UnitOfWork(IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository,
            IStoreRepository storeRepository, IItemRepository itemRepository, IUnitRepository unitRepository)
        {
            InvoiceRepository = invoiceRepository;
            InvoiceItemRepository = invoiceItemRepository;
            StoreRepository = storeRepository;
            ItemRepository = itemRepository;
            UnitRepository = unitRepository;
        }


    }
}
