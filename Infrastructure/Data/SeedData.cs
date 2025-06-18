

using Microsoft.EntityFrameworkCore;
using Data;
using Models;

namespace Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Make sure DB is created
            await context.Database.MigrateAsync();

            // Check if data already exists
            if (!context.Books.Any())
            {
                context.Books.AddRange(new List<Book>
                {
                    new Book { Title = "Clean Code", Author = "Robert C. Martin", Price = 30.0m },
                    new Book { Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Price = 35.0m },
                    new Book { Title = "Design Patterns", Author = "Erich Gamma", Price = 45.0m },
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
