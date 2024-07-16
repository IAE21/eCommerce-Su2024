using IMM.MAUI.ViewModels;

namespace IMM.MAUI.Views;

[QueryProperty(nameof(ItemId), "itemId")]
public partial class ItemView : ContentPage
{
    public int ItemId { get; set; }
	public ItemView()
	{
		InitializeComponent();
	}

    private void OK_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ItemViewModel)?.AddOrUpdate();
        Shell.Current.GoToAsync("//Inventory");
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ItemViewModel(ItemId);
    }
}