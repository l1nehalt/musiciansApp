using Microsoft.EntityFrameworkCore;
using MusiciansApp.Model;

namespace MusiciansApp.Repository
{
    public class MusicDbContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Track> Tracks { get; set; }

        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options) { }
    }
}
