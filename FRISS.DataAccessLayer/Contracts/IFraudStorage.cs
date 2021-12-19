using System.Threading.Tasks;
using Fraud.Component.DataAccessLayer.Context;

namespace Fraud.Component.DataAccessLayer.Contracts
{
    public interface IFraudStorage
    {
        Task<string> InsertPerson(PersonDao data);

        Task<PersonDao> GetPersonById(string id);
    }
}
