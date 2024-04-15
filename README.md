# hbs-homelib
- A blank Solution file created using:
  ```bash
  dotnet new solution -n hbs-homelib -o hbs-homelib
  ```
- A classlib project created using:
   ```bash
	  dotnet new classlib -n homelib -o homelib -f netstandard2.1
	  dotnet add package Microsoft.EntityFrameworkCore
	  dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Microsoft.EntityFrameworkCore.InMemory
   ```
- The hbs-lib solution is created to demonstrate the use of Entity Framework Core in a class library project.
- The homelib class library project is created to demonstrate the use of Entity Framework Core in a class library project.
- The homelibTests project demonstrates the use of InMemory database for testing as well as the Moq library for mocking.
- The homelibCli project (To be created at a later commit) uses the homelib class library to demonstrate the use of Entity Framework Core with sqlite database configuration in a console application.