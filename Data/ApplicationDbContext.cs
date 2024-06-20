using Microsoft.EntityFrameworkCore;
using Commonspace.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Commonspace.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Space> Spaces { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the ASP.NET Identity tables
            builder.Entity<IdentityUserClaim<int>>().ToTable("AspNetUserClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("AspNetUserLogins");
            builder.Entity<IdentityUserToken<int>>().ToTable("AspNetUserTokens");
            builder.Entity<IdentityRole<int>>().ToTable("AspNetRoles");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("AspNetRoleClaims");
            builder.Entity<IdentityUserRole<int>>().ToTable("AspNetUserRoles");
        }
    }
}
