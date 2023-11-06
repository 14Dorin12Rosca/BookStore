using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataBase.Configurations
{
     internal class BookConfiguration : IEntityTypeConfiguration<Book>
     {
          public void Configure(EntityTypeBuilder<Book> builder)
          {
               builder.ToTable(nameof(Book));
               builder.HasKey(b => b.Id);

               builder.Property(b => b.Title).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
               builder.Property(b => b.Description).IsRequired().HasColumnType("varchar(max)");
               builder.Property(b => b.Price).IsRequired().HasColumnType("decimal(8,2)");
               builder.Property(b => b.Quantity).IsRequired().HasColumnType("decimal(8,2)");
               builder.Property(b => b.AuthorId).IsRequired();
               builder.Property(b => b.GenreId).IsRequired();

               //Foreign Keys
               builder.HasOne(b => b.Author).WithMany(a => a.Books).HasForeignKey(b => b.AuthorId);
               builder.HasOne(b => b.Genre).WithMany(g => g.Books).HasForeignKey(b => b.GenreId);
               builder.HasMany(b => b.Users).WithMany(u => u.Books);
          }
     }
}
