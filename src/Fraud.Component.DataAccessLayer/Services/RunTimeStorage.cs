using System;
using System.Threading.Tasks;
using Fraud.Component.DataAccessLayer.Context;
using Fraud.Component.DataAccessLayer.Contracts;
using Fraud.Component.DataAccessLayer.Database;

namespace Fraud.Component.DataAccessLayer.Services
{
    public class RunTimeStorage : IFraudStorage
    {
        public Task<string> InsertPerson(PersonDao data)
        {
            data.Id = Guid.NewGuid().ToString();
            RunTimePersonsDatabase.Db.Add(data.Id,data);
            return Task.FromResult(data.Id);
        }

        public Task<PersonDao> GetPersonById(string id)
        {
            return Task.FromResult(RunTimePersonsDatabase.Db.TryGetValue(id, out PersonDao result) ? result : null);
        }
    }
}
