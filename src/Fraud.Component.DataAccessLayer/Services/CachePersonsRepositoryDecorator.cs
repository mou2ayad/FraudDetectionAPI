using System;
using System.Threading.Tasks;
using Fraud.Component.Common.Models;
using Fraud.Component.DataAccessLayer.Config;
using Fraud.Component.DataAccessLayer.Contracts;
using Fraud.Component.Utilities.Cache;
using Microsoft.Extensions.Options;

namespace Fraud.Component.DataAccessLayer.Services
{
    public class CachePersonsRepositoryDecorator : IPersonsRepository
    {
        private readonly IPersonsRepository _personsRepository;
        private readonly ICache _cache;
        private readonly StorageConfig _config;

        public CachePersonsRepositoryDecorator(IPersonsRepository personsRepository, ICache cache,
            IOptions<StorageConfig> options)
        {
            _personsRepository = personsRepository;
            _cache = cache;
            _config = options.Value;
        }

        public async Task<string> AddPerson(Person person)
        {
            var personId = await _personsRepository.AddPerson(person);
            await SetCache(personId, person);
            return personId;
        }

        public async Task<Person> GetPersonById(string id)
        {
            var person = await _cache.Get<Person>(GetCacheKey(id));
            if (person == null)
            {
                person = await _personsRepository.GetPersonById(id);
                await SetCache(id, person);
            }

            return person;
        }

        private string GetCacheKey(string personId) => $"person_{personId}";

        private Task SetCache(string personId, Person person) => _cache.Set(GetCacheKey(personId), person,
            TimeSpan.FromMinutes(_config.ExpireAfterInMinutes));
    }
}
