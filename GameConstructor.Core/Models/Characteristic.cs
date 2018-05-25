using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core.Models
{
    public class Characteristic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Level { get; set; }
        public int GameId { get; set; }
    }
}
