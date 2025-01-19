using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCore.Data
{
    public class AppDbContext( DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1, Title = "INR", Description = "Indian" },
                new Currency() { Id = 2, Title = "USD", Description = "American" },
                new Currency() { Id = 3, Title = "EURO", Description = "European" },
                new Currency() { Id = 4, Title = "Dirham", Description = "UAE" }
            );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = 1, Title = "Hindi", Description = "Indian" },
                new Language() { Id = 2, Title = "English", Description = "American" },
                new Language() { Id = 3, Title = "British", Description = "Britain" },
                new Language() { Id = 4, Title = "Arabic", Description = "Saudi Arabia" }
            );
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookPrice> BooksPrice { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}
