using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDrive.Core
{
    public abstract class EntityBase : BindableBase
    {
        [LiteDB.BsonId]
        public int Id { get; set; }
    }
}
