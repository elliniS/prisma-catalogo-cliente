using Microsoft.JSInterop;
using PrismaCatalogo.Front.Cliente.Models;
using System.Linq;
using System.Security.Cryptography.Pkcs;

namespace PrismaCatalogo.Front.Cliente.Models
{
    public class ProdutoAvaliacoesViewModel
    {
        public int ProdutoId { get; set; }
        public int QuantTotal { get; set; }
        public float AvaliacaoMedia { get; set; }
        public float[] PosentagemAvaliacao { get; set; }
        public IEnumerable<AvaliacaoViewModel> Avaliacoes { get; set; }

        public ProdutoAvaliacoesViewModel(int produtoId, IEnumerable<AvaliacaoViewModel> avaliacoes)
        {
            if (avaliacoes == null)
            {
                ProdutoId = produtoId;
                QuantTotal = 0;
                AvaliacaoMedia = 0;
                PosentagemAvaliacao = [0, 0, 0, 0, 0];
                Avaliacoes = new List<AvaliacaoViewModel>();
            }
            else
            {
                ProdutoId = produtoId;
                QuantTotal = avaliacoes.Count();
                AvaliacaoMedia = avaliacoes.Sum(a => a.Nota) / QuantTotal;
                PosentagemAvaliacao = [0, 0, 0, 0, 0];
                Avaliacoes = avaliacoes;

                for (int i = 0; i < PosentagemAvaliacao.Count(); i++) {
                    var av = avaliacoes.Where(a => a.Nota == i + 1).ToList();

                    if(av != null)
                    {
                        PosentagemAvaliacao[i] = (av.Count() * 100) / QuantTotal;
                    }
                    
                }



            }

        }

    }
}
