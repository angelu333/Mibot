# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

WORKDIR /src
RUN dotnet restore Mibot.sln

WORKDIR /src/Mibot
RUN dotnet publish -c Release -o /app

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
ENTRYPOINT ["dotnet", "Mibot.dll"]