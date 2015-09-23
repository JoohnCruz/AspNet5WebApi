using Microsoft.AspNet.Mvc;
using System.Net;
using WebApiExample.Infraestrutura.Dados.Entidades;
using WebApiExample.Infraestrutura.Dados.Repositorios;

namespace WebApiExample.Controllers
{
    [Route("api/produtos")]
    public class ProductsController : Controller
    {
        private static ProdutoRepositorio Repositorio = new ProdutoRepositorio();

        [HttpGet]
        public IActionResult Get()
        {
            var produtos = Repositorio.ObterProdutos();

            Context.Response.StatusCode = (int)HttpStatusCode.Found;
            return new ObjectResult(produtos);
        }

        [HttpGet("{id}", Name = "ObterProduto")]
        public IActionResult Get(int id)
        {
            var produto = Repositorio.Obter(id);
            if (produto == null)
                return HttpNotFound();

            Context.Response.StatusCode = (int)HttpStatusCode.Found;
            return new ObjectResult(produto);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Produto produto)
        {
            var id = Repositorio.Criar(produto.Name, produto.Value);

            string url = Url.RouteUrl("ObterProduto", new { id }, Request.Scheme, Request.Host.ToUriComponent());

            Context.Response.Headers["Location"] = url;
            return new HttpStatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Produto ProdutoRequest)
        {
            var produto = Repositorio.Obter(id);
            if (produto == null)
                return HttpNotFound();

            if (id != ProdutoRequest.Id)
                return HttpBadRequest("Post the attribute: Id properly");

            ProdutoRequest.Id = id;
            Repositorio.Update(ProdutoRequest);

            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var produto = Repositorio.Obter(id);
            if (produto == null)
                return HttpNotFound();

            Repositorio.Remover(id);

            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }
    }
}