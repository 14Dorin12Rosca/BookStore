using BookStore.Application.Contracts.User;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataBase.User
{
    internal class SetUserRole : ISetUserRole
    {
        private readonly IDbContextFactory<BookStoreDbContext> _factory;

        public SetUserRole(IDbContextFactory<BookStoreDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<Domain.Entities.User?> SetAsync(string? userEmail, Guid roleId)
        {
            var ctx = await _factory.CreateDbContextAsync();
            var role = await ctx.Role.FindAsync(roleId);
            var user = await ctx.User.FirstOrDefaultAsync(x => x.Email == userEmail);
            if (role != null && user != null)
            {
                user.RoleId = roleId;
                ctx.Update(user);
            }

            var affectedRows = await ctx.SaveChangesAsync();
            return affectedRows > 0 ? user : null;
        }
    }
}
