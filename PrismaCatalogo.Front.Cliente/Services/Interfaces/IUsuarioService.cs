using PrismaCatalogo.Front.Cliente.Models;
using PrismaCatalogo.Front.Cliente.Services.Interfaces;

namespace PrismaCatalogo.Front.Cliente.Services.Interfaces
{
    public interface IUsuarioService : IService<UsuarioViewModel>
    {
        Task<UsuarioViewModel> Login(UsuarioLoginViewModel usuarioViewModel);
    }
}