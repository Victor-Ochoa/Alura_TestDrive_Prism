using LiteDB;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TestDrive.Models;

namespace TestDrive.ViewModels
{
    public class MeusAgendamentosViewModel : Core.ViewModelBase
    {
        private readonly ILiteDatabase _liteDatabase;

        private DelegateCommand<Agendamento> _agendamentoTappedCommand;
        public DelegateCommand<Agendamento> AgendamentoTappedCommand =>
            _agendamentoTappedCommand ?? (_agendamentoTappedCommand = new DelegateCommand<Agendamento>(ExecuteAgendamentoTappedCommand));

        void ExecuteAgendamentoTappedCommand(Agendamento agendamento)
        {

        }

        public ObservableCollection<Agendamento> MeusAgendamentos { get; set; }

        public MeusAgendamentosViewModel(INavigationService navigationService, ILiteDatabase liteDatabase)
            : base(navigationService)
        {
            this._liteDatabase = liteDatabase;

            MeusAgendamentos = new ObservableCollection<Agendamento>();
        }

        public override void OnAppearing()
        {
            var agendamentoDb = _liteDatabase.GetCollection<Agendamento>();
            if (MeusAgendamentos.Count != agendamentoDb.Count())
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
}
