using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        //[Required]
        //public int QuestionId { get; set; }
        [JsonIgnore]
        public virtual List<Effect> Effects { get; set; }
    }
}
