using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;

namespace GameConstructor.Core.Interfaces
{
    public interface IGameStorage
    {
        IRepository<Question> Questions { get; }
        IRepository<Characteristic> Characteristics { get; }
    }
    public interface IStorage
    {

    }
}
