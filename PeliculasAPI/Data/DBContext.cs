using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entities;

namespace PeliculasAPI.Data
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
    }
}
