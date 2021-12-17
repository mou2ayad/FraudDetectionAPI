using Microsoft.EntityFrameworkCore;

namespace FRISS.DataAccessLayer.Context
{
    public class FraudDbContext : DbContext
    {
        public FraudDbContext(DbContextOptions<FraudDbContext> options)
            : base(options)
        {
        }

        public DbSet<PersonDAO> Persons { get; set; }
    }
}
