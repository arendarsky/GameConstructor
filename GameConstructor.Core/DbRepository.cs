﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;
using GameConstructor.Core.Interfaces;

namespace GameConstructor.Core
{
    class DbRepository : IRepository
    {
        public List<Game> Games { get; set; }
        public List<User> Users { get; set; }
        Context context { get; set; }
        public DbRepository()
        {
            context = new Context();
            Games = context.Games.ToList();
            Users = context.Users.ToList();
        }
    }
}
