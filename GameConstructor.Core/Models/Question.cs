using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameConstructor.Core.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Body { get; set; }
        [Required]
        public int GameId { get; set; }
        [JsonIgnore]
        public virtual List<Answer> Answers { get; set; }


        //public Question(string Body)
        //{
        //    this.Body = Body;
        //    Answers = new List<Answer>();
        //}

        //public Question() { }

    }
}
