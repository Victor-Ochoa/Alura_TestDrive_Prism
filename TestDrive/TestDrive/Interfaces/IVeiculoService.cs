using System.Collections.Generic;
using System.Threading.Tasks;
using TestDrive.Models;

namespace TestDrive.Interfaces
{
    public interface IVeiculoService
    {
        Task<IEnumerable<Veiculo>> GetAll();
    }
}
