using System.Threading.Tasks;
using Fraud.Api.Matching.Models;
using Fraud.Component.Common.Models;
using Fraud.Component.DataAccessLayer.Contracts;
using Fraud.Component.Matching.Contracts;
using Fraud.Component.Utilities.ErrorHandling;
using Fraud.Component.Utilities.JWT_Auth;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Fraud.Api.Matching.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class FraudMatchingController : ControllerBase
    {
        private readonly IPersonsRepository _personsRepository;
        private readonly IMatchingService<Person> _matchingService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personsRepository"></param>
        /// <param name="matchingService"></param>
        public FraudMatchingController( IPersonsRepository personsRepository, IMatchingService<Person> matchingService)
        {
            _personsRepository = personsRepository;
            _matchingService = matchingService;
        }


        /// <param name="request">Person to be added</param>
        /// <returns>created person Id</returns>
        [HttpPost("api/v1/Person/Create")]
        [JwtAuthorize("IO")]
        [SwaggerOperation(
            Summary = "Create New Person",
            Description = "Create a new Person into Persons Database",
            OperationId = "CreatePersonEndpoint")]
        [ProducesResponseType(typeof(CreatePersonResponse), 201)]
        [ProducesResponseType(typeof(ExceptionDetails), 400)]
        [ProducesResponseType(typeof(ExceptionDetails), 500)]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest request)
        {
            var id= await _personsRepository.AddPerson(request.ToPerson());
            return StatusCode(201, CreatePersonResponse.From(id));
        }

        /// <param name="id">Id of the person to be retrieved</param>
        /// <returns>Person Info</returns>
        [HttpGet("api/v1/Person/{id}")]
        [JwtAuthorize("IO")]
        [SwaggerOperation(
            Summary = "Get Person",
            Description = "Get person info from database by using the person Unique Id",
            OperationId = "GetPersonByIdEndpoint")]
        [ProducesResponseType(typeof(CreatePersonResponse), 201)]
        [ProducesResponseType(typeof(ExceptionDetails), 400)]
        [ProducesResponseType(typeof(ExceptionDetails), 500)]
        public async Task<ActionResult<Person>> GetPersonById(string id)
        {
            var person = await _personsRepository.GetPersonById(id);
            return Ok(person);
        }


       
        /// <param name="request">request contains First Person and last Person</param>
        /// <returns>Matching Score</returns>
        [HttpPost("api/v1/Fraud/Match")]
        [JwtAuthorize]
        [SwaggerOperation(
            Summary = "Matching two Persons",
            Description = "Match two persons based on the Matching rules and return matching score",
            OperationId = "GetPersonByIdEndpoint")]
        [ProducesResponseType(typeof(CreatePersonResponse), 201)]
        [ProducesResponseType(typeof(ExceptionDetails), 400)]
        [ProducesResponseType(typeof(ExceptionDetails), 500)]
        public async Task<ActionResult<MatchingResponse>> Match([FromBody] MatchingRequest request)
        {
            ValidateMatchRequest(request);
            var result = await _matchingService.Match(request.First,request.Second);
            return MatchingResponse.From(result);
        }

        private static void ValidateMatchRequest(MatchingRequest request)
        {
            if (request == null || request.First == null || request.Second == null)
                throw new InvalidRequestException("Both First and Second Person can't be null");
            if (string.IsNullOrEmpty(request.First.FirstName) 
                || string.IsNullOrEmpty(request.First.LastName) 
                || string.IsNullOrEmpty(request.Second.FirstName) 
                || string.IsNullOrEmpty(request.Second.LastName))
                throw new InvalidRequestException("FirstName and Last Name can't not be null of empty");
        }
    }
}
