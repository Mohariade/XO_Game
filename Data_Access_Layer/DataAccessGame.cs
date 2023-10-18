using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Data_Access_Layer
{
    public partial class clsDataAccess
    {

        public static bool Get_Game_List(ref DataTable table)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM GameInfo";

            SqlCommand command = new SqlCommand(query, connection);

            bool Result = true;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader != null)
                {
                    if (reader.HasRows)
                    {
                        table.Load(reader);
                    }

                    reader.Close();
                }
                else
                {
                    Result = false;
                }
            }
            catch (Exception ex)
            {
                Result = false;
            }
            finally
            {
                connection.Close();
            }

            return Result;  

        }

        public static DataTable? Get_Playable_Game()
        {
            DataTable? table = new DataTable();
            string query = "SELECT * FROM Games WHERE Player2_ID is null";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection); 
            
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                
                if(reader.HasRows)
                {
                    table.Load(reader);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                table = null;
            }
            finally
            {
                connection.Close();
            }

            return table;
        }

        public static bool JoinPlayerToGame(int Game_ID,int Player_ID)
        {
            string query = "UPDATE Games SET Player2_ID = @Player2_ID WHERE Game_ID = @Game_ID";

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Player2_ID", Player_ID);
            command.Parameters.AddWithValue("@Game_ID"   , Game_ID);

            bool result = true;
            try
            {
                connection.Open();

                int RowsAffected = command.ExecuteNonQuery();

               if(RowsAffected != 1)
               {
                    result = false;
               }

            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
    }
}
