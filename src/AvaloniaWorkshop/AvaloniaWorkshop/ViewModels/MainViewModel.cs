using Avalonia.Controls;
using AvaloniaWorkshop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel; using System.Linq; using System.Threading.Tasks;

namespace AvaloniaWorkshop.ViewModels {     public class MainViewModel : ViewModelBase     {         private UserControl _currentView;          public UserControl CurrentView { get => _currentView; set=> SetProperty(ref _currentView,value); }          public IAsyncRelayCommand LoginViewCommand { get; }          public MainViewModel()
        {
            LoginViewCommand = new AsyncRelayCommand(GoToLoginView);
        }          public async Task GoToLoginView()
        {
            CurrentView = App.AppHost.Services.GetService<LoginView>();
        }     } }