using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataBase.Configurations
{
     internal class RoleConfiguration : IEntityTypeConfiguration<Role>
     {
          public void Configure(EntityTypeBuilder<Role> builder)
          {
               builder.ToTable(nameof(Role));
               builder.HasKey(u => u.Id);
                    
               builder.Property(u => u.Name).IsRequired().HasMaxLength(64).HasColumnType("varchar(64)");

               //Foreign Keys
               builder.HasMany(r => r.Users).WithOne(u => u.Role).HasForeignKey(u => u.RoleId).IsRequired(false);
          }
     }
}
