using System;
using System.Collections.Generic;

namespace Agrisustain_Jamaica.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string UserType { get; set; }
        public string Location { get; set; }
        public List<Badge> EarnedBadges { get; set; }
    }
}
