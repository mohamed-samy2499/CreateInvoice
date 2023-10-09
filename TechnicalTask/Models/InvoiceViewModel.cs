using DataAccessLayer.Entities;

namespace TechnicalTask.Models
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public int InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public decimal Taxes { get; set; }
        public decimal Net { get; set; }
        public StoreViewModel? Store { get; set; }
        public int StoreId { get; set; }
        public ICollection<InvoiceItemViewModel>? InvoiceItemsViewModel { get; set; } = new List<InvoiceItemViewModel>();
    }
}
