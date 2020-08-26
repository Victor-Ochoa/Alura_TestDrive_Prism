using Prism.Mvvm;
using System;
using TestDrive.Core;
using Xamarin.Forms;

namespace TestDrive.Models
{
    public class Usuario : EntityBase
    {
        private string nome = "";
        private DateTime dataNascimento = DateTime.Now;
        private string telefone = "";
        private string email = "";
        private byte[] fotoPerfil = new byte[] { };

        public Usuario() { }
        public Usuario(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            DataNascimento = usuario.DataNascimento;
            Telefone = usuario.Telefone;
            Email = usuario.Email;
            FotoPerfil = usuario.FotoPerfil;
        }

        public byte[] FotoPerfil { get { return fotoPerfil; } set => SetProperty(ref fotoPerfil, value); }
        public string Nome { get => nome; set => SetProperty(ref nome, value); }
        public DateTime DataNascimento { get => dataNascimento; set => SetProperty(ref dataNascimento, value); }
        public string Telefone { get => telefone; set => SetProperty(ref telefone, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
    }
}
