using Microsoft.AspNetCore.Http;

namespace FRISS.Components.Utilities.APIKey
{
    public interface IAPIKeyValidator
    {
        bool ValidateAPIKey(HttpContext httpContext, string key);
    }
}