using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameConstructor.Core.Models;

namespace GameConstructor.Core.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Game> Games { get;}
        IEnumerable<User> Users { get;}
    }
}
