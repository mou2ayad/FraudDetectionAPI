using AutoMapper;
using FRISS.Common.Models;
using FRISS.DataAccessLayer.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FRISS.DataAccessLayer.Services
{
    public class PersonsRepository
    {
        private readonly IPersonsDbClient _personsDbClient;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonsRepository> _logger;
        public PersonsRepository(IPersonsDbClient personsDbClient, IMapper mapper, ILogger<PersonsRepository> logger)
        {
            _personsDbClient = personsDbClient;
            _mapper = mapper;
            _logger = logger;
        }

        public bool AddPerson(Person person)
        {
            if (person == null)
            {
                _logger.LogWarning("Can't insert person with null value to db");
                return false;
            }
            var personDAO=_mapper.Map<PersonDAO>(person);
            var id=_personsDbClient.InsertPerson(personDAO);
            _logger.LogInformation("A new Person has been inserted to DB with Id {0} and data: [{1}]", id,
                JsonConvert.SerializeObject(person));
            return true;
        }

    }
}
