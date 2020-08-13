using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using TestDrive.Interfaces;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class AgendamentoViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly IAgendamentoService _agendamentoService;

        public Agendamento Agendamento { get; set; } = new Agendamento();
        public AgendamentoViewModel(INavigationService navigationService, IPageDialogService dialogService, IAgendamentoService agendamentoService)
            : base(navigationService)
        {
            Title = "Agendamento";
            this._dialogService = dialogService;
            this._agendamentoService = agendamentoService;
            AgendarCommand = new Command(async () =>
            {
                if(await dialogService.DisplayAlertAsync("Salvar Agendamento","Deseja mesmo enviar o agendamento?","sim", "não"))
                {
                    if(await _agendamentoService.Salvar(Agendamento))
                    {
                        await _dialogService.DisplayAlertAsync("Agendamento", $"{Agendamento.Veiculo.Nome} agendado com sucesso", "OK");
                        await this.NavigationService.GoBackToRootAsync();
                    }
                    else
                        await _dialogService.DisplayAlertAsync("Agendamento", "Falha ao agendar o test drive! Verifique os dados e tente novamente mais tarde!", "ok");
                }

            }, () =>
            {
                return !string.IsNullOrEmpty(this.Nome)
                 && !string.IsNullOrEmpty(this.Fone)
                 && !string.IsNullOrEmpty(this.Email);
            });
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("veiculo", Agendamento.Veiculo);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            Agendamento.Veiculo = (Veiculo)parameters["veiculo"];
            Title = Agendamento.Veiculo.Nome;
            RaisePropertyChanged(nameof(Agendamento));
        }
        public string Nome
        {
            get {
                return Agendamento.Nome;
            }

            set {
                Agendamento.Nome = value;
                RaisePropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }

        }
        public string Fone
        {
            get {
                return Agendamento.Fone;
            }

            set {
                Agendamento.Fone = value;
                RaisePropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }

        }
        public string Email
        {
            get {
                return Agendamento.Email;
            }

            set {
                Agendamento.Email = value;
                RaisePropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }

        public DateTime DataAgendamento
        {
            get {
                return Agendamento.DataAgendamento;
            }
            set {
                Agendamento.DataAgendamento = value;
            }
        }

        public TimeSpan HoraAgendamento
        {
            get {
                return Agendamento.HoraAgendamento;
            }
            set {
                Agendamento.HoraAgendamento = value;
            }
        }

        public ICommand AgendarCommand { get; set; }

    }
}
