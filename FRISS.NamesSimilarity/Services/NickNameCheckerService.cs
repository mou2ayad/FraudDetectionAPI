using FRISS.NamesSimilarity.Contracts;
using FRISS.NamesSimilarity.Database;
using FRISS.NamesSimilarity.Models;

namespace FRISS.NamesSimilarity.Services
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
