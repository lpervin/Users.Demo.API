FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Users.Demo.API/Users.Demo.API.csproj", "Users.Demo.API/"]
COPY ["Users.Application/Users.Application.csproj", "Users.Application/"]
COPY ["Users.Core/Users.Core.csproj", "Users.Core/"]
RUN dotnet restore "Users.Demo.API/Users.Demo.API.csproj"
COPY . .
WORKDIR "/src/Users.Demo.API"
RUN dotnet build "Users.Demo.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Users.Demo.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.Demo.API.dll"]
