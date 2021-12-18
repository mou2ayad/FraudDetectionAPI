using System.Threading.Tasks;
using FRISS.Common.Models;

namespace FRISS.DataAccessLayer.Contracts
{
    public interface IPersonsRepository
    {
        Task<string> AddPerson(Person person);
        Task<Person> GetPersonById(string id);
    }
}