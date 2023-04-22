using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaWorkshop.ViewModels;
using AvaloniaWorkshop.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaWorkshop.Activations
{
    public class AppActivationHandler
    {
        public static async Task Activate(ISingleViewApplicationLifetime app)
        {
            var navService = App.AppHost.Services.GetService<INavigationService>();

            var mainView = App.AppHost.Services.GetService<MainView>();

            app.MainView = mainView;

            await navService.GoToView<ShellViewModel>();
        }

        public static async Task ActivateDesktop(IClassicDesktopStyleApplicationLifetime app)
        {
            var navService = App.AppHost.Services.GetService<INavigationService>();

            var mainView = App.AppHost.Services.GetService<MainView>();

            app.MainWindow = new MainWindow();
            app.MainWindow.Content = App.AppHost.Services.GetService<MainView>();

            await navService.GoToView<ShellViewModel>();
        }
    }
}
