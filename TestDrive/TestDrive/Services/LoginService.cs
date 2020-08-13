using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Interfaces;
using TestDrive.Models;

namespace TestDrive.Services
{
    public class LoginService : ILoginService
    {
        private const string URL_LOGIN = "https://aluracar.herokuapp.com/login";

        public async Task<Usuario> Login(string username, string password)
        {
            HttpClient cliente = new HttpClient();

            var conteudo = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("email", username),
                new KeyValuePair<string, string>("senha", password)
            });

            var resposta = await cliente.PostAsync(URL_LOGIN, conteudo);
            if (resposta.IsSuccessStatusCode)
            {
                var json = await resposta.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<JsonReturn>(json);
                return new Usuario()
                {
                    Id = obj.usuario.id,
                    Nome = obj.usuario.nome,
                    DataNascimento = StringToDateTime(obj.usuario.dataNascimento),
                    Email = obj.usuario.email,
                    Telefone = obj.usuario.telefone
                };
            }

            return null;

        }
        private DateTime StringToDateTime(string date)
        {
            var list = date.Split('/').Select(int.Parse).ToArray();
            return new DateTime(list[2], list[1], list[0]);
        }
    }

    public class JsonReturn
    {
        public UsuarioJson usuario { get; set; }
    }

    public class UsuarioJson
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string dataNascimento { get; set; }
        public string telefone { get; set; }
        public string email { get; set; }
    }
}
