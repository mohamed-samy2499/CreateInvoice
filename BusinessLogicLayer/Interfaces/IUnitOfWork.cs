using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IInvoiceItemRepository InvoiceItemRepository { get; set; }
        public IInvoiceRepository InvoiceRepository { get; set; }
        public IStoreRepository StoreRepository { get; set; }
    }
}
