using System;
using System.Threading.Tasks;

namespace FRISS.Components.Utilities.Cache
{
    public interface ICache
    {
        Task Set<T>(string key, T value, TimeSpan expirationTime);
        Task<T> Get<T>(string key);
    }
}