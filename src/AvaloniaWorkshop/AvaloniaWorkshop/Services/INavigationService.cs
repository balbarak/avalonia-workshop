using AvaloniaWorkshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaWorkshop
{
    public interface INavigationService
    {
        Task GoToView<TViewModel>() where TViewModel : ViewModelBase;
    }
}
