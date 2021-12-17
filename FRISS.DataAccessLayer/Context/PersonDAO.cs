using System;

namespace FRISS.DataAccessLayer.Context
{
    public class PersonDAO
    {
        public string Id { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime? DateOfBirth { set; get; }
        public string IdentificationNumber { set; get; }
        public DateTime CreationDate { set; get; }
    }
}
