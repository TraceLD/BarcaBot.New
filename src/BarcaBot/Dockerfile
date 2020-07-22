FROM mcr.microsoft.com/dotnet/core/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:5.0-buster AS build
WORKDIR /src
COPY ["BarcaBot/BarcaBot.csproj", "BarcaBot/"]
COPY ["BarcaBot.Core/BarcaBot.Core.csproj", "BarcaBot.Core/"]
COPY ["BarcaBot.Infrastructure/BarcaBot.Infrastructure.csproj", "BarcaBot.Infrastructure/"]
COPY [".nuget/nuget.config", ".nuget/"]
RUN dotnet restore "BarcaBot/BarcaBot.csproj" --configfile .nuget/nuget.config

COPY . .
WORKDIR "/src/BarcaBot"
RUN dotnet publish "BarcaBot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "BarcaBot.dll"]