using Avalonia; using Avalonia.Controls.ApplicationLifetimes; using Avalonia.Markup.Xaml; using AvaloniaWorkshop.Activations;
using AvaloniaWorkshop.ViewModels; using AvaloniaWorkshop.Views; using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; using Microsoft.Extensions.Logging;  namespace AvaloniaWorkshop {     public partial class App : Application     {         public static IHost AppHost { get; private set; }          public App()         {             AppHost = CreateHostBuilder()                 .Build();         }                  public override void Initialize()         {             AvaloniaXamlLoader.Load(this);         }          public async override void OnFrameworkInitializationCompleted()         {             AppHost.Start();              if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)             {                 desktop.MainWindow = new MainWindow                 {                     DataContext = new MainViewModel()                 };             }             else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)             {
                await AppActivationHandler.Activate(singleViewPlatform);             }              base.OnFrameworkInitializationCompleted();         }          private IHostBuilder CreateHostBuilder()
        {
            var result = Host.CreateDefaultBuilder()
               .ConfigureAppConfiguration((hostContext, configApp) =>
               {

               })
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddPageSingleton<MainViewModel,MainView>();

                   services.AddPageTransient<HomeViewModel, HomeView>();
                   services.AddPageTransient<LoginViewModel, LoginView>();

               })
               .ConfigureLogging((hostContext, configLogging) =>
               {


               });

            return result;
        }     } }