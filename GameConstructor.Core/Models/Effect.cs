using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core.Models
{
    class Effect
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public virtual List<Influence> Influences { get; set; }
        public double Value { get; set; }
        public void Influenced()
        {
            foreach ( var influence in Influences)
            {
                influence.Influenced();
            }
        }
    }
}
