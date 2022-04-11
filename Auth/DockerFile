#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Auth/Auth.csproj", "Auth/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]
RUN dotnet restore "Auth/Auth.csproj"
COPY . .
WORKDIR "/src/Auth"
RUN dotnet build "Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.dll"]
