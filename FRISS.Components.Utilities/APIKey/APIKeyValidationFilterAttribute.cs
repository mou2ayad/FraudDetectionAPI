using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FRISS.Components.Utilities.APIKey
{
    public class APIKeyValidationFilterAttribute : ActionFilterAttribute
    {
        private string _keyName;
        public APIKeyValidationFilterAttribute(string keyName="APIKey")
        {
            _keyName = keyName;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {            
            var apiService = context.HttpContext.RequestServices.GetService(typeof(IAPIKeyValidator)) as APIKeyValidator;
            if(!apiService.ValidateAPIKey(context.HttpContext, _keyName))
            {
                context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                context.HttpContext.Response.ContentType = "text/json";
                context.Result = new UnauthorizedObjectResult(new { ErrorMessages = "Unauthorized Access!!" });
            }
            else
                base.OnActionExecuting(context);
        }
    }
}
