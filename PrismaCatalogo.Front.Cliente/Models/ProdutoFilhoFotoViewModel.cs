using Newtonsoft.Json;

namespace PrismaCatalogo.Front.Cliente.Models
{
    public class ProdutoFilhoFotoViewModel
    {
        public int Id { get; set; }

        public string? FotoBytes { get; set; }
        public bool FgExcluir { get; set; }
    }
}
