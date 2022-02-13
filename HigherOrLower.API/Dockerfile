FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HigherOrLower.API/HigherOrLower.API.csproj", "HigherOrLower.API/"]
RUN dotnet restore "HigherOrLower.API/HigherOrLower.API.csproj"
COPY . .
WORKDIR "/src/HigherOrLower.API"
RUN dotnet build "HigherOrLower.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HigherOrLower.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HigherOrLower.API.dll"]
