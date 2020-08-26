using LiteDB;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Interfaces;
using TestDrive.Models;

namespace TestDrive.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        const string URL_POST_AGENDAMENTO = "https://aluracar.herokuapp.com/salvaragendamento";
        private readonly ILiteDatabase _liteDatabase;

        public AgendamentoService(ILiteDatabase liteDatabase)
        {
            this._liteDatabase = liteDatabase;
        }

        public async Task<bool> Salvar(Agendamento agendamento)
        {
            HttpClient cliente = new HttpClient();

            var dataHoraAgendamento = new DateTime(
                agendamento.DataAgendamento.Year, agendamento.DataAgendamento.Month, agendamento.DataAgendamento.Day,
                agendamento.HoraAgendamento.Hours, agendamento.HoraAgendamento.Minutes, agendamento.HoraAgendamento.Seconds);

            var json = JsonConvert.SerializeObject(new
            {
                nome = agendamento.Nome,
                fone = agendamento.Fone,
                email = agendamento.Email,
                carro = agendamento.Veiculo.Nome,
                preco = agendamento.Veiculo.Preco,
                dataAgendamento = dataHoraAgendamento
            });

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            var resposta = await cliente.PostAsync(URL_POST_AGENDAMENTO, conteudo);

            agendamento.Enviado = resposta.IsSuccessStatusCode;

            var agendamentoDb = _liteDatabase.GetCollection<Agendamento>();
            agendamentoDb.Upsert(agendamento);

            return resposta.IsSuccessStatusCode;
        }
    }
}
