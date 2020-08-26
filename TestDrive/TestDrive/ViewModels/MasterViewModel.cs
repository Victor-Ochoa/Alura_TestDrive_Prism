using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TestDrive.Core;
using TestDrive.Interfaces;
using TestDrive.Models;
using Xamarin.Forms;
using Prism.Navigation.TabbedPages;
using Prism.Common;

namespace TestDrive.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        public MasterViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
