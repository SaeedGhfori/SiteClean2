using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Definitions.Contracts.Services.Categorys
{
    public interface ICategoryService
    {
        Task<CategoryDto> GetByIdAsync(int id);
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<BaseResponse<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task UpdateAsync(CategoryDto categoryDto);
        Task DeleteAsync(int id);
    }
}
