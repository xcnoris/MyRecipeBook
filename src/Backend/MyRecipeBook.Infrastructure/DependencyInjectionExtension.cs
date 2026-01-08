using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using MyRecipeBook.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace MyRecipeBook.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfratructure(this IServiceCollection service )
        {
            AddDbContext_SqlServer(service);
            AddRepositories(service);
        }
        
        private static void AddDbContext_SqlServer(IServiceCollection services)
        {
            var connectionString = "Server=AUGUSTO;Database=MyRecipeBook;User Id=SA;Password=123; TrustServerCertificate=True";

            services.AddDbContext<MyRecipeBookDbContext>(dbContextOptions =>
            {
                 dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
