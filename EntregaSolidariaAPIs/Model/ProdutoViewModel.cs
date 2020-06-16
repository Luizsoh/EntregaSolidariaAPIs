using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntregaSolidariaAPIs.Model
{
    [Table("PRODUTOMERCADO")]
    public class ProdutoViewModel
    {
        [Key]
        [Column("PROD_IDPRODUTO")]
        public int IdProduto { get; set; }

        [Column("MERC_IDMERCADO")]
        public int IdMercado { get; set; }

        [Column("PROD_PRECO")]
        public double Preco { get; set; }

        [Column("PROD_DESCRICAO")]
        public string Descricao { get; set; }

        [Column("PROD_ESTOQUE")]
        public int Estoque { get; set; }
    }
}
