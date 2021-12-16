using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FRISS.Components.Utilities.APIKey
{
    public class APIKeyValidator : IAPIKeyValidator
    {
        private readonly IConfiguration _config;
        private readonly ILogger<APIKeyValidator> _logger;
        public APIKeyValidator(IConfiguration config, ILogger<APIKeyValidator> logger)
        {
            _config = config;
            _logger = logger;
        }
        public bool ValidateAPIKey(HttpContext httpContext, string keyName)
        {
            string keyValue=_config.GetValue<string>(string.Format("APIKeys:{0}", keyName));
            if (string.IsNullOrEmpty(keyValue))
                throw new Exception(string.Format("API key {0} is not available in Configurations!!!", keyName));
            if (httpContext.Request.Headers.ContainsKey(keyName) && httpContext.Request.Headers[keyName] == keyValue)
                return true;

            if (!httpContext.Request.Headers.ContainsKey(keyName))
                _logger.LogInformation(string.Format("Unauthorized access, API Key {0} is not found",keyValue));

            else if (httpContext.Request.Headers[keyName] != keyValue)
                _logger.LogInformation("Unauthorized access, invalid key:\nKey=> {0}\nValue=> {1}", keyName, keyValue);

            httpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;

            return false;

        }

    }
}
