using System;

namespace Fraud.Api.Matching.Models
{
    public class GetPersonResponse
    {
        public string Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public string IdentificationNumber { set; get; }
    }
}
