using System.Collections.Generic;

namespace FRISS.NamesSimilarity.Models
{
    public static class MatchingRules
    {
        private static Dictionary<string,MatchingRule> _rules = new Dictionary<string,MatchingRule>();

        public static void Set(MatchingRule rule)
        {
            if (_rules.TryAdd(rule.AttributeName, rule))
                _rules[rule.AttributeName] = rule;
        }

        public static MatchingRule Get(string ruleName)
        {
            if (_rules.TryGetValue(ruleName, out MatchingRule value))
                return value;
            return null;
        } 



        //{
        //    MatchingRule.From("FirstName", 20).With(
        //        SimilarityRule.From(SimilarityServiceType.Initials,15),
        //        SimilarityRule.From(SimilarityServiceType.NickName,15),
        //        SimilarityRule.From(SimilarityServiceType.Typo,15)
        //    ),
        //    MatchingRule.From("LastName", 40),
        //    MatchingRule.From("DateOfBirth", 40),
        //    MatchingRule.From("IdentificationNumber", 100),
        //};
    }
    public class MatchingRule
    {
        private MatchingRule(string attributeName, decimal matchingScore)
        {
            AttributeName = attributeName;
            MatchingScore = matchingScore;
        }

        public static MatchingRule From(string attributeName, decimal matchingScore) 
            => new MatchingRule(attributeName, matchingScore);

        public MatchingRule With(params SimilarityRule[] similarityRules)
        {
            SimilarityRules = similarityRules;
            return this;
        }
        public string AttributeName { set; get; }
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
