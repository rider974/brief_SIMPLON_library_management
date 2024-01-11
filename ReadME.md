<h1>BRIEF SIMPLON Week from 08/01/2024 to 12/01/2024</h1>

<h2>Context </h2>

<p>Create System Management Book with C# & MONGODB </p>

<h2>1 - Install MONGODB Database</h2>

<p>Create ATLAS Account and install MONGODB  (link attached) : https://www.mongodb.com/cloud/atlas/register 
OR Install locally with Compass : https://www.mongodb.com/products/tools/compass (the following steps will use Compass ) </p>


<h2>2 - Create Database (Compass Desktop App)</h2>

<p>Add in appsettings.json : 
  -> Name of the database to create 
  -> the name of the collections 
  -> the url to connect to the database ConnectionURI (in local we use : "mongodb://localhost:27017")
  
  ![image](https://github.com/rider974/brief_SIMPLON_library_management/assets/116554314/23531829-30f5-4e29-9cbe-283a8c31d7a4)
</p>

<h2>3 - Connect the Application to the database</h2>

<p>Install the driver MONGO DB for c# .NET App and Add the following lines to Programm.cs

````
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

var serverAccess = builder.Configuration.GetSection("MongoDBSettings:ConnectionURI").Value;

builder.Services.AddSingleton<IMongoClient>(new MongoClient(serverAccess));

````

![image](https://github.com/rider974/brief_SIMPLON_library_management/assets/116554314/489cbb6d-0c82-4299-8cde-4fac7449dfe1)
</p>

<h2>4 - Launch MONGO DB and RUN the App </h2>

