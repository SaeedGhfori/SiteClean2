namespace Site.Application.Definitions.Contracts.Repositories
{
    public interface ICategoryBrandRepository : IGenericRepository<CategoryBrand>
    {
        Task<List<CategoryBrand>> GetAllocationsWithDetails();
        Task<CategoryBrand> GetAllocationWithDetails(int id);
    }
}
