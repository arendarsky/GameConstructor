namespace GameConstructor.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using GameConstructor.Core.Interfaces;

    internal sealed class Configuration : DbMigrationsConfiguration<GameConstructor.Core.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GameConstructor.Core.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            //IStorage _fileStorage = Factory.Instance.GetFileStorageForDb();
            //foreach (var u in _fileStorage.Users.Items)
            //    context.Users.AddOrUpdate(c => c.Login, u);
        }
    }
}
