namespace FRISS.NamesSimilarity.Models
{
    public class SimilarityRule
    {
        private SimilarityRule(SimilarityServiceType similarityType, decimal similarityScore)
        {
            SimilarityType = similarityType;
            SimilarityScore = similarityScore;
        }

        public static SimilarityRule From(SimilarityServiceType similarityType, decimal similarityScore) => 
            new SimilarityRule(similarityType, similarityScore);

        public SimilarityServiceType SimilarityType { set; get; }
        public decimal SimilarityScore { set; get; }
    }
}