using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Fraud.Component.Common.Models;
using Fraud.Component.DataAccessLayer.Config;
using Fraud.Component.DataAccessLayer.Context;
using Fraud.Component.DataAccessLayer.Contracts;
using Fraud.Component.DataAccessLayer.Profiles;
using Fraud.Component.DataAccessLayer.Services;
using Fraud.Component.Utilities.Cache;
using Fraud.Test.Utilities;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Fraud.Test.DataAccessLayer
{
    public class CachePersonsRepositoryDecoratorShould
    {
        [Test]
        public async Task Cache_person_when_adding_to_the_storage()
        {
            var cache = FakeCache.Create();
            int expirationAfter = 5;
            var storage = FakeFraudStorage.Create();
            var repo = CreatePersonsRepository(storage);
            var cacheConfigOptions = CreateOptions(expirationAfter);
            var person = CreatePerson();
            var sut = Sut(repo, cache, cacheConfigOptions);

            var id= await sut.AddPerson(person);
            string cacheKey = GetCacheKey(id);
            var valueFromCache = await cache.Get<Person>(cacheKey);
            var valueFromStorage = await repo.GetPersonById(id);

            cache.Key.Should().Be(cacheKey);
            valueFromCache.Should().Be(person);
            cache.Expiration.Should().Be(TimeSpan.FromMinutes(expirationAfter));
            valueFromStorage.Should().Be(valueFromCache);
            storage.CallInsertPerson.Should().BeTrue();
        }

        [Test]
        public async Task Cache_person_after_getting_from_repository_if_not_exist_in_cache()
        {
            var cache = FakeCache.Create();
            string personIdInDb = "123456";
            var personDao = CreatePersonDao(personIdInDb);
            var storage = FakeFraudStorage.Create().With(personDao);
            int expirationAfter = 5;
            var repo = CreatePersonsRepository(storage);
            var cacheConfigOptions = CreateOptions(expirationAfter);
            var sut = Sut(repo, cache, cacheConfigOptions);

            var person = await sut.GetPersonById(personIdInDb);
            string cacheKey = GetCacheKey(personIdInDb);
            var valueFromCache = await cache.Get<Person>(cacheKey);

            cache.Key.Should().Be(cacheKey);
            valueFromCache.Should().Be(person);
            cache.Expiration.Should().Be(TimeSpan.FromMinutes(expirationAfter));
            storage.CallGetPerson.Should().BeTrue();
        }

        [Test]
        public async Task Get_person_from_cache_if_exists_without_calling_persons_repository()
        {
            string personIdInDb = "123456";
            string cacheKey = GetCacheKey(personIdInDb);
            var personInCache = CreatePerson();
            var cache = FakeCache.Create().With(cacheKey, personInCache,5);
            var storage = FakeFraudStorage.Create();
            var sut = Sut(CreatePersonsRepository(storage), cache, CreateOptions(5));

            var result = await sut.GetPersonById(personIdInDb);

            result.Should().Be(personInCache);
            storage.CallGetPerson.Should().BeFalse();
        }

        private static CachePersonsRepositoryDecorator Sut(IPersonsRepository repository, ICache cache, IOptions<StorageConfig> configOptions) =>
            new (repository, cache, configOptions);

        private static IOptions<StorageConfig> CreateOptions(int expireAfterInMinutes) =>
            Options.Create(new StorageConfig {ExpireAfterInMinutes = expireAfterInMinutes});
        private static PersonsRepository CreatePersonsRepository(IFraudStorage storage) => new(storage, CreateMapper(), CreateLogger());

        private static IMapper CreateMapper()
            => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PersonProfile());
            }).CreateMapper();


        private static FakeLogger<PersonsRepository> CreateLogger() => FakeLogger<PersonsRepository>.Create();

        private static Person CreatePerson() =>
            new("Andrew", "Craw", new DateTime(1985, 2, 20), "931212312");

        private static PersonDao CreatePersonDao(string id) => new()
        {
            FirstName = "Andrew",
            LastName = "Craw",
            DateOfBirth = new DateTime(1985, 2, 20),
            CreationDate = DateTime.UtcNow,
            Id = id,
            IdentificationNumber = "931212312"
        };

        private string GetCacheKey(string personId) => $"person_{personId}";

    }
}
