using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class clsGame
    {
        public static char[][] XO_Matrix = {
                                new char[] { ' ',' ',' ' } ,
                                new char[] { ' ',' ',' ' } ,
                                new char[] { ' ',' ',' ' }
                            };


        public static clsGame? JoinGame(int Player_ID)
        {

            if (clsDataAccess.Is_Player_In_Game(Player_ID))
            {
                return null;
            }

            DataTable? GameData = clsDataAccess.Get_Playable_Game();

            if (GameData != null)
            {
                clsGame Game = new clsGame(GameData.Rows[0]);
                if (clsDataAccess.JoinPlayerToGame(Game.GameID, Player_ID))
                {
                    Game.Player2ID = Player_ID;
                    return Game;
                }

                return null;
            }
            else
            {
                clsGame Game = new clsGame(clsDataAccess.Create_New_Game(Player_ID));

                return Game;

            }

        }

        public int GameID { get; set; }
        public int Player1ID { get; set; }
        public int? Player2ID { get; set; }
        public int? WinnerID { get; set; }
        public int StatusID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public static int? Convert_To_Int_OR_Null(object? value)
        {
            if (value == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }
        public clsGame(DataRow row)
        {
            if (row != null)
            {

                GameID = Convert.ToInt32(row["Game_ID"]);
                Player1ID = Convert.ToInt32(row["Player1_ID"]);
                Player2ID = Convert_To_Int_OR_Null(row["Player2_ID"]);
                WinnerID = Convert_To_Int_OR_Null(row["Winner_ID"]);
                StatusID = Convert.ToInt32(row["Status_ID"]);
                StartTime = Convert.ToDateTime(row["Start_Time"]);

                // Check if End_Time is DBNull before converting to DateTime
                if (row["End_Time"] != DBNull.Value)
                {
                    EndTime = Convert.ToDateTime(row["End_Time"]);
                }
                else
                {
                    EndTime = null; // Set to null if DBNull
                }

            }
        }
    }
}