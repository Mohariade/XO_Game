using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Data_Access_Layer
{
    public class clsDataAccess
    {

        public static bool Get_Game_List(ref DataTable table)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Games";

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

    }
}
