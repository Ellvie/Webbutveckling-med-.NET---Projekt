using Microsoft.EntityFrameworkCore;

namespace Webbutveckling_med_.NET___Projekt.Data
{
    public class DogContext : DbContext
    {
        public DogContext(DbContextOptions<DogContext> options) : base(options)
        {
        }


        public DbSet<Models.Dog> Dog { get; set; }
        public DbSet<Models.Person> Person { get; set; }

    }
}
