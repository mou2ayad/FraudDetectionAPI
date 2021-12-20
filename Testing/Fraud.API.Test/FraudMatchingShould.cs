using System;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Fraud.Api.Matching;
using Fraud.Api.Matching.Models;
using Fraud.Component.Matching.Models;
using Fraud.Component.Utilities.ErrorHandling;
using Fraud.Component.Utilities.JWT_Auth;
using Fraud.Test;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Fraud.API.Test
{
    public class FraudMatchingShould
    {
     
        [Test]
        public async Task Create_person_successfully()
        {
            var testClient = HttpClient();
            var header=await Login(testClient);

            var createPersonResponse= await testClient.Post<CreatePersonResponse>("api/v1/Person/Create",
                RequestsExamples.CreatePersonRequestExample(), header);

            createPersonResponse.Should().NotBeNull();
            createPersonResponse.PersonId.Should().NotBeNullOrEmpty();
        }

        [Test]
        public async Task Get_bad_request_when_create_person_without_first_name()
        {
            var testClient = HttpClient();
            var header = await Login(testClient);
            var personRequest = RequestsExamples.CreatePersonRequestExample();
            personRequest.FirstName = null;
            var responseMessage = await testClient.Post("api/v1/Person/Create", personRequest, header);
            var errorDetails = await responseMessage.Content.ReadFromJsonAsync<ExceptionDetails>();

           responseMessage.StatusCode.Should().Be(400);
           errorDetails.Should().NotBeNull();
           errorDetails.ErrorMessage.Should().Be("FirstName and LastName can't be null");

        }

        [Test]
        public async Task Get_bad_request_when_create_person_without_last_name()
        {
            var testClient = HttpClient();
            var header = await Login(testClient);
            var personRequest = RequestsExamples.CreatePersonRequestExample();
            personRequest.LastName = null;
            var responseMessage = await testClient.Post("api/v1/Person/Create", personRequest, header);
            var errorDetails = await responseMessage.Content.ReadFromJsonAsync<ExceptionDetails>();

            responseMessage.StatusCode.Should().Be(400);
            errorDetails.Should().NotBeNull();
            errorDetails.ErrorMessage.Should().Be("FirstName and LastName can't be null");
        }

        [Test]
        public async Task Give_unauthenticated_when_call_endpoint_without_login()
        {
            var testClient = HttpClient();

            var createPersonResponse = await testClient.Post("api/v1/Person/Create",
                RequestsExamples.CreatePersonRequestExample());

            createPersonResponse.StatusCode.Should().Be(401);
        }

        [Test]
        public async Task Give_unauthenticated_when_call_endpoint_with_wrong_token()
        {
            var testClient = HttpClient();

            var createPersonResponse = await testClient.Post("api/v1/Person/Create",
                RequestsExamples.CreatePersonRequestExample(), GetAuthHeader("wrong token"));

            createPersonResponse.StatusCode.Should().Be(401);
        }


        [Test]
        public async Task Match_two_persons_successfully()
        {
            var testClient = HttpClient();
            var header = await Login(testClient);

            var matchingResponse = await testClient.Post<MatchingResponse>("api/v1/Fraud/Match",
                RequestsExamples.MatchRequestExample(), header);

            matchingResponse.Should().NotBeNull();
            matchingResponse.MatchingScore.Should().Be(55);
        }

        [Test]
        public async Task Match_two_persons_successfully_and_ignore_similarity_rules_if_not_added_to_config()
        {
            var testClient = HttpClient();

            var header = await Login(testClient);
            ClearMatchingRules();
            var matchingResponse = await testClient.Post<MatchingResponse>("api/v1/Fraud/Match",
                RequestsExamples.MatchRequestExample(), header);

            matchingResponse.Should().NotBeNull();
            matchingResponse.MatchingScore.Should().Be(40);
        }

        [Test]
        public async Task Match_two_persons_successfully_and_with_custom_similarity_rules_value()
        {
            var testClient = HttpClient();

            var header = await Login(testClient);
            ChangeNickNameSimilarityRuleScore(5);
            var matchingResponse = await testClient.Post<MatchingResponse>("api/v1/Fraud/Match",
                RequestsExamples.MatchRequestExample(), header);

            matchingResponse.Should().NotBeNull();
            matchingResponse.MatchingScore.Should().Be(45);
        }

        [Test]
        public async Task Match_give_bad_request_when_one_of_two_persons_is_null()
        {
            var testClient = HttpClient();
            var header = await Login(testClient);

            var request = RequestsExamples.MatchRequestExample();
            request.First = null;
            var responseMessage = await testClient.Post("api/v1/Fraud/Match", request, header);

            var errorDetails = await responseMessage.Content.ReadFromJsonAsync<ExceptionDetails>();

            responseMessage.StatusCode.Should().Be(400);
            errorDetails.Should().NotBeNull();
            errorDetails.ErrorMessage.Should().Be("Both First and Second Person can't be null");
        }

        [Test]
        public async Task Match_give_bad_request_when_both_persons_are_null()
        {
            var testClient = HttpClient();
            var header = await Login(testClient);

            var request = RequestsExamples.MatchRequestExample();
            request.First = null;
            request.Second = null;
            var responseMessage = await testClient.Post("api/v1/Fraud/Match", request, header);

            var errorDetails = await responseMessage.Content.ReadFromJsonAsync<ExceptionDetails>();

            responseMessage.StatusCode.Should().Be(400);
            errorDetails.Should().NotBeNull();
            errorDetails.ErrorMessage.Should().Be("Both First and Second Person can't be null");
        }

        [Test]
        public async Task Match_give_bad_request_when_one_of_persons_has_no_first_name()
        {
            var testClient = HttpClient();
            var header = await Login(testClient);

            var request = RequestsExamples.MatchRequestExample();
            request.First=PersonBuilder.Create(null,"someLastName").Build();
            var responseMessage = await testClient.Post("api/v1/Fraud/Match", request, header);

            var errorDetails = await responseMessage.Content.ReadFromJsonAsync<ExceptionDetails>();

            responseMessage.StatusCode.Should().Be(400);
            errorDetails.Should().NotBeNull();
            errorDetails.ErrorMessage.Should().Be("FirstName and Last Name can't not be null of empty");
        }

        [Test]
        public async Task Match_give_bad_request_when_both_of_persons_have_no_last_names()
        {
            var testClient = HttpClient();
            var first = PersonBuilder.Create("andy", null).Build();
            var second = PersonBuilder.Create("andy", null).Build();

            var header = await Login(testClient);
            var responseMessage = await testClient.Post("api/v1/Fraud/Match", MatchingRequest.From(first,second), header);

            var errorDetails = await responseMessage.Content.ReadFromJsonAsync<ExceptionDetails>();

            responseMessage.StatusCode.Should().Be(400);
            errorDetails.Should().NotBeNull();
            errorDetails.ErrorMessage.Should().Be("FirstName and Last Name can't not be null of empty");
        }

        private async Task<HttpTestHeader> Login(TestClient testClient)
        {
            var authToken = new AuthenticateRequest { Username = "admin", Password = "admin" };
            var authResponse = await testClient.Post<AuthenticateResponse>("api/authentication/token", authToken);
            return GetAuthHeader(authResponse.Token);
        }
        
        private static HttpTestHeader GetAuthHeader(string token)
        => HttpTestHeader.From("Authorization", $"Bearer {token}");


        private static IWebHostBuilder CreateWebHost() =>
            new WebHostBuilder()
                .UseStartup<Startup>()
                .UseWebRoot(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "wwwroot")))
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(TestContext.CurrentContext.TestDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build()
                );

        public static TestClient Create(Action<IServiceCollection> overrideInjections)
        {
            var server = new TestServer(
                CreateWebHost()
                    .ConfigureTestServices(overrideInjections));
            return new TestClient(server);
        }

        private static TestClient HttpClient() => Create(container =>
        {
        });

        private static void ClearMatchingRules() => MatchingRules.Get("FirstName").SimilarityRules=null;

        private static void ChangeNickNameSimilarityRuleScore(decimal score) =>
            MatchingRules.Get("FirstName").SimilarityRules
                .First(e => e.SimilarityType == SimilarityServiceType.NickName).SimilarityScore = score;

    }
}