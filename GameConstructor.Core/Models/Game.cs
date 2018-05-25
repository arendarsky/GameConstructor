﻿using GameConstructor.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.DataStorages;

namespace GameConstructor.Core.Models
{
    public class Game : IGame
    {
        public int Popularity { get; set; }
        public int Id { get; set; }
        public string Source { get; set; }
        Context _context;
        public string Name { get; set; }

        bool _loaded;

        public User User { get; set; }
        List<Question> _questions;
        List<Characteristic> _characteristics;
        public List<Question> Questions => _questions;
        public List<Characteristic> Characteristics => _characteristics;

        public void NewCharacteristics(List<Characteristic> characteristics)
        {
            _characteristics = characteristics;
        }
        public void NewQuestions(List<Question> questions)
        {
            _questions = questions;
        }
        public void SaveGame()
        {
            using(_context = new Context())
            {
                _context.Games.Add(this);
                _context.SaveChanges();
            }
        }
    }
}
