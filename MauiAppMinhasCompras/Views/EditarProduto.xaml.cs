namespace MauiAppMinhasCompras.Views;
using MauiAppMinhasCompras.Models;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto produto_anexado = BindingContext as Produto;

			Produto p = new Produto
			{
				Id = produto_anexado.Id,
				Descriçao = txt_descr.Text,
				Quantidade = Convert.ToDouble(txt_quant.Text),
				Preço = Convert.ToDouble(txt_preço.Text)
			};
			await App.Db.Uptade(p);
			await DisplayAlert("Sucesso", "Registro Concluido", "OK");
			await Navigation.PopAsync();

		}

		catch (Exception ex)
		{
			DisplayAlert("Erro", ex.Message, "OK");
		}
    }
}