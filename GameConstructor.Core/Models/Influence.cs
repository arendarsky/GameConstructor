using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core.Models
{
    class Influence
    {
        public int Id { get; set; }
        public virtual Characteristic Characteristic { get; set; }
        public int Value { get; set; }
        public void Influenced()
        {
            Characteristic.Level += Value;
        }
    }
}
