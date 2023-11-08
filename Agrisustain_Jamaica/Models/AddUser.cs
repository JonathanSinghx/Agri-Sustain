using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Agrisustain_Jamaica.Models
{
    public class AddUserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserType { get; set; }

        [Required]
        public string Location { get; set; }

        public List<Badge> EarnedBadges { get; set; }
    }

}
