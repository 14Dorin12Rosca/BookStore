using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataBase.Configurations
{
     internal class GenreConfiguration :IEntityTypeConfiguration<Domain.Entities.Genre>
     {
          public void Configure(EntityTypeBuilder<Domain.Entities.Genre> builder)
          {
               builder.ToTable(nameof(Genre));
               builder.HasKey(g => g.Id);

               builder.Property(g => g.Name).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
               builder.Property(g => g.Description).IsRequired().HasColumnType("varchar(max)");

               //Foreign Keys
               builder.HasMany(g => g.Books).WithOne(b => b.Genre).HasForeignKey( b => b.GenreId);

          }
     }
}
