using Microsoft.EntityFrameworkCore;

namespace Fraud.Component.DataAccessLayer.Context
{
    public class FraudDbContext : DbContext
    {
        public FraudDbContext(DbContextOptions<FraudDbContext> options)
            : base(options)
        {
        }

        public DbSet<PersonDao> Persons { get; set; }
    }
}
