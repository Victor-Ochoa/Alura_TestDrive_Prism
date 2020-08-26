using TestDrive.Interfaces;
using TestDrive.Models;

namespace TestDrive.Services
{
    public class MemoryService : IMemoryService
    {
        public Usuario Usuario { get; set; } = new Usuario();
    }
}
