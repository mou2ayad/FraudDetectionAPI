namespace FRISS.DataAccessLayer.Contracts
{
    public interface IPersonsDbClient
    {
        string InsertPerson(PersonDAO data);

        PersonDAO GetPersonById(string id);

    }
}
