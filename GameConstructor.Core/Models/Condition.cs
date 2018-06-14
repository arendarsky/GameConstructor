using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameConstructor.Core.Models
{
    public class Condition
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [Required]
        public int ResultId { get; set; }
        [JsonIgnore]
        public virtual Result Result { get; set; }

        public Condition(string text)
        {
            Text = text;
        }

        public Condition() { }
    }
}
