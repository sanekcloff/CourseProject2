using System;
using Microsoft.EntityFrameworkCore;

namespace WPF_StudentsAchievement2.Resources.MVVM.Models.Data
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Discipline> Disciplines { get; set; }
        //public DbSet<Grade> Grades { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Register> Registers { get; set; }

        public ApplicationContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StudentsAchievemnet;Trusted_Connection=True;");
        }
    }
}
