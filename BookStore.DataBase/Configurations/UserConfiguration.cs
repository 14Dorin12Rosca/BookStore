using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataBase.Configurations
{
     internal class UserConfiguration : IEntityTypeConfiguration<Domain.Entities.User>
     {
          public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
          {
               builder.ToTable(nameof(User));
               builder.HasKey(u => u.Id);

               builder.Property(u => u.Email).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");
               builder.Property(u => u.Password).IsRequired().HasColumnType("varchar(max)");
               //Foreign Keys
               builder.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
               builder.HasMany(u => u.Books).WithMany(b => b.Users);
          }
     }
}
