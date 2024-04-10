using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Definitions.Dtos.Invoices
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
