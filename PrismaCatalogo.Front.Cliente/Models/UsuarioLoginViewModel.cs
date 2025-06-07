﻿using PrismaCatalogo.Enuns;
using System.ComponentModel.DataAnnotations;

namespace PrismaCatalogo.Front.Cliente.Models
{
    public class UsuarioLoginViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nome de Usuario")]
        public string NomeUsuario { get; set; }
        public string Senha { get; set; }
        [Display(Name = "Lembre-se de mim")]
        public bool FgLembra { get; set; }
        public string? ReturnUrl { get; set; }
        //public EnumUsuarioTipo UsuarioTipo { get; set; }
    }
}
