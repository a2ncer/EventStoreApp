# EventStoreApp
Implemented REST service using Event sourcing and CQRS approach. 
PUT, DELETE (GET as well by it's definition) methods are ipempotent.

## Swagger API Documentation
http://localhost:5000/swagger

## Solution structure

Project | Description
------------ | -------------
IntegrationTests | Tests for REST API, need to be run WebApi and MongoDB
ApplicationServices | Commands, Queries and Handlers
Domain | Domain models and abstractions
Infrastructure | Repositories implementations
WebApi | ASP .NET Core application with REST API
WebApiClient | Genereted API's Client with Swagger generator

## Requirements
- .NET Core 3.1
- MongoDB

## Answered questions

**1. How many cows are pregnant on farm "A" on a specific date?**

GET /api/v1/cows/count with parameters. 
See GetCowsCountOnFarmOnDateQueryHandler and CowsApiTests classes.

**2. How many sensors died in June across the platform?**

GET /api/v1/sensors/count with parameters. 
See GetSensorsCountOnDateQueryHandler class.

**3. On average how many new sensors are deployed every month in 2020?**

GET /api/v1/sensors/avarage with parameters. 
See GetAvarageSensorsCountEveryMonthQueryHandler class.
