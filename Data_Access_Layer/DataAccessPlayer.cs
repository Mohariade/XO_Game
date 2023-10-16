﻿using System;
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

    }
}
