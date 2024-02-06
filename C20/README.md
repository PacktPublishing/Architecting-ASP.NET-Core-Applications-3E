# How to build and start the apps?

From the `C20` directory:

```bash
# In one terminal, to start the Modular monolith
dotnet run --project applications/aggregator/API/API.csproj

# In a second terminal, to start the BFF
dotnet run --project applications/gateway/BFF/BFF.csproj
```
