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



        public IGame GetGame() => new Game();



        private IStorage _storage;

        public IStorage GetDatabaseStorage() => _storage ?? (_storage = new DatabaseStorage());
        public IStorage GetFileStorage() => _storage ?? (_storage = new FileStorage(false));
        public IStorage GetFileDbStorage() => _storage ?? (_storage = new FileStorage(true));

        //private IRepository _repository;

        //public IRepository GetRepository => _repository ?? (_repository = new Repository());
    }
}
