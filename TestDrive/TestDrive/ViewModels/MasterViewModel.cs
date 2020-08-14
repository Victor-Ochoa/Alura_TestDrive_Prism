using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDrive.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        public MasterViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void NavigateCommandExecuted(string view)
        {
            await _navigationService.NavigateAsync($"/MasterDetail/NavigationPage/{view}");
        }
    }
}
