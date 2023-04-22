using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaWorkshop.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
        }

        public override async Task OnAppearing()
        {
            IsBusy = true;

            await Task.Delay(2000);
            
            IsBusy = false;

        }
    }
}
