using ABtest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Converters;

namespace ABtest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private PostgreeSqlContext db;

        private readonly ILogger<UserController> _logger;



        public UserController(ILogger<UserController> logger, PostgreeSqlContext postgreeSqlContext)
        {
            _logger = logger;
            db = postgreeSqlContext;
        }




        [HttpGet("GetAll")]
        public IActionResult GetAllUsers()
        {

            if (db.User.Any())
            {
                return new JsonResult(db.User.ToList());
            }
            else
                return BadRequest("Пользователи не найдены");
        }




        [HttpPost("Save")]
        public async Task<IActionResult> Post([FromBody] JsonElement users)
        {
            string json = JsonSerializer.Serialize(users);

            var dateFormat = "dd.MM.yyyy";
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = dateFormat };


            User[] Users = Newtonsoft.Json.JsonConvert.DeserializeObject<User[]>(json, dateTimeConverter);

            db.User.UpdateRange(Users);
            await db.SaveChangesAsync();


            return Ok(Users);



        }


    }
}
