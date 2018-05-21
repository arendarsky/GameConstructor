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
        int Id { get; set; }
        string Name { get; set; }
        List<Characteristic> Characteristics { get; set; }
        User User { get; set; }
        List<Question> Questions { get; set; }
    }
}
