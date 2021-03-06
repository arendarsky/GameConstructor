﻿using GameConstructor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.DataStorages;
using Newtonsoft.Json;
using System.Data.Entity.Migrations;
using System.ComponentModel.DataAnnotations;

namespace GameConstructor.Core.Models
{
    public class Game : IGame
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public bool DisplayingInGameMode { get; set; }
        [Required]
        public int UserId { get; set; }
        public int Popularity { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual List<Question> Questions { get; set; }
        public virtual List<Characteristic> Characteristics { get; set; }
        public string Description { get; set; }
        public virtual List<Condition> Conditions { get; set; }
        public virtual List<Result> Results { get; set; }

        [JsonIgnore]
        public IEnumerable<Question> GetQuestions => Questions;
        [JsonIgnore]
        public IEnumerable<Characteristic> GetCharacteristics => Characteristics;
        [JsonIgnore]
        public IEnumerable<Result> GetResults => Results;
        [JsonIgnore]
        public IEnumerable<Condition> GetConditions => Conditions;

        public Game()
        {

        }


        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateSource(string source)
        {
            Source = source;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void UpdatePicture(Picture picture)
        {
            Picture = picture;
        }

        public void UpdateCharacteristics(List<Characteristic> characteristics)
        {
            if (Questions != null)
            {
                foreach (var q in Questions)
                {
                    foreach (var a in q.Answers)
                    {
                        foreach (var e in a.Effects)
                        {
                            e.Influences.RemoveAll(i => characteristics
                            .FirstOrDefault(c => c.Id == i.Characteristic.Id) == null);
                        }
                    }
                }
            }
                        
            Characteristics = characteristics;
        }

        public void UpdateQuestions(List<Question> questions)
        {
            Questions = questions;
        }

        public void UpdateResults(List<Result> results)
        {
            Results = results;
        }
        
        public void UpdateConditions(List<Condition> conditions)
        {
            Conditions = conditions;
        }

        public void UpdatePopularity(int popularity)
        {
            Popularity = popularity;
        }
    }
}
