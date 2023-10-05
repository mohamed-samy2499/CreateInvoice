using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TechnicalTask.Models
{
    public class InvoiceItemViewModel
    {
        public Guid Id { get; set; }
        public string? Unit { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal InvoiceItemTotal { get; set; }
        public decimal InvoiceItemNet { get; set; }
        public int InvoiceViewModelId { get; set; }
        public InvoiceViewModel? InvoiceViewModel { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }
        public IEnumerable<SelectListItem>? AvailableItems { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? AvailableUnits { get; set; } = new List<SelectListItem>();
    }
}
