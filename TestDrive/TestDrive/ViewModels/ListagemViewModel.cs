using Newtonsoft.Json;
using Prism.Behaviors;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using TestDrive.Interfaces;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class ListagemViewModel : ViewModelBase
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IPageDialogService _dialogService;

        public ObservableCollection<Veiculo> Veiculos { get; set; }

        private bool aguarde;

        public bool Aguarde
        {
            get { return aguarde; }
            set { SetProperty(ref aguarde, value, "Aguarde"); }
        }

        public ICommand VeiculoTappedCommand { get; set; }

        public ListagemViewModel(INavigationService navigationService, IVeiculoService veiculoService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Listagem";

            this.Veiculos = new ObservableCollection<Veiculo>();
            this.VeiculoTappedCommand = new Command<Veiculo>(veiculo =>
                {
                    var navParameters = new NavigationParameters
                    {
                        { "veiculo", veiculo }
                    };

                    navigationService.NavigateAsync(name: "Detalhe", navParameters);
                });
            this._veiculoService = veiculoService;
            this._dialogService = dialogService;
        }

        public async override void OnAppearing()
        {
            await GetVeiculos();
        }

        public async Task GetVeiculos()
        {
            if (Veiculos.Count != 0)
                return;
            try
            {
                Aguarde = true;

                var list = await _veiculoService.GetAll();

                foreach (var item in list)
                {
                    Veiculos.Add(item);
                }

                Aguarde = false;

            }
            catch (Exception exc)
            {
                await _dialogService.DisplayAlertAsync("Listagem de Veiculos", "Falha ao buscar os veiculos, tente novamente mais tarde!", "ok");
            }
        }
    }
}
