using LiteDB;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TestDrive.Interfaces;
using TestDrive.Models;

namespace TestDrive.ViewModels
{
    public class MeusAgendamentosViewModel : Core.ViewModelBase
    {
        private readonly ILiteDatabase _liteDatabase;
        private readonly IPageDialogService _pageDialogService;
        private readonly IAgendamentoService _agendamentoService;

        public MeusAgendamentosViewModel(INavigationService navigationService, ILiteDatabase liteDatabase, IPageDialogService pageDialogService, IAgendamentoService agendamentoService)
            : base(navigationService)
        {
            this._liteDatabase = liteDatabase;
            this._pageDialogService = pageDialogService;
            this._agendamentoService = agendamentoService;
            MeusAgendamentos = new ObservableCollection<Agendamento>();
        }


        private DelegateCommand _agendamentoTappedCommand;
        public DelegateCommand AgendamentoTappedCommand =>
            _agendamentoTappedCommand ?? (_agendamentoTappedCommand = new DelegateCommand(ExecuteAgendamentoTappedCommand));

        async void ExecuteAgendamentoTappedCommand()
        {
            if (SelectAgendamento.Enviado || Aguarde)
                return;

            if (await _pageDialogService.DisplayAlertAsync("Agendamento", "Deseja reenviar esse agendamento?", "Sim", "Não"))
            {
                Aguarde = true;

                if (await _agendamentoService.Salvar(SelectAgendamento))
                {
                    await _pageDialogService.DisplayAlertAsync("Agendamento", "Agendamento enviado com sucesso", "Ok");
                    CarregarAgendamentosNaLista(_liteDatabase.GetCollection<Agendamento>());
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Agendamento", "Ocorreu um erro ao enviar o agendamento, tente novamente mais tarde.", "Ok");
                }

                Aguarde = false;

            }
        }


        public ObservableCollection<Agendamento> MeusAgendamentos { get; set; }

        private Agendamento _selectAgendamento;
        public Agendamento SelectAgendamento
        {
            get { return _selectAgendamento; }
            set { SetProperty(ref _selectAgendamento, value); }
        }
        private bool _aguarde;
        public bool Aguarde
        {
            get { return _aguarde; }
            set { SetProperty(ref _aguarde, value); }
        }


        public override void OnAppearing()
        {
            var agendamentoDb = _liteDatabase.GetCollection<Agendamento>();
            if (MeusAgendamentos.Count != agendamentoDb.Count())
            {
                Aguarde = true;

                CarregarAgendamentosNaLista(agendamentoDb);
                
                Aguarde = false;

            }

        }

        private void CarregarAgendamentosNaLista(ILiteCollection<Agendamento> agendamentoDb)
        {
            MeusAgendamentos.Clear();
            var veiculoDb = _liteDatabase.GetCollection<Veiculo>();

            var list = agendamentoDb.FindAll();

            foreach (var item in list)
            {
                item.Veiculo = veiculoDb.FindById(item.Veiculo.Id);
                MeusAgendamentos.Add(item);
            }
        }
    }
}
