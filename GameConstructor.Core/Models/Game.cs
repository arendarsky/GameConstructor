using GameConstructor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core.Models
{
    public class Game : IGame
    {
        List<Question> _questions;

        List<Characteristic> _characteristics;


        public int Id { get; set; }

        public string Name { get; set; }

        public virtual User User { get; set; }


        public IEnumerable<Question> Questions => _questions;

        public virtual IEnumerable<Characteristic> Characteristics => _characteristics;
    }
}
