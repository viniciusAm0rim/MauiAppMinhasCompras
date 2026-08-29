using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Helpers;
using System.Collections.ObjectModel;
using MauiAppMinhasCompras.Views;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // 1. A lista de segurança com todos os dados originais intactos
    private List<Produto> _listaOriginal = new();

    // 2. A lista que atualiza a tela em tempo real
    public ObservableCollection<Produto> ListaNaTela { get; set; } = new();

    public ListaProduto()
    {
        InitializeComponent();

        // ESSENCIAL: Diz para a tela onde procurar as propriedades (como a ListaNaTela)
        BindingContext = this;
    }

    // 3. O método com o nome exato que está no XAML (SearchBar_TextChanged)
    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        string textoDigitado = e.NewTextValue?.Trim() ?? string.Empty;

        ListaNaTela.Clear();

        var resultados = string.IsNullOrWhiteSpace(textoDigitado)
            ? _listaOriginal
            : _listaOriginal.Where(p => p.Descricao != null && p.Descricao.Contains(textoDigitado, StringComparison.OrdinalIgnoreCase));

        foreach (var item in resultados)
        {
            ListaNaTela.Add(item);
        }
    }

    // Método para carregar os produtos do banco de dados (ou da sua fonte de dados)
    private async Task CarregarProdutos()
    {
        // 1. Busca os dados reais salvos (exemplo simulando um método de banco de dados)
        var ListaBanco = await App.Db.GetAll(); // Substitua pela sua chamada real de banco

        // 2. Atualiza a nossa lista de segurança
        _listaOriginal = ListaBanco;

        // 3. Atualiza a lista da tela para exibir tudo inicialmente
        ListaNaTela.Clear();
        foreach (var produto in _listaOriginal)
        {
            ListaNaTela.Add(produto);
        }
    }
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        // Vai para a tela de cadastro e ESPERA o usuário voltar dela
        await Navigation.PushAsync(new Views.NovoProduto());

        // Assim que o usuário volta, chamamos o banco de novo para pegar o produto recém-adicionado!
        var listaDoBanco = await App.Db.GetAll();

        _listaOriginal = listaDoBanco;

        ListaNaTela.Clear();
        foreach (var p in _listaOriginal)
        {
            ListaNaTela.Add(p);
        }
    }

    private async void OnProdutoTapped(object sender, TappedEventArgs e)
    {
        // O Parameter que passamos lá no XAML vem guardado aqui no e.Parameter
        Produto produtoSelecionado = e.Parameter as Produto;

        if (produtoSelecionado != null)
        {
            // Exemplo: Mostrar um alerta com o ID ou Descrição do produto clicado
            await DisplayAlert("Produto Clicado", $"Você clicou em: {produtoSelecionado.Descricao}", "OK");


        }
    }
}