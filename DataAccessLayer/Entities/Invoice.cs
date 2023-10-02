using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Taxes { get; set; }
        public decimal Net { get; set; }
        public Store? Store { get; set; }
        public int StoreId { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new HashSet<InvoiceItem>();
    }
}
