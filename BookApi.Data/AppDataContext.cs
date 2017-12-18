using BookApi.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApi.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext() : base("AppConnectionString")
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<AppDataContext, BookApi.Data.Migrations.Configuration>("AppConnectionString"));
        }

        public DbSet<Book> Books { get; set; }


        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase 
                                                                && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).CreatedDate = DateTime.UtcNow;
                }

                ((EntityBase)entity.Entity).UpdatedDate = DateTime.UtcNow;
            }
        }
    }
}
