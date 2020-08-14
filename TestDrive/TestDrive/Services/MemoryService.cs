using System;
using System.Collections.Generic;
using System.Text;
using TestDrive.Interfaces;
using TestDrive.Models;

namespace TestDrive.Services
{
    public class MemoryService : IMemoryService
    {
        public Usuario Usuario { get; set; } = new Usuario();
    }
}
