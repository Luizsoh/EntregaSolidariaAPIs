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
}
