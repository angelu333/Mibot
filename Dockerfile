# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia los archivos de la solución y restaura dependencias
COPY Mibot.sln ./
COPY Mibot/*.csproj ./Mibot/
RUN dotnet restore ./Mibot/Mibot.csproj

# Copia el resto de los archivos y publica
COPY . .
WORKDIR /src/Mibot
RUN dotnet publish -c Release -o /app

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
ENTRYPOINT ["dotnet", "Mibot.dll"] 