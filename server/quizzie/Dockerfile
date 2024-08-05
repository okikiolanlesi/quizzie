#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
ARG CERT_PASSWORD
RUN dotnet dev-certs https -ep /app/https/aspnetapp.pfx -p "$CERT_PASSWORD"

EXPOSE 8080
EXPOSE 8081

# Pass the vault token
ARG VAULT_TOKEN
ENV VAULT_TOKEN=${VAULT_TOKEN}

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_HTTPS_PORT=
 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./server/quizzie/quizzie.csproj", "quizzie/"]
RUN dotnet restore "./quizzie/quizzie.csproj"
COPY ./server/quizzie/ ./quizzie/
WORKDIR "/src/quizzie"
RUN dotnet build "quizzie.csproj" -c "$BUILD_CONFIGURATION" -o /app/build
 
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "quizzie.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false
 
# Add a step to list the contents of the publish directory for debugging
RUN ls /app/publish
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Add the certificate
COPY --from=base /root/.aspnet/https/aspnetapp.pfx .

# Set the environment variable for the certificate and ASP.NET Core
ARG CERT_PASSWORD
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=$CERT_PASSWORD
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/aspnetapp.pfx
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+;https://+
 
ENTRYPOINT ["dotnet", "quizzie.dll"]