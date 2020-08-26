using Plugin.Media;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TestDrive.Interfaces;
using TestDrive.Core;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class TabEditarViewModel : ViewModelBase
    {
        private readonly IMemoryService _memoryService;
        private readonly IPageDialogService _pageDialogService;
        private Usuario _usuario = new Usuario();

        public TabEditarViewModel(INavigationService navigationService, IMemoryService memoryService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            this._memoryService = memoryService;
            this._pageDialogService = pageDialogService;
            _usuario = new Usuario(_memoryService.Usuario);

            EditarCommand = new DelegateCommand(() => { IsEditable = true; });
            SalvarCommand = new DelegateCommand(() => {
                _memoryService.Usuario.Nome = _usuario.Nome; 
                _memoryService.Usuario.DataNascimento = _usuario.DataNascimento; 
                _memoryService.Usuario.Email = _usuario.Email; 
                _memoryService.Usuario.Telefone = _usuario.Telefone;
                _memoryService.Usuario.FotoPerfil = _usuario.FotoPerfil;
                IsEditable = false; 
            });
        }

        private DelegateCommand _imageTapCommand;
        public DelegateCommand ImageTapCommand =>
            _imageTapCommand ?? (_imageTapCommand = new DelegateCommand(ImageTapComandAction));

        async void ImageTapComandAction()
        {
            if (!IsEditable)
                return;

            await CrossMedia.Current.Initialize();

            if(!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await _pageDialogService.DisplayAlertAsync("Ops", "Nenhuma câmera detectada.", "ok");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum  = true,
                Directory = "TestDrive",
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front
            });

            if (file == null)
                return;

            FotoPerfil = ImageSource.FromFile(file.Path);
        }

        public string Nome
        {
            get { return _usuario.Nome; }
            set {
                _usuario.Nome = value;
                RaisePropertyChanged();
            }
        }

        public DateTime DataNascimento
        {
            get { return _usuario.DataNascimento; }
            set {
                _usuario.DataNascimento = value;
                RaisePropertyChanged();
            }
        }

        public string Email
        {
            get { return _usuario.Email; }
            set {
                _usuario.Email = value;
                RaisePropertyChanged();
            }
        }

        public string Telefone
        {
            get { return _usuario.Telefone; }
            set {
                _usuario.Telefone = value;
                RaisePropertyChanged();
            }
        }

        public ImageSource FotoPerfil
        {
            get { return _usuario.FotoPerfil; }
            set {
                _usuario.FotoPerfil = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEditable = false;

        public bool IsEditable
        {
            get { return _isEditable; }
            set { SetProperty(ref _isEditable, value); }
        }

        public ICommand EditarCommand { get; }
        public ICommand SalvarCommand { get; }
    }
}
