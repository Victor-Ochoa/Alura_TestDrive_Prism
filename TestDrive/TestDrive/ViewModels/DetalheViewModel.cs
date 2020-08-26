using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TestDrive.Core;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class DetalheViewModel : ViewModelBase
    {
        public Veiculo Veiculo { get; set; } = new Veiculo();

        public System.Windows.Input.ICommand ProximoCommand { get; set; }

        public string TextoFreioABS => $"Freio ABS - {Veiculo.FREIO_ABS:C}";
        public string TextoArCondicionado => $"Ar Condicionado - { Veiculo.AR_CONDICIONADO:C}";
        public string TextoMP3Player => $"MP3 Player - {Veiculo.MP3_PLAYER:C}";

        public bool TemFreioABS
        {
            get {
                return Veiculo.TemFreioABS;
            }
            set { 
                Veiculo.TemFreioABS = value;
                RaisePropertyChanged(nameof(ValorTotal));
            }
        }

        public bool TemArCondicionado
        {
            get {
                return Veiculo.TemArCondicionado;
            }
            set {
                Veiculo.TemArCondicionado = value;
                RaisePropertyChanged(nameof(ValorTotal));
            }
        }

        public bool TemMP3Player
        {
            get {
                return Veiculo.TemMP3Player;
            }
            set {
                Veiculo.TemMP3Player = value;
                RaisePropertyChanged(nameof(ValorTotal));
            }
        }

        public decimal ValorTotal
        {
            get {
                return Veiculo.PrecoTotal;
            }
        }

        public DetalheViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Detalhe";
            ProximoCommand = new Command(() =>
            {
                var navParameters = new NavigationParameters
                    {
                        { "veiculo", Veiculo }
                    };

                navigationService.NavigateAsync(name: "Agendamento", navParameters);
            });
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Veiculo = (Veiculo)parameters["veiculo"];
            Title = Veiculo.Nome;
            RaisePropertyChanged(nameof(ValorTotal));
        }
    }
}
