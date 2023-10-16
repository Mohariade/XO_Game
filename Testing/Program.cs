
using Data_Access_Layer;
using System.Data;
using System.Data.SqlClient;

void TestingConnection()
{
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
        else
        {
            Console.WriteLine("Filed to retrieve data");
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

}

void Testing_Get_Games_List()
{
    DataTable table = new DataTable();

    if (clsDataAccess.Get_Game_List(ref table)) 
    {
        foreach(DataRow row in table.Rows) 
        {
            Console.WriteLine($"Game ID : {row["Game_ID"]}");
        }
    }
}

int Main()
{

    //TestingConnection();
    Testing_Get_Games_List();
    return 0;
}

Main();
