using LiteDB;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Linq;
using TestDrive.Core;
using TestDrive.Interfaces;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly ILoginService _loginService;
        private readonly IPageDialogService _dialogService;
        private readonly IMemoryService _memoryService;
        private readonly ILiteDatabase _liteDatabase;
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
        public LoginViewModel(INavigationService navigationService, ILoginService loginService, IPageDialogService dialogService, IMemoryService memoryService, ILiteDatabase liteDatabase)
            : base(navigationService)
        {
            EntrarCommand = new Xamarin.Forms.Command(EntrarCommandAction, EntrarCommandValidate);
            this._loginService = loginService;
            this._dialogService = dialogService;
            this._memoryService = memoryService;
            this._liteDatabase = liteDatabase;
        }

        public override async void OnAppearing()
        {
            var usuarioDb =_liteDatabase.GetCollection<Usuario>();

            if(usuarioDb.Count() != 0)
            {
                var usuario = usuarioDb.FindAll().FirstOrDefault();

                if (usuario == null)
                    return;

                _memoryService.Usuario = usuario;
                await _navigationService.NavigateAsync("/MasterDetail/NavigationPage/Listagem");
            }
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
                    _liteDatabase.GetCollection<Usuario>().Upsert(usuario);
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
