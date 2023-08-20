# How to start both projects?

From the `C19` directory:

```bash
# In one terminal, start Chapter 18 project
cd ../C18/REPR/Web
dotnet run

# In another terminal, start Chapter 19 project
dotnet run --project REPR.BFF/REPR.BFF.csproj
```

> It is important to note that `REPR.BFF` depends on `C18/REPR/Web` so .NET may atempt to build the project.
> If this happens while the application runs, you'll get an error similar to this: `The process cannot access the file '...\C18\REPR\ 
Web\bin\Debug\net8.0\Web.exe' because it is being used by another process.`.
> When this happens, hit `ctrl+C` to stop the app, wait for the build, then restart the service.
