using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestDrive.Models
{
    public class Usuario : BindableBase
    {
        private int id = 0;
        private string nome = "";
        private DateTime dataNascimento = DateTime.Now;
        private string telefone = "";
        private string email = "";
        private ImageSource fotoPerfil = "perfil.png";

        public Usuario() { }
        public Usuario(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            DataNascimento = usuario.DataNascimento;
            Telefone = usuario.Telefone;
            Email = usuario.Email;
        }

        public ImageSource FotoPerfil
        {
            get { return fotoPerfil; }
            set { SetProperty(ref fotoPerfil, value); }
        }
        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Nome { get => nome; set => SetProperty(ref nome, value); }
        public DateTime DataNascimento { get => dataNascimento; set => SetProperty(ref dataNascimento, value); }
        public string Telefone { get => telefone; set => SetProperty(ref telefone, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
    }
}
