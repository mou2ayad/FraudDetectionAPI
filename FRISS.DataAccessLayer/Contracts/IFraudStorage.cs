using System.Threading.Tasks;
using FRISS.DataAccessLayer.Context;

namespace FRISS.DataAccessLayer.Contracts
{
    public interface IFraudStorage
    {
        Task<string> InsertPerson(PersonDAO data);

        Task<PersonDAO> GetPersonById(string id);

    }
}
