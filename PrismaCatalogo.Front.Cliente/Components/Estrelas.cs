using Microsoft.AspNetCore.Mvc;

namespace PrismaCatalogo.Front.Cliente.Components
{
    public class Estrelas : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(int num)
        {
            //var produtos = await _produtoService.GetByCategoria(categoriaId);


            return View(num);
        }
    }
}
