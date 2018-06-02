using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core.Models
{
    public class Result
    {
        public int Id { get; set; }
        public string Body { get; set; }
        [Required]
        public int GameId { get; set; }
        public List<string> Conditions { get; set; }
    }
}
