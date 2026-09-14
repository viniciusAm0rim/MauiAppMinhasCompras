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
                Categoria = picker_categoria.SelectedItem?.ToString() ?? "Outros",
                Quantidade = Convert.ToDouble(txt_quant.Text),
                Preço = Convert.ToDouble(txt_preço.Text)
            };

            await App.Db.Update(p);
            await DisplayAlert("Sucesso", "Registro Concluído", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}