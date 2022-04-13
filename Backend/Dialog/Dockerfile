FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Dialog/Dialog.csproj", "Dialog/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["BusinessLogic/BusinessLogic.csproj", "BusinessLogic/"]

RUN dotnet restore "Dialog/Dialog.csproj"
COPY . .
WORKDIR "/src/Dialog"
RUN dotnet build "Dialog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dialog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dialog.dll"]