using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataBase.Configurations
{
     internal class AuthorConfiguration : IEntityTypeConfiguration<Domain.Entities.Author>
     {
          public void Configure(EntityTypeBuilder<Domain.Entities.Author> builder)
          {
               builder.ToTable(nameof(Author));
               builder.HasKey(a => a.Id);

               builder.Property(a => a.FirstName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
               builder.Property(a => a.LastName).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");

               //Foreign Keys
               builder.HasMany(a => a.Books).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId);

          }
     }
}
