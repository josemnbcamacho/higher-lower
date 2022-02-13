FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["HigherOrLower.API/HigherOrLower.API.csproj", "HigherOrLower.API/"]
RUN dotnet restore "HigherOrLower.API/HigherOrLower.API.csproj"
COPY . .
WORKDIR "/src/HigherOrLower.API"
RUN dotnet build "HigherOrLower.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HigherOrLower.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HigherOrLower.API.dll"]