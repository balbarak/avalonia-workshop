using Avalonia.Controls.ApplicationLifetimes;
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
            var mainView = App.AppHost.Services.GetService<MainView>();

            app.MainView = mainView;
        }
    }
}
