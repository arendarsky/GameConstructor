using GameConstructor.Core.DataStorages;
using GameConstructor.Core.Interfaces;
using GameConstructor.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameConstructor.Core
{
    public class Factory
    {
        private static Factory _instance;

        public static Factory Instance => _instance ?? (_instance = new Factory());

        private Factory () { }


        private IGame _game;

        public IGame GetGame => _game ?? (_game = new Game());


        private IRepository _repository;

        public IRepository GetRepository => _repository ?? (_repository = new DbRepository());
    }
}
