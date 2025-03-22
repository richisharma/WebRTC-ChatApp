 
  
 # Use official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -o out

# Use .NET runtime image for final container
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy built files from build stage
COPY --from=build /app/out ./

# Expose the application port
EXPOSE 7189

# Set entry point
ENTRYPOINT ["dotnet", "MessagingService.dll"]