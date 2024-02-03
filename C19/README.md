# How to start the project using Docker?

## Configure HTTPS on Windows

> Reference: https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https

```powershell
dotnet dev-certs https -ep "$env:APPDATA\ASP.NET\Https\adpg-net8-chapter-19.pfx" -p devpassword
dotnet dev-certs https --trust
```

## Build and start the apps

From the `C19` directory:

```bash
docker-compose up
```

To shut it down, hit `ctrl+c`, then run the following to destroy the containers:

```bash
docker-compose down
```

# How to start both projects manually?

From the `C19` directory:

```bash
# In one terminal
cd Baskets
dotnet run

# In a second terminal
cd Products
dotnet run

# In a third terminal
cd BFF
dotnet run
```

# How to start the project from Visual Studio?

You can use the `Docker Compose` target from Visual Studio and start the solution as usual (using `F5` or `ctrl+F5`, for example).
