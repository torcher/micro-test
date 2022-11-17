#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["hhe-service/hhe-service.csproj", "hhe-service/"]
RUN dotnet restore "hhe-service/hhe-service.csproj"
COPY . .
WORKDIR "/src/hhe-service"
RUN dotnet build "hhe-service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "hhe-service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "hhe-service.dll"]