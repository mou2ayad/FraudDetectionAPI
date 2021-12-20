namespace Fraud.Api.Matching.Models
{
    public class CreatePersonResponse
    {
        public static CreatePersonResponse From(string personId)
            => new() {PersonId = personId};
        public string PersonId { set; get; }
    }
}
