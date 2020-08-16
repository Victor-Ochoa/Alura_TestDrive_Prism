using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using TestDrive.Interfaces;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly ILoginService _loginService;
        private readonly IPageDialogService _dialogService;
        private readonly IMemoryService _memoryService;
        private bool _busy;
        public bool Busy
        {
            get { return _busy; }
            set {
                SetProperty(ref _busy, value);
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        private string _usuario = "joao@alura.com.br";
        public string Usuario
        {
            get { return _usuario; }
            set {
                SetProperty(ref _usuario, value);
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        private string _senha = "alura123";
        public string Senha
        {
            get { return _senha; }
            set {
                SetProperty(ref _senha, value);
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }


        public System.Windows.Input.ICommand EntrarCommand { get; set; }
        public LoginViewModel(INavigationService navigationService, ILoginService loginService, IPageDialogService dialogService, IMemoryService memoryService)
            : base(navigationService)
        {
            EntrarCommand = new Xamarin.Forms.Command(EntrarCommandAction, EntrarCommandValidate);
            this._loginService = loginService;
            this._dialogService = dialogService;
            this._memoryService = memoryService;
        }

        private async void EntrarCommandAction()
        {
            Busy = true;
            try
            {
                var usuario = await _loginService.Login(_usuario, _senha);
                if (usuario != null)
                {
                    _memoryService.Usuario = usuario;
                    await _navigationService.NavigateAsync("/MasterDetail/NavigationPage/Listagem");
                }
                else
                    await _dialogService.DisplayAlertAsync("Login", "Usuário ou senha incorreto, tente novamente.", "ok");

            }
            catch (Exception e)
            {
                await _dialogService.DisplayAlertAsync("Login", "Ocorreu um erro ao logar, por favor, verifique sua conexão e tente novamente.", "ok");
            }
            finally
            {
                Busy = false;
            }

        }

        private bool EntrarCommandValidate() =>
            !string.IsNullOrWhiteSpace(_usuario) &&
            !string.IsNullOrWhiteSpace(_senha) &&
            !_busy;
    }
}
