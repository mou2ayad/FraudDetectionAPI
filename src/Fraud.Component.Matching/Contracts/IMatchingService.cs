using System.Threading.Tasks;
using Fraud.Component.Common.Contracts;

namespace Fraud.Component.Matching.Contracts
{
    public interface IMatchingService<in T> where T : IMatchable<T>
    { 
        Task<decimal> Match(T first, T second);
    }
}