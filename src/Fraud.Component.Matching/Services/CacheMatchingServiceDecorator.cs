using System;
using System.Threading.Tasks;
using Fraud.Component.Common.Contracts;
using Fraud.Component.Matching.Configuration;
using Fraud.Component.Matching.Contracts;
using Fraud.Component.Utilities.Cache;
using Microsoft.Extensions.Options;

namespace Fraud.Component.Matching.Services
{
    public class CacheMatchingServiceDecorator<T> : IMatchingService<T> where T : IMatchable<T> , ICacheable
    {
        private readonly MatchingService<T> _matchingService;
        private readonly ICache _cache;
        private readonly MatchingConfig _config;
        public CacheMatchingServiceDecorator(MatchingService<T> matchingService, ICache cache,IOptions<MatchingConfig> options)
        {
            _matchingService = matchingService;
            _cache = cache;
            _config = options.Value;
        }

        public async Task<decimal> Match(T first, T second)
        {
            var firstKey = GetCacheKey(first, second);
            decimal? matchingScore = await _cache.Get<decimal?>(firstKey);
            if (!matchingScore.HasValue)
            {
                var secondKey = GetCacheKey(second, first);
                matchingScore = await _cache.Get<decimal?>(secondKey);
                if (!matchingScore.HasValue)
                {
                    matchingScore = await _matchingService.Match(first, second);
                    await _cache.Set(firstKey, matchingScore, TimeSpan.FromMinutes(_config.ExpireAfterInMinutes));
                }
            }
            return await Task.FromResult(matchingScore.Value);

        }

        private string GetCacheKey(T first, T second)
            => $"{first.GetCacheKey()}__{second.GetCacheKey()}";

    }
}
