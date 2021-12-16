namespace FRISS.Components.Utilities.JWT_Auth
{
    public class JwtSettings
    {
        public string SecretKey { set; get; }
        public int ValidityInHours { set; get; }     
    }
}
