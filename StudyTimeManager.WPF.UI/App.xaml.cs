using MaterialDesignThemes.Wpf;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudyTimeManager.Repository;
using StudyTimeManager.Repository.ContextFactory;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.State.Authenticators;
using StudyTimeManager.WPF.UI.ViewModels;
using System;
using System.Configuration;
using System.IO;
using System.Windows;

namespace StudyTimeManager.WPF.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public string ConnectionString { get; private set;}
        public App()
        {
            //configure dependency injection
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(c =>
                {
                    c.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

#if DEBUG
                    c.AddJsonFile("appsettings.Development.json", true, true);
#else
                    builder.AddJsonFile("appsettings.Production.json", true, true);
#endif
                })
                .ConfigureServices((context, services) =>
                {
                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string relativeDirectory = @"..\..\..\..\Database\";
                    string absolutePath = Path.GetFullPath(Path.Combine(baseDirectory, relativeDirectory));

                    //AppDomain.CurrentDomain.SetData("DataDirectory", absolutePath);

                    ConnectionString = context.Configuration.GetConnectionString("sqlServerConnection")
                        .Replace("[DataDirectory]", absolutePath);

                    //services.AddSingleton<IConfiguration>(AddConfiguration());
                    string connectionString = context.Configuration.GetConnectionString("sqlConnection");
                    services.AddDbContext<RepositoryContext>(opt => opt.UseSqlServer(ConnectionString));
                    services.AddSingleton(new RepositoryContextFactory(ConnectionString));
                    services.AddAutoMapper(typeof(MappingProfile));
                    services.AddSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();

                    //Repository
                    services.AddScoped<IRepositoryManager, RepositoryManager>();

                    //services to be injected 
                    services.AddScoped<IServiceManager, ServiceManager>();
                    services.AddScoped<IAuthenticator, Authenticator>();
                    services.AddSingleton<IPasswordHasher, PasswordHasher>();

                    //snackbar message queue to be injected
                    services.AddSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();

                    //ViewModels to be injected via constructors
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<LoginViewModel>();
                    services.AddSingleton<RegisterViewModel>();
                    services.AddSingleton<DashboardViewModel>();
                    services.AddSingleton<SemesterViewModel>();
                    services.AddSingleton<CreateModuleViewModel>();
                    services.AddSingleton<CreateModuleStudySessionViewModel>();
                    services.AddSingleton<ModulesListingViewModel>();
                    services.AddSingleton<ModuleSemesterWeekListingViewModel>();

                    //assign datacontext to the mainwindow to be the main window viewmodel
                    services.AddSingleton<MainWindow>((services) => new MainWindow
                    {
                        DataContext = services.GetRequiredService<MainWindowViewModel>()
                    });

                }).Build();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            MigrateDatabase();
            //_host.Services.GetService<RepositoryContext>()?.Database.Migrate();
            //IServiceManager? serviceManager = _host.Services.GetService<IServiceManager>();
            //serviceManager?.AuthenticationService.Register("MASO","Password@1234", "Password@1234");
            //serviceManager?.AuthenticationService.Login("MASO", "Password@1234");

            MainWindow = _host.Services.GetService<MainWindow>();
            MainWindow?.Show();

            base.OnStartup(e);
        }

        private IConfiguration AddConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

#if DEBUG
            builder.AddJsonFile("appsettings.Development.json", true, true);
#else
            builder.AddJsonFile("appsettings.Production.json", true, true);
#endif
            return builder.Build();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }

        private void MigrateDatabase()
        {
            DbContextOptions options = new DbContextOptionsBuilder<RepositoryContext>()
               .UseSqlServer(ConnectionString).Options;
/*
            DbContextOptions options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlite(ConnectionString,
                b => b.MigrationsAssembly("StudyTimeManager.WPF.UI"))
                .Options;

            using (RepositoryContext context = 
                _host.Services.GetService<RepositoryContext>())
            {
                context.Database.Migrate();
            }
*/
            RepositoryContext repositoryContext = new RepositoryContext(options);
            repositoryContext.Database.Migrate();
        }
    }
}