using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TestDrive.Core;

namespace TestDrive.ViewModels
{
    public class MasterDetailViewModel : ViewModelBase
    {
        public MasterDetailViewModel(INavigationService navigationService)
            : base(navigationService) { }

    }
}
