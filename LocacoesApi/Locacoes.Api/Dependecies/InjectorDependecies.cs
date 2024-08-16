using Locacoes.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Locacoes.Api.Dependecies
{
    public static class InjectorDependencies
    {
        public static void RegisterDependencies(this IServiceCollection services, string defaultConnection)
        {
            services.AddDbContext<LocacoesContext>(options => options.UseNpgsql(defaultConnection));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 
            services.RegisterRepository();
            services.RegisterService();
        }
    }
}
