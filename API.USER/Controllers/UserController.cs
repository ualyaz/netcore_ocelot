using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.USER.Controllers
{
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


        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<UserModel> Get()
        {
            var rng = new Random();
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
}
