using MySql.Data.MySqlClient;
using ShopContent_Radosteva.Classes;
using ShopContent_Radosteva.Modell;
using System.Collections.ObjectModel;

namespace ShopContent_Radosteva.Context
{
    public class CategorysContext : Categorys
    {
        public static ObservableCollection<CategorysContext> AllCategorys()
        {
            ObservableCollection<CategorysContext> allCategorys = new ObservableCollection<CategorysContext>();
            MySqlConnection connection = new MySqlConnection(Connection.config);
            MySqlDataReader dataCategorys = Connection.Query("SELECT * FROM `Categorys`", connection);
            while (dataCategorys.Read())
            {
                allCategorys.Add(new CategorysContext()
                {
                    Id = dataCategorys.GetInt32(0),
                    Name = dataCategorys.GetString(1)
                });
            }
            Connection.CloseConnection(connection);
            return allCategorys;
        }
    }
}
