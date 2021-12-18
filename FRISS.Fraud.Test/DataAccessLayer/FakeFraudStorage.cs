using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FRISS.DataAccessLayer.Context;
using FRISS.DataAccessLayer.Contracts;

namespace FRISS.Fraud.Test.DataAccessLayer
{
    public class FakeFraudStorage : IFraudStorage
    {
        public bool CallGetPerson { private set; get; }

        public bool CallInsertPerson { private set; get; }

        protected FakeFraudStorage() { }

        public static FakeFraudStorage Create() => new();

        public List<PersonDAO> Db = new();
        public virtual Task<string> InsertPerson(PersonDAO data)
        {
            data.Id = "123456789";
            Db.Add(data);
            CallInsertPerson = true;
            return Task.FromResult(data.Id);
        }

        public virtual Task<PersonDAO> GetPersonById(string id)
        {
            CallGetPerson = true;
            return Task.FromResult(Db.FirstOrDefault(e => e.Id == id));
        }
        

        public FakeFraudStorage With(PersonDAO personDao)
        {
            Db.Add(personDao);
            return this;
        } 
    }

    public class SpyFraudStorage : FakeFraudStorage
    {
        public bool IsGetPersonByIdCalled { private set; get; }

        public bool IsInsertPersonCalled { private set; get; }
    }
}