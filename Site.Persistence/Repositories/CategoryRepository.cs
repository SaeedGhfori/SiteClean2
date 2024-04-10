using Microsoft.EntityFrameworkCore;
using Site.Application.Definitions.Contracts.Repositories;
using Site.Persistence.Contexts.ApplicationContext;

namespace Site.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeApprovalStatus(Category leaveRequest, bool? approvalStatus)
        {
            //leaveRequest.Name = approvalStatus;
            _dbContext.Entry(leaveRequest).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategoryRequestsWithDetails()
        {
            var leaveRequests = await _dbContext.Categories
                //.Include(t => t.Products)
                .ToListAsync();
            return leaveRequests;
        }
    }
}
