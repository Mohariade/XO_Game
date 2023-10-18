using Business_Logic_Layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation_Layer_Web_App.Pages.Screens
{
    public class HomeModel : PageModel
    {
   
        public IActionResult OnGetRefreach()
        {
            return Page();
        }

        public IActionResult OnPost()
        {


            return Page();
        }

        

        public void Set_Cell(int row,int colm,char val)
        {
            clsGame.XO_Matrix[row][colm] = val;
        }

        public char Get_Cell(int row,int colm)
        {
            return clsGame.XO_Matrix[row][colm];
        }

        public IActionResult OnPostTest00()
        {
            Set_Cell(0, 0, 'X');
            return RedirectToPage();
        }

        public IActionResult OnPostTest01()
        {
            Set_Cell(0, 1, 'X');
            return RedirectToPage();
        }

        public IActionResult OnPostTest02()
        {
            Set_Cell(0, 2, 'X');
            return RedirectToPage();
        }


        public IActionResult OnPostTest10()
        {
            Set_Cell(1, 0, 'X');
            return RedirectToPage();
        }

        public IActionResult OnPostTest11()
        {
            Set_Cell(1, 1, 'X');
            return RedirectToPage();
        }

        public IActionResult OnPostTest12()
        {
            Set_Cell(1, 2, 'X');
            return RedirectToPage();
        }


        public IActionResult OnPostTest20()
        {
            Set_Cell(2, 0, 'X');
            return RedirectToPage();
        }

        public IActionResult OnPostTest21()
        {
            Set_Cell(2, 1, 'X');
            return RedirectToPage();
        }

        public IActionResult OnPostTest22()
        {
            Set_Cell(2, 2, 'X');
            return RedirectToPage();
        }


    }
}
