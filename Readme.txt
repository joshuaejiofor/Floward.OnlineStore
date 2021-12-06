Technology
---------------------
	.Net 5
	Visual Studio 2019 or higher



Questions
---------------------
	1. Design the Microservice Architecture for the example (diagram that represent it)?
		=> ./microservice architecture.png
	
	2. Define the design pattern you are going to use?
		=> Factory Pattern

	3. Create a .net core project that have the designed architecture (only to show architecture)?
		=> src/shared/Floward.OnlineStore.ApplicationCore : this shows Factories / Providers / Services

	4. Create at least one of the databases you are going to use (locally)?
		=> src/shared/Floward.OnlineStore.EntityFrameworkCore : this shows the database / repositories

	5. Create one repository and one controller for add to cart part?
		=> Floward.OnlineStore.WebApi : this shows the OrderApi (add / remove from Cart) and ProductApi (CRUD)

	6. Do RabbitMQ integration (dummy)?
		=> src/shared/Floward.OnlineStore.RabbitMQCore : this shows RabbitMQ integration

	7. Adding a testing unite is a plus?
		=> tests/Floward.OnlineStore.WebApi.Tests

