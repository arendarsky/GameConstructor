using GameConstructor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.DataStorages;
using Newtonsoft.Json;
using System.Data.Entity.Migrations;

namespace GameConstructor.Core.Models
{
    public class Game : IGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public bool DisplayingInGameMode { get; set; }
        public int Popularity { get; set; }
        [JsonIgnore]
        public virtual Picture Picture { get; set; }
        [JsonIgnore]
        public virtual List<Question> Questions { get; set; }
        [JsonIgnore]
        public virtual List<Characteristic> Characteristics { get; set; }


        public IEnumerable<Question> GetQuestions => Questions;
        public IEnumerable<Characteristic> GetCharacteristics => Characteristics;
        public Game()
        {

        }
        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateSource(string source)
        {
            Source = source;
        }

        public void UpdatePicture(Picture picture)
        {
            Picture = picture;
        }

        public void UpdateCharacteristics(List<Characteristic> characteristics)
        {
            Characteristics = characteristics;
        }

        public void UpdateQuestions(List<Question> questions)
        {
            Questions = questions;
        }
        
    }
}
