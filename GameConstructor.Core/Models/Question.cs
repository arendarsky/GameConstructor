﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core.Models
{
    class Question
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public List<Answer> Answers { get; set; }
        public Question(string Body)
        {
            this.Body = Body;
            Answers = new List<Answer>();
        }
        public void Answered(Answer answer)
        {
            answer.Answered();
        }
    }
}
