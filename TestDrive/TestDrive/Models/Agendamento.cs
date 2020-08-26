using System;
using TestDrive.Core;

namespace TestDrive.Models
{
    public class Agendamento : EntityBase
    {
        [LiteDB.BsonRef]
        public Veiculo Veiculo { get; set; }
        public string Nome { get; set; }
        public string Fone { get; set; }
        public string Email { get; set; }

        DateTime dataAgendamento = DateTime.Today;
        public DateTime DataAgendamento
        {
            get {
                return dataAgendamento;
            }
            set {
                dataAgendamento = value;
            }
        }

        public TimeSpan HoraAgendamento { get; set; }
    }
}
