using PrismaCatalogo.Enuns;
using System.ComponentModel.DataAnnotations;

namespace PrismaCatalogo.Front.Cliente.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        [Display(Name = "Nome de Usuario")]
        public string? NomeUsuario { get; set; }
        public string Email { get; set; }
        public string? Senha { get; set; }
        [Display(Name = "Confirme sua Senha")]
        public string? SenhaConfirma { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken {  get; set; }
        public bool FgLembra { get; set; }
        public EnumUsuarioTipo UsuarioTipo { get; set; }
    }
}
