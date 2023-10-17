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

        static public clsPlayer Find(string Player_Name,string Password)
        {
            DataTable tmp_table = new DataTable();
            clsPlayer player = null;

            if (clsDataAccess.Get_Player(Player_Name,ref tmp_table))
            {
                player = new clsPlayer(tmp_table.Rows[0]);

                if(player.Password == Password)
                {
                    return player;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        private clsPlayer (DataRow row)
        {
            this.ID = Convert.ToInt32(row["Player_ID"]);
            this.Name = (string) row["Name"];
            this.Password = (string) row["Password"];
        }

    }
}
