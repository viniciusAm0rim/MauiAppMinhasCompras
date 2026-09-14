using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descriçao;
        string _categoria;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Descriçao
        {
            get => _descriçao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Preencha a descrição");
                }
                _descriçao = value;
            }
        }

        public string Categoria
        {
            get => _categoria;
            set => _categoria = value;
        }

        public double Quantidade { get; set; }
        public double Preço { get; set; }

        public double Total => Quantidade * Preço;
    }
}