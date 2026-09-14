namespace MauiAppMinhasCompras.Views;

using MauiAppMinhasCompras.Helpers;

public partial class RelatorioCategoria : ContentPage
{
    public RelatorioCategoria()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            var relatorio = await App.Db.GetRelatorioPorCategoria();
            lst_relatorio.ItemsSource = relatorio;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}