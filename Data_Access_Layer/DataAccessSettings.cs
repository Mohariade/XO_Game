using System.Data.Common;

namespace Data_Access_Layer
{
    public class clsDataAccessSettings
    {
        static public string ConnectionString { get; } = "data source=.;Initial Catalog = XO_Game; Integrated Security = True;";
    }
}