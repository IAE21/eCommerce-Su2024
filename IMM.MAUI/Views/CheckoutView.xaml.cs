using IMM.MAUI.ViewModels;
using IMM.Models;
using IMM.Services;

namespace IMM.MAUI.Views;

[QueryProperty(nameof(CartId), "cartId")]
public partial class CheckoutView : ContentPage
{
    public int CartId { get; set; }
	public CheckoutView()
	{
		InitializeComponent();
	}

    private void KeepShopping_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Shop?cartId={1}");
    }

    private void MainMenu_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CheckoutViewModel(CartId);
        (BindingContext as CheckoutViewModel).RefreshCart();
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        if (CartId == 1)
        {
            CartServiceProxy.Current.Cart.Contents.Clear();
        }
        else
        {
            CartServiceProxy.Current.DeleteCart(CartId);
        }
    }
}