using FRISS.NamesSimilarity.Models;

namespace FRISS.NamesSimilarity.Contracts
{
    public interface INameSimilarity
    {
        bool Match(string name, string anotherName);
        SimilarityServiceType Type { get; }
    }
}
