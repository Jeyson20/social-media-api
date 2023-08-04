FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build

COPY ./*.sln ./
COPY ./src ./src
COPY ./tests ./tests
RUN dotnet restore

COPY . .
RUN dotnet build ./src/SocialMedia.API/*.csproj -c release -o /app
RUN dotnet build ./tests/SocialMedia.Domain.UnitTests/*.csproj -c release -o /app
RUN dotnet build ./tests/SocialMedia.Application.UnitTests/*.csproj -c release -o /app
RUN dotnet test --logger "trx;LogFileName=UnitTests.trx" --collect:"XPlat Code Coverage" --results-directory /app/testresults --no-restore 

FROM build as publish
RUN dotnet publish ./src/SocialMedia.API/*.csproj -c release -o /app/publish --no-restore

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialMedia.API.dll"]


