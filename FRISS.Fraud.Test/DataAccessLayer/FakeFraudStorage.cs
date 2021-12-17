using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FRISS.DataAccessLayer.Context;
using FRISS.DataAccessLayer.Contracts;

namespace FRISS.Fraud.Test.DataAccessLayer
{
    public class FakeFraudStorage : IFraudStorage
    {
        private FakeFraudStorage() { }

        public static FakeFraudStorage Create() => new();

        public List<PersonDAO> Db = new();
        public Task<string> InsertPerson(PersonDAO data)
        {
            data.Id = "123456789";
            Db.Add(data);
            return Task.FromResult(data.Id);
        }

        public Task<PersonDAO> GetPersonById(string id)
            => Task.FromResult(Db.FirstOrDefault(e => e.Id == id));

        public FakeFraudStorage With(PersonDAO personDao)
        {
            Db.Add(personDao);
            return this;
        } 
    }
}