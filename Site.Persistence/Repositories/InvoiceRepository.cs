using Site.Application.Definitions.Contracts.Repositories;
using Site.Persistence.Contexts.ApplicationContext;

namespace Site.Persistence.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvoiceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
