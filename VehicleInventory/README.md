Vehicle Inventory Microservice

Assignment #1 – Clean Architecture & DDD 



Overview:
In this project, the Inventory Bounded Context of a car renting service has been implemented with-

ASP.NET Core Web API (.NET 8)
Entity Framework Core
Clean Architecture
Domain-Driven Design (DDD)
Swagger for API testing

The service manages vehicles, availability control and lifecycle regulations. The primary objective of this assignment was to provide the separation of concerns and business enforcement within the Domain layer.



Architecture:
The solution is based on Clean Architecture and it is separated into four layers:

Domain: Business rules and entities.
Application: Use cases and service logic.
Infrastructure: EF Core and database persistence.
WebAPI: Controllers and HTTP endpoints.

Dependency Direction-
WebAPI -> Application 
Application -> Domain
Infrastructure -> Application.
Domain -> No dependencies

This guarantees that the Domain layer is not connected or intertwined with framework code.




Domain Model:
The main aggregate is JPVehicle.

Properties-
Id
VehicleCode
LocationId
VehicleType
Status


Status Values-
Available
Rented
Reserved
UnderService

Business Rules-
All rules are enforced inside the JPVehicle entity-
A vehicle cannot be rented if it is already rented.
A vehicle cannot be rented if it is reserved.
A vehicle cannot be rented if it is under service.
A reserved vehicle cannot be marked available without explicit release.
Invalid transitions throw JPDomainException.

Controllers and EF Core do not enforce these rules.



Application Layer:
The Application layer contains-
DTOs
JPIvehicleRepository interface
JPVehicleService

Implemented use cases-
CreateVehicle
GetVehicleById
GetAllVehicles
UpdateVehicleStatus
DeleteVehicle

The service makes use of domain behavior methods (e.g., MarkRented()), so that rules do not move out of the Domain layer.



Infrastructure:
Uses-
JPInventoryDbContext
JPVehicleRepository
EF Core migrations

Infrastructure can handle only data persistence and does not contain business logic here.



API Endpoints:
Base route-
/api/v1/vehicles

Available endpoints-
GET /api/v1/vehicles
GET /api/v1/vehicles/{id}
POST /api/v1/vehicles
PUT /api/v1/vehicles/{id}/status
DELETE /api/v1/vehicles/{id}

Swagger is activated, and every enpoint passed the test.



Database & Migrations:
To Create Migration-
Add-Migration InitialCreate -Project VehicleInventory.Infrastructure -StartupProject VehicleInventory.WebAPI

To Update database-
Update-Database -Project VehicleInventory.Infrastructure -StartupProject VehicleInventory.WebAPI



How to Run:
1- Open Visual Studio 2022 and open solution in it
2- Select WebAPI as startup project
3- Click the green run button to start the Application
4- Swagger open automatically and can test all endpoints.



Conclusion:
There is adequate application of Clean Architecture and DDD principles in this project. The Domain layer completely captures business rules and each layer has a well defined its responsibility.

The microservice satisfies all the requirements of the assignment and was tested with Swagger.


