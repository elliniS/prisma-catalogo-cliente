using PrismaCatalogo.Front.Cliente.Models;
using PrismaCatalogo.Front.Cliente.Services.Interfaces;

namespace PrismaCatalogo.Front.Cliente.Services.Interfaces
{
    public interface IProdutoFilhoService : IService<ProdutoFilhoViewModel>
    {
        Task<IEnumerable<ProdutoFilhoViewModel>> FindByPruduto(int id);
    }
}