using System;
using System.Linq;
using System.Threading.Tasks;
using EntregaSolidariaAPIs.Model;
using EntregaSolidariaAPIs.Model.Data;
using EntregaSolidariaAPIs.Model.ParametrosAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntregaSolidariaAPIs.Controllers
{
    public class APIsController : Controller
    {

        protected ContextoBanco Contexto { get; set; }

        public APIsController(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContextoBanco>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BancoAzure"));
            Contexto = new ContextoBanco(optionsBuilder.Options);
        }



        [HttpPost("/api/CadastrarUsuario")]
        public async Task<JsonResult> CadastrarUsuario([FromBody]CadastroUsuario cadastroUsuario)
        {
            if (!string.IsNullOrEmpty(cadastroUsuario.CPF) || !string.IsNullOrEmpty(cadastroUsuario.Email) || !string.IsNullOrEmpty(cadastroUsuario.Endereco)
                || !string.IsNullOrEmpty(cadastroUsuario.Nome) || !string.IsNullOrEmpty(cadastroUsuario.Senha) || !string.IsNullOrEmpty(cadastroUsuario.Telefone)
                || cadastroUsuario.TipoUsuario != 0 || cadastroUsuario.NumeroEndereco != 0)
            {
                try
                {
                    if (!await VerificaExistente(cadastroUsuario.CPF, cadastroUsuario.Email))
                    {
                        var usuario = new UsuarioViewModel();

                        usuario.CPF = cadastroUsuario.CPF;
                        usuario.Email = cadastroUsuario.Email;
                        usuario.Endereco = cadastroUsuario.Endereco;
                        usuario.Nome = cadastroUsuario.Nome;
                        usuario.Senha = cadastroUsuario.Senha;
                        usuario.Telefone = cadastroUsuario.Telefone;
                        usuario.TipoUsuario = cadastroUsuario.TipoUsuario;
                        usuario.NumeroEndereco = cadastroUsuario.NumeroEndereco;

                        await Contexto.UsuarioViewModel.AddAsync(usuario);
                        await Contexto.SaveChangesAsync();

                        return Json(new { mensagem = "Sucesso", resultado = true });
                    }
                    else
                    {
                        return Json(new { mensagem = "E-mail ou CPF ja cadastradps no sistema", resultado = false });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { mensagem = ex.Message, resultado = false });
                }
            }
            else
            {
                return Json(new { mensagem = "Todos os campos devem ser preenchidos", resultado = false });
            }
        }

        [HttpPost("/api/Login")]
        public async Task<JsonResult> Login([FromBody]Login login)
        {
            try
            {
                var usuario = await Contexto.UsuarioViewModel.AsNoTracking().Where(x => x.Email == login.Email && x.Senha == login.Senha).FirstOrDefaultAsync();

                if (usuario != null)
                {
                    return Json(new { mensagem = "Sucesso", resultado = true, Usuario = usuario });
                }
                else
                {
                    return Json(new { mensagem = "e-mail e/ou senha invalidos", resultado = false });

                }
            }
            catch (Exception ex)
            {

                return Json(new { mensagem = ex.Message, resultado = false });
            }
        }

        [HttpGet("/api/ListarComercios")]
        public async Task<JsonResult> ListarComercios()
        {
            try
            {
                var comercio = await Contexto.MercadoViewModel.AsNoTracking().ToListAsync();

                if (comercio.Count() >= 1)
                {
                    return Json(new { comercio = comercio, resultado = true });
                }
                else
                {
                    return Json(new { mensagem = "Nenhum comercio cadastrado", resultado = true });

                }

            }
            catch (Exception ex)
            {

                return Json(new { mensagem = ex.Message, resultado = false });
            }
        }

        private async Task<bool> VerificaExistente(string cPF, string email)
        {
            var duplicado = await Contexto.UsuarioViewModel.AsNoTracking().Where(x => x.Email == email || x.CPF == cPF).FirstOrDefaultAsync();

            if (duplicado != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}