using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.ViewModels;
using System.Windows;

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
                    services.AddSingleton<Semester>();

                    services.AddSingleton<ISnackbarMessageQueue, SnackbarMessageQueue>();

                    //services to be injected 
                    services.AddSingleton<IServiceManager, ServiceManager>();
                    services.AddSingleton<IModuleService, ModuleService>();
                    services.AddSingleton<IModuleSemesterWeekService, ModuleSemesterWeekService>();
                    services.AddSingleton<IStudySessionService, StudySessionService>();

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

            MainWindow = _host.Services.GetService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}