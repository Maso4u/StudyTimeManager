using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudyTimeManager.Repository;
using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services;
using StudyTimeManager.Services.Contracts;
using System;
using System.IO;

namespace StudyTimeManager.WebApp.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relativeDirectory = @"..\..\..\..\Database\";
            string absolutePath = Path.GetFullPath(Path.Combine(baseDirectory, relativeDirectory));

            //AppDomain.CurrentDomain.SetData("DataDirectory", absolutePath);

            ConnectionString = Configuration.GetConnectionString("sqlServerConnection")
                .Replace("[DataDirectory]", absolutePath);
        }

        public IConfiguration Configuration { get; }
        public string ConnectionString { get;}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RepositoryContext>(opt => opt.UseSqlServer(ConnectionString));
            services.AddSingleton(new RepositoryContextFactory(ConnectionString));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddRazorPages();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Forms/Account";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Forbidden/";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };

            MigrateDatabase();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
            });
        }
                
        private void MigrateDatabase()
        {
            DbContextOptions options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlServer(ConnectionString,
                b => b.MigrationsAssembly("StudyTimeManager.WPF.UI")).Options;

            /*DbContextOptions options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlite(connectionString,
                b => b.MigrationsAssembly("StudyTimeManager.WPF.UI"))
                .Options;*/

            RepositoryContext repositoryContext = new RepositoryContext(options);
            repositoryContext.Database.Migrate();
        }
    }
}
