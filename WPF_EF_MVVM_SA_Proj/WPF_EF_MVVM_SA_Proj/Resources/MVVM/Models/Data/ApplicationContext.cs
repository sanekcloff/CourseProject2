using System;
using Microsoft.EntityFrameworkCore;

namespace WPF_EF_MVVM_SA_Proj.Resources.MVVM.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Register> Registers { get; set; }

        public ApplicationContext() => Database.EnsureCreated();
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SAMVVMEF;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-I8L1GP6;Initial Catalog=SAMVVMEF;Integrated Security = True");
            //optionsBuilder.UseSqlServer(@"Data Source=PC-232-12\SQLEXPRESS;Initial Catalog=SAMVVMEF;Integrated Security = True;User ID=U-19;Password=19$RPEe");
        }
    }
}
