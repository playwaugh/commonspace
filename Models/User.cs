using Microsoft.AspNetCore.Identity;

namespace Commonspace.Models
{
    public class User : IdentityUser<int>
    {
        public ICollection<Booking>? Bookings { get; set; }
    }
}
