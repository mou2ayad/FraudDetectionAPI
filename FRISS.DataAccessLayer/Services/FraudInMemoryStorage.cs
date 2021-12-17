using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FRISS.DataAccessLayer.Context;
using FRISS.DataAccessLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FRISS.DataAccessLayer.Services
{
    public class FraudInMemoryStorage :IFraudStorage
    {
        private readonly IDesignTimeDbContextFactory<FraudDbContext> _dbContextFactory;

        public FraudInMemoryStorage(IDesignTimeDbContextFactory<FraudDbContext> dbContextFactory) => 
            _dbContextFactory = dbContextFactory;

        private FraudDbContext GetContext() => _dbContextFactory.CreateDbContext(null);

        public Task<string> InsertPerson(PersonDAO data)
        {
            data.Id = Guid.NewGuid().ToString();
            using var context = GetContext();
            var result=context.Persons.Add(data);
            return Task.FromResult(result.Entity.Id);
        }

        public Task<PersonDAO> GetPersonById(string id)
        {
            using var context = GetContext();
            return context.Persons.FirstOrDefaultAsync(person => person.Id == id);
        }
    }
}
