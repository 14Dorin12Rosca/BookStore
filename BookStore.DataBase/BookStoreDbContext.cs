using System.Reflection;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase
{
     public class BookStoreDbContext :DbContext
     {
          public virtual DbSet<Author> Author { get; set; }
          public virtual DbSet<Book> Book { get; set; }
          public virtual DbSet<Genre> Genre { get; set; }
          public virtual DbSet<Role> Role { get; set; }
          public virtual DbSet<Domain.Entities.User> User { get; set; }

          public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }
          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
               base.OnModelCreating(modelBuilder);

               modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
          }
          public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
          {
               foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
               {
                    switch (entry.State)
                    {
                         case EntityState.Added:
                              entry.Entity.CreatedOn = DateTime.Now;
                              break;
                         case EntityState.Modified:
                              entry.Entity.ModifiedOn = DateTime.Now;
                              break;
                    }
               }
               return base.SaveChangesAsync(cancellationToken);
          }
     }
}
