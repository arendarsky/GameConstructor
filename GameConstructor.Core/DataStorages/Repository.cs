using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;
using GameConstructor.Core.Interfaces;
using System.Data.Entity;

namespace GameConstructor.Core.DataStorages
{
    internal abstract class BaseRepository<T> : IRepository<T>
    {
        protected List<T> _items;
        public IEnumerable<T> Items => _items;
        
        public void Add(T item)
        {
            _items.Add(item);
        }
        public void Remove(T item)
        {
            _items.Remove(item);
        }
    }
   
}
