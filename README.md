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
 #### 5. Fraud.Test
 > .net Core 5 NUnit test, contains the unit test project of Fraud.Component.DataAccessLayer and Fraud.Component.Matching projects (Project 3+4)

#### 6. Fraud.API.Test
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
