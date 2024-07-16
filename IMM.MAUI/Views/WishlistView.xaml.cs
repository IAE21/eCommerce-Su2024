using IMM.MAUI.ViewModels;

namespace IMM.MAUI.Views;

[QueryProperty(nameof(CartId), "cartId")]
public partial class WishlistView : ContentPage
{
	public int CartId { get; set; }
	public WishlistView()
	{
		InitializeComponent();
	}

    private void OK_Clicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel)?.AddOrUpdate();
        Shell.Current.GoToAsync("//WishlistManage");
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//WishlistManage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CartViewModel(CartId);
    }
}