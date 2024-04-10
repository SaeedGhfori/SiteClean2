using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Contracts.Services.Invoices;
using Site.Application.Implementations.Validators.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Services.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;
        private readonly IMapper mapper;

        public InvoiceService(IInvoiceRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public async Task<InvoiceDto> GetByIdAsync(int id)
        {
            var categoryBrand = await _repository.GetById(id);
            return mapper.Map<InvoiceDto>(categoryBrand);
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _repository.GetAll(i => i.Id, false);
            return mapper.Map<List<InvoiceDto>>(invoices);
        }

        public async Task<BaseResponse<InvoiceDto>> CreateAsync(InvoiceDto invoiceDto)
        {
            var response = new BaseResponse<InvoiceDto>();
            var validationResult = await new InvoiceDtoValidator().ValidateAsync(invoiceDto);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
                return response;
            }

            var invoice = mapper.Map<Invoice>(invoiceDto);
            invoice = await _repository.Add(invoice);

            var invoiceDtoCreated = mapper.Map<InvoiceDto>(invoice);
            response.Success = true;
            response.Message = "Invoice created successfully";
            response.Data = invoiceDtoCreated;
            return response;
        }

        public async Task UpdateAsync(InvoiceDto invoiceDto)
        {
            var invoices = mapper.Map<Invoice>(invoiceDto);
            await _repository.Update(invoices);
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await _repository.GetById(id);
            if (invoice != null)
            {
                await _repository.Remove(invoice);
            }
        }
    }
}
