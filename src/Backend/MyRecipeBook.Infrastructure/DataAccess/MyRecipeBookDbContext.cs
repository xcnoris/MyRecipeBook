using Microsoft.EntityFrameworkCore;

namespace MyRecipeBook.Infrastructure.DataBase
{
    public class MyRecipeBookDbContext : DbContext
    {
        public MyRecipeBookDbContext(DbContextOptions<MyRecipeBookDbContext> options)
            : base(options){}


        public DbSet<Domain.Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbContext).Assembly);
        }


    }
}
