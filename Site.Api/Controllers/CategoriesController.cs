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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Create(CategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }

            var createdCategory = await _categoryService.CreateAsync(categoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Data.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest("Category ID mismatch");
            }

            var categoryToUpdate = await _categoryService.GetByIdAsync(id);
            if (categoryToUpdate == null)
            {
                return NotFound($"Category with Id = {id} not found");
            }

            await _categoryService.UpdateAsync(categoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryToDelete = await _categoryService.GetByIdAsync(id);
            if (categoryToDelete == null)
            {
                return NotFound($"Category with Id = {id} not found");
            }

            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}