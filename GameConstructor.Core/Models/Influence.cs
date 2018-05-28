using System;
using System.Collections.Generic;
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
        public int Value { get; set; }
        public void Influenced()
        {
            Characteristic.Value += Value;
        }
    }
}
