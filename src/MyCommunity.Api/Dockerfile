FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY [".", "."]
run ls -la .
RUN dotnet restore "src/MyCommunity.Api/MyCommunity.Api.csproj"
#COPY . .
#WORKDIR "/src/WebApplication1"
RUN dotnet build "src/MyCommunity.Api/MyCommunity.Api.csproj"
RUN dotnet publish "src/MyCommunity.Api/MyCommunity.Api.csproj" -c Release -o /app/publish


#FROM build AS publish
#RUN dotnet publish "MyCommunity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MyCommunity.Api.dll"]


#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#WORKDIR /dotnetapp
#COPY ./bin/Docker .
#ENV ASPNETCORE_URLS http://*:5000
#ENV ASPNETCORE_ENVIRONMENT docker
#ENTRYPOINT ["dotnet", "MyCommunity.Api.dll"]