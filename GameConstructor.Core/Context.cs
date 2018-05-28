using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GameConstructor.Core.Models;

namespace GameConstructor.Core
{
    public class Context: DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Effect> Effects { get; set; }
        public DbSet<Influence> Influences { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public Context(): base("GameConstructorDb")
        {

        }
    }
}
