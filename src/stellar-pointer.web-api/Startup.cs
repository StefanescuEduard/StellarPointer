using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StellarPointer.Business;
using StellarPointer.Business.Commands;
using StellarPointer.Business.Services;
using StellarPointer.Persistence;
using System.Text;

namespace StellarPointer.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(typeof(AuthenticateCommand).Assembly);

            IConfigurationSection settingsSection = Configuration.GetSection("AppSettings");
            AppSettings settings = settingsSection.Get<AppSettings>();
            byte[] signingKey = Encoding.UTF8.GetBytes(settings.EncryptionKey);
            services.Configure<AppSettings>(settingsSection);

            services.AddAuthentication(signingKey);
            services.AddCouchContext<StellarPointerContext>(builder => builder
                .UseEndpoint(settings.DatabaseSettings.ServerAddress)
                .UseBasicAuthentication(settings.DatabaseSettings.Username, settings.DatabaseSettings.Password)
                .EnsureDatabaseExists());
            services.AddTransient<UserRepository>();
            services.AddTransient<TokenService>();
            services.AddTransient<CredentialsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(c => c.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
