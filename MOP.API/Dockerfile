FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["MOP.API/MOP.API.csproj", "MOP.API/"]
RUN dotnet restore "MOP.API/MOP.API.csproj"
COPY . .
WORKDIR "/src/MOP.API"
RUN dotnet build "MOP.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MOP.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MOP.API.dll"]