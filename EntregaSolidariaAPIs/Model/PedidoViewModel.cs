using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntregaSolidariaAPIs.Model
{
    [Table("PEDIDO")]
    public class PedidoViewModel
    {
        [Key]
        [Column("PED_IDPEDIDO")]
        public int IdPedido { get; set; }

        [Column("PED_IDUSUARIOSOLIC")]
        public int IdUsuarioSolic { get; set; }

        [Column("MERC_IDMERCADO")]
        public int IdMercado { get; set; }

        [Column("PED_IDUSUARIOENTREG")]
        public int IdUsuarioEntreg { get; set; }

        [Column("PRODPED_PRODUTOS")]
        public string ProdutosPedidos { get; set; }

        [Column("PED_STATUSPEDIDO")]
        public int StatusPedido { get; set; }

        [Column("PED_DATASOLICITADA")]
        public DateTime DataSolicitada { get; set; }

        [Column("PED_DATAFINALIZADA")]
        public DateTime? DataFinalizada { get; set; }

        [Column("PED_USUARIOSNOTIFICADOS")]
        public string UsuariosNotificados { get; set; }


        [NotMapped]
        public List<ProdutoPedidoViewModel> produtos { get; set; }
    }
}
