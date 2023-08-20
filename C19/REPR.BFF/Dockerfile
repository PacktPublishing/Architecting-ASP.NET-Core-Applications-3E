FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
EXPOSE 5185

ENV ASPNETCORE_URLS=http://+:5185

USER app
FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
ARG configuration=Release
WORKDIR /src
# COPY ["C19/REPR.BFF/REPR.BFF.csproj", "REPR.BFF/"]
# COPY ["C18/REPR/Web/Web.csproj", "REPR.Web/"]
COPY C19/REPR.BFF ./REPR.BFF
COPY C18/REPR/Web ./REPR.Web
RUN dotnet remove "REPR.BFF/REPR.BFF.csproj" reference "..\..\C18\REPR\Web\Web.csproj"
RUN dotnet add "REPR.BFF/REPR.BFF.csproj" reference "REPR.Web/Web.csproj"
RUN dotnet restore "REPR.BFF/REPR.BFF.csproj"
WORKDIR "/src/REPR.Web"
RUN rm appsettings*.json
WORKDIR "/src/REPR.BFF"
RUN dotnet build "REPR.BFF.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "REPR.BFF.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "REPR.BFF.dll"]
