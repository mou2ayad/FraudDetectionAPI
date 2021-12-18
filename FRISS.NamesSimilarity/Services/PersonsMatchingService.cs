using System;
using System.Collections.Generic;
using System.Text;
using FRISS.Common.Models;
using FRISS.NamesSimilarity.Configuration;
using FRISS.NamesSimilarity.Models;

namespace FRISS.NamesSimilarity.Services
{
    public class PersonsMatchingService
    {
        public int Match<T>(T first, T second) where T :IMatchable
        {
            
            var mr = MatchingRules.Get(nameof(firstPerson.FirstName));
            

        }
    }
}
