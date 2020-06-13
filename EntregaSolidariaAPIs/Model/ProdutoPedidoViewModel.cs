using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntregaSolidariaAPIs.Model
{
    [Table("PRODUTOPEDIDO")]
    public class ProdutoPedidoViewModel
    {
        [Key]
        [Column("PRODPED_IDPRODUTOPEDIDO")]
        public int IdProdutoPedido { get; set; }

        [Column("PROD_IDPRODUTO")]
        public int IdProduto { get; set; }

        [Column("MERC_IDMERCADO")]
        public int IdMercado { get; set; }

        [Column("PED_IDPEDIDO")]
        public int IdPedido { get; set; }

        [Column("PRODPED_QUANTIDADE")]
        public int Quantidade { get; set; }

    }
}
