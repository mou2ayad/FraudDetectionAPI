using System;

namespace FRISS.Common.Models
{
    public class Person :IEquatable<Person>
    {
        //internal Person()
        //{
        //}

        public Person(string firstName,string lastName, DateTime? dateOfBirth=null,string identificationNumber=null)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            IdentificationNumber = identificationNumber;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime? DateOfBirth { get; }
        public string IdentificationNumber { get; }

        public bool Equals(Person other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FirstName == other.FirstName && LastName == other.LastName &&
                   Nullable.Equals(DateOfBirth, other.DateOfBirth) &&
                   IdentificationNumber == other.IdentificationNumber;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Person) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, DateOfBirth, IdentificationNumber);
        }
    }
}
