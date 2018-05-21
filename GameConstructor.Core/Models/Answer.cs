using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core.Models
{
    class Answer
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public List<Effect> Effects { get; set; }
        public void Answered()
        {
            foreach(var effect in Effects)
            {
                effect.Influenced();
            }
        }
    }
}
