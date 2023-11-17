using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AgriSustain_Jamaica.Models;
using Microsoft.AspNetCore.Mvc;


namespace AgriSustain_Jamaica.Models
{
    public class UserSelectorModel : Controller
    {
		public IActionResult ShareProduce()
		{
			return View();
		}

		public IActionResult ManageGarden()
		{
			return View();
		}
        public IActionResult StarterGuide()
        {
            return View();
        }


        public void OnGet()
        {
        }
    }
}
