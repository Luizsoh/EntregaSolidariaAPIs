using System;
using System.Linq;
using System.Threading.Tasks;
using EntregaSolidariaAPIs.Model;
using EntregaSolidariaAPIs.Model.Data;
using EntregaSolidariaAPIs.Model.ParametrosAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
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
            if (!string.IsNullOrEmpty(cadastroUsuario.CPF) && !string.IsNullOrEmpty(cadastroUsuario.Email) && !string.IsNullOrEmpty(cadastroUsuario.Endereco)
                && !string.IsNullOrEmpty(cadastroUsuario.Nome) && !string.IsNullOrEmpty(cadastroUsuario.Senha) && !string.IsNullOrEmpty(cadastroUsuario.Telefone)
                && cadastroUsuario.TipoUsuario != 0 && cadastroUsuario.NumeroEndereco != 0)
            {
                try
                {
                    if (!await VerificaUsuarioExistente(cadastroUsuario.CPF, cadastroUsuario.Email))
                    {
                        var usuario = new UsuarioViewModel()
                        {
                            CPF = cadastroUsuario.CPF,
                            Email = cadastroUsuario.Email,
                            Endereco = cadastroUsuario.Endereco,
                            Nome = cadastroUsuario.Nome,
                            Senha = cadastroUsuario.Senha,
                            Telefone = cadastroUsuario.Telefone,
                            TipoUsuario = cadastroUsuario.TipoUsuario,
                            NumeroEndereco = cadastroUsuario.NumeroEndereco
                        };

                        await Contexto.UsuarioViewModel.AddAsync(usuario);
                        await Contexto.SaveChangesAsync();

                        return Json(new { usuario, resultado = true });
                    }
                    else
                    {
                        return Json(new { mensagem = "E-mail ou CPF ja cadastrados no sistema", resultado = false });
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
                    usuario.Senha = null;
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

        [HttpPost("/api/CadastrarComercio")]
        public async Task<JsonResult> CadastrarComercio([FromBody]CadastroComercio cadastroComercio)
        {
            if (!string.IsNullOrEmpty(cadastroComercio.CNPJ) && !string.IsNullOrEmpty(cadastroComercio.MercadoEndereco) && !string.IsNullOrEmpty(cadastroComercio.RazaoSocial)
                && !string.IsNullOrEmpty(cadastroComercio.MercadoTelefone) && cadastroComercio.IdUsuario != 0 && cadastroComercio.HoraInicio != DateTime.MinValue
                && cadastroComercio.HoraFim != DateTime.MinValue)
            {
                try
                {
                    if (!await VerificaMercadoExistente(cadastroComercio.CNPJ))
                    {
                        var mercado = new MercadoViewModel()
                        {
                            MercadoTelefone = cadastroComercio.MercadoTelefone,
                            CNPJ = cadastroComercio.CNPJ,
                            MercadoEndereco = cadastroComercio.MercadoEndereco,
                            RazaoSocial = cadastroComercio.RazaoSocial,
                            IdUsuario = cadastroComercio.IdUsuario,
                            HoraInicio = cadastroComercio.HoraInicio,
                            HoraFim = cadastroComercio.HoraFim
                        };



                        await Contexto.MercadoViewModel.AddAsync(mercado);
                        await Contexto.SaveChangesAsync();

                        return Json(new { mensagem = "Sucesso", resultado = true });
                    }
                    else
                    {
                        return Json(new { mensagem = "CNPJ ja cadastrado", resultado = false });
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

        [HttpPost("/api/CadastrarProduto")]
        public async Task<JsonResult> CadastrarProduto([FromBody]CadastrarProduto cadastroProduto)
        {
            if (!string.IsNullOrEmpty(cadastroProduto.Descricao) && cadastroProduto.IdMercado != 0 && cadastroProduto.Preco != 0 && cadastroProduto.Estoque != 0)
            {
                try
                {
                    var produto = new ProdutoViewModel()
                    {
                        Descricao = cadastroProduto.Descricao,
                        IdMercado = cadastroProduto.IdMercado,
                        Preco = cadastroProduto.Preco,
                        Estoque = cadastroProduto.Estoque
                    };



                    await Contexto.ProdutoViewModel.AddAsync(produto);
                    await Contexto.SaveChangesAsync();

                    return Json(new { mensagem = "Sucesso", resultado = true });
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

        [HttpGet("/api/ListarProdutos")]
        public async Task<JsonResult> ListarProdutos(int IdMercado)
        {
            if (IdMercado != 0)
            {
                try
                {
                    var produtos = await Contexto.ProdutoViewModel.Where(x => x.IdMercado == IdMercado).AsNoTracking().ToListAsync();

                    if (produtos.Count() > 0)
                    {
                        return Json(new { produtos, resultado = true });
                    }
                    else
                    {
                        return Json(new { mensagem = "Não foram encontrados produtos para o comercio informado", resultado = true });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { mensagem = ex.Message, resultado = false });
                }
            }
            else
            {
                return Json(new { mensagem = "Um comércio deve ser informado", resultado = false });
            }
        }

        [HttpPost("/api/EditarProduto")]
        public async Task<JsonResult> EditarProduto([FromBody]CadastrarProduto editProduto)
        {

            try
            {
                var produto = await Contexto.ProdutoViewModel.Where(x => x.IdMercado == editProduto.IdMercado && x.IdProduto == editProduto.IdProduto).FirstOrDefaultAsync();

                if (produto != null)
                {
                    produto.Preco = editProduto.Preco;
                    produto.Descricao = editProduto.Descricao;
                    produto.Estoque = editProduto.Estoque;

                    Contexto.ProdutoViewModel.Update(produto);
                    await Contexto.SaveChangesAsync();

                    return Json(new { produto, resultado = true });
                }
                else
                {
                    return Json(new { mensagem = "Produto não encontrado", resultado = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { mensagem = ex.Message, resultado = false });
            }
        }

        [HttpPost("/api/CriaPedido")]
        public async Task<JsonResult> CriaPedido([FromBody]CriarPedido criarPedido)
        {
            if (criarPedido.IdMercado > 0 && criarPedido.IdProduto > 0 && criarPedido.Quantidade > 0 && criarPedido.IdUsuario > 0)
            {
                try
                {
                    var produtoPedido = new ProdutoPedidoViewModel()
                    {
                        IdMercado = criarPedido.IdMercado,
                        IdProduto = criarPedido.IdProduto,
                        Quantidade = criarPedido.Quantidade
                    };

                    await Contexto.ProdutoPedidoViewModel.AddAsync(produtoPedido);
                    await Contexto.SaveChangesAsync();

                    var pedido = new PedidoViewModel()
                    {
                        IdUsuarioSolic = criarPedido.IdUsuario,
                        IdMercado = criarPedido.IdMercado,
                        ProdutosPedidos = produtoPedido.IdProdutoPedido.ToString(),
                        StatusPedido = 0,
                        DataSolicitada = DateTime.Now
                    };

                    await Contexto.PedidoViewModel.AddAsync(pedido);
                    await Contexto.SaveChangesAsync();

                    produtoPedido.IdPedido = pedido.IdPedido;

                    Contexto.ProdutoPedidoViewModel.Update(produtoPedido);
                    await Contexto.SaveChangesAsync();

                    return Json(new { mensagem = "Pedido Criado", resultado = true });

                }
                catch (Exception ex)
                {
                    return Json(new { mensagem = ex.Message, resultado = false });
                }
            }
            else
            {
                return Json(new { mensagem = "Campos obrigatorio não preenchidos", resultado = false });
            }
        }

        [HttpPost("/api/AdicionarPedido")]
        public async Task<JsonResult> AdicionarPedido([FromBody]AdicionarPedido adicionarPedido)
        {
            if (adicionarPedido.IdMercado > 0 && adicionarPedido.IdProduto > 0 && adicionarPedido.Quantidade > 0 && adicionarPedido.IdUsuario > 0 && adicionarPedido.IdPedido != 0)
            {
                try
                {
                    var produtoPedido = new ProdutoPedidoViewModel()
                    {
                        IdMercado = adicionarPedido.IdMercado,
                        IdProduto = adicionarPedido.IdProduto,
                        Quantidade = adicionarPedido.Quantidade,
                        IdPedido = adicionarPedido.IdPedido
                    };

                    await Contexto.ProdutoPedidoViewModel.AddAsync(produtoPedido);
                    await Contexto.SaveChangesAsync();

                    var pedido = await Contexto.PedidoViewModel.Where(x => x.IdPedido == adicionarPedido.IdPedido).FirstOrDefaultAsync();
                    if (pedido != null)
                    {
                        pedido.ProdutosPedidos = string.Concat(pedido.ProdutosPedidos, ",", produtoPedido.IdProdutoPedido);

                        Contexto.PedidoViewModel.Update(pedido);
                        await Contexto.SaveChangesAsync();

                        return Json(new { mensagem = "Pedido Criado", resultado = true });
                    }
                    else
                    {
                        return Json(new { mensagem = "Pedido informado não encontrado", resultado = false });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { mensagem = ex.Message, resultado = false });
                }
            }
            else
            {
                return Json(new { mensagem = "Campos obrigatorio não preenchidos", resultado = false });
            }
        }

        [HttpGet("/api/ListarPedidos")]
        public async Task<JsonResult> ListarPedidos(int idUsuario)
        {
            if (idUsuario != 0)
            {
                try
                {
                    var pedidos = await Contexto.PedidoViewModel.Where(x => x.IdUsuarioSolic == idUsuario).AsNoTracking().ToListAsync();

                    if (pedidos.Count() > 0)
                    {
                        foreach (var item in pedidos)
                        {
                            item.produtos = await Contexto.ProdutoPedidoViewModel.Where(x => x.IdPedido == item.IdPedido).AsNoTracking().ToListAsync();
                        }

                        return Json(new { pedidos, resultado = true });
                    }
                    else
                    {
                        return Json(new { mensagem = "Não foram encontrados pedidos", resultado = false });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { mensagem = ex.Message, resultado = false });
                }
            }
            else
            {
                return Json(new { mensagem = "Um usuario deve ser informado", resultado = false });
            }
        }

        [HttpGet("/api/VisualizarPedido")]
        public async Task<JsonResult> VisualizarPedido(int idPedido)
        {
            if (idPedido != 0)
            {
                try
                {
                    var pedido = await Contexto.PedidoViewModel.Where(x => x.IdPedido == idPedido).AsNoTracking().FirstOrDefaultAsync();

                    if (pedido != null)
                    {
                        pedido.produtos = await Contexto.ProdutoPedidoViewModel.Where(x => x.IdPedido == pedido.IdPedido).AsNoTracking().ToListAsync();

                        return Json(new { pedido, resultado = true });
                    }
                    else
                    {
                        return Json(new { mensagem = "Pedido não encontrado", resultado = false });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { mensagem = ex.Message, resultado = false });
                }
            }
            else
            {
                return Json(new { mensagem = "Um pedido deve ser informado", resultado = false });
            }
        }

        private async Task<bool> VerificaMercadoExistente(string cNPJ)
        {
            var duplicado = await Contexto.MercadoViewModel.AsNoTracking().Where(x => x.CNPJ == cNPJ).FirstOrDefaultAsync();

            if (duplicado != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> VerificaUsuarioExistente(string cPF, string email)
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