
using Business_Logic_Layer;
using Data_Access_Layer;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

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
        foreach (DataRow row in table.Rows)
        {
            clsGame game = new clsGame(row);
            Console.WriteLine($"Game Info :\nGame ID : {game.ID} | Game Status : {game.StatusID} | PLayer 1 : {game.Player1ID} | Player 2 : {game.Player2ID} | Start Time : {game.StartTime}        ");
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
            Console.WriteLine($" {row["Player_ID"]}  |  {row["Name"]}  | {row["Password"]}");
        }
    }

    Console.WriteLine("\n\n");
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

void Testing_clsPlayer_Create()
{
    clsPlayer? player = clsPlayer.Create("Mohamed", "Moha");

    if(player == null)
    {
        Console.WriteLine("Filed to create player\n\n\n");
        Testing_Get_Players_List();
        return;
    }

    Console.WriteLine("Player Created Successfuly\n\n\n");
    Testing_Get_Players_List();
}

void Testing_clsPerson_Update()
{
    clsPlayer? player = clsPlayer.Find("Houdaifa","H123");

    if(player == null)
    {
        Console.WriteLine("Player Not found\n\n");
        return;
    }

    Testing_Get_Players_List();

    player.Password = "H0000";
    player.Name = "HoudaifaBouamine";

    if (player.Update())
    {
        Console.WriteLine("Updated Successfuly\n\n");
    }
    else
    {
        Console.WriteLine("Failed to update playerz\n\n");
    }

    Testing_Get_Players_List();
}


void Testing_clsPerson_Delete()
{
    clsPlayer? player = clsPlayer.Find("Player2","2222");

    if(player == null)
    {
        Console.WriteLine("Player Not found\n\n");
        return;
    }

    Testing_Get_Players_List();


    if (clsPlayer.Delete(ref player))
    {
        Console.WriteLine("deleted Successfuly\n\n");
    }
    else
    {
        Console.WriteLine("Failed to delete player\n\n");
    }

    Testing_Get_Players_List();
}

void Testing_Game_Join_Create()
{
    clsPlayer? player = clsPlayer.Find("Ibrahime", "1234");

    if(player == null)
    {
        Console.WriteLine("Player not found");
        return;
    }

    clsGame? game = clsGame.JoinGame(player.ID);

    if(game == null)
    {
        Console.WriteLine("Falied to join game");
        return;
    }


    Console.WriteLine($"Game Info :\nGame ID : {game.ID} | Game Status : {game.StatusID} | PLayer 1 : {game.Player1ID} | Player 2 : {game.Player2ID} | Start Time : {game.StartTime}\n\n");

}

void Testing_IS_Player_In_Game()
{
    clsPlayer? player = clsPlayer.Find("Ibrahime", "1234");

    if (player == null)
    {
        Console.WriteLine("Player not found");
        return;
    }

    if (clsDataAccess.Is_Player_In_Game(player.ID))
    {
        Console.WriteLine("In game");
    }
    else
    {
        Console.WriteLine("Not in game");
    }

}

void Testing_Game_Play()
{
    clsPlayer? player1 = clsPlayer.Find("HoudaifaBouamine", "H0000");

    if(player1 == null)
    {
        Console.WriteLine("Player not found");
        return;
    }

    if(player1.IsPlaying)
    {
        Console.WriteLine($"Player {player1.Name} is playing now");
        return ;
    }

    clsPlayer? player2 = clsPlayer.Find("Ibrahime", "1234");

    if (player2 == null)
    {
        Console.WriteLine("Player not found");
        return;
    }

    if (player2.IsPlaying)
    {
        Console.WriteLine($"Player {player2.Name} is playing now");
        return;
    }

    Console.WriteLine("Game Starting ...\n");
    clsGame? Game1 = clsGame.JoinGame(player1.ID);

    if(Game1 == null)
    {
        Console.WriteLine($"Player 1 [{player1.Name}] Failed To join game\n");
        return;
    }

    clsGame? Game2 = clsGame.JoinGame(player2.ID);

    if(Game2 == null)
    {
        Console.WriteLine($"Player 2 [{player2.Name}] Failed To join game\n");
        return;
    }

    if(Game1.ID == Game2.ID)
    {
        Console.WriteLine("  Both Players On the same Game\n\n");
    }


    Console.Write("\n\nEnter Anny Key to contenue\n\n");
    Console.ReadLine();

    while (Game1.Running && Game2.Running)
    {

        Game1.Refreach();
        Console.Clear();
        Print(Game1);

        Console.WriteLine("Player 1");
        Console.Write("Enter Row  >> ");
        int row1  = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Colm >> ");
        int colm1 = Convert.ToInt32(Console.ReadLine());

        while (!Game1.Play(player1.ID, row1, colm1))
        {
            Console.Clear();
            Print(Game1);

            Console.WriteLine("Player 1");
            Console.WriteLine("\nThe index is alreay used, choose other one !!!\n");
            Console.Write("Enter Row  >> ");
            row1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Colm >> ");
            colm1 = Convert.ToInt32(Console.ReadLine());
        }




        //////////////////////////
        //Console.Write("\n\nEnter Anny Key to contenue\n\n");
        //Console.ReadLine();
        //////////////////////////








        Game2.Refreach();

        Console.Clear();
        Print(Game2);

        if (!Game2.Running)
        {
            break;
        }

        Console.WriteLine("\nPlayer 2");
        Console.Write("Enter Row  >> ");
        int row2  = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter Colm >> ");
        int colm2 = Convert.ToInt32(Console.ReadLine());

        while (!Game2.Play(player2.ID, row2, colm2))
        {
            Console.Clear();
            Print(Game2);

            Console.WriteLine("\nPlayer 2");
            Console.WriteLine("\nThe index is alreay used, choose other one !!!\n");
            Console.Write("Enter Row  >> ");
            row2 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Colm >> ");
            colm2 = Convert.ToInt32(Console.ReadLine());

        }

        //Console.Write("\n\nEnter Anny Key to contenue\n\n");
        //Console.ReadLine();


    }

    Console.WriteLine("\n\nG A M E    E N D\n\n");
   

}

void Print(clsGame game)
{

        Console.WriteLine();

    Console.WriteLine($"Game ID: {game.ID}, Player1: {game.Player1ID}, Player2: {game.Player2ID}, Turn ID:{game.PlayerTurn_ID}");

    Console.WriteLine();

    for(int  i = 0; i < 3; i++)
    {
        for(int j = 0; j < 3; j++)
        {
            Console.Write($"  {game.Board[i,j]}");
        }
        Console.WriteLine("\n");
    }
}

int Main()
{
    Testing_Game_Play();
    //Print(clsGame.Find(1));
    //Testing_Game_Play();
    //Testing_IS_Player_In_Game();
    //Testing_Get_Games_List();


    //Testing_Game_Join_Create();
    //Testing_Get_Players_List();
    // Testing_clsPerson_Delete();
    //Testing_Get_Players_List();
    //Testing_clsPlayer_Create();
    //Testing_clsPlayer_Find();
    //Testing_Get_Player();
    //TestingConnection();
    //Testing_Get_Players_List();
    //Testing_Get_Games_List();
    return 0;
}

Main();
