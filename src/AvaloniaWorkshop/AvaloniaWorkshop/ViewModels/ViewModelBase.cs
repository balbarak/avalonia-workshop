using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AvaloniaWorkshop.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        protected INavigationService _navService;

        public ViewModelBase()
        {
            _navService = App.AppHost.Services.GetService<INavigationService>();
        }

        public virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }
    }
}