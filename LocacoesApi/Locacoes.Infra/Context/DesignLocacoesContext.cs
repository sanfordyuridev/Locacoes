using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Locacoes.Infra.Context
{
    public class DesignLocacoesContext : IDesignTimeDbContextFactory<LocacoesContext>
    {
        public LocacoesContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<LocacoesContext> builder = new DbContextOptionsBuilder<LocacoesContext>();
            builder.UseNpgsql("Host=127.0.0.1;Database=Locacoes;Username=postgres;Password=1234;");
            return new LocacoesContext(builder.Options);
        }
    }
}
