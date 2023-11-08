using Agrisustain_Jamaica.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agrisustain_Jamaica.Controllers
{
    public class BadgeController : Controller
    {

        private readonly List<BadgeCriteria> badgeCriteria = new List<BadgeCriteria>
        {
            new BadgeCriteria { Name = "Drip Irrigation", RequiredCount = 90 },
            new BadgeCriteria { Name = "Organic Farming", RequiredCount = 180 },
            new BadgeCriteria { Name = "Crop Rotation", RequiredCount = 6 },
            new BadgeCriteria { Name = "Rainwater Harvesting", RequiredCount = 90 },
            new BadgeCriteria { Name = "Mulching", RequiredCount = 6 },
            new BadgeCriteria { Name = "Reducing Food Waste", RequiredCount = 90 },
            new BadgeCriteria { Name = "Hydroponics and Aquaponics", RequiredCount = 90 },
            new BadgeCriteria { Name = "Composting", RequiredCount = 180 },
            new BadgeCriteria { Name = "Natural Pest Management", RequiredCount = 6 }
        };

        // GET: BadgeTracking
        public IActionResult Index()
        {
            var badgeProgressList = new List<BadgeProgress>();

            foreach (var badge in badgeCriteria)
            {
                badgeProgressList.Add(new BadgeProgress
                {
                    BadgeName = badge.Name,
                    DaysUsed = 0,
                    ProgressMessage = string.Empty
                });
            }

            return View(badgeProgressList);
        }

        // POST: BadgeTracking/TrackProgress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TrackProgress(List<BadgeProgress> badgeProgressList)
        {
            for (int i = 0; i < badgeProgressList.Count; i++)
            {
                var badge = badgeCriteria[i];
                var badgeProgress = badgeProgressList[i];

                if (badgeProgress.DaysUsed >= badge.RequiredCount)
                {
                    badgeProgress.ProgressMessage = $"Congratulations! You have earned the {badge.Name} badge.";
                }
                else
                {
                    badgeProgress.ProgressMessage = $"You have used {badge.Name} for {badgeProgress.DaysUsed} days. Keep it up!";
                }
            }

            return View("Index", badgeProgressList);
        }
    }
}
