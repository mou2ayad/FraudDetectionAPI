using System;
using Fraud.Api.Matching.Models;
using Fraud.Component.Common.Models;

namespace Fraud.Api.Matching.Utils.Swagger
{
    public class SwaggerExamples
    {
        public static CreatePersonRequest CreatePersonRequestExample() => new CreatePersonRequest()
        {
            DateOfBirth = DateTime.Parse("1985-02-20"),
            FirstName = "Andrew",
            LastName = "Craw",
            IdentificationNumber = "931212312"
        };

        public static CreatePersonResponse CreatePersonResponseExample() => new CreatePersonResponse()
        {
            PersonId = Guid.NewGuid().ToString()
        };

        public static GetPersonResponse GetPersonResponseExample() => new GetPersonResponse()
        {
            DateOfBirth = DateTime.Parse("1985-02-20"),
            FirstName = "Andrew",
            LastName = "Craw",
            IdentificationNumber = "931212312"
        };

        public static MatchingRequest MatchRequestExample() => new MatchingRequest()
        {
            First = new Person("Andrew", "Craw", DateTime.Parse("1985-02-20"), "931212312"),
            Second = new Person("Andy", "Smith", DateTime.Parse("1985-02-20"), "931212311"),
        };

        public static MatchingResponse MatchResponseExample() => new MatchingResponse()
        {
            MatchingScore = 65
        };
    }
}
