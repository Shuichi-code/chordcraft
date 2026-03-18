FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ChordCraft.sln .
COPY src/ChordCraft.Core/*.csproj src/ChordCraft.Core/
COPY src/ChordCraft.Infrastructure/*.csproj src/ChordCraft.Infrastructure/
COPY src/ChordCraft.Api/*.csproj src/ChordCraft.Api/
COPY src/ChordCraft.Client/*.csproj src/ChordCraft.Client/
RUN dotnet restore

COPY . .
RUN dotnet publish src/ChordCraft.Api -c Release -o /app
RUN dotnet publish src/ChordCraft.Client -c Release -o /client
RUN cp -r /client/wwwroot/* /app/wwwroot/

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ChordCraft.Api.dll"]
