using PrismaCatalogo.Front.Cliente.Models;
using PrismaCatalogo.Front.Cliente.Services.Interfaces;

namespace PrismaCatalogo.Front.Cliente.Services.Interfaces
{
    public interface IProdutoService : IService<ProdutoViewModel>
    {
        Task<IEnumerable<ProdutoViewModel>> GetByCategoria(int categoriaId);
    }
}