using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
      : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
