﻿using Prism.Navigation;
using Prism.Services;
using System;
using System.Windows.Input;
using TestDrive.Core;
using TestDrive.Interfaces;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class AgendamentoViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly IAgendamentoService _agendamentoService;
        private readonly IMemoryService _memoryService;

        public Agendamento Agendamento { get; set; } = new Agendamento();
        public AgendamentoViewModel(INavigationService navigationService, IPageDialogService dialogService, IAgendamentoService agendamentoService, IMemoryService memoryService)
            : base(navigationService)
        {
            Title = "Agendamento";
            this._dialogService = dialogService;
            this._agendamentoService = agendamentoService;
            this._memoryService = memoryService;
            AgendarCommand = new Command(AgendarCommandAction, AgendarCommandValidate);
        }

        private bool AgendarCommandValidate()
        {
            return !string.IsNullOrEmpty(this.Nome)
                             && !string.IsNullOrEmpty(this.Fone)
                             && !string.IsNullOrEmpty(this.Email);
        }

        private async void AgendarCommandAction()
        {
            if (await _dialogService.DisplayAlertAsync("Salvar Agendamento", "Deseja mesmo enviar o agendamento?", "sim", "não"))
            {
                if (await _agendamentoService.Salvar(Agendamento))
                {
                    await _dialogService.DisplayAlertAsync("Agendamento", $"{Agendamento.Veiculo.Nome} agendado com sucesso", "OK");
                    await this._navigationService.GoBackToRootAsync();
                }
                else
                {
                    await _dialogService.DisplayAlertAsync("Agendamento", "Falha ao agendar o test drive! Verifique os dados e tente novamente mais tarde!", "ok");
                    await this._navigationService.GoBackToRootAsync();
                }
            }
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

        public override void OnAppearing()
        {
            Nome = _memoryService.Usuario.Nome;
            Email = _memoryService.Usuario.Email;
            Fone = _memoryService.Usuario.Telefone;
        }

    }
}
