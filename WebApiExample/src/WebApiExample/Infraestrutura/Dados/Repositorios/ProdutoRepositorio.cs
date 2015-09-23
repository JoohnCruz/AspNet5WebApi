using System.Collections.Generic;
using System.Linq;
using WebApiExample.Infraestrutura.Dados.Entidades;

namespace WebApiExample.Infraestrutura.Dados.Repositorios
{
    public class ProdutoRepositorio
    {
        private readonly List<Produto> Produtos = new List<Produto>();

        public ProdutoRepositorio()
        {
            Enumerable.Range(1, 100)
                .ToList()
                .ForEach(x =>
                {
                    Produtos.Add(new Produto { Id = x, Name = $"Produto {x}", Value = x });
                });
        }

        public IEnumerable<Produto> ObterProdutos()
        {
            return Produtos;
        }

        public Produto Obter(int id)
        {
            return Produtos.SingleOrDefault(x => x.Id == id);
        }

        public int Criar(string nome, decimal valor)
        {
            var proximoId = Produtos.Select(x => x.Id).Max() + 1;

            Produtos.Add(new Produto { Id = proximoId, Name = nome, Value = valor });

            return proximoId;
        }

        public void Remover(int id)
        {
            Produtos.RemoveAll(x => x.Id == id);
        }

        public void Update(Produto entidade)
        {
            var produto = Obter(entidade.Id);
            if (produto == null)
                return;

            produto.Name = entidade.Name;
            produto.Value = entidade.Value;
        }
    }
}