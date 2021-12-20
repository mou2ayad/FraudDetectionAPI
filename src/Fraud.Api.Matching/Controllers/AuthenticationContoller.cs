using Fraud.Component.Utilities.JWT_Auth;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Fraud.Api.Matching.Controllers
{   
    [ApiController]   
    public class AuthenticationContoller : ControllerBase
    {       
        [HttpPost("api/authentication/token")]
        [SwaggerOperation(
            Summary = "Authentication",
            Description = "Obtain Auth Token to use in calling Fraud Api endpoints.\n\n for local testing you can use ***admin*** as UserName and Password.",
            OperationId = "AuthenticationRequest")]
        public IActionResult Token([FromServices]IUserService userService,AuthenticateRequest model)
        {
            var response = userService.Authenticate(model);

            if (response == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
       
    }
}
