#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["REPR.BFF/REPR.BFF.csproj", "REPR.BFF/"]
COPY ["REPR.Baskets/REPR.Baskets.csproj", "REPR.Baskets/"]
COPY ["REPR.Products/REPR.Products.csproj", "REPR.Products/"]
RUN dotnet restore "REPR.BFF/REPR.BFF.csproj"
COPY . .
WORKDIR "/src/REPR.Baskets"
RUN rm appsettings*.json
WORKDIR "/src/REPR.Products"
RUN rm appsettings*.json
WORKDIR "/src/REPR.BFF"
RUN dotnet build "REPR.BFF.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "REPR.BFF.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "REPR.BFF.dll"]