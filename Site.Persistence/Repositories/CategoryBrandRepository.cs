using Microsoft.EntityFrameworkCore;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Persistence.Contexts.ApplicationContext;

namespace Site.Persistence.Repositories
{
    public class CategoryBrandRepository : GenericRepository<CategoryBrand>, ICategoryBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryBrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CategoryBrand>> GetAllocationsWithDetails()
        {
            var leaveAllocations = await _dbContext.CategoryBrands
                //.Include(t => t.Products)
                .ToListAsync();
            return leaveAllocations;
        }

        public async Task<CategoryBrand> GetAllocationWithDetails(int id)
        {
            var leaveAllocation = await _dbContext.CategoryBrands
               //.Include(t => t.Products)
               .FirstOrDefaultAsync(l => l.Id == id);
            return leaveAllocation;
        }
    }
}
