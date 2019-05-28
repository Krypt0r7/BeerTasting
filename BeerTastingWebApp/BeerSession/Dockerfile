FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["BeerSession/BeerSession.csproj", "BeerSession/"]
RUN dotnet restore "BeerSession/BeerSession.csproj"
COPY . .
WORKDIR "/src/BeerSession"
RUN dotnet build "BeerSession.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BeerSession.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BeerSession.dll"]