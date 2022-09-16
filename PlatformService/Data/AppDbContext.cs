using Microsoft.EntityFrameworkCore;
using PaltformService.Models;

namespace PaltformService.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {


        }

        public DbSet<Platform> Platforms { get; set; }
    }


}