using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudyTimeManager.Domain.Models;
using StudyTimeManager.Domain.Services;
using StudyTimeManager.Domain.Services.Contracts;
using StudyTimeManager.WPF.UI.ViewModels;
using StudyTimeManager.WPF.UI.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StudyTimeManager.WPF.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private readonly Semester semester;
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<Semester>();

                    services.AddSingleton<IServiceManager, ServiceManager>();
                    services.AddSingleton<IModuleService, ModuleService>();
                    services.AddSingleton<IModuleSemesterWeekService, ModuleSemesterWeekService>();
                    services.AddSingleton<IStudySessionService, StudySessionService>();

                    //ViewModels
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<CreateSemesterViewModel>();
                    services.AddSingleton<CreateModuleViewModel>();
                    services.AddSingleton<CreateModuleStudySessionViewModel>();
                    services.AddSingleton<ModulesListingViewModel>();

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