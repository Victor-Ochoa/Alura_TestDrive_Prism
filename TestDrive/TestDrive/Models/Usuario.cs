using System;
using System.Collections.Generic;
using System.Text;

namespace TestDrive.Models
{
    public class Usuario
    {
        public int Id { get; set; } = 0;
        public string Nome { get; set; } = "";
        public DateTime DataNascimento { get; set; } = DateTime.Now;
        public string Telefone { get; set; } = "";
        public string Email { get; set; } = "";
    }
}
