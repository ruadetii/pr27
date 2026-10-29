using System.Windows.Controls;

namespace ShopContent_Radosteva.View
{
    public partial class Add : Page
    {
        public Add(object Context)
        {
            InitializeComponent();
            DataContext = new 
            {
                item = Context,
                categorys = new ViewModell.VMCategorys()
            };
        }
    }
}
