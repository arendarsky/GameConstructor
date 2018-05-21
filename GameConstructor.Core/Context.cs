using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GameConstructor.Core.Models;

namespace GameConstructor.Core
{
    class Context: DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public List<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Effect> Effects { get; set; }
    }
}
