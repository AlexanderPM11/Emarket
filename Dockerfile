FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

# Copy solution file and project files first to leverage Docker cache
COPY ["Emarket.sln", "./"]
COPY ["Emarket/WebApp.Emarket.csproj", "Emarket/"]
COPY ["Aplication/Emarket.Core.Aplication.csproj", "Aplication/"]
COPY ["Emarket.Domain/Emarket.Core.Domain.csproj", "Emarket.Domain/"]
COPY ["Emarket.Infrastructure.Persistence/Emarket.Infrastructure.Persistence.csproj", "Emarket.Infrastructure.Persistence/"]
COPY ["percisten/percisten.csproj", "percisten/"]

# Restore dependencies
RUN dotnet restore

# Copy the remaining source files and build
COPY . .
WORKDIR "/src/Emarket"
RUN dotnet build "WebApp.Emarket.csproj" -c Release -o /app/build

# Publish the build artifact
FROM build AS publish
RUN dotnet publish "WebApp.Emarket.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Generate final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set default ASP.NET Core URLs to bind to port 80
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Configure a persistent volume for uploaded product images
VOLUME ["/app/wwwroot/Image"]

ENTRYPOINT ["dotnet", "WebApp.Emarket.dll"]
