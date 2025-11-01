using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class db_taskManegment : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; } 
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Mo-Elshinawy;Initial Catalog=db_taskManegment;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
