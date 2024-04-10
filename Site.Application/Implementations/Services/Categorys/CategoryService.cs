using Site.Application.Definitions.Contracts.Repositories;
using Site.Application.Definitions.Contracts.Services.Categorys;
using Site.Application.Implementations.Validators.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Implementations.Services.Categorys
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var categories = await _repository.GetById(id);
            return mapper.Map<CategoryDto>(categories);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAll(i => i.Id, false);
            return mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<BaseResponse<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var response = new BaseResponse<CategoryDto>();

            var category = mapper.Map<Category>(categoryDto);
            category = await _repository.Add(category);

            var categoryDtoCreated = mapper.Map<CategoryDto>(category);
            response.Success = true;
            response.Message = "Category created successfully";
            response.Data = categoryDtoCreated;
            return response;
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {
            var categories = mapper.Map<Category>(categoryDto);
            await _repository.Update(categories);
        }

        public async Task DeleteAsync(int id)
        {
            var Category = await _repository.GetById(id);
            if (Category != null)
            {
                await _repository.Remove(Category);
            }
        }
    }
}
