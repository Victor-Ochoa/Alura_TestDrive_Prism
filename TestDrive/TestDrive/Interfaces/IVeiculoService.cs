using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;

namespace TestDrive.Interfaces
{
    public interface IVeiculoService
    {
        Task<IEnumerable<Veiculo>> GetAll();
    }
}
