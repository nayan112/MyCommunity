## MyCommunity
### -Microservice Test Project


Steps to Run the project
1. Build the solution. In case of failure, make sure individual web projects build.
2. On successful build, publish each individual web projects with command

	> dotnet restore ./MyCommunity.sln && dotnet publish ./MyCommunity.sln -c Release -o ./bin/Docker

3. Move to the root and run following commands

   > docker-compose run start-dependencies
   
   > docker-compose up   

Some other usefull commands used in the course of development
dotnet new sln
md src
cd src
dotnet new webapi MyCommunity.Api
dotnet new webapi MyCommunity.Services.Identity
dotnet new webapi MyCommunity.Services.Activities

docker pull rabbitmq
docker run -p 15672:15672 rabbitmq
docker run -d -p 15672:15672 -p 5672:5672 rabbitmq:3-management
docker run -d -p 27017:27017 mongo
