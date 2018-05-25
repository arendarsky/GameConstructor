using GameConstructor.Core.Interfaces;
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
        public List<Question> Questions
        {
            get
            {
                using(_context = new Context())
                {
                    if (!_context.Entry(this).Reference(g => g.Questions).IsLoaded)
                        _context.Entry(this).Reference(g => g.Questions).Load();
                    return _questions;
                }
            }
            set
            {
                _questions = value;
            }
        }
        public List<Characteristic> Characteristics
        {
            get
            {
                using (_context = new Context())
                {
                    if (!_context.Entry(this).Reference(g => g.Characteristics).IsLoaded)
                        _context.Entry(this).Reference(g => g.Characteristics).Load();
                    return _characteristics;
                }
            }
            set
            {
                _characteristics = value;
            }
        }

    }
}
