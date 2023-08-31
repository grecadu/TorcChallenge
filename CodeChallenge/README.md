How to start up 

The api is build an run, to create the database we have 2 options Migrations or Query 

=> Migrations 

Set up the CodeChallenge as start up project go to Tools Nugget Package Management => Package Manager Console and open a new console 
run the following command Update-Database the database will be created 

Then run the SeedData.sql script attached in the scripts folder.


SQL Files

Run the CreateProject.sql to generate entities and data for the project.


=> How to get autheticated 

We have 2 options Swagger and Postman 

one is using swagger 

There is a method in the Security Controller GetToken that returns the jwt token for testing 

Swagger has a locker in the top of the screen what allow you to past that token with ""

Two using postman 

There is a folder called Postman Collection you can import the collection 
Execute the first method is the GetToken and after that automatically you can interact with the others methods, the token will be set automatically by the first request
