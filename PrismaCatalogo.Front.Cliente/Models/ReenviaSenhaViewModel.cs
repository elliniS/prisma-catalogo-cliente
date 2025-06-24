using PrismaCatalogo.Enuns;
using System.ComponentModel.DataAnnotations;

namespace PrismaCatalogo.Front.Cliente.Models
{
    public class ReenviaSenhaViewModel
    {
        public int UsuarioId { get; set; }
        public string? Codigo { get; set; }
        public string? Senha {  get; set; }
        public string? ConfirmaSenha { get; set; }
    }
}
