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
                
                if(reader == null)
                {
                    table = null;
                }

                if(reader.HasRows)
                {
                    table.Load(reader);

                }
                else
                {
                    return null;
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

        /// <summary>
        /// Create New Game In the data base for player with <c>Player_ID = Player1_ID</c> and return it's <c>ID</c>
        /// </summary>
        /// <param name="Player1_ID"></param>
        /// <returns><c>Game_ID</c> of the new game , null if failed to create game</returns>
        public static DataRow? Create_New_Game(int Player1_ID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " INSERT INTO Games (Player1_ID, Player2_ID, Winner_ID, Status_ID, Start_Time, End_Time) VALUES (@Player1_ID, null, null, 7, GETDATE(), null);  SELECT SCOPE_IDENTITY() as Game_ID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Player1_ID", Player1_ID);

            int? New_Game_ID = null;
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result == null)
                {
                    New_Game_ID = null;
                }
                else
                {
                    New_Game_ID = Convert.ToInt32(result);
                }

            }
            catch(Exception ex)
            {
                New_Game_ID = null;
            }
            finally
            {
                connection.Close();
            }

            DataTable? gamedata = new DataTable();


            query = "SELECT * FROM Games WHERE Game_ID = @Game_ID";
            command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Game_ID", New_Game_ID);
            
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader == null)
                {
                    gamedata = null;
                }
                else
                {
                    if (reader.HasRows)
                    {
                        gamedata.Load(reader);
                        reader.Close();
                    }
                }
            }
            catch 
            {
                gamedata = null;
            }
            finally 
            {
                connection.Close();
            }

            if (gamedata == null) return null;

            return gamedata.Rows[0];
        }

        public static bool Is_Player_In_Game(int Player_ID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "select * from games where End_Time is null and (Player1_ID = @Player_ID or Player2_ID = @Player_ID)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Player_ID", Player_ID);

            bool isPlaying = true;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader == null)
                {
                    isPlaying = false;
                }
                else
                {
                    if(reader.HasRows)
                    {
                        isPlaying = true;
                    }
                    else
                    {
                        isPlaying = false;
                    }

                    reader.Close();
                }
            }
            catch {
                
            }
            finally
            {
                connection.Close();
            }

            return isPlaying;
        }
    }
}
