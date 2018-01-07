## MyCommunity
### -Microservice Test Project


Steps to Run the project
1. Build the solution. In case of failure, make sure individual web projects build.
2. On successful build, publish each individual web projects with command

	> dotnet restore ./MyCommunity.sln && dotnet publish ./MyCommunity.sln -c Release -o ./bin/Docker

3. Move to the root and run following commands

   > docker-compose run start-dependencies
   
   > docker-compose up   
   
4. Once all servies running, you'll get the ports of each service.
5. RabbitMQ & MongoDB is also running in container along with services.
6. For Rabbit MQ the urls is http://localhost:15672/ & the username/password is guest/guest
7. Mongo db is running in port 27017. For navigation, RoboMongo can be used without any installation
8. On successfull running, urls can be validated with Advance Rest Client as below
	1. POST to http://localhost:5000/users/register with body  {"email":"user@user.com","password":"password","name":"user"}
    2. POST to http://localhost:5051/login with body {"email":"user@user.com","password":"password"}
    3. Save the JSON Token & check the activities with GET request to http://localhost:5000/activities & header Authorization<> Bearer <token>
    4. To add activity
      

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
