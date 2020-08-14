using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TestDrive.Interfaces;
using TestDrive.Models;

namespace TestDrive.ViewModels
{
    public class MasterViewModel : ViewModelBase
    {
        private Usuario _usuario = new Usuario();

        public string Nome
        {
            get { return _usuario.Nome; }
        }
        public string Email
        {
            get { return _usuario.Email; }
        }


        public MasterViewModel(INavigationService navigationService, IMemoryService memoryService)
            : base(navigationService)
        {
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
            _usuario = memoryService.Usuario;
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void NavigateCommandExecuted(string view)
        {
            await _navigationService.NavigateAsync($"/MasterDetail/NavigationPage/{view}");
        }
    }
}
