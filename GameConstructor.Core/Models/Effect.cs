using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameConstructor.Core.Models
{
    public class Effect
    {
        public int Id { get; set; }
        public int AnswerId { get; set; }
        public string Body { get; set; }
        [JsonIgnore]
        public List<Influence> Influences { get; set; }

        public int Value { get; set; }


        public void Influenced()
        {
            foreach ( var influence in Influences)
            {
                influence.Influenced();
            }
        }
    }
}
