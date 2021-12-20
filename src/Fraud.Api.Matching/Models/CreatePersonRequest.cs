using System;
using Fraud.Component.Common.Models;
using Fraud.Component.Utilities.ErrorHandling;

namespace Fraud.Api.Matching.Models
{
    public class CreatePersonRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdentificationNumber { get; set; }

        private void Validate()
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                throw new InvalidRequestException("FirstName and LastName can't be null");
        }

        public Person ToPerson()
        {
            Validate();
            return new Person(FirstName, LastName, DateOfBirth, IdentificationNumber);
        }
    }
}
