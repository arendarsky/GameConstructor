using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameConstructor.Core.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int QuestionId { get; set; }
        [JsonIgnore]
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
