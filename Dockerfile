FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files for restore (only what we need to run)
COPY src/ChordCraft.Core/ChordCraft.Core.csproj src/ChordCraft.Core/
COPY src/ChordCraft.Infrastructure/ChordCraft.Infrastructure.csproj src/ChordCraft.Infrastructure/
COPY src/ChordCraft.Api/ChordCraft.Api.csproj src/ChordCraft.Api/
COPY src/ChordCraft.Client/ChordCraft.Client.csproj src/ChordCraft.Client/

RUN dotnet restore src/ChordCraft.Api/ChordCraft.Api.csproj

# Copy source and publish
COPY src/ src/
RUN dotnet publish src/ChordCraft.Api/ChordCraft.Api.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["sh", "-c", "dotnet ChordCraft.Api.dll --urls http://+:${PORT:-8080}"]
