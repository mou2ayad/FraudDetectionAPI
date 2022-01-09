
## The code coverage

As requested, this solution covers the main mandatory 2 points and most of the additional points
 #### -  Main Requested Endpoints:
>  1. **Receives and stores a person**
>  2. **Calculates** the probability that 2 persons are the same physical person: Here I didn't want to create a simple Matching service that matches  only Person type based on fixed properties, so I tried to show more skills by building a smart MatchingService that can Match any type of objects based on "Matching" and "Similarity" configs, for example, we can use this to Match 2 persons,  2 addresses, or 2 organizations ... etc.
>  
 #### -  The Additional Requests :
 >  1. **Logging**: I used NLog to log (info, warnings, errors),  and also created ExceptionMiddleware to handle any unhandled error from the API and recognize the type of the error ( Client Error "Bad Request", or internal server error) and log the entire error details along with the parameter that caused this error in order to reproduce the issue and to facilitate the troubleshooting process, and finally, it returns a general error message to the client if the environment is Production otherwise it returns the full error details to the developer in a non-Prod environment 
 >  2. **Documentation**: I used Swagger for that
 >  3. **Security**: I used JWT for the security with simple userService to check the username/password, and added an endpoint to generate the JWT token in order to use it to call the other endpoint, I didn't use the Microsoft built-In attributes/middleware to check the Authentication in the endpoint, I rathered to create the required jwt middleware and attribute manually to show that I have a good understanding of JWT authentication concept
 >   4. **Request caching**: I created 2 flavours of caching, MemoryCache and Distributed Cache using Memcached in case we need to run the api through LoadBalancer in multiple nodes
 >   5. **Matching rules are configurable**:  The Matching rules are fully configurable, and can be updated during runtime, and also it can be created easily, where I used Builder pattern to create them along with Similarity rules , for Example:
 >   
```Creating MatchingRules example
MatchingRules.SetRange(
                 MatchingRule.From("FirstName", 20).With(
                    SimilarityRule.From(SimilarityServiceType.Initials, 15),
                    SimilarityRule.From(SimilarityServiceType.NickName, 15),
                    SimilarityRule.From(SimilarityServiceType.Typo, 15)
                ),
                MatchingRule.From("LastName", 40),
                MatchingRule.From("DateOfBirth", 40),
                MatchingRule.From("IdentificationNumber", 100));
```

## The Solution consists of these Projects

 #### 1. Fraud.Component.Common:
 > Class Library project (.net standard), it contains the main common interfaces and models we use in the other projects
 #### 2. Fraud.Component.Utilities
 > Class Library project (.net standard), it is a shared component, doesn't cover any business logic, practically it doesn't belong to any specific solution, it can be used in any other solution.
it provides the Error handling, Logging (NLog), CustomExceptions, JWT, Swagger and Caching(in-memory and distributed Cache using **Memcached**) features to any .net solution
 #### 3. Fraud.Component.DataAccessLayer
> Class Library project (.net standard), it is used to comunicate with databases (it can be MongoDb,Sql, or any other fake db) 
#### 4. Fraud.Component.Matching
> Class Library project (.net standard), it is used to provide matching services based on configurable matching and similarity rules
it has its own lookup database for matching purposes 

 #### 5. Fraud.Test ( Unit Test 60 Test Cases)
 > .net Core 5 NUnit test, contains the unit test project of Fraud.Component.DataAccessLayer and Fraud.Component.Matching projects (Project 3+4)

#### 6. Fraud.API.Test (Component Leve Test "12 Test cases")
 > .net Core NUnit test, to test the api endpoints including middleware (AuthenticationMiddleware and ErrorHandlingMiddleware)
 
#### 7. Fraud.Api.Matching
> .net Core 5 RESTful API, it is the Startup project and the final user front API
>
> - Swagger URL : https://localhost:44326/swagger/index.html
The API can be tested using Swagger directly without needing to use postman or fiddler 
 > - Authentication Endpoint : https://localhost:44326/api/authentication/token
 it is POST request, it is needed to generate the bearer token, We can use this JSON to login and get the required permission  
```Request Body Example
{
      "username": "admin",
      "password": "admin"
}
```
> **Note:By calling the endpoints using swagger, no need to add "bearer" in front of the token, it is automatically appended to the token**
> - CreatePerson Endpoint : https://localhost:44326/api/v1/Person/Create , Post request,we need to use the generated auth token from the Authentication endpoint, and pass it to the header of this request as following 
   "bearer {*token*}" 
```Request Body example
{
  "firstName": "Andrew",
  "lastName": "Craw",
  "dateOfBirth": "2021-12-19",
  "identificationNumber": "931212312"
}
```
> - GetPerson Endpoint : https://localhost:44326/api/v1/Person/{id} , Get request, we need to use the generated auth token from the Authentication endpoint, and pass it to the header of this request as following 
   "bearer {*token*}" 
>   
> - Matching two persons Endpoint : https://localhost:44326/api/v1/Fraud/Match , Post request, we need to use the generated auth token from the Authentication endpoint, and pass it to the header of this request as following 
   "bearer {*token*}" 
```Request Body Example
{
  "first": {
  "firstName": "Andy",
  "lastName": "Crao",
  "dateOfBirth": "2021-12-19",
  "identificationNumber": "931212311"
},
  "second": {
  "firstName": "Andrew",
  "lastName": "Craw",
  "dateOfBirth": "2021-12-18",
  "identificationNumber": "931212312"
}
```
