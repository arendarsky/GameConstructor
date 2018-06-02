using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;

namespace GameConstructor.Core.Interfaces
{
    public interface IRepository<T>
    {
        IEnumerable<T> Items { get; }
        void Add(T item);
        void Remove(T item);
        void Save();
    }
}
