using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Site.Application.Definitions.Contracts.Services.Categorys;
using Site.Application.Definitions.Contracts.Services.Invoices;
using Site.Application.Definitions.Dtos.Categorys;
using Site.Application.Definitions.Dtos.Invoices;

namespace Site.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryBrandsController : ControllerBase
    {
        private readonly ICategoryBrandService _categoryBrandService;

        public CategoryBrandsController(ICategoryBrandService categoryBrandService)
        {
            _categoryBrandService = categoryBrandService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryBrandDto>> GetById(int id)
        {
            var categoryBrand = await _categoryBrandService.GetByIdAsync(id);
            if (categoryBrand == null)
            {
                return NotFound();
            }
            return Ok(categoryBrand);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryBrandDto>>> GetAll()
        {
            var categoryBrands = await _categoryBrandService.GetAllAsync();
            return Ok(categoryBrands);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryBrandDto>> Create([FromBody] CategoryBrandDto categoryBrandDto)
        {
            if (categoryBrandDto == null)
            {
                return BadRequest();
            }
            var createdCategoryBrand = await _categoryBrandService.CreateAsync(categoryBrandDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategoryBrand.Data.Id }, createdCategoryBrand);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CategoryBrandDto categoryBrandDto)
        {
            //if (id != categoryBrandDto.Id)
            //{
            //    return BadRequest("CategoryBrand ID mismatch");
            //}

            var categoryBrandToUpdate = await _categoryBrandService.GetByIdAsync(id);
            if (categoryBrandToUpdate == null)
            {
                return NotFound($"CategoryBrand with Id = {id} not found");
            }

            await _categoryBrandService.UpdateAsync(categoryBrandDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryBrandToDelete = await _categoryBrandService.GetByIdAsync(id);
            if (categoryBrandToDelete == null)
            {
                return NotFound($"CategoryBrand with Id = {id} not found");
            }

            await _categoryBrandService.DeleteAsync(id);
            return NoContent();
        }
    }

}