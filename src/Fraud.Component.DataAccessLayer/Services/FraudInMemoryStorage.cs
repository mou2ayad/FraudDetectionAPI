using System;
using System.Threading.Tasks;
using Fraud.Component.DataAccessLayer.Context;
using Fraud.Component.DataAccessLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fraud.Component.DataAccessLayer.Services
{
    public class FraudInMemoryStorage :IFraudStorage
    {
        private readonly IDesignTimeDbContextFactory<FraudDbContext> _dbContextFactory;

        public FraudInMemoryStorage(IDesignTimeDbContextFactory<FraudDbContext> dbContextFactory) => 
            _dbContextFactory = dbContextFactory;

        private FraudDbContext GetContext() => _dbContextFactory.CreateDbContext(null);

        public Task<string> InsertPerson(PersonDao data)
        {
            data.Id = Guid.NewGuid().ToString();
            using var context = GetContext();
            var result=context.Persons.Add(data);
            return Task.FromResult(result.Entity.Id);
        }

        public Task<PersonDao> GetPersonById(string id)
        {
            using var context = GetContext();
            return context.Persons.FirstOrDefaultAsync(person => person.Id == id);
        }
    }
}
