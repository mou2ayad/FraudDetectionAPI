using System.Threading.Tasks;
using Fraud.Api.Matching.Models;
using Fraud.Component.Common.Models;
using Fraud.Component.DataAccessLayer.Contracts;
using Fraud.Component.Matching.Contracts;
using Fraud.Component.Utilities.ErrorHandling;
using Fraud.Component.Utilities.JWT_Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fraud.Api.Matching.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class FraudMatchingController : ControllerBase
    {
        private readonly ILogger<FraudMatchingController> _logger;
        private readonly IPersonsRepository _personsRepository;
        private readonly IMatchingService<Person> _matchingService;
        public FraudMatchingController(ILogger<FraudMatchingController> logger, IPersonsRepository personsRepository, IMatchingService<Person> matchingService)
        {
            _logger = logger;
            _personsRepository = personsRepository;
            _matchingService = matchingService;
        }


        [HttpPost("api/v1/Person/Create")]
        [JwtAuthorize("IO")]

        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonRequest request)
        {
            var id= await _personsRepository.AddPerson(request.ToPerson());
            return StatusCode(201, CreatePersonResponse.From(id));
        }

        [HttpGet("api/v1/Person/{id}")]
        [JwtAuthorize("IO")]
        public async Task<ActionResult<Person>> GetPersonById(string id)
        {
            var person = await _personsRepository.GetPersonById(id);
            return Ok(person);
        }


        [HttpPost("api/v1/Fraud/Match")]
        [JwtAuthorize]
        public async Task<ActionResult<MatchingResponse>> Match([FromBody] MatchingRequest request)
        {
            if (request == null || request.First == null || request.Second == null)
                throw new InvalidRequestException("Both First and Second Person can't be null");

            var result= await _matchingService.Match(request.First,request.Second);
            return MatchingResponse.From(result);
        }
    }
}
