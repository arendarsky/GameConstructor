using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;
using GameConstructor.Core.Interfaces;

namespace GameConstructor.Core.DataStorages
{
    public class DbRepository : IRepository
    {
        Context _context;

        List<Game> _games;

        List<User> _users;


        public IEnumerable<Game> Games => _games;

        public IEnumerable<User> Users => _users;
        

        public DbRepository()
        {
            _context = new Context();
            _games = _context.Games.ToList();
            _users = _context.Users.ToList();
        }
    }
}
