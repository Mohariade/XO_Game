using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public partial class clsDataAccess
    {
        #region Create
        public static bool Add_New_Player(ref int ID,string Name,string Password)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "INSERT INTO Players (Name,Password) " +
                "Values (@Name,@Password); " +
                "SELECT Players.Player_ID FROM Players WHERE Name = @Name";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Password", Password);

            bool isAdded = true;

            try
            {
                connection.Open();

                object Result = command.ExecuteScalar();

                if (Result == DBNull.Value)
                {
                    isAdded = false;
                }
                else
                {
                    ID = Convert.ToInt32(Result);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return isAdded;
        }

        #endregion

        #region Read
        public static bool Get_Players_List(ref DataTable table)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Players";

            SqlCommand command = new SqlCommand(query, connection);
            bool Result = true;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if(reader == null)
                {
                    Result = false;
                }
                else
                {
                    if (reader.HasRows)
                    {
                        table.Load(reader);
                    }
                    reader.Close();
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
        public static bool Get_Player(string Name,ref DataTable Table)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Players Where Name = @Name";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", Name);

            bool Result = true;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader == null)
                {
                    Result = false;
                }
                else
                {
                    if (reader.HasRows)
                    {
                        Table.Load(reader);
                    }
                    else
                    {
                        Result = false;
                    }
                    reader.Close();
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
        public static bool Get_Player(int ID,ref DataTable Table)
        {

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Players Where Player_ID = @Player_ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Player_ID", ID);

            bool Result = true;

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader == null)
                {
                    Result = false;
                }
                else
                {
                    if (reader.HasRows)
                    {
                        Table.Load(reader);
                    }
                    else
                    {
                        Result = false;
                    }
                    reader.Close();
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

        #endregion

        #region Update

        public static bool Update_Player(int ID, string Name, string Password)
        {
            bool isUpdated = true;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Players SET Name = @Name,Password = @Password " +
                "WHERE Player_ID = @Player_ID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Player_ID", ID);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();

                command.ExecuteScalar();
            }
            catch(Exception ex)
            {
                isUpdated = false;
            }
            finally
            {
                connection.Close();
            }


            return isUpdated;
        }

        #endregion

        #region Delete

        #endregion
    }
}
