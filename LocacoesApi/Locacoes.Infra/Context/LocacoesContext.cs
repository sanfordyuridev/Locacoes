using Microsoft.EntityFrameworkCore;

namespace Locacoes.Infra.Context
{
    public class LocacoesContext : DbContext
    {
        public LocacoesContext(DbContextOptions<LocacoesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
