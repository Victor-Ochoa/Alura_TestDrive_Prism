using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TestDrive.Core;
using TestDrive.Interfaces;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class TabUsuarioViewModel : ViewModelBase
    {
        private Usuario usuario = new Usuario();
        private readonly IMemoryService _memoryService;

        public string Nome
        {
            get { return Usuario.Nome; }
        }
        public string Email
        {
            get { return Usuario.Email; }
        }
        public Usuario Usuario 
        { 
            get => usuario; 
            set => SetProperty(ref usuario,value); 
        }

        public TabUsuarioViewModel(INavigationService navigationService, IMemoryService memoryService)
            : base(navigationService)
        {
            _memoryService = memoryService;
            Usuario = _memoryService.Usuario;


            NavigateCommand = new DelegateCommand<string>(NavigateCommandAction);
            SelectFirstTabCommand = new Command(SelectFirstTabCommandAction);
        }
        public DelegateCommand<string> NavigateCommand { get; }
        public ICommand SelectFirstTabCommand { get; }

        private async void NavigateCommandAction(string view)
        {
            await _navigationService.NavigateAsync($"/MasterDetail/NavigationPage/{view}", useModalNavigation: false);
        }

        private async void SelectFirstTabCommandAction()
        {
            var nav = await _navigationService.SelectTabAsync("TabEditar");

            if (!nav.Success)
                System.Diagnostics.Debug.WriteLine(nav.Exception);
        }
    }
}
