using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.Entities.Invoices
{
    [Auditable]
    public class Invoice
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
