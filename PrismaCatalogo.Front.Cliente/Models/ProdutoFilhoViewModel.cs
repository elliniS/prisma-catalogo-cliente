﻿using System.ComponentModel.DataAnnotations;

namespace PrismaCatalogo.Front.Cliente.Models
{
    public class ProdutoFilhoViewModel
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
        public double? Preco { get; set; }
        [Display(Name = "Quantidade em estoque")]
        public int? QuantEstoque { get; set; }
        public bool Ativo { get; set; }
        public FotoViewModel FotoCapa { get; set; }

        [Display(Name = "Tamanho")]
        public int? TamanhoId { get; set; }
        public TamanhoViewModel Tamanho { get; set; }
        [Display(Name = "Cor")]
        public int? CorId { get; set; }
        public CorViewModel Cor { get; set; }

        public List<FotoViewModel>? Fotos { get; set; }
    }
}
