using Agrisustain_Jamaica.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agrisustain_Jamaica.DataAccess
{
    public class UserRepository
    {
        public List<UserProfile> User { get; set; }

        public UserRepository()
        {
            // Initialize an in-memory list to store user profiles.
            User = new List<UserProfile>();
        }

        // Add a method to retrieve a user by email
        public UserProfile GetUserByEmail(string email)
        {
            return User.FirstOrDefault(user => user.Email == email);
        }

        // Add a method to update a user
        public void UpdateUser(UserProfile user)
        {
            var existingUser = User.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                // Replace the existing user with the updated user
                User.Remove(existingUser);
                User.Add(user);
            }
        }
    }
}