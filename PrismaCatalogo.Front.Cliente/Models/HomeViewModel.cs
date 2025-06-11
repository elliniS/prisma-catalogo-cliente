using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PrismaCatalogo.Front.Cliente.Models
{
    public class HomeViewModel
    {
        public string? Pesquisa { get; set; }
        public IEnumerable<ProdutoViewModel>? ProdutoViewModels { get; set; }
        public IEnumerable<CategoriaViewModel>? CategoriasViewModels { get; set;}
    }
}
