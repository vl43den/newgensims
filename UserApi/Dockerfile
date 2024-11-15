# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy the project files and restore the dependencies
COPY ["UserApi/UserApi.csproj", "UserApi/"]
RUN dotnet restore "UserApi/UserApi.csproj"

# Publish the app to the /app directory in the container
COPY . .
WORKDIR "/src/UserApi"
RUN dotnet publish "UserApi.csproj" -c Release -o /app/publish

# Use the official .NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published app from the build container
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "UserApi.dll"]
