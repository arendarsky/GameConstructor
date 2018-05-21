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
        List<Game> Games { get; set; }
        List<User> Users { get; set; }
    }
}
