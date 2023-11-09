using Microsoft.AspNetCore.Mvc;
using Agrisustain_Jamaica.Models;
using Agrisustain_Jamaica.DataAccess;
using System.Collections.Generic;

namespace Agrisustain_Jamaica.Controllers
{
    public class UserController : Controller
    {
        private UserRepository userRepository;

        public UserController()
        {
            userRepository = new UserRepository(); 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUpForm()
        {
            return View();
        }

        public IActionResult LogIn()
        {
            return View("User");
        }

        [HttpPost]
        public IActionResult SignUpForm(AddUserModel userInput)
        {
            var user = new UserProfile
            {
                Id = Guid.NewGuid(),
                FirstName = userInput.FirstName,
                LastName = userInput.LastName,
                Email = userInput.Email,
                Password = userInput.Password,
                PhoneNumber = userInput.PhoneNumber,
                UserType = userInput.UserType,
                Location = userInput.Location,
                EarnedBadges = new List<Badge>(),
            };

            // Add the new user to the repository
            userRepository.User.Add(user);

            if (ModelState.IsValid)
            {
                return Ok("User created successfully.");
            }

            return View("User", user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBadge(string badgeName, string userEmail)
        {
            // Get the user profile by email from your repository
            var user = userRepository.GetUserByEmail(userEmail);

            // Ensure you have an EarnedBadges property in your UserProfile model.
            if (user.EarnedBadges == null)
            {
                user.EarnedBadges = new List<Badge>();
            }

            // Create a new Badge based on the badge name and any other relevant data
            var badge = new Badge
            {
                Name = badgeName,
            };

            user.EarnedBadges.Add(badge);

            userRepository.UpdateUser(user);

            return View();
        }

    }
}
