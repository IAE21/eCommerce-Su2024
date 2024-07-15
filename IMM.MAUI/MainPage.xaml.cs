using IMM.MAUI.ViewModels;

namespace IMM.MAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private void Inventory_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Inventory");
        }

        private void Shop_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//Shop?cartId={1}");
        }
        private void Exit_Clicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }
    }

}
