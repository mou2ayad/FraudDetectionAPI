using System.Collections.Generic;

namespace FRISS.NamesSimilarity.Models
{
    public static class MatchingRules
    {
        private static Dictionary<string,MatchingRule> _rules = new Dictionary<string,MatchingRule>();

        public static void Set(params MatchingRule[] matchingRule)
        {
            if(matchingRule==null) return;
            foreach (var rule in matchingRule)
                Set(rule);
        }
        public static void Set(MatchingRule rule)
        {
            if (_rules.TryAdd(rule.PropertyName, rule))
                _rules[rule.PropertyName] = rule;
        }

        public static MatchingRule Get(string propertyName)
        {
            if (_rules.TryGetValue(propertyName, out MatchingRule value))
                return value;
            return null;
        } 


    }
    public class MatchingRule
    {
        private MatchingRule(string propertyName, decimal matchingScore)
        {
            PropertyName = propertyName;
            MatchingScore = matchingScore;
        }

        public static MatchingRule From(string propertyName, decimal matchingScore) 
            => new MatchingRule(propertyName, matchingScore);

        public MatchingRule With(params SimilarityRule[] similarityRules)
        {
            SimilarityRules = similarityRules;
            return this;
        }
        public string PropertyName { set; get; }
        public decimal MatchingScore { set; get; }

        public IEnumerable<SimilarityRule> SimilarityRules { set; get; }
    }

    public class SimilarityRule
    {
        private SimilarityRule(SimilarityServiceType similarityType, decimal similarityScore)
        {
            SimilarityType = similarityType;
            IsEnabled = true;
            SimilarityScore = similarityScore;
        }

        public static SimilarityRule From(SimilarityServiceType similarityType, decimal similarityScore) => 
            new SimilarityRule(similarityType, similarityScore);

        public SimilarityServiceType SimilarityType { set; get; }
        public bool IsEnabled { set; get; } 
        public decimal SimilarityScore { set; get; }

    }
}
