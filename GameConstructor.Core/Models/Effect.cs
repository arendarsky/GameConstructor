using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameConstructor.Core.Models
{
    public class Effect
    {
        public int Id { get; set; }
        public string Body { get; set; }
        [Required]
        public int AnswerId { get; set; }
        [JsonIgnore]
        public virtual List<Influence> Influences { get; set; }

        public int Value { get; set; }
    }
}
