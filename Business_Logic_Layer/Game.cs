using Data_Access_Layer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class clsGame
    {

        public static List<clsGame> List()
        {
            List<clsGame> games = new List<clsGame>();

            DataTable dt = new DataTable(); 
            clsDataAccess.Get_Game_List(ref dt);

            foreach(DataRow row in dt.Rows)
            {
                games.Add(new clsGame(row));
            }

            return games;
        }

        public static string ListAsJSON()
        {
            List<clsGame> games = List();

            return JsonConvert.SerializeObject(games);
        }
        
        public void Refreach()
        {
            clsGame? game = clsGame.Find(this.ID);

            if (game == null) return;

            this.Copy(game);
        }

        public void Update()
        {
            DataTable table = new DataTable();
            ToDataRow(ref table);
            clsDataAccess.Update_Game(table);
        }

        private void Copy(clsGame SourceGame)
        {
            this.Player1ID = SourceGame.Player1ID;
            this.Player2ID = SourceGame.Player2ID;
            this.WinnerID = SourceGame.WinnerID;
            this.StatusID = SourceGame.StatusID;
            this.StartTime = SourceGame.StartTime;
            this.EndTime = SourceGame.EndTime;
            this.Board = SourceGame.Board;
            this.PlayerTurn_ID = SourceGame.PlayerTurn_ID;
        }

        static public string FindAsJson(int Id)
        {
            clsGame? game = clsGame.Find(Id);

            if(game == null)
            {
                return JsonConvert.NaN;
            }
            
            return JsonConvert.SerializeObject(game);
        }
        static public clsGame? Find(int ID)
        {
            DataTable? table =new DataTable();
            
            if(clsDataAccess.Get_Game(ref table, ID))
            {
                return new clsGame(table.Rows[0]);
            }

            else
            {
                return null;
            }
        }

        public static string JoinGameAsJson(int Player_Id)
        {
            clsGame? game = clsGame.JoinGame(Player_Id);

            if(game == null)
            {
                return JsonConvert.NaN;
            }

            return JsonConvert.SerializeObject(game);
        }
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
                if (clsDataAccess.JoinPlayerToGame(Game.ID, Player_ID))
                {
                    Game.Player2ID = Player_ID;
                    Game.StatusID = (int)enStatus.OnGoing;
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

        private enStatus CheckGameEnd()
        {
            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (Board[row, 0] == Board[row, 1] && Board[row, 1] == Board[row, 2] && Board[row, 0] != '/')
                {
                    if (Board[row, 0] == 'X')
                        return enStatus.Player_1_Won; 
                    else
                        return enStatus.Player_2_Won;
                }
            }

            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (Board[0, col] == Board[1, col] && Board[1, col] == Board[2, col] && Board[0, col] != '/')
                {
                    if (Board[0, col] == 'X')
                        return enStatus.Player_1_Won;
                    else
                        return enStatus.Player_2_Won;

                }
            }

            // Check diagonals
            if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2] && Board[0, 0] != '/')
            {
                if (Board[0, 0] == 'X')
                    return enStatus.Player_1_Won;
                else
                    return enStatus.Player_2_Won;
            }

            if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0] && Board[0, 2] != '/' )
            {
                if (Board[0, 2] == 'X')
                    return enStatus.Player_1_Won;
                else
                    return enStatus.Player_2_Won;
            }

            // No winner, check for a draw
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Board[row, col] == '/')
                    {
                        return enStatus.OnGoing; 
                    }
                }
            }

            return enStatus.Draw;
        }

        private void EndGame(enStatus status)
        {
            this.StatusID = (int)status;
            this.EndTime = DateTime.Now;
            this.WinnerID = this.PlayerTurn_ID;
        }
        private bool _SetXO(int Player_ID,int row,int colm)
        {
            if (Board[row, colm] != '/')
            {
                return false;
            }

            if (Player_ID == Player1ID)
            {
                Board[row, colm] = 'X';
            }
            else
            {
                Board[row, colm] = 'O';
            }
            return true;
           
        }
        public bool Play(int Player_ID,int Row,int Colm)
        {
            if(Player_ID == PlayerTurn_ID)
            {
                if (!_SetXO(Player_ID, Row, Colm))
                {
                    return false;
                }

                enStatus status = CheckGameEnd();

                if (status != enStatus.OnGoing)
                {
                    EndGame(status);
                }
                else
                {
                    ChangeTurn();
                }

                Update();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ChangeTurn()
        {
            if (Player1ID == PlayerTurn_ID)
            {
                PlayerTurn_ID = Player2ID;
            }
            else
            {
                PlayerTurn_ID = Player1ID;
            }
        }
        public void ToDataRow(ref DataTable SingleGameData)
        {
            SingleGameData.Columns.Add("Game_ID");
            SingleGameData.Columns.Add("Player1_ID");
            SingleGameData.Columns.Add("Player2_ID");
            SingleGameData.Columns.Add("Winner_ID");
            SingleGameData.Columns.Add("Status_ID");
            SingleGameData.Columns.Add("Start_Time");
            SingleGameData.Columns.Add("End_Time");
            SingleGameData.Columns.Add("Board");
            SingleGameData.Columns.Add("CurrentPlayerTurn_ID");

            DataRow newRow = SingleGameData.NewRow();
            SingleGameData.Rows.Add(newRow);
            newRow["Game_ID"] = this.ID;
            newRow["Player1_ID"] = this.Player1ID;
            newRow["Player2_ID"] = this.Player2ID ?? (object)DBNull.Value;
            newRow["Winner_ID"] = this.WinnerID ?? (object)DBNull.Value;
            newRow["Status_ID"] = this.StatusID;
            newRow["Start_Time"] = this.StartTime;
            newRow["End_Time"] = this.EndTime ?? (object)DBNull.Value;
            newRow["Board"] = BoardAsString();
            newRow["CurrentPlayerTurn_ID"] = this.PlayerTurn_ID;

            string BoardAsString()
            {
                string sBoard = "";
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        sBoard += this.Board[i, j];
                    }
                }

                return sBoard;

            }
        }

        
        public int ID { get; private set; }
        public int Player1ID { get; private set; }
        public int? Player2ID { get; private set; }
        public int? WinnerID { get;private set; }
        public int StatusID { get; private set; }
        public int? PlayerTurn_ID { get;private set; }
        public DateTime StartTime { get;private set; }
        public DateTime? EndTime { get;private set; }
        public char[,] Board { get; private set; } = { { '#', '#', '#' }, { '#', '#', '#' }, { '#', '#', '#' } };

        //

        public bool OnGoing
        {
            get
            {
                return (enStatus)this.StatusID == enStatus.OnGoing;
            }
        }
        public bool Running
        {
            get 
            {
                return (enStatus)StatusID == enStatus.OnGoing || (enStatus)StatusID == enStatus.Ready;
            }
        }
        enum enStatus { Draw = 3,OnGoing = 1,Player_1_Won = 8,Player_2_Won = 9,Ready=7};


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

                ID = Convert.ToInt32(row["Game_ID"]);
                Player1ID = Convert.ToInt32(row["Player1_ID"]);
                Player2ID = Convert_To_Int_OR_Null(row["Player2_ID"]);
                WinnerID = Convert_To_Int_OR_Null(row["Winner_ID"]);
                StatusID = Convert.ToInt32(row["Status_ID"]);
                StartTime = Convert.ToDateTime(row["Start_Time"]);
                PlayerTurn_ID = Convert_To_Int_OR_Null(row["CurrentPlayerTurn_ID"]);
                // Check if End_Time is DBNull before converting to DateTime
                if (row["End_Time"] != DBNull.Value)
                {
                    EndTime = Convert.ToDateTime(row["End_Time"]);
                }
                else
                {
                    EndTime = null; // Set to null if DBNull
                }

                string board = row["Board"].ToString() ;

                if(board == null)
                {
                    return;
                }

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Board[i,j] = board[i * 3 + j];
                    }
                }

            }
        }
    }
}