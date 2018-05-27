using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;

namespace GameConstructor.Core.DataStorages
{
    internal class FileStorage : IStorage
    {
        IRepository<Game> _games;
        IRepository<User> _users;
        bool _loaded;
        bool ForDb;
        public IRepository Games
        {
            get
            {
                Load();
                return _games;
            }
        }
        private void Load()
        {
            if (_loaded)
                return;
            if (ForDb)
            {
                _games = new FileRepository<Game>("GameConstructor.Core/Data/Games.json");
                _users = new FileRepository<User>("GameConstructor.Core/Data/Users.json");
            }
            else
            {
                _games = new FileRepository<Game>("../../../GameConstructor.Core/Data/Games.json");
                _users = new FileRepository<User>("../../../GameConstructor.Core/Data/Users.json");
            }
        }
    }
    
}
