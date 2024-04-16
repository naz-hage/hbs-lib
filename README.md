# hbs-lib

This repository contains the following projects:

- **hbs-lib**: This solution demonstrates the use of Entity Framework Core in a class library project. It was created using the command `dotnet new solution -n hbs-homelib -o hbs-homelib`.

- **homelib**: The homelib class library project demonstrates the use of Entity Framework Core. It was created using the commands `dotnet new classlib -n homelib -o homelib -f netstandard2.1`.
The dependencies for the homelib project were added using the following commands:
  ```bash
  
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.Sqlite
  dotnet add package Microsoft.EntityFrameworkCore.InMemory
  ```

- **homelibTests**: This project demonstrates the use of an InMemory database for testing, as well as the Moq library for mocking.

- **homelibCli** (To be created at a later commit): This project will use the homelib class library to demonstrate the use of Entity Framework Core with SQLite database configuration in a console application.

- The **Design** folder contains the architecture and design documentation.

For more details about the project progress, please refer to [HISTORY.md](./HISTORY.md).
