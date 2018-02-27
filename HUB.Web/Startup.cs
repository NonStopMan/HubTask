using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Hub.Data;
using Hub.Domain.Handlers.Post;
using HUB.CrossCutting.Identity;
using HUB.Data;
using HUB.Data.Repositories.Implementation;
using HUB.Domain.Model;
using HUB.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.Data;
using HUB.CrossCutting.Framework.Identity;
//using NetCore.Data;

namespace HUB.Web
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
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddAzureAd(options => Configuration.Bind("AzureAd", options))
            .AddCookie();

            services.AddMvc();
            var connection = @"Data Source=hubtask.database.windows.net;Database=PostsDB;Trusted_Connection=False;User ID = hubtaskadmin;Password = P@ssw0rd;Encrypt = True; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            services.AddDbContext<PostContext>(options => options.UseSqlServer(connection));
            services.AddTransient(typeof(IQueryableUnitOfWork), typeof(SqlUnitOfWork));
            services.AddTransient(typeof(System.Lazy<GetPosts>), typeof(System.Lazy<GetPosts>));
            services.AddTransient(typeof(GetPosts), typeof(GetPosts));
            services.AddTransient(typeof(IReadOnlyRepository<Post>), typeof(ReadOnlyRepository<Post>));
            services.AddTransient(typeof(PostContext), typeof(PostContext));
            services.AddSingleton(typeof(IHttpContextAccessor), typeof(HttpContextAccessor));
            services.AddTransient(typeof(IIdentityProviderFactory), typeof(CLPIdentityProviderFactory));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
