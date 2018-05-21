﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;

namespace GameConstructor.Core
{
    class Repository
    {
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Characteristic> Properties { get; set; }
    }
}
