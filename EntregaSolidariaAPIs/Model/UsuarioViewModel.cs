using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntregaSolidariaAPIs.Model
{
    [Table("USUARIO")]
    public class UsuarioViewModel
    {
        [Key]
        [Column("USR_IDUSUARIO")]
        public int IdUsuario { get; set; }

        [Column("USR_TIPOUSUARIO")]
        public int TipoUsuario { get; set; }

        [Column("USR_NOME")]
        public string Nome { get; set; }

        [Column("USR_CPF")]
        public string CPF { get; set; }

        [Column("USR_ENDERECO")]
        public string Endereco { get; set; }

        [Column("USR_NUMEROEND")]
        public int NumeroEndereco { get; set; }

        [Column("USR_EMAIL")]
        public string Email { get; set; }

        [Column("USR_TELEFONE")]
        public string Telefone { get; set; }

        [Column("USR_SENHA")]
        public string Senha { get; set; }
    }
}
