using GameConstructor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.DataStorages;

namespace GameConstructor.Core.Models
{
    public class Game : IGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public int Popularity { get; set; }
        public User User { get; set; }
        public List<Question> Questions { get; set; }
        public virtual List<Characteristic> Characteristics { get; set; }

        public IEnumerable<Question> GetQuestions => Questions;
        public IEnumerable<Characteristic> GetCharacteristics => Characteristics;


        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateSource(string source)
        {
            Source = source;
        }

        public void UpdateCharacteristics(List<Characteristic> characteristics)
        {
            Characteristics = characteristics;
        }

        public void UpdateQuestions(List<Question> questions)
        {
            Questions = questions;
        }


        Context context;

        public void SaveGame()
        {
            using(context = new Context())
            {
                context.Games.Add(this);
                context.SaveChanges();
            }
        }
    }
}
