using MySql.Data.MySqlClient;
using ShopContent_Radosteva.Classes;
using ShopContent_Radosteva.Modell;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace ShopContent_Radosteva.Context
{
    public class ItemsContext : Items
    {
        public ItemsContext(bool save = false)
        {
            if (save) Save(true);
            Category = new Categorys();
        }

        public static ObservableCollection<ItemsContext> AllItems()
        {
            ObservableCollection<ItemsContext> allItems = new ObservableCollection<ItemsContext>();
            ObservableCollection<CategorysContext> allCategorys = CategorysContext.AllCategorys();
            MySqlConnection connection = new MySqlConnection(Connection.config);
            MySqlDataReader dataItems = Connection.Query("SELECT * FROM `Items`", connection);
            while (dataItems.Read())
            {
                allItems.Add(new ItemsContext()
                {
                    Id = dataItems.GetInt32(0),
                    Name = dataItems.GetString(1),
                    Price = dataItems.GetDouble(2),
                    Description = dataItems.GetString(3),
                    Category = dataItems.IsDBNull(4) ?
                        null :
                        allCategorys.Where(x => x.Id == dataItems.GetInt32(4)).First()
                });
            }
            Connection.CloseConnection(connection);
            return allItems;
        }

        public void Save(bool New = false)
        {
            MySqlConnection connection = new MySqlConnection(Connection.config);
            string price = this.Price.ToString(CultureInfo.InvariantCulture);
            if (New)
            {
                MySqlDataReader dataItems = Connection.Query("INSERT INTO " +
                    "`Items`(" +
                        "`Name`, " +
                        "`Price`, " +
                        "`Description`) " +
                    $"VALUES (" +
                        $"'{this.Name}', " +
                        $"{price}, " +
                        $"'{this.Description}'); " +
                    "SELECT LAST_INSERT_ID();", connection);
                dataItems.Read();
                this.Id = dataItems.GetInt32(0);
            }
            else
            {
                Connection.Query("UPDATE `Items` " +
                    "SET " +
                        $"`Name` = '{this.Name}', " +
                        $"`Price` = {price}, " +
                        $"`Description` = '{this.Description}', " +
                        $"`IdCategory` = {this.Category.Id} " +
                    "WHERE " +
                        $"`Id` = {this.Id}", connection);
            }
            Connection.CloseConnection(connection);
            MainWindow.init.frame.Navigate(MainWindow.init.Main);
        }

        public void Delete()
        {
            MySqlConnection connection = new MySqlConnection(Connection.config);
            Connection.Query("DELETE FROM `Items` " +
                "WHERE " +
                    $"`Id` = {this.Id}", connection);
            Connection.CloseConnection(connection);
        }

        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    MainWindow.init.frame.Navigate(new View.Add(this));
                });
            }
        }

        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Category = CategorysContext.AllCategorys().Where(x => x.Id == this.Category.Id).First();
                    Save();
                });
            }
        }

        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Delete();
                    (MainWindow.init.Main.DataContext as ViewModell.VMItems).Items.Remove(this);
                });
            }
        }
    }
}
