using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using IdentityServerAspNetIdentity.Data;
using anubis.Data;

namespace IdentityServerAspNetIdentity
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
            services.AddDbContext<ApplicationDbContext>(options =>
            {

                options.UseOracle(
                    Configuration.GetConnectionString("DefaultConnection"));
            }
                );
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();

            services.AddIdentityServer()
                .AddInMemoryClients(new Client[] {
                    new Client
                    {
                        ClientId = "seth",
                        ClientName="DRCPFA FrontEnd",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequirePkce = true,
                        RequireClientSecret = false,
                        AllowAccessTokensViaBrowser = true,
                        RedirectUris = { "http://localhost:4200/auth-callback", "http://localhost:4200/silent-refresh.html" },
                        PostLogoutRedirectUris = { "http://localhost:4200/" },
                        AllowedCorsOrigins =  { "http://localhost:4200" },
                        AllowedScopes = { "openid", "profile", "email", "phone", "apiPagos" }
                    },
                    new Client {
                    ClientId = "osiris",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("Secreto@3#2!2018".Sha256())
                    },
                     AllowedScopes = { "apiPagos" }
                },
                })
                .AddInMemoryIdentityResources(new IdentityResource[] {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile(),
                    new IdentityResources.Email(),
                    new IdentityResources.Phone(),
                })
                .AddInMemoryApiScopes(configuration.getApiScopes())
                .AddInMemoryApiResources(configuration.apiResources)
                .AddAspNetIdentity<IdentityUser>();

            services.AddLogging(options =>
            {
                options.AddFilter("Duende", LogLevel.Debug);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
