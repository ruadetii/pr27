using System.Windows;
using System.Windows.Input;

namespace ShopContent_Radosteva
{
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public View.Main Main = new View.Main();
        public MainWindow()
        {
            InitializeComponent();
            init = this;
            frame.Navigate(Main);
        }

        private void OpenIndex(object sender, MouseButtonEventArgs e)
        {
            frame.Navigate(Main);
        }
    }
}