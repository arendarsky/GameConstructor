using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameConstructor.Core.Models
{
    public class Influence
    {
        public int Id { get; set; }
        public int CharacteristicId { get; set; }
        [JsonIgnore]
        public Characteristic Characteristic { get; set; }
        [Required]
        public int EffectId { get; set; }
        public int Value { get; set; }
    }
}
