namespace FRISS.Components.Utilities.JWT_Auth
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);      
    }
}
