using System;
using System.Collections.Generic;
using System.Text;

namespace TestDrive.Models
{
    public class Agendamento
    {
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
