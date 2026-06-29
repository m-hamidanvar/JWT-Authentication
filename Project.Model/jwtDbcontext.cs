using Microsoft.EntityFrameworkCore;

namespace Project.Model
{
    public class jwtDbcontext:DbContext
    {
        public jwtDbcontext() { }
        public jwtDbcontext(DbContextOptions<jwtDbcontext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
