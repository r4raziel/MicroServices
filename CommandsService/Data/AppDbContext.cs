using Microsoft.EntityFrameworkCore;
using CommandsService.Models;

namespace CommandsService.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {


        }

        public DbSet<Command> Commands { get; set; }
    }


}