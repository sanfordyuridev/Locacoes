using Locacoes.Api.Configuracoes;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;
using Swashbuckle.AspNetCore.SwaggerUI;
using Locacoes.Service.AutoMapper;
using Locacoes.Api.Dependecies;

namespace Locacoes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Name = configuration.GetValue<string>("Application:Name");
            Version = configuration.GetValue<string>("Application:Version");
            DefaultConnection = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        }

        public string Name { get; }

        public string Version { get; }

        public string DefaultConnection { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddJwtSetup();
            services.AddAutoMapper(typeof(AutoMapping));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.RegisterDependencies(DefaultConnection);
            services.AddDataProtection()
                .UseCryptographicAlgorithms(
                    new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(ui =>
            {
                ui.EnableFilter();
                ui.DocumentTitle = Name + " - API";
                ui.DocExpansion(DocExpansion.None);
                ui.InjectStylesheet("/swagger-ui/custom.css");
                ui.SwaggerEndpoint("/swagger/v" + Version + "/swagger.json", Name + " V" + Version);
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
