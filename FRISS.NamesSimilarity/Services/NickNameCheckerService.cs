using Fraud.Component.Matching.Contracts;
using Fraud.Component.Matching.Database;
using Fraud.Component.Matching.Models;

namespace Fraud.Component.Matching.Services
{
    public class NickNameDetectorService : INameSimilarity
    {
        public SimilarityServiceType Type => SimilarityServiceType.NickName;

        private bool IsNickName(string name, string anotherName) =>
            CommonNicknameDb.Db.Contains(SimilarNamesPair.Create(name, anotherName));

        public bool Match(string name, string anotherName)
            => IsNickName(name, anotherName);
    }
}
