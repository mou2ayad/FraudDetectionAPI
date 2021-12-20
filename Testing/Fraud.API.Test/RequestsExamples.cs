using System;
using Fraud.Api.Matching.Models;
using Fraud.Component.Common.Models;

namespace Fraud.API.Test
{
    public class RequestsExamples
    {
        public static CreatePersonRequest CreatePersonRequestExample() => new ()
        {
            DateOfBirth = DateTime.Parse("1985-02-20"),
            FirstName = "Andrew",
            LastName = "Craw",
            IdentificationNumber = "931212312"
        };
        

        public static MatchingRequest MatchRequestExample() => new ()
        {
            First = new Person("Andrew", "Craw", DateTime.Parse("1985-02-20"), "931212312"),
            Second = new Person("Andy", "Smith", DateTime.Parse("1985-02-20"), "931212311"),
        };

    }
}
