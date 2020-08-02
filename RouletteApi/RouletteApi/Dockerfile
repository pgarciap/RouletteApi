#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["RouletteApi/RouletteApi.csproj", "RouletteApi/"]
RUN dotnet restore "RouletteApi/RouletteApi.csproj"
COPY . .
WORKDIR "/src/RouletteApi"
RUN dotnet build "RouletteApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RouletteApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RouletteApi.dll"]