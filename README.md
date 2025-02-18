# Bootstrap.Solutions
This is a solution template for creating a new solution with a web api project, class libraries for interactors, data services, services, ef core data access, and third party services.

## Data flow
This is a web api project that uses controllers and accepts requests from clients. It is the entry point for the solution.
The controllers in this project will interact with the interactors and no business logic should be in the controllers.

The solutios uses following flow for processing requests:
1. Controller receives request from client
2. Controller create a message object and include the request data and any other data needed for processing
3. The message object will go through a series of pipelines called pipeline behaviors. Each pipeline behavior will process the message object and pass it to the next pipeline behavior.
	3.1. The first pipeline behavior will validate the message object
	3.2. Every message will have one or more validators that will validate the message object. If the message object is not valid, the pipeline behavior will return an error 		message to the client.
	3.3. Any other pipeline behaviors can be added to the pipeline to process the message object.
4. Once the message object has gone through all the pipeline behaviors, it will be passed request handler. The request handler will process the message object and return a response message object. There can only be one request handler for each message object.

### Bootstrap.Web.Api
This is a web api project that uses controllers and accepts requests from clients. It is the entry point for the solution.

### Bootstrap.Interactors
This is a class library that contains interactors. Interactors are classes that process requests from clients. The interactors will contain the business logic for processing requests.

### Bootstrap.Data.Services
This is a class library that contains data services. Data services are classes that interact with the database. The data services will contain the logic for interacting with the database.

### Bootstrap.Services
This is a class library that contains internal services wich are used by interactors. Example of services include Azure Blob Storage Service, Azure Table Storage Service, etc.

### Bootstrap.Data.EntityFramework
This is a class library that contains the ef core data access layer. This library will contain the dbcontext and entity classes.

### Bootstrap.ThirdParty.Services
This is a class library that contains third party services. These services can be used to interact with third party services. Example of third party services include external api services.

### Bootstrap.Solutions.Tests
This is a class library that contains unit tests and integration tests for the solution.