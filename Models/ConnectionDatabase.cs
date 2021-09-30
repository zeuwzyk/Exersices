using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace you.Models
{
    public class ConnectionDatabase : DbContext
    {
        public ConnectionDatabase(DbContextOptions<ConnectionDatabase> options) : base(options)
        {

        }

        public DbSet<EmployeeData> Employee { get; set; }
    }
}
