using System;

namespace EntregaSolidariaAPIs.Model.ParametrosAPI
{
    public class CadastroUsuario
    {
        public int TipoUsuario { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public int NumeroEndereco { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
    }

    public class Login : CadastroUsuario
    {

    }

    public class CadastroComercio
    {
        public string RazaoSocial { get; set; }
        public string MercadoEndereco { get; set; }
        public string CNPJ { get; set; }
        public string MercadoTelefone { get; set; }
        public int IdUsuario { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFim { get; set; }
    }

    public class CadastrarProduto
    {
        public int IdProduto { get; set; }
        public int IdMercado { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
    }

    public class CriarPedido
    {
        public int IdProduto { get; set; }
        public int IdMercado { get; set; }
        public int Quantidade { get; set; }
        public int IdUsuario { get; set; }
    }

    public class AdicionarPedido
    {
        public int IdProduto { get; set; }
        public int IdMercado { get; set; }
        public int Quantidade { get; set; }
        public int IdUsuario { get; set; }
        public int IdPedido { get; set; }
    }
}
