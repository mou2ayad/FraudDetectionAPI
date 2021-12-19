using System;
using System.Threading.Tasks;
using AutoMapper;
using Fraud.Component.Common.Models;
using Fraud.Component.DataAccessLayer.Context;
using Fraud.Component.DataAccessLayer.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Fraud.Component.DataAccessLayer.Services
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly IFraudStorage _fraudStorage;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonsRepository> _logger;
        public PersonsRepository(IFraudStorage fraudStorage , IMapper mapper, ILogger<PersonsRepository> logger)
        {
            _fraudStorage = fraudStorage;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> AddPerson(Person person)
        {
            if (person == null)
            {
                _logger.LogWarning("Can't insert person with null value into db");
                return null;
            }
            var personDAO=_mapper.Map<PersonDao>(person);
            personDAO.CreationDate=DateTime.UtcNow;
            var id=await _fraudStorage.InsertPerson(personDAO);
            _logger.LogInformation("A new Person has been inserted to DB with Id {0} and data: [{1}]", id,
                JsonConvert.SerializeObject(person));
            return id;
        }

        public async Task<Person> GetPersonById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Can't get person with Id = null");
                return null;
            }
            var personDao= await _fraudStorage.GetPersonById(id);
            return _mapper.Map<Person>(personDao);
        }

    }
}
