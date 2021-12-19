using System.Threading.Tasks;
using Fraud.Component.Common.Models;

namespace Fraud.Component.DataAccessLayer.Contracts
{
    public interface IPersonsRepository
    {
        Task<string> AddPerson(Person person);
        Task<Person> GetPersonById(string id);
    }
}