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
        IEnumerable<Question> Questions { get; }
        IEnumerable<Characteristic> Characteristics { get; }

        void UpdateCharacteristics(List<Characteristic> characteristics);
        void UpdateQuestions(List<Question> questions);
        void SaveGame();
    }
}
