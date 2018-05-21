using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;
using GameConstructor.Core.Interfaces;

namespace GameConstructor.Core.DataStorages
{
    class DbRepository : IRepository
    {
        public List<Game> Games { get; set; }
        public List<User> Users { get; set; }

        Context _context;

        public DbRepository()
        {
            _context = new Context();
            Games = _context.Games.ToList();
            Users = _context.Users.ToList();
        }
    }
}
