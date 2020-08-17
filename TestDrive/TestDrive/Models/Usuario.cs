using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDrive.Models
{
    public class Usuario : BindableBase
    {
        private int id = 0;
        private string nome = "";
        private DateTime dataNascimento = DateTime.Now;
        private string telefone = "";
        private string email = "";

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Nome { get => nome; set => SetProperty(ref nome, value); }
        public DateTime DataNascimento { get => dataNascimento; set => SetProperty(ref dataNascimento, value); }
        public string Telefone { get => telefone; set => SetProperty(ref telefone, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
    }
}
