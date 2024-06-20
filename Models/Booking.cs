using System.ComponentModel.DataAnnotations;
using Commonspace.Models;

public class Booking
{
    public int BookingId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public required string Status { get; set; }

    public int UserId { get; set; }

    public int SpaceId { get; set; }

    public required User User { get; set; }

    public required Space Space { get; set; }
}
