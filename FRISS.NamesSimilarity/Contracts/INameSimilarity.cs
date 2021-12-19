using Fraud.Component.Matching.Models;

namespace Fraud.Component.Matching.Contracts
{
    public interface INameSimilarity
    {
        bool Match(string name, string anotherName);
        SimilarityServiceType Type { get; }
    }
}
