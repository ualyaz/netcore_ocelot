# api gateway with ocelot


## Getting Started
Sending incoming requests to the relevant address using ocelot on C# ASPNET CORE

GATEWAY project


install package ocelot
```C#
Install-Package Ocelot -Version 17.0.0
```

startup.cs setup ocelot 
```C#
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

 public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ...
            services.AddOcelot();
            ...
        }

        async public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            ...
            await app.UseOcelot();
            ...
        }
    }
```

ocelot.json configure ocelot routes
```JSON
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/User",  //subsub- service request endpoint 
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost", //sub-service location(url) 
          "Port": 53194 //sub-service1 url port 
        }
      ],
      "UpstreamPathTemplate": "/department-user", // gateway request endpoint
      "UpstreamHttpMethod": [ "Get" ] // get, post, put, delete
    },
    //... configure other sub service routes 
  ],
  "GlobalConfiguration": {
    "BaseUrl": "http://localhost:53079" //gateway service location (url) 
  }
}

```

SUBSERVICE 

API.USER (Sub-Service) Project

Create random user 

```C#

  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
    {
        private static readonly string[] Departments = new[]
        {
            "IT", "Digital Marketing", "HR", "Security", "Food", "Energy", "Textile"
        };
        private static readonly string[] Names = new[]
       {
            "John", "Oliver", "Harry", "George", "Noah", "Jack", "Leo"
        };
        private static readonly string[] Surnames = new[]
               {
            "Oscar", "Smith", "Jones", "Taylor", "Brown", "Williams", "Wilson"
        };

        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            var rng = new Random();
            //create random entites
            return Enumerable.Range(1, 7).Select(index => new UserModel
            {
                Date = DateTime.Now.AddDays(index),
                Name = Names[rng.Next(Names.Length)],
                Surname = Surnames[rng.Next(Surnames.Length)],
                Department = Departments[rng.Next(Departments.Length)]
            })
            .ToArray();
        }
    } 
```
