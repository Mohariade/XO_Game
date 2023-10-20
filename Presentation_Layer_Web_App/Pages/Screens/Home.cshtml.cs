using Business_Logic_Layer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation_Layer_Web_App.Pages.Screens
{


   
    public class HomeModel : PageModel
    {
   
        public IActionResult OnGetRefreach()
        {
            clsGlobal.Game.Refreach();
            return Page();
        }

        public string Rand_String()
        {
            return ((new Random().Next())%10000).ToString();
        }

        public void OnGet()
        {

            if (clsGlobal.Player == null & clsGlobal.Game == null)
            {

                clsGlobal.Player = clsPlayer.Create("Player" + Rand_String(), Rand_String());

                if (clsGlobal.Player == null)
                {
                    int x = 1;
                }

                clsGlobal.Game = clsGame.JoinGame(clsGlobal.Player.ID);

                if (clsGlobal.Game == null)
                {
                    int y = 1;
                }

                while (!clsGlobal.Game.OnGoing)
                {
                    clsGlobal.Game.Refreach();
                    Thread.Sleep(200);
                }
            }
            else
            {
                clsGlobal.Game.Refreach();
            }
        }
        public IActionResult OnPost()
        {
            return Page();
        }


        public void Set_Cell(int row,int colm,char val)
        {
            clsGlobal.Game.Refreach();
            clsGlobal.Game.Play(clsGlobal.Player.ID, row, colm);
        }

        public char Get_Cell(int row,int colm)
        {
            //clsGlobal.Game.Refreach();

            if(clsGlobal.Game.Board[row, colm] == '/')
            {
                return ' ';
            }
            return clsGlobal.Game.Board[row,colm];
        }

        public IActionResult OnPostTest00()
        {
            Set_Cell(0, 0, 'X');
            return null;//RedirectToPage();
        }

        public IActionResult OnPostTest01()
        {
            Set_Cell(0, 1, 'X');
            return null;//RedirectToPage();
        }

        public IActionResult OnPostTest02()
        {
            Set_Cell(0, 2, 'X');
            return null;//RedirectToPage();
        }


        public IActionResult OnPostTest10()
        {
            Set_Cell(1, 0, 'X');
            return null;//RedirectToPage();
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
