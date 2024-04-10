namespace Site.Application.Definitions.Contracts.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task ChangeApprovalStatus(Category leaveRequest, bool? approvalStatus);
        Task<List<Category>> GetCategoryRequestsWithDetails();
    }
}
