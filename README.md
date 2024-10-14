# e-Appointment Backend

![Alt text](/screenshots/12-e-appointment.png)

## Project Goal and Scope

* This project is an e-appointment system developed for a sample hospital. 
* By logging into the system, users can make an appointment for the department and doctor they want on the appropriate day and time.
* In addition, operations such as appointment status management, past appointment tracking, and user management can be performed through the system.

* You can try the live version of the project at the following address:
* Link: https://www.e.appointment.erengaygusuz.com.tr
  
* You can access the system with the sample user information below and experience the system with different roles.
  
    * Super Admin 
        * Username: ``` superadmin ```
        * Password: ``` 12345 ```

    * Admin 
        * Username: ``` admin ```
        * Password: ``` 12345 ```

    * Doctor 
        * Username: ``` doctor ```
        * Password: ``` 12345 ```

    * Patient 
        * Username: ``` patient ```
        * Password: ``` 12345 ```

## User Types of the Project and the Transactions They Can Perform

* There are 4 user types available in this application. These are:

  - Super Admin
  - Admin
  - Doctor
  - Patient

* You can see the transaction table of user types in the system at below.

|                                                 |Super Admin|Admin|Doctor|Patient|
|-------------------------------------------------|:---------:|:---:|:----:|:-----:|
|Log in to the system                             |     x     |  x  |  x   |   x   |
|Log out of the system                            |     x     |  x  |  x   |   x   |
|View their profile                               |     x     |  x  |  x   |   x   |
|Update their profile                             |           |  x  |  x   |   x   |
|Change the system’s theme color                  |     x     |  x  |  x   |   x   |
|Change the system’s language                     |     x     |  x  |  x   |   x   |
|View weekly appointments by department and doctor|           |     |      |   x   |
|Book appointments by department and doctor       |           |     |      |   x   |
|Cancel a booked appointment                      |           |     |      |   x   |
|List their appointments                          |           |     |  x   |   x   |
|Filter appointments                              |           |     |  x   |   x   |
|Search in the appointment list                   |           |     |  x   |   x   |
|Export the appointment list in different formats |           |     |  x   |   x   |
|Update the status of appointments                |     x     |     |  x   |       |
|List users with the admin role                   |     x     |     |      |       |
|Edit users with the admin role                   |     x     |     |      |       |
|Delete users with the admin role                 |     x     |     |      |       |
|List users with the doctor role                  |     x     |  x  |      |       |
|Edit users with the doctor role                  |     x     |  x  |      |       |
|Delete users with the doctor role                |     x     |  x  |      |       |
|List users with the patient role                 |     x     |  x  |      |       |
|Edit users with the patient role                 |     x     |  x  |      |       |
|Delete users with the patient role               |     x     |  x  |      |       |
|Filter listed users                              |     x     |  x  |      |       |
|Search in the user list                          |     x     |  x  |      |       |
|Export the user list in different formats        |     x     |  x  |      |       |
|View the system’s audit logs                     |     x     |     |      |       |
|View the system’s statistical graphs             |     x     |     |      |       |

## General Technical Features of the Project

* Layered Architecture in Backend with ASP.NET Core 8 WEB API
* Command Query Responsibility Segregation (CQRS) Pattern With MediatR
* Generic Repository and Unit of Work Pattern
* MS SQL Server Database
* Using Entity Framework Core 8 ORM
* Database Migrations with Code First Approach
* Advanced Role and Permission Based Authentication and Authorization with JWT
* JSON Based Multi-Language Support in Frontend with Ngx-Translate
* JSON Based Multi-Language Support in Backend
* Database Based Multi-Language Support for Some Project Components (Menu Items, Departments)
* Object to Object Mapping in Frontend with Dynamic Mapper
* Object to Object Mapping in Backend with Auto Mapper 
* Using Records as DTOs in data transfer in Backend
* Using PrimeNG as UI Library in Frontend
* Component Based Architecture in Frontend
* Some Custom Components like Advanced Table, Simple Table
* Logging Database Transactions with Audit Log
* Error Management with Global Error Handler in Frontend
* Error Management with Error Handler Middleware in Backend
* Request and Response Encryption Decryption with Encryption Decryption Middleware
* Logging API Operations with Serilog and Visualization with Seq
* Server Side Pagination, Searching and Filtering with PrimeNG Tables
* Multi-Format (PDF, XML, JSON, XLSX, CSV) Data Export for Tables
* Server Side Validation with Fluent Validation
* Client Side Validation with Fluent Validation TS
* Using Interceptors for Authorization, Encryption-Decryption and Spinner in Frontend
* Using Environments in Frontend for Deployment
* Using Environments in Backend with DotNetEnv for Deployment
* Docker and Docker Compose Support
* Using Eslint in Frontend for Code Consistency
* Response Compression in Backend
* <strike>Fake Data Generation with Bogus
* Using Indexes in Some Database Table Columns for Query Performance
* Health Check in Backend
* Response Caching with Redis in Backend
* Rate Limiting in Backend
* API Versioning in Backend
* State Management with NgRx in Frontend
* Theme Settings for Users in Frontend
* Using Nginx for Http Server
* Using SignalR for Real Time Data Communication
* Using Hangfire and Quartz Jobs for Timed E-Mail Sending
* Logging Frontend Errors with Serilog
* Unit Tests in Backend
* Using Charts in Frontend for Statistics
* Multi-Format Data Import in Frontend
* Viewing Appointments in PDF format with Ngx-Extented-Viewer
* Notifications with Rabbit MQ in Backend</strike>

## Backend Layer Folder Structure

- eAppointment.Backend.Application
    - Behaviors
    - Features
    - Mapping
    - Services
    - DependencyInjection.cs
    - eAppointment.Backend.Application.csproj
- eAppointment.Backend.Domain
    - Abstractions
    - Constants
    - Entities
    - Enums
    - Extensions
    - Helpers
    - eAppointment.Backend.Domain.csproj
- eAppointment.Backend.Infrastructure
    - Concretes
    - Configurations
    - Context
    - Migrations
    - Services
    - DependencyInjection.cs
    - eAppointment.Backend.Infrastructure.csproj
- eAppointment.Backend.WebAPI
    - Abstractions
    - Controllers
    - Filters
    - Helpers
    - Middlewares
    - Properties
    - Resources
    - .env
    - eAppointment.Backend.WebAPI.csproj
    - Helper.cs
    - Program.cs
    - web.config
- docker-compose.yml
- Dockerfile
- eAppointment.Backend.sln

## Tools and Technologies Used in the Backend

The list of all packages and tools used in backend is provided below, along with their version.

* General Technologies
  
  - ASP.NET Core 8 WEB API
 
* Nuget Packages
  - Ardalis.SmartEnum 8.0.0
  - AutoMapper 13.0.1
  - DotNetEnv 3.1.1
  - FluentValidation 11.9.2
  - FluentValidation.DependencyInjectionExtensions 11.9.2
  - MediatR 12.4.0
  - Microsoft.AspNetCore.Authentication.JwtBearer 8.0.8
  - Microsoft.AspNetCore.Http.Features 2.0.2
  - Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.8
  - Microsoft.AspNetCore.Identity.UI 8.0.8
  - Microsoft.EntityFrameworkCore.Design 8.0.8
  - Microsoft.EntityFrameworkCore.SqlServer 8.0.8
  - Microsoft.EntityFrameworkCore.Tools 8.0.8
  - Microsoft.Extensions.DependencyInjection.Abstractions 8.0.1
  - Microsoft.Extensions.Localization.Abstractions 8.0.8
  - Microsoft.VisualStudio.Azure.Containers.Tools.Targets 1.19.6
  - Newtonsoft.Json 13.0.3
  - Scrutor 4.2.2
  - Serilog 4.0.1
  - Serilog.AspNetCore 8.0.2
  - Serilog.Enrichers.AspNetCore 1.0.0
  - Serilog.Enrichers.ClientInfo 2.1.1
  - Serilog.Enrichers.Environment 3.0.1
  - Serilog.Enrichers.Process 3.0.0
  - Serilog.Enrichers.Thread 4.0.0
  - Serilog.Settings.Configuration 8.0.2
  - Serilog.Sinks.Seq 8.0.0
  - Swashbuckle.AspNetCore 6.7.3

## License

The MIT License (MIT)

## Screenshots

![Alt text](/screenshots/01-e-appointment.png)