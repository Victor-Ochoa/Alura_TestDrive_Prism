using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TestDrive.Interfaces;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class TabUsuarioViewModel : ViewModelBase
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

        public TabUsuarioViewModel(INavigationService navigationService, IMemoryService memoryService)
            : base(navigationService)
        {
            NavigateCommand = new DelegateCommand<string>(NavigateCommandAction);
            _usuario = memoryService.Usuario;
            SelectFirstTabCommand = new Command(SelectFirstTabCommandAction);

        }

        public DelegateCommand<string> NavigateCommand { get; }
        public ICommand SelectFirstTabCommand { get; }

        private async void NavigateCommandAction(string view)
        {
            await _navigationService.NavigateAsync($"/MasterDetail/NavigationPage/{view}");
        }

        private void SelectFirstTabCommandAction()
        {
            var currentPage = ((IPageAware)_navigationService).Page;

            TabbedPage tabbedPage = null;
            if (currentPage is TabbedPage current)
            {
                tabbedPage = current;
            }
            else if (currentPage.Parent is TabbedPage parent)
            {
                tabbedPage = parent;
            }
            else if (currentPage.Parent is NavigationPage navPage)
            {
                if (navPage.Parent != null && navPage.Parent is TabbedPage parent2)
                {
                    tabbedPage = parent2;
                }
            }

            tabbedPage.CurrentPage = tabbedPage.Children[1];
        }
    }
}
