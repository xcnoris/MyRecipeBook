using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataBase;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        // Usamos o readonly para garantir que o DbContext não seja modificado após a injeção de dependência
        // E apenas o construtor pode inicializá-lo ou modificá-lo
        private readonly MyRecipeBookDbContext _dbContext;

        public UserRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;

        public async Task Add(User user) =>  await _dbContext.Users.AddAsync(user);
        public async Task<bool> ExistActiveUserWithEmail(string email)
        {
            return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);
        }
    }
}
