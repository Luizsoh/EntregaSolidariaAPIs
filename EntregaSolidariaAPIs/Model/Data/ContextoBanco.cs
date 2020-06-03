using Microsoft.EntityFrameworkCore;

namespace EntregaSolidariaAPIs.Model.Data
{
    public class ContextoBanco : DbContext
    {

        public DbSet<UsuarioViewModel> UsuarioViewModel { get; set; }
        public DbSet<MercadoViewModel> MercadoViewModel { get; set; }

        public ContextoBanco(DbContextOptions<ContextoBanco> options): base(options){
        }
    }
}
