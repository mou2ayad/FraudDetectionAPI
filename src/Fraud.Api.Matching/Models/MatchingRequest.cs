using Fraud.Component.Common.Models;

namespace Fraud.Api.Matching.Models
{
    public class MatchingRequest
    {
        public Person First { set; get; }
        public Person Second { set; get; }
    }
}
