FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["User/User.csproj", "User/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]

RUN dotnet restore "User/User.csproj"
COPY . .
WORKDIR "/src/User"
RUN dotnet build "User.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.dll"]