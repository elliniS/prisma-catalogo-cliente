using Microsoft.AspNetCore.Mvc;
using PrismaCatalogo.Front.Cliente.Services;
using PrismaCatalogo.Front.Cliente.Services.Interfaces;

namespace PrismaCatalogo.Front.Cliente.Components
{
    public class CategoriaProdutos : ViewComponent
    {
        private IProdutoService _produtoService;

        public CategoriaProdutos(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int categoriaId)
        {
            var produtos = (await _produtoService.GetByCategoria(categoriaId)).Where(p => p.Ativo);

            return View(produtos);
        }
    }
}
