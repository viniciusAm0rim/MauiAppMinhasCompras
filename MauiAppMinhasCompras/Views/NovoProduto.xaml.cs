using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto
			{
				Descriçao = txt_descr.Text,
				Quantidade = Convert.ToDouble(txt_quant.Text),
				Preço = Convert.ToDouble(txt_Preço.Text)
			};

			 await App.Db.Insert(p);
			await DisplayAlertAsync("Sucesso", "Registo Concluido", "Ok");
			

		} catch(Exception ex)
		{
			 await DisplayAlertAsync("Erro", ex.Message, "OK");
		}
    }
}