using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FRISS.Common.Models;
using FRISS.DataAccessLayer.Context;
using FRISS.DataAccessLayer.Contracts;
using FRISS.DataAccessLayer.Profiles;
using FRISS.DataAccessLayer.Services;
using FRISS.Fraud.Test.Utilities;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace FRISS.Fraud.Test.DataAccessLayer
{
    public class PersonsRepositoryShould
    {
        [Test]
        public async Task Store_person_into_database()
        {
            var logger = CreateLogger();
            var storage = FakeFraudStorage.Create();
            var sut = Sut(storage, logger);
            var person = CreatePerson();

            var id=await sut.AddPerson(person);

            id.Should().NotBeNull();
            storage.Db.Any(e => e.Id == id).Should().BeTrue();
            logger.AddedLogLevel.Should().Be(LogLevel.Information);
            logger.LoggedMessage.Should().StartWith($"A new Person has been inserted to DB with Id {id}");
        }

        [Test]
        public async Task Not_send_null_person_to_storage()
        {
            var logger = CreateLogger();
            var storage = FakeFraudStorage.Create();
            var sut = Sut(storage, logger);

            var id = await sut.AddPerson(null);

            id.Should().BeNull();
            storage.Db.Any(e => e.Id == id).Should().BeFalse();
            logger.AddedLogLevel.Should().Be(LogLevel.Warning);
            logger.LoggedMessage.Should().StartWith("Can't insert person with null value into db");
        }

        [Test]
        public async Task Return_added_data_from_storage_if_exists_in_db()
        {
            string personId = "123";
            var logger = CreateLogger();
            var personDao = CreatePersonDao(personId);
            var expectedPerson = CreatePerson();
            var storage = FakeFraudStorage.Create().With(personDao);
            var sut = Sut(storage, logger);
            

            var person = await sut.GetPersonById(personId);

            person.Should().NotBeNull();
            person.Should().Be(expectedPerson);
            logger.IsLogAdded.Should().BeFalse();
        }

        [Test]
        public async Task Return_null_if_not_exists_in_db()
        {
            string personId = "123";
            var logger = CreateLogger();
            var storage = FakeFraudStorage.Create();
            var sut = Sut(storage, logger);

            var person = await sut.GetPersonById(personId);

            person.Should().BeNull();
            logger.IsLogAdded.Should().BeFalse();
        }

        [Test]
        public async Task Return_null_and_log_warning_when_get_person_by_null_id()
        {
            var logger = CreateLogger();
            var storage = FakeFraudStorage.Create();
            var sut = Sut(storage, logger);

            var person = await sut.GetPersonById(null);

            person.Should().BeNull();
            logger.AddedLogLevel.Should().Be(LogLevel.Warning);
            logger.LoggedMessage.Should().Be("Can't get person with Id = null");
        }

        private static PersonsRepository Sut(IFraudStorage storage,ILogger<PersonsRepository> logger) => new(storage, CreateMapper(), logger);

        private static IMapper CreateMapper()
            => new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PersonProfile());
            }).CreateMapper();
        

        private static FakeLogger<PersonsRepository> CreateLogger() => FakeLogger<PersonsRepository>.Create();

        private static Person CreatePerson()=>
            new("Andrew", "Craw", new DateTime(1985, 2, 20), "931212312");

        private static PersonDAO CreatePersonDao(string id) => new()
        {
            FirstName = "Andrew", LastName = "Craw", DateOfBirth = new DateTime(1985, 2, 20),
            CreationDate = DateTime.UtcNow, Id = id, IdentificationNumber = "931212312"
        };

    }
}
