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
    public class TabEditarViewModel : ViewModelBase
    {
        private readonly IMemoryService _memoryService;
        private Usuario _usuario = new Usuario();

        public TabEditarViewModel(INavigationService navigationService, IMemoryService memoryService)
            : base(navigationService)
        {
            this._memoryService = memoryService;
            _usuario = _memoryService.Usuario;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            RaisePropertyChanged(nameof(Nome));
        }

        public string Nome
        {
            get { return _usuario.Nome; }
            set {
                _usuario.Nome = value;
                RaisePropertyChanged();
                _memoryService.Usuario = _usuario;
            }
        }

    }
}
