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
        static public clsPlayer? Create(string Name,string Password)
        {
            if (clsPlayer.Find(Name) != null)
            {
                return null;
            }
            int ID = -1;

            if(!clsDataAccess.Add_New_Player(ref ID, Name, Password))
            {
                return null;
            }

            return new clsPlayer(ID,Name,Password);
        }
        public bool Update()
        {
            return clsDataAccess.Update_Player(this.ID, this.Name, this.Password);
        }

        public static bool Delete(ref clsPlayer? player)
        {
            if(player == null) return false;

            bool deleted = clsDataAccess.Delete_Player(player.ID);

            if (deleted)
            {
                player = null;
                return true;
            }

            return false;
        }
        static public clsPlayer? Find(string Player_Name,string Password)
        {
            
            clsPlayer? player = clsPlayer.Find(Player_Name);

            if(player == null)
            {
                return null;
            }

            if(player.Password == Password)
            {
                return player;
            }
            else
            {
                return null;
            }
        }
        static private clsPlayer? Find(string Player_Name)
        {
            DataTable tmp_table = new DataTable();
            clsPlayer? player = null;

            if (clsDataAccess.Get_Player(Player_Name, ref tmp_table))
            {
                player = new clsPlayer(tmp_table.Rows[0]);
            }

            return player;
        }
        static private clsPlayer? Find(int ID)
        {
            DataTable tmp_table = new DataTable();
            clsPlayer? player = null;

            if (clsDataAccess.Get_Player(ID, ref tmp_table))
            {
                player = new clsPlayer(tmp_table.Rows[0]);
            }

            return player;
        }

        static public bool Exist(int ID)
        {
            return clsPlayer.Find(ID) != null;
        }

        static public bool Exist(string Name)
        {
            return clsPlayer.Find(Name) != null;
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
