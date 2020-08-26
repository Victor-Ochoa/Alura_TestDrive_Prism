using System.Threading.Tasks;
using TestDrive.Models;

namespace TestDrive.Interfaces
{
    public interface IAgendamentoService
    {
        Task<bool> Salvar(Agendamento agendamento);
    }
}
