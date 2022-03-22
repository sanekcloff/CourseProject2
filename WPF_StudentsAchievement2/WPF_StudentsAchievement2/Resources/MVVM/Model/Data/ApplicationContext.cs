using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WPF_StudentsAchievement2.Resources.MVVM.Model.Data
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Discipline> Disciplines;
        public DbSet<Grade> Grades;
        public DbSet<Group> Groups;
        public DbSet<Student> Students;
        public DbSet<Register> Registers;
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=PC-232-12\SQLEXPRESS;Initial Catalog=StudentsAchievement;User ID=U-19;Password=19$RPEe");
        }
    }
}
