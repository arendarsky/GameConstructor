using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core.Models
{
    class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Characteristic> Characteristics { get; set; }
        public virtual User User { get; set; }
        public List<Question> Questions { get; set; }
    }
}
