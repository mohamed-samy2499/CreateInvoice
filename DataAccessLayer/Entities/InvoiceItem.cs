﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public string? Unit { get; set; }
        public decimal Price { get; set; }
        public string? Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal Net { get; set; }

        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
        public int ItemId { get; set; }
        public Item? Item { get; set; }

    }
}
