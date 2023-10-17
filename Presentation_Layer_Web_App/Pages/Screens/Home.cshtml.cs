using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation_Layer_Web_App.Pages.Screens
{
    public class HomeModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnGetRefreach()
        {
            return Page();
        }
    }
}
