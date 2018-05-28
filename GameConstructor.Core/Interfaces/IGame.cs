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
        bool DisplayingInGameMode { get; }
        Picture Picture { get; }
        IEnumerable<Question> GetQuestions { get; }
        IEnumerable<Characteristic> GetCharacteristics { get; }

        void UpdateName(string name);
        void UpdateSource(string source);
        void UpdatePicture(Picture picture);
        void UpdateQuestions(List<Question> questions);
        void UpdateCharacteristics(List<Characteristic> characteristics);
    }
}
