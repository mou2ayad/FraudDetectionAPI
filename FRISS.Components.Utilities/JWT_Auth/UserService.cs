using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FRISS.Components.Utilities.JWT_Auth
{

    public abstract class UserService : IUserService
    {
        private readonly JwtSettings _jwtSettings;

        protected UserService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public abstract UserClient Login(string userName,string password);
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            
            var user = Login(model.Username, model.Password);
            if (user == null) return null;

            var token = GenerateJwtToken(user, out var tokenExpiryDate);

            return new AuthenticateResponse(token, tokenExpiryDate);
        }
        private string GenerateJwtToken(UserClient user,out DateTime tokenExpiryDate)
        {           
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            tokenExpiryDate = DateTime.UtcNow.AddHours(_jwtSettings.ValidityInHours);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserName", user.UserName) , new Claim("permissions",string.Join(",", user.Permissions)) }),
                Expires = tokenExpiryDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
  
}
