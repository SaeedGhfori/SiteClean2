using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Definitions.Contracts.Services.Invoices;
using Site.Application.Definitions.Dtos.Invoices;

namespace Site.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;

        public InvoicesController(IInvoiceService invoiceService, IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var invoices = await _invoiceService.GetAllAsync();
            return Ok(invoices);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(InvoiceDto invoiceDto)
        {
            var response = await _invoiceService.CreateAsync(invoiceDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { id = response.Data.Id }, response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, InvoiceDto invoiceDto)
        {
            if (id != invoiceDto.Id) return BadRequest("Identifier mismatch");
            await _invoiceService.UpdateAsync(invoiceDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _invoiceService.DeleteAsync(id);
            return NoContent();
        }

        [HttpHead("{id}")]
        public async Task<IActionResult> HeadAsync(int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);
            if (invoice == null) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(int id, JsonPatchDocument<InvoiceDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Patch document is empty");

            var invoiceFromDb = await _invoiceService.GetByIdAsync(id);
            if (invoiceFromDb == null) return NotFound();

            var invoiceToPatch = _mapper.Map<InvoiceDto>(invoiceFromDb);

            patchDoc.ApplyTo(invoiceToPatch, error =>
            {
                ModelState.AddModelError(error.Operation.path, error.ErrorMessage);
            });

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _mapper.Map(invoiceToPatch, invoiceFromDb);
            await _invoiceService.UpdateAsync(invoiceFromDb);

            return NoContent();
        }

    }
}
