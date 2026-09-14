using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<int> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descriçao = ?, Categoria = ?, Quantidade = ?, Preço = ? WHERE Id = ?";
            return _conn.ExecuteAsync(sql, p.Descriçao, p.Categoria, p.Quantidade, p.Preço, p.Id);
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE Descriçao LIKE ?";
            return _conn.QueryAsync<Produto>(sql, "%" + q + "%");
        }

        // Novo: Filtrar por categoria específica
        public Task<List<Produto>> GetByCategory(string categoria)
        {
            string sql = "SELECT * FROM Produto WHERE Categoria = ?";
            return _conn.QueryAsync<Produto>(sql, categoria);
        }

        // Novo: Relatório de total gasto agrupado por categoria
        public async Task<List<CategoriaGasto>> GetRelatorioPorCategoria()
        {
            string sql = "SELECT Categoria, SUM(Quantidade * Preço) as TotalGasto FROM Produto GROUP BY Categoria";
            var resultado = await _conn.QueryAsync<CategoriaGastoReportQuery>(sql);

            return resultado.Select(r => new CategoriaGasto
            {
                Categoria = string.IsNullOrEmpty(r.Categoria) ? "Geral / Sem Categoria" : r.Categoria,
                TotalGasto = r.TotalGasto
            }).ToList();
        }
    }

    // Classes auxiliares para mapear o relatório de agrupamento
    public class CategoriaGastoReportQuery
    {
        public string Categoria { get; set; }
        public double TotalGasto { get; set; }
    }

    public class CategoriaGasto
    {
        public string Categoria { get; set; }
        public double TotalGasto { get; set; }
    }
}
