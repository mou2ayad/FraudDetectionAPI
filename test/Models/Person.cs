using System;

namespace FRISS.Models
{
    public class Person
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public string IdentificationNumber { set; get; }
    }
}
