#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["src/SocialMedia.API/SocialMedia.API.csproj", "src/SocialMedia.API/"]
COPY ["src/SocialMedia.Application/SocialMedia.Application.csproj", "src/SocialMedia.Application/"]
COPY ["src/SocialMedia.Domain/SocialMedia.Domain.csproj", "src/SocialMedia.Domain/"]
COPY ["src/SocialMedia.Infrastructure/SocialMedia.Infrastructure.csproj", "src/SocialMedia.Infrastructure/"]
RUN dotnet restore "src/SocialMedia.API/SocialMedia.API.csproj"

COPY ["tests/SocialMedia.Domain.UnitTests/SocialMedia.Domain.UnitTests.csproj", "tests/SocialMedia.Domain.UnitTests/"]
RUN dotnet restore "tests/SocialMedia.Domain.UnitTests/SocialMedia.Domain.UnitTests.csproj"

COPY ["tests/SocialMedia.Application.UnitTests/SocialMedia.Application.UnitTests.csproj", "tests/SocialMedia.Application.UnitTests/"]
RUN dotnet restore "tests/SocialMedia.Application.UnitTests/SocialMedia.Application.UnitTests.csproj"

COPY . .
WORKDIR "/src/src/SocialMedia.API"
RUN dotnet build "SocialMedia.API.csproj" -c Release -o /app

#Building Domain UnitTests
WORKDIR "/src/tests/SocialMedia.Domain.UnitTests"
RUN dotnet build "SocialMedia.Domain.UnitTests.csproj" -c Release -o /app
RUN dotnet test "SocialMedia.Domain.UnitTests.csproj" --logger "trx;LogFileName=SocialMedia.Domain.UnitTests.trx" --collect:"XPlat Code Coverage" --results-directory /app/testresults

#Building Application UnitTests
WORKDIR "/src/tests/SocialMedia.Application.UnitTests"
RUN dotnet build "SocialMedia.Application.UnitTests.csproj" -c Release -o /app
RUN dotnet test "SocialMedia.Application.UnitTests.csproj" --logger "trx;LogFileName=SocialMedia.Application.UnitTests.trx" --collect:"XPlat Code Coverage" --results-directory /app/testresults

FROM build AS publish
WORKDIR "/src/src/SocialMedia.API"
RUN dotnet publish "SocialMedia.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SocialMedia.API.dll"]