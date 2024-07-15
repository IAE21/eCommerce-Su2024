using IMM.Services;
using IMM.MAUI.ViewModels;

namespace IMM.MAUI.Views;

public partial class TaxRateView : ContentPage
{
    public TaxRateView()
    {
        InitializeComponent();
        BindingContext = new TaxRateViewModel();
    }

    private void OK_Clicked(object sender, EventArgs e)
    {
        (BindingContext as TaxRateViewModel).UpdateTaxRate();
        Shell.Current.GoToAsync("//Inventory");
    }

    private void Cancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
    }
}