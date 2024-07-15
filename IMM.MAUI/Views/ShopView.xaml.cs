using IMM.MAUI.ViewModels;
using IMM.Models;

namespace IMM.MAUI.Views;

[QueryProperty(nameof(CartId), "cartId")]
public partial class ShopView : ContentPage
{
    public int CartId { get; set; }
	public ShopView()
	{
		InitializeComponent();
	}

    private void Add_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshShop();
    }

    private void Remove_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ShopViewModel).RefreshShop();
    }

    private void Checkout_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Checkout?cartId={CartId}");
    }

    private void Wishlist_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//WishlistManage");
    }

    private void Home_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ShopViewModel(CartId);
        (BindingContext as ShopViewModel).RefreshShop();
    }
}