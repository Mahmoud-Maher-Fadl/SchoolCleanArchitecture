# School Clean Architecture API
  **This is a School Clean Architecture API built with ASP.NET Core 7, Entity Framework Core, and Microsoft SQL Server.    
    The API is designed to manage various aspects of a school, including Departments, Students, Subjects, and Employees.  
    It follows a clean architecture pattern to ensure maintainability, scalability, and separation of concerns.**

  ## Table of Contents
    Features
    Getting Started
    Prerequisites
    Installation
    Project Structure
    Usage
    API Endpoints
    Contributing
    License
  
  ## Features
  - **Department Management:** Create, update, and delete departments.
  - **Student Management:** Create, update, and delete student records.
  - **Subject Management:** Manage subjects offered in the school.
  - **Employee Management:** Keep track of school employees.
  - **Clean Architecture:** Follows a clean architecture pattern with clear separation of concerns.
  - **Entity Framework Core:** Utilizes Entity Framework Core for data access.
  - **Authentication and Authorization:** Implement authentication and authorization mechanisms.
  - **Swagger Documentation:** Provides API documentation using Swagger.
  - **Microsoft SQL Server:** Stores data in a Microsoft SQL Server database.

  ## Dynamic Search and Ordering

  This API provides dynamic search and ordering features, allowing you to customize the results returned by various endpoints.
  You can use query parameters to specify search criteria and order the results based on your preferences.
  Here are some of the features you can utilize:
  
  - **Dynamic Search:** Use query parameters to filter results based on specific criteria.
                        For example, you can search for students by their name or employees by their role.
  
  - **Dynamic Ordering:** Specify query parameters to order the results based on a specific field.
                          You can sort students, employees, or other data entities as needed.
      
  - **Complex Queries:** You can build complex queries dynamically based on the provided parameters.
                         For instance, you can construct dynamic `OrderBy` clauses for different field names.
  
  - **Pagination:** To handle large result sets, you can use pagination parameters (e.g., `page` and `pageSize)
                        to limit the number of results returned in a single request.

  ## Getting Started
  ### Prerequisites
    Before you begin, make sure you have the following tools and frameworks installed:
    .NET 7
    Microsoft SQL Server
    Entity Framework Core
    Your favorite code editor (e.g., Visual Studio, JetBrains Rider)
    
  ### Installation
    Clone the repository:
        git clone https://github.com/Mahmoud-Maher-Fadl/SchoolCleanArchitecture
        
    Create the SQL Server database:
        Replace The DataBase Name In The Connection String with your Own.
    
    Build and run the project:
      Throw Your Code Editor


  ## Project Structure
    The project follows a clean architecture pattern, which includes the following layers:
    
    Application: Contains application logic and use cases.
    Domain: Defines domain entities and business rules.
    Infrastructure: Implements data access and external services.
    Web: Provides API controllers, authentication, and presentation logic.
      
  ## Usage
    You can use the API to manage departments, students, subjects, and employees in a school.
    To access and use the API, please refer to the API documentation provided via Swagger.

  ## API Endpoints
    For detailed information on available API endpoints and how to use them,
    please refer to the Swagger documentation.
    You can access it at https://localhost:5001/swagger (or http://localhost:5000/swagger) after starting the application.

  ## Contributing
    Contributions to this project are welcome. If you would like to contribute, please follow these steps:

    1.Fork the repository.
    2.Create a new branch for your feature or bug fix.
    3.Make your changes and commit them.
    4.Submit a pull request to the main repository.
  **Please ensure your code follows the project's coding standards and includes appropriate tests.**





