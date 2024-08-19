FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Code.Kata.9.Api/Code.Kata.9.Api.csproj", "Code.Kata.9.Api/"]
RUN dotnet restore "Code.Kata.9.Api/Code.Kata.9.Api.csproj"
COPY . .
WORKDIR "/src/Code.Kata.9.Api"
RUN dotnet build "Code.Kata.9.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Code.Kata.9.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Code.Kata.9.Api.dll"]
