using Avalonia.Controls;
using AvaloniaWorkshop.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel; using System.Linq; using System.Threading.Tasks;

namespace AvaloniaWorkshop.ViewModels {     public class MainViewModel : ViewModelBase     {         private UserControl _currentView;          public UserControl CurrentView { get => _currentView; set=> SetProperty(ref _currentView,value); }          public IAsyncRelayCommand GoToLoginCommand { get; }         public IAsyncRelayCommand GoToHomeCommand { get; }         public IAsyncRelayCommand GoToControlsCommand { get; }          public MainViewModel()
        {
            GoToLoginCommand = new AsyncRelayCommand(GoToLoginView);
            GoToHomeCommand = new AsyncRelayCommand(GoToHome);
            GoToControlsCommand = new AsyncRelayCommand(GoToControls);
        }

        private async Task GoToControls()
        {
            await _navService.GoToView<ControlsViewModel>();
        }

        public async Task GoToLoginView()
        {
            await _navService.GoToView<LoginViewModel>();
        }          public async Task GoToHome()
        {
            await _navService.GoToView<HomeViewModel>();
        }     } }