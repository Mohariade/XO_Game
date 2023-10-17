
using Business_Logic_Layer;
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
            Console.WriteLine($"Game ID : {row["Game_ID"]} | Game Status : {row["Status"]} | Winner Name : {row["Winner_Name"]}");
        }
    }
}

void Testing_Get_Players_List()
{
    DataTable table = new DataTable();

    if (clsDataAccess.Get_Players_List(ref table))
    {
        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine($"Player ID : {row["Player_ID"]} | {row["Password"]}");
        }
    }
}

void Testing_Get_Player()
{
    DataTable table = new DataTable();

    string Name = "Pelayer1";
    if (clsDataAccess.Get_Player(Name,ref table))
    {
        Console.WriteLine($"Player ID : {table.Rows[0]["Player_ID"]}  |  Name  : {table.Rows[0]["Name"]}  | {table.Rows[0]["Password"]}");
    }
    else
    {
        Console.WriteLine("Player with Name " + Name + " is Not Found");
    }
}

void Testing_clsPlayer_Find()
{
    clsPlayer player = clsPlayer.Find("Player1", "1111");

    Console.WriteLine($"Player ID : {player.ID}     #|#  Name : {player.Name}  #|#    password : {player.Password}");

}

int Main()
{

    Testing_clsPlayer_Find();
    //Testing_Get_Player();
    //TestingConnection();
    //Testing_Get_Players_List();
    //Testing_Get_Games_List();
    return 0;
}

Main();
