using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Definitions.Contracts.Services.Categorys
{
    public interface ICategoryBrandService
    {
        Task<CategoryBrandDto> GetByIdAsync(int id);
        Task<IEnumerable<CategoryBrandDto>> GetAllAsync();
        Task<BaseResponse<CategoryBrandDto>> CreateAsync(CategoryBrandDto categoryBrandDto);
        Task UpdateAsync(CategoryBrandDto categoryBrandDto);
        Task DeleteAsync(int id);
    }
}
