FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Messages/Messages.csproj", "Messages/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]

RUN dotnet restore "Messages/Messages.csproj"
COPY . .
WORKDIR "/src/Messages"
RUN dotnet build "Messages.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Messages.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Messages.dll"]