using System;

namespace FRISS.Components.Utilities.JWT_Auth
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }
        public DateTime TokenExpiryDate { get; set; }


        public AuthenticateResponse( string token,DateTime tokenExpiryDate)
        {
            Token = token;
            TokenExpiryDate = tokenExpiryDate;
        }
    }
}
