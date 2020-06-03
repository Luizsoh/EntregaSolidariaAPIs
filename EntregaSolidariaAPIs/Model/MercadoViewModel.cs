using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntregaSolidariaAPIs.Model
{
    [Table("MERCADO")]
    public class MercadoViewModel
    {
        [Key]
        [Column("MERC_IDMERCADO")]
        public int IdMercado { get; set; }

        [Column("MERC_RAZAOSOCIAL")]
        public string RazaoSocial { get; set; }
        [Column("MERC_ENDERECO")]
        public string MercadoEndereco { get; set; }

        [Column("MERC_CNPJ")]
        public string CNPJ { get; set; }

        [Column("MERC_TELEFONE")]
        public string MercadoTelefone { get; set; }

        [Column("USR_IDUSUARIO")]
        public int IdUsuario { get; set; }

        [Column("MERC_HORAINICIO")]
        public DateTime HoraInicio { get; set; }

        [Column("MERC_HORAFIM")]
        public DateTime HoraFim { get; set; }
    }
}
