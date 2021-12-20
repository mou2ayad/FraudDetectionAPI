using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fraud.Component.DataAccessLayer.Context;
using Fraud.Component.DataAccessLayer.Contracts;

namespace Fraud.Test.DataAccessLayer
{
    public class FakeFraudStorage : IFraudStorage
    {
        public bool CallGetPerson { private set; get; }

        public bool CallInsertPerson { private set; get; }

        protected FakeFraudStorage() { }

        public static FakeFraudStorage Create() => new();

        public List<PersonDao> Db = new();
        public virtual Task<string> InsertPerson(PersonDao data)
        {
            data.Id = "123456789";
            Db.Add(data);
            CallInsertPerson = true;
            return Task.FromResult(data.Id);
        }

        public virtual Task<PersonDao> GetPersonById(string id)
        {
            CallGetPerson = true;
            return Task.FromResult(Db.FirstOrDefault(e => e.Id == id));
        }

        public FakeFraudStorage With(PersonDao personDao)
        {
            Db.Add(personDao);
            return this;
        } 
    }

}