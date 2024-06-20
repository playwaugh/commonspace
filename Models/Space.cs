using System.ComponentModel.DataAnnotations;

namespace Commonspace.Models
{
    public class Space
    {
        public int SpaceId { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        public required string Description { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public required User User { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
