using IMM.MAUI.ViewModels;
using IMM.Services;
namespace IMM.MAUI.Views;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
        InitializeComponent();
        BindingContext = new InventoryViewModel();
	}

    private void Create_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Item");
    }

    private void Home_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void Delete_Clicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel)?.RefreshItems();
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryViewModel)?.RefreshItems();
    }

    private void Tax_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Tax");
    }
}