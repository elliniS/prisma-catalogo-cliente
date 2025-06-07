using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrismaCatalogo.Front.Cliente.Models;
using PrismaCatalogo.Front.Cliente.Services.Interfaces;
using System.Diagnostics;

namespace PrismaCatalogo.Front.Cliente.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProdutoService _produtoService;
        private ICategoriaService _categoriaService;

        public HomeController(ILogger<HomeController> logger, IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _logger = logger;
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        { 
            var categorias = await _categoriaService.GetAll();

            return View(categorias);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
