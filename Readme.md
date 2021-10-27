# Leaderboard API

A basic REST API for tracking players and their standing on a leaderboard

There are two controllers, each with CRUD operations:

	1. PlayerController 
	2. LeaderboardController

Documentation for the JSON request structure is outlined on the API's swagger page (```/swagger/index.html```)

## Unit Tests

The application is fully unit tested (at least for the specific requirements I can think of). The idea of the tests is to act as requirements for the application (e.g. There are tests to make sure the email of a player on edit/creation is correct). Most of the tests involve sending a sample request and asserting a specific response. The reason for this high level testing is so that if the logic that handles requests changes, the tests do not have to be changed. 

Generally, I try to follow TDD (although this isn't always possible), as aside from the obvious benefits of catching bugs early on, it encourages developers to write the minimum amount of application code possible. In other words, the only application code that is written is the code required for the tests to pass. This ultimately leads to a more robust and simpler application. 

## Things to consider

- It doesn't make sense for a player to have two entries in the leaderboard, and it also doesn't make sense for the leaderboad to reference players that don't exist.

For players:
	- First and last names are required parameters
	- Emails must be valid (at least contain an '@' sign as a minimum)

For leaderboard:
    - All items required except Id (makes the most sense)


## Next steps

 - As an application, this API is pretty basic, there is no front end, security, or methods of deployment. Some ideas of where this could go next are as follows:

	- Give the app a front end - could be a single page application in React. Alternatively, the app could be simply turned into an MVC app by getting the controllers to return views instead of data.

	- Give the app some authentication - In .Net MVC apps (which are basically just APIs with views), there is an ```[Authorize(Roles="")]``` decorator for controller classes and methods. This can be combined with some middleware that validates claims in a JSON Web Token (JWT) with specific roles for each controller/method. For this API, a React front end could be programmed to allow login (using a service such as AWS Cognito). On login, the app would receive a (JWT) with which requests (with the JWT in the header) can be validated. That way, we don't get unauthorised users deleting things!

	- To expand on this further, this API could be containerised with docker (although if used with docker, the database has to be accessible - it can't just be dumped on localhost) and hosted in a VPC behind an API gateway (or written as a Lambda function if hosted on AWS). Attached to the API gateway would be an authoriser that validates the JWTs of incoming requests, meaning that the container/lambda function would not need to worry about security as much (I've used this architecture a few times in the past to build OPEX focused systems with little to no CAPEX requirement).

	- In terms of deployment, the most obvious approach would be to setup a CI/CD pipeline (with something like GitHub actions or AWS CodePipeline) that checks out the code from its repository, runs the tests, and deploys it to a staging environment. Once in the staging environment, any integration tests/QA can be done, before moving it on to production. 

## A Note on Entity Framework

Entity framework is very often a useful tool for abstracting away the details of a database, but it isn't always the best tool to use. Because it relies on mappings to database tables, any complex queries are not done with SQL on the database side, but LINQ on the application side. In most cases this is fine, but it can have a performance impact when dealing with large quantities of data. Hence, sometimes it is better to take the more low level approach of sending parameterised SQL queries (although this can limit an app to using one flavour of SQL), or stored procedures (I generally try to avoid using stored procedures for the simple reason that it means you end up with logic in different places, but they can make things easier).