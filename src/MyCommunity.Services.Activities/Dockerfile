FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://*:5050
ENV ASPNETCORE_ENVIRONMENT docker

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY [".", "."]
run ls -la .
RUN dotnet restore "src/MyCommunity.Services.Activities/MyCommunity.Services.Activities.csproj"
RUN dotnet build "src/MyCommunity.Services.Activities/MyCommunity.Services.Activities.csproj"
RUN dotnet publish "src/MyCommunity.Services.Activities/MyCommunity.Services.Activities.csproj" -c Release -o /app/publish


#FROM build AS publish
#RUN dotnet publish "MyCommunity.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MyCommunity.Services.Activities.dll"]



#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#WORKDIR /dotnetapp
#COPY ./bin/Docker .
#ENV ASPNETCORE_URLS http://*:5050
#ENV ASPNETCORE_ENVIRONMENT docker
#ENTRYPOINT ["dotnet", "MyCommunity.Services.Activities.dll"]