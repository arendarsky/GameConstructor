using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;

namespace GameConstructor.Core.Interfaces
{
    public interface IGame
    {
        int Id { get; }

        string Name { get; }

        IEnumerable<Characteristic> Characteristics { get;}

        User User { get;}

        IEnumerable<Question> Questions { get;}
    }
}
