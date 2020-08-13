using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Interfaces;
using TestDrive.Models;

namespace TestDrive.Services
{
    public class VeiculoService : IVeiculoService
    {
        const string URL_GET_VEICULOS = "http://aluracar.herokuapp.com/";

        public async Task<IEnumerable<Veiculo>> GetAll()
        {

            HttpClient cliente = new HttpClient();

            var resultado = await cliente.GetStringAsync(URL_GET_VEICULOS);

            var veiculosJson = JsonConvert.DeserializeObject<VeiculoJson[]>(resultado);

            var listReturn = new List<Veiculo>();

            foreach (var veiculoJson in veiculosJson)
            {
                listReturn.Add(new Veiculo
                {
                    Nome = veiculoJson.nome,
                    Preco = veiculoJson.preco
                });
            }

            return listReturn;
        }
    }

    class VeiculoJson
    {
        public string nome { get; set; }
        public int preco { get; set; }
    }
}
