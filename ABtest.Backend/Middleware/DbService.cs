using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABtest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ABtest.Services
{
    public class DbService
    {

        private PostgreeSqlContext db;


        public DbService(PostgreeSqlContext postgreeSqlContext)
        {
            db = postgreeSqlContext;

        }


        public void AddUser()
        {

            //Заполняем таблицу если нет пользователей 

            if (!db.User.Any())
            {

                var rand = new Random();

                //Количество добавляемых пользователей
                int userCount = 5;

                for (int i = 1; i <= userCount; i++)
                {

                    DateTime date = DateTime.Now;
                    int days = rand.Next(100);

                    User user = new User()
                    {
                        DateRegistration = DateTime.Now.AddDays(-days),
                        DateLastActivity = date.AddDays(-days + rand.Next(days))
                    };

                    db.User.AddAsync(user);


                }

                db.SaveChanges();
            }

        }



        



    }
}
