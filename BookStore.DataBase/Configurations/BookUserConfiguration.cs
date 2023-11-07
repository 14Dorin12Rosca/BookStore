using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataBase.Configurations
{
     /*internal class BookUserConfiguration:IEntityTypeConfiguration<BookUser>
     {
          public void Configure(EntityTypeBuilder<BookUser> builder)
          {
               builder.ToTable(nameof(BookUser));
               builder.HasKey(bu => new { bu.BooksId, bu.UsersId });

               builder.Property(b => b.BooksId).IsRequired();
               builder.Property(b => b.UsersId).IsRequired();

               //Foreign Keys
          }
     }*/
}
