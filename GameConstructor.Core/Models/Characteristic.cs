﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameConstructor.Core.Models
{
    public class Characteristic
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [Required]
        public int GameId { get; set; }
        public int Value { get; set; }
        public List<Influence> Influences { get; set; }
        public Characteristic(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public Characteristic() { }
    }
}
