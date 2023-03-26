# AlumniNetworkAPI

Alumni Network Backend is a robust ASP.NET 6 Web API built using C# that serves as the backbone for the Alumni Network application. The API integrates with Keycloak for user authentication, hosted on Cloud-IAM, and utilizes Swagger for documentation of all endpoints. A code-first relational database with SQL Express Server is employed to manage data. The project adheres to the repository pattern, featuring controllers, services with interfaces, and custom mock data for testing all API endpoints. Query parameters are available for select GET endpoints.

## Table of Contents

1. [Overview](#overview)
2. [Prerequisites](#prerequisites)
3. [Getting Started](#getting-started)
4. [Usage Instructions](#usage-instructions)
5. [Database Diagram](#database-diagram)
6. [Contributing Guidelines](#contributing-guidelines)
7. [License Information](#license-information)

## Overview

The Alumni Network Backend offers the following key functionalities:

- Keycloak integration for secure user authentication
- Swagger documentation for API endpoints
- Code-first relational database with SQL Express Server
- Repository pattern with controllers and services
- Custom mock data for testing
- Query parameters support for select GET endpoints

## Prerequisites

Before you begin, ensure your development environment meets the following requirements:

- .NET 6 SDK
- SQL Express Server
- A configured Keycloak server on Cloud-IAM

## Getting Started

Follow these steps to set up and run the Alumni Network Backend locally:

1. Clone the repository:

    git clone https://github.com/Filipll97/Case_Alumni_Network.git
    
2. Restore the required dependencies:

    dotnet restore
    
3. Set up the appropriate environment variables or modify the `appsettings.json` file with the necessary values for Keycloak integration and database connection.

4. Apply the database migrations:
    
    dotnet ef database update

5. Launch the development server:

    dotnet run
    
The API should now be accessible at `http://localhost:5000`.

## Usage Instructions

1. Open your preferred web browser and navigate to `http://localhost:5000/swagger`.
2. Authenticate using your Keycloak credentials (if required).
3. Explore and test the API endpoints using the provided mock data and query parameters.

## Database Diagram

<!-- Insert Database Diagram Here -->

## License Information

The Alumni Network Backend project is licensed under the MIT License. For more details, refer to the [LICENSE](LICENSE) file.

Made by: Filip Lindell, Tintin Petersson and Maryam Almashhadi
