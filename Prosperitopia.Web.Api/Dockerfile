FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7117
ENV ASPNETCORE_URLS=http://+:7117
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Prosperitopia.Web.Api/Prosperitopia.Web.Api.csproj", "Prosperitopia.Web.Api/"]
COPY ["Prosperitopia.Application/Prosperitopia.Application.csproj", "Prosperitopia.Application/"]
COPY ["Prosperitopia.DataAccess/Prosperitopia.DataAccess.csproj", "Prosperitopia.DataAccess/"]
COPY ["Prosperitopia.Domain/Prosperitopia.Domain.csproj", "Prosperitopia.Domain/"]
RUN dotnet restore "./Prosperitopia.Web.Api/./Prosperitopia.Web.Api.csproj"
COPY . .
WORKDIR "/src/Prosperitopia.Web.Api"
RUN dotnet build "./Prosperitopia.Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Prosperitopia.Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Prosperitopia.Web.Api.dll"]