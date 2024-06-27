#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApiMovies/WebApiMovies.csproj", "WebApiMovies/"]
RUN dotnet restore "WebApiMovies/WebApiMovies.csproj"
COPY . .
WORKDIR "/src/WebApiMovies"
RUN dotnet build "WebApiMovies.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApiMovies.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApiMovies.dll"]