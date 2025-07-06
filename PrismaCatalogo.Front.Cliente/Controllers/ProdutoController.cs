using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrismaCatalogo.Front.Cliente.Comparers;
using PrismaCatalogo.Front.Cliente.Models;
using PrismaCatalogo.Front.Cliente.Services.Interfaces;
using System.Diagnostics;

namespace PrismaCatalogo.Front.Cliente.Controllers
{
    //[Authorize]
    public class ProdutoController : Controller
    {
        private IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService) 
        {
            _produtoService = produtoService;
        }

        public async Task<IActionResult> Index(int produtoId)
        { 
            var produto = await _produtoService.FindById(produtoId);

            if (produto.ProdutosFilhos != null) {
                var tamanhos= produto.ProdutosFilhos.Select(p => p.Tamanho).Distinct().ToList();

                //ViewBag.ProdutoTamanhos = new SelectList(tamanhos, "Id", "Nome");
                var compCor = new CorComparer();
                var compTamanho = new TamanhoComparer();

                ViewBag.ProdutoCores = produto.ProdutosFilhos.Select(p => p.Cor).Distinct(compCor).ToList();
                ViewBag.ProdutoTamanhos = produto.ProdutosFilhos.Select(p => p.Tamanho).Distinct(compTamanho).ToList();

            }

            var produtoDetalhe = new ProdutoDetalheViewModel(produto);

            //if (produto.Fotos != null && produto.Fotos.Count() > 0)
            //    produto.Fotos.OrderBy(f => f.Id).FirstOrDefault().FgPrincipal = true;

            
            return View(produtoDetalhe);

        }

        [HttpGet]
        public async Task<IActionResult> SelecionaFilho(int produtoId, int? corId, int? tamanhoId)
        {
            var produto = await _produtoService.FindById(produtoId);

           if (produto.ProdutosFilhos != null)
            {
                var compCor = new CorComparer();
                var compTamanho = new TamanhoComparer();

                ViewBag.ProdutoCores = produto.ProdutosFilhos.Select(p => p.Cor).Distinct(compCor).ToList();
                ViewBag.ProdutoTamanhos = produto.ProdutosFilhos.Select(p => p.Tamanho).Distinct(compTamanho).ToList();
            }

            ProdutoDetalheViewModel produtoDetalhe = new ProdutoDetalheViewModel(produto, corId, tamanhoId); //tamanhoId ==null ? new ProdutoDetalheViewModel(produto, corId) : new ProdutoDetalheViewModel(produto, corId, tamanhoId);



            //var selectListsTamanho = new SelectList(produtoDetalhe.Tamanhos, "Id", "Nome");

            //if(tamanhoId != null)
            //{
            //    var item = selectListsTamanho.Where(p => p.Value == tamanhoId.ToString()).FirstOrDefault();

            //    if (item != null)
            //    {
            //        item.Selected = true;
            //    } 
            //}

            //ViewBag.ProdutoTamanhos = selectListsTamanho;



            return View("index", produtoDetalhe);
        }

        //[HttpPost]
        //public async Task<IActionResult> SelecionaFilho(int produtoId, int? corId, int? tamahoId)
        //{
        //    var produto = await _produtoService.FindById(produtoId);

        //    if (produto.ProdutosFilhos != null)
        //    {
        //        var tamanhos = produto.ProdutosFilhos.Select(p => p.Tamanho).Distinct().ToList();

        //        ViewBag.ProdutoTamanhos = new SelectList(tamanhos, "Id", "Nome");
        //        ViewBag.ProdutoCores = produto.ProdutosFilhos.Select(p => p.Cor).Distinct().ToList();
        //    }

        //    var produtoDetalhe = new ProdutoDetalheViewModel(produto, corId, tamahoId);

        //    return View();
        //}
    }
}
