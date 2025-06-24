using PrismaCatalogo.Front.Cliente.Models;
using PrismaCatalogo.Front.Cliente.Services.Interfaces;

namespace PrismaCatalogo.Front.Cliente.Services.Interfaces
{
    public interface IUsuarioService : IService<UsuarioViewModel>
    {
        Task<UsuarioViewModel> Login(UsuarioLoginViewModel usuarioViewModel);
        Task<ReenviaSenhaViewModel> CodigoReenviaSenha(UsuarioLoginViewModel reenviaSenhaViewModel);
        Task<ReenviaSenhaViewModel> VerificaCodigo(ReenviaSenhaViewModel reenviaSenhaViewModel);
        Task<ReenviaSenhaViewModel> AlteraSenha(ReenviaSenhaViewModel reenviaSenhaViewModel);

    }
}