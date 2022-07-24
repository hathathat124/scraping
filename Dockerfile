#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app
EXPOSE 80
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . ./
RUN dotnet restore ./ScrapingAPI.sln
COPY . .
WORKDIR "/src/"

RUN dotnet build ScrapingAPI.sln -c Release -o /app/build
FROM build AS publish
RUN dotnet publish ScrapingAPI.sln -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ScrapingAPI.dll"]

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet ScrapingAPI.dll

