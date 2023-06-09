using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetCoreCURDApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreCURDApp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
    }
}