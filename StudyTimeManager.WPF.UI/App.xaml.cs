using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudyTimeManager.Repository;
using StudyTimeManager.Repository.Contracts;
using StudyTimeManager.Services;
using StudyTimeManager.Services.Contracts;
using StudyTimeManager.WPF.UI.ViewModels;
using System.IO;
using System.Windows;
using StudyTimeManager.WPF.UI.ContextFactory;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace StudyTimeManager.WPF.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            //configure dependency injection
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IConfiguration>(AddConfiguration());

                    services.AddDbContext<RepositoryContext>((services,opt) =>
                    {
                        IConfiguration conf = services.GetRequiredService<IConfiguration>();
                        string connectionString = conf.GetConnectionString("sqlConnection");
                        opt.UseSqlite(connectionString);
                    });

                    services.AddAutoMapper(typeof(MappingProfile));
                    services.AddSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();

                    //Repository
                    services.AddScoped<IRepositoryManager, RepositoryManager>();

                    //services to be injected 
                    services.AddScoped<IServiceManager, ServiceManager>();
                    
                    //snackbar message queue to be injected
                    services.AddSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();

                    //ViewModels to be injected via constructors
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<CreateSemesterViewModel>();
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
            IConfiguration configuration = _host.Services.GetService<IConfiguration>();
            string? connectionString = configuration.GetConnectionString("sqlConnection");

            DbContextOptions options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseSqlite(connectionString, 
                b => b.MigrationsAssembly("StudyTimeManager.WPF.UI"))
                .Options;

            RepositoryContext repositoryContext = new RepositoryContext(options);
            repositoryContext.Database.Migrate();
        }
    }
}