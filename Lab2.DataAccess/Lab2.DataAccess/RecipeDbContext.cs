using Microsoft.EntityFrameworkCore;

namespace Lab2.DataAccess
{
    public class RecipeDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Cookbook> Cookbooks { get; set; }

        public RecipeDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(
                "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=D:\\RTU2\\1.semestris\\VisualStudio\\Documents\\Work\\Lab2\\Lab2.DataAccess\\Lab2.DataAccess\\RecipeDb.mdf;Integrated Security = True");
    }

}


