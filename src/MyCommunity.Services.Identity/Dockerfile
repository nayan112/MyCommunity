FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5051
ENV ASPNETCORE_ENVIRONMENT docker

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY [".", "."]
#run ls -la .
RUN dotnet restore "src/MyCommunity.Services.Identity/MyCommunity.Services.Identity.csproj"
RUN dotnet build "src/MyCommunity.Services.Identity/MyCommunity.Services.Identity.csproj"
RUN dotnet publish "src/MyCommunity.Services.Identity/MyCommunity.Services.Identity.csproj" -c Release -o /app/publish


#FROM build AS publish
#RUN dotnet publish "MyCommunity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MyCommunity.Services.Identity.dll"]

