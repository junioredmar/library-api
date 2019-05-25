# How to run
### via Visual Studio
1. Press "Start" and wait for the swagger to be opened in your browser with all available endpoints
### via CLI
1. Run the following command
```
dotnet run --project ./src/Library.API
```
2. On your browser go to http://localhost:5000/index.html

# How to execute tests
### via Visual Studio
1. Open "Test Explorer" tab
2. Press "Run all"
### via CLI
1. Run the following command
```
dotnet test ./src/Library.sln
```

# Structure
- `Library.API`: Entry point for the API. 
- `Library.Core`: Holds the business logic and shared classes
- `Library.Repositories`: Data layer (can be replaced in the future as it is decoupled)
- `Library.Tests.Unit`: **Unit tests** are meant to test small pieces of code. For this reason, I am using the library Moq, which allows us to mock both entry and exit points of a function.
- `Library.Tests.Integration`: **Integration tests** ensure that an our components function correctly at a level that includes the supporting infrastructure, such as the database, file system, and network. For this type of tests I am using TestServer to raise an in-memory server to trigger real HTTP requests

## Separation of concerns
- This solution does not know where books come from. No external queries are made into this microservice. The only responsability of the Library is to know about the books that the Library have.
- In case of future implementations, we could think about creating one wrapper for the external source that retrieves books by isbn. Probably we could add data normalization in it. But no more than that. It will be a gateway only.

# Appendix
### External packages
- [Swashbuckle.AspNetCore](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2)
- [Moq](https://github.com/moq/moq4)
- [FluentAssertions](https://fluentassertions.com/)

### Book model
- In a relational database, I would split the Book and create a entity called Rent into another table. Thinking in replacing the database to a NoSQL it would make sense to create the entity as a whole and store in a document format.

### Swagger documentation
- The Swashbuckle package offers a way of documenting our APIs based on the assembly xml, the comments above each endpoint will be transformed to documentation when running the application.

### Error handling middleware
- In case of any unexpected error we are returning an exception for debugging reasons (only in case we have this service deployed in a non-production environment, on production environments, we don´t display the expection). The object model returned is called `ProblemDetails`, it respects the specification [RFC 7807](https://tools.ietf.org/html/rfc7807)

### Status codes
- Meaninful status codes for the responses were implemented in order to reach a high level of best practices of a restfull API (Based on the [Richardson Maturity Model](https://martinfowler.com/articles/richardsonMaturityModel.html)).

### Future releases
- `Data Layer Microservice`: Extracting the data layer and creating one microservice for it
- `AutoMapper`: Include AutoMapper to Distinct between View Model and Database entities
- `Database`: Create a database for the application 
- `Logging mechanism`: Agree on a logging mechanism and make more use of ILogger 
- `Safeguard cryptographic keys`: Store future key, passwords and secrets to an external storage like Azure Key Vault
- `Authentication`: Include authentication to the API
- `Validation`: Improve Model valitations like fields ranges, required, length
- `CI/CD`: Create build and release pipelines
- `Deployment`: Agreement where to deploy the application
- `Container`: Conteinerize the application and use Docker and Kubernetes to orchestrate
- `Multiple environments`: We can have Dev, Staging and Production environment and specific settings for each
- `New functionalities`: Include new functionalities like "Lending History", "Delete Book"...
