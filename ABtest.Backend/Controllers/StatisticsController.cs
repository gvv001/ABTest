using ABtest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABtest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {

        private PostgreeSqlContext db;

        private readonly ILogger<UserController> _logger;



        public StatisticsController(ILogger<UserController> logger, PostgreeSqlContext postgreeSqlContext)
        {
            _logger = logger;
            db = postgreeSqlContext;
        }








        [HttpGet("RollingRetention")]
        public IActionResult GetRollingRetention(UInt16 day)
        {
            double rollingRetention = 0;

            if (day == 0)
                return BadRequest("Количество дней должно быть больше 0 ");

            double countUsersReturnedAfterXDay = db.User.Where(c => c.LifespanInDays >= day).Count();
            double countUsersRegisteredEarlyXDay = db.User.Where(c => c.DateRegistration <= DateTime.Now.AddDays(-day)).Count();

            if(countUsersRegisteredEarlyXDay != 0)
                rollingRetention = countUsersReturnedAfterXDay / countUsersRegisteredEarlyXDay * 100;

            rollingRetention = Math.Round(rollingRetention, 2);


            return Ok(rollingRetention);

        }



        [HttpGet("UsersLifespan")]
        public string GetUsersLifespan()
        {


            var result = db.User.GroupBy(u => u.LifespanInDays).Select(u =>
                new UserLifeSpan
                {
                    LifeSpan = u.Key,
                    Count = u.Count()

                }
            ).OrderBy(c => c.Count);


            return SerializeTableForHightCharts(result);
        }


        private string SerializeTableForHightCharts(IEnumerable<UserLifeSpan> UsersLifeSpan)
        {

            //Для hicharts нужен след формат 
            // [
            //   [дата в юниксе],[значение],  
            //   [дата в юниксе],[значение]  
            // ]
 

            string result = String.Empty;
            string comma = String.Empty;


            foreach(UserLifeSpan userLifeSpan in UsersLifeSpan) 
            {

                result += $"{comma}[{userLifeSpan.LifeSpan},{userLifeSpan.Count.ToString().Replace(",", ".")}]";
                comma = ",";
            }

            result = $"[{result}]";


            return result;

        }






    }
}
