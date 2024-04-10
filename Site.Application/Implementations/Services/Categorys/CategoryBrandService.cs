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
    public class CategoryBrandService : ICategoryBrandService
    {
        private readonly ICategoryBrandRepository _repository;
        private readonly IMapper mapper;

        public CategoryBrandService(ICategoryBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            this.mapper = mapper;
        }

        public async Task<CategoryBrandDto> GetByIdAsync(int id)
        {
            var categoryBrand = await _repository.GetById(id);
            return mapper.Map<CategoryBrandDto>(categoryBrand);
        }

        public async Task<IEnumerable<CategoryBrandDto>> GetAllAsync()
        {
            var categoryBrand = await _repository.GetAll(i => i.Id, false);
            return mapper.Map<List<CategoryBrandDto>>(categoryBrand);
        }

        public async Task<BaseResponse<CategoryBrandDto>> CreateAsync(CategoryBrandDto categoryBrandDto)
        {
            var response = new BaseResponse<CategoryBrandDto>();

            var categoryBrand = mapper.Map<CategoryBrand>(categoryBrandDto);
            categoryBrand = await _repository.Add(categoryBrand);

            var categoryBrandDtoCreated = mapper.Map<CategoryBrandDto>(categoryBrand);
            response.Success = true;
            response.Message = "CategoryBrand created successfully";
            response.Data = categoryBrandDtoCreated;
            return response;
        }

        public async Task UpdateAsync(CategoryBrandDto categoryBrandDto)
        {
            var categoryBrand = mapper.Map<CategoryBrand>(categoryBrandDto);
            await _repository.Update(categoryBrand);
        }

        public async Task DeleteAsync(int id)
        {
            var CategoryBrand = await _repository.GetById(id);
            if (CategoryBrand != null)
            {
                await _repository.Remove(CategoryBrand);
            }
        }
    }
}
