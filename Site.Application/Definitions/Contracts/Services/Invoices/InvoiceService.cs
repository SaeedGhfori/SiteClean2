using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Definitions.Contracts.Services.Invoices
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> GetByIdAsync(int id);
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
        Task<BaseResponse<InvoiceDto>> CreateAsync(InvoiceDto invoiceDto);
        Task UpdateAsync(InvoiceDto invoiceDto);
        Task DeleteAsync(int id);
    }
}
