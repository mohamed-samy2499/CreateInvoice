using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        public IInvoiceRepository? InvoiceRepository { get; set; }
        public IInvoiceItemRepository? InvoiceItemRepository { get; set; }
        public IStoreRepository? StoreRepository { get; set; }

        public UnitOfWork(IInvoiceRepository invoiceRepository, IInvoiceItemRepository invoiceItemRepository,
            IStoreRepository storeRepository)
        {
            InvoiceRepository = invoiceRepository;
            InvoiceItemRepository = invoiceItemRepository;
            StoreRepository = storeRepository;
        }


    }
}
