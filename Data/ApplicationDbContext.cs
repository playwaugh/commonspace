using Microsoft.EntityFrameworkCore;
using Commonspace.Models;

namespace Commonspace.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Space> Spaces { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
