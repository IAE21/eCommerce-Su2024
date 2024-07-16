using IMM.MAUI.ViewModels;

namespace IMM.MAUI.Views;

public partial class WishlistManagementView : ContentPage
{
	public WishlistManagementView()
	{
		InitializeComponent();
        BindingContext = new WishlistManagementViewModel();
	}

    private void Delete_Clicked(object sender, EventArgs e)
    {
        (BindingContext as WishlistManagementViewModel)?.RefreshWishlists();
    }

    private void NewWishlist_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Wishlist");
    }

    private void ReturnShop_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Shop?cartId={1}");
    }

    private void MainMenu_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as WishlistManagementViewModel)?.RefreshWishlists();
    }
}