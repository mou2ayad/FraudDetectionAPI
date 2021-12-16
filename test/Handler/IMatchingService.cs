using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FRISS.Common.Models;

namespace Friss.Handler
{
    public interface IMatchingService
    {
        int Match(Person firstPerson, Person secondPerson);
    }

    public class IdentificationNumberMatchingService : IMatchingService
    {
        public int Match(Person firstPerson, Person secondPerson)
            => firstPerson.IdentificationNumber == secondPerson.IdentificationNumber ? 100 : 0;
    }

    public class LastNumberMatchingService : IMatchingService
    {
        public int Match(Person firstPerson, Person secondPerson)
            => firstPerson.LastName.Equals(secondPerson.LastName) ? 40 : 0;
    }
}
