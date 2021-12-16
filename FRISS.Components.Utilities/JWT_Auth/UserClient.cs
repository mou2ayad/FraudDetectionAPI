using System.Collections.Generic;
using Newtonsoft.Json;

namespace FRISS.Components.Utilities.JWT_Auth
{
    public class UserClient
    {
        public int Id { get; set; }
        public string UserName { set; get; }
        public List<string> Permissions  { set; get; }
        [JsonIgnore]
        public string Password { set; get; }
    }
}
