using System.Collections.Generic;
using System.Linq;
using Fraud.Component.Utilities.JWT_Auth;
using Microsoft.Extensions.Options;

namespace Fraud.Api.Matching.Services
{
    //IMPORTANT: this is just mock testing (POC), we should logging using real Authentication and Authorization service
    public class StaticUserService : UserService
    {
        private List<UserClient> _users;
        public StaticUserService(IOptions<JwtSettings> jwtSettings) : base(jwtSettings)
        {
            _users = new List<UserClient>
            {
                new UserClient { Id = 1, UserName = "admin", Password = "admin", Permissions = new List<string>() { "IO" } },
                new UserClient { Id = 2, UserName = "test", Password = "test", Permissions = new List<string>() { } }
            };
        }
        public override UserClient Login(string userName, string password)
        {
            return _users.SingleOrDefault(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password);
        }
    }
}
