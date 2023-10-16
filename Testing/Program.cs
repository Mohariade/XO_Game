
using Data_Access_Layer;
using System.Data.SqlClient;

SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

string query = "Select * from Game_Statuses";

SqlCommand cmd = new SqlCommand(query, connection);

connection.Open();

try
{



    SqlDataReader reader = cmd.ExecuteReader();
    if (reader != null)
    {

        while (reader.Read())
        {
            Console.WriteLine($"-> {reader["Status_ID"]} : {reader["Name"]}");

        }

        reader.Close();
    }

}
catch (Exception ex)
{
    Console.WriteLine("Error : " + ex.Message);
}
finally
{
    connection.Close();
}