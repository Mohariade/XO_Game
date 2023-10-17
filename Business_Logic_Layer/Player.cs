using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class clsPlayer
    {
        public int ID { get; private set; }
        public string Name {  get; set; }
        public string Password { get; set; }    

        /// <summary>
        /// Create New Player in the data base and return the new player's ID
        /// </summary>
        /// <param name="Name">Player Name (Must be unique)</param>
        /// <param name="Password">Player account password</param>
        /// <returns>Player's ID</returns>
        static public int Create_New_Player(string Name,string Password)
        {
            if (clsPlayer.Find(Name) != null)
            {
                return -1;
            }
            int ID = -1;

            if(!clsDataAccess.Add_New_Player(ref ID, Name, Password))
            {
                return -1;
            }

            return ID;
        }
        static public clsPlayer Find(string Player_Name,string Password)
        {
            
            clsPlayer player = clsPlayer.Find(Player_Name);

            if(player.Password == Password)
            {
                return player;
            }
            else
            {
                return null;
            }
        }

        static public clsPlayer Find(string Player_Name)
        {
            DataTable tmp_table = new DataTable();
            clsPlayer player = null;

            if (clsDataAccess.Get_Player(Player_Name, ref tmp_table))
            {
                player = new clsPlayer(tmp_table.Rows[0]);
            }

            return player;
        }

        private clsPlayer (DataRow row)
        {
            this.ID = Convert.ToInt32(row["Player_ID"]);
            this.Name = (string) row["Name"];
            this.Password = (string) row["Password"];
        }

        private clsPlayer(int ID, string Name, string Password)
        {
            this.ID = ID;
            this.Name = Name;
            this.Password = Password;
        }
    }
}
