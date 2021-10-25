using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABtest.Models
{
    public class PostgreeSqlContext: DbContext
    {

        public IConfiguration Configuration { get; }

        public DbSet<User> User { get; set; }


        public PostgreeSqlContext(DbContextOptions<PostgreeSqlContext> options) : base(options)
        {
             //Database.EnsureCreated();
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

            optionsBuilder.UseNpgsql(options => {

                optionsBuilder.LogTo(System.Console.WriteLine);

            });


        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.LifespanInDays).HasComputedColumnSql("\"DateLastActivity\" - \"DateRegistration\"", true);
            modelBuilder.Entity<User>().HasIndex(x => x.LifespanInDays);
            base.OnModelCreating(modelBuilder);
        }

    }
}
