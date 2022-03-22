# Andrew-DeMarco-P1

# Planet Paintball Store API

## Project Description

This is a store app where we can Add users, verify their credentials, look at products different stores offer, Place an order, view their order history by date and total cost.
Stores can also view the order history of their store and managers can replace the inventory for their store. 

## Technologies Used
-C#

-LINQ

-JSON

-ADO.NET

-Xunit

-Serilog

-Azure DevOps

-ASP.NET WebAPI

-ASP.NET SignalR

-Swagger/ThunderClient

-Moq

-VS Code

-DBeaver

-Git

-GitHub

-SonarCloud

## Usage
Clone the repo.

cd into the projects API folder and run the cli command: dotnet run

Test the project in a local host: http://localhost:{yourport}/swagger/index.html

can run tests using dotnet test in the project's test folder.

## Links
Base Azure Deployed App Link
https://ppdbwebapp.azurewebsites.net

use proper endpoints for certain tools with the API
example for getting all customers (Note the credentials for this application are basic and do not take security into account. Proper authentication with security is something to add).
https://ppdbwebapp.azurewebsites.net/api/Customer/GetAllCustomers
