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
        int Popularity { get; }
        string Name { get; }
        string Source { get; }
        User User { get; }
        List<Question> Questions { get; }
        List<Characteristic> Characteristics { get; }

        void NewCharacteristics(List<Characteristic> characteristics);
        void NewQuestions(List<Question> questions);
        void SaveGame();
    }
}
