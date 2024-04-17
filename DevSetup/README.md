`DevSetup` will help install development tools needed for this project.

1. Change the PowerShell execution policy to allow the installation script to run. Run the following command:

```
Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope Process
```
This command will allow the installation script to run. Once the installation is complete, the execution policy will revert to its original state.

2. Run the following command to install the tools:
```
.\DevSetup.ps1
```
This command will install the following tools:

| App name           | Target version | 
|--------------------|----------------|
| Dotnet Runtime     | 8.0.1          |
| Visual Studio 2022 | 17.0.0         |
| Git for Windows    | 2.44.0         |
| Powershell         | 7.4.2          |
| Visual Studio Code | 1.88.1.0       |
| Nuget              | 6.9.1.3        | 
| SysInternals       | 2.90.0.0       |
| SQLite             | 3.45.2         |
| SQLite tools       | 3.45.2         |
