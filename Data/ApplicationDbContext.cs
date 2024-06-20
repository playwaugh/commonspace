using Microsoft.EntityFrameworkCore;
using Commonspace.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Commonspace.Data
{
    public class ApplicationDbContext : IdentityDbContext<Commonspace.Models.User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Space> Spaces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
