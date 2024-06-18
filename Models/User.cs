using System.ComponentModel.DataAnnotations;

namespace Commonspace.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Email { get; set; }
    }
}
